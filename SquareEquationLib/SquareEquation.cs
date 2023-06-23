namespace SquareEquationLib;

public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        if (Math.Abs(a) < double.Epsilon)
        {
            throw new ArgumentException();
        }

        if (double.IsNaN(a) || double.IsNaN(b) || double.IsPositiveInfinity(a) || double.IsPositiveInfinity(b) || double.IsNegativeInfinity(a) || double.IsNegativeInfinity(b))
        {
            throw new ArgumentException();
        }

        double d = b * b - 4 * a * c;

        if (d < 0) { return new double[0]; }
        
        if (d == 0)
        {
            double x = -b / 2 * a;
            return new double[] { x, x };
        }

        double x1 = -(b + Math.Sign(b) * Math.Pow(d, 0.5) / 2 * a);
        double x2 = c / x1;
        return new double[] { x1, x2 };
    }
}
