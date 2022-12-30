using AoC2015.Solvers;

namespace AoC2015.Test;

public class SolutionTests
{
    [Theory]
    [InlineData(typeof(Day01), "74", "1795")]
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