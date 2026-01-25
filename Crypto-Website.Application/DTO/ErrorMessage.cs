namespace Crypto_Website.Application.DTO
{
    public class ErrorMessage
    {
        public int statusCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
