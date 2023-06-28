using Newtonsoft.Json.Linq;
using SquareEquationLib;
using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace SpecFlowTestingProject.StepDefinitions
{
    [Binding]
    public class SolveStepDefinitions
    {
        private double[] array = new double[3];
        private double[]? result;
        Exception? exception;

        [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), (.*)\)")]
        public void GivenКвадратноеУравнениеСКоэффициентами(string p0, string p1, string p2)
        {
            double _p0 = double.Parse(p0, System.Globalization.CultureInfo.InvariantCulture);
            double _p1 = double.Parse(p1, System.Globalization.CultureInfo.InvariantCulture);
            double _p2 = double.Parse(p2, System.Globalization.CultureInfo.InvariantCulture);

            array = new double[] { _p0, _p1, _p2 };
        }

        [When(@"вычисляются корни квадратного уравнения")]
        public void WhenВычисляютсяКорниКвадратногоУравнения()
        {
            try
            {
                result = SquareEquation.Solve(array[0], array[1], array[2]);
            }
            catch (ArgumentException ex)
            {
                exception = ex;
            }
        }

        [Then (@"квадратное уравнение имеет два корня \((.*), (.*)\) кратности один")]
        public void ThenHave2Solutions(double p0, double p1)
        {
            result.Should().Equal(new double[] { p0, p1 });
        }

        [Then(@"квадратное уравнение имеет один корень (.*) кратности два")]
        public void ThenHave1Solutions(double p0)
        {
            result.Should().Equal(new double[] { p0 });
        }

        [Then(@"множество корней квадратного уравнения пустое")]
        public void ThenHaveNoSolutions()
        {
            result.Should().Equal(Array.Empty<double>());
        }

        [Then(@"выбрасывается исключение ArgumentException")]
        public void ThenThrowArgEx()
        {
            Assert.Equal("Invalid Values", exception.Message);
        }
    }
}
