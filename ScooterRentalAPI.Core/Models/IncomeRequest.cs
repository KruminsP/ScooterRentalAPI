namespace ScooterRentalAPI
{
    public class IncomeRequest
    {
        public int? Year { get; set; }
        public bool IncludeRented { get; set; }

        public IncomeRequest()
        {
            Year = null;
            IncludeRented = false;
        }
    }
}