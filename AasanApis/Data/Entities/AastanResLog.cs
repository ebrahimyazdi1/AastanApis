using System.Text.Json;

namespace AastanApis.Data.Entities
{
    public sealed class AastanResLog :BaseEntity<string>
    {
        public string ResCode { get; set; }
        public string HTTPStatusCode { get; set; }
        public string JsonRes { get; set; }
        public string PublicReqId { get; set; }
        //***********//
        public string ReqLogId { get; set; }
        public AastanReqLog ReqLog { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
