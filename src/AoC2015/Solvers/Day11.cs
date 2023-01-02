namespace AoC2015.Solvers;

public sealed class Day11 : BaseProblem<string>
{
    private readonly string _input;
    private string _part1Answer = null!;

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    protected override string Solve1()
    {
        _part1Answer = GeneratePassword(_input);
        return _part1Answer;
    }

    protected override string Solve2() => GeneratePassword(_part1Answer);

    // Won't work for all inputs, I took shortcuts
    private static string GeneratePassword(string password)
    {
        var p = password.ToCharArray().Select(x => (int)x).ToArray();
        while (true)
        {
            // Increment and carry
            for (var i = p.Length - 1; i >= 0; --i)
            {
                p[i] = (char)(p[i] + 1);
                if (p[i] <= 'z')
                {
                    break;
                }

                for (var j = i; j < p.Length; ++j)
                {
                    p[j] = 'a';
                }
            }

            // Remove all invalid letters
            for (var i = 0; i < p.Length; ++i)
            {
                if (p[i] is not ('i' or 'o' or 'l'))
                {
                    continue;
                }

                ++p[i];
                for (var j = i + 1; j < p.Length; ++j)
                {
                    p[j] = 'a';
                }
            }

            if (p[^5] + 2 > 'z' || p[^3] + 1 > 'z')
            {
                ++p[^6];
                p[^5] = 'a';
            }

            p[^4] = p[^5];
            p[^3] = (char)(p[^5] + 1);
            p[^2] = (char)(p[^5] + 2);
            p[^1] = p[^2];

            var invalidLetters = false;
            var increasingLetters = false;
            var pairs = new HashSet<int>();
            for (var i = p.Length - 1; i >= 0; --i)
            {
                if (p[i] is 'i' or 'o' or 'l')
                {
                    invalidLetters = true;
                    break;
                }

                if (i + 1 < p.Length && p[i] == p[i + 1])
                {
                    pairs.Add(p[i]);
                }

                if (i + 2 < p.Length && p[i] + 1 == p[i + 1] && p[i] + 2 == p[i + 2])
                {
                    increasingLetters = true;
                }
            }

            if (!invalidLetters && increasingLetters && pairs.Count > 1)
            {
                return new string(p.Select(x => (char)x).ToArray());
            }
        }
    }
}