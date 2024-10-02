using AasanApis.ErrorHandling;
using System.Text.Json.Serialization;

namespace AasanApis.Models
{
    public class MatchingEncryptRes : ErrorResult
    {
        [JsonPropertyName("result")]
        public Result Result { get; set; }

        [JsonPropertyName("status")]
        public ResultStatus Status { get; set; }

    }

    public class Result
    {

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }

    }
    public class Status
    {
        [JsonPropertyName("statusCode")]
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

    }


    public class ResultStatus
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
