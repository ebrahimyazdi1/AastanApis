using System.Text.Json;

namespace AasanApis.Data.Entities
{
    public sealed class AastanReqLog : BaseEntity<String>
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
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
