using System.Collections.Generic;

namespace AdventOfCode.Days;

public class Day01 : TestableBaseDay
{
    private readonly List<int> _column1 = new List<int>();
    private readonly List<int> _column2 = new List<int>();

    public Day01()
    {
        var input = File.ReadAllText(InputFilePath);

        foreach (string line in input.Split("\n"))
        {
            var columns = line.Split("   ").Select(int.Parse).ToArray();

            _column1.Add(columns[0]);
            _column2.Add(columns[1]);
        }

        _column1.Sort();
        _column2.Sort();
    }

    public override ValueTask<string> Solve_1()
    {
        //now compare and get output
        var answer = _column1.Zip(_column2, (n1, n2) => Math.Abs(n1 - n2)).Sum();
        return new(answer.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var weights = _column2.CountBy(x => x).ToDictionary();
        var answer = _column1.Select(num => weights.GetValueOrDefault(num) * num).Sum();
        return new(answer.ToString());
    }


}
