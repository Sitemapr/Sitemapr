using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitemapr.Sitemap
{
    public sealed class SitemapIndexSource : ISitemapSource
    {
        public Task<IReadOnlyCollection<Uri>> GetSitemaps(Uri domainUri)
        {
            throw new NotImplementedException();
        }
    }
}