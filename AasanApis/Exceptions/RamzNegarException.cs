using AasanApis.ErrorHandling;

namespace AasanApis.Exceptions
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
