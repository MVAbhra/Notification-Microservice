using Foreman_Backend_Notif.Models;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace Foreman_Backend_Notif.Services;

public class NotificationService
{
    private readonly IConfiguration _configuration;

    public NotificationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendNotificationEmail(Notification n)
    {
        var client = new MailjetClient(
            _configuration["Mailjet:ApiKey"],
            _configuration["Mailjet:ApiSecret"]);

        var request = new MailjetRequest
        {
            Resource = SendV31.Resource
        };

        var body = new JObject
        {
            ["Messages"] = new JArray
            {
                new JObject
                {
                    ["From"] = new JObject
                    {
                        ["Email"] = _configuration["Mailjet:SenderEmail"],
                        ["Name"] = _configuration["Mailjet:SenderName"]
                    },

                    ["To"] = new JArray
                    {
                        new JObject
                        {
                            ["Email"] = n.ReceiverEmail
                        }
                    },

                    ["Subject"] = n.Title,

                    ["TextPart"] = n.Message,

                    ["HTMLPart"] =
                        $"<h3>{n.Title}</h3><p>{n.Message}</p>"
                }
            }
        };

        // Replace SetBody with assignment to the Body property
        request.Body = body;

        var response = await client.PostAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.GetErrorInfo());
        }
    }
}