using System;
using Sitemapr.SitemapSources;

namespace Sitemapr
{
    public sealed class Sitemap : IEquatable<Sitemap>
    {
        internal Sitemap(Uri sitemapPath, SitemapStatus status, SitemapSource source, Exception exception = null)
        {
            SitemapPath = sitemapPath ?? throw new ArgumentNullException(nameof(sitemapPath));
            Status = status;
            Source = source;
            Exception = exception;
        }

        public Uri SitemapPath { get; }
        public SitemapStatus Status { get; }
        public SitemapSource Source { get; }
        public Exception Exception { get; }

        public bool Equals(Sitemap other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(SitemapPath, other.SitemapPath);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Sitemap other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (SitemapPath != null ? SitemapPath.GetHashCode() : 0);
        }
    }
}