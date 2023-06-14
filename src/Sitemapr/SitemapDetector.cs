using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.SitemapCollectors;
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

        public Task<IReadOnlyList<Sitemap>> GetSitemapsAsync(Uri domainUri)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            return GetSitemapsInternalAsync(domainUri, default, CancellationToken.None);
        }
        
        public Task<IReadOnlyList<Sitemap>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options)
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
        
        public Task<IReadOnlyList<Sitemap>> GetSitemapsAsync(Uri domainUri, SitemapDetectionOptions options, CancellationToken cancellationToken)
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

        private async Task<IReadOnlyList<Sitemap>> GetSitemapsInternalAsync(Uri domainUri, SitemapDetectionOptions options, CancellationToken cancellationToken)
        {
            if (domainUri is null)
            {
                throw new ArgumentNullException(nameof(domainUri));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var domainBaseUri = domainUri.ToCleanBaseUri();

            var sitemapSources = options.GetSitemapCollectors();
            var sitemaps = new Dictionary<Uri, Sitemap>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.SitemapDetector);

            foreach (var sitemapSource in sitemapSources)
            {
                var sitemapSourceResult = await sitemapSource.GetSitemapsAsync(domainBaseUri, httpClient, cancellationToken);
                
                // TODO: Check result
                
                foreach (var sitemapUri in sitemapSourceResult.SitemapUris)
                {
                    var sitemapStatus = SitemapStatus.Valid;
                    
                    if (options.ValidateSitemaps)
                    {
                        var validationResult = await _sitemapValidator.IsValidSitemapAsync(sitemapUri, cancellationToken);
                        
                        if (sitemaps.TryGetValue(sitemapUri, out var existingSitemap) && existingSitemap.Status.IsBetterThanOrEqualTo(validationResult.Status))
                        {
                            continue;
                        }
                    }

                    sitemaps[sitemapUri] = new Sitemap(
                        sitemapPath: sitemapUri,
                        status: sitemapStatus,
                        collector: sitemapSource
                    );
                }
            }

            return sitemaps.Values.ToList().AsReadOnly();
        }
    }

    public sealed class SitemapDetectionOptions
    {
        public DefaultSitemapCollectors? Collectors { get; set; }
        public IList<SitemapCollector> CustomCollectors { get; }
        public bool ValidateSitemaps { get; set; }
        public FilteringLevel FilteringLevel { get; set; }

        public SitemapDetectionOptions()
        {
            Collectors = DefaultSitemapCollectors.Sitemap | DefaultSitemapCollectors.SitemapIndex | DefaultSitemapCollectors.Robots;
            CustomCollectors = new List<SitemapCollector>();
            ValidateSitemaps = true;
            FilteringLevel = FilteringLevel.NotFound;
        }
    }

    [Flags]
    public enum DefaultSitemapCollectors
    {
        Sitemap,
        SitemapIndex,
        Robots
    }

    public enum FilteringLevel
    {
        None,
        Invalid,
        NotFound,
        Error
    }
}