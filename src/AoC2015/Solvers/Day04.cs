using System.Security.Cryptography;

namespace AoC2015.Solvers;

public sealed class Day04 : BaseProblem<int>, IDisposable
{
    private readonly string _input;
    private readonly MD5 _md5 = MD5.Create();

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    protected override int Solve1() => Md5HashStartsWith("00000");

    protected override int Solve2() => Md5HashStartsWith("000000");

    private int Md5HashStartsWith(string s)
    {
        var i = 0;
        do
        {
            var hash = Convert.ToHexString(_md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes($"{_input}{i}")));
            if (hash.StartsWith(s))
            {
                return i;
            }

            ++i;
        } while (true);
    }

    public void Dispose() => _md5.Dispose();
}