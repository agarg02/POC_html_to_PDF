
using Microsoft.AspNetCore.Mvc;

namespace JSON_To_PDF.Response
{
    public class Result
    {
        public class ResultResponse
        {
            public bool Status { get; set; }
            public string? Message { get; set; }
            public byte[]? PdfInByte { get; set; }

        }
        public class Data
        {
            public object? JsonData { get; set; }
            public string? Htmlcode { get; set; }
            public byte[]? Bytearr { get; set; }
        }

        public class HeaderResponse
        {
            public DateTime ActualValue { get; set; }
            public string FormattedValue { get; set; }

            public int Index { get; set; }
        }
    }
}
