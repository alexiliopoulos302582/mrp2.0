namespace Mrp2.Models
   
{
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Double;
    using MathNet.Numerics.LinearRegression;
    using MathNet.Numerics.Statistics;

    public class Regression
    {
        public double[] Months { get; set; }
        public double[] Demands { get; set; }
        public double Slope { get; private set; }
        public double R2 { get; private set; }
        public double AdjustedR2 { get; private set; }
        public double StandardError { get; private set; } = 0;
        public double Intercept { get; private set; }
        public double ForecastedDemand { get; private set; }

        public void PerformRegression()
        {
            // Perform linear regression
            var p = SimpleRegression.Fit(Months, Demands);
            Slope = p.Item2;
            Intercept = p.Item1;
            var yHat = Months.Select(x => Slope * x + Intercept).ToArray();
            var residuals = Demands.Zip(yHat, (y, yH) => y - yH).ToArray();
            var ssRes = residuals.Select(r => r * r).Sum();
            var ssTot = Demands.Select(y => Math.Pow(y - Demands.Average(), 2)).Sum();
            R2 = 1 - ssRes / ssTot;

            var n = Demands.Length;
            var k = 1; // number of predictors
            AdjustedR2 = 1 - (1 - R2) * (n - 1) / (n - k - 1);

            var meanSquareError = ssRes / (n - 2);
            StandardError = Math.Sqrt(meanSquareError);
        }

        public double Forecast(double futureMonth)
        {
            // Forecast future demand
            return ForecastedDemand = Slope * futureMonth + Intercept;
        }


    }
}
