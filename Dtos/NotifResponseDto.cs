using Microsoft.AspNetCore.Mvc;

namespace Foreman_Backend_Notif.Dtos
{
    public class NotificationResponseDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public bool IsRead { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ReceiverId { get; set; }
    }
}
