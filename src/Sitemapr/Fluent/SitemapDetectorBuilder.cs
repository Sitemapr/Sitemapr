
using System;
using Sitemapr.Fluent.SitemapCollectors;

namespace Sitemapr.Fluent
{
    public sealed class SitemapDetectorBuilder
    {
        public SitemapDetectorBuilder(Uri rootUri)
        {
            RootUri = rootUri;
        }
        
        public Uri RootUri { get; }
        public SitemapDetectionOptions Options { get; private set; } = new SitemapDetectionOptions();

        public SitemapDetectorBuilder WithAllDefaultSources() 
            => WithDefaultSources(DefaultSitemapSource.Robots | DefaultSitemapSource.Sitemap | DefaultSitemapSource.SitemapIndex);

        public SitemapDetectorBuilder WithDefaultSources(DefaultSitemapSource defaultSources)
        {
            Options.DefaultSources = defaultSources;
            return this;
        }

        public SitemapDetectorBuilder WithNoDefaultSources()
        {
            Options.DefaultSources = null;
            return this;
        }

        public SitemapDetectorBuilder WithSitemap(string sitemapUri)
        {
            if (sitemapUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapUri));
            }

            var uri = new Uri(sitemapUri, UriKind.RelativeOrAbsolute);
            return WithSitemap(uri);
        }
        
        public SitemapDetectorBuilder WithSitemap(Uri sitemapUri)
        {
            if (sitemapUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapUri));
            }

            var collector = new StandardSitemapCollector(sitemapUri);
            Options.CustomCollectors.Add(collector);

            return this;
        }
        
        public SitemapDetectorBuilder WithSitemapIndex(string sitemapIndexUri)
        {
            if (sitemapIndexUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexUri));
            }

            var uri = new Uri(sitemapIndexUri, UriKind.RelativeOrAbsolute);
            return WithSitemapIndex(uri);
        }
        
        public SitemapDetectorBuilder WithSitemapIndex(Uri sitemapIndexUri)
        {
            if (sitemapIndexUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexUri));
            }

            var collector = new IndexSitemapCollector(sitemapIndexUri);
            Options.CustomCollectors.Add(collector);

            return this;
        }

        public SitemapDetectorBuilder ConfigureOptions(Action<SitemapDetectionOptions> configureOptions)
        {
            if (configureOptions is null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }
            
            configureOptions.Invoke(Options);
            return this;
        }

        public SitemapDetectorBuilder WithOptions(SitemapDetectionOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Options = options;
            return this;
        }

        public string BuildReportGenerator()
        {
            
        }

        public string ToSitemapFetcher()
        {
            
        }
    }
}