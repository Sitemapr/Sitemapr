using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.Utils;

namespace Sitemapr.Sitemap
{
    public sealed class StandardSitemapsSource : SitemapsSource
    {
        public StandardSitemapsSource(string sitemapPath)
        {
            SitemapPath = sitemapPath ?? throw new ArgumentNullException(nameof(sitemapPath));
        }

        public string SitemapPath { get; }
        
        internal override Task<IEnumerable<SitemapInfo>> GetSitemapPathsAsync(Uri domainUri, HttpClient httpClient, CancellationToken cancellationToken)
        {
            var sitemapUri = domainUri.WithPath(SitemapPath);
            
            return Task.FromResult(
                Enumerable.Repeat(new SitemapInfo(sitemapUri), 1)
            );
        }

        internal static StandardSitemapsSource CreateDefaultSitemapsSource() =>
            new StandardSitemapsSource("/sitemap.xml");
    }
}