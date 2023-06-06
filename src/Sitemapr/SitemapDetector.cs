using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sitemapr.SitemapSources;
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

        private async Task<IReadOnlyList<Uri>> GetSitemapsInternalAsync(Uri domainUri, SitemapDetectionOptions options, CancellationToken cancellationToken)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var cleanDomainUri = domainUri.ToCleanUri();

            var sitemapSources = options.GetSitemapSources();
            var sitemaps = new SortedSet<Uri>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.SitemapDetector);

            foreach (var sitemapSource in sitemapSources)
            {
                var sitemapSourceResult = await sitemapSource.GetSitemapUrisAsync(cleanDomainUri, httpClient, cancellationToken);
                
                // TODO: Check result
                
                foreach (var sitemapUri in sitemapSourceResult.SitemapUris)
                {
                    // TODO: Validate
                    // if (options.ValidateSitemaps)
                    // {
                    //     var isValid =_sitemapValidator.IsValidSitemap(sitemapUri, cancellationToken);
                    // }
                    
                    sitemaps.Add(sitemapUri);
                }
            }

            return sitemaps.ToList().AsReadOnly();
        }
    }

    public sealed class SitemapDetectionOptions
    {
        public DefaultSitemapSource? Source { get; set; } = DefaultSitemapSource.Sitemap | DefaultSitemapSource.SitemapIndex | DefaultSitemapSource.RobotsTxt;
        public IList<SitemapSource> Sources { get; } = new List<SitemapSource>();
        public bool ValidateSitemaps { get; set; } = true;
        public FilteringMode FilteringMode { get; set; }
    }

    [Flags]
    public enum DefaultSitemapSource
    {
        Sitemap,
        SitemapIndex,
        RobotsTxt
    }

    public enum FilteringMode
    {
        None,
        Invalid,
        Failed
    }
}