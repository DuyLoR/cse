using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using CSE_website.Services;
using CSE_website.Interfaces;

namespace CSE_website.Services;
public class SendMailService : BaseService, ISendMailService
{
    public SendMailService(IWebHostEnvironment environment) : base(environment) {}
    
    public void SendEmail(string to, string subject, string body)
    {
        // Load configuration file
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        // Build IConfigurationBuilder to get mail configuration 
        var configuration = configBuilder.Build();

        // Get email section
        var emailConfiguration = configuration.GetSection("Email");

        // Create MimeMessage object for configuring attribute to send mail which are From, To, Subject, Body, CC, BCC, ...
        var message = new MimeMessage();
        // Configure From
        message.From.Add(new MailboxAddress("Your Name", emailConfiguration["Username"]));
        // Configure To
        message.To.Add(new MailboxAddress("", to));
        // Configure Subject
        message.Subject = subject;

        // Configure Body
        message.Body = new TextPart("html") { Text = body };

        /*
         * Class which is using smtp protocol for sending mail.
         * It provides methods to connect to an SMTP server, authenticate the login and send email
         */
        using var client = new SmtpClient();
        client.Connect(emailConfiguration["SmtpServer"], int.Parse(emailConfiguration["SmtpPort"]), SecureSocketOptions.StartTls);
        client.Authenticate(emailConfiguration["Username"], emailConfiguration["Password"]);
        client.Send(message);
        client.Disconnect(true);
    }
}

