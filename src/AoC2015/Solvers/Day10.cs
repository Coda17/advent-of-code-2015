using System.Text;

namespace AoC2015.Solvers;

public sealed class Day10 : BaseProblem<int>
{
    private readonly string _input;

    public Day10()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    protected override int Solve1() => LookAndSay(_input, 40).Length;

    protected override int Solve2() => LookAndSay(_input, 50).Length;

    private static string LookAndSay(string sequence, int n)
    {
        for (var i = 0; i < n; ++i)
        {
            var sb = new StringBuilder();
            var prev = sequence[0];
            var count = 0;

            foreach (var c in sequence)
            {
                if (c == prev)
                {
                    ++count;
                }
                else
                {
                    sb.Append($"{count}{prev}");
                    count = 1;
                }

                prev = c;
            }

            sb.Append($"{count}{prev}");

            sequence = sb.ToString();
        }

        return sequence;
    }
}