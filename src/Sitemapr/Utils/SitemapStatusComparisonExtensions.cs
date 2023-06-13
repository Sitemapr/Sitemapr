namespace Sitemapr.Utils
{
    internal static class SitemapStatusComparisonExtensions
    {
        public static bool IsBetterThanOrEqualTo(this SitemapStatus rhs, SitemapStatus lhs)
        {
            return (int) rhs <= (int) lhs;
        }
    }
}