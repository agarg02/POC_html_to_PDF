
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using RazorLight;
using System.Net;
using static JSON_To_PDF.Response.Result;


namespace JSON_To_PDF.Repository.Services
{
    public class HtmlToPdfRepository : IHtmlToPdfRepository
    {

        private readonly IRazorLightEngine _razorLightEngine;

        public HtmlToPdfRepository(IRazorLightEngine razorLightEngine)
        {
            _razorLightEngine = razorLightEngine;
        }

        #region main calling unit
        public async Task<ResultResponse> GeneratePdfFromModel(RikiResultSet rikiResult)
        {
            ResultResponse result = new ResultResponse();
            try
            {               

                if (rikiResult != null)
                {
                    string filePath = @"Views/QualifiedBorrowerReport.html";
                    var readHtml = await ReadLocalFileAsync(filePath);

                    string htmlCode = PopulateHtmlWithDynamicValues(readHtml, rikiResult);
                    var convertedInByte =  await ConvertHtmlToPdf(htmlCode);

                    if (convertedInByte != null && convertedInByte.Status)
                    {
                        result.PdfInByte = convertedInByte.PdfInByte;
                    }
                }
            }
            catch (Exception ex) 
            {
                result.Message = ex.Message;
                result.Status = false;
            }
            return result;

        }
        #endregion

        #region populate dynamic value to html

        private string PopulateHtmlWithDynamicValues(string templatePath, RikiResultSet model)
        {
            try
            {
                var result = _razorLightEngine.CompileRenderAsync("QualifiedBorrowerReport.cshtml", model).GetAwaiter().GetResult();
                return result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion


        #region convert html code to pdf
            private async Task<ResultResponse> ConvertHtmlToPdf(string htmlCode)
        {
            ResultResponse result = new ResultResponse();
            try
            {

                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true
                });

                var page = await browser.NewPageAsync();

                await page.SetContentAsync(htmlCode);

                var pdfOptions = new PdfOptions
                {
                    Format = PuppeteerSharp.Media.PaperFormat.A3,
                    PrintBackground = true,
                };

                var pdfData = await page.PdfDataAsync(pdfOptions);

                await browser.CloseAsync();

                result.PdfInByte = pdfData;
                result.Status = true;
                return result;

            }catch(Exception ex)
            {
                result.Message = ex.Message;
                result.Status = false;
            }
            return result;
        }
        #endregion


        #region Read Local File and pass to stream reader
        public static async Task<string> ReadLocalFileAsync(string filePath)
        {
            try
            {
                string fileContent;

                using (var reader = new StreamReader(filePath))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                return fileContent; 
            }
            catch(Exception ex) 
            {   
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
        #endregion

    }
}
