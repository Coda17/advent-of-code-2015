namespace AoC2015.Solvers;

public sealed class Day02 : BaseProblem<int>
{
    private readonly string[] _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    protected override int Solve1() => _input
        .Select(l => l.Split('x').Select(int.Parse).ToArray())
        .Select(d => new[] { 2 * d[0] * d[1], 2 * d[1] * d[2], 2 * d[2] * d[0] })
        .Select(a => a[0] + a[1] + a[2] + a.Min() / 2)
        .Sum();

    protected override int Solve2() => _input
        .Select(l => l.Split('x').Select(int.Parse).OrderBy(s => s).ToArray())
        .Select(d => d[0] + d[0] + d[1] + d[1] + d[0] * d[1] * d[2])
        .Sum();
}