using System.Text.RegularExpressions;
using SheepTools.Model;

namespace AoC2015.Solvers;

public sealed partial class Day06 : BaseProblem<int>
{
    private readonly IEnumerable<Instruction> _input;

    public Day06()
    {
        var regex = InputRegex();
        _input = File.ReadAllLines(InputFilePath)
            .Select(l =>
            {
                var match = regex.Match(l);
                return new Instruction(match.Groups["d"].Value switch
                    {
                        "toggle" => Direction.Toggle,
                        "turn on" => Direction.On,
                        _ => Direction.Off
                    },
                    new IntPoint(int.Parse(match.Groups["x1"].Value), int.Parse(match.Groups["y1"].Value)),
                    new IntPoint(int.Parse(match.Groups["x2"].Value), int.Parse(match.Groups["y2"].Value)));
            });
    }

    protected override int Solve1()
    {
        var lights = new bool[1000][];

        foreach (var i in _input)
        {
            for (var y = i.P1.Y; y <= i.P2.Y; ++y)
            {
                for (var x = i.P1.X; x <= i.P2.X; ++x)
                {
                    // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
                    lights[y] ??= Enumerable.Range(0, 1000).Select(_ => false).ToArray();

                    lights[y][x] = i.D switch
                    {
                        Direction.Off => false,
                        Direction.On => true,
                        Direction.Toggle => !lights[y][x],
                        _ => throw new SolveFailedException()
                    };
                }
            }
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return lights.Where(l => l is not null).Sum(y => y.Sum(x => x ? 1 : 0));
    }

    protected override int Solve2()
    {
        var lights = new int[1000][];

        foreach (var i in _input)
        {
            for (var y = i.P1.Y; y <= i.P2.Y; ++y)
            {
                for (var x = i.P1.X; x <= i.P2.X; ++x)
                {
                    // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
                    lights[y] ??= Enumerable.Range(0, 1000).Select(_ => 0).ToArray();

                    lights[y][x] = i.D switch
                    {
                        Direction.Off => lights[y][x] == 0 ? 0 : lights[y][x] - 1,
                        Direction.On => lights[y][x] + 1,
                        Direction.Toggle => lights[y][x] + 2,
                        _ => throw new SolveFailedException()
                    };
                }
            }
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return lights.Where(l => l is not null).Sum(y => y.Sum(x => x));
    }

    private record Instruction(Direction D, IntPoint P1, IntPoint P2);

    private enum Direction
    {
        Off,
        On,
        Toggle
    }

    [GeneratedRegex("(?'d'toggle|turn on|turn off) (?'x1'\\d+),(?'y1'\\d+) through (?'x2'\\d+),(?'y2'\\d+)")]
    private static partial Regex InputRegex();
}