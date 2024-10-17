public class PSGBErrorCodesProvider
{
    public int SafeResponseCode { get; set; }
    public int OutResponseCode { get; set; }
    public string? SafeResponseMessage { get; set; }
    public string? SafeResponseMessageDescription { get; set; }
    public PSGBErrorCodesProvider ErrorCodesResponseResult(string input) => input switch
    {
        "101" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 101,
            SafeResponseMessage = "UnAuthorized",
            OutResponseCode = 401,
            SafeResponseMessageDescription = " .شخص نزد قوه قضاییه احراز نشد"
        },

        "102" or "103" or "104" or "105" or "106" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 102,
            SafeResponseMessage = "IndividualIsUnableToTrade",
            OutResponseCode = 102,
            SafeResponseMessageDescription = ".شخص اهلیت قانونی معامله ندارد"
        },

        "107" or "108" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 107,
            SafeResponseMessage = "IndividualDoesNotHaveFinancialAbility",
            OutResponseCode = 107,
            SafeResponseMessageDescription = ".شخص تمکن مالی ندارد"
        },
        "109" or "110" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 109,
            SafeResponseMessage = "IndividualHasCriminalRecord",
            OutResponseCode = 109,
            SafeResponseMessageDescription = ".شخص سابقه محکومیت قضایی دارد"
        },
        "400" or "401" or "402" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 400,
            SafeResponseMessage = "InvalidInput",
            OutResponseCode = 400,
            SafeResponseMessageDescription = ".پارامترهای ورودی نامعتبر هستند"
        },


        _ => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 500,
            SafeResponseMessage = "InternalServerError", 
            OutResponseCode = 500,
            SafeResponseMessageDescription = "خطای داخلی یا ناشناخته"
        }

    }; 

}


