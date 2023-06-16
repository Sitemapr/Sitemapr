
using System;
using Sitemapr.Fluent.SitemapCollectors;

namespace Sitemapr.Fluent
{
    public sealed class SitemapDetectionBuilder
    {
        public SitemapDetectionBuilder(Uri rootUri)
        {
            RootUri = rootUri;
        }
        
        public Uri RootUri { get; }
        public SitemapDetectionOptions Options { get; private set; } = new SitemapDetectionOptions();

        public SitemapDetectionBuilder WithAllDefaultSources() 
            => WithDefaultSources(DefaultSitemapSource.Robots | DefaultSitemapSource.Sitemap | DefaultSitemapSource.SitemapIndex);

        public SitemapDetectionBuilder WithDefaultSources(DefaultSitemapSource defaultSources)
        {
            Options.DefaultSources = defaultSources;
            return this;
        }

        public SitemapDetectionBuilder WithNoDefaultSources()
        {
            Options.DefaultSources = null;
            return this;
        }

        public SitemapDetectionBuilder WithSitemap(string sitemapUri)
        {
            if (sitemapUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapUri));
            }

            var uri = new Uri(sitemapUri, UriKind.RelativeOrAbsolute);
            return WithSitemap(uri);
        }
        
        public SitemapDetectionBuilder WithSitemap(Uri sitemapUri)
        {
            if (sitemapUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapUri));
            }

            var collector = new StandardSitemapCollector(sitemapUri);
            Options.CustomCollectors.Add(collector);

            return this;
        }
        
        public SitemapDetectionBuilder WithSitemapIndex(string sitemapIndexUri)
        {
            if (sitemapIndexUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexUri));
            }

            var uri = new Uri(sitemapIndexUri, UriKind.RelativeOrAbsolute);
            return WithSitemapIndex(uri);
        }
        
        public SitemapDetectionBuilder WithSitemapIndex(Uri sitemapIndexUri)
        {
            if (sitemapIndexUri is null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexUri));
            }

            var collector = new IndexSitemapCollector(sitemapIndexUri);
            Options.CustomCollectors.Add(collector);

            return this;
        }

        public SitemapDetectionBuilder ConfigureOptions(Action<SitemapDetectionOptions> configureOptions)
        {
            if (configureOptions is null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }
            
            configureOptions.Invoke(Options);
            return this;
        }

        public SitemapDetectionBuilder WithOptions(SitemapDetectionOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            return this;
        }

        public ISitemapReportGenerator BuildReportGenerator()
        {
            
        }

        public ISitemapFetcher BuildSitemapFetcher() => new SitemapFetcher(Options);
    }
}