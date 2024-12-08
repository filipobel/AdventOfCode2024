using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Map = System.Collections.Immutable.ImmutableDictionary<(int, int), char>;

namespace AdventOfCode.Days;

public class Day04 : TestableBaseDay
{
    private readonly Map _map;

    private readonly (int, int) _up = (-1,0);
    private readonly(int, int) _upRight = (-1, 1);
    private readonly(int, int) _right = (0, 1);
    private readonly (int, int) _downRight = (1, 1);

    public Day04()
    {
        _map = GetMap(File.ReadAllText(InputFilePath));
    }
    public override ValueTask<string> Solve_1()
    {

        var answer = (from location in _map.Keys
                      from direction in new[] { _up, _upRight, _right, _downRight }
                      where Matches(_map, location, direction, "XMAS")
                      select 1).Count();

        return new(answer.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = (from location in _map.Keys
                      where Matches(_map, location, _downRight, "MAS")
                        && Matches(_map, (location.Item1 +2, location.Item2), _upRight, "MAS")
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

    public Map GetMap(string input)
    {
        var stringMap = input.Split("\r\n");

        return (
            from y in Enumerable.Range(0, stringMap.Length)
            from x in Enumerable.Range(0,stringMap[0].Length)
            select new KeyValuePair<(int, int), char>((y,x), stringMap[y][x]) 
            ).ToImmutableDictionary();
    }

}
