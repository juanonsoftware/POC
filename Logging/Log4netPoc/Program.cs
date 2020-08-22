using log4net;
using System;

namespace Log4netPoc
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Logger.Debug(new { Time = DateTime.Now });
            Logger.Debug(new { Time = DateTime.Now }, null);

            Logger.DebugFormat("{0} - {1}", 100, new
            {
                Time = DateTime.Now,
                Message = "This is a \"NICE\""
            });
            Logger.DebugFormat("Info 3 - {0}", new User
            {
                Id = 1,
                DateOfBirth = DateTime.Today,
                Status = true
            });

            Console.ReadLine();
        }
    }
}
