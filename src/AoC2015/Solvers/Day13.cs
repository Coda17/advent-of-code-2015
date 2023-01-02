using AoC2015.Extensions;

namespace AoC2015.Solvers;

public sealed class Day13 : BaseProblem<int>
{
    private readonly HashSet<string> _vertices = new();
    private readonly IDictionary<(string, string), int> _edges = new Dictionary<(string, string), int>();

    public Day13()
    {
        foreach (var line in File.ReadLines(InputFilePath)
                     .Select(x => x.Split(' '))
                     .Select(x => (left: x[0], gain: x[2] == "gain", happiness: int.Parse(x[3]), right: x[10][..^1])))
        {
            _ = _vertices.Add(line.left);
            _ = _vertices.Add(line.right);

            var happiness = line.gain ? line.happiness : -1 * line.happiness;
            _ = _edges.TryAdd((line.left, line.right), happiness);
        }
    }

    protected override int Solve1() => Max(_vertices, _edges);

    protected override int Solve2() => Max(_vertices.Append("Yourself").ToArray(), _edges);

    private static int Max(IEnumerable<string> vertices, IDictionary<(string, string), int> edges)
    {
        var max = int.MinValue;
        var orders = vertices.ToArray().Permutations();
        foreach (var o in orders)
        {
            var sum = 0;

            // First person
            sum += edges.TryGetValue((o[0], o[^1]), out var happiness) ? happiness : 0;
            sum += edges.TryGetValue((o[0], o[1]), out happiness) ? happiness : 0;

            // Last person
            sum += edges.TryGetValue((o[^1], o[0]), out happiness) ? happiness : 0;
            sum += edges.TryGetValue((o[^1], o[^2]), out happiness) ? happiness : 0;

            // Other people
            for (var i = 1; i < o.Length - 1; ++i)
            {
                sum += edges.TryGetValue((o[i], o[i - 1]), out happiness) ? happiness : 0;
                sum += edges.TryGetValue((o[i], o[i + 1]), out happiness) ? happiness : 0;
            }

            max = Math.Max(max, sum);
        }

        return max;
    }
}