using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.Utils;

namespace Sitemapr.Sitemap
{
    public sealed class RobotsTxtSitemapsSource : SitemapsSource
    {
        public RobotsTxtSitemapsSource(string robotsTxtPath)
        {
            RobotsTxtPath = robotsTxtPath ?? throw new ArgumentNullException(nameof(robotsTxtPath));
        }

        public string RobotsTxtPath { get; }

        internal override async Task<IEnumerable<SitemapInfo>> GetSitemapPathsAsync(Uri domainUri, HttpClient httpClient, CancellationToken cancellationToken)
        {
            var robotsTxtUri = domainUri.WithPath(RobotsTxtPath);
            var response = await httpClient.GetAsync(RobotsTxtPath, cancellationToken);

            if (response.IsSuccessStatusCode is false)
            {
                // TODO: Find a better way to handle this.
                throw new ApplicationException("Unable to read robots.txt");
            }

            var sitemapInfo = new List<SitemapInfo>();

            var responseContentStream = await response.Content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(responseContentStream))
            {
                while (streamReader.EndOfStream is false)
                {
                    var robotsTxtLine = await streamReader.ReadLineAsync();
                    var sitemapPath = AnalyzeRobotsTxtLine(robotsTxtLine);
                    
                }
            }

            return sitemapInfo;
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
    }
}