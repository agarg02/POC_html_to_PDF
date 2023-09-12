using JSON_To_PDF.Validators.CoreValidator;
using JSON_To_PDF.Validators.Interface;

namespace JSON_To_PDF.Validators.Services
{
    public class MortgageValidation : IMortgageValidation
    {
        public AddMortgageValidator AddMortgageValidator { get; set; } = new();

    }
}
