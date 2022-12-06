using ChannelProcessing.ChannelCalculators;
using ChannelProcessing.MetricCalculators;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleChannelProcessorApp
{
    internal class IoCConfig
    {
        internal static ServiceProvider GetDIServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IMetricBCalculator, MetricBCalculator>()
                .AddScoped<IChannelACalculator, ChannelACalculator>()
                .AddScoped<IChannelBCalculator, ChannelBCalculator>()
                .AddScoped<IChannelCCalculator, ChannelCCalculator>()
                .AddScoped<IChannelYCalculator, ChannelYCalculator>()
                .AddScoped<ISelectedChannelCalculator, SelectedChannelCalculator>()
                .AddScoped<IChannelCalculator, ChannelACalculator>()
                .AddScoped<IChannelCalculator, ChannelBCalculator>()
                .AddScoped<IChannelCalculator, ChannelCCalculator>()
                .AddScoped<IChannelCalculator, ChannelYCalculator>()
                .AddSingleton<ISelectedMetricCalculator, SelectedMetricCalculator>()
                .AddScoped<IMetricCalculator, MetricBCalculator>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}