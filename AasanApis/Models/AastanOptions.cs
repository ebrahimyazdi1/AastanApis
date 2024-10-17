namespace AasanApis.Models
{
    public sealed class AastanOptions
    {
        public const string SectionName = "Aastan";
        public string BaseAddress { get; set; }
        public string TokenAddress { get; set; }
        public string PgsbTokenAddress { get; set; }
        public string PersonConsentInquiryAddress { get; set; }
        public string CriminalRecordAddress { get; set; }
        public string RefreshTokenAddress { get; set; }
        public string MachingServiceAddress { get; set; }
        public string GrantType { get; set; }
        public string CompanyCode { get; set; }

        //Radio userName and password are taken from radio communications organization
        public string RadioUserName { get; set; }
        public string RadioPassword { get; set; }

        //Astan username and password are taken from Astan organization
        public string AstanUserName { get; set; }
        public string AstanPassword { get; set; }

    }
}
