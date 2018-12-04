using Common.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Converters;

namespace LogTestInCore
{
    class DisplayLogger : AbstractLogger
    {
        private static Lazy<DisplayLogger> _instance = new Lazy<DisplayLogger>();

        public static DisplayLogger Instance([CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            _instance.Value.SetCallerMetaData(filepath, caller, lineNo);
            return _instance.Value;
        }

        protected override void WriteTheLogEntry(LogEntry logEntry)
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
