using System;
using System.Collections.Generic;
using Sitemapr.SitemapSources;

namespace Sitemapr.Utils
{
    internal static class SitemapDetectionOptionsExtensions
    {
        public static IReadOnlyCollection<SitemapSource> GetSitemapSources(this SitemapDetectionOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var sitemapSources = new List<SitemapSource>();

            if (options.DefaultSources.HasValue)
            {
                if (options.DefaultSources.Value.HasFlag(DefaultSitemapSource.Sitemap))
                {
                    sitemapSources.Add(StandardSitemapSource.CreateDefaultSource());
                }
                
                if (options.DefaultSources.Value.HasFlag(DefaultSitemapSource.SitemapIndex))
                {
                    sitemapSources.Add(new IndexSitemapSource());
                }
                
                if (options.DefaultSources.Value.HasFlag(DefaultSitemapSource.RobotsTxt))
                {
                    sitemapSources.Add(RobotsTxtSitemapSource.CreateDefaultSource());
                }
            }
            
            sitemapSources.AddRange(options.Sources);
            return sitemapSources;
        }
    }
}