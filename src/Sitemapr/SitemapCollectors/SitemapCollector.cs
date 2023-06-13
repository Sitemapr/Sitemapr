using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.SitemapCollectors
{
    public abstract class SitemapCollector
    {
        internal abstract Task<SitemapCollectionResult> GetSitemapsAsync(Uri rootUri, HttpClient httpClient, CancellationToken cancellationToken);
    }
}