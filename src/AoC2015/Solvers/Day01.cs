namespace AoC2015.Solvers;

public sealed class Day01 : BaseProblem<int>
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    protected override int Solve1() => _input.Aggregate(0, (i, c) => i + (c == '(' ? 1 : -1));

    protected override int Solve2()
    {
        var floor = 0;
        for (var i = 0; i < _input.Length; ++i)
        {
            floor += _input[i] == '(' ? 1 : -1;
            if (floor == -1)
            {
                return i + 1;
            }
        }

        throw new SolveFailedException();
    }
}