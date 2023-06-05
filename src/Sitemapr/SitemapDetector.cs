using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sitemapr.Sitemap;
using Sitemapr.Utils;

namespace Sitemapr
{
    public sealed class SitemapDetector : ISitemapDetector
    {
        private readonly ISitemapValidator _sitemapValidator;
        private readonly IHttpClientFactory _httpClientFactory;

        public SitemapDetector(ISitemapValidator sitemapValidator, IHttpClientFactory httpClientFactory)
        {
            _sitemapValidator = sitemapValidator;
            _httpClientFactory = httpClientFactory;
        }

        public Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            return GetSitemapsInternalAsync(domainUri, default, CancellationToken.None);
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
            
            return GetSitemapsInternalAsync(domainUri, options, CancellationToken.None);
        }
        
        public Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options, CancellationToken cancellationToken)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            return GetSitemapsInternalAsync(domainUri, options, cancellationToken);
        }

        private async Task<IReadOnlyList<Uri>> GetSitemapsInternalAsync(Uri domainPath, SitemapDetectionOptions options, CancellationToken cancellationToken)
        {
            if (domainPath is null)
            {
                throw new ArgumentNullException(nameof(domainPath));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var sitemapSources = options.GetSitemapSources(domainPath);
            var sitemaps = new SortedSet<Uri>();

            // TODO: Fix name.
            var httpClient = _httpClientFactory.CreateClient("bob");

            foreach (var sitemapSource in sitemapSources)
            {
                var sitemapsFromSource = await sitemapSource.GetSitemapPathsAsync(httpClient, cancellationToken);
                foreach (var sitemapFromSource in sitemapsFromSource)
                {
                    if (options.ValidateSitemap)
                    {
                        
                        var isValid =_sitemapValidator.IsValidSitemap(sitemapFromSource.);
                    }
                    
                    sitemaps.Add(sitemapFromSource);
                }
            }

            return sitemaps.ToList().AsReadOnly();
        }
    }

    public sealed class SitemapDetectionOptions
    {
        public SitemapSource? Source { get; set; } = SitemapSource.DefaultSitemap | SitemapSource.DefaultSitemapIndex | SitemapSource.RobotsTxt;
        public IList<SitemapsSource> CustomSources { get; } = new List<SitemapsSource>();
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