using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;
using ChannelProcessing.MetricCalculators;
using Moq;

namespace ChannelProcessingUnitTests.MetricCalculatorTests
{
    public class MetricBCalculatorTests
    {
        public static IEnumerable<object[]> MetricBCalculatorTestData_Valid =>
           new List<object[]>
           {
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 4.19030303030303m, 100.52m, 3.57019616204691m, 3.39656338028169m }, 27.9192656431579075m },
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -2.53030303030303m, 100.52m, 3.57019616204691m, 3.39656338028169m }, 26.2391141280063925m },
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 2.87030303030303m, 100.52m, 3.57019616204691m, 3.39656338028169m }, 27.5892656431579075m },
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -2.53030303030303m, 100.5m, 2.63219616204691m, 2.26056338028169m }, 25.7156141280063925m },
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { 3.53030303030303m, 100.5m, 2.63219616204691m, 2.26056338028169m }, 27.2307656431579075m },
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, new decimal[] { -2.53030303030303m, 100.5m, 2.63219616204691m, 2.26056338028169m }, 25.7156141280063925m }
           };

        [Theory]
        [MemberData(nameof(MetricBCalculatorTestData_Valid))]
        public void MetricBCalculator_CalculateChannels_ShouldReturnCorrectOutputsWithValidInputs(Parameters parameters, decimal[] channelInputs, decimal[] bOutputs, decimal expectedOutput)
        {
            //Arrange
            Mock<IChannelBCalculator> mockBCalculator = new();
            mockBCalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Returns(bOutputs);

            var calculator = new MetricBCalculator(mockBCalculator.Object);

            //Act
            var result = calculator.CalculateMetric(parameters, channelInputs);

            //Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void MetricBCalculator_CalculateChannels_ShouldThrowExceptionsFoundDuringAnyCalculations()
        {
            //Arrange
            Mock<IChannelBCalculator> mockBCalculator = new();
            const string errorMessage = "Random error message.";
            mockBCalculator
               .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
               .Throws(new Exception(errorMessage));

            var calculator = new MetricBCalculator(mockBCalculator.Object);

            //Act & Assert
            var exception = Assert.Throws<Exception>(() => calculator.CalculateMetric(It.IsAny<Parameters>(), It.IsAny<decimal[]>()));
            Assert.Equal(errorMessage, exception.Message);
        }
    }
}