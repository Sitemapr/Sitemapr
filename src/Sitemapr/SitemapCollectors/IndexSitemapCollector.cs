using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.SitemapCollectors
{
    public sealed class IndexSitemapCollector : SitemapCollector
    {
        internal override Task<SitemapCollectionResult> GetSitemapsAsync(Uri rootUri, HttpClient httpClient, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        public static IndexSitemapCollector CreateDefaultCollector() =>
            new IndexSitemapCollector();
    }
}