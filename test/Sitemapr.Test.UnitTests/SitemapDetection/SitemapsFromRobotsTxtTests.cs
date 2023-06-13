using System.Net;
using FluentAssertions;
using Sitemapr.SitemapCollectors;
using Sitemapr.Test.Shared.HttpMessageHandlers;
using Xunit;

namespace Sitemapr.Test.UnitTests.SitemapDetection;

public sealed class SitemapsFromRobotsTxtTests
{
    private readonly string _robotsTxtPath = "/robots.txt";
    private readonly Uri _domainUri = new Uri("https://www.example.com");
    
    [Fact]
    public async Task WHEN_RobotsTxtContainsNoSitemaps_THEN_ReturnEmptyResult()
    {
        // Arrange
        var robotsTxtContent = new StringContent(@"
            User-agent: *
            Disallow: /wp-admin/
            Allow: /wp-admin/admin-ajax.php
        ");

        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsTxtContent);
        
        var robotsTxtSitemapSource = new RobotsTxtSitemapCollector(_robotsTxtPath);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);
        
        // Act
        var result = await robotsTxtSitemapSource.GetSitemapsAsync(_domainUri, mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        Assert.Empty(result.SitemapUris);
    }

    [Fact]
    public async Task WHEN_RobotsTxtContainsSitemapsInTheEnd_THEN_ReturnAllSitemapUrls()
    {
        // Arrange
        var robotsTxtContent = new StringContent(@"User-agent: *
Disallow: /wp-admin/
Allow: /wp-admin/admin-ajax.php
Sitemap: https://www.example.com/en/sitemap.xml
Sitemap: https://www.example.com/da/sitemap.xml
Sitemap: https://www.example.com/de/sitemap.xml");

        var expectedSitemaps = new[]
        {
            new Uri("https://www.example.com/de/sitemap.xml"),
            new Uri("https://www.example.com/da/sitemap.xml"),
            new Uri("https://www.example.com/en/sitemap.xml")
        };

        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsTxtContent);
        
        var robotsTxtSitemapSource = new RobotsTxtSitemapCollector(_robotsTxtPath);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);
        
        // Act
        var result = await robotsTxtSitemapSource.GetSitemapsAsync(_domainUri, mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemaps, options => options.WithoutStrictOrdering());
    }

    [Fact]
    public async Task WHEN_RobotsTxtContainsSitemapsInDifferentPlaces_THEN_ReturnAllSitemapUrls()
    {
        // Arrange
        var robotsTxtContent = new StringContent(@"Sitemap: https://www.example.com/da/sitemap.xml
User-agent: *
Disallow: /wp-admin/
Allow: /wp-admin/admin-ajax.php
Sitemap: https://www.example.com/en/sitemap.xml
Sitemap: https://www.example.com/de/sitemap.xml");

        var expectedSitemaps = new[]
        {
            new Uri("https://www.example.com/de/sitemap.xml"),
            new Uri("https://www.example.com/da/sitemap.xml"),
            new Uri("https://www.example.com/en/sitemap.xml")
        };

        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsTxtContent);
        
        var robotsTxtSitemapSource = new RobotsTxtSitemapCollector(_robotsTxtPath);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);
        
        // Act
        var result = await robotsTxtSitemapSource.GetSitemapsAsync(_domainUri, mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemaps, options => options.WithoutStrictOrdering());
    }

    [Fact]
    public async Task WHEN_RobotsTxtContainsBrokenSitemapUrl_THEN_ReturnValidSitemapsUrls()
    {
        // Arrange
        var robotsTxtContent = new StringContent(@"User-agent: *
Disallow: /wp-admin/
Allow: /wp-admin/admin-ajax.php
Sitemap: httpswww.examp
Sitemap: https://www.example.com/en/sitemap.xml
Sitemap: https://www.example.com/de/sitemap.xml");

        var expectedSitemaps = new[]
        {
            new Uri("https://www.example.com/de/sitemap.xml"),
            new Uri("https://www.example.com/en/sitemap.xml")
        };

        var mockedHttpMessageHandler = new CustomHttpMessageHandler(HttpStatusCode.OK, robotsTxtContent);
        
        var robotsTxtSitemapSource = new RobotsTxtSitemapCollector(_robotsTxtPath);
        var mockedHttpClient = new HttpClient(mockedHttpMessageHandler);
        
        // Act
        var result = await robotsTxtSitemapSource.GetSitemapsAsync(_domainUri, mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Successful, result.Status);
        result.SitemapUris.Should().BeEquivalentTo(expectedSitemaps, options => options.WithoutStrictOrdering());
    }

    [Fact]
    public async Task WHEN_RobotsTxtIsNotFound_THEN_ReturnNotFoundResult()
    {
        // Arrange
        var robotsTxtSitemapSource = new RobotsTxtSitemapCollector(_robotsTxtPath);
        var mockedHttpClient = new HttpClient(new CustomHttpMessageHandler(HttpStatusCode.NotFound));
        
        // Act
        var result = await robotsTxtSitemapSource.GetSitemapsAsync(_domainUri, mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.NotFound, result.Status);
        Assert.Empty(result.SitemapUris);
    }

    [Fact]
    public async Task WHEN_RobotsTxtReturnError_InternalServerError_THEN_ReturnFailedResult()
    {
        // Arrange
        var robotsTxtSitemapSource = new RobotsTxtSitemapCollector(_robotsTxtPath);
        var mockedHttpClient = new HttpClient(new CustomHttpMessageHandler(HttpStatusCode.InternalServerError));
        
        // Act
        var result = await robotsTxtSitemapSource.GetSitemapsAsync(_domainUri, mockedHttpClient, CancellationToken.None);
        
        // Assert
        Assert.Equal(SitemapSourceStatus.Failed, result.Status);
        Assert.Empty(result.SitemapUris);
    }
}