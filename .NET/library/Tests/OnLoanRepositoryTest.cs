using Moq;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using Shouldly;
using Xunit;

namespace OneBeyondApi.Tests
{

    public class OnLoanRepositoryTests
    {
        private readonly IOnLoanRepository _onLoanRepository;
        public OnLoanRepositoryTests(IOnLoanRepository onLoanRepository)
        {
            _onLoanRepository = onLoanRepository;
        }

        [Fact]
        public void GetAllLoans_Should_Return_A_List_Of_Names()
        {
            // Arrange

            // Act
            var result = _onLoanRepository.GetAllLoans();
            // Assert
            result.ShouldBeOfType<List<Loan>?>();
        }

        [Fact]
        public void GetAllActiveLoans_Should_Return_A_List_Of_Names()
        {
            // Arrange

            // Act
            var result = _onLoanRepository.GetAllActiveLoans();
            // Assert
            result.ShouldBeOfType<List<Loan>?>();
        }

        [Fact]
        public void GetAllBorrowersWithActiveLoans_Should_Return_List_Of_Borrowers()
        {
            // Arrange

            // Act
            var result = _onLoanRepository.GetAllActiveLoans();
            // Assert
            result.ShouldBeOfType<List<Borrower>?>();
        }

        [Fact]
        public void GetAllBooksOnActiveLoans_Should_Return_List_Of_Borrowers()
        {
            // Arrange

            // Act
            var result = _onLoanRepository.GetAllBooksOnActiveLoans();
            // Assert
            result.ShouldBeOfType<List<string>?>();
        }

        [Fact]
        public void GetLoan_Should_Return_Loan()
        {
            // Arrange

            // Act
            var result = _onLoanRepository.GetAllBooksOnActiveLoans();
            // Assert
            result.ShouldBeOfType<Loan?>();
        }

        [Fact]
        public void SearchLoan_Should_Return_Loan()
        {
            // Arrange
            var search = new LoanSearch { LoanId = new Guid() };
            // Act
            var result = _onLoanRepository.SearchLoans(search);
            // Assert
            result.ShouldBeOfType<List<Loan>?>();
        }

        [Fact]
        public void ReturnBook_Should_Return_Loan()
        {
            // Arrange
            var Id = new Guid();
            // Act
            var result = _onLoanRepository.ReturnBook(Id);
            // Assert
            result.ShouldBeOfType<List<Loan>?>();
        }

        [Fact]
        public void RaiseFineCheck_Should_Return_Bool()
        {
            // Arrange
            var Id = new Guid();
            // Act
            var result = _onLoanRepository.ReturnBook(Id);
            // Assert
            result.ShouldBeOfType<bool>();
        }
    }
}