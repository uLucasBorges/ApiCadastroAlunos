using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2___CadastroAlunos.Domain.Notification
{
    public interface INotificationContext
    {
        bool HasNotifications();
        int Code { get; }
        IReadOnlyCollection<string> GetNotifications();
        void AddNotification(int code, string message);
        void AddNotifications(IEnumerable<string> notifications);
        void AddNotification(int code, object message);
        void CleanNotifications(int code);
        void CleanNotifications();
    }
}
