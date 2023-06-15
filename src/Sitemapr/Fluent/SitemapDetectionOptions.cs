using System;
using System.Collections.Generic;
using Sitemapr.Fluent.SitemapCollectors;

namespace Sitemapr.Fluent
{
    public sealed class SitemapDetectionOptions
    {
        public DefaultSitemapCollector? DefaultCollectors { get; set; }
        public List<SitemapCollector> CustomCollectors { get; } = new List<SitemapCollector>();
    }

    [Flags]
    public enum DefaultSitemapCollector
    {
        SitemapXml,
        SitemapIndexXml,
        RobotsTxt
    }
}