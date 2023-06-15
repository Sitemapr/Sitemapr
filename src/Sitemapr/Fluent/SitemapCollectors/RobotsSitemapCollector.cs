using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sitemapr.Utils;

namespace Sitemapr.Fluent.SitemapCollectors
{
    public sealed class RobotsSitemapCollector : SitemapCollector
    {
        public RobotsSitemapCollector(Uri uri) : base(uri)
        {
        }
        
        internal override async Task<SitemapCollectionResult> GetSitemapsAsync(HttpClient httpClient, CancellationToken cancellationToken)
        {
            try
            {
                var response = await httpClient.GetAsync(Uri, cancellationToken);

                if (response.IsSuccessStatusCode is false)
                {
                    return new FailedSitemapCollectionResult(message: $"{response.StatusCode} - {response.ReasonPhrase}");
                }
            
                var sitemapUris = new List<Uri>();

                var responseContentStream = await response.Content.ReadAsStreamAsync();
                
                cancellationToken.ThrowIfCancellationRequested();
                
                using (var streamReader = new StreamReader(responseContentStream))
                {
                    while (streamReader.EndOfStream is false)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        
                        var robotsLine = await streamReader.ReadLineAsync();
                        var parsingResult = robotsLine.TryParseRobotsSitemapUri(out var sitemapUri);

                        if (parsingResult == RobotsSitemapUriParsingResult.ValidUrl)
                        {
                            sitemapUris.Add(sitemapUri);
                        }
                    }
                }

                return new SuccessfulSitemapCollectionResult(sitemapUris);
            }
            catch(Exception exception)
            {
                return new FailedSitemapCollectionResult(exception: exception);
            }
        }
    }
}