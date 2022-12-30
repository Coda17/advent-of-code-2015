namespace AoC2015;

public abstract class BaseProblem : AoCHelper.BaseProblem
{
    protected override string ClassPrefix => "Day";
    protected override string InputFileExtension => ".in";

    protected abstract string Solve1();
    protected abstract string Solve2();

    public override ValueTask<string> Solve_1() => new(Solve1());
    public override ValueTask<string> Solve_2() => new(Solve2());
}