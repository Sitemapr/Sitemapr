using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitemapr
{
    public sealed class SitemapsResult
    {
        public SitemapsResult(IReadOnlyList<ValidSitemap> validSitemaps, IReadOnlyList<InvalidSitemap> invalidSitemaps)
        {
            ValidSitemaps = validSitemaps ?? throw new ArgumentNullException(nameof(validSitemaps));
            InvalidSitemaps = invalidSitemaps ?? throw new ArgumentNullException(nameof(invalidSitemaps));
        }

        public IReadOnlyList<ValidSitemap> ValidSitemaps { get; }
        public IReadOnlyList<InvalidSitemap> InvalidSitemaps { get; }

        public bool HasInvalidSitemaps => InvalidSitemaps.Any();
    }
}