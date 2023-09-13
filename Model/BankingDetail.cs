namespace JSON_To_PDF.Model
{

    public class EmployeeBankDetail
    {
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string EmployeeType { get; set; }
        public FinancialDetails FinancialDetails { get; set; }
        public BankDetail BankDetail { get; set; }

    }
    public class FinancialDetails
    {
            public string MonthlyIncome { get; set; }
            public string MonthlyExpense { get; set; }
            public string NetWorth { get; set; }
            public string TotalLiailities { get; set; }
            public string Riki { get; set; }
            public string AbilityToPay { get; set; }
            public string CashFlowIndex { get; set; }
    }

    public class BankDetail
    {
        public string BankName { get; set; }
        public string Balance { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public List<CheckIn> CheckIn { get; set; }

    }

    public class CheckIn
    {
        public string AccountNumber { get; set; }
        public string Amount { get; set; }
    }
}
