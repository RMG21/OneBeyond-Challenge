namespace OneBeyondApi.Model
{
    public class BookStock
    {
        public Guid Id { get; set; }
        //public Borrower? OnLoanTo { get; set; }
        public Loan? Loan { get; set; }
    }
}
