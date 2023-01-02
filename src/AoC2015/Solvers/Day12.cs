using System.Text.Json;

namespace AoC2015.Solvers;

public sealed class Day12 : BaseProblem<int>
{
    private readonly byte[] _input;

    public Day12()
    {
        _input = File.ReadAllBytes(InputFilePath);
    }

    protected override int Solve1()
    {
        var sum = 0;
        var reader = new Utf8JsonReader(_input);
        while (reader.Read())
        {
            sum += reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetInt32(),
                _ => 0
            };
        }

        return sum;
    }

    protected override int Solve2() => SumJsonElement(JsonDocument.Parse(_input).RootElement);

    private static int SumJsonElement(JsonElement element)
    {
        if (element.ValueKind is JsonValueKind.Number)
        {
            return element.GetInt32();
        }

        if (element.ValueKind is JsonValueKind.Array)
        {
            return element.EnumerateArray().Sum(SumJsonElement);
        }

        if (element.ValueKind is not JsonValueKind.Object)
        {
            return 0;
        }

        var sum = 0;
        foreach (var property in element.EnumerateObject())
        {
            if (property.Value.ValueKind is JsonValueKind.String && property.Value.GetString() == "red")
            {
                return 0;
            }

            if (property.Value.ValueKind is JsonValueKind.Number)
            {
                sum += property.Value.GetInt32();
            }
            else if (property.Value.ValueKind is JsonValueKind.Object or JsonValueKind.Array)
            {
                sum += SumJsonElement(property.Value);
            }
        }

        return sum;
    }
}