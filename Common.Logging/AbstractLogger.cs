using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common.Logging
{
    /// <summary>
    /// Abstraction of a Logger 
    /// </summary>
    public abstract class AbstractLogger : IFluentLogger
    {
        #region Private
        private GenericSpooler<LogEntry> _logEntrySpooler;
        ApplicationMetadata _appMetaData;
        #endregion

        #region Properties
        public string ElaspedTime { get; set; }

        public string CallerFile { get; set; }

        public string CallerMethod { get; set; }

        public int LineNo { get; set; }

        public Exception Exception { get; set; }

        private ApplicationMetadata AppMetaData
        {
            get
            {
                _appMetaData = _appMetaData ?? new ApplicationMetadata();
                return _appMetaData;
            }
        }

        private GenericSpooler<LogEntry> LogEntrySpooler
        {
            get
            {
                if (_logEntrySpooler == null)
                {
                    _logEntrySpooler = new GenericSpooler<LogEntry>(WriteTheLogEntry);
                }
                return _logEntrySpooler;
            }
        }
        #endregion

        #region Publics
        /// <summary>
        /// Log a Debug message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        public void LogDebug(string message = "", params object[] args)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                CallerMetadata = new CallerMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                },
                ElaspedTime = ElaspedTime
            };

            logEntry.Level = LogLevels.Debug;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            LogEntrySpooler.AddItem(logEntry);

            ResetDefaults();
        }

        /// <summary>
        /// Log an Error message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        public void LogError(string message = "", params object[] args)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                CallerMetadata = new CallerMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                },
                ElaspedTime = ElaspedTime
            };

            logEntry.Level = LogLevels.Error;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            LogEntrySpooler.AddItem(logEntry);

            ResetDefaults();
        }

        /// <summary>
        /// Log a Fatal message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        public void LogFatal(string message = "", params object[] args)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                CallerMetadata = new CallerMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                },
                ElaspedTime = ElaspedTime
            };

            logEntry.Level = LogLevels.Fatal;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            LogEntrySpooler.AddItem(logEntry);

            ResetDefaults();
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        public void LogInfo(string message = "", params object[] args)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                CallerMetadata = new CallerMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                },
                ElaspedTime = ElaspedTime
            };

            logEntry.Level = LogLevels.Info;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            LogEntrySpooler.AddItem(logEntry);

            ResetDefaults();
        }

        /// <summary>
        /// Log a Warning message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        public void LogWarning(string message = "", params object[] args)
        {
            var logEntry = new LogEntry()
            {
                ApplicationMetadata = AppMetaData,
                CallerMetadata = new CallerMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                },
                ElaspedTime = ElaspedTime
            };

            logEntry.Level = LogLevels.Warning;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            LogEntrySpooler.AddItem(logEntry);

            ResetDefaults();
        }

        /// <summary>
        /// Set an exception before logging a message
        /// </summary>
        /// <param name="ex">the exception to include in the log</param>
        /// <returns>this logger object</returns>
        public IFluentLogger SetException(Exception ex)
        {
            Exception = ex;
            return this;
        }

        /// <summary>
        /// Set the Elasped time before logging the message
        /// </summary>
        /// <param name="elasped">the elasped time</param>
        /// <returns>this logger object</returns>
        public IFluentLogger SetElaspedTimeSpan(TimeSpan elasped)
        {
            ElaspedTime = elasped.ToString();
            return this;
        }

        /// <summary>
        /// Sets the overall application metadata from the Enviroment
        /// </summary>
        /// <returns>this logger object</returns>
        public IFluentLogger SetApplicationMetaData()
        {
            Assembly asm = Assembly.GetEntryAssembly();

            AppMetaData.ApplicationName = asm.FullName;
            AppMetaData.Environment = Environment.UserDomainName;
            AppMetaData.OS = Environment.OSVersion.Platform.ToString();

            return this;
        }

        /// <summary>
        /// Sets the Caller metadata using the Compiler options
        /// </summary>
        /// <param name="filepath">the code filepath</param>
        /// <param name="caller">the method making the call</param>
        /// <param name="lineNo">the line number where the call originated</param>
        /// <returns>this logger object</returns>
        public IFluentLogger SetCallerMetaData([CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            CallerFile = filepath.Substring(filepath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            CallerMethod = caller;
            LineNo = lineNo;

            return this;
        }
        #endregion

        #region Abstracts
        /// <summary>
        /// The overridde for implementing how the spooled LogEntry is persisted 
        /// </summary>
        /// <param name="logEntry"></param>
        protected abstract void WriteTheLogEntry(LogEntry logEntry);
        #endregion

        #region privates
        private void ResetDefaults()
        {
            CallerFile = null;
            CallerMethod = null;
            LineNo = 0;
            Exception = null;
            ElaspedTime = null;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_logEntrySpooler != null)
                    {
                        _logEntrySpooler.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~AbstractLogger()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose of this Logger
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
