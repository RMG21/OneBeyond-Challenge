using System.Diagnostics;
using System.IO.Compression;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class OnLoanRepository : IOnLoanRepository
    {
        private readonly ILogger<OnLoanRepository> _logger;
        private readonly IBorrowerRepository _borrowerRepository;

//COMPUTER DOSENT LIKE THE IFineRepository fineRepository DEPENDENCY INJECTION!!!!
        public OnLoanRepository(ILogger<OnLoanRepository> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger; // add logging to all methods
            _borrowerRepository = borrowerRepository;
        }

        public List<Loan>? GetAllLoans()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Loans
                .Include(x => x.Book)
                .Include(x => x.Fine)
                .ToList();
                if (list != null)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Loan>? GetAllActiveLoans()
        {
            using (var context = new LibraryContext())
            {
                var list = GetAllLoans()?.Where(x => x.Active == true).ToList();
                if (list != null)
                {
                    return list;
                }
                else
                {
                    return new List<Loan>();
                }
            }
        }

        public List<Borrower>? GetAllBorrowersWithActiveLoans()
        {
            using (var context = new LibraryContext())
            {
                var returnList = new List<Borrower>();

                try
                {
                    var borrowersWithActiveLoan = context.Borrowers
                    .Include(x => x.Loans)?
                    .ThenInclude(x => x.Fine)?
                    .Include(x => x.Loans)?
                    .ThenInclude(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .ToList();

                    if (borrowersWithActiveLoan != null)
                    {
                        foreach (var b in borrowersWithActiveLoan)
                        {
                            if (b.Loans != null && b.Loans.Any(x => x.Active == true))
                            {
                                returnList.Add(b);
                            }
                        }
                        return returnList;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed! to get all borrowers with active loans. \nMessage: {e.Message} \nStack Trace: {e.StackTrace} Error: {e}");
                }
            }
            return null;
        }

        public List<string>? GetAllBooksOnActiveLoans()
        {
            using (var context = new LibraryContext())
            {
                var returnList = new List<string>();
                var list = GetAllLoans()?.Where(x => x.Active == true).ToList();
                if (list != null)
                {
                    foreach (var l in list)
                    {
                        if (l != null)
                        {
                            returnList.Add(l.Book.Name.ToString());
                        }
                    }
                    return returnList;
                }
                else
                {
                    Console.WriteLine("No Books active loan found!"); //do propper logging
                    return null;
                }
            }
        }

        public Loan? GetLoan(Guid id)
        {
            using (var context = new LibraryContext())
            {
                var loan = GetAllLoans()?
                    .Where(x => x.Id == id)
                    .ToList()
                    .FirstOrDefault();
                return loan ?? null;
            }
        }

        public List<Loan> SearchLoans(LoanSearch? search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Loans
                .Include(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.Book)
                .ThenInclude(x => x.Format)
                .Include(x => x.Fine)
                .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.LoanId.ToString()))
                    {
                        list = list.Where(x => x.Id == search.LoanId);
                    }
                }
                return list.ToList();
            }
        }

        public Loan? ReturnBook(Guid id)
        {

            using (var context = new LibraryContext())
            {
                var loans = context.Loans;
                bool? check = null;
                var returningBook = context.Loans
                                    .Include(x => x.Book)
                                    .ThenInclude(x => x.Author)
                                    .Include(x => x.Fine)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                
                try
                {
                    if (returningBook != null)
                    {
                        if (returningBook.Fine == null)
                        {
                            check = RaiseFineCheck(returningBook.EndDate, returningBook.LoanExtention, returningBook.ExtentionDate);
                            if (check == true)
                            {
                                returningBook.Fine = returningBook.CreateFine(returningBook);
                            } 
                             loans.Update(returningBook);

                            return returningBook;                      
                        }

                        check = RaiseFineCheck(returningBook.EndDate, returningBook.LoanExtention, returningBook.ExtentionDate);

                        if (check == true && returningBook.Fine?.Outstanding == true)
                        {
                            Console.WriteLine($"FINES! MUST BE PAID BEFORE LOAN CAN BE CLOSED!!!\n\nPlease pay Book fine on hand off. \nFine Id: {returningBook.Fine.Id} \nPrice: £{returningBook.Fine.Price}");
                            return null;
                        }
                        else
                        {
                            returningBook.Active = false;
                        } 
                        loans.Update(returningBook); //CHECK WHY ALL OTHER FINES ARE EMPTY AFTER HERE

                        return returningBook;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed! to return book!\nBook ID: {id} \nMessage: {e.Message} \nStack Trace: {e.StackTrace}");
                    //_logger.Log(); //How do i use this logger
                }
            }
            return null;
        }

        public bool? RaiseFineCheck(DateTime endDate, bool loanExtention, DateTime? extentionDate)
        {
            bool? raiseFine = null;
            if (loanExtention == true)
            {
                if (DateTime.Now > extentionDate)
                {
                    raiseFine = true;
                    Console.WriteLine($"Fine will be raised. Due to late return. \nExtended Loan Date: {extentionDate}");// see if you can add logger
                }
            }
            else if (loanExtention != true)
            {
                if (DateTime.Now > endDate)
                {
                    raiseFine = true;
                    Console.WriteLine($"Fine will be raised. Due to late return. \nEnd Loan Date: {endDate}");// see if you can add logger
                }
            }
            else
            {
                raiseFine = false;
            }
            return raiseFine;
        }
    }
}
