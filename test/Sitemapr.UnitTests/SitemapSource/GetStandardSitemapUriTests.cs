using FluentAssertions;
using Moq;
using Sitemapr.SitemapSources;
using Xunit;

namespace Sitemapr.UnitTests.SitemapSource;

public sealed class GetStandardSitemapUriTests
{
    [Theory]
    [InlineData("", "")]
    public async Task WHEN_Sitemap_Path_Is_Valid_THEN_Return_SitemapUri(string domainUri, string validSitemapPath)
    {
        // Arrange
        //const string validSitemapPath = "/hello/sitemap.xml";
        var standardSitemapSource = new StandardSitemapSource(validSitemapPath);

        var expectedSitemapUris = new[] { new Uri("https://www.example.com/hello/sitemap.xml") };

        var mockedHttpClient = new Mock<HttpClient>();
        
        // Act
        var result = await standardSitemapSource.GetSitemapUrisAsync(new Uri("https://www.example.com"), new HttpClient(), CancellationToken.None);

        // Assert
        mockedHttpClient.VerifyNoOtherCalls();
        
        Assert.Equal(SitemapSourceStatus.Valid, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemapUris);
        Assert.Null(result.Exception);
    }

    [Fact]
    public void WHEN_Sitemap_Path_Is_Invalid_THEN_Return_InvalidUri_Result()
    {
        // Arrange
        
        // Act
        
        // Assert

    }
}