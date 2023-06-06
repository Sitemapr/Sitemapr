using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitemapr.SitemapSources
{
    public sealed class SitemapSourceResult
    {
        public SitemapSourceResult(IEnumerable<Uri> sitemapUris, SitemapSourceStatus status, Exception exception = null)
        {
            SitemapUris = sitemapUris?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(sitemapUris));
            Status = status;
            Exception = exception;
        }

        public IReadOnlyCollection<Uri> SitemapUris { get; }
        public SitemapSourceStatus Status { get; }
        public Exception Exception { get; }

        public static SitemapSourceResult CreateValidResult(IEnumerable<Uri> sitemapUris) =>
            new SitemapSourceResult(sitemapUris, SitemapSourceStatus.Valid);

        public static SitemapSourceResult CreateInvalidUriResult() =>
            new SitemapSourceResult(Array.Empty<Uri>(), SitemapSourceStatus.InvalidUri);

        public static SitemapSourceResult CreateErrorResult(Exception exception = null) =>
            new SitemapSourceResult(Array.Empty<Uri>(), SitemapSourceStatus.Invalid, exception);
    }

    public enum SitemapSourceStatus
    {
        Valid,
        InvalidUri,
        Invalid
    }
}