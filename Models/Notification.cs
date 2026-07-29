using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foreman_Backend_Notif.Models
{
    [Table("notifs")]
    public class Notification
    {

        [Key]
        [Column("notif_id")]
        public long Id { get; set; }

        [Column("title")]
        public required string Title { get; set; }

        [Column("message")]
        public required string Message { get; set; }

        [Column("is_read")]
        public required bool IsRead { get; set; } = false;

        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column("receiver_id")]
        public long ReceiverId { get; set; }

        [Column("receiver_email")]
        public string ReceiverEmail { get; set; }
    }
}
