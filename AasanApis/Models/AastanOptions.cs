namespace AasanApis.Models
{
    public sealed class AastanOptions
    {
        public const string SectionName = "Aastan";
        public string BaseAddress { get; set; }
        public string TokenAddress { get; set; }
        public string RefreshTokenAddress { get; set; }
        public string MachingServiceAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Authorization { get; set; }


    }
}
