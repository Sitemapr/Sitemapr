namespace Sitemapr.Fluent
{
    public sealed class SitemapReportGenerator : ISitemapReportGenerator
    {
        private readonly SitemapDetectionOptions _options;

        public SitemapReportGenerator(SitemapDetectionOptions options)
        {
            _options = options;
        }
    }
}