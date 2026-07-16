namespace LovatoOpticalApp.Core.Common
{
    public class ApiServiceResponse
    {
        public string Message { get; set; }
        public int Status { get; set; }

        public ApiServiceResponse(string message, int status)
        {
            Message = message;
            Status = status;
        }
    }
}
