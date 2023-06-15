using System;
using System.Collections.Generic;
using Sitemapr.Fluent.SitemapCollectors;

namespace Sitemapr.Fluent
{
    public sealed class SitemapDetectionOptions
    {
        public DefaultSitemapSource? DefaultSources { get; set; }
        public List<SitemapCollector> CustomCollectors { get; }

        public SitemapDetectionOptions()
        {
            DefaultSources = DefaultSitemapSource.Robots | DefaultSitemapSource.Sitemap | DefaultSitemapSource.SitemapIndex;
            CustomCollectors = new List<SitemapCollector>();
        }
    }

    [Flags]
    public enum DefaultSitemapSource
    {
        Sitemap,
        SitemapIndex,
        Robots
    }
}