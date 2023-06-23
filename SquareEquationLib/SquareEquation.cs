namespace SquareEquationLib;

public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        double eps = 1e-9;
        if (Math.Abs(a) < eps || (new double[] { a, b, c }).Any(double.IsNaN) || (new double[] { a, b, c }).Any(double.IsInfinity))
        {
            throw new ArgumentException("Invalid Values");
        }

        b = b / a;
        c = c / a;

        double d = Math.Pow(b, 2.0) - (4 * c);


        if (d <= -eps) { return Array.Empty<double>(); }

        if (Math.Abs(d) < eps)
        {
            double x = -b / 2;
            return new double[] { x };
        }

        double x1 = -((b + Math.Sign(b) * Math.Sqrt(d)) / 2);
        double x2 = c / x1;
        return new double[] { x1, x2 };
    }
}
