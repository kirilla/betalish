namespace Betalish.Common.Extensions;

public static class IEnumerableExtensions
{
    public static string? Join(this IEnumerable<string> list, string separator)
    {
        if (list == null) 
            return null;

        return string.Join(separator, list);
    }
}
