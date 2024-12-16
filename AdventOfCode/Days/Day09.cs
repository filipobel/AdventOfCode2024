namespace AdventOfCode.Days;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        var (i, j) = (blocks.First, blocks.Last);
        while (i != j)
        {
            if (i.Value.fileId != -1)
            {
                i = i.Next;
            }
            else if (j.Value.fileId == -1)
            {
                j = j.Previous;
            }
            else
            {
                RelocateBlocks(i,j,true);
                j = j.Previous;
                
            }
        }
        return new(GetBlocksValue().ToString());
    }


    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private long GetBlocksValue()
    {
        var res = 0L;
        var location = 0;
        for (var i = blocks.First; i != null; i = i.Next)
        {
            for (var k = 0; k < i.Value.length; k++)
            {
                if (i.Value.fileId != -1)
                {
                    res += location * i.Value.fileId;
                }
                location++;
            }
        }
        return res;
    }

    private void RelocateBlocks(LinkedListNode<Block> start, LinkedListNode<Block> toMove, bool allowFragment)
    {
        for (var i = start; i != toMove; i = i.Next)
        {
            if (i.Value.fileId != -1)
            {
            }
            else if (i.Value.length == toMove.Value.length)
            {
                (i.Value, toMove.Value) = (toMove.Value, i.Value);
                return;
            }
            else if (i.Value.length > toMove.Value.length)
            {
                //move blocks into space
                var difference = i.Value.length - toMove.Value.length;

                i.Value = toMove.Value;
                toMove.Value = toMove.Value with { fileId = -1 };
                blocks.AddAfter(i, new Block(-1, difference));
                return;
            }
            else if (allowFragment && i.Value.length < toMove.Value.length)
            {
                var difference = toMove.Value.length - i.Value.length;
                i.Value = i.Value with { fileId = toMove.Value.fileId };
                toMove.Value = toMove.Value with { length = difference };
                blocks.AddAfter(toMove, new Block(-1, i.Value.length));
            }
        }
    }
}