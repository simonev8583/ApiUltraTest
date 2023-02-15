using System;
namespace ApiUltraTest.Infrastructure.Providers.Configs
{
    public class MailSettings
    {
        public string Server { get; set; } = null!;

        public string User { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Port { get; set; } = 0!;

    }
}

