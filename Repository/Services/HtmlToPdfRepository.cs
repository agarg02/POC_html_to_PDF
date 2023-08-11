
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using RazorLight;
using System.IO;
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

                    String[] File_Paths = new string[] { "QualifiedBorrowerReport.cshtml", "RikiReport.cshtml" };

                    foreach (var filepath in File_Paths)
                    {

                        string htmlCode = PopulateHtmlWithDynamicValues(filepath, rikiResult);
                        var convertedInByte = await ConvertHtmlToPdf(htmlCode);

                        if (convertedInByte != null && convertedInByte.Status && convertedInByte.PdfInByte != null)
                        {
                            result.PdfInByte = convertedInByte.PdfInByte;

                            //to save data to desktop folder directly
                            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //get the desktop path
                            string outputPath = Path.Combine(desktopPath, "Reports"); // Add subfolder on the desktop
                            string fetchFileNameFromfilepath = System.IO.Path.GetFileNameWithoutExtension(filepath);
                            string outputFilePath = Path.Combine(outputPath, fetchFileNameFromfilepath + DateTime.Now.ToString("dd-H.mmtt") + ".pdf");

                            Directory.CreateDirectory(outputPath); // Create the output directory if it doesn't exist
                                                                   //to save data to desktop folder directly

                            File.WriteAllBytes(outputFilePath, convertedInByte.PdfInByte);

                            Console.WriteLine("PDF saved successfully.");

                        }
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

        private string PopulateHtmlWithDynamicValues(string filePath, RikiResultSet model)
        {
            try
            {
                var result = _razorLightEngine.CompileRenderAsync(filePath, model).GetAwaiter().GetResult();
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
                    Format = PuppeteerSharp.Media.PaperFormat.A4, 
                    PrintBackground = true,
                    HeaderTemplate = @"<span style='font-size: 30px;background-color: white;color: black;text-align: center;
                    margin: 0 auto; position: relative;'></span>",
                    DisplayHeaderFooter = true,
                    MarginOptions =
                    {
                      Top= "40px",
                      Bottom= "0px",
                      Left="0px",
                      Right="0px",
                    }
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


    }
}
