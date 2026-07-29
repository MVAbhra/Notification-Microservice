using Foreman_Backend_Notif.Models;
using Microsoft.EntityFrameworkCore;

namespace Foreman_Backend_Notif.Data
{
    //DbContext = EntityManager + Persistence Ctx + Hibernate Session
    public class NotifDbContext: DbContext
    {

        public NotifDbContext(DbContextOptions<NotifDbContext> options) : base(options)
        {


        }

        public DbSet<Notification> Notifications { get; set; } 
    }
}
