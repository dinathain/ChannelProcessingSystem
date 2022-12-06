using ChannelProcessing.MetricCalculators;

namespace ChannelProcessing.ChannelCalculators
{
    public interface IChannelCCalculator : IChannelCalculator
    {
    }

    public class ChannelCCalculator : IChannelCCalculator
    {
        private readonly IMetricBCalculator _averageBCalculator;

        public ChannelCCalculator(IMetricBCalculator averageBCalculator)
        {
            _averageBCalculator = averageBCalculator;
        }

        public ChannelType ChannelType => ChannelType.C;

        /// <summary>
        /// Calculates the C channel value for the provided parameters and inputs
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <returns>An array of decimals containing the calculated channel values for C</returns>
        public decimal[] CalculateChannels(Parameters parameters, decimal[] inputs)
        {
            var averageB = _averageBCalculator.CalculateMetric(parameters, inputs);
            if (averageB == 0)
            {
                return inputs;
            }

            decimal[] result = new decimal[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                var output = inputs[i] + averageB;
                result[i] = output;
            }
            return result;
        }
    }
}