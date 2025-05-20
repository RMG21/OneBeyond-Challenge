namespace OneBeyondApi.Model
{
    public class Borrower
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string EmailAddress { get; set; }
        public List<Loan>? Loans { get; set; }

        public float GetTotalFine(List<Loan>? loans)
        {
            var totalPendingFine = 0.0f;
            if (totalPendingFine != null && loans.Any(x => x.Fine != null))
                foreach (var l in loans)
                {
                    totalPendingFine = (float)(totalPendingFine + l.Fine?.Price);
                }

            Console.WriteLine($"The total accumilation price of all your fines amount to £{totalPendingFine}.");
            return totalPendingFine;
        }
    }
}
