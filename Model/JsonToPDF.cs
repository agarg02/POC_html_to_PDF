namespace JSON_To_PDF.Model
{
    public class JsonToPdf
    {
        public class Address
        {
            public string Street { get; set; } = string.Empty;
            public string Street2 { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Region { get; set; } = string.Empty;
            public string PostalCode { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
        }

        public class CalendarMonthStatistics
        {
            public string? Label { get; set; }
            public int MonthsRequested { get; set; }
            public int MonthsDelivered { get; set; }
            public string? LastDayOfLastFullMonthCovered { get; set; }
            public string? BeginningOfFirstMonth { get; set; }
        }

        public class Consumer
        {
            public string? ConsumerId { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? DateOfBirth { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
            public string? AssociatedCustomerId { get; set; }
        }


        public class GroupedTaggedTransactions
        {
            public string? GroupType { get; set; }
            public Transaction? Transaction { get; set; }
        }

        public class GroupedTransactions
        {
            public RecordData? RecordData { get; set; }
        }

        public class Identifier
        {
            public string? Source { get; set; }
            public string? Id { get; set; }
        }


        public class MonthlyAnalyses
        {
            public int MonthlyDataCompleteness { get; set; }
            public int AvgDailySpending { get; set; }
            public int IncomeExpenseRatio { get; set; }
            public int DepletionDays { get; set; }
            public int MonthlyAllocationChangeScore { get; set; }
            public int EstimatedDiscretionarySpending { get; set; }
            public int UnadjustedAvailableIncome { get; set; }
            public int AdjustedAvailableIncome { get; set; }
            public int CashFlowIndexMonthly { get; set; }
            public int UnadjustedCashFlowIndexMonthly { get; set; }
            public int ProspectiveCashFlowIndexMonthly500 { get; set; }
        }


        public class MonthlyStatistic
        {
            public int DaysInMonth { get; set; }
            public int DaysInFullMonth { get; set; }
            public int AccountsTrancked { get; set; }
            public int IncomeTransactions { get; set; }
            public double TotalAmountIncomeTransactions { get; set; }
            public int ExpenseTransactions { get; set; }
            public double TotalAmountExpenseTransactions { get; set; }
            public int TransactionsNotAcctToAcctTransfers { get; set; }
            public double TotalAmountTransactionsNotAcctToAcctTransfers { get; set; }
            public double LargestSingleTransactionNotAcctToAcctTransfers { get; set; }
            public int BankAccountsVisible { get; set; }
            public int DDAAccountsVisible { get; set; }
            public double CombinedBankAccountBalanceAvg { get; set; }
            public double CombinedBankAccountBalanceMin { get; set; }
            public double CombinedBankAccountBalanceMax { get; set; }
            public int CreditCardAccountsVisible { get; set; }
            public double CombinedCreditCardBalanceAvg { get; set; }
            public double CombinedCreditCardBalanceMin { get; set; }
            public double CombinedCreditCardBalanceMax { get; set; }
            public int CreditCardCharges { get; set; }
            public int CreditCardPayments { get; set; }
            public double TotalAmountCreditCardCharges { get; set; }
            public double TotalAmountCreditCardPayments { get; set; }
        }

        public class RecurrentItems
        {
            public string? GroupType { get; set; }
            public string? Description { get; set; }
            public int Occurrences { get; set; }
            public int MaxConsecutive { get; set; }
            public string? StartDate { get; set; }
            public string? EndDate { get; set; }
        }

        public class Remarks
        {
            public string? Items { get; set; }
        }

        public class ResponseTransaction
        {
            public string? AccountId { get; set; }
            public string? ExternalTransactionId { get; set; }
            public string? Date { get; set; }
            public string? Description { get; set; }
            public string? Action { get; set; }
            public double Amount { get; set; }
        }

        public class RikiData
        {
            public string? RIKIasWords { get; set; }
            public double RIKI { get; set; }
            public double MonthToMonthStabilityScore { get; set; }
            public double TypicalMonthsTotalIncome { get; set; }
            public double TypicalMonthsUnadjustedAvailableIncome { get; set; }
            public double TypicalMonthsAdjustedAvailableIncome { get; set; }
            public double CashFlowIndex { get; set; }
            public double UnadjustedCashFlowIndex { get; set; }
            public double UnadjustedRIKI { get; set; }
            public double ProspectiveRIKI_500 { get; set; }
            public double TotalMonthlyIncomeTrend { get; set; }
            public double CashFlowIndexTrend { get; set; }
            public double CCTrend { get; set; }
            public double CCChangeTrend { get; set; }
            public double BankAccountTrend { get; set; }
            public double BankAccountChangeTrend { get; set; }

        }

        public class RikiResultSet
        {
            public string? ResultSetId { get; set; }
            public Consumer? Consumer { get; set; }
            public GroupedTransactions? GroupedTransactions { get; set; }
            public RikiData? RikiData { get; set; }
            public Remarks? Remarks { get; set; }
            public RecurrentItems? RecurrentItems { get; set; }
            public CalendarMonthStatistics? CalendarMonthStatistics { get; set; }

        }
        public class Transaction
        {
            public RecordData? RecordData { get; set; }
        }
        public class RecordData
        {
            public object? JsonData { get; set; }
            public string? JwtToken { get; set; }
            public string? Bytarr { get; set; }
        }
        public class Definitions
        {
            public Address? Address { get; set; }
            public CalendarMonthStatistics? CalendarMonthStatistics { get; set; }
            public Consumer? Consumer { get; set; }
            public GroupedTaggedTransactions? GroupedTaggedTransactions { get; set; }
            public Identifier? Identifier { get; set; }
            public MonthlyAnalyses? MonthlyAnalyses { get; set; }
            public List<MonthlyStatistic>? MonthlyStatistic { get; set; }
            public RecurrentItems? RecurrentItems { get; set; }
            public ResponseTransaction? ResponseTransaction { get; set; }
            public RikiData? RikiData { get; set; }
        }


    }

}

