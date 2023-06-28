using Xunit;
using SquareEquationLib;

namespace SquareEquationLib.Tests;

public class SquareEquationLib_isSolutionRight
{
    [Fact]
    public void Solve_ReturnsTwoRoots()
    {
        double[] expected = new double[] { 1.25, -1 };
        double[] actual = SquareEquation.Solve(4, -1, -5);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Solve_ReturnsOneRoots()
    {
        double[] expected = new double[] { 4 };
        double[] actual = SquareEquation.Solve(1, -8, 16);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Solve_ReturnsEmpty()
    {
        double[] expected = Array.Empty<double>();
        double[] actual = SquareEquation.Solve(3, 1, 2);

        Assert.Equal(expected, actual);
    }



    [Fact]
    public void Solve_ThrowsArgumentException()
    {
        Assert.Throws<System.ArgumentException>(() => SquareEquation.Solve(0, 1.34, 5));
    }

    [Theory]
    [InlineData(double.NaN, 5, 11)]
    [InlineData(1, double.PositiveInfinity, 5)]
    [InlineData(1.4, 25, double.NegativeInfinity)]
    public void Solve_InvalidCoefficients(double a, double b, double c)
    {
        Assert.Throws<System.ArgumentException>(() => SquareEquation.Solve(a, b, c));
    }
}