using System.Net;
using FluentAssertions;
using Sitemapr.SitemapCollectors;
using Sitemapr.Test.Shared.HttpClient;
using Xunit;

namespace Sitemapr.Test.UnitTests.SitemapDetection;

public sealed class CollectSitemapsFromRobotsTests
{
    [Fact]
    public async Task WHEN_Robots_Contains_No_Sitemaps_THEN_Return_Empty_Result()
    {
        using var robotsStreamContent = StreamContentFactory.Robots.CreateWithNoSitemaps();
        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsStreamContent);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);
        
        var robotsSitemapSource = new RobotsSitemapCollector("/robots.txt");

        // Act
        var result = await robotsSitemapSource.GetSitemapsAsync(new Uri("https://www.example.com"), mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        Assert.Empty(result.SitemapUris);
    }

    [Fact]
    public async Task WHEN_Robots_Contains_Sitemaps_In_End_THEN_Return_All_Sitemaps()
    {
        // Arrange
        using var robotsStreamContent = StreamContentFactory.Robots.CreateWithMultipleSitemapsInEnd();
        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsStreamContent);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);

        var expectedSitemaps = new[]
        {
            new Uri("https://www.example.com/de/sitemap.xml"),
            new Uri("https://www.example.com/en/sitemap.xml"),
            new Uri("https://www.example.com/da/sitemap.xml")
        };
        
        var robotsSitemapSource = new RobotsSitemapCollector("/robots.txt");

        // Act
        var result = await robotsSitemapSource.GetSitemapsAsync(new Uri("https://www.example.com"), mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemaps, options => options.WithoutStrictOrdering());
    }

    [Fact]
    public async Task WHEN_Robots_Contains_Sitemaps_In_Different_Places_THEN_Return_All_Sitemap()
    {
        // Arrange
        using var robotsStreamContent = StreamContentFactory.Robots.CreateWithMultipleSitemapsInMultiplePlaces();
        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsStreamContent);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);

        var expectedSitemaps = new[]
        {
            new Uri("https://www.example.com/de/sitemap.xml"),
            new Uri("https://www.example.com/da/sitemap.xml"),
            new Uri("https://www.example.com/en/sitemap.xml")
        };
        
        var robotsSitemapSource = new RobotsSitemapCollector("/robots.txt");
        
        // Act
        var result = await robotsSitemapSource.GetSitemapsAsync(new Uri("https://www.example.com"), mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemaps, options => options.WithoutStrictOrdering());
    }

    [Fact]
    public async Task WHEN_Robots_Contains_Broken_Sitemap_THEN_Return_Valid_Sitemaps()
    {
        // Arrange
        using var robotsStreamContent = StreamContentFactory.Robots.CreateWithMultipleSitemapsOneBroken();
        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsStreamContent);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);

        var expectedSitemaps = new[]
        {
            new Uri("https://www.example.com/de/sitemap.xml"),
            new Uri("https://www.example.com/en/sitemap.xml")
        };
        
        var robotsSitemapSource = new RobotsSitemapCollector("/robots.txt");
        
        // Act
        var result = await robotsSitemapSource.GetSitemapsAsync(new Uri("https://www.example.com"), mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemaps, options => options.WithoutStrictOrdering());
    }

    [Fact]
    public async Task WHEN_Robots_Is_NotFound_THEN_Return_NotFound_Result()
    {
        // Arrange
        var robotsSitemapSource = new RobotsSitemapCollector("/robots.txt");
        var mockedHttpClient = new HttpClient(new CustomHttpMessageHandler(HttpStatusCode.NotFound));
        
        // Act
        var result = await robotsSitemapSource.GetSitemapsAsync(new Uri("https://www.example.com"), mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.NotFound, result.Status);
        Assert.Empty(result.SitemapUris);
    }

    [Fact]
    public async Task WHEN_Robots_Return_Error_InternalServerError_THEN_Return_Failed_Result()
    {
        // Arrange
        var robotsSitemapSource = new RobotsSitemapCollector("/robots.txt");
        var mockedHttpClient = new HttpClient(new CustomHttpMessageHandler(HttpStatusCode.InternalServerError));
        
        // Act
        var result = await robotsSitemapSource.GetSitemapsAsync(new Uri("https://www.example.com"), mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Failed, result.Status);
        Assert.Empty(result.SitemapUris);
    }

    [Theory]
    [InlineData("https://www.example.com", "robots.txt", "https://www.example.com/robots.txt")]
    [InlineData("https://www.example.com", "/robots.txt", "https://www.example.com/robots.txt")]
    [InlineData("https://www.example.com/some/path", "robots.txt", "https://www.example.com/some/path/robots.txt")]
    [InlineData("https://www.example.com/some/path/", "robots.txt", "https://www.example.com/some/path/robots.txt")]
    [InlineData("https://www.example.com/some/path/", "/robots.txt", "https://www.example.com/some/path/robots.txt")]
    [InlineData("https://www.example.com/some/path", "to/different/robots.txt", "https://www.example.com/some/path/to/different/robots.txt")]
    public async Task WHEN_Requesting_Sitemap_THEN_Add_Robots_Path_To_Root_Uri(string rootUri, string robotsPath, string expectedRobotsUri)
    {
        // Arrange
        var robotsSitemapSource = new RobotsSitemapCollector(robotsPath);
        var httpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.NotFound);
        var mockedHttpClient = new HttpClient(httpMessageHandler);
        
        // Act
        await robotsSitemapSource.GetSitemapsAsync(new Uri(rootUri), mockedHttpClient, CancellationToken.None);

        var handledHttpRequestMessages = httpMessageHandler.Requests;

        // Assert
        Assert.Equal(1, handledHttpRequestMessages.Count);
        Assert.Equal(expectedRobotsUri, handledHttpRequestMessages[0].RequestUri?.AbsoluteUri);
    }
}