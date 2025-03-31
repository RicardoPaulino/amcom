using System.Net;

namespace Questao5.Domain.Interfaces
{
    public interface IDomainContextNotification
    {
        void AddNotification(string message, HttpStatusCode statusCode);
    }
}