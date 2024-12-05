using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day05Tests
{
    private readonly string _exampleInput = """
                                            47|53
                                            97|13
                                            97|61
                                            97|47
                                            75|29
                                            61|13
                                            75|53
                                            29|13
                                            97|29
                                            53|29
                                            61|53
                                            97|53
                                            61|29
                                            47|13
                                            75|47
                                            97|75
                                            47|61
                                            75|61
                                            47|29
                                            75|13
                                            53|13
                                            
                                            75,47,61,53,29
                                            97,61,53,29,13
                                            75,29,13
                                            75,97,47,61,53
                                            61,13,29
                                            97,13,75,29,47
                                            """;

    private readonly Day05 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var (rules, manuals) = _solution.Parse(_exampleInput);
        var result = _solution.Part1(rules, manuals);
        Assert.Equal(143, result);
    }

    [Fact]
    public void TestFixFirstIncorrectManual()
    {
        var manual = new[] { 75, 97, 47, 61, 53 };
        var expected = new[] { 97, 75, 47, 61, 53 };
        var (rules, _) = _solution.Parse(_exampleInput);
        var result = _solution.FixManual(rules, manual);
        foreach (var pair in expected.Zip(result))
        {
            var exp = pair.First;
            var actual = pair.Second;
            Assert.Equal(exp, actual);
        }
    }

    [Fact]
    public void TestPart2()
    {
        var (rules, manuals) = _solution.Parse(_exampleInput);
        var result = _solution.Part2(rules, manuals);
        Assert.Equal(123, result);
    }
}
