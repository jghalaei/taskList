using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotApiGw
{
    public class CorrelationIdDelgatingHandler : DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("CorrelationId"))
            {
                request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
            }
            return base.SendAsync(request, cancellationToken);
        }

    }
}