using System;
using System.Collections.Generic;
using Sitemapr.SitemapCollectors;

namespace Sitemapr.Utils
{
    internal static class SitemapDetectionOptionsExtensions
    {
        public static IReadOnlyCollection<SitemapCollector> GetSitemapCollectors(this SitemapDetectionOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var sitemapSources = new List<SitemapCollector>();

            if (options.Collectors.HasValue)
            {
                if (options.Collectors.Value.HasFlag(DefaultSitemapCollectors.Sitemap))
                {
                    sitemapSources.Add(StandardSitemapCollector.CreateDefaultCollector());
                }
                
                if (options.Collectors.Value.HasFlag(DefaultSitemapCollectors.SitemapIndex))
                {
                    sitemapSources.Add(IndexSitemapCollector.CreateDefaultCollector());
                }
                
                if (options.Collectors.Value.HasFlag(DefaultSitemapCollectors.RobotsTxt))
                {
                    sitemapSources.Add(RobotsTxtSitemapCollector.CreateDefaultCollector());
                }
            }
            
            sitemapSources.AddRange(options.CustomCollectors);
            return sitemapSources;
        }
    }
}