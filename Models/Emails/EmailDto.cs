using MimeKit;

namespace WebApplication1.Models.Emails
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;


        public EmailDto(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
