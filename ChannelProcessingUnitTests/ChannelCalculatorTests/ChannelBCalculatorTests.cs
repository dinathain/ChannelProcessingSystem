using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;
using Moq;

namespace ChannelProcessingUnitTests.ChannelCalculatorTests
{
    public class ChannelBCalculatorTests
    {
        public static IEnumerable<object[]> ChannelBCalculatorTestData_Valid =>
           new List<object[]>
           {
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 1.16m, 0.52m, 1.438m, 1.636m }, new decimal[] { 3.03030303030303m, 100m, 2.13219616204691m, 1.76056338028169m }, new decimal[] { 4.19030303030303m, 100.52m, 3.57019616204691m, 3.39656338028169m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.52m, 1.438m, 1.636m }, new decimal[] { -3.03030303030303m, 100m, 2.13219616204691m, 1.76056338028169m }, new decimal[] { -2.53030303030303m, 100.52m, 3.57019616204691m, 3.39656338028169m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -0.16m, 0.52m, 1.438m, 1.636m }, new decimal[] { 3.03030303030303m, 100m, 2.13219616204691m, 1.76056338028169m }, new decimal[] { 2.87030303030303m, 100.52m, 3.57019616204691m, 3.39656338028169m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.5m, 0.5m, 0.5m }, new decimal[] { -3.03030303030303m, 100m, 2.13219616204691m, 1.76056338028169m }, new decimal[] { -2.53030303030303m, 100.5m, 2.63219616204691m, 2.26056338028169m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.5m, 0.5m, 0.5m }, new decimal[] { 3.03030303030303m, 100m, 2.13219616204691m, 1.76056338028169m }, new decimal[] { 3.53030303030303m, 100.5m, 2.63219616204691m, 2.26056338028169m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 0.5m, 0.5m, 0.5m, 0.5m }, new decimal[] { -3.03030303030303m, 100m, 2.13219616204691m, 1.76056338028169m }, new decimal[] { -2.53030303030303m, 100.5m, 2.63219616204691m, 2.26056338028169m }}
           };

        public static IEnumerable<object[]> ChannelBCalculatorTestData_InvalidChannelOutputLengths =>
            new List<object[]>
            {
                         new object[] { new decimal[5], new decimal[2], new decimal[5]},
                         new object[] { new decimal[5], new decimal[5], new decimal[7]}
            };

        [Theory]
        [MemberData(nameof(ChannelBCalculatorTestData_Valid))]
        public void ChannelBCalculator_CalculateChannels_ShouldReturnCorrectOutputsWithValidInputs(Parameters parameters, decimal[] channelInputs, decimal[] yOutputs, decimal[] aOutputs, decimal[] expectedOutputs)
        {
            //Arrange
            Mock<IChannelACalculator> mockACalculator = new();
            mockACalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Returns(aOutputs);
            Mock<IChannelYCalculator> mockYCalculator = new();
            mockYCalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Returns(yOutputs);

            var calculator = new ChannelBCalculator(mockYCalculator.Object, mockACalculator.Object);

            //Act
            var result = calculator.CalculateChannels(parameters, channelInputs);

            //Assert
            Assert.True(result.Length == channelInputs.Length);
            Assert.Equal(expectedOutputs, result);
        }

        [Theory]
        [MemberData(nameof(ChannelBCalculatorTestData_InvalidChannelOutputLengths))]
        public void ChannelBCalculator_CalculateChannels_ShouldThrowExceptionIfOtherMetricCalculationsNotCorrectLength(decimal[] channelInputs, decimal[] yOutputs, decimal[] aOutputs)
        {
            //Arrange
            Mock<IChannelACalculator> mockACalculator = new();
            mockACalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Returns(aOutputs);
            Mock<IChannelYCalculator> mockYCalculator = new();
            mockYCalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Returns(yOutputs);

            var calculator = new ChannelBCalculator(mockYCalculator.Object, mockACalculator.Object);

            //Act
            var action = () => calculator.CalculateChannels(It.IsAny<Parameters>(), channelInputs);

            //Assert
            var exception = Assert.Throws<Exception>(action);
            const string expectedErrorMessage = "Error occured whilst gathering Y and A channel values. Not all outputs were successfully calculated.";
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void ChannelBCalculator_CalculateChannels_ShouldThrowExceptionsFoundDuringAnyCalculations()
        {
            //Arrange
            Mock<IChannelACalculator> mockACalculator = new();
            const string errorMessage = "Random error message.";
            mockACalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Throws(new Exception(errorMessage));
            Mock<IChannelYCalculator> mockYCalculator = new();
            mockYCalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Returns(It.IsAny<decimal[]>());

            var calculator = new ChannelBCalculator(mockYCalculator.Object, mockACalculator.Object);

            //Act
            var action = () => calculator.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>());

            //Assert
            var exception = Assert.Throws<Exception>(action);
            Assert.Equal(errorMessage, exception.Message);
        }
    }
}