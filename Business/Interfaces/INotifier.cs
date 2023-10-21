using SupplierRegServer.Business.Notifications;

namespace SupplierRegServer.Business.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}