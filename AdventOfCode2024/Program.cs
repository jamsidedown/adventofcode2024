﻿using AdventOfCode2024.Solutions;

var solutions = new Dictionary<int, IDay>
{
    {1, new Day01()},
    {2, new Day02()},
    {3, new Day03()},
    {4, new Day04()},
    {5, new Day05()},
    {6, new Day06()},
    {7, new Day07()},
    {8, new Day08()},
    {9, new Day09()},
    {10, new Day10()},
    {11, new Day11()},
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