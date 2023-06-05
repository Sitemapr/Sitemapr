using System;

namespace Sitemapr
{
    public sealed class SitemapInfo
    {
        internal SitemapInfo(Uri sitemapUri, SitemapStatus status, Exception exception = null)
        {
            SitemapUri = sitemapUri;
            Status = status;
            Exception = exception;
        }

        public Uri SitemapUri { get; }
        public SitemapStatus Status { get; }
        public Exception Exception { get; }
    }

    public enum SitemapStatus
    {
        Valid,
        Invalid,
        NotFound
    }
}