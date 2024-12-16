namespace AdventOfCode.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

record struct Block(int fileId, int length) { }

public class Day09 : TestableBaseDay
{
    private LinkedList<Block> blocks;
    public Day09()
    {
        var input = File.ReadAllText(InputFilePath);
        blocks = new LinkedList<Block>(input.Select((ch, i) => new Block(i % 2 == 1 ? -1 : i / 2, ch - '0')));
    }
    public override ValueTask<string> Solve_1()
    {
        OutputLL();
        throw new NotImplementedException();
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    public void OutputLL()
    {
        foreach (var item in blocks)
        {
            if(item.fileId == -1)
            {
                Console.Write(new string('.', item.length));
            }
            else
            {
                Console.Write(new string(char.Parse(item.fileId.ToString()), item.length));
            }
        }
    }
}
