using System;
namespace ApiUltraTest.Application.Interfaces
{
    public interface IMailNotificationService
    {

        void SendNotification(string subject, string body, List<string> mailAddresses);
    }
}

