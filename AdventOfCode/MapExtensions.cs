using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
public static class MapExtensions
{
    public static (int,int) AddTuple(this (int,int) firstTuple, (int,int) secondTuple)
    {
        return (firstTuple.Item1 + secondTuple.Item1, firstTuple.Item2 + secondTuple.Item2);
    }

    public static Map GetMap(this string input)
    {
        var stringMap = input.Split("\r\n");

        return (
            from y in Enumerable.Range(0, stringMap.Length)
            from x in Enumerable.Range(0, stringMap[0].Length)
            select new KeyValuePair<(int, int), char>((y, x), stringMap[y][x])
            ).ToDictionary();
    }
}
