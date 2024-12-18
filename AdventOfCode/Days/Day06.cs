using System.Collections.Immutable;

namespace AdventOfCode.Days;
public class Day06 : TestableBaseDay
{
    private readonly Map _map;
    private readonly (int, int) _start;

    public Day06()
    {
        _map = File.ReadAllText(InputFilePath).GetMap();
        _start = _map.First(x => x.Value == '^').Key;
    }

    public override ValueTask<string> Solve_1()
    {
        var visited = Walk(_start, MapExtensions.UP);

        return new(visited.Distinct().Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var visited = Walk(_start, MapExtensions.UP).Distinct();

        var loops = 0;

        foreach (var testPosition in visited.Where(pos => _map[pos] == '.'))
        {
            _map[testPosition] = '#';
            if (IsBlocked(_map, _start, MapExtensions.UP))
            {
                loops++;
            }
            _map[testPosition] = '.';
        }
        return new(loops.ToString());
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
