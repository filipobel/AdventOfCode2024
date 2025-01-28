using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return new(EvaluateInput(25).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private int EvaluateInput(int numberOfParse)
    {
        var result = 0;
        var stack = new Stack<KeyValuePair<long, int>>();

        foreach (var item in input.Reverse())
        {
            stack.Push(new KeyValuePair<long, int>(long.Parse(item), numberOfParse));
        }
        while (stack.Count > 0)
        {
            var currentStone = stack.Pop();

            switch (currentStone)
            {
                case (_, 0):
                    result++;
                    break;
                case (0, _):
                    stack.Push(new KeyValuePair<long, int>(1L, currentStone.Value - 1));
                    break;
                case (var l, _) when l.ToString().Length % 2 == 0:
                    var st = l.ToString();
                    stack.Push(new KeyValuePair<long, int>(long.Parse(st[(st.Length / 2)..]), currentStone.Value - 1));
                    stack.Push(new KeyValuePair<long, int>(long.Parse(st[0..(st.Length / 2)]), currentStone.Value - 1));
                    break;
                default:
                    stack.Push(new KeyValuePair<long, int>(2024 * currentStone.Key, currentStone.Value - 1));
                    break;

            }
        }
        return result;
    }
}
