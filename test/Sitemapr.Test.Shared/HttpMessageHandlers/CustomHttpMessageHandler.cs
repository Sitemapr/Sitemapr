using System.Net;

namespace Sitemapr.Test.Shared.HttpMessageHandlers;

public sealed class CustomHttpMessageHandler : HttpMessageHandler
{
    public CustomHttpMessageHandler(HttpStatusCode httpStatusCode, HttpContent? httpContent = null)
    {
        HttpStatusCode = httpStatusCode;
        HttpContent = httpContent;
    }

    public HttpStatusCode HttpStatusCode { get; }
    public HttpContent? HttpContent { get; }
    
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode,
            Content = HttpContent
        });
    }
}