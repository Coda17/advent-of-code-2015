using AoC2015.Extensions;

namespace AoC2015.Solvers;

public sealed class Day09 : BaseProblem<int>
{
    private readonly HashSet<string> _vertices = new();
    private readonly Dictionary<(string, string), int> _edges = new();
    private readonly IEnumerable<string[]> _paths;

    public Day09()
    {
        foreach (var l in File.ReadLines(InputFilePath))
        {
            var split1 = l.Split(" = ");
            var split2 = split1[0].Split(" to ");
            _vertices.Add(split2[0]);
            _vertices.Add(split2[1]);
            _edges[(split2[0], split2[1])] = int.Parse(split1[1]);
            _edges[(split2[1], split2[0])] = int.Parse(split1[1]);
        }

        //FloydWarshall(_vertices, _edges);

        _paths = _vertices.ToArray().Permutations();
    }

    static void FloydWarshall(HashSet<string> vertices, IDictionary<(string, string), int> edges)
    {
        foreach (var u in vertices)
        {
            foreach (var v in vertices.Where(v => !edges.ContainsKey((u, v))))
            {
                edges[(u, v)] = int.MaxValue;
            }
        }

        foreach (var v in vertices)
        {
            edges[(v, v)] = 0;
        }

        foreach (var k in vertices)
        {
            foreach (var i in vertices)
            {
                foreach (var j in vertices.Where(j => edges[(i, k)] != int.MaxValue && edges[(k, j)] != int.MaxValue))
                {
                    edges[(i, j)] = Math.Min(edges[(i, j)],
                        edges[(i, k)] + edges[(k, j)]);
                }
            }
        }
    }

    protected override int Solve1()
    {
        var min = int.MaxValue;
        foreach (var path in _paths)
        {
            var distance = 0;
            for (var c = 0; c < path.Length - 1; ++c)
            {
                distance += _edges[(path[c], path[c + 1])];
            }

            min = Math.Min(min, distance);
        }

        return min;
    }

    protected override int Solve2()
    {
        var max = int.MinValue;
        foreach (var path in _paths)
        {
            var distance = 0;
            for (var c = 0; c < path.Length - 1; ++c)
            {
                distance += _edges[(path[c], path[c + 1])];
            }

            max = Math.Max(max, distance);
        }

        return max;
    }
}