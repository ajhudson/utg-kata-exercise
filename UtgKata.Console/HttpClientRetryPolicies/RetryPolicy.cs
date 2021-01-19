using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace UtgKata.Console.HttpClientRetryPolicies
{
    public class RetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                                        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                                        .Or<OperationCanceledException>()
                                        .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
