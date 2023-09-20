using AastanApis.ErrorHandling;
using System.Text.Json.Serialization;

namespace AasanApis.Models
{
    public class MatchingEncryptRes : ErrorResult
    {
        [JsonPropertyName("Result.data")]
        public Data Result { get; set; }

        [JsonPropertyName("result.status.message")]
        public string Message { get; set; }

        [JsonPropertyName("status")]
        public List<ResultStatus> Status { get; set; }

        [JsonPropertyName("result.status.statusCode")]
        public int StatusCode { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }

        [JsonPropertyName("response")]
        public int Response { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

    }
    public class ResultStatus
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
