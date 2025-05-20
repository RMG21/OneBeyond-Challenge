using System.ComponentModel.DataAnnotations;
using OneBeyondApi.DataAccess;

namespace OneBeyondApi.Model
{
    public class Loan
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool LoanExtention { get; set; } 
        public DateTime? ExtentionDate { get; set; }
        public Fine? Fine { get; set; }
        public required Book Book { get; set; }

          public Fine CreateFine(Loan loan)
        {
            using (var context = new LibraryContext())
            {
                var fines = context.Fines;
                var fine = new Fine();

                fine.Id = Guid.NewGuid();
                fine.FineDate = DateTime.Now;
                fine.FineRevoked = false;
                fine.Outstanding = true;
                // Find a formula/equation to define finePrice use "price * daysPastEndDate(DateTime.Now - EndDate = daysPastEndDate)@ loan without extention 
                // If (LoanExtention ==true) then "price * daysPastExtendedDate(DateTime.Now - extendedDate)"
                fine.Price = FineCalculator(loan.EndDate, loan.LoanExtention, loan.ExtentionDate);
                fines.Update(fine);
                return fine;
            }
        }

        public float FineCalculator(DateTime endDate, bool loanExtention, DateTime? extentionDate)
        {
            var finePrice = 0.0f;
            var finePricePerHour = 0.10f;
            var daysPastEndDate = DateTime.Now.Subtract(endDate);
            var daysPastExtentionDate = DateTime.Now.Subtract((DateTime)extentionDate) ;

            if (loanExtention != true)
            {
                finePrice = finePricePerHour * (float)daysPastEndDate.TotalHours;
            }
            else
            {
                finePrice = finePricePerHour * (float)daysPastExtentionDate.TotalHours;
            }
            return finePrice;
        }
    }
}
