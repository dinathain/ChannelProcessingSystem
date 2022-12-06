namespace ExampleChannelProcessorApp
{
    internal static class UserInputRequester
    {
        internal static ConsoleKey AskUserForMetricOrChannel()
        {
            ConsoleKey metricOrChannelKeyPress = 0;
            while (metricOrChannelKeyPress == 0)
            {
                RequestMetricOrChannelFromUserOrRetry(out metricOrChannelKeyPress);
            }

            return metricOrChannelKeyPress;
        }

        internal static ConsoleKey AskUserForCalculationType<T>()
        {
            ConsoleKey keyPressed = 0;
            while (keyPressed == 0)
            {
                var types = Enum.GetNames(typeof(T));
                if (types == null)
                {
                    keyPressed = 0;
                }
                else
                {
                    var typesString = types.Aggregate((i, j) => $"'{i}',  {j}");
                    Console.WriteLine($"What would you like to calculate? Please type one of the following: {typesString}.");
                    keyPressed = Console.ReadKey(true).Key;
                    if (!types.Select(x => x.ToUpper()).Contains(keyPressed.ToString()))
                    {
                        keyPressed = 0;
                        Console.WriteLine("Invalid key pressed.");
                    }
                }
            }

            return keyPressed;
        }

        private static void RequestMetricOrChannelFromUserOrRetry(out ConsoleKey keyPressed)
        {
            Console.WriteLine("Would you like to calculate channels or a metric? Please type 'c' for channels, or 'm' for metric.");
            keyPressed = Console.ReadKey(true).Key;
            if (!(keyPressed == ConsoleKey.M || keyPressed == ConsoleKey.C))
            {
                keyPressed = 0;
                Console.WriteLine("Invalid key pressed.");
            }
        }
    }
}