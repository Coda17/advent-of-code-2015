namespace AoC2015.Solvers;

public sealed class Day08 : BaseProblem<int>
{
    private readonly string[] _input;

    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    protected override int Solve1() => _input.Aggregate(0, (i, s) => i + (s.Length - CharactersInMemory(s)));

    protected override int Solve2() => _input.Aggregate(0, (i, s) => i + (EncodedCharacters(s) - s.Length));

    private static int CharactersInMemory(string s)
    {
        var inMemory = 0;
        for (var i = 1; i < s.Length - 1; ++i)
        {
            ++inMemory;
            var cs = s.Substring(i, 2);
            if (cs is "\\\\" or "\\\"")
            {
                i += 1;
            }
            else if (cs is "\\x")
            {
                i += 3;
            }
        }

        return inMemory;
    }

    private static int EncodedCharacters(string s)
    {
        var encoded = 6;
        for (var i = 1; i < s.Length - 1; ++i)
        {
            ++encoded;
            if (s[i] is '\\' or '"')
            {
                ++encoded;
            }
        }

        return encoded;
    }
}