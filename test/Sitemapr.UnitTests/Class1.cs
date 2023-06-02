using System.Xml.Linq;
using Xunit;

namespace Sitemapr.UnitTests;

public class Class1
{
    [Fact]
    public async Task Test()
    {
        var validator = new DefaultSitemapValidator();
        var doc = XDocument.Parse("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\"><url><loc>https://www.cookiebot.com/</loc><lastmod>2022-03-02T06:27:29+00:00</lastmod></url><url><loc>https://www.cookiebot.com/en/what-is-ccpa/</loc><lastmod>2022-04-28T12:23:30+00:00</lastmod></url></urlset>");
        var result = await validator.IsValidSitemap(doc);
        var bob = "";
    }
}