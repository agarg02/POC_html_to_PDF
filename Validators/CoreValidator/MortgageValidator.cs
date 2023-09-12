using FluentValidation;
using Org.BouncyCastle.Ocsp;
using Request = JSON_To_PDF.Model;

namespace JSON_To_PDF.Validators.CoreValidator
{
    public class AddMortgageValidator : AbstractValidator<Request.Mortgage>
    {
        public AddMortgageValidator()
        {
            RuleFor(prop => prop.LoanDetails.TypeOfPurchase).NotEmpty().NotNull();
            RuleFor(prop => prop.LoanDetails.LoanAmount).NotEmpty().NotNull().Must(x => x > 0).WithMessage("Loan Amount must be greater than 0");
            RuleFor(prop => prop.LoanDetails.Term).NotEmpty().NotNull().Must(x => x >= 0).WithMessage("Term can't be negative");

            RuleFor(prop => prop.PersonalDetails.FirstName).NotEmpty().NotNull();
            //RuleFor(prop => prop.PersonalDetails.LastName).NotEmpty().NotNull();
            RuleFor(prop => prop.PersonalDetails.BirthDate).NotEmpty();
            //RuleFor(prop => prop.PersonalDetails.IDNumber).NotEmpty().NotNull();
            RuleFor(prop => prop.PersonalDetails.Email).NotEmpty().NotNull();
            RuleFor(prop => prop.PersonalDetails.PhoneNumber).NotEmpty();
            RuleFor(prop => prop.PersonalDetails.HouseStatus).NotEmpty().NotNull();
            //RuleFor(prop => prop.PersonalDetails.RentAmount).NotEmpty().NotNull().Must(x => x > 0 ).WithMessage("Rent Amount must be greater than 0");

            RuleFor(prop => prop.MortgageAddress.StreetAddress).NotEmpty().NotNull();
            //RuleFor(prop => prop.MortgageAddress.StreetAddressLine2).NotEmpty().NotNull();
            RuleFor(prop => prop.MortgageAddress.City).NotEmpty().NotNull();
            RuleFor(prop => prop.MortgageAddress.State).NotEmpty().NotNull();
            RuleFor(prop => prop.MortgageAddress.PostalCode).NotEmpty().NotNull();
        }
    }
}
