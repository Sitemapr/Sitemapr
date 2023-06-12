using Sitemapr.Utils;
using Xunit;

namespace Sitemapr.UnitTests;

public sealed class AppendPathToUriTests
{
    [Fact]
    public void WHEN_Uri_Has_No_Path_THEN_Append_Path_To_Uri()
    {
        // Arrange
        var baseUri = new Uri("https://www.example.com");
        const string pathToAppend = "/some/path";

        var expectedUri = new Uri("https://www.example.com/some/path");
        
        // Act
        var pathAppended = baseUri.TryAppendPath(pathToAppend, out var actualUri);

        // Assert
        Assert.True(pathAppended);
        Assert.Equal(expectedUri, actualUri);
    }
    
    [Fact]
    public void WHEN_Uri_Has_Path_THEN_Append_Path_After_Existing_Path()
    {
        // Arrange
        var baseUri = new Uri("https://www.example.com/some/path");
        const string pathToAppend = "/right/here";

        var expectedUri = new Uri("https://www.example.com/some/path/right/here");
        
        // Act
        var pathAppended = baseUri.TryAppendPath(pathToAppend, out var actualUri);

        // Assert
        Assert.True(pathAppended);
        Assert.Equal(expectedUri, actualUri);
    }
    
    [Fact]
    public void WHEN_Path_To_Append_Has_Query_Parameters_THEN_Append_Path_And_Query_Parameters()
    {
        // Arrange
        var baseUri = new Uri("https://www.example.com/some/path");
        const string pathToAppend = "/right/here?stop=hammertime";

        var expectedUri = new Uri("https://www.example.com/some/path/right/here?stop=hammertime");
        
        // Act
        var pathAppended = baseUri.TryAppendPath(pathToAppend, out var actualUri);

        // Assert
        Assert.True(pathAppended);
        Assert.Equal(expectedUri, actualUri);
    }
    
    [Fact]
    public void WHEN_Path_To_Append_Has_Trailing_Slash_THEN_Combined_Uri_Has_Trailing_Slash()
    {
        // Arrange
        var baseUri = new Uri("https://www.example.com/some/path");
        const string pathToAppend = "/right/here/";

        var expectedUri = new Uri("https://www.example.com/some/path/right/here/");
        
        // Act
        var pathAppended = baseUri.TryAppendPath(pathToAppend, out var actualUri);

        // Assert
        Assert.True(pathAppended);
        Assert.Equal(expectedUri, actualUri);
    }
}