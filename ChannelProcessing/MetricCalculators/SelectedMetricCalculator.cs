using ChannelProcessing.MetricCalculators;

namespace ChannelProcessing.ChannelCalculators
{
    public interface ISelectedMetricCalculator
    {
        /// <summary>
        /// Calculates the metric value for the provided parameters, inputs and selected metric type
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <param name="metricType">The desired metric type for the output calculated value</param>
        /// <returns>The calculated decimal value, in the form of the selected metric type</returns>
        decimal CalculateMetric(Parameters parameters, decimal[] inputs, MetricType metricType);
    }

    public class SelectedMetricCalculator : ISelectedMetricCalculator
    {
        private readonly IEnumerable<IMetricCalculator> _metricCalculators;

        public SelectedMetricCalculator(IEnumerable<IMetricCalculator> metricCalculators)
        {
            _metricCalculators = metricCalculators;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">Thrown when inputs is empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the selected metric type is not found amongst the available calculators.</exception>
        public decimal CalculateMetric(Parameters parameters, decimal[] inputs, MetricType metricType)
        {
            if (inputs.Length == 0)
            {
                throw new ArgumentException("Input channel data needs to have at least one value.");
            }

            var metricCalculator = _metricCalculators.FirstOrDefault(c => c.MetricType == metricType);
            if (metricCalculator == null)
            {
                throw new ArgumentException($"Invalid {nameof(MetricType)}: {metricType}");
            }

            return metricCalculator.CalculateMetric(parameters, inputs);
        }
    }
}