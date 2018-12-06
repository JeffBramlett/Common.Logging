﻿using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;

namespace LogTestInCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Logger logger = new Logger(WriteTheLogEntry);

            sw.Stop();

            logger.LogDebug(typeof(Program), "Hello World!");
            logger.LogInfo(typeof(Program), "Info 1");
            LogMore(logger);
            LogWithException(logger);

            Console.ReadKey();

            logger.Dispose();
        }

        private static void LogMore(Logger logger)
        {
            Stopwatch sw = Stopwatch.StartNew();
            logger.LogWarning(typeof(Program), "with MORE");
            sw.Stop();

            logger.LogInfo(typeof(Program), "How much time to log", sw.Elapsed);
        }

        private static void LogWithException(Logger logger)
        {
            try
            {
                var zero = 0;
                var exception = 4 / zero;  // throws divide by zero exception
            }
            catch (Exception ex)
            {
                logger.LogError(typeof(Program), "Divide by Zero test", null, ex);

            }
        }

        private static void WriteTheLogEntry(LogEntry logEntry)
        {
            var originColor = Console.ForegroundColor;

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            serializerSettings.Converters.Add(new StringEnumConverter());

            string contents = JsonConvert.SerializeObject(logEntry, serializerSettings);

            switch (logEntry.Level)
            {
                case LogLevels.Debug:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(contents);
                    break;
                case LogLevels.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(contents);
                    break;
                case LogLevels.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(contents);
                    break;
                case LogLevels.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(contents);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(contents);
                    break;
            }

            Console.ForegroundColor = originColor;
        }

    }
}
