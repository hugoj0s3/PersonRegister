using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PersonRegistration.Domain.Core
{
    public class NotificationContainer
    {
        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        public IReadOnlyCollection<DomainNotification> Notifications
            => new ReadOnlyCollection<DomainNotification>(_notifications);

        public void Add(DomainNotification notification)
        {
            _notifications.Add(notification);
        }

        public void Add(string key, string value)
        {
            Add(new DomainNotification(key, value));
        }

        public void Add(params DomainNotification[] notifications)
        {
            foreach (var domainNotification in notifications)
            {
                Add(domainNotification);
            }
        }

        public void AddRange(IEnumerable<DomainNotification> notifications)
        {
            Add(notifications.ToArray());
        }

        public bool Remove(DomainNotification notification)
        {
            return _notifications.Remove(notification);
        }

        public bool HasErrors => _notifications.Any();
    }
}
