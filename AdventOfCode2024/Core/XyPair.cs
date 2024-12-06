namespace AdventOfCode2024.Core;

public readonly struct XyPair(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public override int GetHashCode() =>
        (X, Y).GetHashCode();

    public static XyPair operator +(XyPair a, XyPair b) =>
        new(a.X + b.X, a.Y + b.Y);
}