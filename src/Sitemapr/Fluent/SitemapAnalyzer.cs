using System;

namespace Sitemapr.Fluent
{
    public sealed class SitemapAnalyzer : ISitemapAnalyzer
    {
        public SitemapDetectorBuilder CreateSitemapDetector(Uri rootUri) => new SitemapDetectorBuilder(rootUri);
    }
}