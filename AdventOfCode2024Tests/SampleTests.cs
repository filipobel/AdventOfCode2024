using AdventOfCode.Days;
using AoCHelper;

namespace AdventOfCode2024Tests
{
    public class SampleTests
    {
        [TestCase(typeof(Day01), "11", "31")]
        public async Task Test(Type type, string sol1, string sol2)
        {
            // Can't use BaseDay since some of them aren't days, but you probably can
            if (Activator.CreateInstance(type) is BaseProblem instance)
            {
                Assert.AreEqual(sol1, await instance.Solve_1());
                Assert.AreEqual(sol2, await instance.Solve_2());
            }
            else
            {
                Assert.Fail($"{type} is not a BaseProblem");
            }
        }
    }
}