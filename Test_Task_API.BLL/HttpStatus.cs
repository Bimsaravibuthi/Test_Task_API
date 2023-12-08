using System.Net;
using Test_Task_API.BOL;

namespace Test_Task_API.BLL
{
    public class HttpStatus
    {
        public Status StatusCodeWithContent(HttpStatusCode statusCode)
        {
            return new Status { status = statusCode, ReasonPhrase = statusCode.ToString()};
        }
        public Status StatusCodeWithContent(HttpStatusCode statusCode, object? content)
        {
            return new Status { status = statusCode, ReasonPhrase = statusCode.ToString(), Content = content};
        }
    }
}
