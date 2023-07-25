using System;
using System.Threading.Tasks;

namespace MiraeDigital.InvestmentFunds.Infrastructure.Http
{
    public interface IRefitHttpWrapper
    {
        void UseRetryPolicy(int retryCount);
        Task<RefitResult<T>> Execute<T>(string methodName, Func<Task<T>> func);
    }
}
