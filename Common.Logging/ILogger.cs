using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common.Logging
{
    /// <summary>
    /// Interface for a Logger with Fluent methods
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">the type of the calling object</param>
        /// <param name="filepath"></param>
        /// <param name="caller"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        ILogger SetModuleMetadata(Type type, [CallerFilePath] string filepath = "", [CallerMemberName] string caller = "", [CallerLineNumber] int lineNo = 0);

        /// <summary>
        /// Set an exception before logging a message
        /// </summary>
        /// <param name="ex">the exception to include in the log</param>
        /// <returns>this logger object</returns>
        ILogger SetException(Exception ex);

        /// <summary>
        /// Set the Elasped time before logging the message
        /// </summary>
        /// <param name="elasped">the elasped time</param>
        /// <returns>this logger object</returns>
        ILogger SetElaspedTimeSpan(TimeSpan elasped);

        /// <summary>
        /// Log a Debug message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        void LogDebug(string message = "", params object[] args);

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        void LogInfo(string message = "", params object[] args);

        /// <summary>
        /// Log a Warning message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        void LogWarning(string message = "", params object[] args);

        /// <summary>
        /// Log an Error message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        void LogError(string message = "", params object[] args);

        /// <summary>
        /// Log a Fatal message
        /// </summary>
        /// <param name="message">the message to log (may use format template too)</param>
        /// <param name="args">if the message is a format template the objects for the format</param>
        void LogFatal(string message = "", params object[] args);
    }
}
