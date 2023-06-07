using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.Utils;

namespace Sitemapr.SitemapSources
{
    public sealed class RobotsTxtSitemapSource : SitemapSource
    {
        public RobotsTxtSitemapSource(string robotsTxtPath)
        {
            RobotsTxtPath = robotsTxtPath ?? throw new ArgumentNullException(nameof(robotsTxtPath));
        }

        public string RobotsTxtPath { get; }

        internal override async Task<SitemapSourceResult> GetSitemapUrisAsync(Uri rootUri, HttpClient httpClient, CancellationToken cancellationToken)
        {
            if (rootUri.TryAppendPath(RobotsTxtPath, out var robotsTxtUri) is false)
            {
                return SitemapSourceResult.CreateInvalidUriResult();
            }
            
            try
            {
                var response = await httpClient.GetAsync(robotsTxtUri, cancellationToken);
            
                if (response.IsSuccessStatusCode is false)
                {
                    return SitemapSourceResult.CreateFailedResult();
                }
            
                var sitemapUris = new List<Uri>();
            
                var responseContentStream = await response.Content.ReadAsStreamAsync();
                using (var streamReader = new StreamReader(responseContentStream))
                {
                    while (streamReader.EndOfStream is false)
                    {
                        var robotsTxtLine = await streamReader.ReadLineAsync();
                        var sitemapUri = AnalyzeRobotsTxtLine(robotsTxtLine);
                        sitemapUris.Add(sitemapUri);
                    }
                }
            
                return SitemapSourceResult.CreateSuccessfulResult(sitemapUris);
            }
            catch(Exception exception)
            {
                return SitemapSourceResult.CreateFailedResult(exception);
            }
        }

        private static Uri AnalyzeRobotsTxtLine(string robotsTxtLine)
        {
            if (robotsTxtLine.StartsWith("Sitemap:") is false)
            {
                return null;
            }

            var sitemapSplit = robotsTxtLine.Split(new []{ ':' }, 2);

            if (sitemapSplit.Length != 2)
            {
                return null;
            }

            var sitemapPathString = sitemapSplit[1].Trim();

            return Uri.TryCreate(sitemapPathString, UriKind.Absolute, out var sitemapPath) ? sitemapPath : null;
        }
        
        public static RobotsTxtSitemapSource CreateDefaultSource() =>
            new RobotsTxtSitemapSource("/robots.txt");
    }
}