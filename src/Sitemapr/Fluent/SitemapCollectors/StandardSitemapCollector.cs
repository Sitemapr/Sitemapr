using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.Fluent.SitemapCollectors
{
    internal sealed class StandardSitemapCollector : SitemapCollector
    {
        public StandardSitemapCollector(Uri uri) : base(uri)
        {
        }

        
        internal override Task<SitemapCollectionResult> GetSitemapsAsync(HttpClient httpClient, CancellationToken cancellationToken)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            return Task.FromResult<SitemapCollectionResult>(new SuccessfulSitemapCollectionResult(new []{ Uri }));
        }
    }
}