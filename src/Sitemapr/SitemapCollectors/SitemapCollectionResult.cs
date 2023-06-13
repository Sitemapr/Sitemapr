using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitemapr.SitemapCollectors
{
    public sealed class SitemapCollectionResult
    {
        public SitemapCollectionResult(IEnumerable<Uri> sitemapUris, SitemapSourceStatus status, Exception exception = null)
        {
            SitemapUris = sitemapUris?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(sitemapUris));
            Status = status;
            Exception = exception;
        }

        public IReadOnlyCollection<Uri> SitemapUris { get; }
        public SitemapSourceStatus Status { get; }
        public Exception Exception { get; }

        public static SitemapCollectionResult CreateSuccessfulResult(IEnumerable<Uri> sitemapUris) =>
            new SitemapCollectionResult(sitemapUris, SitemapSourceStatus.Successful);

        public static SitemapCollectionResult CreateInvalidUriResult() =>
            new SitemapCollectionResult(Array.Empty<Uri>(), SitemapSourceStatus.InvalidUri);
        
        public static SitemapCollectionResult CreateNotFoundResult(Exception exception = null) =>
            new SitemapCollectionResult(Array.Empty<Uri>(), SitemapSourceStatus.NotFound, exception);

        public static SitemapCollectionResult CreateFailedResult(Exception exception = null) =>
            new SitemapCollectionResult(Array.Empty<Uri>(), SitemapSourceStatus.Failed, exception);
    }

    public enum SitemapSourceStatus
    {
        Successful,
        InvalidUri,
        NotFound,
        Failed
    }
}