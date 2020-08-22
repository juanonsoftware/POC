using Serilog;
using System;

namespace SerilogPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

            Log.Information("Hello, Serilog!");
            Log.Information("Hello, Serilog! {@Number}", 100);
            Log.Information("Ext Info 1 {0}", new { Number = 100 });
            Log.Information("Hello, Serilog! {0} - {1}", new { Number = 100 }, new
            {
                Date = DateTime.Today,
                Message = "This is a \"NICE\""
            });
            Log.Information("Ext Info 3 {@User}", new User
            {
                Id = 1,
                DateOfBirth = DateTime.Today,
                Status = true
            });
            Log.Information("Ext Info 4 {@User}", null);

            LogDebugWithObjectValueAndException();

            Log.CloseAndFlush();
            Console.ReadLine();
        }

        private static void LogDebugWithObjectValueAndException()
        {
            try
            {
                int y = 0;
                object x = 100 / y;
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Error occured {Info}", new { X = 100, Y = 0 });
            }
        }
    }
}
