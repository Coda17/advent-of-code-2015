namespace AoC2015.Solvers;

public sealed class Day16 : BaseProblem<int>
{
    private readonly string[][] _input;

    public Day16()
    {
        _input = File.ReadLines(InputFilePath)
            .Select(x => x[(x.IndexOf(':') + 2)..].Split(", ").ToArray())
            .ToArray();
    }

    protected override int Solve1()
    {
        var tickerTape = new[]
        {
            "children: 3",
            "cats: 7",
            "samoyeds: 2",
            "pomeranians: 3",
            "akitas: 0",
            "vizslas: 0",
            "goldfish: 5",
            "trees: 3",
            "cars: 2",
            "perfumes: 1"
        };

        for (var i = 0; i < _input.Length; ++i)
        {
            if (_input[i].All(x => tickerTape.Contains(x)))
            {
                return i + 1;
            }
        }

        throw new SolveFailedException();
    }

    protected override int Solve2()
    {
        var tickerTape = new Dictionary<string, int>
        {
            { "children", 3 },
            { "cats", 7 },
            { "samoyeds", 2 },
            { "pomeranians", 3 },
            { "akitas", 0 },
            { "vizslas", 0 },
            { "goldfish", 5 },
            { "trees", 3 },
            { "cars", 2 },
            { "perfumes", 1 }
        };

        for (var i = 0; i < _input.Length; ++i)
        {
            var isAuntSue = true;
            foreach (var prop in _input[i])
            {
                var split = prop.Split(": ");
                var (name, value) = (split[0], int.Parse(split[1]));
                if ((name is "cats" && tickerTape["cats"] < value) || (name is "trees" && tickerTape["trees"] < value))
                {
                    continue;
                }

                if ((name is "pomeranians" && tickerTape["pomeranians"] > value) ||
                    (name is "goldfish" && tickerTape["goldfish"] > value))
                {
                    continue;
                }

                if (tickerTape.TryGetValue(name, out var v) && v == value)
                {
                    continue;
                }

                isAuntSue = false;
                break;
            }

            if (isAuntSue)
            {
                return i + 1;
            }
        }

        throw new SolveFailedException();
    }
}