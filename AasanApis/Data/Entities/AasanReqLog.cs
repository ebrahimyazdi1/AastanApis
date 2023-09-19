namespace AasanApis.Data.Entities
{
    public class AasanReqLog : BaseEntity<String>
    {
        public AasanReqLog()
        {
            LogDateTime = DateTime.Now;
        }
        public DateTime LogDateTime { get; set; }
        public string JsonReq { get; set; }
        //***************//
        public string UserId { get; set; }
        public string PublicAppId { get; set; }
        public string ServiceId { get; set; }
        public string PublicReqId { get; set; }

        public ICollection<AasanResLog> AasanResLogs { get; set; }

    }
}
