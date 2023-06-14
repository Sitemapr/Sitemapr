using System.Net;
using Sitemapr.SitemapCollectors;
using Sitemapr.Test.Shared.HttpClient;
using Xunit;

namespace Sitemapr.Test.UnitTests.SitemapDetection;

public sealed class CollectSitemapTests
{
    [Theory]
    [InlineData("https://www.example.com", "sitemap.xml", "https://www.example.com/sitemap.xml")]
    [InlineData("https://www.example.com", "/sitemap.xml", "https://www.example.com/sitemap.xml")]
    [InlineData("https://www.example.com/some/path", "sitemap.xml", "https://www.example.com/some/path/sitemap.xml")]
    [InlineData("https://www.example.com/some/path/", "sitemap.xml", "https://www.example.com/some/path/sitemap.xml")]
    [InlineData("https://www.example.com/some/path/", "/sitemap.xml", "https://www.example.com/some/path/sitemap.xml")]
    [InlineData("https://www.example.com/some/path", "to/different/sitemap.xml", "https://www.example.com/some/path/to/different/sitemap.xml")]
    public async Task WHEN_Requesting_Sitemap_THEN_Add_Sitemap_Path_To_Root_Uri(string rootUri, string sitemapPath, string expectedSitemapUri)
    {
        // Arrange
        var standardSitemapCollector = new StandardSitemapCollector(sitemapPath);
        var httpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.NotFound);
        var mockedHttpClient = new HttpClient(httpMessageHandler);
        
        // Act
        var result = await standardSitemapCollector.GetSitemapsAsync(new Uri(rootUri), mockedHttpClient, CancellationToken.None);


        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        Assert.Equal(1, result.SitemapUris.Count);
        Assert.Equal(expectedSitemapUri, result.SitemapUris.First().AbsoluteUri);
    }
}