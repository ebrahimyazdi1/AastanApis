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

        "107" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 107,
            SafeResponseMessage = "IndividualHasBankruptcyRecord",
            OutResponseCode = 107,
            SafeResponseMessageDescription = ".شخص سابقه ورشکستگی دارد"
        },

        "108" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 108,
            SafeResponseMessage = "IndividualDoesNotHaveFinancialAbility",
            OutResponseCode = 108,
            SafeResponseMessageDescription = ".شخص تمکن مالی ندارد"
        },

        "109" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 109,
            SafeResponseMessage = "IndividualIsConvictedPersonOntheRun",
            OutResponseCode = 109,
            SafeResponseMessageDescription = ".شخص محکوم متواری است"
        },

        "110" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 110,
            SafeResponseMessage = "IndividualHasARecordOfUnenforcedFinancialConvictions",
            OutResponseCode = 110,
            SafeResponseMessageDescription = ".شخص سابقه محکومیت مالی اجرا نشده دارد"
        },

        "400" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 400,
            SafeResponseMessage = "InvalidInput",
            OutResponseCode = 400,
            SafeResponseMessageDescription = ".پارامترهای ورودی نامعتبر هستند"
        },

        "401" or "402" => new PSGBErrorCodesProvider
        {
            SafeResponseCode = 401,
            SafeResponseMessage = "InvalidRegisterCode",
            OutResponseCode = 401,
            SafeResponseMessageDescription = " .کد رضایت مندی نامعتبر است"
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


