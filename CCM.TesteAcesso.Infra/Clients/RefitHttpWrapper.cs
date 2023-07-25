using Microsoft.Extensions.Logging;
using MiraeDigital.InvestmentFunds.Infrastructure.Http;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Refit;
using System.Net;

namespace CCM.TesteAcesso.Infra.Clients
{
    public class RefitHttpWrapper : IRefitHttpWrapper
    {
        private readonly ILogger<RefitHttpWrapper> _logger;
        private AsyncRetryPolicy _retryPolicy;
        private string _methodName;

        public RefitHttpWrapper(ILogger<RefitHttpWrapper> logger)
        {
            _logger = logger;
        }
        public async Task<RefitResult<T>> Execute<T>(string methodName, Func<Task<T>> func)
        {
            try
            {
                _methodName = methodName;
                T result;
                if (_retryPolicy != null)
                {
                    _logger.LogInformation("Starting an API call with retry policy {@Method}", methodName);
                    result = await _retryPolicy.ExecuteAsync(() => func());
                }
                else
                {
                    _logger.LogInformation("Starting an API call {@Method}", methodName);
                    result = await func();
                }

                _logger.LogInformation("Response {@Method} {@Response}", methodName, JsonConvert.SerializeObject(result));
                return new RefitResult<T>(result);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Error to call {@Method} to uri: {@Uri} response code: {@StatusCode}",
                    methodName,
                    ex.RequestMessage.RequestUri, ex.StatusCode);
                return new RefitResult<T>(ex.Content, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal Server Error {@Method}", methodName);
                throw;
            }
        }

        public void UseRetryPolicy(int retryCount)
        {
            var retry = Policy
                        .Handle<HttpRequestException>()
                        .Or<ApiException>(p => p.StatusCode != HttpStatusCode.BadRequest && p.StatusCode != HttpStatusCode.NotFound)
                        .WaitAndRetryAsync(retryCount: retryCount, sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                        onRetry: (exception, sleepDuration, attemptNumber, context) =>
                        {
                            _logger.LogError(exception, "Executing retry policy... Retrying in {@SleepDuration}. attempt {@AttemptNumber} of {@RetryCount} - {@MethodName}", sleepDuration, attemptNumber, retryCount, _methodName);
                        });

            _retryPolicy = retry;
        }
    }
}
