using System.Collections.Generic;

namespace AdventOfCode.Days;

public class Day01 : TestableBaseDay
{
    private readonly string _input;

    private List<int> column1 = new List<int>();
    private List<int> column2 = new List<int>();

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);

        foreach (string line in _input.Split("\n"))
        {
            var columns = line.Split("   ").Select(s => int.Parse(s)).ToArray();

            column1.Add(columns[0]);
            column2.Add(columns[1]);
        }

        column1.Sort();
        column2.Sort();
    }

    public override ValueTask<string> Solve_1()
    {
        //now compare and get output
        var answer = column1.Zip(column2, (n1, n2) => Math.Abs(n1 - n2)).Sum();
        return new(answer.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var weights = column2.CountBy(x => x).ToDictionary();
        var answer = column1.Select(num => weights.GetValueOrDefault(num) * num).Sum();
        return new(answer.ToString());
    }


}
