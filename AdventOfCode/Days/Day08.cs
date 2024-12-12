using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days;
public class Day08 : TestableBaseDay
{
    private readonly Map _map;


    public Day08()
    {
        _map = File.ReadAllText(InputFilePath).GetMap();
    }

    public override ValueTask<string> Solve_1()
    {
        var antennasPositions = _map.Where(x => char.IsAsciiLetterOrDigit(x.Value)).Select(x => x.Key).ToList();
        var antiNodes = new List<(int,int)>();
        foreach (var sourceAntenna in antennasPositions)
        {
            foreach( var destinationAntenna in antennasPositions)
            {
                if(sourceAntenna != destinationAntenna && _map[sourceAntenna] == _map[destinationAntenna])
                {
                    var antiNode = GetAntiNodes(sourceAntenna, destinationAntenna);
                    if(_map.ContainsKey(antiNode))
                        antiNodes.Add(antiNode);
                }
            }
        }

        return new(antiNodes.Distinct().Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var antennasPositions = _map.Where(x => char.IsAsciiLetterOrDigit(x.Value)).Select(x => x.Key).ToList();
        var antiNodes = new List<(int, int)>();
        foreach (var sourceAntenna in antennasPositions)
        {
            foreach (var destinationAntenna in antennasPositions)
            {
                 
                if (sourceAntenna != destinationAntenna && _map[sourceAntenna] == _map[destinationAntenna])
                {
                    var jump = (destinationAntenna.Item1 - sourceAntenna.Item1, destinationAntenna.Item2 - sourceAntenna.Item2);
                    var antiNode = destinationAntenna;
                    while (_map.ContainsKey(antiNode))
                    {
                        antiNodes.Add(antiNode);
                        antiNode = antiNode.AddTuple(jump);
                    }
                }
            }
        }
        return new(antiNodes.Distinct().Count().ToString());
    }

    private (int, int) GetAntiNodes((int, int) source, (int, int) destination)
    {
        var jump = (destination.Item1 - source.Item1, destination.Item2 - source.Item2);

        return destination.AddTuple(jump);
    }
}
