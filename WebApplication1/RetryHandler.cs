using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class RetryHandler : DelegatingHandler
    {
        public int RetryCount { get; set; } = 5;

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    var response = await base.SendAsync(request, cancellationToken);
                    response.EnsureSuccessStatusCode();
                    return response;
                }
                catch (HttpRequestException) when (i == RetryCount - 1)
                {
                    throw;
                }
                catch (HttpRequestException)
                {
                    // Retry
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
                }
            }

            // Unreachable.
            throw null;
        }
    }
}
