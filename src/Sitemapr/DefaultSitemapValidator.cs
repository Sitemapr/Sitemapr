using System;
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

        public async Task<bool> IsValidSitemap(Uri sitemapPath, CancellationToken cancellationToken)
        {
            try
            {
                // TODO: Fix client name.
                var httpClient = _httpClientFactory.CreateClient("bob");
                var httpResponse = await httpClient.GetAsync(sitemapPath, cancellationToken);

                if (httpResponse.IsSuccessStatusCode is false)
                {
                    return false;
                }

                var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                var sitemapXmlDocument = XDocument.Load(responseStream);

                var xmlSchemas = _xmlSchemaSource.SitemapXmlSchema;
            
                var sitemapIsValid = true;
                sitemapXmlDocument.Validate(xmlSchemas, (sender, args) =>
                {
                    sitemapIsValid = false;
                });

                return sitemapIsValid;
            }
            catch
            {
                return false;
            }
        }
    }
}