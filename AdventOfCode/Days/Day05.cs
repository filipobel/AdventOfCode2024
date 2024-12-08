using System.Text;

namespace AdventOfCode.Days;

public class Day05: TestableBaseDay
{
    string _input;
    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var (updates, comparer) = Parse(_input);
        return new (updates
            .Where(pages => Sorted(pages, comparer))
            .Sum(GetMiddlePage).ToString());
        
    }

    public override ValueTask<string> Solve_2()
    {
        var (updates, comparer) = Parse(_input);
        return new(updates
            .Where(pages => !Sorted(pages, comparer))
            .Select(pages => pages.OrderBy(p => p, comparer).ToArray())
            .Sum(GetMiddlePage).ToString());
    }

    (string[][] updates, Comparer<string>) Parse(string input) {
        var parts = input.Split("\r\n\r\n");

        var ordering = new HashSet<string>(parts[0].Split("\r\n"));
        var comparer = 
            Comparer<string>.Create((p1, p2) => ordering.Contains(p1 + "|" + p2) ? -1 : 1);

        var updates = parts[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(line => line.Split(",")).ToArray();
        return (updates, comparer);
    }
    
    int GetMiddlePage(string[] nums) => int.Parse(nums[nums.Length / 2]);
   
    bool Sorted(string[] pages, Comparer<string> comparer) =>
        Enumerable.SequenceEqual(pages, pages.OrderBy(x=>x, comparer));

}