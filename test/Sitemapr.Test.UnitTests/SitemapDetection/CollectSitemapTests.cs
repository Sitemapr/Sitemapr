using System.Net;
using Sitemapr.SitemapCollectors;
using Sitemapr.Test.Shared.HttpClient;
using Xunit;

namespace Sitemapr.Test.UnitTests.SitemapDetection;

public sealed class GetStandardSitemapUriTests
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
        var robotsTxtSitemapSource = new StandardSitemapCollector(sitemapPath);
        var httpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.NotFound);
        var mockedHttpClient = new HttpClient(httpMessageHandler);
        
        // Act
        await robotsTxtSitemapSource.GetSitemapsAsync(new Uri(rootUri), mockedHttpClient, CancellationToken.None);

        var handledHttpRequestMessages = httpMessageHandler.Requests;

        // Assert
        Assert.Equal(1, handledHttpRequestMessages.Count);
        Assert.Equal(expectedSitemapUri, handledHttpRequestMessages[0].RequestUri?.AbsoluteUri);
    }
}