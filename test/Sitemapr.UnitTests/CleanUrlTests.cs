using Sitemapr.Utils;
using Xunit;

namespace Sitemapr.UnitTests;

public sealed class CleanUrlTests
{
    [Theory]
    [InlineData("https://www.example.com/", "https://www.example.com/")]
    [InlineData("https://example.com/", "https://example.com/")]
    [InlineData("http://www.example.com/", "http://www.example.com/")]
    [InlineData("http://example.com/", "http://example.com/")]
    [InlineData("https://www.example.com:1337/", "https://www.example.com:1337/")]
    public void WHEN_Uri_Has_No_Path_Or_Query_THEN_Preserve_Schema_Host_And_Port(string startUriString, string expectedUriString)
    {
        // Arrange
        var startUri = new Uri(startUriString);
        var expectedUri = new Uri(expectedUriString);

        // Act
        var actualUri = startUri.ToCleanBaseUri();

        // Assert
        Assert.Equal(expectedUri, actualUri);
    }
    
    [Fact]
    public void WHEN_Uri_Does_Not_Have_Trailing_Slash_THEN_Clean_Uri_Has_Trailing_Slash()
    {
        // Arrange
        var uriWithoutTrailingSlash = new Uri("https://www.example.com/some/path/here");
        var expectedCleanedUri = new Uri("https://www.example.com/some/path/here/");

        // Act
        var actualCleanedUri = uriWithoutTrailingSlash.ToCleanBaseUri();

        // Assert
        Assert.Equal(expectedCleanedUri, actualCleanedUri);
    }

    [Fact]
    public void WHEN_Uri_Has_Path_Without_Extension_THEN_Preserve_Path()
    {
        // Arrange
        var uriWithPath = new Uri("https://www.example.com/some/path/here/");
        var expectedCleanedUri = new Uri("https://www.example.com/some/path/here/");

        // Act
        var actualCleanedUri = uriWithPath.ToCleanBaseUri();

        // Assert
        Assert.Equal(expectedCleanedUri, actualCleanedUri);
    }

    [Fact]
    public void WHEN_Uri_Has_Path_With_Extension_THEN_Strip_Last_Part_Of_Path()
    {
        // Arrange
        var uriWithExtension = new Uri("https://www.example.com/some/path/here.txt");
        var expectedCleanedUri = new Uri("https://www.example.com/some/path/");

        // Act
        var actualCleanedUri = uriWithExtension.ToCleanBaseUri();

        // Assert
        Assert.Equal(expectedCleanedUri, actualCleanedUri);
    }
    
    [Fact]
    public void WHEN_Uri_Has_Query_THEN_Strip_Query()
    {
        // Arrange
        var uriWithQuery = new Uri("https://www.example.com/some/path/here?stop=hammertime");
        var expectedCleanedUri = new Uri("https://www.example.com/some/path/here/");

        // Act
        var actualCleanedUri = uriWithQuery.ToCleanBaseUri();

        // Assert
        Assert.Equal(expectedCleanedUri, actualCleanedUri);
    }
}