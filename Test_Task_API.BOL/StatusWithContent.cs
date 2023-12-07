using System.Net;

namespace Test_Task_API.BOL
{
    public class StatusWithContent
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ReasonPhrase { get; set; }
        public object? Content { get; set; }
    }
}
