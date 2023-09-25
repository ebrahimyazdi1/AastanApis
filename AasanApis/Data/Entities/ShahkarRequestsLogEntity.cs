namespace AasanApis.Data.Entities
{
    public class ShahkarRequestsLogEntity
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
        public string ExpireTimeInSecond { get; set; }
        public string RequestId { get; set; }
    }
}
