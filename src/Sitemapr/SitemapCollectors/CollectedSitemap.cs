using System;

namespace Sitemapr.SitemapCollectors
{
    public sealed class CollectedSitemap
    {
        public CollectedSitemap(Uri sitemapUri, string rawSitemapUrl, bool isValid)
        {
            SitemapUri = sitemapUri;
            RawSitemapUrl = rawSitemapUrl;
            IsValid = isValid;
        }

        public Uri SitemapUri { get; }
        public string RawSitemapUrl { get; }
        public bool IsValid { get; }
    }
}