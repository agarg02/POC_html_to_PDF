using JSON_To_PDF.Model;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Repository.Interfaces
{
    public interface IHtmlToPdfRepository
    {
        public Task<ResultResponse> GeneratePdfFromModel(RikiResultSet rikiResult);

    }
}
