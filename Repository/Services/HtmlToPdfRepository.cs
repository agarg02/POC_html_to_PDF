
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using RazorLight;
using System.IO;
using System.Net;
using static JSON_To_PDF.Response.Result;

//
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Css.Apply.Impl;
using iText.StyledXmlParser.Css.Media;
using iText.Kernel.Geom;
using iText.Layout.Font;
//using iText.Kernel.Geom;

//


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
                //var result = _razorLightEngine.CompileRenderAsync(filePath, model).GetAwaiter().GetResult();

                //testing with html

                string templateKey = "myUniqueTemplateKey" + DateTime.Now.ToString("dd-H:mm:ss:fff.tt");
                var result = _razorLightEngine.CompileRenderStringAsync(templateKey,
                    htmlContent, model).GetAwaiter().GetResult();

                //testing with html

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

                //

                Byte[] pdfBytes;
                //ConverterProperties converterProperties = new ConverterProperties();
                //converterProperties.SetTagWorkerFactory(new DefaultTagWorkerFactory());
                //converterProperties.SetCssApplierFactory(new DefaultCssApplierFactory());
                //converterProperties.SetMediaDeviceDescription(new MediaDeviceDescription(MediaType.SCREEN));


                //using (var memoryStream = new MemoryStream())
                //{


                //    HtmlConverter.ConvertToPdf(htmlCode, memoryStream, converterProperties);

                //    pdfBytes = memoryStream.ToArray();
                //}


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


                    //PdfWriter pdfWriter = new PdfWriter(memoryStream);
                    //PdfDocument pdfDocument = new PdfDocument(pdfWriter);

                    //PageSize pageSize = PageSize.A4;
                    //pdfDocument.SetDefaultPageSize(pageSize);

                    //// Set margins if needed
                    //float leftMargin = 36;
                    //float rightMargin = 36;
                    //float topMargin = 36;
                    //float bottomMargin = 36;
                    //pdfDocument.SetMargins(leftMargin, rightMargin, topMargin, bottomMargin);


                    //pdfDocument.SetTagged();



                    //HtmlConverter.ConvertToPdf(htmlCode, pdfDocument, converterProperties);
                    //pdfBytes = memoryStream.ToArray();
                }


                //


                //await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

                //var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                //{
                //    Headless = true
                //});

                //var page = await browser.NewPageAsync();

                //await page.SetContentAsync(htmlCode);

                //var pdfOptions = new PdfOptions 
                //{
                //    Format = PuppeteerSharp.Media.PaperFormat.A4, 
                //    PrintBackground = true,
                //    HeaderTemplate = @"<span style='font-size: 30px;background-color: white;color: black;text-align: center;
                //    margin: 0 auto; position: relative;'></span>",
                //    DisplayHeaderFooter = true,
                //    MarginOptions =
                //    {
                //      Top= "40px",
                //      Bottom= "0px",
                //      Left="0px",
                //      Right="0px",
                //    }
                //};

                //var pdfData = await page.PdfDataAsync(pdfOptions);

                //await browser.CloseAsync();

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
