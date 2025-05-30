﻿using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
        //private readonly IOnLoanRepository _onLoanRepository;
        public BorrowerRepository()
        {
            //_onLoanRepository = onLoanRepository;
        }
        public List<Borrower> GetBorrowers()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Borrowers
                    .Include(x => x.BookStocks)?
                    .ThenInclude(x => x.Loan)?
                    .ThenInclude(x => x.Fine)?
                    .Include(x => x.BookStocks)?
                    .ThenInclude(x => x.Loan)?
                    .ThenInclude(x => x.Book)
                    .ThenInclude(x =>x.Author)
                    .ToList();
                return list;
            }
        }

        public Guid AddBorrower(Borrower borrower)
        {
            using (var context = new LibraryContext())
            {
                context.Borrowers.Add(borrower);
                context.SaveChanges();
                return borrower.Id;
            }
        }

        public void ReserveBook(BookStock bookToReserve)
        {
            //var allActiveLoans =_onLoanRepository.GetAllActiveLoans();
            throw new NotImplementedException();
        }
    }
}
