using System;
using System.Collections.Generic;
using System.Collections.Immutable;
namespace AdventOfCode.Days;

public class Day04 : MapableTestableBaseDay
{
    private readonly (int, int) _up = (-1,0);
    private readonly(int, int) _upRight = (-1, 1);
    private readonly(int, int) _right = (0, 1);
    private readonly (int, int) _downRight = (1, 1);

    public override ValueTask<string> Solve_1()
    {

        var answer = (from location in map.Keys
                      from direction in new[] { _up, _upRight, _right, _downRight }
                      where Matches(map, location, direction, "XMAS")
                      select 1).Count();

        return new(answer.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = (from location in map.Keys
                      where Matches(map, location, _downRight, "MAS")
                        && Matches(map, (location.Item1 +2, location.Item2), _upRight, "MAS")
                        select 1).Count();

        return new(answer.ToString());
    }

    private bool Matches(Map map, (int,int) location, (int,int) direction, string pattern)
    {
        var chars = Enumerable.Range(0, pattern.Length).
            Select(i => map.GetValueOrDefault((location.Item1 + i*direction.Item1, location.Item2 + i*direction.Item2))).
            ToArray();

        return chars.SequenceEqual(pattern) || chars.SequenceEqual(pattern.Reverse());
    }

}
