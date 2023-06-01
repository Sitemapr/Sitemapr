using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitemapr.Locator
{
    public sealed class DefaultSitemapsSource : ISitemapsSource
    {
        public Task<IReadOnlyCollection<Uri>> GetSitemaps(Uri domainUri)
        {
            throw new NotImplementedException();
        }
    }
}