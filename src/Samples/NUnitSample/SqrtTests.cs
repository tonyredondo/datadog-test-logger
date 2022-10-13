namespace NUnitSample;

public class SqrtTests
{
    [DatapointSource]
    public double[] values = new double[] { 0.0, 1.0, -1.0, 42.0 };

    [Theory]
    public void SquareRootDefinition(double num)
    {
        Assume.That(num >= 0.0);

        double sqrt = Math.Sqrt(num);

        Assert.That(sqrt >= 0.0);
        Assert.That(sqrt * sqrt, Is.EqualTo(num).Within(0.000001));
    }
}