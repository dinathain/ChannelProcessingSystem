using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;

namespace ChannelProcessingUnitTests.ChannelCalculatorTests
{
    public class ChannelYCalculatorTests
    {
        public static IEnumerable<object[]> ChannelYCalculatorTestData =>
            new List<object[]>
            {
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 1.16m, 0.52m, 1.438m, 1.636m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { 0m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.52m, 1.438m, 1.636m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -0.16m, 0.52m, 1.438m, 1.636m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.5m, 0.5m, 0.5m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { 0m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.5m, 0.5m, 0.5m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.5m, 0.5m, 0.5m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.16m, -0.48m, 0.438m, 0.636m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { 0m, 0.01m, 0.469m, 0.568m }, new decimal[] { -0.5m, -0.48m, 0.438m, 0.636m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -1.16m, -0.48m, 0.438m, 0.636m }}
            };

        [Theory]
        [MemberData(nameof(ChannelYCalculatorTestData))]
        public void ChannelYCalculator_CalculateChannels_ShouldReturnCorrectOutputs(Parameters parameters, decimal[] channelInputs, decimal[] expectedOutputs)
        {
            //Arrange
            var calculator = new ChannelYCalculator();

            //Act
            var result = calculator.CalculateChannels(parameters, channelInputs);

            //Assert
            Assert.True(result.Length == channelInputs.Length);
            Assert.Equal(expectedOutputs, result);
        }
    }
}