using System.IO.Compression;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class FineRepository : IFineRepository
    {
        private readonly ILogger<FineRepository> _logger;
        private readonly IOnLoanRepository _onLoanRepository;

        public FineRepository()
        {
        }

        public FineRepository(ILogger<FineRepository> logger, IOnLoanRepository onLoanRepository)
        {
            _logger = logger; // add logging to all methods
            _onLoanRepository = onLoanRepository;
        }

        public List<Fine>? GetAllFines()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Fines.ToList();
                if (list != null)
                {
                    return list;
                }
                else
                {
                    Console.WriteLine("No fines available!");
                    return null;
                }
            }
        }

        public Fine? GetFine(Guid id)
        {
            using (var context = new LibraryContext())
            {
                var fine = context.Fines
                    .Where(x => x.Id == id)
                    .ToList()
                    .FirstOrDefault();
                return fine ?? null;
            }
        }

        public Fine PayFine(Guid Id)
        {
            using (var context = new LibraryContext())
            {
                var fines = context.Fines;
                var fineToPay = GetFine(Id);

                try
                {
                    if (fineToPay != null && fineToPay.Outstanding == true)
                    {
                        fineToPay.Outstanding = false;
                        System.Console.WriteLine($"Fine:{fineToPay.Id}\nPrice:{fineToPay.Price}\nFine Date:{fineToPay.FineDate}\nRevocked Status:{fineToPay.FineRevoked}\nFine Status:{fineToPay.Outstanding}\nDate Paid:{DateTime.Now} ");
                        System.Console.WriteLine($"Fine Paid!");

                        fines.Update(fineToPay);

                        return fineToPay;
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"ID:{Id}\nError Messager: {e.Message}\nStack Trace: {e.StackTrace}");
                }
                 return new Fine();
            }
        }

        public List<Fine>? SearchFines(FineSearch search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Fines
                    .Include(x => x.Outstanding == true)
                    .AsQueryable();

                var loan = _onLoanRepository.GetLoan(search.LoanId);
                if (loan != null && loan?.Fine?.Id != null)
                {
                    if (search != null)
                    {
                        if (!string.IsNullOrEmpty(search.LoanId.ToString()))
                        {
                            list = list.Where(x => x.Id == loan.Fine.Id);
                        }
                        if (!string.IsNullOrEmpty(search.FineId.ToString()))
                        {
                            list = list.Where(x => x.Id == search.FineId);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Fines Found!");//Change this for propper logging
                    return null;
                }
                return list?.ToList();
            }
        }
    }
}
