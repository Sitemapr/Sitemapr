using System;

namespace Sitemapr.Fluent
{
    public interface ISitemapAnalyzer
    {
        SitemapDetectorBuilder CreateSitemapDetector(Uri rootUri);
    }
}