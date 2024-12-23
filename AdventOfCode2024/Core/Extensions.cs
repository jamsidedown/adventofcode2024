namespace AdventOfCode2024.Core;

public static class Extensions
{
    public static IEnumerable<T> FilterNull<T>(this IEnumerable<T?> elems) =>
        elems.OfType<T>();

    public static void AddBefore<T>(this LinkedList<T> list, T value, Func<T, bool> predicate)
    {
        var current = list.First;
        
        while (current != null)
        {
            if (predicate(current.Value))
            {
                list.AddBefore(current, value);
                return;
            }
            
            current = current.Next;
        }
    }

    public static IEnumerable<(T, T)> Pairwise<T>(this T[] elems)
    {
        for (var i = 1; i < elems.Length; i++)
            yield return (elems[i - 1], elems[i]);
    }
}