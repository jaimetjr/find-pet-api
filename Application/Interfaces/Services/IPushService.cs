using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPushService
    {
        Task SendNotificationAsync(string expoPushToken, string title, string body, string data = "");
    }
}
