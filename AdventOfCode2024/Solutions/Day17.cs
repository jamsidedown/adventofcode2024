using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public struct Registers(long a, long b, long c) : IEquatable<Registers>
{
    public long A = a;
    public long B = b;
    public long C = c;

    public bool Equals(Registers other) =>
        A == other.A && B == other.B && C == other.C;

    public override bool Equals(object? obj) =>
        obj is Registers other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(A, B, C);
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

    private long Combo(Registers registers, int operand)
    {
        if (operand <= 3)
            return operand;

        if (operand == 4)
            return registers.A;

        if (operand == 5)
            return registers.B;

        if (operand == 6)
            return registers.C;

        throw new ArgumentException($"Invalid operand: {operand}");
    }

    private int Pow2(long power)
    {
        if (power == 0)
            return 1;

        return 2 << (int)(power - 1);
    }
    
    private List<int> Compute(Registers registers, int[] instructions)
    {
        var output = new List<int>();
        var pointer = 0;
        
        while (pointer < instructions.Length - 1)
        {
            var opcode = instructions[pointer++];
            var operand = instructions[pointer++];
            
            if (opcode == 0) // adv
            {
                var denominator = Pow2(Combo(registers, operand));
                registers = registers with { A = registers.A / denominator };
            }
            else if (opcode == 1) // bxl
            {
                registers = registers with { B = registers.B ^ operand };
            }
            else if (opcode == 2) // bst
            {
                registers = registers with { B = Combo(registers, operand) % 8 };
            }
            else if (opcode == 3) // jnz
            {
                if (registers.A == 0)
                    continue;
                pointer = operand;
            }
            else if (opcode == 4) // bxc
            {
                registers = registers with { B = registers.B ^ registers.C };
            }
            else if (opcode == 5) // out
            {
                var result = (int)(Combo(registers, operand) % 8);
                output.Add(result);
            }
            else if (opcode == 6) // bdv
            {
                var denominator = Pow2(Combo(registers, operand));
                registers = registers with { B = registers.A / denominator };
            }
            else if (opcode == 7) // cdv
            {
                var denominator = Pow2(Combo(registers, operand));
                registers = registers with { C = registers.A / denominator };
            }
            else
            {
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
        var output = Compute(registers, instructions);
        return string.Join(",", output);
    }

    public long Part2(int[] instructions)
    {
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