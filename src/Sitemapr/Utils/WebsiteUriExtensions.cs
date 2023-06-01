using System;

namespace Sitemapr.Utils
{
    internal static class WebsiteUriExtensions
    {
        public static Uri ToRobotsTxtUri(this Uri uri) =>
            new UriBuilder(uri.Scheme, uri.Host, uri.Port, pathValue: "robots.txt").Uri;
    }
}