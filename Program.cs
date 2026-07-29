using Foreman_Backend_Notif.Data;
using Microsoft.EntityFrameworkCore;

namespace Foreman_Backend_Notif
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<NotifDbContext>(options => options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                 ServerVersion.AutoDetect(
                    builder.Configuration.GetConnectionString("DefaultConnection"))));

            builder.Services.AddControllers();

            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            //if not in dev mode, only then use Https
            // if (!app.Environment.IsDevelopment())
            // {
            //     app.UseHttpsRedirection();
            // }

            app.UseAuthorization();


            app.MapControllers();
            
            app.MapGet("/", () => "Notification Service is running!");

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<NotifDbContext>();
                db.Database.Migrate();
            }
            
            app.Run();
        }
    }
}
