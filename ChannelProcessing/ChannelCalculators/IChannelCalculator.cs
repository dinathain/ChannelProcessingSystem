namespace ChannelProcessing.ChannelCalculators
{
    public interface IChannelCalculator
    {
        /// <summary>
        /// The type of Channel value that the Calculator will output
        /// </summary>
        ChannelType ChannelType { get; }

        /// <summary>
        /// Calculates the channel value for the provided parameters and inputs
        /// </summary>
        /// <param name="parameters">Scalars used for calculations</param>
        /// <param name="inputs">An array of decimal inputs for channel X values used for calculations</param>
        /// <returns>An array of decimals containing the calculated channel values</returns>
        decimal[] CalculateChannels(Parameters parameters, decimal[] inputs);
    }
}