namespace ChannelProcessing.ChannelCalculators
{
    public interface ISelectedChannelCalculator
    {
        /// <summary>
        /// Calculates the channel value for the provided parameters, inputs and selected channel type
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <param name="channelType">The desired channel type of the output calculated values</param>
        /// <returns>An array of decimals containing the calculated channel values, converting the provided inputs to the channel value selected</returns>
        decimal[] CalculateChannels(Parameters parameters, decimal[] inputs, ChannelType channelType);
    }

    public class SelectedChannelCalculator : ISelectedChannelCalculator
    {
        private readonly IEnumerable<IChannelCalculator> _channelCalculators;

        public SelectedChannelCalculator(IEnumerable<IChannelCalculator> channelCalculators)
        {
            _channelCalculators = channelCalculators;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">Thrown when inputs is empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the selected channel type is not found amongst the available calculators.</exception>
        public decimal[] CalculateChannels(Parameters parameters, decimal[] inputs, ChannelType channelType)
        {
            if (inputs.Length == 0)
            {
                throw new ArgumentException("Input channel data needs to have at least one value.");
            }

            var channelCalculator = _channelCalculators.FirstOrDefault(c => c.ChannelType == channelType);
            if (channelCalculator == null)
            {
                throw new ArgumentException($"Invalid {nameof(ChannelType)}: {channelType}");
            }

            return channelCalculator.CalculateChannels(parameters, inputs);
        }
    }
}