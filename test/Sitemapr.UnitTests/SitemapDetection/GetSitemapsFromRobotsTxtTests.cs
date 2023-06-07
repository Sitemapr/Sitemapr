using Moq;
using Sitemapr.SitemapSources;
using Xunit;

namespace Sitemapr.UnitTests.SitemapDetection;

public sealed class GetSitemapsFromRobotsTxtTests
{
    [Fact]
    public async Task WHEN_RobotsTxtContainsNoSitemaps_THEN_ReturnEmptyResult()
    {
        
    }

    [Fact]
    public async Task WHEN_RobotsTxtContainsSitemapsInTheEnd_THEN_ReturnAllSitemapUrls()
    {
        
    }

    [Fact]
    public async Task WHEN_RobotsTxtContainsSitemapsInDifferentPlaces_THEN_ReturnAllSitemapUrls()
    {
        
    }

    [Fact]
    public async Task WHEN_RobotsTxtContainsBrokenSitemapUrl_THEN_ReturnValidSitemapsUrls()
    {
        
    }

    [Fact]
    public async Task WHEN_RobotsTxtIsNotFound_THEN_ReturnFailedResult()
    {
        // Arrange
        const string robotsTxtPath = "/robots.txt";
        var domainUri = new Uri("https://www.example.com");
        
        var robotsTxtSitemapSource = new RobotsTxtSitemapSource(robotsTxtPath);

        var mockedHttpClient = new Mock<HttpClient>(MockBehavior.Strict);
        
        // Act
        var result = await robotsTxtSitemapSource.GetSitemapUrisAsync(domainUri, mockedHttpClient.Object, CancellationToken.None);
        
        // Assert
    }
}