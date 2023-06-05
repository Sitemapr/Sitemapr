using System;
using System.Collections.Generic;
using Sitemapr.Sitemap;

namespace Sitemapr.Utils
{
    internal static class SitemapDetectionOptionsExtensions
    {
        public static IReadOnlyCollection<SitemapsSource> GetSitemapSources(this SitemapDetectionOptions options, Uri domainPath)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var sitemapSources = new List<SitemapsSource>();

            if (options.Source.HasValue)
            {
                if (options.Source.Value.HasFlag(SitemapSource.DefaultSitemap))
                {
                    sitemapSources.Add(new StandardSitemapsSource(domainPath.ToDefaultSitemapUri()));
                }
                
                if (options.Source.Value.HasFlag(SitemapSource.DefaultSitemapIndex))
                {
                    sitemapSources.Add(new IndexSitemapsSource());
                }
                
                if (options.Source.Value.HasFlag(SitemapSource.RobotsTxt))
                {
                    sitemapSources.Add(new RobotsTxtSitemapsSource(domainPath.ToRobotsTxtUri()));
                }
            }
            
            sitemapSources.AddRange(options.CustomSources);
            return sitemapSources;
        }
    }
}