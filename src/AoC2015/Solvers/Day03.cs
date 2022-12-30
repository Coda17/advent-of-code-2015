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
        var santa = new Point2D();
        var visited = new Dictionary<Point2D, int>
        {
            { santa, 1 }
        };

        foreach (var c in _input)
        {
            santa = c switch
            {
                '^' => santa with { Y = santa.Y + 1 },
                'v' => santa with { Y = santa.Y - 1 },
                '>' => santa with { X = santa.X + 1 },
                '<' => santa with { X = santa.X - 1 },
                _ => santa
            };

            visited[santa] = visited.ContainsKey(santa) ? visited[santa] + 1 : 1;
        }

        return visited.Count;
    }

    protected override int Solve2()
    {
        var santa = new Point2D();
        var roboSanta = new Point2D();
        var visited = new Dictionary<Point2D, int>
        {
            { santa, 1 }
        };

        for (var i = 0; i < _input.Length; ++i)
        {
            var current = i % 2 == 0 ? santa : roboSanta;
            current = _input[i] switch
            {
                '^' => current with { Y = current.Y + 1 },
                'v' => current with { Y = current.Y - 1 },
                '>' => current with { X = current.X + 1 },
                '<' => current with { X = current.X - 1 },
                _ => current
            };

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

    internal record Point2D(int X = 0, int Y = 0);
}