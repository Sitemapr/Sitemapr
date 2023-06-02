using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Sitemapr.Sitemap;
using Sitemapr.Utils;

namespace Sitemapr
{
    public sealed class SitemapDetector : ISitemapDetector
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SitemapDetector(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            return GetSitemapsInternalAsync(domainUri, default);
        }
        
        public Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            return GetSitemapsInternalAsync(domainUri, options);
        }

        private Task<IReadOnlyList<Uri>> GetSitemapsInternalAsync(Uri domainUri, SitemapDetectionOptions options)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var sitemapSources = options.GetSitemapSources();

            return GetSitemapsOnDomain(domainUri, sitemapSources);
        }

        private Task<IReadOnlyList<Uri>> GetSitemapsOnDomain(Uri uri, IReadOnlyCollection<ISitemapSource> sitemapSources)
        {
            
        }
    }

    public sealed class SitemapDetectionOptions
    {
        public SitemapSource? Source { get; set; } = SitemapSource.DefaultSitemap | SitemapSource.DefaultSitemapIndex | SitemapSource.RobotsTxt;
        public IList<ISitemapSource> CustomSources { get; } = new List<ISitemapSource>();
        public bool ValidateSitemap { get; set; } = true;
        
    }

    [Flags]
    public enum SitemapSource
    {
        DefaultSitemap,
        DefaultSitemapIndex,
        RobotsTxt
    }
}