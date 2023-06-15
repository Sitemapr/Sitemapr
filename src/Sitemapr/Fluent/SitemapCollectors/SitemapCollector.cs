using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.Fluent.SitemapCollectors
{
    public abstract class SitemapCollector : IEquatable<SitemapCollector>
    {
        protected SitemapCollector(Uri uri)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        public Uri Uri { get; }

        internal abstract Task<SitemapCollectionResult> GetSitemapsAsync(HttpClient httpClient, CancellationToken cancellationToken);

        public bool Equals(SitemapCollector other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Uri, other.Uri) && Equals(GetType(), other.GetType());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SitemapCollector)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Uri.GetHashCode() * 397) ^ GetType().GetHashCode();
            }
        }
    }
}