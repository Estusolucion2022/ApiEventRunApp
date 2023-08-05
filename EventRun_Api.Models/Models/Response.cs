using Microsoft.Identity.Client;

namespace EventRun_Api.Models
{
    public class Response
    {
        public int? Code { get; set; }
        public object? Data { get; set; }
        public string? Error { get; set; }
        public string? Message { get; set; }

    }
}
