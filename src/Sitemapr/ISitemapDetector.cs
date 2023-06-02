using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitemapr
{
    public interface ISitemapDetector
    {
        Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri);
    }
}