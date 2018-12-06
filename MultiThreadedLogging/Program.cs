using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadedLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger(WriteTheLogEntry);

            ThreadPool.QueueUserWorkItem(new WaitCallback(StartLoop1ToLog), logger);
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartLoop2ToLog), logger);

            Console.ReadKey();

            logger.Dispose();
        }

        private static void StartLoop1ToLog(object state)
        {
            var logger = state as Logger;
            for (var i = 0; i < 10; i++)
            {
                var something = "Loop: 1\tStep:" + i;
                logger.SetModuleMetadata(typeof(Program)).LogInfo(something);
            }
        }

        private static void StartLoop2ToLog(object state)
        {
            var logger = state as Logger;
            for (var i = 0; i < 10; i++)
            {
                var something = "Loop: 2\tStep:" + i;
                logger.LogInfo(something);
            }
        }

        private static void WriteTheLogEntry(LogEntry logEntry)
        {
            ShowLogEntryInConsole(logEntry);
        }

        private static void ShowLogEntryInConsole(LogEntry logEntry)
        {
            string contentToLog = string.Format("{0} - {1}", logEntry.Timestamp, logEntry.Message);
            Console.WriteLine(contentToLog);
        }

    }
}
