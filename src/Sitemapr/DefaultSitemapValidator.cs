using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Sitemapr
{
    public sealed class DefaultSitemapValidator : ISitemapValidator
    {
        private readonly IXmlSchemaSource _xmlSchemaSource;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultSitemapValidator(IXmlSchemaSource xmlSchemaSource, IHttpClientFactory httpClientFactory)
        {
            _xmlSchemaSource = xmlSchemaSource;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SitemapValidationResult> IsValidSitemapAsync(Uri sitemapPath, CancellationToken cancellationToken)
        {
            try
            {
                XDocument sitemapXmlDocument = null;
                using (var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.SitemapValidator))
                using (var sitemapResponse = await httpClient.GetAsync(sitemapPath, cancellationToken))
                {
                    if (sitemapResponse.IsSuccessStatusCode is false && sitemapResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        return new SitemapValidationResult(SitemapStatus.NotFound, message: sitemapResponse.ReasonPhrase);
                    }

                    sitemapResponse.EnsureSuccessStatusCode();
                
                    sitemapXmlDocument = await LoadXmlDocumentFromHttpResponse(sitemapResponse);
                }
                
                var xmlSchemas = _xmlSchemaSource.SitemapXmlSchema;
                
                SitemapValidationResult validationResult = null;
                sitemapXmlDocument.Validate(xmlSchemas, (sender, args) =>
                {
                    if (args.Severity == XmlSeverityType.Warning)
                    {
                        validationResult = new SitemapValidationResult(
                            status: SitemapStatus.Invalid,
                            message: args.Message,
                            exception: args.Exception
                        );
                    }
                    
                    if (args.Severity == XmlSeverityType.Error)
                    {
                        validationResult = new SitemapValidationResult(
                            status: SitemapStatus.Failed,
                            message: args.Message,
                            exception: args.Exception
                        );
                    }
                });

                return validationResult ??  new SitemapValidationResult(status: SitemapStatus.Valid);
            }
            catch (Exception exception)
            {
                return new SitemapValidationResult(SitemapStatus.Failed, exception: exception);
            }
        }

        private static async Task<XDocument> LoadXmlDocumentFromHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            using (var sitemapStream = await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                return XDocument.Load(sitemapStream);
            }
        }
    }
}