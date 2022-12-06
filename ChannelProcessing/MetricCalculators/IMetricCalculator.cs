namespace ChannelProcessing.MetricCalculators
{
    public interface IMetricCalculator
    {
        MetricType MetricType { get; }

        decimal CalculateMetric(Parameters parameters, decimal[] inputs);
    }
}