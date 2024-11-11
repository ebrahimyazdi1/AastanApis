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
            "211" => new ErrorCodesProvider
            {
                SafeReponseCode = 211,
                SafeReponseMessage = "InvalidParameters",
                OutReponseCode = 202,
                SafeReponseMesageDecription = ".اطلاعات هویتی ارسالی با مرجع تطبیق در یکی یا بیشتر از فیلد ها مشابهت ندارد"
            },

            "300" => new ErrorCodesProvider
            {
                SafeReponseCode = 300,
                SafeReponseMessage = "UnregisteredProvider",
                OutReponseCode = 401,
                SafeReponseMesageDecription = ".به عنوان یک سرویس دهنده مجاز احراز هویت نشدید"
            },

            "301" => new ErrorCodesProvider
            {
                SafeReponseCode = 301,
                SafeReponseMessage = "NotAllowedForServiceSubmission",
                OutReponseCode = 401,
                SafeReponseMesageDecription = ".مجاز به ثبت سرویس نیستید"
            },

            "302" => new ErrorCodesProvider
            {
                SafeReponseCode = 302,
                SafeReponseMessage = "ServiceIsnotSupportedForHandling",
                OutReponseCode = 504,
                SafeReponseMesageDecription = ".سرویس مورد نظر برای بررسی در دست پشتیبانی نیست"
            },

            "303" => new ErrorCodesProvider
            {
                SafeReponseCode = 303,
                SafeReponseMessage = "UsernameUnregisteredAsProvider",
                OutReponseCode = 401,
                SafeReponseMesageDecription = ".نام کاربری شما به عنوان سرویس دهنده ثبت نشده است"
            },

            "310" => new ErrorCodesProvider
            {
                SafeReponseCode = 310,
                SafeReponseMessage = "InvalidRequestFormat",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".فرمت داده های ارسالی صحیح نیست"
            },

            "311" or "AastanApiError" => new ErrorCodesProvider
            {
                SafeReponseCode = 311,
                SafeReponseMessage = "InvalidValue",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".مقادیر نامعتبر"
            },

            "312" => new ErrorCodesProvider
            {
                SafeReponseCode = 312,
                SafeReponseMessage = "DuplicateRequestId",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".شماره درخواست تکراری است"
            },

            "313" => new ErrorCodesProvider
            {
                SafeReponseCode = 313,
                SafeReponseMessage = "RequestedServiceIsAlreadyAllocated",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".سرویس درخواستی قبلا تخصیص داده شده است"
            },

            "314" => new ErrorCodesProvider
            {
                SafeReponseCode = 314,
                SafeReponseMessage = "MobileWifiRegistraionServiceError",
                OutReponseCode = 501,
                SafeReponseMesageDecription = ".اشتباه در فراخوانی ثبت سرویس وایفای موبایل"
            },

            "316" => new ErrorCodesProvider
            {
                SafeReponseCode = 316,
                SafeReponseMessage = "SubmittedClassDoesNotMatchTheOperator",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".کلاسه ارسالی با اپراتور تطابق ندارد"
            },

            "317" => new ErrorCodesProvider
            {
                SafeReponseCode = 317,
                SafeReponseMessage = "ServiceIdCouldNotBeFound",
                OutReponseCode = 404,
                SafeReponseMesageDecription = ".شماره مشخصه سرویس در پایگاه داده موجود نمی باشد"
            },

            "318" or "327" or "328" or "330" => new ErrorCodesProvider
            {
                SafeReponseCode = 318,
                SafeReponseMessage = "TooManyRequests",
                OutReponseCode = 429,
                SafeReponseMesageDecription = ".در خواست های ارسالی بیش از حد مجاز است"
            },

            "319" => new ErrorCodesProvider
            {
                SafeReponseCode = 319,
                SafeReponseMessage = "ChannelIsTemporaryClosed",
                OutReponseCode = 503,
                SafeReponseMesageDecription = ".کانال شما به صورت موقت مسدود است"
            },

            "320" => new ErrorCodesProvider
            {
                SafeReponseCode = 320,
                SafeReponseMessage = "ClassIdIsNotAvailableInDatabase",
                OutReponseCode = 404,
                SafeReponseMesageDecription = ".شماره کلاسه در پایگاه داده موجود نیست"
            },

            "321" => new ErrorCodesProvider
            {
                SafeReponseCode = 321,
                SafeReponseMessage = "ClassErrorRequestedActionCantBeDone",
                OutReponseCode = 503,
                SafeReponseMesageDecription = ".عملیات مورد نظر به خاطر انتقال یا حذف کلاسه امکان پذیر نیست"
            },

            "322" or "323" or "326" => new ErrorCodesProvider
            {
                SafeReponseCode = 322,
                SafeReponseMessage = "RequestTimedOut",
                OutReponseCode = 429,
                SafeReponseMesageDecription = ".زمان مجاز برای ثبت درخواست گذشته است"
            },

            "324" or "331" => new ErrorCodesProvider
            {
                SafeReponseCode = 324,
                SafeReponseMessage = "TransMissionError",
                OutReponseCode = 503,
                SafeReponseMesageDecription = ".خطای انتقال سرویس"
            },

            "325" => new ErrorCodesProvider
            {
                SafeReponseCode = 325,
                SafeReponseMessage = "RequestIdIsInvalid",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".شماره درخواست صحیح نیست"
            },

            "329" => new ErrorCodesProvider
            {
                SafeReponseCode = 329,
                SafeReponseMessage = "ClassIdIsNotMatchedWithServiceType",
                OutReponseCode = 404,
                SafeReponseMesageDecription = ".شماره کلاسه با شماره سرویس همخوانی ندارد"
            },

            "332" => new ErrorCodesProvider
            {
                SafeReponseCode = 332,
                SafeReponseMessage = "AgentMobileNumberNotRegistered",
                OutReponseCode = 404,
                SafeReponseMesageDecription = ".شماره موبایل نماینده در سامانه وجود ندارد"
            },

            "333" => new ErrorCodesProvider
            {
                SafeReponseCode = 333,
                SafeReponseMessage = "MobileNumberNotRegistered",
                OutReponseCode = 404,
                SafeReponseMesageDecription = ".شماره موبایل در سامانه وجود ندارد"
            },

            "334" => new ErrorCodesProvider
            {
                SafeReponseCode = 334,
                SafeReponseMessage = "ServiceCanNotBeGrantedToNaturalPerson",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".اعطای سرویس به افراد حقیقی میسر نیست"
            },

            "335" => new ErrorCodesProvider
            {
                SafeReponseCode = 335,
                SafeReponseMessage = ",UserNameIsInActive",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".نام کاربری غیر فعال است"
            },

            "336" => new ErrorCodesProvider
            {
                SafeReponseCode = 336,
                SafeReponseMessage = "NumberOnBlacklist",
                OutReponseCode = 429,
                SafeReponseMesageDecription = ".شماره در لیست سیاه است"
            },

            "340" => new ErrorCodesProvider
            {
                SafeReponseCode = 340,
                SafeReponseMessage = "InputIpsConflict",
                OutReponseCode = 403,
                SafeReponseMesageDecription = ".ادرس ای پی های ورودی با هم تداخل دارند"
            },

            "341" or "347" => new ErrorCodesProvider
            {
                SafeReponseCode = 340,
                SafeReponseMessage = "InputIpsConflict",
                OutReponseCode = 403,
                SafeReponseMesageDecription = ".ادرس ای پی های ورودی با ادرس ای پی های ثبت شده توسط دیگران تداخل دارد یا در زیرمجموعه ها یافت نشده است"
            },

            "348" => new ErrorCodesProvider
            {
                SafeReponseCode = 348,
                SafeReponseMessage = "IpCanNotBeRegistered",
                OutReponseCode = 403,
                SafeReponseMesageDecription = ".برای ثبت ای پی شناسه ملی باید ثبت شده باشد"
            },

            "360" => new ErrorCodesProvider
            {
                SafeReponseCode = 360,
                SafeReponseMessage = "ServiceCanNotBeRegisteredForLegalPerson",
                OutReponseCode = 403,
                SafeReponseMesageDecription = ".امکان ثبت سرویس برای اشخاص حقوقی مجاز نیست"
            },

            "361" => new ErrorCodesProvider
            {
                SafeReponseCode = 361,
                SafeReponseMessage = "ImmigrantSIMCardIsOnlyAvailableForImmigrants",
                OutReponseCode = 403,
                SafeReponseMesageDecription = ".ثبت سیم کارت موقت اتباع فقط برای اشخاص حقیقی غیر ایرانی مجاز است"
            },

            "370" => new ErrorCodesProvider
            {
                SafeReponseCode = 370,
                SafeReponseMessage = "RequestedDocumentIsInvalid",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".داکیومنت درخواست داده شده نامعتبر است"
            },

            "500" or "510" or "InternalServerError" => new ErrorCodesProvider
            {
                SafeReponseCode = 500,
                SafeReponseMessage = "InternalServerError",
                OutReponseCode = 503,
                SafeReponseMesageDecription = ".وب سرویس شاهکار دچار مشکل است"
            },

            "501" => new ErrorCodesProvider
            {
                SafeReponseCode = 501,
                SafeReponseMessage = "ServicesynchronizationError",
                OutReponseCode = 503,
                SafeReponseMesageDecription = ".وب سرویس شاهکار دچار خطای همزمانی است"
            },

            "600" => new ErrorCodesProvider
            {
                SafeReponseCode = 600,
                SafeReponseMessage = "InvalidSubmittedInformation",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".اطلاعات ارسالی در یکی یا بیشتر از فیلد ها مشابهت ندارد"
            },

            "601" or "602" or "604" => new ErrorCodesProvider
            {
                SafeReponseCode = 601,
                SafeReponseMessage = "ClientIsNotAllowedForService",
                OutReponseCode = 429,
                SafeReponseMesageDecription = ".سرویس گیرنده مجاز به گرفتن سرویس نیست"
            },

            "603" => new ErrorCodesProvider
            {
                SafeReponseCode = 603,
                SafeReponseMessage = "PhoneNumberNotRegisteredForMobileService",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".برای این شماره مشخصه قبلا سرویس موبایل ثبت نشده است"
            },

            "610" => new ErrorCodesProvider
            {
                SafeReponseCode = 610,
                SafeReponseMessage = "RequestedPersonCouldNotBeMatched",
                OutReponseCode = 404,
                SafeReponseMesageDecription = ".شخص مورد نظر در مرجع تطبیق یافت نشد"
            },

            "611" => new ErrorCodesProvider
            {
                SafeReponseCode = 611,
                SafeReponseMessage = "PersonIsAlive",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".شخص در قید حیات است"
            },

            "612" => new ErrorCodesProvider
            {
                SafeReponseCode = 612,
                SafeReponseMessage = "PersonIsDead",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".شخص در قید حیات نیست"
            },

            "615" => new ErrorCodesProvider
            {
                SafeReponseCode = 615,
                SafeReponseMessage = "MatchingServerIsNotResponding",
                OutReponseCode = 504,
                SafeReponseMesageDecription = ".وب سرویس تطبیق مرجع مورد نظر در دسترس نیست"
            },

            "616" => new ErrorCodesProvider
            {
                SafeReponseCode = 616,
                SafeReponseMessage = "LocationDataCouldNotBeMatched",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".اطلاعات مکانی با مرجع تطبیق همخوانی ندارد"
            },

            "617" => new ErrorCodesProvider
            {
                SafeReponseCode = 617,
                SafeReponseMessage = "MobilePhoneServiceIsNotResponding",
                OutReponseCode = 504,
                SafeReponseMesageDecription = ".سرویس قطع و وصل تلفن همراه پاسخ نمی دهد"
            },

            "618" => new ErrorCodesProvider
            {
                SafeReponseCode = 618,
                SafeReponseMessage = "PersonsOlderThanAgeLimit",
                OutReponseCode = 400,
                SafeReponseMesageDecription = ".شخص حقیقی باید زیر سن حداکثری باشد"
            },

            _ => new ErrorCodesProvider
            {
                SafeReponseCode = 700,
                SafeReponseMessage = "UnknownError",
                OutReponseCode = 999,
                SafeReponseMesageDecription = ".ارور مورد نظر تعریف نشده است"
            },
        };

    }
}