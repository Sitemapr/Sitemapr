using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitemapr.Locator
{
    public interface ISitemapsSource
    {
        Task<IReadOnlyCollection<Uri>> GetSitemaps(Uri domainUri);
    }
}