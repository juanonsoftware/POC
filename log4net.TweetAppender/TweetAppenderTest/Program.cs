using System;
using log4net;
using log4net.Config;

namespace TweetAppenderTest
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private static void Main(string[] args)
        {
            // Set up a simple configuration that logs on the console.
            XmlConfigurator.Configure();

            Log.Info("Entering application.");

            do
            {
                Console.WriteLine("Press Y then enter to continue");

                Log.Info(new
                {
                    Message = string.Format("You press at {0}", DateTime.Now),
                    Link = "",
                });

            } while (Console.ReadLine().Trim() == "Y");

            Log.Info("Exiting application.");
        }
    }
}