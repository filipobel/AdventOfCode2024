using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
public abstract class MapableTestableBaseDay : TestableBaseDay
{
    protected readonly Map map;

    public MapableTestableBaseDay()
    {
        map = GetMap(File.ReadAllText(InputFilePath));
    }

    protected (int, int) AddTuple((int, int) firstTuple, (int, int) secondTuple)
    {
        return (firstTuple.Item1 + secondTuple.Item1, firstTuple.Item2 + secondTuple.Item2);
    }

    protected Map GetMap(string input)
    {
        var stringMap = input.Split("\r\n");

        return (
            from y in Enumerable.Range(0, stringMap.Length)
            from x in Enumerable.Range(0, stringMap[0].Length)
            select new KeyValuePair<(int, int), char>((y, x), stringMap[y][x])
            ).ToDictionary();
    }
}
