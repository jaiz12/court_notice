using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace BAL.Services.SignalR
{
    public class NotificationAlert : Hub
    {
        public override async Task OnConnectedAsync()
        {
            try
            {
                await Clients.Caller.SendAsync("NewNoticeAlert");
                await Clients.Caller.SendAsync("NewMessageAlert");
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in OnConnectedAsync method: {ex.Message}");
                throw new Exception(ex.Message); // Rethrow the exception to signal the error
            }
        }
    }
}
