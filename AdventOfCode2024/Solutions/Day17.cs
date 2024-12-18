using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public struct Registers(long a, long b, long c)
{
    public long A { get; set; } = a;
    public long B { get; set; } = b;
    public long C { get; set; } = c;
}

public class Day17 : IDay
{
    public (Registers, int[]) Parse(string s)
    {
        var parts = s.Trim().Split($"{Environment.NewLine}{Environment.NewLine}");
        
        var registers = parts[0].Trim()
            .Split(Environment.NewLine)
            .Select(line => line.Trim().Split(' ')[^1])
            .Select(long.Parse)
            .ToArray();
        
        var instructions = parts[1].Trim()
            .Split(' ')[1]
            .Split(',')
            .Select(int.Parse)
            .ToArray();
        
        return (new Registers(registers[0], registers[1], registers[2]), instructions);
    }

    private long Combo(Registers registers, int operand) =>
        operand switch
        {
            <= 3 => operand,
            4 => registers.A,
            5 => registers.B,
            6 => registers.C,
            _ => throw new ArgumentException($"Invalid operand: {operand}")
        };

    private int Pow2(long power) =>
        power == 0 ? 1 : 2 << (int)(power - 1);

    private List<int> Compute(Registers registers, int[] instructions)
    {
        var output = new List<int>();
        var pointer = 0;
        
        while (pointer < instructions.Length - 1)
        {
            var opcode = instructions[pointer++];
            var operand = instructions[pointer++];
            
            switch (opcode)
            {
                // adv
                case 0:
                    registers.A /= Pow2(Combo(registers, operand));
                    break;
                // bxl
                case 1:
                    registers.B ^= operand;
                    break;
                // bst
                case 2:
                    registers.B = Combo(registers, operand) % 8;
                    break;
                // jnz
                case 3 when registers.A == 0:
                    continue;
                case 3:
                    pointer = operand;
                    break;
                // bxc
                case 4:
                    registers.B ^= registers.C;
                    break;
                // out
                case 5:
                    output.Add((int)(Combo(registers, operand) % 8));
                    break;
                // bdv
                case 6:
                    registers.B = registers.A / Pow2(Combo(registers, operand));
                    break;
                // cdv
                case 7:
                    registers.C = registers.A / Pow2(Combo(registers, operand));
                    break;
                default:
                    throw new InvalidDataException($"Invalid opcode: {opcode}");
            }
        }

        return output;
    }
    
    private bool SequenceEqual(int[] expected, List<int> actual)
    {
        if (expected.Length != actual.Count)
            return false;

        for (var i = 0; i < expected.Length; i++)
        {
            if (expected[i] != actual[i])
                return false;
        }
        
        return true;
    }

    public string Part1(Registers registers, int[] instructions)
    {
        // bulk of the work is in the Compute function
        // copy opcode definitions to a switch statement
        // output to a list
        
        var output = Compute(registers, instructions);
        return string.Join(",", output);
    }

    public long Part2(int[] instructions)
    {
        // wrote down the instructions on paper, and realised that for the last digit register A would need to be less than 8
        // this is due to the adv 3 instruction in my puzzle input
        // for the second last digit, 8 <= A < 64
        // third last, 64 <= A < 512 etc.
        // keep adding powers of 8 to feasible numbers that produce the correct ending digits
        
        var possibleAs = new List<long> { 0 };
        
        for (var digit = 0; digit < instructions.Length; digit++)
        {
            var nextPossibleAs = new List<long>();
            
            foreach (var tail in possibleAs)
            {
                for (var head = 0; head < 8; head++)
                {
                    var a = (tail * 8) + head;
                    var output = Compute(new Registers(a, 0, 0), instructions);
                    if (SequenceEqual(instructions[^(digit + 1)..], output))
                        nextPossibleAs.Add(a);
                }
            }

            possibleAs = nextPossibleAs;
        }

        return possibleAs.Min();
    }
    
    public void Run()
    {
        var (registers, instructions) = Parse(FileHelpers.ReadText(17));
        Console.WriteLine($"Part 1: {Part1(registers, instructions)}");
        Console.WriteLine($"Part 2: {Part2(instructions)}");
    }
}