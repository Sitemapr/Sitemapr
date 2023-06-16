using System;

namespace Sitemapr.Fluent
{
    public sealed class SitemapAnalyzer : ISitemapAnalyzer
    {
        public SitemapDetectionBuilder BuildSitemapDetector(Uri rootUri) => new SitemapDetectionBuilder(rootUri);
    }
}