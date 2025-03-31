using System.Net;

namespace Questao5.Domain.Entities
{
    public class Notification
    {
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }

        public Notification(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}