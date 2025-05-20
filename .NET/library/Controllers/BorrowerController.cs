using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowerController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerController(ILogger<BorrowerController> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;   
        }

        [HttpGet]
        [Route("GetBorrowers")]
        public IList<Borrower> Get()
        {
            return _borrowerRepository.GetBorrowers();
        }

        [HttpGet]
        [Route("GetTotalFine")]
        public float GetTotalFine(Borrower borrower)
        {
            //return borrower.GetTotalFine(borrower.Loans);
            //get list of loans from all borrowers bookstacks
            var listOfLoans = new List<Loan>();
            foreach (var b in borrower.BookStocks)
            {
                listOfLoans.Add(b.Loan);
            }
            return borrower.GetTotalFine(listOfLoans);
        }

        [HttpPost]
        [Route("AddBorrower")]
        public Guid Post(Borrower borrower)
        {
            return _borrowerRepository.AddBorrower(borrower);
        }
        
    }
}