using System;

public static class MathExtensions
{
    /// <summary>
    ///     Box-Muller Transform fast Gaussian distribution
    ///     Implemented by Jarrett Meyer
    /// </summary>
    /// <param name="mean"></param>
    /// <param name="stdDev"></param>
    /// <returns></returns>
    public static int NextGaussian(this Random rand, int mean, float stdDev)
    {
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal;

        float randFloat = (float) randNormal;

        return (int) Math.Round(randFloat);
    }
}
