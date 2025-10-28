namespace Tortillas.Infrastructure.Configuration
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string AppPassword { get; set; } = string.Empty;

        // Nuevo campo
        public string ReplyToEmail { get; set; } = string.Empty;
    }
}
