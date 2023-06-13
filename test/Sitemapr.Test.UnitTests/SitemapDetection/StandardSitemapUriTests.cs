using FluentAssertions;
using Moq;
using Sitemapr.SitemapCollectors;
using Xunit;

namespace Sitemapr.Test.UnitTests.SitemapDetection;

public sealed class GetStandardSitemapUriTests
{
    [Theory]
    [InlineData("https://www.example.com", "/hello/sitemap.xml","https://www.example.com/hello/sitemap.xml")]
    [InlineData("https://www.example.com/some/path", "/hello/sitemap.xml","https://www.example.com/some/path/hello/sitemap.xml")]
    public async Task WHEN_Sitemap_Path_Is_Valid_THEN_Return_SitemapUri(string domainUri, string validSitemapPath, string expectedSitemapUri)
    {
        // Arrange
        var standardSitemapSource = new StandardSitemapCollector(validSitemapPath);
        var mockedHttpClient = new Mock<HttpClient>();
        
        var expectedSitemapUris = new[] { new Uri(expectedSitemapUri) };
        
        // Act
        var result = await standardSitemapSource.GetSitemapsAsync(new Uri(domainUri), new HttpClient(), CancellationToken.None);

        // Assert
        mockedHttpClient.VerifyNoOtherCalls();
        
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemapUris);
        Assert.Null(result.Exception);
    }
}