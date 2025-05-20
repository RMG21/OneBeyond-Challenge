using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FineController : ControllerBase
    {
        private readonly ILogger<FineController> _logger;
        private readonly IFineRepository _fineRepository;

        public FineController(ILogger<FineController> logger, IFineRepository fineRepository)
        {
            _logger = logger;
            _fineRepository = fineRepository;   
        }

        //Check this method properly its returning different value to it in get all
        [HttpGet]
        [Route("GetFine/{id?}")]
        public Fine? GetLoanById(Guid id)
        {
            return _fineRepository.GetFine(id);
        }

        [HttpGet]
        [Route("GetAllFines")]
        public IList<Fine>? GetAllFines()
        {
            return _fineRepository.GetAllFines();
        }

        [HttpPost]
        [Route("CreateFine/{loan?}")]
        public Fine CreateFine(Loan loan)
        {
            return loan.CreateFine(loan);
        }

        [HttpPost]
        [Route("PayFine/{id?}")]
        public Fine? PayFine(Guid id)
        {
            return _fineRepository.PayFine(id);
        }

        [HttpPost]
        [Route("SearchFines")]
        public IList<Fine> Post(FineSearch search)
        {
            return _fineRepository.SearchFines(search);
        }
    }
}