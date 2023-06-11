using System;

namespace Sitemapr
{
    public sealed class SitemapValidationResult
    {
        public SitemapValidationResult(SitemapStatus status, string message = null, Exception exception = null)
        {
            Status = status;
            Message = message;
            Exception = exception;
        }

        public SitemapStatus Status { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}