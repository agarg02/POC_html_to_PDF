using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using static JSON_To_PDF.Model.JsonToPdf;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Repository.Interfaces
{
    public interface IHtmlToPdfRepository
    {
        public Task<ResultResponse> GeneratePdfFromModel(Definitions definition, IBrowser browser);

    }
}
