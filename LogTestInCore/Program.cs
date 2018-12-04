using System;
using System.Diagnostics;

namespace LogTestInCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            DisplayLogger.Instance().SetApplicationMetaData();
            sw.Stop();

            DisplayLogger.Instance().SetElaspedTimeSpan(sw.Elapsed).LogFatal("Metadata set");

            DisplayLogger.Instance().LogDebug("Hello World!");
            DisplayLogger.Instance().LogInfo("Info {0}", 1);
            LogMore();
            LogWithException();
            Console.ReadKey();

            DisplayLogger.Instance().Dispose();
        }

        private static void LogMore()
        {
            Stopwatch sw = Stopwatch.StartNew();
            DisplayLogger.Instance().LogWarning("with MORE");
            sw.Stop();

            DisplayLogger.Instance().SetElaspedTimeSpan(sw.Elapsed).LogInfo("How much time to log");
        }

        private static void LogWithException()
        {
            try
            {
                var zero = 0;
                var exception = 4 / zero;  // throws divide by zero exception
            }
            catch (Exception ex)
            {
                DisplayLogger.Instance().SetException(ex).LogError("Divide by Zero test");
                
            }
        }
    }
}
