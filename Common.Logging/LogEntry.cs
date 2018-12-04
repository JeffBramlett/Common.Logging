using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Logging
{
    /// <summary>
    /// Enumeration of logging levels
    /// </summary>
    public enum LogLevels
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    /// <summary>
    /// Data class of a Log Entry
    /// </summary>
    public class LogEntry
    {
        #region fields
        ApplicationMetadata _appMetadata;
        CallerMetadata _callerMetadata;
        #endregion

        #region Properties
        /// <summary>
        /// The log level for this log entry
        /// </summary>
        public LogLevels Level { get; set; }

        /// <summary>
        /// The log message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// (optional) the exception to include with this log
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// (optional) the elasped time to report in this log entry
        /// </summary>
        public string ElaspedTime { get; set; }

        /// <summary>
        /// When this log was entered
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// The application data for this log entry
        /// </summary>
        public ApplicationMetadata ApplicationMetadata
        {
            get
            {
                if (_appMetadata == null)
                {
                    _appMetadata = new ApplicationMetadata();
                }

                return _appMetadata;
            }
            set
            {
                _appMetadata = value;
            }
        }

        /// <summary>
        /// The caller metadata for this log entry
        /// </summary>
        public CallerMetadata CallerMetadata
        {
            get
            {
                _callerMetadata = _callerMetadata ?? new CallerMetadata();
                return _callerMetadata;
            }
            set { _callerMetadata = value; }
        }
        #endregion

        #region Ctors and Dtors
        /// <summary>
        /// Default Ctor
        /// </summary>
        public LogEntry()
        {
            Timestamp = DateTimeOffset.Now;
        }
        #endregion
    }
}
