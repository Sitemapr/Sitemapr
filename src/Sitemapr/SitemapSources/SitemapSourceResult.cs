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

        public static SitemapSourceResult CreateSuccessfulResult(IEnumerable<Uri> sitemapUris) =>
            new SitemapSourceResult(sitemapUris, SitemapSourceStatus.Successful);

        public static SitemapSourceResult CreateInvalidUriResult() =>
            new SitemapSourceResult(Array.Empty<Uri>(), SitemapSourceStatus.InvalidUri);
        
        public static SitemapSourceResult CreateNotFoundResult(Exception exception = null) =>
            new SitemapSourceResult(Array.Empty<Uri>(), SitemapSourceStatus.NotFound, exception);

        public static SitemapSourceResult CreateFailedResult(Exception exception = null) =>
            new SitemapSourceResult(Array.Empty<Uri>(), SitemapSourceStatus.Failed, exception);
    }

    public enum SitemapSourceStatus
    {
        Successful,
        InvalidUri,
        NotFound,
        Failed
    }
}