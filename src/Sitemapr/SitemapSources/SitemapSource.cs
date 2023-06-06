using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr.SitemapSources
{
    public abstract class SitemapSource
    {
        internal abstract Task<SitemapSourceResult> GetSitemapUrisAsync(Uri domainUri, HttpClient httpClient, CancellationToken cancellationToken);
    }
}