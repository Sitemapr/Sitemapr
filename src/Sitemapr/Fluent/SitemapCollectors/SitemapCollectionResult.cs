using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitemapr.Fluent.SitemapCollectors
{
    public abstract class SitemapCollectionResult
    {

    }

    public sealed class SuccessfulSitemapCollectionResult : SitemapCollectionResult
    {
        public SuccessfulSitemapCollectionResult(IEnumerable<Uri> sitemapUris)
        {
            SitemapUris = sitemapUris?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(sitemapUris));
        }

        public IReadOnlyCollection<Uri> SitemapUris { get; }
    }

    public sealed class FailedSitemapCollectionResult : SitemapCollectionResult
    {
        public FailedSitemapCollectionResult(Exception exception = null, string message = null)
        {
            Exception = exception;
            Message = message;
        }

        public Exception Exception { get; }
        public string Message { get; }
    }
}