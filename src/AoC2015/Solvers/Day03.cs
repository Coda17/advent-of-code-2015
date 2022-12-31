using SheepTools.Model;

namespace AoC2015.Solvers;

public sealed class Day03 : BaseProblem<int>
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    protected override int Solve1()
    {
        var santa = new IntPoint(0, 0);
        var visited = new Dictionary<IntPoint, int>
        {
            { santa, 1 }
        };

        foreach (var c in _input)
        {
            santa = santa.Move(c);
            visited[santa] = visited.ContainsKey(santa) ? visited[santa] + 1 : 1;
        }

        return visited.Count;
    }

    protected override int Solve2()
    {
        var santa = new IntPoint(0, 0);
        var roboSanta = new IntPoint(0, 0);
        var visited = new Dictionary<IntPoint, int>
        {
            { santa, 1 }
        };

        for (var i = 0; i < _input.Length; ++i)
        {
            var current = i % 2 == 0 ? santa : roboSanta;
            current = current.Move(_input[i]);

            visited[current] = visited.ContainsKey(current) ? visited[current] + 1 : 1;

            if (i % 2 == 0)
            {
                santa = current;
            }
            else
            {
                roboSanta = current;
            }
        }

        return visited.Count;
    }
}