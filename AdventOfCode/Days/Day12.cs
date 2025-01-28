using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Region = System.Collections.Generic.HashSet<(int, int)>;

namespace AdventOfCode.Days;
public class Day12 : MapableTestableBaseDay
{
    private Dictionary<(int, int), Region> regions;
    public Day12() : base()
    {
        var positions = map.Keys.ToHashSet();
        MapRegions(positions);
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 0;
        foreach(var region in regions.Values.Distinct())
        {
            var perimeter = 0;
            foreach(var point in region)
            {
                perimeter += FindPerimeter(point);
            }
            result += perimeter * region.Count;
        }
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private int FindPerimeter((int,int) point)
    {
        var perimeter = 0;
        var value = map[point];
        foreach (var direction in new[] { MapExtensions.UP, MapExtensions.RIGHT, MapExtensions.DOWN, MapExtensions.LEFT })
        {
            if(map.GetValueOrDefault(AddTuple(point, direction)) != value)
            {
                perimeter++;
            }
        }
        return perimeter;
    }

    private void MapRegions(Region initialPositions)
    {
        //Go over each point and use floodfill to find each region.
        regions = new Dictionary<(int, int), Region>();

        while (initialPositions.Any())
        {
            var startPoint = initialPositions.First();
            var startValue = map[startPoint];

            var region = new Region { startPoint };
            var regionQueue = new Queue<(int, int)>();
            regionQueue.Enqueue(startPoint);

            while (regionQueue.Any())
            {
                var point = regionQueue.Dequeue();
                regions[point] = region;
                initialPositions.Remove(point);
                foreach (var direction in new[] { MapExtensions.UP, MapExtensions.RIGHT, MapExtensions.DOWN, MapExtensions.LEFT })
                {
                    var newPoint = AddTuple(point, direction);
                    if (!region.Contains(newPoint) && map.GetValueOrDefault(newPoint) == startValue)
                    {
                        region.Add(newPoint);
                        regionQueue.Enqueue(newPoint);
                    }
                }
            }
        }
    }
}
