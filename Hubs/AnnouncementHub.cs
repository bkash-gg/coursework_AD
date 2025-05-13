using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AD_Coursework.Hubs
{
    // This hub is used for sending announcements to all connected members.
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