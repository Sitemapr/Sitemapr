using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.Sitemap
{
    public abstract class SitemapsSource
    {
        internal abstract Task<IEnumerable<SitemapInfo>> GetSitemapPathsAsync(Uri domainUri, HttpClient httpClient, CancellationToken cancellationToken);
    }
}