namespace ChannelProcessing.ChannelCalculators
{
    public interface IChannelBCalculator : IChannelCalculator
    {
    }

    public class ChannelBCalculator : IChannelBCalculator
    {
        private readonly IChannelYCalculator _yCalculator;
        private readonly IChannelACalculator _aCalculator;

        public ChannelBCalculator(IChannelYCalculator yCalculator, IChannelACalculator aCalculator)
        {
            _yCalculator = yCalculator;
            _aCalculator = aCalculator;
        }

        public ChannelType ChannelType => ChannelType.B;

        /// <summary>
        /// Calculates the B channel value for the provided parameters and inputs
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <returns>An array of decimals containing the calculated channel values for B</returns>
        /// <exception cref="Exception">Thrown if calculators for Y and A do not return the correct number of values.</exception>
        public decimal[] CalculateChannels(Parameters parameters, decimal[] inputs)
        {
            var yValues = _yCalculator.CalculateChannels(parameters, inputs);
            var aValues = _aCalculator.CalculateChannels(parameters, inputs);

            // Validate that the outputs are the same length and therefore can be matched by index
            if (inputs.Length != yValues.Length || inputs.Length != aValues.Length)
            {
                throw new Exception("Error occured whilst gathering Y and A channel values. Not all outputs were successfully calculated.");
            }

            decimal[] result = new decimal[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                var output = yValues[i] + aValues[i];
                result[i] = output;
            }
            return result;
        }
    }
}