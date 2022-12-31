using SheepTools.Model;

namespace AoC2015.Solvers;

public sealed class Day07 : BaseProblem<int>
{
    private readonly string[][] _input;

    public Day07()
    {
        _input = File.ReadAllLines(InputFilePath)
            .Select(l => l.Split(' ').ToArray())
            .ToArray();
    }

    protected override int Solve1()
    {
        var (gates, values) = ParseGates(_input);

        return GetValue(gates, values, gates["a"]);
    }

    protected override int Solve2()
    {
        var (gates, values) = ParseGates(_input);

        var a = GetValue(gates, values, gates["a"]);

        values = values.Where(x => int.TryParse(x.Key.Id, out _))
            .ToDictionary(x => x.Key, x => x.Value);

        values.Add(gates["b"], a);

        return GetValue(gates, values, gates["a"]);
    }

    private static (IDictionary<string, Gate>, IDictionary<Gate, int>) ParseGates(IEnumerable<string[]> input)
    {
        var gates = new Dictionary<string, Gate>();
        var values = new Dictionary<Gate, int>();

        foreach (var i in input)
        {
            switch (i.Length)
            {
                case 3:
                {
                    var lhs = new Gate(i[0]);
                    if (int.TryParse(i[0], out var constant))
                    {
                        _ = gates.TryAdd(lhs.Id, lhs);
                        values.Add(lhs, constant);
                    }

                    var gate = new Gate(Op.PassThrough, i[2], lhs);
                    gates.Add(gate.Id, gate);

                    break;
                }
                case 4:
                {
                    var lhs = new Gate(i[1]);
                    var gate = new Gate(Op.Not, i[3], lhs);
                    gates.Add(gate.Id, gate);

                    break;
                }
                case 5:
                {
                    var op = Enum.Parse<Op>(i[1], true);
                    var lhs = new Gate(i[0]);
                    var rhs = new Gate(i[2]);
                    if (op is Op.LShift or Op.RShift && int.TryParse(i[2], out var constant))
                    {
                        _ = gates.TryAdd(rhs.Id, rhs);
                        _ = values.TryAdd(rhs, constant);
                    }

                    var gate = new Gate(op, i[4], new[] { lhs, rhs });
                    gates.Add(gate.Id, gate);

                    break;
                }
            }
        }

        return (gates, values);
    }

    private static int GetValue(IDictionary<string, Gate> gates, IDictionary<Gate, int> values, Gate g)
    {
        if (values.ContainsKey(g))
        {
            return values[g];
        }

        var value = g.Op switch
        {
            Op.PassThrough => GetValue(gates, values, gates[g.Children.First().Id]),
            Op.Not => ~GetValue(gates, values, gates[g.Children.First().Id]) & 0xFFFF,
            Op.And => GetValue(gates, values, gates[g.Children.First().Id]) &
                      GetValue(gates, values, gates[g.Children.Last().Id]),
            Op.Or => GetValue(gates, values, gates[g.Children.First().Id]) |
                     GetValue(gates, values, gates[g.Children.Last().Id]),
            Op.LShift => (GetValue(gates, values, gates[g.Children.First().Id]) <<
                          GetValue(gates, values, gates[g.Children.Last().Id])) & 0xFFFF,
            Op.RShift => GetValue(gates, values, gates[g.Children.First().Id]) >>
                         GetValue(gates, values, gates[g.Children.Last().Id]),
            _ => throw new SolveFailedException()
        };

        values[g] = value;
        return value;
    }

    public record Gate : TreeNode<string>
    {
        public Op Op { get; }

        public Gate(string id) : base(id)
        {
        }

        public Gate(Op op, string id, Gate child) : base(id, child)
        {
            Op = op;
        }

        public Gate(Op op, string id, IEnumerable<Gate> children) : base(id, children)
        {
            Op = op;
        }
    }

    public enum Op
    {
        PassThrough,
        Not,
        And,
        Or,
        LShift,
        RShift
    }
}