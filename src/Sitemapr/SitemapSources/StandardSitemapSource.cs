using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.Utils;

namespace Sitemapr.SitemapSources
{
    internal sealed class StandardSitemapSource : SitemapSource
    {
        public StandardSitemapSource(string sitemapPath)
        {
            SitemapPath = sitemapPath ?? throw new ArgumentNullException(nameof(sitemapPath));
        }

        public string SitemapPath { get; }
        
        internal override Task<SitemapSourceResult> GetSitemapUrisAsync(Uri rootUri, HttpClient httpClient, CancellationToken cancellationToken)
        {
            if (rootUri is null)
            {
                throw new ArgumentNullException(nameof(rootUri));
            }
            
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }
            
            try
            {
                if (rootUri.TryAppendPath(SitemapPath, out var sitemapUri) is false)
                {
                    return Task.FromResult(SitemapSourceResult.CreateInvalidUriResult());
                }

                return Task.FromResult(SitemapSourceResult.CreateSuccessfulResult(new []{ sitemapUri }));
            }
            catch
            {
                return Task.FromResult(SitemapSourceResult.CreateInvalidUriResult());
            }
        }

        public static StandardSitemapSource CreateDefaultSource() =>
            new StandardSitemapSource(Constants.Paths.Sitemap);
    }
}