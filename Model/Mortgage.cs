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

}
