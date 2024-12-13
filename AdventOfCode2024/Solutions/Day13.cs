using System.Text.RegularExpressions;
using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public record ClawMachine(XyPair<long> Prize, XyPair<long> A, XyPair<long> B);

public class Day13 :IDay
{
    private readonly Regex _aPattern = new(@"Button A: X\+(\d+), Y\+(\d+)");
    private readonly Regex _bPattern = new(@"Button B: X\+(\d+), Y\+(\d+)");
    private readonly Regex _prizePattern = new(@"Prize: X=(\d+), Y=(\d+)");
    
    public ClawMachine[] Parse(string s)
    {
        var clawMachines = new List<ClawMachine>();
        
        foreach (var chunk in s.Trim().Split($"{Environment.NewLine}{Environment.NewLine}"))
        {
            var aMatch = _aPattern.Match(chunk);
            var bMatch = _bPattern.Match(chunk);
            var prizeMatch = _prizePattern.Match(chunk);

            if (!(aMatch.Success && bMatch.Success && prizeMatch.Success))
                throw new ParseException("Failed to parse claw machine");

            var aGroups = aMatch.Groups.Cast<Group>().Skip(1).Select(g => g.Value).Select(long.Parse).ToArray();
            var bGroups = bMatch.Groups.Cast<Group>().Skip(1).Select(g => g.Value).Select(long.Parse).ToArray();
            var prizeGroups = prizeMatch.Groups.Cast<Group>().Skip(1).Select(g => g.Value).Select(long.Parse).ToArray();

            var a = new XyPair<long>(aGroups[0], aGroups[1]);
            var b = new XyPair<long>(bGroups[0], bGroups[1]);
            var prize = new XyPair<long>(prizeGroups[0], prizeGroups[1]);
            
            clawMachines.Add(new ClawMachine(prize, a, b));
        }
        
        return clawMachines.ToArray();
    }

    private (bool, long, long) Solve(ClawMachine clawMachine)
    {
        // solve for b
        var bNumerator = (clawMachine.A.X * clawMachine.Prize.Y) - (clawMachine.A.Y * clawMachine.Prize.X);
        var bDenominator = (clawMachine.A.X * clawMachine.B.Y) - (clawMachine.A.Y * clawMachine.B.X);
        
        if (bDenominator == 0L || bNumerator % bDenominator != 0L)
            return (false, 0L, 0L);

        var b = bNumerator / bDenominator;

        var aNumerator = clawMachine.Prize.X - (clawMachine.B.X * b);
        var aDenominator = clawMachine.A.X;

        if (aDenominator == 0L || aNumerator % aDenominator != 0L)
            return (false, 0L, 0L);

        var a = aNumerator / aDenominator;

        return (true, a, b);
    }
    
    public long Part1(ClawMachine[] clawMachines)
    {
        var tokens = 0L;

        foreach (var clawMachine in clawMachines)
        {
            var (success, a, b) = Solve(clawMachine);
            if (success)
                tokens += (3 * a) + b;
        }

        return tokens;
    }

    public long Part2(ClawMachine[] clawMachines)
    {
        clawMachines = clawMachines.Select(machine => machine with { Prize = machine.Prize + 10000000000000L })
            .ToArray();

        return Part1(clawMachines);
    }
    
    public void Run()
    {
        var clawMachines = Parse(File.ReadAllText(FileHelpers.GetFilepath(13)));
        Console.WriteLine($"Part 1: {Part1(clawMachines)}");
        Console.WriteLine($"Part 2: {Part2(clawMachines)}");
    }
}