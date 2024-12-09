using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public record ContiguousBlock(int Id, int Start, int Length);

public class Day09 : IDay
{
    public int[] ParseDisk(string s)
    {
        var map = s.Trim()
            .Select(c => c - '0')
            .ToArray();
        var isFile = true;
        var id = 0;
        var index = 0;

        var disk = new int[map.Sum()];
        for (var i = 0; i < disk.Length; i++)
            disk[i] = -2;
        
        foreach (var digit in map)
        {
            for (var i = 0; i < digit; i++)
                disk[index++] = isFile ? id : -1;

            isFile = !isFile;
            if (isFile)
                id++;
        }

        return disk;
    }

    public (ContiguousBlock[], ContiguousBlock[]) ParseBlocks(string s)
    {
        var map = s.Trim().Select(c => c - '0').ToArray();
        var isFile = true;
        var id = 0;
        var index = 0;

        var files = new List<ContiguousBlock>();
        var gaps = new List<ContiguousBlock>();
        foreach (var digit in map)
        {
            if (isFile)
                files.Add(new ContiguousBlock(id++, index, digit));
            else
                gaps.Add(new ContiguousBlock(-1, index, digit));

            index += digit;
            isFile = !isFile;
        }

        return (files.ToArray(), gaps.ToArray());
    }

    private int[] DefragBlocks(int[] disk)
    {
        var current = disk.ToArray();
        var from = 0;
        var to = current.Length - 1;

        while (true)
        {
            while (current[from] >= 0)
                from++;

            while (current[to] < 0)
                to--;

            if (from >= to)
                break;

            current[from++] = current[to];
            current[to--] = -1;
        }

        return current;
    }

    private Dictionary<int, LinkedList<int>> FindGaps(int[] disk)
    {
        var gaps = new Dictionary<int, LinkedList<int>>();
        
        for (var i = 0; i < disk.Length; i++)
        {
            if (disk[i] >= 0)
                continue;
            
            var start = i;
            var length = 0;

            while (disk[i++] < 0)
            {
                length++;
            }

            if (!gaps.ContainsKey(length))
                gaps[length] = [];
            gaps[length].AddLast(start);
        }

        return gaps;
    }

    // Key is length of gap
    // Value is linked list of gaps, ordered by starting index
    private Dictionary<int, LinkedList<ContiguousBlock>> GetGaps(ContiguousBlock[] gaps)
    {
        var dict = new Dictionary<int, LinkedList<ContiguousBlock>>();
        
        foreach (var gap in gaps.OrderBy(g => g.Start))
        {
            if (!dict.ContainsKey(gap.Length))
                dict[gap.Length] = [];
            dict[gap.Length].AddLast(gap);
        }

        return dict;
    }

    private ContiguousBlock? FindGap(Dictionary<int, LinkedList<ContiguousBlock>> gaps, ContiguousBlock file)
    {
        return gaps
            .Where(pair => pair.Key >= file.Length)
            .Select(pair => pair.Value.First?.Value)
            .Where(cb => cb is not null)
            .Where(g => g.Start < file.Start)
            .OrderBy(g => g.Start)
            .FirstOrDefault();
    }

    private int[] ToDisk(List<ContiguousBlock> files)
    {
        var lastFile = files.MaxBy(f => f.Start);
        if (lastFile is null)
            throw new NullReferenceException("Files cannot be null");

        var lastIndex = lastFile.Start + lastFile.Length;

        var disk = new int[lastIndex];

        foreach (var file in files)
        {
            for (var i = 0; i < file.Length; i++)
                disk[file.Start + i] = file.Id;
        }

        return disk;
    }
    
    private int[] DefragFiles(ContiguousBlock[] files, ContiguousBlock[] gaps)
    {
        var movedFiles = new List<ContiguousBlock>();
        var remainingFiles = new LinkedList<ContiguousBlock>(files.OrderByDescending(f => f.Id));
        var gapMap = GetGaps(gaps);

        while (remainingFiles.Count > 0)
        {
            var file = remainingFiles.First?.Value;
            remainingFiles.RemoveFirst();

            if (file is null)
                throw new NullReferenceException("File should not be null");

            var gap = FindGap(gapMap, file);
            if (gap is not null)
            {
                movedFiles.Add(file with {Start = gap.Start});
                gapMap[gap.Length].Remove(gap);

                if (gap.Length > file.Length)
                {
                    var newGap = gap with { Length = gap.Length - file.Length, Start = gap.Start + file.Length };

                    var targetList = gapMap[newGap.Length];
                    var addBefore = targetList.First;
                    while (addBefore is not null && addBefore.Value.Start < newGap.Start)
                        addBefore = addBefore.Next;
                    if (addBefore is null)
                        targetList.AddLast(newGap);
                    else
                        targetList.AddBefore(addBefore, newGap);
                }
            }
            else
            {
                movedFiles.Add(file);
            }
        }

        return ToDisk(movedFiles);
    }

    private long Checksum(int[] disk)
    {
        var checksum = 0L;
        
        for (var i = 0; i < disk.Length; i++)
            if (disk[i] > 0)
                checksum += disk[i] * i;
        
        return checksum;
    }
    
    public long Part1(int[] disk)
    {
        var defragged = DefragBlocks(disk);
        return Checksum(defragged);
    }
    
    public long Part2(ContiguousBlock[] files, ContiguousBlock[] gaps)
    {
        var defragged = DefragFiles(files, gaps);
        return Checksum(defragged);
    }
    
    public void Run()
    {
        var s = File.ReadAllText(FileHelpers.GetFilepath(9));
        var disk = ParseDisk(s);
        Console.WriteLine($"Part 1: {Part1(disk)}");
        var (files, gaps) = ParseBlocks(s);
        Console.WriteLine($"Part 2: {Part2(files, gaps)}");
    }
}