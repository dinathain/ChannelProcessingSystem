using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;

namespace ChannelProcessingUnitTests.ChannelCalculatorTests
{
    public class ChannelACalculatorTests
    {
        public static IEnumerable<object[]> ChannelACalculatorTestData_Valid =>
            new List<object[]>
            {
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 3.0303030303030303030303030303m, 100m, 2.1321961620469083155650319829m, 1.7605633802816901408450704225m } },
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -3.0303030303030303030303030303m, 100m, 2.1321961620469083155650319829m, 1.7605633802816901408450704225m } },
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 3.0303030303030303030303030303m, 100m, 2.1321961620469083155650319829m, 1.7605633802816901408450704225m } },
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -3.0303030303030303030303030303m, 100m, 2.1321961620469083155650319829m, 1.7605633802816901408450704225m } },
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 3.0303030303030303030303030303m, 100m, 2.1321961620469083155650319829m, 1.7605633802816901408450704225m } },
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -3.0303030303030303030303030303m, 100m, 2.1321961620469083155650319829m, 1.7605633802816901408450704225m } }
            };

        public static IEnumerable<object[]> ChannelACalculatorTestData_Invalid =>
            new List<object[]>
            {
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { 0m, 0.01m, 0.469m, 0.568m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { 0m, 0.01m, 0.469m, 0.568m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { 0m, 0.01m, 0.469m, 0.568m }}
            };

        [Theory]
        [MemberData(nameof(ChannelACalculatorTestData_Valid))]
        public void ChannelACalculator_CalculateChannels_ShouldReturnCorrectOutputsWithValidInputs(Parameters parameters, decimal[] channelInputs, decimal[] expectedOutputs)
        {
            //Arrange
            var calculator = new ChannelACalculator();

            //Act
            var result = calculator.CalculateChannels(parameters, channelInputs);

            //Assert
            Assert.True(result.Length == channelInputs.Length);
            Assert.Equal(expectedOutputs, result);
        }

        [Theory]
        [MemberData(nameof(ChannelACalculatorTestData_Invalid))]
        public void ChannelACalculator_CalculateChannels_ShouldThrowExceptionWithInvalidInputs(Parameters parameters, decimal[] channelInputs)
        {
            //Arrange
            var calculator = new ChannelACalculator();

            //Act
            var action = () => calculator.CalculateChannels(parameters, channelInputs);

            //Assert
            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Input channel data is invalid. Some X channel values are 0 and therefore corresponding values of A could not be calculated. Please amend.", exception.Message);
        }
    }
}