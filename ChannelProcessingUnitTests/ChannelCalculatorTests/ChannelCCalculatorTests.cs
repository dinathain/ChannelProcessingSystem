using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;
using ChannelProcessing.MetricCalculators;
using Moq;

namespace ChannelProcessingUnitTests.ChannelCalculatorTests
{
    public class ChannelCCalculatorTests
    {
        public static IEnumerable<object[]> ChannelCCalculatorTestData_Valid =>
           new List<object[]>
           {
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, 27.9192656431579m, new decimal[] { 28.2492656431579m, 27.9292656431579m, 28.3882656431579m, 28.4872656431579m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, 26.2391141280064m, new decimal[] { 25.9091141280064m, 26.2491141280064m, 26.7081141280064m, 26.8071141280064m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, 27.5892656431579m, new decimal[] { 27.9192656431579m, 27.5992656431579m, 28.0582656431579m, 28.1572656431579m }},
                new object[] { new Parameters() { ScalarM = 0m, ScalarC = 0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, 25.7156141280064m, new decimal[] { 25.3856141280064m, 25.7256141280064m, 26.1846141280064m, 26.2836141280064m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { 0.33m, 0.01m, 0.469m, 0.568m }, 27.2307656431579m, new decimal[] { 27.5607656431579m, 27.2407656431579m, 27.6997656431579m, 27.7987656431579m }},
                new object[] { new Parameters() { ScalarM = 2m, ScalarC = -0.5m }, new decimal[] { -0.33m, 0.01m, 0.469m, 0.568m }, 25.7156141280064m, new decimal[] { 25.3856141280064m, 25.7256141280064m, 26.1846141280064m, 26.2836141280064m }}
           };

        [Theory]
        [MemberData(nameof(ChannelCCalculatorTestData_Valid))]
        public void ChannelCCalculator_CalculateChannels_ShouldReturnCorrectOutputsWithValidInputs(Parameters parameters, decimal[] channelInputs, decimal averageBOutput, decimal[] expectedOutputs)
        {
            //Arrange
            Mock<IMetricBCalculator> mockAverageBCalculator = new();
            mockAverageBCalculator
                .Setup(x => x.CalculateMetric(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Returns(averageBOutput);

            var calculator = new ChannelCCalculator(mockAverageBCalculator.Object);

            //Act
            var result = calculator.CalculateChannels(parameters, channelInputs);

            //Assert
            Assert.True(result.Length == channelInputs.Length);
            Assert.Equal(expectedOutputs, result);
        }

        [Fact]
        public void ChannelCCalculator_CalculateChannels_ShouldThrowExceptionsFoundDuringAnyCalculations()
        {
            //Arrange
            Mock<IMetricBCalculator> mockAverageBCalculator = new();
            const string errorMessage = "Random error message.";
            mockAverageBCalculator
                .Setup(x => x.CalculateMetric(It.IsAny<Parameters>(), It.IsAny<decimal[]>()))
                .Throws(new Exception(errorMessage));

            var calculator = new ChannelCCalculator(mockAverageBCalculator.Object);

            //Act
            var action = () => calculator.CalculateChannels(It.IsAny<Parameters>(), It.IsAny<decimal[]>());

            //Assert
            var exception = Assert.Throws<Exception>(action);
            Assert.Equal(errorMessage, exception.Message);
        }
    }
}