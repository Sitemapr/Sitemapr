// See https://aka.ms/new-console-template for more information

using Sitemapr.Fluent;

Console.WriteLine("Hello, World!");
Console.WriteLine("Lets get it started in here!!");

var sitemapAnalyzer = new SitemapAnalyzer();

var cookiebotUri = new Uri("https://www.cookiebot.com/");

var validSitemapUrlsList = sitemapAnalyzer.CreateSitemapDetector(cookiebotUri).GetValidSitemapUris().ToList();
var bob = sitemapAnalyzer.CreateSitemapDetector(cookiebotUri).