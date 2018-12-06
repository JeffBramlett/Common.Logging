using System;
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
        public string ElaspedTime { get; set; }

        public string ModuleName { get; set; }

        public string CallerFile { get; set; }

        public string CallerMethod { get; set; }

        public int LineNo { get; set; }

        public Exception Exception { get; set; }

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

        public Logger(Action<LogEntry> writeAction) :
            base(writeAction)
        {

        }
        #endregion

        #region Publics
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">the type of the calling object</param>
        /// <param name="filepath"></param>
        /// <param name="caller"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        public ILogger SetModuleMetadata(Type type, [CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0)
        {
            ModuleName = type.Name;
            CallerFile = filepath.Substring(filepath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            CallerMethod = caller;
            LineNo = lineNo;

            return this;
        }

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
                ElaspedTime = ElaspedTime,

            };

            if (!string.IsNullOrEmpty(CallerFile))
            {
                logEntry.ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                    ModuleName = ModuleName
                };
            }

            logEntry.Level = LogLevels.Debug;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            AddItem(logEntry);

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
                ElaspedTime = ElaspedTime
            };

            if (!string.IsNullOrEmpty(CallerFile))
            {
                logEntry.ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                    ModuleName = ModuleName
                };
            }

            logEntry.Level = LogLevels.Error;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            AddItem(logEntry);

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
                ElaspedTime = ElaspedTime
            };

            if (!string.IsNullOrEmpty(CallerFile))
            {
                logEntry.ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                    ModuleName = ModuleName
                };
            }

            logEntry.Level = LogLevels.Fatal;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            AddItem(logEntry);

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
                ElaspedTime = ElaspedTime
            };

            if (!string.IsNullOrEmpty(CallerFile))
            {
                logEntry.ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                    ModuleName = ModuleName
                };
            }

            logEntry.Level = LogLevels.Info;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            AddItem(logEntry);

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
                ElaspedTime = ElaspedTime
            };

            if (!string.IsNullOrEmpty(CallerFile))
            {
                logEntry.ModuleMetadata = new ModuleMetadata()
                {
                    CallerFile = CallerFile,
                    CallerMethod = CallerMethod,
                    LineNo = LineNo,
                    ModuleName = ModuleName
                };
            }

            logEntry.Level = LogLevels.Warning;

            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            logEntry.Message = msg;
            logEntry.Exception = Exception;
            logEntry.Timestamp = DateTimeOffset.Now;

            AddItem(logEntry);

            ResetDefaults();
        }

        /// <summary>
        /// Set an exception before logging a message
        /// </summary>
        /// <param name="ex">the exception to include in the log</param>
        /// <returns>this logger object</returns>
        public ILogger SetException(Exception ex)
        {
            Exception = ex;
            return this;
        }

        /// <summary>
        /// Set the Elasped time before logging the message
        /// </summary>
        /// <param name="elasped">the elasped time</param>
        /// <returns>this logger object</returns>
        public ILogger SetElaspedTimeSpan(TimeSpan elasped)
        {
            ElaspedTime = elasped.ToString();
            return this;
        }
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

        #region Disposable Support
        public override void GeneralDispose()
        {
            base.GeneralDispose();
        }
        #endregion
    }
}
