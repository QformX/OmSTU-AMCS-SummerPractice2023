namespace SquareEquationLib;

public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        if (Math.Abs(a) < double.Epsilon || (new double[] {a, b, c}).Any(double.IsNaN) || (new double[] { a, b, c }).Any(double.IsInfinity))
        {
            throw new ArgumentException();
        }

        double d = Math.Pow(b, 2.0) - (4 * a * c);

        if (d < 0) { return Array.Empty<double>(); }
        
        if (Math.Abs(d) < double.Epsilon)
        {
            double x = -b / (2 * a);
            return new double[] { x };
        }

        double x1 = -((b + Math.Sign(b) * Math.Sqrt(d)) / (2*a));
        double x2 = c / (x1 * a);
        return new double[] { x1, x2 };
    }
}
