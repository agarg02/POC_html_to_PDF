
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System.Net;
using static JSON_To_PDF.Model.JsonToPdf;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Repository.Services
{
    public class HtmlToPdfRepository : IHtmlToPdfRepository
    {
        #region main calling unit
        public async Task<ResultResponse> GeneratePdfFromModel(Definitions definition, IBrowser browser)
        {
            ResultResponse result = new ResultResponse();
            try
            {               

                if (definition != null)
                {
                    string filePath = @"Views/QualifiedBorrowerReport.html";
                    var readHtml = await ReadLocalFileAsync(filePath);

                    string htmlCode = PopulateHtmlWithDynamicValues(readHtml, definition);
                    var convertedInByte =  await ConvertHtmlToPdf(htmlCode, browser);

                    if (convertedInByte != null)
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
        private string PopulateHtmlWithDynamicValues(string readHtml, Definitions model)
        {
            try
            {
                readHtml = readHtml.Replace("{Name}", model.Consumer?.FirstName + ' ' + model.Consumer?.LastName);

                #region start Report Information
                readHtml = readHtml.Replace("{ReportDate}", model.Consumer?.DateOfBirth);

                readHtml = readHtml.Replace("{Email}", model.Consumer?.Email);

                readHtml = readHtml.Replace("{PhoneNumber}", model.Consumer?.PhoneNumber);

                readHtml = readHtml.Replace("{Street}", model.Address?.Street);
                readHtml = readHtml.Replace("{Street2}", model.Address?.Street2);
                readHtml = readHtml.Replace("{City}", model.Address?.City);
                readHtml = readHtml.Replace("{Region}", model.Address?.Region);
                readHtml = readHtml.Replace("{PostalCode}", model.Address?.PostalCode);
                readHtml = readHtml.Replace("{Country}", model.Address?.Country);
                #endregion  end Report Information


                #region start ATP Analytics
                readHtml = readHtml.Replace("{RikiIndex}", (model.RikiData?.RIKI).ToString());
                readHtml = readHtml.Replace("{RikiTrend}", model.RikiData?.RIKIasWords);
                readHtml = readHtml.Replace("{CashFlowIndex}", (model.RikiData?.CashFlowIndex).ToString());
                readHtml = readHtml.Replace("{CashFlowIndexTrend}", (model.RikiData?.CashFlowIndexTrend).ToString());
                readHtml = readHtml.Replace("{FICOScore}", (model.RikiData?.MonthToMonthStabilityScore).ToString());
                #endregion  end ATP Analytics


                #region start Income Analysis
                readHtml = readHtml.Replace("{TypicalMonthlyIncome}", (model.RikiData?.TypicalMonthsTotalIncome).ToString());
                readHtml = readHtml.Replace("{TypicalMonthlyResidualIncome}", (model.RikiData?.TypicalMonthsAdjustedAvailableIncome).ToString());
                readHtml = readHtml.Replace("{HousingAbilitytoPay}", (model.RikiData?.TypicalMonthsUnadjustedAvailableIncome).ToString());
                readHtml = readHtml.Replace("{Month-to-MonthStabilityScore}", (model.RikiData?.MonthToMonthStabilityScore).ToString());
                readHtml = readHtml.Replace("{MonthlyIncomeTrend}", (model.RikiData?.TotalMonthlyIncomeTrend).ToString());
                #endregion end Income Analysis


                return readHtml;
            }
            catch (Exception ex) 
            { 
                return ex.Message; 
            }
        }
        #endregion


        #region convert html code to pdf
        private async Task<ResultResponse> ConvertHtmlToPdf(string htmlCode, IBrowser browser)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                byte[] pdf;
                var page = await browser.NewPageAsync();

                await page.SetContentAsync(htmlCode);

                var pdfOptions = new PdfOptions
                {
                    Format = PuppeteerSharp.Media.PaperFormat.A3,
                    PrintBackground = true,
                };

                var pdfData = await page.PdfDataAsync(pdfOptions);

                await browser.CloseAsync();

                File.WriteAllBytes("Pdf-Report/QualifiedBorrowerReport.pdf", pdfData);
                pdf = File.ReadAllBytes("Pdf-Report/QualifiedBorrowerReport.pdf");

                result.PdfInByte = pdf;
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
