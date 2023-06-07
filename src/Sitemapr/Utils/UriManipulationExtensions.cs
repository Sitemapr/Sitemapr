using System;
using System.IO;
using System.Linq;

namespace Sitemapr.Utils
{
    public static class UriManipulationExtensions
    {
        public static bool TryAppendPath(this Uri uri, string path, out Uri result)
        {
            var uriPart = uri.AbsoluteUri.TrimEnd('/');
            var pathPart = path.TrimStart('/');
            var combinedUri = Path.Combine(uriPart, pathPart);
            return Uri.TryCreate(combinedUri, UriKind.Absolute, out result);
        }

        /// <summary>
        /// Cleans a <see cref="Uri"/> by removing the query part and by removing the last part of the path if it points to a file with an extension.
        /// </summary>
        /// <example>
        /// https://www.example.com:8080/some/path/to/file.txt?stop=hammertime
        /// becomes
        /// https://www.example.com:8080/some/path/to/
        /// </example>
        /// <param name="uri">The uri to clean.</param>
        /// <returns>The cleaned base uri.</returns>
        public static Uri ToCleanBaseUri(this Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            
            var path = uri.AbsolutePath;
            
            if (Path.HasExtension(uri.GetLeftPart(UriPartial.Query)))
            {
                var allPathSegmentsButTheLast = uri.Segments.Take(uri.Segments.Length - 1).ToArray();
                path = Path.Combine(allPathSegmentsButTheLast);
            }
            
            path = path.EndsWith("/") ? path : $"{path}/";

            return new UriBuilder(uri.Scheme, uri.Host, uri.Port, path).Uri;
        }
    }
}