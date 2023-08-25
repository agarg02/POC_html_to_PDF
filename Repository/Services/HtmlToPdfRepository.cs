
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using RazorLight;
using System.IO;
using System.Net;
using static JSON_To_PDF.Response.Result;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Css.Apply.Impl;
using iText.StyledXmlParser.Css.Media;
using iText.Kernel.Geom;
using iText.Layout.Font;


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

                    String[] File_Paths = new string[] { "QualifiedBorrowerReport.cshtml" , "RikiReport.cshtml" };

                    foreach (var filepath in File_Paths)
                    {

                        // to read file and return html string

                        string htmlContent = "";

                        using (var reader = new StreamReader(@"Views/" + filepath))
                        {
                            htmlContent = await reader.ReadToEndAsync();
                        }

                        //to read file and return html string

                        string htmlCode = PopulateHtmlWithDynamicValues(htmlContent, rikiResult);
                        var convertedInByte = await ConvertHtmlToPdf(htmlCode);

                        if (convertedInByte != null && convertedInByte.Status && convertedInByte.PdfInByte != null)
                        {
                            result.PdfInByte = convertedInByte.PdfInByte;

                            //to save data to desktop folder directlyy
                            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //get the desktop path
                            string outputPath = System.IO.Path.Combine(desktopPath, "Reports"); // Add subfolder on the desktop
                            string fetchFileNameFromfilepath = System.IO.Path.GetFileNameWithoutExtension(filepath);
                            string outputFilePath = System.IO.Path.Combine(outputPath, fetchFileNameFromfilepath + DateTime.Now.ToString("dd-H.mmtt") + ".pdf");

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

        private string PopulateHtmlWithDynamicValues(string htmlContent, RikiResultSet model)
        {
            try
            {

                string templateKey = "myUniqueTemplateKey" + DateTime.Now.ToString("dd-H:mm:ss:fff.tt");
                var result = _razorLightEngine.CompileRenderStringAsync(templateKey,
                    htmlContent, model).GetAwaiter().GetResult();

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

                Byte[] pdfBytes;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ConverterProperties converterProperties = new ConverterProperties();
                    PdfDocument pdfDocument = new PdfDocument(new PdfWriter(memoryStream));
                    pdfDocument.SetDefaultPageSize(PageSize.A4);



                    FontProvider fontProvider = new FontProvider();
                    fontProvider.AddStandardPdfFonts();
                    converterProperties.SetFontProvider(fontProvider);
                    HtmlConverter.ConvertToPdf(htmlCode, pdfDocument, converterProperties);
                    pdfDocument.Close(); // Close the PdfDocument before converting to a byte array



                    pdfBytes = memoryStream.ToArray();
                }


                result.PdfInByte = pdfBytes;
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
