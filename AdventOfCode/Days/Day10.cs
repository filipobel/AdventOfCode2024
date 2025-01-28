namespace AdventOfCode.Days;
public class Day10 : MapableTestableBaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var trailHeads = map.Keys.Where(x => map[x] == '0');
        var trails = trailHeads.ToDictionary(t => t, t => GetTrailsFrom(t)).Sum(t => t.Value.Distinct().Count());
        return new(trails.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var trailHeads = map.Keys.Where(x => map[x] == '0');
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
            if (map[currentPoint] == '9')
            {
                trails.Add(currentPoint);
            }
            else
            {
                foreach (var direction in new[] { MapExtensions.UP, MapExtensions.RIGHT, MapExtensions.DOWN, MapExtensions.LEFT })
                {
                    var pointToCheck = AddTuple(currentPoint,direction);
                    if (map.GetValueOrDefault(pointToCheck) == map[currentPoint] + 1)
                        positionsToCheck.Enqueue(pointToCheck);
                }
            }
        }
        return trails;
    }
}
