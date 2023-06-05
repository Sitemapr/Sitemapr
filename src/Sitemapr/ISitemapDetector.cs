using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr
{
    public interface ISitemapDetector
    {
        Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri);
        Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options);
        Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options, CancellationToken cancellationToken);
    }
}