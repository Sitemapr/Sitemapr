using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.Utils;

namespace Sitemapr.SitemapCollectors
{
    internal sealed class StandardSitemapCollector : SitemapCollector
    {
        public StandardSitemapCollector(string sitemapPath)
        {
            SitemapPath = sitemapPath ?? throw new ArgumentNullException(nameof(sitemapPath));
        }

        public string SitemapPath { get; }
        
        internal override Task<SitemapCollectionResult> GetSitemapsAsync(Uri rootUri, HttpClient httpClient, CancellationToken cancellationToken)
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
                    return Task.FromResult(SitemapCollectionResult.CreateInvalidUriResult());
                }

                return Task.FromResult(SitemapCollectionResult.CreateSuccessfulResult(new []{ sitemapUri }));
            }
            catch
            {
                return Task.FromResult(SitemapCollectionResult.CreateInvalidUriResult());
            }
        }

        public static StandardSitemapCollector CreateDefaultCollector() =>
            new StandardSitemapCollector(Constants.Paths.Sitemap);
    }
}