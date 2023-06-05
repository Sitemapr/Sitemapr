using System;

namespace Sitemapr.Utils
{
    internal static class WebsiteUriExtensions
    {
        public static Uri ToRobotsTxtUri(this Uri uri) =>
            new UriBuilder(uri.Scheme, uri.Host, uri.Port, pathValue: "robots.txt").Uri;

        public static Uri WithPath(this Uri uri, string path) =>
            new UriBuilder(uri.Scheme, uri.Host, uri.Port, pathValue: path).Uri;
    }
}