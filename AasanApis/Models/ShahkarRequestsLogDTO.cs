namespace AasanApis.Models
{
    public sealed class ShahkarRequestsLogDTO
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
        public int? ExpireTimeInSecond { get; set; }
        public string? RequestId { get; set; }
        public string? SafeServiceId { get; set; }
        public DateTime CreateDate { get; set; }   
        public DateTime ExpirationDateTime { get; set;}
    }
}
