using System;
using System.Net.Http;

namespace Sitemapr.Fluent
{
    public abstract class Sitemap
    {
        protected Sitemap(SitemapStatus status)
        {
            Status = status;
        }

        public SitemapStatus Status { get; }
        public Sitemap
    }

    public sealed class ValidSitemap : Sitemap
    {
        internal ValidSitemap(Uri uri) : base(SitemapStatus.Valid)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        public Uri Uri { get; }
    }

    public sealed class InvalidUrlSitemap : Sitemap
    {
        internal InvalidUrlSitemap(string uriString) : base(SitemapStatus.Invalid)
        {
            UriString = uriString;
        }

        public string UriString { get; }
    }

    public sealed class FailedSitemap : Sitemap
    {
        internal FailedSitemap(Uri uri, HttpResponseMessage httpResponse = null, string message = null, Exception exception = null) : base(SitemapStatus.Failed)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }
        
        public Uri Uri { get; }
        public HttpResponseMessage HttpResponse { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}