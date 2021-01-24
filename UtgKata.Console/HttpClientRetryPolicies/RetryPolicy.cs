// <copyright file="RetryPolicy.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console.HttpClientRetryPolicies
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Polly;
    using Polly.Extensions.Http;

    /// <summary>
    /// Retry policy for polly.
    /// </summary>
    public class RetryPolicy
    {
        /// <summary>Gets the retry policy.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                                        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                                        .Or<OperationCanceledException>()
                                        .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
