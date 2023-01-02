namespace AoC2015.Solvers;

public sealed class Day14 : BaseProblem<int>
{
    private readonly Deer[] _input;

    public Day14()
    {
        _input = File.ReadLines(InputFilePath)
            .Select(x => x.Split(' '))
            .Select(x => new Deer(x[0], int.Parse(x[3]), int.Parse(x[6]), int.Parse(x[^2])))
            .ToArray();
    }

    protected override int Solve1() => _input.Max(d => Race(d, 2503));

    private static int Race(Deer deer, int seconds) =>
        seconds / (deer.Duration + deer.Rest) * deer.Speed * deer.Duration +
        deer.Speed * int.Min(deer.Duration, seconds % (deer.Duration + deer.Rest));

    protected override int Solve2() => Max(_input.Select(x => new DeerState(x)).ToArray(), 2503);

    private static int Max(DeerState[] deer, int seconds)
    {
        for (var t = 0; t < seconds; ++t)
        {
            foreach (var d in deer)
            {
                d.Run();
            }

            deer = deer.OrderByDescending(x => x.Distance).ToArray();
            var current = deer[0].Distance;
            foreach (var d in deer)
            {
                if (d.Distance == current)
                {
                    d.Points++;
                }
            }
        }

        return deer.MaxBy(x => x.Points)!.Points;
    }
    
    public record Deer(string Name, int Speed, int Duration, int Rest);

    public class DeerState
    {
        public Deer Deer { get; }
        public int Time { get; private set; }
        public bool IsResting { get; private set; }
        public int Distance { get; private set; }
        public int Points { get; set; }

        public DeerState(Deer deer)
        {
            Deer = deer;
        }

        public void Run()
        {
            if (Time % (Deer.Duration + Deer.Rest) < Deer.Duration)
            {
                Distance += Deer.Speed;
            }

            ++Time;
            if (Time % (Deer.Duration + Deer.Rest) >= Deer.Duration)
            {
                IsResting = true;
            }
            else
            {
                IsResting = false;
            }
        }
    }
}