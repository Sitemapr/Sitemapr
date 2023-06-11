using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemapr
{
    public interface ISitemapValidator
    {
        Task<SitemapValidationResult> IsValidSitemapAsync(Uri sitemapPath, CancellationToken cancellationToken);
    }
}