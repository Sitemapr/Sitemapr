namespace Sitemapr.Test.Shared.HttpClient;

public static class StreamContentFileLoader
{
    public static StreamContent CreateStreamContentFromFile(string filePath)
    {
        var fileStream = File.Open(filePath, FileMode.Open);
        return new StreamContent(fileStream);
    }
}