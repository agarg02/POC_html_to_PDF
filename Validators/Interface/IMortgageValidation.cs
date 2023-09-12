using JSON_To_PDF.Validators.CoreValidator;

namespace JSON_To_PDF.Validators.Interface
{
    public interface IMortgageValidation
    {
        public AddMortgageValidator AddMortgageValidator { get; set; }
    }
}
