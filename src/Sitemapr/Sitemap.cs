using System;
using Sitemapr.SitemapCollectors;

namespace Sitemapr
{
    public abstract class Sitemap : 
    {
        internal Sitemap(Uri sitemapPath, SitemapStatus status, SitemapCollector collector, Exception exception = null)
        {
            Uri = sitemapPath ?? throw new ArgumentNullException(nameof(sitemapPath));
            Status = status;
            Collector = collector;
            Exception = exception;
        }

        
        public SitemapStatus Status { get; }
        public SitemapCollector Collector { get; }
        public Exception Exception { get; }

        
    }

    public sealed class ValidSitemap : Sitemap, IEquatable<ValidSitemap>
    {
        public ValidSitemap()
        {
            
        }
        
        public Uri Uri { get; }
        
        public bool Equals(Sitemap other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Uri, other.Uri);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Sitemap other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Uri != null ? Uri.GetHashCode() : 0);
        }
    }

    public sealed class InvalidSitemap : Sitemap
    {
        
    }
}