using Newtonsoft.Json;
using System.Net;

namespace JSON_To_PDF.Model
{
    public class RikiResultSet
    {
        public string? RikiId { get; set; }
        public Consumer? Consumer { get; set; }
        public List<GroupedTransaction>? GroupedTransactions { get; set; }
        public RikiData? RikiData { get; set; }
        public List<Remark>? Remarks { get; set; }
        public List<RecurrentItem>? RecurrentItems { get; set; }
        public List<CalendarMonthStatistic>? CalendarMonthStatistics { get; set; }
    }

    public class Consumer
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Identifier>? Identifiers { get; set; }
        public string? DateOfBirth { get; set; }
        //public string? Email { get; set; }

        public List<string> Email { get; set; }

        public string? PhoneNumber { get; set; }
        public Address? Address { get; set; }
        public string? AssociatedCustomerId { get; set; }
        public string? ConsumerId { get; set; }
    }

    public class Identifier
    {
        public string? Source { get; set; }
        public string? Id { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }

    public class GroupedTransaction
    {
        public string? GroupType { get; set; }
        public List<GroupedAccountDatum>? GroupedAccountData { get; set; }
    }

    public class GroupedAccountDatum
    {
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? ExternalAccountId { get; set; }
        public float CurrentBalance { get; set; }
        public DateTime CurrentBalanceDate { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }

    public class Transaction
    {
        public string? ExternalTransactionId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; }
        public float Amount { get; set; }
    }

    public class RikiData
    {
        public string? RIKIasWords { get; set; }
        public string RIKI { get; set; }
        public string MonthToMonthStabilityScore { get; set; }
        public float TypicalMonthsTotalIncome { get; set; }
        public float TypicalMonthsUnadjustedAvailableIncome { get; set; }
        public float TypicalMonthsAdjustedAvailableIncome { get; set; }
        public float CashFlowIndex { get; set; }
        public float UnadjustedCashFlowIndex { get; set; }
        public string UnadjustedRIKI { get; set; }
        public string ProspectiveRIKI_500 { get; set; }
        public string TotalMonthlyIncomeTrend { get; set; }
        public string CashFlowIndexTrend { get; set; }
        public string CCTrend { get; set; }
        public string CCChangeTrend { get; set; }
        public string BankAccountTrend { get; set; }
        public float BankAccountChangeTrend { get; set; }
    }

    public class Remark
    {
        public string? Label { get; set; }
        public string? Message { get; set; }
    }

    public class RecurrentItem
    {
        public string? GroupType { get; set; }
        public string? Description { get; set; }
        public int Occurrences { get; set; }
        public int MaxConsecutive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CalendarMonthStatistic
    {
        public string? Label { get; set; }
        public int MonthsRequested { get; set; }
        public int MonthsDelivered { get; set; }
        public DateTime LastDayOfLastFullMonthCovered { get; set; }
        public DateTime BeginningOfFirstMonth { get; set; }
        public List<MonthlyStatistic>? MonthlyStatistics { get; set; }
        public List<MonthlyAnalysis>? MonthlyAnalyses { get; set; }
    }

    public class MonthlyStatistic
    {
        public int DaysInMonth { get; set; }
        public int DaysInFullMonth { get; set; }
        public int AccountsTracked { get; set; }
        public int IncomeTransactions { get; set; }
        public float TotalAmountIncomeTransactions { get; set; }
        public int ExpenseTransactions { get; set; }
        public float TotalAmountExpenseTransactions { get; set; }
        public int TransactionsNotAcctToAcctTransfers { get; set; }
        public float TotalAmountTransactionsNotAcctToAcctTransfers { get; set; }
        public float LargestSingleTransactionNotAcctToAcctTransfers { get; set; }
        public int BankAccountsVisible { get; set; }
        public int DDAAccountsVisible { get; set; }
        public float CombinedBankAccountBalanceAvg { get; set; }
        public float CombinedBankAccountBalanceMin { get; set; }
        public float CombinedBankAccountBalanceMax { get; set; }
        public int CreditCardAccountsVisible { get; set; }
        public float CombinedCreditCardBalanceAvg { get; set; }
        public float CombinedCreditCardBalanceMin { get; set; }
        public float CombinedCreditCardBalanceMax { get; set; }
        public int CreditCardCharges { get; set; }
        public int CreditCardPayments { get; set; }
        public float TotalAmountCreditCardCharges { get; set; }
        public float TotalAmountCreditCardPayments { get; set; }
    }

    public class MonthlyAnalysis
    {
        public float MonthlyDataCompleteness { get; set; }
        public float AvgDailySpending { get; set; }
        public float IncomeExpenseRatio { get; set; }
        public float DepletionDays { get; set; }
        public float MonthlyAmountChangeScore { get; set; }
        public float MonthlyAllocationChangeScore { get; set; }
        public float EstimatedDiscretionarySpending { get; set; }
        public float UnadjustedAvailableIncome { get; set; }
        public float AdjustedAvailableIncome { get; set; }
        public float CashFlowIndexMonthly { get; set; }
        public float UnadjustedCashFlowIndexMonthly { get; set; }
        public float ProspectiveCashFlowIndexMonthly500 { get; set; }
    }


}

