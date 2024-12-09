namespace AdventOfCode2024.Core;

public static class Extensions
{
    public static IEnumerable<T> FilterNull<T>(this IEnumerable<T?> elems) =>
        elems.OfType<T>();
}