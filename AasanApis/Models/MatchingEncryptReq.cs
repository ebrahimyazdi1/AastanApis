using System.Text.Json.Serialization;

namespace AastanApis.Models
{
    public sealed class MatchingEncryptReq
    {
        /// <summary>
        /// شماره درخواست
        /// </summary>
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }


        /// <summary>
        /// شناسه هویتی)رم گذاری شده( 
        /// برای اشخاص حقیقی ایرانی کد ملی و برای اشخاص حقوقی شناسه ملی
        /// </summary>
        [JsonPropertyName("identificationNo")]
        public string IdentificationNo { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        [JsonPropertyName("serviceNumber")]
        public string ServiceNumber { get; set; }


        /// <summary>
        /// نوع شناسه هویتی
        ///کد ملی =0 و شماره گذرنامه =1 و شماره کارت ازمایش =2  و شماره کارت پناهندگی = 3 
        /// شماره گذرنامه = 6 و شماره کارت هویت =4 و شناسه ملی= 5 
        /// </summary>
        [JsonPropertyName("identificationType")]
        public int IdentificationType { get; set; }

        /// <summary>
        /// نوع سرویس )مقدار برابر 2( 
        /// </summary>
        [JsonPropertyName("serviceType")]
        public int ServiceType { get; set; }



    }
}
