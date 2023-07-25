using System.Net;

namespace MiraeDigital.InvestmentFunds.Infrastructure.Http
{
    public class RefitResult<T>
    {
        public RefitResult(T resultData)
        {
            ResultData = resultData;
        }

        public RefitResult(string error, HttpStatusCode errorCode)
        {
            Error = error;
            ErrorCode = errorCode;
        }

        public T ResultData { get; }
        public string Error { get; }
        public HttpStatusCode? ErrorCode { get; }
        public bool HasError() => ErrorCode != null;
    }
}
