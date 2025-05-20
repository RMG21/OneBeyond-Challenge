using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnLoanController : ControllerBase
    {
        private readonly ILogger<OnLoanController> _logger;
        private readonly IOnLoanRepository _onLoanRepository;

        public OnLoanController(ILogger<OnLoanController> logger, IOnLoanRepository onLoanRepository)
        {
            _logger = logger;
            _onLoanRepository = onLoanRepository;   
        }

        //Check this method properly its returning different value to it in get all
        [HttpGet]
        [Route("GetLoan/{id?}")]
        public Loan? GetLoanById(Guid id)
        {
            return _onLoanRepository.GetLoan(id);
        }

        [HttpGet]
        [Route("GetAllLoans")]
        public IList<Loan>? GetAllLoans()
        {
            return _onLoanRepository.GetAllLoans();
        }

        [HttpGet]
        [Route("GetAllActiveLoans")]
        public IList<Loan>? GetAllActiveLoans()
        {
            return _onLoanRepository.GetAllActiveLoans();
        }

        [HttpGet]
        [Route("GetAllBooksOnActiveLoans")]
        public IList<string>? GetAllBooksOnActiveLoans()
        {
            return _onLoanRepository.GetAllBooksOnActiveLoans();
        }

        [HttpGet]
        [Route("GetAllBorrowersWithActiveLoans")]
        public IList<Borrower>? GetAllBorrowersWithActiveLoans()
        {
            return _onLoanRepository.GetAllBorrowersWithActiveLoans();
        }

        [HttpPost]
        [Route("ReturnBook/{id?}")]
        public Loan? PayFine(Guid id)
        {
            return _onLoanRepository.ReturnBook(id);
        }

        [HttpPost]
        [Route("SearchLoan")]
        public IList<Loan> Post(LoanSearch search)
        {
            return _onLoanRepository.SearchLoans(search);
        }
    }
}