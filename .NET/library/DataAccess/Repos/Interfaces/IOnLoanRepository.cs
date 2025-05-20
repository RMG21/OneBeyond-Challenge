using OneBeyondApi.Model;
namespace OneBeyondApi.DataAccess
{
    public interface IOnLoanRepository
    {
        public List<Loan>? GetAllLoans();
        public List<Loan>? GetAllActiveLoans();

        public Loan? GetLoan(Guid Id);
        public List<Loan> SearchLoans(LoanSearch search);

        public Loan? ReturnBook(Guid Id);

        public List<Borrower>? GetAllBorrowersWithActiveLoans();
        public List<string>? GetAllBooksOnActiveLoans();

    }
}