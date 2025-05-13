using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AD_Coursework.Hubs
{
    [Authorize]
    public class AnnouncementHub : Hub
    {
        public async Task JoinAnnouncementGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "AnnouncementGroup");
        }

        public async Task LeaveAnnouncementGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AnnouncementGroup");
        }
    }
}