using AastanApis.ErrorHandling;


namespace AastanApis.Exceptions
{
    public class RamzNegarException : Exception
    {
        public ErrorCode Code { get; set; }
        public RamzNegarException(ErrorCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
