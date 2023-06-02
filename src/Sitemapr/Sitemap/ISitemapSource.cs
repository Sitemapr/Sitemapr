using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitemapr.Sitemap
{
    public interface ISitemapSource
    {
        Task<IReadOnlyCollection<Uri>> GetSitemaps(Uri domainUri);
    }
}