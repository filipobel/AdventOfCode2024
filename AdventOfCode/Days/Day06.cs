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

    public Day06()
    {
        _map = GetMap(File.ReadAllText(InputFilePath));
        _start = _map.First(x => x.Value == '^').Key;
    }

    public override ValueTask<string> Solve_1()
    {
        var visited = Walk(_start, _up);

        return new(visited.Distinct().Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var visited = Walk(_start, _up).Distinct();

        var loops = 0;

        foreach (var testPosition in visited.Where(pos => _map[pos] == '.'))
        {
            _map[testPosition] = '#';
            if (IsBlocked(_map, _start, _up))
            {
                loops++;
            }
            _map[testPosition] = '.';
        }
        return new(loops.ToString());
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

    private bool IsBlocked(Map _map, (int, int) currentPosition, (int, int) direction)
    {
        var visited = new HashSet<((int, int) position, (int,int) direction)>();
        while (_map.ContainsKey(currentPosition) && !visited.Contains((currentPosition,direction)))
        {
            visited.Add((currentPosition, direction));

            if (_map.GetValueOrDefault(currentPosition.AddTuple(direction)) == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                currentPosition = currentPosition.AddTuple(direction);
            }
        }

        return visited.Contains((currentPosition, direction));
    }

    private HashSet<(int, int)> Walk((int,int) currentPosition, (int,int) direction)
    {
        var visited = new HashSet<(int, int)>();
        while (_map.ContainsKey(currentPosition))
        {
            visited.Add(currentPosition);

            if (_map.GetValueOrDefault(currentPosition.AddTuple(direction)) == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                currentPosition = currentPosition.AddTuple(direction);
            } 
        }

        return visited;
    }

    private (int,int) RotateClockwise((int,int) point)
    {
        return (point.Item2, -point.Item1);
    }
}
