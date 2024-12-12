using AdventOfCode.Days;
using AoCHelper;

namespace AdventOfCode2024Tests;

public class SampleTests
{
    [TestCase(typeof(Day01), "11", "31")]
    [TestCase(typeof(Day02), "2","4")]
    [TestCase(typeof(Day03), "161", "48")]
    [TestCase(typeof(Day04), "18", "9")]
    [TestCase(typeof(Day06),"41", "6")]
    [TestCase(typeof(Day07), "3749", "11387")]
    public async Task Test(Type type, string sol1, string sol2)
    {
        // Can't use BaseDay since some of them aren't days, but you probably can
        if (Activator.CreateInstance(type) is BaseProblem instance)
        {
            Assert.That(await instance.Solve_1(), Is.EqualTo(sol1));
            Assert.That(await instance.Solve_2(), Is.EqualTo(sol2));
        }
        else
        {
            Assert.Fail($"{type} is not a BaseProblem");
        }
    }
}
