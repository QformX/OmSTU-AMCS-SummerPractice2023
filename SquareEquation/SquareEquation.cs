namespace SquareEquationLib;

public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        double eps = 1e-6;
        if (Math.Abs(a) < eps || (new double[] { a, b, c }).Any(double.IsNaN) || (new double[] { a, b, c }).Any(double.IsInfinity))
        {
            throw new ArgumentException("Invalid Values");
        }

        b = b / a;
        c = c / a;
        double x1 = 0;
        double x2 = 0;

        double d = Math.Pow(b, 2.0) - (4 * c);


        if (d <= -eps) { return Array.Empty<double>(); }

        if (Math.Abs(d) < eps)
        {
            double x = -b / 2;
            return new double[] { x };
        }

        if (b < eps)
        {
            x1 = -((b + Math.Sqrt(d)) / 2);
        }
        else
        {
            x1 = -((b + Math.Sign(b) * Math.Sqrt(d)) / 2);
        }

        x2 = c / x1;
        return new double[] { x2, x1 };
    }
}
