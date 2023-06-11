using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr
{
    public interface ISitemapDetector
    {
        Task<IReadOnlyList<Sitemap>> GetSitemapsAsync(Uri domainUri);
        Task<IReadOnlyList<Sitemap>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options);
        Task<IReadOnlyList<Sitemap>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options, CancellationToken cancellationToken);
    }
}