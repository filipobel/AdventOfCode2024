using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cache = System.Collections.Concurrent.ConcurrentDictionary<(string, int), long>;

namespace AdventOfCode.Days;
public class Day11 : TestableBaseDay
{
    public string[] input;

    public Day11()
    {
        input = File.ReadAllText(InputFilePath).Split(" ");
    }

    public override ValueTask<string> Solve_1()
    {
        var cache = new Cache();
        return new(input.Sum(n => EvaluateInput(long.Parse(n),25,cache)).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var cache = new Cache();
        return new(input.Sum(n => EvaluateInput(long.Parse(n), 75, cache)).ToString());
    }

    private long EvaluateInput(long stoneNumber,int numberOfParses, Cache cache)
    {
        return cache.GetOrAdd((stoneNumber.ToString(), numberOfParses), key =>
        {
            switch (key)
            {
                case (_, 0):
                    return 1L;
                case ("0", _):
                    return EvaluateInput(1L, key.Item2 - 1, cache);
                case (var st, _) when st.Length % 2 == 0:
                    return EvaluateInput(long.Parse(st[(st.Length / 2)..]), key.Item2 - 1, cache) +
                        EvaluateInput(long.Parse(st[0..(st.Length / 2)]), key.Item2 - 1, cache);
                default:
                    return EvaluateInput(2024 * long.Parse(key.Item1), key.Item2 - 1, cache);
            }
        });
    }
}
