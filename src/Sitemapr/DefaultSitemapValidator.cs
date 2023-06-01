using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Sitemapr
{
    public sealed class DefaultSitemapValidator : ISitemapValidator
    {
        public async Task<bool> IsValidSitemap(XDocument sitemapXmlDocument)
        {
            if (sitemapXmlDocument is null)
            {
                throw new ArgumentNullException(nameof(sitemapXmlDocument));
            }

            var xmlSchemas = LoadXmlSchemaSet();
            
            var sitemapIsValid = true;
            sitemapXmlDocument.Validate(xmlSchemas, (sender, args) =>
            {
                sitemapIsValid = false;
            });
            return sitemapIsValid;
            
            System.AppContext.
        }

        private static XmlSchemaSet LoadXmlSchemaSet()
        {
            return new XmlSchemaSet().Add("", XmlReader.Create(MediaTypeNames.Application.))
        }
    }
}