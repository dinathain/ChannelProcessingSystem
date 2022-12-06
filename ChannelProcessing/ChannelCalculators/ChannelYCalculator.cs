namespace ChannelProcessing.ChannelCalculators
{
    public interface IChannelYCalculator : IChannelCalculator
    {
    }

    public class ChannelYCalculator : IChannelYCalculator
    {
        public ChannelType ChannelType => ChannelType.Y;

        /// <summary>
        /// Calculates the Y channel value for the provided parameters and inputs
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <returns>An array of decimals containing the calculated channel values for Y</returns>
        public decimal[] CalculateChannels(Parameters parameters, decimal[] inputs)
        {
            decimal[] result = new decimal[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                var output = (parameters.ScalarM * inputs[i]) + parameters.ScalarC;
                result[i] = output;
            }
            return result;
        }
    }
}