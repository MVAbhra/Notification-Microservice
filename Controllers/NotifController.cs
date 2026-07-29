using Foreman_Backend_Notif.Data;
using Foreman_Backend_Notif.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foreman_Backend_Notif.Services;

namespace Foreman_Backend_Notif.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotifController : ControllerBase
    {
        private readonly NotifDbContext _context;
        private readonly NotificationService _notificationService;

        public NotifController(NotifDbContext context, NotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOneNotification([FromBody] Notification notification)
        {
            Console.WriteLine(notification.ToString());

            notification.CreatedOn = DateTime.UtcNow;
            notification.IsRead = false;

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotificationEmail(notification);

            return CreatedAtAction(
                nameof(GetOneNotification),
                new { id = notification.Id },
                notification);
        }

        [HttpGet("user/{receiverId}")]
        public async Task<IActionResult> GetAllNotifications(long receiverId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.ReceiverId == receiverId)
                .ToListAsync();

            return Ok(notifications);
        }


        [HttpGet("user/{receiverId}/unread")]
        public async Task<IActionResult> GetAllUnreadNotifications(long receiverId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.ReceiverId == receiverId && n.IsRead == false)
                .ToListAsync();

            return Ok(notifications);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneNotification(long id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound($"No notification exists with id {id}!");

            return Ok(notification);
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(long id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound($"No notification exists with id {id}!");

            if (notification.IsRead)
                return Ok(notification);

            notification.IsRead = true;

            await _context.SaveChangesAsync();

            return Ok(notification);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneNotification(long id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound($"No notification exists with id {id}!");

            _context.Notifications.Remove(notification);

            await _context.SaveChangesAsync();

            return Ok($"Notification {id} deleted!");

        }
    }
}