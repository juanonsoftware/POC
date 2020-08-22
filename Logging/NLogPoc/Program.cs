using System;
using NLog;

namespace NLogPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("Hello World");
            logger.Info("Hello World {Time}", new { Date = DateTime.Now });
            logger.Info("Hello World {Number} - {Time}", 100, new { Date = DateTime.Now });

            Console.ReadLine();
        }
    }
}
