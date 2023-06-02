using System.Xml.Schema;

namespace Sitemapr
{
    public interface IXmlSchemaSource
    {
        XmlSchemaSet SitemapXmlSchema { get; }
        XmlSchemaSet SitemapIndexXmlSchema { get; }
    }
}