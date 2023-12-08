using MimeKit;

namespace WebApplication1.Models
{
    public class EmailDto
    {
        public string Body { get; set; } = string.Empty;

        public string To { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

    }
}
