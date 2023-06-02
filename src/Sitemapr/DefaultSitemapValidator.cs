using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Sitemapr
{
    public sealed class DefaultSitemapValidator : ISitemapValidator
    {
        private readonly IXmlSchemaSource _xmlSchemaSource;

        public DefaultSitemapValidator(IXmlSchemaSource xmlSchemaSource)
        {
            _xmlSchemaSource = xmlSchemaSource;
        }

        public Task<bool> IsValidSitemap(XDocument sitemapXmlDocument)
        {
            if (sitemapXmlDocument is null)
            {
                throw new ArgumentNullException(nameof(sitemapXmlDocument));
            }

            var xmlSchemas = _xmlSchemaSource.SitemapXmlSchema;
            
            var sitemapIsValid = true;
            sitemapXmlDocument.Validate(xmlSchemas, (sender, args) =>
            {
                sitemapIsValid = false;
            });
            
            return Task.FromResult(sitemapIsValid);
        }
    }
}