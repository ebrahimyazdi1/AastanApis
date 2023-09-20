namespace AastanApis.Data.Entities
{
    public class AastanReqLog : BaseEntity<String>
    {
        public AastanReqLog()
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

        public ICollection<AastanResLog> AastanResLogs { get; set; }

    }
}
