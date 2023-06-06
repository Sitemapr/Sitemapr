using Sitemapr.Utils;
using Xunit;

namespace Sitemapr.UnitTests;

public sealed class UrlCleaningTests
{
    [Theory]
    [InlineData("https://www.example.com", "https://www.example.com")]
    [InlineData("https://example.com", "https://example.com")]
    [InlineData("http://www.example.com", "http://www.example.com")]
    [InlineData("http://example.com", "http://example.com")]
    [InlineData("https://www.example.com:1337", "https://www.example.com:1337")]
    [InlineData("https://www.example.com/some/path/here", "https://www.example.com/some/path/here")]
    [InlineData("https://www.example.com/some/path/here.txt", "https://www.example.com/some/path/")]
    [InlineData("https://www.example.com/some/path/here?stop=hammertime", "https://www.example.com/some/path/here")]
    [InlineData("https://www.example.com/some/path/here.txt?stop=hammertime", "https://www.example.com/some/path/")]
    public void THEN_Return_Cleaned_Uri(string startUriString, string expectedUriString)
    {
        // Arrange
        var startUri = new Uri(startUriString);
        var expectedUri = new Uri(expectedUriString);

        // Act
        var actualUri = startUri.ToCleanUri();

        // Assert
        Assert.Equal(expectedUri, actualUri);
    }

    [Fact]
    public void WHEN_Uri_Has_Path_With_Extension_THEN_Strip_Last_Part_Of_Path()
    {
        // Arrange
        
        // Act
        
        // Assert
        
    }
}