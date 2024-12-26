using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day23Tests
{
    private readonly string _exampleInput = """
                                            kh-tc
                                            qp-kh
                                            de-cg
                                            ka-co
                                            yn-aq
                                            qp-ub
                                            cg-tb
                                            vc-aq
                                            tb-ka
                                            wh-tc
                                            yn-cg
                                            kh-ub
                                            ta-co
                                            de-co
                                            tc-td
                                            tb-wq
                                            wh-td
                                            ta-ka
                                            td-qp
                                            aq-cg
                                            wq-ub
                                            ub-vc
                                            de-ta
                                            wq-aq
                                            wq-vc
                                            wh-yn
                                            ka-de
                                            kh-ta
                                            co-tc
                                            wh-qp
                                            tb-vc
                                            td-yn
                                            """;
    
    private readonly Day23 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var connections = _solution.Parse(_exampleInput);
        var result = _solution.Part1(connections);
        Assert.Equal(7, result);
    }

    [Fact]
    public void TestPart2()
    {
        var connections = _solution.Parse(_exampleInput);
        var result = _solution.Part2(connections);
        Assert.Equal("co,de,ka,ta", result);
    }
}