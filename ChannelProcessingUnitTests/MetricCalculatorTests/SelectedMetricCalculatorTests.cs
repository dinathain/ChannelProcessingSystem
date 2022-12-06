using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;
using ChannelProcessing.MetricCalculators;
using Moq;

namespace ChannelProcessingUnitTests.ChannelCalculatorTests
{
    public class SelectedMetricCalculatorTests
    {
        public static IEnumerable<object[]> SelectedMetricCalculatorTestData_Valid =>
           new List<object[]>
           {
                new object[] {new decimal[4], 4.4354353m },
                new object[] {new decimal[4], -0.9080982m },
                new object[] { new decimal[4], 0m }
           };

        [Theory]
        [MemberData(nameof(SelectedMetricCalculatorTestData_Valid))]
        public void SelectedMetricCalculator_CalculateChannels_ShouldReturnOutputFromSelectedCalculator(decimal[] channelInputs, decimal expectedOutput)
        {
            //Arrange
            Mock<IMetricCalculator> mockCalculator = new();
            mockCalculator
                .Setup(x => x.CalculateMetric(It.IsAny<Parameters>(), channelInputs))
                .Returns(expectedOutput);

            var calculator = new SelectedMetricCalculator(new List<IMetricCalculator>() { mockCalculator.Object });

            //Act
            var result = calculator.CalculateMetric(It.IsAny<Parameters>(), channelInputs, It.IsAny<MetricType>());

            //Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void SelectedMetricCalculator_CalculateChannels_ShouldThrowExceptionIfSelectedCalculatorThrowsException()
        {
            //Arrange
            var channelInputs = new decimal[4];
            Mock<IMetricCalculator> mockCalculator = new();
            const string errorMessage = "Random error message.";
            mockCalculator
                .Setup(x => x.CalculateMetric(It.IsAny<Parameters>(), channelInputs))
                .Throws(new Exception(errorMessage));

            var calculator = new SelectedMetricCalculator(new List<IMetricCalculator>() { mockCalculator.Object });

            //Act & Assert
            var exception = Assert.Throws<Exception>(() => calculator.CalculateMetric(It.IsAny<Parameters>(), channelInputs, It.IsAny<MetricType>()));
            Assert.Equal(errorMessage, exception.Message);
        }

        [Fact]
        public void SelectedMetricCalculator_CalculateChannels_ShouldThrowArgumentExceptionIfInputsAreEmpty()
        {
            //Arrange
            var channelInputs = Array.Empty<decimal>();
            Mock<IMetricCalculator> mockCalculator = new();
            var calculator = new SelectedMetricCalculator(new List<IMetricCalculator>() { mockCalculator.Object });

            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => calculator.CalculateMetric(It.IsAny<Parameters>(), channelInputs, It.IsAny<MetricType>()));
            const string expectedErrorMessage = "Input channel data needs to have at least one value.";
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void SelectedMetricCalculator_CalculateChannels_ShouldThrowArgumentExceptionIfCalculatorCannotBeFound()
        {
            //Arrange
            Mock<IMetricCalculator> mockCalculator = new();
            var calculator = new SelectedMetricCalculator(new List<IMetricCalculator>() { mockCalculator.Object });

            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => calculator.CalculateMetric(new Parameters(), new decimal[5], (MetricType)(-1)));
            const string expectedErrorMessage = "Invalid MetricType: -1";
            Assert.Equal(expectedErrorMessage, exception.Message);
        }
    }
}