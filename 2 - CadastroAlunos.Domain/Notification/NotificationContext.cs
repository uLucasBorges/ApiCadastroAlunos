using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2___CadastroAlunos.Domain.Notification
{
    public class NotificationContext : INotificationContext
    {
        private List<string> _notifications;
        public IReadOnlyCollection<string> GetNotifications() => _notifications;
        public bool HasNotifications() => _notifications.Any();
        public int Code { get; set; }
        public NotificationContext()
        {
            _notifications = new List<string>();
        }

        public void AddNotification(int code, string message)
        {
            Code = code;
            _notifications.Add(message);
        }

        public void AddNotification(int code, object message)
        {
            if (message is string @string)
            {
                Code = code;
                _notifications.Add(@string);
            }

            if (message is List<string> list)
            {
                Code = code;
                foreach (var item in list)
                    _notifications.Add(item);
            }
        }

        public void AddNotifications(IEnumerable<string> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void CleanNotifications(int code)
        {
            if (Code == code)
                _notifications = new List<string>();
        }
        public void CleanNotifications()
        {
            _notifications = new List<string>();
        }

    }
}
