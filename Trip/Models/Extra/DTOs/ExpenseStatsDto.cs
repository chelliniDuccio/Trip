namespace Trip.Models.Extra.DTOs
{
    public class ExpenseStatsDto
    {
        public TravelExpense TravelExpenses { get; set; }

        public decimal TotalAmount { get; set; }

        public List<UserDto> Users { get; set; }
    }
    public class TravelExpense
    {
        public TravelExpense(TravelTotalExpense travelTotalExpense)
        {
            AmountSum = travelTotalExpense.AmountSum;
            Currency = travelTotalExpense.Currency;
            CurrencySymbol = travelTotalExpense.CurrencySymbol;
        }

        public decimal AmountSum { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
    }

    public class TravelTotalExpense
    {
        public int PaidBy { get; set; }
        public decimal AmountSum { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
    }


}
