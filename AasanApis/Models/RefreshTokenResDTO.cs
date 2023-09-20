namespace AasanApis.Models
{
    public class RefreshTokenResDTO
    {
        public string AccsessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public string ExpireTimesInSecond{ get; set; }
    }
}
