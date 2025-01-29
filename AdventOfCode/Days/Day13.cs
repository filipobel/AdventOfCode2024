using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Days;
public class Day13 : TestableBaseDay
{
    List<ClawMachine> clawMachines;
    public Day13()
    {
        var intialInput = File.ReadAllText(InputFilePath);
        clawMachines = intialInput.Split("\r\n\r\n").Select(n => new ClawMachine(n)).ToList();

    }
    public override ValueTask<string> Solve_1()
    {
        return new (clawMachines.Sum(x => x.Solve(0)).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(clawMachines.Sum(x => x.Solve(10000000000000)).ToString());
    }
}

public class ClawMachine
{
    private (int, int) ButtonA;
    private (int, int) ButtonB;
    private (int, int) Prize;

    public ClawMachine(String input)
    {
        var inputLines = input.Split("\r\n");
        ButtonA = Regex.Matches(inputLines[0], @"\d+") switch { var a => (int.Parse(a[0].Value), int.Parse(a[1].Value))};
        ButtonB = Regex.Matches(inputLines[1], @"\d+") switch { var a => (int.Parse(a[0].Value), int.Parse(a[1].Value)) };
        Prize = Regex.Matches(inputLines[2], @"\d+") switch { var a => (int.Parse(a[0].Value), int.Parse(a[1].Value)) };
    }

    public long Solve(long shift)
    {
        (long,long) tempPrize = (Prize.Item1 + shift, Prize.Item2 + shift);

        if ((ButtonA.Item1 * tempPrize.Item2 - tempPrize.Item1 * ButtonA.Item2) % (ButtonA.Item1 * ButtonB.Item2 - ButtonA.Item2 * ButtonB.Item1) != 0) {
            return 0;
        }
        long bPress = (ButtonA.Item1 * tempPrize.Item2 - tempPrize.Item1 * ButtonA.Item2) / (ButtonA.Item1 * ButtonB.Item2 - ButtonB.Item1 * ButtonA.Item2);

        if ((tempPrize.Item1 - bPress * ButtonB.Item1) % ButtonA.Item1 != 0) { return 0; }
        long a = (tempPrize.Item1 - bPress * ButtonB.Item1) / ButtonA.Item1;

        return 3*a+ bPress;
    }
}
