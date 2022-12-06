using ChannelProcessing.ChannelCalculators;

namespace ChannelProcessing.MetricCalculators
{
    public interface IMetricBCalculator : IMetricCalculator
    {
    }

    public class MetricBCalculator : IMetricBCalculator
    {
        private readonly IChannelBCalculator _bCalculator;

        public MetricBCalculator(IChannelBCalculator bCalculator)
        {
            _bCalculator = bCalculator;
        }

        public MetricType MetricType => MetricType.b;

        /// <summary>
        /// Calculates the b metric value for the provided parameters and inputs
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <returns>The calculated decimal value for the metric b</returns>
        public decimal CalculateMetric(Parameters parameters, decimal[] inputs)
        {
            var bValues = _bCalculator.CalculateChannels(parameters, inputs);
            var result = bValues.Average();
            return result;
        }
    }
}