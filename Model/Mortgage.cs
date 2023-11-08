namespace JSON_To_PDF.Model
{
    public class Mortgage
    {
        public LoanDetails? LoanDetails { get; set; }
        public PersonalDetails? PersonalDetails { get; set; }
        public MortgageAddress? MortgageAddress { get; set; }
    }

    public class MortgageAddress
    {
        public string StreetAddress { get; set; }
        public string? StreetAddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }

    public class PersonalDetails
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? IDNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool HouseStatus { get; set; }
        public int? RentAmount { get; set; }
    }

    public class LoanDetails
    {
        public string TypeOfPurchase { get; set; }
        public int LoanAmount { get; set; }
        public int Term { get; set; }
    }


    public class Loan
    {
        public string LoanType { get; set; }
        public double LoanAmount { get; set; }
        public double CollateralValue { get; set; }
        public string LTV { get; set; }
        public string Terms { get; set; }
        public double ResidualIncome { get; set; }
        public float Riki { get; set; }
        public bool CRA { get; set; }
        public bool DPA { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class PaginationModel
    {
        //public int TotalRecords { get; set; }
        //public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        //public List<T> Data { get; set; }
        public LoanFilterType LoanFilterType { get; set; }
    }

    public class LoanFilterType
    {
        public Dictionary<string, List<string>>? FilterType { get; set; }
    }

    //public class LoanFilter
    //{
    //    public List<string> LoanType { get; set; }
    //    public double MinimumLoanAmount {  get; set;}
    //    public double MaximumLoanAmount { get; set;}
    //    public double MinimumResidualAmount { get; set; }
    //    public double MaximumResidualAmount { get; set; }
    //    public int FromRiki { get; set; }
    //    public int ToRiki { get; set; }
    //    public List<string> DownPaymentProgram { get; set; }
    //}
}
