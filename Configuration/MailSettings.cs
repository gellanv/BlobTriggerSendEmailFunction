using System;

namespace SendEmailFunction.Configuration
{
    public class MailSettings
    {
        public string DisplayName { get; set; }
        public string From { get; set; }
        public string UserEmailName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public MailSettings()
        {
            DisplayName = Environment.GetEnvironmentVariable("DisplayName");
            From = Environment.GetEnvironmentVariable("From");
            UserEmailName = Environment.GetEnvironmentVariable("UserEmailName");
            Password = Environment.GetEnvironmentVariable("Password");
            Host = Environment.GetEnvironmentVariable("Host");
            Port = int.Parse(Environment.GetEnvironmentVariable("Port"));
        }
    }
}
