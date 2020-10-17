using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

namespace Emailer
{
    public class EmailDeliveryService : ScopedBackgroundService
    {
        public EmailDeliveryService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var smtpClient = _scopedServiceProvider.GetService<SmtpClient>();
                var engine = new RazorLightEngineBuilder()
                    .UseEmbeddedResourcesProject((typeof(Program)))
                    .UseMemoryCachingProvider()
                    .Build();

                var emailTemplate =
                    "<div>Hi @Model.Name</div><div>You just won Publisher Clearing House $1,000,000.</div>";
                var recipient = new EmailRecipient
                {
                    Name = "Jane Doe",
                    Email = "jane@gmail.com"
                };

                var emailBody = await engine.CompileRenderStringAsync("templateKey"
                    , emailTemplate, recipient);
                
                var mailMessage = new MailMessage("no-reply@publishersclearinghouse.com", recipient.Email);
                mailMessage.Body = emailBody;
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

                await Console.Out.WriteAsync(("Send email"));
                await Task.Delay(3000, cancellationToken);
            }
        }
    }
}