namespace AoC2015.Solvers;

public sealed class Day05 : BaseProblem<int>
{
    private readonly string[] _input;

    public Day05()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    protected override int Solve1() => _input.Sum(s => IsNice1(s) ? 1 : 0);

    protected override int Solve2() => _input.Sum(s => IsNice2(s) ? 1 : 0);

    private static bool IsNice1(string s)
    {
        char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        string[] naughtSubstrings = { "ab", "cd", "pq", "xy" };

        var vowelCount = 0;
        var doubleLetter = false;
        for (var i = 0; i < s.Length; ++i)
        {
            vowelCount += vowels.Contains(s[i]) ? 1 : 0;
            if (i >= s.Length - 1)
            {
                continue;
            }

            if (naughtSubstrings.Contains(s.Substring(i, 2)))
            {
                return false;
            }

            if (s[i] == s[i + 1])
            {
                doubleLetter = true;
            }
        }

        return doubleLetter && vowelCount > 2;
    }

    private static bool IsNice2(string s)
    {
        var hasPair = false;
        var repeatLetter = false;

        for (var i = 0; i < s.Length; ++i)
        {
            if (i >= s.Length - 1)
            {
                continue;
            }

            if (i < s.Length - 3)
            {
                var pair = s[i..(i + 2)];
                if (s.LastIndexOf(pair, StringComparison.Ordinal) > i + 1)
                {
                    hasPair = true;
                }
            }

            if (i < s.Length - 2 && s[i] == s[i + 2])
            {
                repeatLetter = true;
            }
        }

        return repeatLetter && hasPair;
    }
}