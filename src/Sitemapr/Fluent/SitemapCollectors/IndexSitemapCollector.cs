using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.Fluent.SitemapCollectors
{
    public sealed class IndexSitemapCollector : SitemapCollector
    {
        public IndexSitemapCollector(Uri uri) : base(uri)
        {
        }
        
        internal override Task<SitemapCollectionResult> GetSitemapsAsync(HttpClient httpClient, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}