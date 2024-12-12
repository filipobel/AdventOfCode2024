using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
public static class TupleExtensions
{
    public static (int,int) AddTuple(this (int,int) firstTuple, (int,int) secondTuple)
    {
        return (firstTuple.Item1 + secondTuple.Item1, firstTuple.Item2 + secondTuple.Item2);
    }
}
