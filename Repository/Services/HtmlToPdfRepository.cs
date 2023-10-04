﻿
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
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Borders;
using System;
using iText.Kernel.Font;
using iText.Layout.Properties;
using iText.IO.Font;


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
                        var convertedInByte = await ConvertHtmlToPdf(htmlCode, filepath);

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
            private async Task<ResultResponse> ConvertHtmlToPdf(string htmlCode, string File_Paths)
            {
            ResultResponse result = new ResultResponse();
            try
            {

                byte[] pdfbytes;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ConverterProperties converterProperties = new ConverterProperties();
                    PdfDocument pdfDocument = new PdfDocument(new PdfWriter(memoryStream));
                    pdfDocument.SetDefaultPageSize(PageSize.A4);
                    string htmlHeaderContent = "";
                    if (File_Paths == "RikiReport.cshtml")
                    {
                        htmlHeaderContent = "   <table border-spacing=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" margin-top: 40px; \r\nfont-family: ' Roboto', sans-serif; " +
                            "background-color: #fff;\">\r\n        " +
                            "<tr>\r\n            " +
                            "<td align=\"justify\">\r\n                " +
                            "<span style=\"background: linear-gradient(90deg, rgba(193, 139, 64, 0) -30%, rgba(193, 57, 19, 1) 27%, rgba(36, 112, 230, 1) 46%, rgba(62, 200, 159, 1) 72%, " +
                            "rgba(47, 237, 237, 1) 100%, rgba(245, 250, 255, 1) 100%);\r\n            height:1px;width:565px;display:block;\"></span>\r\n           " +
                            " </td>\r\n            " +
                            "<td align=\"right\"\r\n                style=\"font-family: 'Roboto', sans-serif;letter-spacing: 3px;font-size:25px;color:#000;font-weight:900;width:150px; " +
                            "padding-left:15px;\">\r\n               <b style=\"font-weight:bold;\">FORMFREE<span style=\"position: relative; top: -15px; font-size: 5px;\">®</span></b>          </td>\r\n   " +
                            "\r\n        </tr>\r\n     </ table >\r\n" +
                            "<!-- Include Google Fonts link for Roboto -->\r\n" +
                            "<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700;900&display=swap\">";
                    }
                    else
                    {
                        htmlHeaderContent = "    <table border-spacing=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" margin-top: 40px;\r\nfont-family: \" " +
                            "Roboto\", sans-serif;\r\n           background-color: #fff;\r\n       \">\r\n        <tr>\r\n            " +
                            "<td colspan=\"2\" style=\"font-size: 13px;color:#000;font-family: 'Roboto', sans-serif;\">\r\n                " +
                            "<span style=\"color:#ccc;font-family: 'Roboto', sans-serif;\">QB TOKEN:</span>\r\n N/A\r\n            </td>\r\n        " +
                            "</tr>\r\n        <tr>\r\n            <td align=\"justify\">\r\n                " +
                            "<span style=\"background: linear-gradient(90deg, rgba(193, 139, 64, 0) -30%, rgba(193, 57, 19, 1) 27%, rgba(36, 112, 230, 1) 46%, " +
                            "rgba(62, 200, 159, 1) 72%, rgba(47, 237, 237, 1) 100%, rgba(245, 250, 255, 1) 100%);\r\n            " +
                            "height:1px;width:565px;display:block;\"></span>\r\n            </td>\r\n            " +
                            "<td align=\"right\"\r\n                style=\"font-family: 'Roboto', sans-serif;letter-spacing: 3px;font-size:25px;color:#5c4e4e;" +
                            "font-weight:550;width:136px; padding-left:15px;\">\r\n                FORMFREE<span style=\"position: relative; top: -15px; font-size: 5px;\">®</span>           " +
                            " </td>\r\n        </tr>\r\n    </table>\r\n" +
                            "<!-- Include Google Fonts link for Roboto -->\r\n" +
                            "<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700;900&display=swap\">";
                    }

              


                    // Create a custom page event handler for adding headers
                    pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE,new CustomHeaderEventHandler(htmlHeaderContent));

                    FontProvider fontProvider = new FontProvider();
                    fontProvider.AddStandardPdfFonts();
                    converterProperties.SetFontProvider(fontProvider);
                    HtmlConverter.ConvertToPdf(htmlCode, pdfDocument, converterProperties);
                    pdfDocument.Close(); // Close the pdfDocument before converting to a byte array

                    pdfbytes = memoryStream.ToArray();
                }

                 result.PdfInByte = pdfbytes;
                result.Status = true;
                return result;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = false;
            }
            return result;
            }
        #endregion


        #region
            
        #endregion

    }


    public class CustomHeaderEventHandler : IEventHandler
    {
        private string htmlHeader;

        public CustomHeaderEventHandler(string htmlHeader)
        {
            this.htmlHeader = htmlHeader;
        }
        public void HandleEvent(Event currentEvent)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
            PdfDocument pdfDoc = docEvent.GetDocument();

            int pageNumber = docEvent.GetDocument().GetPageNumber(docEvent.GetPage());

            if (pageNumber >= 1)
            {

                PdfPage page = docEvent.GetPage();

            // Use predefined page size like A4
            PageSize pageSize = PageSize.A4;

            // Create a layout for the header
            float pageWidth = pdfDoc.GetDefaultPageSize().GetWidth();
            float pageHeight = pdfDoc.GetDefaultPageSize().GetHeight();
            float marginLeft = 36; // Adjust this as needed
            float marginRight = 36; // Adjust this as needed
            float marginTop = 36; // Adjust this as needed

            PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);


            // Create a div for the header content
            Div headerDiv = new Div()
                .SetWidth(pageWidth - marginLeft - marginRight)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            //// Add your header content here
            //PdfFont font = PdfFontFactory.CreateFont("Helvetica", PdfEncodings.WINANSI, pdfDoc);
            //Paragraph headerText = new Paragraph("Your Header Text")
            //    .SetFont(font)
            //    .SetFontSize(12);

            //// Add the header text to the header div
            //headerDiv.Add(headerText);

            // Convert the HTML string to iText layout elements
            IList<IElement> elements = HtmlConverter.ConvertToElements(htmlHeader);

            // Add the elements to the header div
            foreach (var element in elements)
            {
                headerDiv.Add((IBlockElement)element);
            }

            // Add the header div to the page
            new Canvas(canvas, PageSize.A4, true)
                .Add(headerDiv);
        }
        }
    }

}
