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

    //TODO: need to implement caching here.

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
        //var result = 0L;
        //var stack = new Stack<KeyValuePair<long, int>>();

        //foreach (var item in input.Reverse())
        //{
        //    stack.Push(new KeyValuePair<long, int>(long.Parse(item), numberOfParses));
        //}
        //while (stack.Count > 0)
        //{
        //    var currentStone = stack.Pop();

        //    switch (currentStone)
        //    {
        //        case (_, 0):
        //            result++;
        //            break;
        //        case (0, _):
        //            stack.Push(new KeyValuePair<long, int>(1L, currentStone.Value - 1));
        //            break;
        //        case (var l, _) when l.ToString().Length % 2 == 0:
        //            var st = l.ToString();
        //            stack.Push(new KeyValuePair<long, int>(long.Parse(st[(st.Length / 2)..]), currentStone.Value - 1));
        //            stack.Push(new KeyValuePair<long, int>(long.Parse(st[0..(st.Length / 2)]), currentStone.Value - 1));
        //            break;
        //        default:
        //            stack.Push(new KeyValuePair<long, int>(2024 * currentStone.Key, currentStone.Value - 1));
        //            break;

        //    }
        //}
        //return result;
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
