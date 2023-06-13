namespace Sitemapr.Test.Shared.HttpClient;

public static class StreamContentFactory
{
    public static class RobotsTxt
    {
        public static StreamContent CreateWithNoSitemaps()
            => StreamContentFileLoader.CreateStreamContentFromFile("./Files/Robots_NoSitemaps.txt");
        public static StreamContent CreateWithMultipleSitemapsInEnd()
            => StreamContentFileLoader.CreateStreamContentFromFile("./Files/Robots_MultipleSitemapInEnd.txt");
        
        public static StreamContent CreateWithMultipleSitemapsInMultiplePlaces()
            => StreamContentFileLoader.CreateStreamContentFromFile("./Files/Robots_MultipleSitemapsInMultiplePlaces.txt");
        public static StreamContent CreateWithMultipleSitemapsOneBroken()
            => StreamContentFileLoader.CreateStreamContentFromFile("./Files/Robots_MultipleSitemapsOneBroken.txt");
    }
}