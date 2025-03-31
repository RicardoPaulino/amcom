using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using System.Net;

namespace Questao5.Application.Notifications
{
    public class DomainContextNotification : IDomainContextNotification
    {
        private readonly List<Notification> _notifications = new();

        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

        public void AddNotification(string message, HttpStatusCode statusCode)
        {
            _notifications.Add(new Notification(message, statusCode));
        }

        public bool HasNotifications() => _notifications.Count > 0;
    }
}