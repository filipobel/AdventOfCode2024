using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days;

public class Day02 : TestableBaseDay
{
    private readonly List<List<int>> _reports = new List<List<int>>();
    public Day02()
    {
        var input = File.ReadAllText(InputFilePath);

        foreach (string line in input.Split("\n"))
        {
            _reports.Add(line.Split(" ").Select(s => int.Parse(s)).ToList());
        }
    }
    public override ValueTask<string> Solve_1()
    {
        return new (_reports.Count(Valid).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = 0;
        foreach (var report in _reports)
        {
            if (Valid(report))
            {  
                answer++;
                continue;
            }

            for (var i = 0; i < report.Count; i++)
            {
                List<int> reportToDampen = new(report);
                reportToDampen.RemoveAt(i);

                if (Valid(reportToDampen))
                {
                    answer++;
                    break;
                }
            }
        }
        return new(answer.ToString());
    }

    private bool Valid(List<int> report)
    {
        var pairs = Enumerable.Zip(report, report.Skip(1));
        return pairs.All(p => 1 <= p.Second - p.First && p.Second - p.First <= 3) ||
                pairs.All(p => 1 <= p.First - p.Second && p.First - p.Second <= 3);
    }

    IEnumerable<int[]> Attenuate(int[] samples) =>
    from i in Enumerable.Range(0, samples.Length + 1)
    let before = samples.Take(i - 1)
    let after = samples.Skip(i)
    select Enumerable.Concat(before, after).ToArray();
}
