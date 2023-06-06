using System;
using System.IO;
using System.Linq;

namespace Sitemapr.Utils
{
    internal static class WebsiteUriExtensions
    {
        public static bool TryWithPath(this Uri uri, string path, out Uri result)
        {
            var baseUri = uri.ToBaseUri();
            return Uri.TryCreate(baseUri, path, out result);
        }

        public static Uri ToBaseUri(this Uri uri)
        {
            return new UriBuilder(uri.Scheme, uri.Host, uri.Port).Uri;
        }

        public static Uri ToCleanUri(this Uri uri)
        {
            if (Path.HasExtension(uri.GetLeftPart(UriPartial.Query)))
            {
                var allPathSegmentsButTheLast = uri.Segments.Take(uri.Segments.Length - 1).ToArray();
                var newPath = Path.Combine(allPathSegmentsButTheLast);
                return new UriBuilder(uri.Scheme, uri.Host, uri.Port, newPath).Uri;
            }

            return new UriBuilder(uri.Scheme, uri.Host, uri.Port, uri.AbsolutePath).Uri;
        }
    }
}