namespace AdventOfCode.Days;

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
record struct Vec2 (int x, int y);
record struct Robot(Vec2 pos, Vec2 vel);

public class Day14 : TestableBaseDay
{
    private IEnumerable<Robot> _robots;
    const int WIDTH = 101;
    const int HEIGHT = 103;

    public Day14()
    {
        _robots = from line in File.ReadAllText(InputFilePath).Split("\r\n")
                  let numbers = Regex.Matches(line, @"-?\d+").Select(m => int.Parse(m.Value)).ToArray()
                  select new Robot(new Vec2(numbers[0], numbers[1]), new Vec2(numbers[2], numbers[3]));

    }

    public override ValueTask<string> Solve_1()
    {
        //Console.WriteLine(WriteRobots(_robots));
        
        Simulate(100);

        var safetyScore = _robots.Select(r => GetQuadrant(r))
            .Where(q => q.x != 0 && q.y != 0)
            .GroupBy(quadrant => new { quadrant.x, quadrant.y })
            .Aggregate(1, (acc, group) => acc * group.Count());

        //Console.WriteLine(WriteRobots(_robots));

        return new(safetyScore.ToString());
    }

    private void Simulate(int count)
    {
        _robots = _robots.Select(r => Step(r, count));
    }

    private Vec2 GetQuadrant(Robot robot)
    {
        return new Vec2(Math.Sign(robot.pos.x- WIDTH/2), Math.Sign(robot.pos.y -HEIGHT/2));
    }

    private static Robot Step(Robot robot, int totalSteps) => robot with { pos = GetPosition(robot.pos, robot.vel, totalSteps) };

    private static Vec2 GetPosition(Vec2 pos, Vec2 vel, int totalSteps) =>
        new Vec2(Mod(pos.x + totalSteps * vel.x, WIDTH), Mod(pos.y + totalSteps * vel.y,HEIGHT));

    string WriteRobots(IEnumerable<Robot> robots)
    {
        var res = new int[HEIGHT, WIDTH];
        foreach (var robot in robots)
        {
            try
            {
                res[robot.pos.y, robot.pos.x]++;
            }
            catch (Exception e)
            { 
                var test = robot; 
            }
        }
        var sb = new StringBuilder();
        for (var y = 0; y < HEIGHT; y++)
        {
            for (var x = 0; x < WIDTH; x++)
            {
                sb.Append(res[y, x].ToString());
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    // % operator doesnt deal well with looping and wrapped numbers

    public static int Mod(int value, int divisor)
    {
        int r = value % divisor;
        return r < 0 ? r + divisor : r;
    }
}
