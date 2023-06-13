using System.Net;

namespace Sitemapr.Test.Shared.HttpClient;

public sealed class CustomHttpMessageHandler : HttpMessageHandler
{
    private readonly List<HttpRequestMessage> _requests = new();
    
    public CustomHttpMessageHandler(HttpStatusCode httpStatusCode, HttpContent? httpContent = null)
    {
        HttpStatusCode = httpStatusCode;
        HttpContent = httpContent;
    }

    public HttpStatusCode HttpStatusCode { get; }
    public HttpContent? HttpContent { get; }

    public IReadOnlyList<HttpRequestMessage> Requests => _requests.AsReadOnly();

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _requests.Add(request);
        
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode,
            Content = HttpContent
        });
    }
}