using System.Net;

namespace Questao5.Domain.Enumerators
{
    public enum NotificationType
    {
        NotFound = HttpStatusCode.NotFound,
        BadRequest = HttpStatusCode.BadRequest
    }
}