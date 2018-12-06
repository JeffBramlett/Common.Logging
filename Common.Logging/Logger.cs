﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Common.Logging
{
    /// <summary>
    /// Abstraction of a Logger 
    /// </summary>
    public class Logger : GenericSpooler<LogEntry>, ILogger
    {
        #region Private
        private ApplicationMetaData _appMetaData;
        #endregion

        #region Properties

        private ApplicationMetaData AppMetaData
        {
            get
            {
                if (_appMetaData == null)
                {
                    Assembly asm = Assembly.GetEntryAssembly();
                    _appMetaData = new ApplicationMetaData()
                    {
                        ApplicationName = asm.FullName,
                        Environment = Environment.UserDomainName,
                        OS = Environment.OSVersion.Platform.ToString()
                    };
                }
                return _appMetaData;
            }
        }
        #endregion

        #region Ctors and Dtors
        /// <summary>
        /// Singleton Ctor (must set the write action for log entries)
        /// </summary>
        public Logger():
            base(DefaultWriteLogEntry)
        {

        }

        /// <summary>
        /// Default Ctor
        /// </summary>
        /// <param name="writeAction">write Action for log entries (spooled to this delegate)</param>
        public Logger(Action<LogEntry> writeAction) :
            base(writeAction)
        {

        }
        #endregion

        #region Singleton
        private static Lazy<Logger> _instance = new Lazy<Logger>();

        public static ILogger Instance
        {
            get { return _instance.Value; }
        }
        #endregion

        #region Publics
        /// <summary>
        /// Sets the write action for Log entries (the logger asynchrously spools to this action)
        /// </summary>
        /// <param name="writeAction">the delegate that writes the LogEntry, or, passes to other logger (i.e. Log4Net)</param>
        public void SetWriteAction(Action<LogEntry> writeAction)
        {
            if (writeAction == null)
                throw new ArgumentNullException(nameof(writeAction));

            SpoolerAction = writeAction;
        }

        /// <summary>
        /// Log a Debug message
        /// </summary>
        /// <param name="type">the module(class) type</param>
        /// <param name="message">the message to log (optional)</param>
        /// <param name="elaspedTime">the elasped time for the LogEntry (optional)</param>
        /// <param name="ex">The exception to include in the LogEntry (optional)</param>
        /// <param name="filepath">The filepath where the log originates (supplied by system)</param>
        /// <param name="caller">The calling method (supplied by system)</param>
        /// <param name="lineNo">The line number where the Log originates (supplied by system)/param>
        public void LogDebug(Type type, string message = "", TimeSpan? elaspedTime = null, Exception ex = null, [CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                Message = message,
                Exception = ex,
                Level = LogLevels.Debug,
                Timestamp = DateTimeOffset.Now,
                ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = caller,
                    CallerMethod = filepath.Substring(filepath.LastIndexOf(Path.DirectorySeparatorChar) + 1),
                    LineNo = lineNo,
                    ModuleName = type.Name
                }
            };

            if (elaspedTime != null)
                logEntry.ElaspedTime = elaspedTime.Value.ToString();

            AddItem(logEntry);
        }

        /// <summary>
        /// Log an Error message
        /// </summary>
        /// <param name="type">the module(class) type</param>
        /// <param name="message">the message to log (optional)</param>
        /// <param name="elaspedTime">the elasped time for the LogEntry (optional)</param>
        /// <param name="ex">The exception to include in the LogEntry (optional)</param>
        /// <param name="filepath">The filepath where the log originates (supplied by system)</param>
        /// <param name="caller">The calling method (supplied by system)</param>
        /// <param name="lineNo">The line number where the Log originates (supplied by system)/param>
        public void LogError(Type type, string message = "", TimeSpan? elaspedTime = null, Exception ex = null, [CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                Message = message,
                Exception = ex,
                Level = LogLevels.Error,
                Timestamp = DateTimeOffset.Now,
                ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = caller,
                    CallerMethod = filepath.Substring(filepath.LastIndexOf(Path.DirectorySeparatorChar) + 1),
                    LineNo = lineNo,
                    ModuleName = type.Name
                }
            };

            if (elaspedTime != null)
                logEntry.ElaspedTime = elaspedTime.Value.ToString();

            AddItem(logEntry);
        }

        /// <summary>
        /// Log a Fatal message
        /// </summary>
        /// <param name="type">the module(class) type</param>
        /// <param name="message">the message to log (optional)</param>
        /// <param name="elaspedTime">the elasped time for the LogEntry (optional)</param>
        /// <param name="ex">The exception to include in the LogEntry (optional)</param>
        /// <param name="filepath">The filepath where the log originates (supplied by system)</param>
        /// <param name="caller">The calling method (supplied by system)</param>
        /// <param name="lineNo">The line number where the Log originates (supplied by system)/param>
        public void LogFatal(Type type, string message = "", TimeSpan? elaspedTime = null, Exception ex = null, [CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                Message = message,
                Exception = ex,
                Level = LogLevels.Fatal,
                Timestamp = DateTimeOffset.Now,
                ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = caller,
                    CallerMethod = filepath.Substring(filepath.LastIndexOf(Path.DirectorySeparatorChar) + 1),
                    LineNo = lineNo,
                    ModuleName = type.Name
                }
            };

            if (elaspedTime != null)
                logEntry.ElaspedTime = elaspedTime.Value.ToString();

            AddItem(logEntry);
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="type">the module(class) type</param>
        /// <param name="message">the message to log (optional)</param>
        /// <param name="elaspedTime">the elasped time for the LogEntry (optional)</param>
        /// <param name="ex">The exception to include in the LogEntry (optional)</param>
        /// <param name="filepath">The filepath where the log originates (supplied by system)</param>
        /// <param name="caller">The calling method (supplied by system)</param>
        /// <param name="lineNo">The line number where the Log originates (supplied by system)/param>
        public void LogInfo(Type type, string message = "", TimeSpan? elaspedTime = null, Exception ex = null, [CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                Message = message,
                Exception = ex,
                Level = LogLevels.Info,
                Timestamp = DateTimeOffset.Now,
                ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = caller,
                    CallerMethod = filepath.Substring(filepath.LastIndexOf(Path.DirectorySeparatorChar) + 1),
                    LineNo = lineNo,
                    ModuleName = type.Name
                }
            };

            if (elaspedTime != null)
                logEntry.ElaspedTime = elaspedTime.Value.ToString();

            AddItem(logEntry);
        }

        /// <summary>
        /// Log a Warning message
        /// </summary>
        /// <param name="type">the module(class) type</param>
        /// <param name="message">the message to log (optional)</param>
        /// <param name="elaspedTime">the elasped time for the LogEntry (optional)</param>
        /// <param name="ex">The exception to include in the LogEntry (optional)</param>
        /// <param name="filepath">The filepath where the log originates (supplied by system)</param>
        /// <param name="caller">The calling method (supplied by system)</param>
        /// <param name="lineNo">The line number where the Log originates (supplied by system)/param>
        public void LogWarning(Type type, string message = "", TimeSpan? elaspedTime = null, Exception ex = null, [CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                Message = message,
                Exception = ex,
                Level = LogLevels.Warning,
                Timestamp = DateTimeOffset.Now,
                ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = caller,
                    CallerMethod = filepath.Substring(filepath.LastIndexOf(Path.DirectorySeparatorChar) + 1),
                    LineNo = lineNo,
                    ModuleName = type.Name
                }
            };

            if (elaspedTime != null)
                logEntry.ElaspedTime = elaspedTime.Value.ToString();

            AddItem(logEntry);
        }

        #endregion

        #region privates

        private static void DefaultWriteLogEntry(LogEntry logEntry)
        {

        }

        #endregion

        #region Disposable Support
        public override void GeneralDispose()
        {
            base.GeneralDispose();
        }
        #endregion
    }
}
