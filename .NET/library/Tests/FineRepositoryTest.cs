using Moq;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using Shouldly;
using Xunit;

namespace OneBeyondApi.Tests
{

    public class FineRepositoryTests
    {
        private readonly IFineRepository _fineRepository;
        public FineRepositoryTests(FineRepository fineRepository, IOnLoanRepository onLoanRepository)
        {
             _fineRepository = fineRepository;
        }

        [Fact]
        public void GetAllFines_Should_Return_ListOFines()
        {
            // Arrange

            // Act
            var result = _fineRepository.GetAllFines();

            // Assert
            result.ShouldBeOfType<List<Fine>?>();
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public void GetFine_Should_Return_A_Fine()
        {
            // Arrange
            var Id = new Guid();

            // Act
            var result = _fineRepository.GetFine(Id);

            // Assert
            result.ShouldBeOfType<Fine?>();
        }

        [Fact]
        public void PayFine_Should_Return_A_Fineme()
        {
            var Id = new Guid();

            // Act
            var result = _fineRepository.PayFine(Id);

            // Assert
            result.ShouldBeOfType<Fine>();
            result.Outstanding.ShouldBe(false);
        }

        [Fact]
        public void SearchFines_Should_Return_A_List_Of_Fines()
        {
            var search = new FineSearch { FineId = new Guid(), LoanId = new Guid() };

            // Act
            var result = _fineRepository.PayFine(search.FineId);

            // Assert
            result.ShouldBeOfType<Fine>();
            result.Outstanding.ShouldBe(false);
        }
    }
}