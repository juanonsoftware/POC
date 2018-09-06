using FluentEmail.Core;
using FluentEmail.Smtp;
using System;
using System.Net;
using System.Net.Mail;

namespace FluentEmailTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var cred = new NetworkCredential(
                Environment.GetEnvironmentVariable("GmailTestUserName"),
                Environment.GetEnvironmentVariable("GmailTestPassword")
            );

            Email.DefaultSender = new SmtpSender(new SmtpClient()
            {
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = cred,
                DeliveryFormat = SmtpDeliveryFormat.International,
            });

            var r = Email.From(Environment.GetEnvironmentVariable("TestAddress_From"), "From a TESTER")
                 .To(Environment.GetEnvironmentVariable("TestAddress_TO"))
                 .Subject("TEST MAIL")
                 .UsingTemplate("Send at @Model.Time", new { Time = DateTime.Now })
                 .BodyAsPlainText()
                 .Send();

            var errors = string.Join(Environment.NewLine, r.ErrorMessages);
            Console.WriteLine(errors);
        }
    }
}
