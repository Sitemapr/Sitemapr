using System;

namespace Sitemapr.Sitemap
{
    internal sealed class SitemapInfo
    {
        public SitemapInfo(Uri sitemapPath, Exception exception = null)
        {
            SitemapPath = sitemapPath;
            Exception = exception;
        }

        public Uri SitemapPath { get; }
        public Exception Exception { get; }
        public bool HasException => Exception != null;
    }
}