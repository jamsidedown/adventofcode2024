using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

using Rules = Dictionary<int, HashSet<int>>;

public class Day05 : IDay
{
    private Rules ParseRules(string[] lines)
    {
        var rules = new Rules();

        foreach (var line in lines)
        {
            var parts = line.Split('|').Select(int.Parse).ToArray();
            var key = parts[0];
            var value = parts[1];
            if (!rules.ContainsKey(key))
                rules[key] = new HashSet<int>();
            rules[key].Add(value);
        }
        
        return rules;
    }

    private int[][] ParseManuals(string[] lines)
    {
        return lines
            .Where(line => line.Length >= 1)
            .Select(line =>
                line.Split(',')
                    .Select(int.Parse)
                    .ToArray())
            .ToArray();
    }
    
    public (Rules, int[][]) Parse(string text)
    {
        var halves = text.Split($"{Environment.NewLine}{Environment.NewLine}");
        var rules = ParseRules(halves[0].Split(Environment.NewLine));
        var manuals = ParseManuals(halves[1].Split(Environment.NewLine));
        return (rules, manuals);
    }

    private bool IsManualValid(Rules rules, HashSet<int> seen, int[] manual)
    {
        var remaining = manual[..];
        
        while (remaining.Length > 0)
        {
            var curr = remaining[0];

            if (rules.TryGetValue(curr, out var rule))
            {
                if (rule.Intersect(seen).Any())
                    return false;
            }

            seen.Add(curr);
            remaining = remaining[1..];
        }

        return true;
    }

    private int Middle(int[] manual)
    {
        return manual[manual.Length / 2];
    }

    public int Part1(Rules rules, int[][] manuals)
    {
        // parse ruleset as a dictionary mapping each page to a set of pages that must come after it
        // for each page in a manual, keep track of pages seen
        // ensure that the set of pages that must come after has no intersection with pages seen
        // all manuals have odd number of pages, so middle is just length/2
        
        var total = 0;

        foreach (var manual in manuals)
        {
            if (IsManualValid(rules, new HashSet<int>(), manual))
                total += Middle(manual);
        }
        
        return total;
    }

    public int[] FixManual(Rules rules, int[] manual)
    {
        var manualSet = manual.ToHashSet();
        var relevantRules = new Rules();
        foreach (var pair in rules)
        {
            if (manualSet.Contains(pair.Key))
                relevantRules[pair.Key] = pair.Value.Intersect(manualSet).ToHashSet();
        }

        return relevantRules.OrderByDescending(pair => pair.Value.Count)
            .Select(pair => pair.Key)
            .ToArray();
    }

    public int Part2(Rules rules, int[][] manuals)
    {
        // only fix incorrect manuals
        // noticed while debugging that rules had unique numbers of pages that must come after
        // only look at relevant rules for a manual by intersecting rules with pages in a manual
        // order pages by number of rules that apply to it, highest to lowest
        // find middle same as part 1
        
        var total = 0;

        foreach (var manual in manuals)
        {
            if (IsManualValid(rules, new HashSet<int>(), manual))
                continue;
            
            var fixedManual = FixManual(rules, manual);
            total += Middle(fixedManual);
        }
        
        return total;
    }
    
    public void Run()
    {
        var (rules, manuals) = Parse(File.ReadAllText(FileHelpers.GetFilepath(5)));
        Console.WriteLine($"Part 1: {Part1(rules, manuals)}");
        Console.WriteLine($"Part 2: {Part2(rules, manuals)}");
    }
}