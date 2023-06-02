using System;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Sitemapr
{
    public sealed class DefaultXmlSchemaSource : IXmlSchemaSource
    {
        private readonly Lazy<XmlSchemaSet> _lazySitemapXmlSchema = new Lazy<XmlSchemaSet>(() => LoadXmlSchema("Sitemapr.Xsd.sitemap.xsd"));
        private readonly Lazy<XmlSchemaSet> _lazySitemapIndexXmlSchema = new Lazy<XmlSchemaSet>(() => LoadXmlSchema("Sitemapr.Xsd.siteindex.xsd"));
        
        public XmlSchemaSet SitemapXmlSchema => _lazySitemapXmlSchema.Value;
        public XmlSchemaSet SitemapIndexXmlSchema => _lazySitemapIndexXmlSchema.Value;

        private static XmlSchemaSet LoadXmlSchema(string resourceName)
        {
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (resourceStream is null)
                {
                    throw new ArgumentException($"No XSD with the resource name '{resourceName}' was found.", nameof(resourceName));
                }
                
                var xmlSchemaSet = new XmlSchemaSet();
                xmlSchemaSet.Add(null, XmlReader.Create(resourceStream));
                return xmlSchemaSet;
            }
        }
    }
}