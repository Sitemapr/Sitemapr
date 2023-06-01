using System;

namespace Sitemapr.Utils
{
    internal static class SitemapUriExtensions
    {
        public static Uri ToDefaultSitemapUri(this Uri uri) =>
            new UriBuilder(uri.Scheme, uri.Host, uri.Port, pathValue: "sitemap.xml").Uri;
        
        public static Uri ToDefaultSitemapIndexUri(this Uri uri) =>
            new UriBuilder(uri.Scheme, uri.Host, uri.Port, pathValue: "sitemap_index.xml").Uri;
    }
}