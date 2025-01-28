using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days;
public class Day08 : MapableTestableBaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var antennasPositions = map.Where(x => char.IsAsciiLetterOrDigit(x.Value)).Select(x => x.Key).ToList();
        var antiNodes = new List<(int,int)>();
        foreach (var sourceAntenna in antennasPositions)
        {
            foreach( var destinationAntenna in antennasPositions)
            {
                if(sourceAntenna != destinationAntenna && map[sourceAntenna] == map[destinationAntenna])
                {
                    var antiNode = GetAntiNodes(sourceAntenna, destinationAntenna);
                    if(map.ContainsKey(antiNode))
                        antiNodes.Add(antiNode);
                }
            }
        }

        return new(antiNodes.Distinct().Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var antennasPositions = map.Where(x => char.IsAsciiLetterOrDigit(x.Value)).Select(x => x.Key).ToList();
        var antiNodes = new List<(int, int)>();
        foreach (var sourceAntenna in antennasPositions)
        {
            foreach (var destinationAntenna in antennasPositions)
            {
                 
                if (sourceAntenna != destinationAntenna && map[sourceAntenna] == map[destinationAntenna])
                {
                    var jump = (destinationAntenna.Item1 - sourceAntenna.Item1, destinationAntenna.Item2 - sourceAntenna.Item2);
                    var antiNode = destinationAntenna;
                    while (map.ContainsKey(antiNode))
                    {
                        antiNodes.Add(antiNode);
                        antiNode = AddTuple(antiNode, jump);
                    }
                }
            }
        }
        return new(antiNodes.Distinct().Count().ToString());
    }

    private (int, int) GetAntiNodes((int, int) source, (int, int) destination)
    {
        var jump = (destination.Item1 - source.Item1, destination.Item2 - source.Item2);

        return AddTuple(destination, jump);
    }
}
