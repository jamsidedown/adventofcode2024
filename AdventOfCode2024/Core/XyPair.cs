namespace AdventOfCode2024.Core;

public readonly struct XyPair(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public static XyPair operator +(XyPair a, XyPair b) =>
        new XyPair(a.X + b.X, a.Y + b.Y);
}