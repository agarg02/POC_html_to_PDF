using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using JSON_To_PDF.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System;
using System.Net;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HtmlToPdfController : ControllerBase
    {

        private readonly IHtmlToPdfRepository _htmltopdfRepository;
        public HtmlToPdfController(IHtmlToPdfRepository htmltopdfRepository)
        {
            _htmltopdfRepository = htmltopdfRepository;
        }


        /// <summary>
        /// generate pdf from json data using Html file
        /// </summary>
        /// <param name="rikiResult" type="RikiResultSet"></param>
        [HttpPost]
        public async Task<IActionResult> GeneratePdf(RikiResultSet rikiResult)
        {
            ErrorResponse errorResponse;
            ResultResponse result = new ResultResponse();
            try
            {

                var generatedData = _htmltopdfRepository.GeneratePdfFromModel(rikiResult);

                if (generatedData != null && generatedData.Result != null)  
                {
                        result.PdfInByte = generatedData.Result.PdfInByte;
                        result.Message = "Pdf Generated Successfully!!!, Please Check 'Reports' folder on your Desktop";
                        result.Status = true;
                        return Ok(Result<ResultResponse>.Success("Pdf Generated Successfully!!!", result));

                }
                else
                {
                    result.Message = "Pdf not Generated!!!";
                    result.Status = false;
                    return Ok(Result<DBNull>.Failure("Pdf not Generated!!!"));
                }
            }
            catch (Exception ex)
            {           
                errorResponse = new()
                {
                    ErrorCode = 500,
                    Message = ex.Message
                };

                return BadRequest(errorResponse);
            }
        }
    }

}

