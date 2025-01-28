using System.Collections.Immutable;

namespace AdventOfCode.Days;
public class Day06 : MapableTestableBaseDay
{
    private readonly (int, int) _start;

    public Day06() : base()
    {
        _start = map.First(x => x.Value == '^').Key;
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

        foreach (var testPosition in visited.Where(pos => map[pos] == '.'))
        {
            map[testPosition] = '#';
            if (IsBlocked(map, _start, MapExtensions.UP))
            {
                loops++;
            }
            map[testPosition] = '.';
        }
        return new(loops.ToString());
    }

    private bool IsBlocked(Map map, (int, int) currentPosition, (int, int) direction)
    {
        var visited = new HashSet<((int, int) position, (int,int) direction)>();
        while (map.ContainsKey(currentPosition) && !visited.Contains((currentPosition,direction)))
        {
            visited.Add((currentPosition, direction));

            if (map.GetValueOrDefault(AddTuple(currentPosition, direction)) == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                currentPosition = AddTuple(currentPosition, direction);
            }
        }

        return visited.Contains((currentPosition, direction));
    }

    private HashSet<(int, int)> Walk((int,int) currentPosition, (int,int) direction)
    {
        var visited = new HashSet<(int, int)>();
        while (map.ContainsKey(currentPosition))
        {
            visited.Add(currentPosition);

            if (map.GetValueOrDefault(AddTuple(currentPosition, direction)) == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                currentPosition = AddTuple(currentPosition, direction);
            } 
        }

        return visited;
    }

    private (int,int) RotateClockwise((int,int) point)
    {
        return (point.Item2, -point.Item1);
    }
}
