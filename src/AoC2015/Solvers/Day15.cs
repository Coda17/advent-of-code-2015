namespace AoC2015.Solvers;

public sealed class Day15 : BaseProblem<long>
{
    private readonly Ingredient[] _input;

    public Day15()
    {
        _input = File.ReadLines(InputFilePath)
            .Select(x => x.Split(' '))
            .Select(x => new Ingredient(x[0][..^1], 
                int.Parse(x[2][..^1]), 
                int.Parse(x[4][..^1]), 
                int.Parse(x[6][..^1]),
                int.Parse(x[8][..^1]),
                int.Parse(x[10])))
            .ToArray();
    }

    protected override long Solve1() => MaxScore(100);

    protected override long Solve2() => MaxScore(100, 500);

    private long MaxScore(int teaspoons, int? calorieReq = null)
    {
        var max = long.MinValue;
        for (var h = 0; h < teaspoons; ++h)
        {
            for (var i = 0; i < teaspoons; ++i)
            {
                for (var j = 0; j < teaspoons; ++j)
                {
                    var k = teaspoons - h - i - j;

                    var capacity = Calculate(h, i, j, k, x => x.Capacity);
                    var durability = Calculate(h, i, j, k, x => x.Durability);
                    var flavor = Calculate(h, i, j, k, x => x.Flavor);
                    var texture = Calculate(h, i, j, k, x => x.Texture);
                    var calories = Calculate(h, i, j, k, x => x.Calories);

                    if (calorieReq.HasValue && calories != calorieReq.Value)
                    {
                        continue;
                    }

                    var score = capacity < 0 || durability < 0 || flavor < 0 || texture < 0
                        ? 0
                        : capacity * durability * flavor * texture;

                    max = long.Max(max, score);
                }
            }
        }

        return max;
    }

    private int Calculate(int h, int i, int j, int k, Func<Ingredient, int> property) =>
        h * property(_input[0])
        + i * property(_input[1])
        + j * property(_input[2])
        + k * property(_input[3]);

    public record Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);
}