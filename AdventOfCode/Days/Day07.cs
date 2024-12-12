using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Days;
public class Day07 : TestableBaseDay
{
    public List<KeyValuePair<long, long[]>> _calibrations = new List<KeyValuePair<long, long[]>>();

    public Day07()
    {
       var _lines = File.ReadAllText(InputFilePath).Split("\r\n").ToList();
        foreach (var line in _lines)
        {
            var regMatches = Regex.Matches(line, @"\d+").Select(m => long.Parse(m.Value));
            _calibrations.Add(new KeyValuePair<long, long[]>(regMatches.First(), regMatches.Skip(1).ToArray()));
        }
    }

    public override ValueTask<string> Solve_1()
    {
       var answer = _calibrations.Where(x => CheckCalibrationOne(x.Key, x.Value[0], x.Value[1..])).Sum(x => x.Key);
        return new(answer.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = _calibrations.Where(x => CheckCalibrationTwo(x.Key, x.Value[0], x.Value[1..])).Sum(x => x.Key);
        return new(answer.ToString());
    }

    private bool CheckCalibrationOne(long target, long actual, long[] numbers)
    {
        if (numbers.Length == 0)
        { return target == actual; }
        else
        {
            return CheckCalibrationOne(target, actual + numbers[0], numbers[1..]) || CheckCalibrationOne(target, actual * numbers[0], numbers[1..]);
        };
    }

    private bool CheckCalibrationTwo(long target, long actual, long[] numbers)
    {
        if (numbers.Length == 0)
        { return target == actual; }
        else
        {
            if (actual > target)
                return false;
            return CheckCalibrationTwo(target, long.Parse($"{actual}{numbers[0]}"), numbers[1..]) || CheckCalibrationTwo(target, actual + numbers[0], numbers[1..]) || CheckCalibrationTwo(target, actual * numbers[0], numbers[1..]);
        };
    }
}
