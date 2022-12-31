using AoC2015.Solvers;
using AoCHelper;

namespace AoC2015.Test;

public class SolutionTests
{
    [Theory]
    [InlineData(typeof(Day01), "74", "1795")]
    [InlineData(typeof(Day02), "1606483", "3842356")]
    [InlineData(typeof(Day03), "2565", "2639")]
    [InlineData(typeof(Day04), "282749", "9962624")]
    [InlineData(typeof(Day05), "255", "55")]
    [InlineData(typeof(Day06), "543903", "14687245")]
    [InlineData(typeof(Day07), "46065", "14134")]
    [InlineData(typeof(Day08), "1371", "2117")]
    public async Task Test(Type type, string sol1, string sol2)
    {
        if (Activator.CreateInstance(type) is BaseProblem instance)
        {
            sol1.Should().Be(await instance.Solve_1());
            sol2.Should().Be(await instance.Solve_2());
        }
        else
        {
            Assert.Fail($"{type} is not a {nameof(BaseProblem)}");
        }
    }
}