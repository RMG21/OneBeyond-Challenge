namespace OneBeyondApi.Model
{
    public class FineSearch
    {
        public Guid FineId { get; set; }
        public Guid LoanId { get; set; } //to allow search for all loans under a borrower

    }
}
