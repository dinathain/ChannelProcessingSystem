namespace ChannelProcessing.ChannelCalculators
{
    public interface IChannelACalculator : IChannelCalculator
    {
    }

    public class ChannelACalculator : IChannelACalculator
    {
        public ChannelType ChannelType => ChannelType.A;

        /// <summary>
        /// Calculates the A channel value for the provided parameters and inputs
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <returns>An array of decimals containing the calculated channel values for A</returns>
        /// <exception cref="ArgumentException">Thrown when any inputs have a value of 0.</exception>
        public decimal[] CalculateChannels(Parameters parameters, decimal[] inputs)
        {
            // Validate input values to ensure we do not divide by zero
            if (inputs.Any(x => x == 0))
            {
                throw new ArgumentException($"Input channel data is invalid. Some X channel values are 0 and therefore corresponding values of {ChannelType} could not be calculated. Please amend.");
            }

            decimal[] result = new decimal[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                var output = 1 / inputs[i];
                result[i] = output;
            }
            return result;
        }
    }
}