
using System;

namespace Sitemapr.Fluent
{
    public sealed class SitemapDetectorBuilder
    {
        public SitemapDetectorBuilder(Uri rootUri)
        {
            RootUri = rootUri;
        }
        
        public Uri RootUri { get; }
        public SitemapDetectionOptions Options { get; } = new SitemapDetectionOptions();

        public SitemapDetectorBuilder WithAllDefaultCollectors() 
            => WithDefaultCollectors(DefaultSitemapCollector.SitemapXml | DefaultSitemapCollector.SitemapIndexXml | DefaultSitemapCollector.RobotsTxt);

        public SitemapDetectorBuilder WithDefaultCollectors(DefaultSitemapCollector defaultCollectors)
        {
            Options.DefaultCollectors = defaultCollectors;
            return this;
        }

        public SitemapDetectorBuilder NoDefaultCollectors()
        {
            Options.DefaultCollectors = null;
            return this;
        }
    }
}