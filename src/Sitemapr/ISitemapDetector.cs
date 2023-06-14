using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr
{
    public interface ISitemapDetector
    {
        Task<SitemapsResult> GetSitemapsAsync(Uri domainUri);
        Task<SitemapsResult> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options);
        Task<SitemapsResult> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options, CancellationToken cancellationToken);
    }
}