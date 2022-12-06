using ChannelProcessing;
using ChannelProcessing.ChannelCalculators;
using ExampleChannelProcessorApp;
using Microsoft.Extensions.DependencyInjection;

try
{
    // Set up DI
    Console.WriteLine("Initialising...");
    ServiceProvider serviceProvider = IoCConfig.GetDIServiceProvider();

    // Get calculators
    var selectedMetricCalculator = serviceProvider.GetService<ISelectedMetricCalculator>();
    var selectedChannelCalculator = serviceProvider.GetService<ISelectedChannelCalculator>();

    // Read embedded resources for example data
    var parameters = FileReader.GetParametersFromEmbeddedResource();
    var channelInputs = FileReader.GetChannelsFromEmbeddedResource();

    // Ask user to pick between metric and channel calculation
    ConsoleKey metricOrChannelKeyPress = UserInputRequester.AskUserForMetricOrChannel();

    // Channel calculation requested
    if (metricOrChannelKeyPress == ConsoleKey.C)
    {
        ConsoleKey keyPressedForChannelType = UserInputRequester.AskUserForCalculationType<ChannelType>();

        if (selectedChannelCalculator == null)
        {
            throw new Exception("Channel calculator could not be found.");
        }
        var channelTypeSelected = (ChannelType)(Enum.Parse(typeof(ChannelType), keyPressedForChannelType.ToString()));
        var result = string.Join(", ", selectedChannelCalculator.CalculateChannels(parameters, channelInputs, channelTypeSelected));
        Console.WriteLine($"Values for channel {channelTypeSelected} = [{result}]");
    }

    // Metric calculation requested
    else if (metricOrChannelKeyPress == ConsoleKey.M)
    {
        ConsoleKey keyPressedForMetricType = UserInputRequester.AskUserForCalculationType<MetricType>();

        if (selectedMetricCalculator == null)
        {
            throw new Exception("Metric calculator could not be found.");
        }
        var metricTypeSelected = (MetricType)(Enum.Parse(typeof(MetricType), keyPressedForMetricType.ToString().ToLower()));
        var result = selectedMetricCalculator.CalculateMetric(parameters, channelInputs, metricTypeSelected).ToString();
        Console.WriteLine($"Value of metric {metricTypeSelected} = " + result);
    }
}
catch (Exception ex)
{
    Console.WriteLine("An error occured. Please contact Support. Details: " + ex.Message);
    Thread.Sleep(1000 * 3); // Wait 3 seconds before closing
    Environment.Exit(0);
}