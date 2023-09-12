using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using JSON_To_PDF.Response;
using JSON_To_PDF.Validators.Interface;
using Microsoft.AspNetCore.Mvc;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MortgageController : ControllerBase
    {

        private readonly IMortgageRepository _mortgageRepository;
        private readonly IMortgageValidation _mortgageValidation;
        public MortgageController(IMortgageRepository mortgageRepository, 
            IMortgageValidation mortgageValidation )
        {
            _mortgageRepository = mortgageRepository;
            _mortgageValidation = mortgageValidation;
        }


        /// <summary>
        /// adding mortgage data
        /// </summary>
        /// <param name="mortgage" type="Mortgage"></param>
        [HttpPost]
        public async Task<IActionResult> AddMortgage(Mortgage mortgage)
        {
            try
            {
                var validation = await _mortgageValidation.AddMortgageValidator.ValidateAsync(mortgage);

                if (validation.IsValid)
                {
                    var mortgagedata = _mortgageRepository.AddMortgageRecord(mortgage);
                   return Ok(mortgagedata);
                }

                return BadRequest(validation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
