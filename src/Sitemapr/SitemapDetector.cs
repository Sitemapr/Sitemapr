using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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

        public Task<IReadOnlyList<Uri>> GetSitemapsAsync(Uri uri)
        {
            
        }

        private async Task<List<Uri>> GetSitemapsFromRobotsTxt(Uri uri)
        {
            var robotsTxtUri = uri.ToRobotsTxtUri();
            var responseStream = await _httpClientFactory.CreateClient("bob").GetStreamAsync(robotsTxtUri);

            var sitemaps = new List<Uri>();
            using (var streamReader = new StreamReader(responseStream))
            {
                while (streamReader.EndOfStream is false)
                {
                    var line = await streamReader.ReadLineAsync();
                    if (line.StartsWith("Sitemap: ", StringComparison.OrdinalIgnoreCase))
                    {
                        
                    }
                }
            }

            return sitemaps;
        }
        
        private 
    }
}