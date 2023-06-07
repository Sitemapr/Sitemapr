using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.SitemapSources
{
    public sealed class IndexSitemapSource : SitemapSource
    {
        internal override Task<SitemapSourceResult> GetSitemapUrisAsync(Uri rootUri, HttpClient httpClient, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}