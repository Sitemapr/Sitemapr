using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.Utils;

namespace Sitemapr.SitemapCollectors
{
    public sealed class RobotsTxtSitemapCollector : SitemapCollector
    {
        public RobotsTxtSitemapCollector(string robotsTxtPath)
        {
            RobotsTxtPath = robotsTxtPath ?? throw new ArgumentNullException(nameof(robotsTxtPath));
        }

        public string RobotsTxtPath { get; }

        internal override async Task<SitemapCollectionResult> GetSitemapsAsync(Uri rootUri, HttpClient httpClient, CancellationToken cancellationToken)
        {
            if (rootUri.TryAppendPath(RobotsTxtPath, out var robotsTxtUri) is false)
            {
                return SitemapCollectionResult.CreateInvalidUriResult();
            }
            
            try
            {
                var response = await httpClient.GetAsync(robotsTxtUri, cancellationToken);
            
                if (response.IsSuccessStatusCode is false && response.StatusCode == HttpStatusCode.NotFound)
                {
                    return SitemapCollectionResult.CreateNotFoundResult();
                }
                
                if (response.IsSuccessStatusCode is false)
                {
                    return SitemapCollectionResult.CreateFailedResult();
                }
            
                var sitemapPaths = new List<Uri>();

                var responseContentStream = await response.Content.ReadAsStreamAsync();
                using (var streamReader = new StreamReader(responseContentStream))
                {
                    while (streamReader.EndOfStream is false)
                    {
                        var robotsTxtLine = await streamReader.ReadLineAsync();
                        if (TryGetSitemapFromRobotsTxtLine(robotsTxtLine, out var sitemapUri))
                        {
                            sitemapPaths.Add(sitemapUri);
                        }
                    }
                }
            
                return SitemapCollectionResult.CreateSuccessfulResult(sitemapPaths);
            }
            catch(Exception exception)
            {
                return SitemapCollectionResult.CreateFailedResult(exception);
            }
        }

        private static bool TryGetSitemapFromRobotsTxtLine(string robotsTxtLine, out Uri sitemapPath)
        {
            if (robotsTxtLine.StartsWith("Sitemap:") is false)
            {
                sitemapPath = null;
                return false;
            }

            var sitemapSplit = robotsTxtLine.Split(new []{ ':' }, 2);

            if (sitemapSplit.Length != 2)
            {
                sitemapPath = null;
                return false;
            }

            var sitemapPathString = sitemapSplit[1].Trim();

            if (Uri.TryCreate(sitemapPathString, UriKind.Absolute, out var tempSitemapPath))
            {
                sitemapPath = tempSitemapPath;
                return true;
            }
            
            sitemapPath = null;
            return false;
        }
        
        public static RobotsTxtSitemapCollector CreateDefaultCollector() =>
            new RobotsTxtSitemapCollector("/robots.txt");
    }
}