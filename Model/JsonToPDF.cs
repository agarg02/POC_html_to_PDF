using System.Net;

namespace JSON_To_PDF.Model
{
    public class RikiResultSet
    {
        public string? rikiId { get; set; }
        public Consumer? consumer { get; set; }
        public List<GroupedTransaction>? groupedTransactions { get; set; }
        public RikiData? rikiData { get; set; }
        public List<Remark>? remarks { get; set; }
        public List<RecurrentItem>? recurrentItems { get; set; }
        public List<CalendarMonthStatistic>? calendarMonthStatistics { get; set; }
    }

    public class Consumer
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public List<Identifier>? identifiers { get; set; }
        public string? dateOfBirth { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public Address? address { get; set; }
        public string? associatedCustomerId { get; set; }
        public string? consumerId { get; set; }
    }

    public class Identifier
    {
        public string? source { get; set; }
        public string? id { get; set; }
    }

    public class Address
    {
        public string? street { get; set; }
        public string? street2 { get; set; }
        public string? city { get; set; }
        public string? region { get; set; }
        public string? postalCode { get; set; }
        public string? country { get; set; }
    }

    public class GroupedTransaction
    {
        public string? groupType { get; set; }
        public List<GroupedAccountDatum>? groupedAccountData { get; set; }
    }

    public class GroupedAccountDatum
    {
        public string? accountNumber { get; set; }
        public string? accountType { get; set; }
        public string? externalAccountId { get; set; }
        public int currentBalance { get; set; }
        public DateTime currentBalanceDate { get; set; }
        public List<Transaction>? transactions { get; set; }
    }

    public class Transaction
    {
        public string? externalTransactionId { get; set; }
        public DateTime date { get; set; }
        public string? description { get; set; }
        public string? action { get; set; }
        public int amount { get; set; }
    }

    public class RikiData
    {
        public string? rikIasWords { get; set; }
        public int riki { get; set; }
        public int monthToMonthStabilityScore { get; set; }
        public int typicalMonthsTotalIncome { get; set; }
        public int typicalMonthsUnadjustedAvailableIncome { get; set; }
        public int typicalMonthsAdjustedAvailableIncome { get; set; }
        public int cashFlowIndex { get; set; }
        public int unadjustedCashFlowIndex { get; set; }
        public int unadjustedRIKI { get; set; }
        public int prospectiveRIKI_500 { get; set; }
        public int totalMonthlyIncomeTrend { get; set; }
        public int cashFlowIndexTrend { get; set; }
        public int ccTrend { get; set; }
        public int ccChangeTrend { get; set; }
        public int bankAccountTrend { get; set; }
        public int bankAccountChangeTrend { get; set; }
    }

    public class Remark
    {
        public string? label { get; set; }
        public string? message { get; set; }
    }

    public class RecurrentItem
    {
        public string? groupType { get; set; }
        public string? description { get; set; }
        public int occurrences { get; set; }
        public int maxConsecutive { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class CalendarMonthStatistic
    {
        public string? label { get; set; }
        public int monthsRequested { get; set; }
        public int monthsDelivered { get; set; }
        public DateTime lastDayOfLastFullMonthCovered { get; set; }
        public DateTime beginningOfFirstMonth { get; set; }
        public List<MonthlyStatistic>? monthlyStatistics { get; set; }
        public List<MonthlyAnalysis>? monthlyAnalyses { get; set; }
    }

    public class MonthlyStatistic
    {
        public int daysInMonth { get; set; }
        public int daysInFullMonth { get; set; }
        public int accountsTracked { get; set; }
        public int incomeTransactions { get; set; }
        public int totalAmountIncomeTransactions { get; set; }
        public int expenseTransactions { get; set; }
        public int totalAmountExpenseTransactions { get; set; }
        public int transactionsNotAcctToAcctTransfers { get; set; }
        public int totalAmountTransactionsNotAcctToAcctTransfers { get; set; }
        public int largestSingleTransactionNotAcctToAcctTransfers { get; set; }
        public int bankAccountsVisible { get; set; }
        public int ddaAccountsVisible { get; set; }
        public int combinedBankAccountBalanceAvg { get; set; }
        public int combinedBankAccountBalanceMin { get; set; }
        public int combinedBankAccountBalanceMax { get; set; }
        public int creditCardAccountsVisible { get; set; }
        public int combinedCreditCardBalanceAvg { get; set; }
        public int combinedCreditCardBalanceMin { get; set; }
        public int combinedCreditCardBalanceMax { get; set; }
        public int creditCardCharges { get; set; }
        public int creditCardPayments { get; set; }
        public int totalAmountCreditCardCharges { get; set; }
        public int totalAmountCreditCardPayments { get; set; }
    }

    public class MonthlyAnalysis
    {
        public int monthlyDataCompleteness { get; set; }
        public int avgDailySpending { get; set; }
        public int incomeExpenseRatio { get; set; }
        public int depletionDays { get; set; }
        public int monthlyAmountChangeScore { get; set; }
        public int monthlyAllocationChangeScore { get; set; }
        public int estimatedDiscretionarySpending { get; set; }
        public int unadjustedAvailableIncome { get; set; }
        public int adjustedAvailableIncome { get; set; }
        public int cashFlowIndexMonthly { get; set; }
        public int unadjustedCashFlowIndexMonthly { get; set; }
        public int prospectiveCashFlowIndexMonthly500 { get; set; }
    }


}

