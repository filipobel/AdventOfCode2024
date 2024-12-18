namespace AdventOfCode.Days;
public class Day10 : TestableBaseDay
{
    private readonly Map _map;
    public Day10()
    {
        _map = File.ReadAllText(InputFilePath).GetMap();
    }
    public override ValueTask<string> Solve_1()
    {
        var trailHeads = _map.Keys.Where(x => _map[x] == '0');
        var trails = trailHeads.ToDictionary(t => t, t => GetTrailsFrom(t)).Sum(t => t.Value.Distinct().Count());
        return new(trails.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var trailHeads = _map.Keys.Where(x => _map[x] == '0');
        var trails = trailHeads.ToDictionary(t => t, t => GetTrailsFrom(t));
        return new(trails.Sum(t => t.Value.Count()).ToString());
    }

    public List<(int,int)> GetTrailsFrom((int, int) trailHead)
    {
        var positionsToCheck = new Queue<(int, int)>();
        positionsToCheck.Enqueue(trailHead);
        var trails = new List<(int,int)>();

        while (positionsToCheck.Any())
        {
            var currentPoint = positionsToCheck.Dequeue();
            if (_map[currentPoint] == '9')
            {
                trails.Add(currentPoint);
            }
            else
            {
                foreach (var direction in new[] { MapExtensions.UP, MapExtensions.RIGHT, MapExtensions.DOWN, MapExtensions.LEFT })
                {
                    var pointToCheck = currentPoint.AddTuple(direction);
                    if (_map.GetValueOrDefault(pointToCheck) == _map[currentPoint] + 1)
                        positionsToCheck.Enqueue(pointToCheck);
                }
            }
        }
        return trails;
    }
}
