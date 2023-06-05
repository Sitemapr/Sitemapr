using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr
{
    public interface ISitemapValidator
    {
        Task<bool> IsValidSitemap(Uri sitemapPath, CancellationToken cancellationToken);
    }
}