using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Days;

public class Day03 : TestableBaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);

    }
    public override ValueTask<string> Solve_1()
    {
        var answer = Regex.Matches(_input, @"mul\((\d{1,3}),(\d{1,3})\)")
            .Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value))
            .Sum();
        
        return new(answer.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var doCalculation = true;
        var matches = Regex.Matches(_input, @"mul\((\d{1,3}),(\d{1,3})\)|don't\(\)|do\(\)");
        var answer = 0;

        foreach(Match match in matches)
        {
            switch (match.Value)
            {
                case "don't()":
                    doCalculation = false;
                    break;
                case "do()":
                    doCalculation = true;
                    break;
                default:
                    if (doCalculation)
                        answer += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                    break;
            }
        }

        return new(answer.ToString());
    }

}
