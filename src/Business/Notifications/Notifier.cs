using SupplierRegServer.Business.Interfaces;

namespace SupplierRegServer.Business.Notifications;

public class Notifier : INotifier
{
    public List<Notification> _notifications;

    public Notifier()
    {
        _notifications = new List<Notification>();
    }

    public void Handle(Notification notification)
    {
        _notifications.Add(notification);
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotification()
    {
        return _notifications.Any();
    }
}
