using System.Net;

namespace Test_Task_API.BOL
{
    public class Status
    {
        public HttpStatusCode status { get; set; }
        public string? ReasonPhrase { get; set; }
        public object? Content { get; set; }
    }
}
