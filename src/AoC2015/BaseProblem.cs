namespace AoC2015;

public abstract class BaseProblem<TU> : BaseProblem<TU, TU>
    where TU : notnull
{
}

public abstract class BaseProblem<TU, TV> : AoCHelper.BaseProblem
    where TU : notnull
    where TV : notnull
{
    protected override string ClassPrefix => "Day";
    protected override string InputFileExtension => ".in";

    protected abstract TU Solve1();
    protected abstract TV Solve2();

    public override ValueTask<string> Solve_1() => new(Solve1().ToString()!);
    public override ValueTask<string> Solve_2() => new(Solve2().ToString()!);
}