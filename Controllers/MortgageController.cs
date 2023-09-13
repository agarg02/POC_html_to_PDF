using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using JSON_To_PDF.Validators.Interface;
using Microsoft.AspNetCore.Mvc;

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
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        /// <summary>
        /// get mortgage data by id
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getMortgageData")]
        public async Task<IActionResult> GetMortgageData(string employeeId)
        {
            try
            {
                if (employeeId != null && employeeId != "")
                {
                    var mortgageData = _mortgageRepository.GetMortgageRecord(employeeId);
                    return Ok(mortgageData);
                }
                return null;
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        /// <summary>
        /// get banking detail
        /// </summary>
        /// <param></param>
        [HttpGet]
        [Route("getBankDetailByEmpid")]
        public async Task<IActionResult> GetBankingDetail(string employerId)
        {
            try
            {
                if(employerId != null && employerId != "")
                {
                    var bankingDetail = _mortgageRepository.GetBankDetailByEmployeeId(employerId);
                    return Ok(bankingDetail);
                }
                return null;
            }
            catch(Exception exception)
            {
                return BadRequest(exception);
            }
        }

    }
}
