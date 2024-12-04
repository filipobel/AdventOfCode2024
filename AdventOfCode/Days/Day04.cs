using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Map = System.Collections.Immutable.ImmutableDictionary<(int, int), char>;

namespace AdventOfCode.Days;

public class Day04 : TestableBaseDay
{
    private readonly string _input;

    (int, int) Up = (-1,0);
    (int, int) UpRight = (-1, 1);
    (int, int) Right = (0, 1);
    (int, int) DownRight = (1, 1);

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);

    }
    public override ValueTask<string> Solve_1()
    {
        var Map= GetMap(_input);

        var answer = (from location in Map.Keys
                      from direction in new[] { Up, UpRight, Right, DownRight }
                      where Matches(Map, location, direction, "XMAS")
                      select 1).Count();

        return new(answer.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

    private bool Matches(Map map, (int,int) location, (int,int) direction, string pattern)
    {
        var chars = Enumerable.Range(0, pattern.Length).
            Select(i => map.GetValueOrDefault((location.Item1 + i*direction.Item1, location.Item2 + i*direction.Item2))).
            ToArray();

        return Enumerable.SequenceEqual(chars, pattern) || Enumerable.SequenceEqual(chars, pattern.Reverse());
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