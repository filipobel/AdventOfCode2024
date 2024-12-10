using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Map = System.Collections.Generic.Dictionary<(int, int), char>;

namespace AdventOfCode.Days;
public class Day06 : TestableBaseDay
{
    private readonly Map _map;
    private readonly (int, int) _start;

    private readonly (int, int) _up = (-1, 0);
    private readonly (int, int) _right = (0, 1);
    private readonly (int, int) _left = (0, -1);
    private readonly (int, int) _down = (1, 0);

    public Day06()
    {
        _map = GetMap(File.ReadAllText(InputFilePath));
        _start = _map.First(x => x.Value == '^').Key;
    }

    public override ValueTask<string> Solve_1()
    {
        Walk(_start, _up);

        return new(_map.Where(x => x.Value == 'X').Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

    public Map GetMap(string input)
    {
        var stringMap = input.Split("\r\n");

        return (
            from y in Enumerable.Range(0, stringMap.Length)
            from x in Enumerable.Range(0, stringMap[0].Length)
            select new KeyValuePair<(int, int), char>((y, x), stringMap[y][x])
            ).ToDictionary();
    }

    private void Walk((int,int) currentPosition, (int,int) direction)
    {
        while (_map.ContainsKey(currentPosition))
        {
            _map[currentPosition] = 'X';

            if (_map.GetValueOrDefault(currentPosition.AddTuple(direction)) == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                currentPosition = currentPosition.AddTuple(direction);
            } 
        }
    }

    private (int,int) RotateClockwise((int,int) point)
    {
        return (point.Item2, -point.Item1);
    }

    private void PrintMap(int size)
    {
        Console.WriteLine();
        foreach (var y in Enumerable.Range(0, size))
        {
            foreach (var x in Enumerable.Range(0, size))
            {
                Console.Write(_map.GetValueOrDefault((y,x)));
            }
             Console.WriteLine();
        }
    }
}
