using System;

namespace Sitemapr.Utils
{
    internal static class RobotsSitemapStringExtensions
    {
        public static RobotsSitemapUriParsingResult TryParseRobotsSitemapUri(this string line, out Uri sitemapUri)
        {
            if (line.StartsWith("Sitemap:") is false)
            {
                sitemapUri = null;
                return RobotsSitemapUriParsingResult.NotSitemap;
            }

            var lineSplit = line.Split(new []{ ':' }, 2);

            if (lineSplit.Length != 2)
            {
                sitemapUri = null;
                return RobotsSitemapUriParsingResult.NotSitemap;
            }

            var sitemapUrl = lineSplit[1].Trim();

            if (Uri.TryCreate(sitemapUrl, UriKind.Absolute, out sitemapUri))
            {
                return RobotsSitemapUriParsingResult.ValidUrl;
            }
            
            sitemapUri = null;
            return RobotsSitemapUriParsingResult.InvalidUrl;
        }
    }

    internal enum RobotsSitemapUriParsingResult
    {
        ValidUrl,
        NotSitemap,
        InvalidUrl
    }
}