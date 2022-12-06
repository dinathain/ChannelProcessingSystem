using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;
using Moq;

namespace ChannelProcessingUnitTests.ChannelCalculatorTests
{
    public class SelectedChannelCalculatorTests
    {
        public static IEnumerable<object[]> SelectedChannelCalculatorTestData_Valid =>
           new List<object[]>
           {
                new object[] {new decimal[4], new decimal[] { 4.19030303030303m, 100.52m, 3.57019616204691m, 3.39656338028169m }},
                new object[] {new decimal[4], new decimal[] { 0m, 100.52m, 3.57019616204691m, 3.39656338028169m }},
           };

        [Theory]
        [MemberData(nameof(SelectedChannelCalculatorTestData_Valid))]
        public void SelectedChannelCalculator_CalculateChannels_ShouldReturnOutputFromSelectedCalculator(decimal[] channelInputs, decimal[] expectedOutputs)
        {
            //Arrange
            Mock<IChannelCalculator> mockCalculator = new();
            mockCalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), channelInputs))
                .Returns(expectedOutputs);

            var calculator = new SelectedChannelCalculator(new List<IChannelCalculator>() { mockCalculator.Object });

            //Act
            var result = calculator.CalculateChannels(It.IsAny<Parameters>(), channelInputs, It.IsAny<ChannelType>());

            //Assert
            Assert.True(result.Length == expectedOutputs.Length);
            Assert.Equal(expectedOutputs, result);
        }

        [Fact]
        public void SelectedChannelCalculator_CalculateChannels_ShouldThrowExceptionIfSelectedCalculatorThrowsException()
        {
            //Arrange
            var channelInputs = new decimal[4];
            Mock<IChannelCalculator> mockCalculator = new();
            const string errorMessage = "Random error message.";
            mockCalculator
                .Setup(x => x.CalculateChannels(It.IsAny<Parameters>(), channelInputs))
                .Throws(new Exception(errorMessage));

            var calculator = new SelectedChannelCalculator(new List<IChannelCalculator>() { mockCalculator.Object });

            //Act
            var action = () => calculator.CalculateChannels(It.IsAny<Parameters>(), channelInputs, It.IsAny<ChannelType>());

            //Assert
            var exception = Assert.Throws<Exception>(action);
            Assert.Equal(errorMessage, exception.Message);
        }

        [Fact]
        public void SelectedChannelCalculator_CalculateChannels_ShouldThrowArgumentExceptionIfInputsAreEmpty()
        {
            //Arrange
            var channelInputs = Array.Empty<decimal>();
            Mock<IChannelCalculator> mockCalculator = new();
            var calculator = new SelectedChannelCalculator(new List<IChannelCalculator>() { mockCalculator.Object });

            //Act
            var action = () => calculator.CalculateChannels(It.IsAny<Parameters>(), channelInputs, It.IsAny<ChannelType>());

            //Assert
            var exception = Assert.Throws<ArgumentException>(action);
            const string expectedErrorMessage = "Input channel data needs to have at least one value.";
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void SelectedChannelCalculator_CalculateChannels_ShouldThrowArgumentExceptionIfCalculatorCannotBeFound()
        {
            //Arrange
            Mock<IChannelCalculator> mockCalculator = new();
            var calculator = new SelectedChannelCalculator(new List<IChannelCalculator>() { mockCalculator.Object });

            //Act
            var action = () => calculator.CalculateChannels(new Parameters(), new decimal[5], (ChannelType)(-1));

            //Assert
            var exception = Assert.Throws<ArgumentException>(action);
            const string expectedErrorMessage = "Invalid ChannelType: -1";
            Assert.Equal(expectedErrorMessage, exception.Message);
        }
    }
}