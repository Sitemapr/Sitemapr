using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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

            var sitemapSources = options.GetSitemapSources();
            var sitemaps = new Dictionary<Uri, Sitemap>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.SitemapDetector);

            foreach (var sitemapSource in sitemapSources)
            {
                var sitemapSourceResult = await sitemapSource.GetSitemapUrisAsync(domainBaseUri, httpClient, cancellationToken);
                
                // TODO: Check result
                
                foreach (var sitemapUri in sitemapSourceResult.SitemapUris)
                {
                    var sitemapStatus = SitemapStatus.Valid;
                    
                    
                    if (options.ValidateSitemaps)
                    {
                        var validationResult = await _sitemapValidator.IsValidSitemapAsync(sitemapUri, cancellationToken);
                        
                    }
                    
                    
                    
                    if (sitemaps.TryGetValue(sitemapUri, out var existingSitemap))
                    {
                        
                    }

                    sitemaps[sitemapUri] = new Sitemap(
                        sitemapPath: sitemapUri,
                        status: sitemapStatus,
                        source: sitemapSource
                    );
                }
            }

            return sitemaps.Values.ToList().AsReadOnly();
        }

        // private bool IsBetterSitemap(Sitemap existingSitemap)
        // {
        //     if (existingSitemap is null)
        //     {
        //         throw new ArgumentNullException(nameof(existingSitemap));
        //     }
        //     
        //     
        // }
    }

    public sealed class SitemapDetectionOptions
    {
        public DefaultSitemapSource? DefaultSources { get; set; }
        public IList<SitemapSource> Sources { get; }
        public bool ValidateSitemaps { get; set; }
        public FilteringMode FilteringMode { get; set; }

        public SitemapDetectionOptions()
        {
            DefaultSources = DefaultSitemapSource.Sitemap | DefaultSitemapSource.SitemapIndex | DefaultSitemapSource.RobotsTxt;
            Sources = new List<SitemapSource>();
            ValidateSitemaps = true;
            FilteringMode = FilteringMode.OnFoundNot;
        }
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
        NoFiltering,
        OnValidationError,
        OnFoundNot,
        OnFailure
    }
}