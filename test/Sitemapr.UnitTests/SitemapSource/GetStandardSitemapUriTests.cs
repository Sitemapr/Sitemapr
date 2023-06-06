using FluentAssertions;
using Moq;
using Sitemapr.SitemapSources;
using Xunit;

namespace Sitemapr.UnitTests.SitemapSource;

public sealed class GetStandardSitemapUriTests
{
    [Theory]
    [InlineData("https://www.example.com", "/hello/sitemap.xml","https://www.example.com/hello/sitemap.xml")]
    [InlineData("https://www.example.com/some/path", "/hello/sitemap.xml","https://www.example.com/some/path/hello/sitemap.xml")]
    public async Task WHEN_Sitemap_Path_Is_Valid_THEN_Return_SitemapUri(string domainUri, string validSitemapPath, string expectedSitemapUri)
    {
        // Arrange
        var standardSitemapSource = new StandardSitemapSource(validSitemapPath);
        var mockedHttpClient = new Mock<HttpClient>();
        
        var expectedSitemapUris = new[] { new Uri(expectedSitemapUri) };
        
        // Act
        var result = await standardSitemapSource.GetSitemapUrisAsync(new Uri(domainUri), new HttpClient(), CancellationToken.None);

        // Assert
        mockedHttpClient.VerifyNoOtherCalls();
        
        Assert.Equal(SitemapSourceStatus.Valid, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemapUris);
        Assert.Null(result.Exception);
    }

    [Fact]
    public async Task WHEN_Sitemap_Path_Is_Invalid_THEN_Return_InvalidUri_Result()
    {
        // Arrange
        var standardSitemapSource = new StandardSitemapSource("this is an invalid path");
        var mockedHttpClient = new Mock<HttpClient>();
        
        // Act
        var result = await standardSitemapSource.GetSitemapUrisAsync(new Uri("https://www.example.com"), new HttpClient(), CancellationToken.None);

        // Assert
        mockedHttpClient.VerifyNoOtherCalls();
        
        Assert.Equal(SitemapSourceStatus.InvalidUri, result.Status);
        Assert.Empty(result.SitemapUris);
        Assert.Null(result.Exception);
    }
}