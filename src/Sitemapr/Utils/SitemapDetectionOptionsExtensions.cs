using System;
using System.Collections.Generic;
using Sitemapr.Sitemap;

namespace Sitemapr.Utils
{
    internal static class SitemapDetectionOptionsExtensions
    {
        public static IReadOnlyCollection<ISitemapSource> GetSitemapSources(this SitemapDetectionOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var sitemapSources = new List<ISitemapSource>();

            if (options.Source.HasValue)
            {
                if (options.Source.Value.HasFlag(SitemapSource.DefaultSitemap))
                {
                    sitemapSources.Add(new DefaultSitemapSource());
                }
                
                if (options.Source.Value.HasFlag(SitemapSource.DefaultSitemapIndex))
                {
                    sitemapSources.Add(new SitemapIndexSource());
                }
                
                if (options.Source.Value.HasFlag(SitemapSource.RobotsTxt))
                {
                    sitemapSources.Add(new RobotsTxtSitemapSource());
                }
            }
            
            sitemapSources.AddRange(options.CustomSources);
            return sitemapSources;
        }
    }
}