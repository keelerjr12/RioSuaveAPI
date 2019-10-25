using MailKit.Net.Smtp;
using MimeKit;

namespace RioSuaveLib
{
    public class EmailService
    {
        public EmailService(string host, int port, string username, string password)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }
        public void Send(string to, string from, string subject, string body)
        {
            var message = CreateMessage(to, from, subject, body);

            using var client = new SmtpClient();

            client.Connect(_host, _port);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_username, _password);
            client.Send(message);
            client.Disconnect(true);
        }

        private static MimeMessage CreateMessage(string to, string from, string subject, string body)
        {
            var message = new MimeMessage();

            message.To.Add(new MailboxAddress(to));
            message.From.Add(new MailboxAddress(from));
            message.Subject = subject;
            message.Body = new TextPart()
            {
                Text = body
            };

            return message;
        }

        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
    }
}
