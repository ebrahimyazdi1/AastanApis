namespace AasanApis.Data.Entities
{
    public class ShahkarRequestsLogEntity
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
        public long ExpireTimeInSecond { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public string RequestId { get; set; }
        public string SafeServiceId { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreateDate {  get; set; }
    }
}
