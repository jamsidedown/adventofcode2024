using System.Collections.Concurrent;

namespace AdventOfCode2024.Core;

public static class Cache
{
    public static Func<TInput, TOutput> Memoize<TInput, TOutput>(Func<TInput, TOutput> func) where TInput : notnull
    {
        var cache = new ConcurrentDictionary<TInput, TOutput>();
        return input => cache.GetOrAdd(input, func);
    }
    
    public static Func<TInput, TOutput> Memoize<TInput, TOutput>(Func<Func<TInput, TOutput>, TInput, TOutput> func) where TInput : notnull
    {
        var cache = new ConcurrentDictionary<TInput, TOutput>();
        TOutput Rec(TInput input) => cache.GetOrAdd(input, _ => func(Rec, input));
        return Rec;
    }
}