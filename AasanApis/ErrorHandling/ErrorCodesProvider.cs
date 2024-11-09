namespace AastanApis.ErrorHandling
{
    public class ErrorCodesProvider
    {
        public int SafeReponseCode { get; set; }
        public int OutReponseCode { get; set; }
        public string? SafeReponseMessage { get; set; }
        public string? SafeReponseMesageDecription { get; set; }
        public ErrorCodesProvider errorCodesResponseResult(string input) => input switch
        {
            "311" => new ErrorCodesProvider
            {
                SafeReponseCode = 211,
                SafeReponseMessage = "OK",
                OutReponseCode = 211,
                SafeReponseMesageDecription = "اطلاعات هویتی ارسالی با مرجع تطبیق در یکی یا بیشتر از فیلد ها مشابهت ندارد"
            },

        };

    }
}