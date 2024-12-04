using AdventOfCode2024.Solutions;

var solutions = new Dictionary<int, IDay>
{
    {1, new Day01()},
    {2, new Day02()},
    {3, new Day03()},
    {4, new Day04()},
};

var chosenDays = args switch
{
    [] => [solutions.Keys.OrderDescending().First()],
    ["--all"] => solutions.Keys.ToList(),
    _ => args.Select(int.Parse).ToList(),
};

chosenDays.Sort();

foreach (var day in chosenDays)
{
    if (solutions.TryGetValue(day, out var solution))
    {
        Console.WriteLine($"Day {day}");
        solution.Run();
    }
}