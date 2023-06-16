using System;

namespace Sitemapr.Fluent
{
    public interface ISitemapAnalyzer
    {
        SitemapDetectionBuilder BuildSitemapDetector(Uri rootUri);
        //
    }
}