using System.Numerics;

namespace AdventOfCode2024.Core;

public readonly struct XyPair<T>(T x, T y) : IEquatable<XyPair<T>> where T : INumber<T>
{
    public T X { get; } = x;
    public T Y { get; } = y;

    public override int GetHashCode() =>
        (X, Y).GetHashCode();

    public static XyPair<T> operator +(XyPair<T> a, XyPair<T> b) =>
        new(a.X + b.X, a.Y + b.Y);

    public static bool operator ==(XyPair<T> a, XyPair<T> b) =>
        a.X == b.X && a.Y == b.Y;

    public static bool operator !=(XyPair<T> a, XyPair<T> b) =>
        a.X != b.X || a.Y != b.Y;

    public bool Equals(XyPair<T> other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is XyPair<T> other && Equals(other);
    }
}