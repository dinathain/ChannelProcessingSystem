using ChannelProcessing;
using System.Reflection;

namespace ExampleChannelProcessorApp
{
    internal static class FileReader
    {
        internal static decimal[] GetChannelsFromEmbeddedResource()
        {
            // Get contents from embedded resources
            var assembly = Assembly.GetExecutingAssembly();
            const string channelsResourceName = "ExampleChannelProcessorApp.ExampleInputs.channels.txt";

            decimal[] channelInputValues = null;
            bool xChannelInputValuesFound;
            using (Stream stream = assembly.GetManifestResourceStream(channelsResourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        throw new Exception("The file is empty. Please populate.");
                    }

                    if (line.StartsWith("X, "))
                    {
                        channelInputValues = GetValuesForChannel('X', line);
                        break;
                    }

                    throw new Exception("All lines should begin with 'X, '.");
                }
            }

            return channelInputValues;
        }

        internal static Parameters GetParametersFromEmbeddedResource()
        {
            // Get contents from embedded resources
            var assembly = Assembly.GetExecutingAssembly();
            const string parametersResourceName = "ExampleChannelProcessorApp.ExampleInputs.parameters.txt";

            decimal mValue = 0;
            decimal cValue = 0;

            using (Stream stream = assembly.GetManifestResourceStream(parametersResourceName))
            using (StreamReader reader = new(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        throw new Exception("The file is empty. Please populate.");
                    }

                    if (line.StartsWith("m, "))
                    {
                        mValue = GetValueForScalar('m', line);
                        continue;
                    }

                    if (line.StartsWith("c, "))
                    {
                        cValue = GetValueForScalar('c', line);
                        continue;
                    }

                    throw new Exception("All lines should begin with either 'm, ' or 'c, '.");
                }
            }

            return new Parameters()
            {
                ScalarM = mValue,
                ScalarC = cValue
            };
        }

        private static decimal GetValueForScalar(char scalarLetter, string line)
        {
            line = line.Replace($"{scalarLetter}, ", "");
            var values = line.Split(", ");
            if (values.Length == 0)
            {
                throw new Exception($"There needs to be a value provided for the scalar {scalarLetter}.");
            }
            if (values.Length > 1)
            {
                throw new Exception($"There should only be one value provided for the scalar {scalarLetter}.");
            }
            if (!decimal.TryParse(values[0], out decimal value))
            {
                throw new Exception($"Value for {scalarLetter} should be a decimal.");
            }
            return value;
        }

        private static decimal[] GetValuesForChannel(char channelTypeLetter, string line)
        {
            line = line.Replace($"{channelTypeLetter}, ", "");
            var values = line.Split(", ");
            decimal[] channelValues = new decimal[values.Length];
            if (values.Length == 0)
            {
                throw new Exception($"There needs to be a value provided for the scalar {channelTypeLetter}.");
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (!decimal.TryParse(values[i], out decimal value))
                {
                    throw new Exception($"Values for {channelTypeLetter} should be a decimal.");
                }
                else
                {
                    channelValues[i] = value;
                }
            }

            return channelValues;
        }
    }
}