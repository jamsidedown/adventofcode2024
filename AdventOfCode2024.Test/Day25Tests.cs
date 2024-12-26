using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day25Tests
{
    private readonly string _exampleInput = """
                                            #####
                                            .####
                                            .####
                                            .####
                                            .#.#.
                                            .#...
                                            .....

                                            #####
                                            ##.##
                                            .#.##
                                            ...##
                                            ...#.
                                            ...#.
                                            .....

                                            .....
                                            #....
                                            #....
                                            #...#
                                            #.#.#
                                            #.###
                                            #####

                                            .....
                                            .....
                                            #.#..
                                            ###..
                                            ###.#
                                            ###.#
                                            #####

                                            .....
                                            .....
                                            .....
                                            #....
                                            #.#..
                                            #.#.#
                                            #####
                                            """;
    
    private readonly Day25 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var (keys, locks) = _solution.Parse(_exampleInput);
        var result = _solution.Part1(keys, locks);
        Assert.Equal(3, result);
    }
}