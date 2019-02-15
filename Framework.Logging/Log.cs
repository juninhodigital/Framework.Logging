using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Framework.Core;
using log4net;

namespace Framework.Logging
{
    /// <summary>
    /// Provides interaction with Windows event logs and logs stored on the disk, and database log storage as well
    /// It depends on the log4net component that comes from Nugget (Install-Package log4net)
    /// </summary>
    public class Log
    {
        #region| Fields |

        private readonly ILog LogFile;
        private readonly ILog logViewer;
        private readonly string classNameIdentifier;

        #endregion

        #region| Constructor |

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="classNameIdentifier"></param>
        public Log(string classNameIdentifier)
        {
            // Automatically configures the log4net system based on the application's configuration settings.
            log4net.Config.XmlConfigurator.Configure();

            this.classNameIdentifier = classNameIdentifier;

            this.LogFile   = LogManager.GetLogger("LogInFile");
            this.logViewer = LogManager.GetLogger("LogInEventViewer");
        } 

        #endregion

        #region| Methods |

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logType">LogTypeEnumerator</param>
        /// <param name="message">Business validation message or error, or other additional technical information</param>
        /// <param name="logTo"></param>
        /// <param name="exception"></param>
        /// <param name="printToOutput"></param>
        public void Add(LogTypeEnumerator logType, string message, LogToSourceEnumerator logTo = LogToSourceEnumerator.Both, Exception exception = null, bool printToOutput = false)
        {
            if (printToOutput)
            {
                Console.WriteLine(message);
            }

            if (exception != null)
            {
                var logDetails = GetExceptionDetails(exception);

                message += GetDetails(logDetails);
            }

            #region| OFF .

            if (logType == LogTypeEnumerator.OFF)
            {
                return;
            }

            #endregion

            #region| FATAL .

            if (logType == LogTypeEnumerator.FATAL)
            {
                if (logTo == LogToSourceEnumerator.Both)
                {
                    LogFile.Fatal(message);
                    logViewer.Fatal(message);
                }
                else
                {
                    if (logTo == LogToSourceEnumerator.File)
                    {
                        LogFile.Fatal(message);
                    }
                    else if (logTo == LogToSourceEnumerator.EventViewer)
                    {
                        logViewer.Fatal(message);
                    }
                }
            }
            #endregion

            #region| ERROR .

            if (logType == LogTypeEnumerator.ERROR)
            {
                if (logTo == LogToSourceEnumerator.Both)
                {
                    LogFile.Error(message);
                    logViewer.Error(message);
                }
                else
                {
                    if (logTo == LogToSourceEnumerator.File)
                    {
                        LogFile.Error(message);
                    }
                    else if (logTo == LogToSourceEnumerator.EventViewer)
                    {
                        logViewer.Error(message);
                    }
                }
            }
            #endregion

            #region| WARN .

            if (logType == LogTypeEnumerator.WARN)
            {
                if (logTo == LogToSourceEnumerator.Both)
                {
                    LogFile.Warn(message);
                    logViewer.Warn(message);
                }
                else
                {
                    if (logTo == LogToSourceEnumerator.File)
                    {
                        LogFile.Warn(message);
                    }
                    else if (logTo == LogToSourceEnumerator.EventViewer)
                    {
                        logViewer.Warn(message);
                    }
                }
            }
            #endregion

            #region| INFO .

            if (logType == LogTypeEnumerator.INFO)
            {
                if (logTo == LogToSourceEnumerator.Both)
                {
                    LogFile.Info(message);
                    logViewer.Info(message);
                }
                else
                {
                    if (logTo == LogToSourceEnumerator.File)
                    {
                        LogFile.Info(message);
                    }
                    else if (logTo == LogToSourceEnumerator.EventViewer)
                    {
                        logViewer.Info(message);
                    }
                }
            }
            #endregion

            #region| DEBUG .

            if (logType == LogTypeEnumerator.DEBUG)
            {
                if (logTo == LogToSourceEnumerator.Both)
                {
                    LogFile.Debug(message);
                    logViewer.Debug(message);
                }
                else
                {
                    if (logTo == LogToSourceEnumerator.File)
                    {
                        LogFile.Debug(message);
                    }
                    else if (logTo == LogToSourceEnumerator.EventViewer)
                    {
                        logViewer.Debug(message);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Logs that the caller method has started properly
        /// </summary>
        public void NotifyStarted([CallerMemberName] string methodName = null)
        {
            Add(LogTypeEnumerator.INFO, $"{classNameIdentifier}.{methodName} - Started");
        }

        /// <summary>
        /// Logs that the caller method has finished properly
        /// </summary>
        public void NotifyFinished([CallerMemberName] string methodName = null)
        {
            Add(LogTypeEnumerator.INFO, $"{classNameIdentifier}.{methodName} - Finished");
        }

        /// <summary>
        /// Get technical details about the exception
        /// </summary>
        /// <param name="exception">thrownedException</param>
        /// <returns></returns>
        private ExceptionDetail GetExceptionDetails(Exception @exception)
        {
            var output = new ExceptionDetail();

            output.EnvServerName = Environment.MachineName;
            output.EnvUser       = Environment.UserName;
            output.EnvProcessId  = System.Diagnostics.Process.GetCurrentProcess().Id;
            output.EnvThreadId   = System.Threading.Thread.CurrentThread.ManagedThreadId;
            output.Date          = DateTime.Now;

            #region| Performance Issue .

            /*
            var callerstacktrace = new System.Diagnostics.StackTrace(3, false);

            if (callerstacktrace != null)
            {
                var callerFrame = callerstacktrace.GetFrame(0) ?? callerstacktrace.GetFrame(0);

                if (callerFrame != null)
                {
                    var callermethod = callerFrame.GetMethod();

                    if (callermethod != null)
                    {
                        var callertype = callermethod.DeclaringType;

                        output.EnvMethodName = callermethod.ToString();

                        if (callertype != null)
                        {
                            output.EnvNamespace = callertype.Namespace;
                            output.EnvClassName = callertype.Name;
                            output.EnvAssemblyName = callertype.Assembly.GetName().FullName;
                        }
                    }
                }
            }

                */
            #endregion

            if (exception != null)
            {
                output.ErrorCode    = exception.HResult;
                output.ErrorSource  = exception.Source;
                output.ErrorMessage = exception.Message;

                if (exception.InnerException != null)
                {
                    output.InnerExceptionMessage = exception.InnerException.Message;
                }
            }

            return output;
        }

        /// <summary>
        /// Returns a friendly string content with detailed information about the exception
        /// </summary>
        /// <param name="logDetail"></param>
        /// <returns></returns>
        private string GetDetails(ExceptionDetail logDetail)
        {
            var output = $"| ProcessId:{logDetail.EnvProcessId}  Environment:{ logDetail.EnvServerName}  User:{logDetail.EnvUser}  Error:{logDetail.ErrorMessage}";

            if (logDetail.InnerExceptionMessage.IsNotNull())
            {
                output += $"  Inner: {logDetail.InnerExceptionMessage}";
            }

            if (logDetail.ErrorCode.HasValue)
            {
                output += $"  ErrorCode: {logDetail.ErrorCode.Value}";
            }

            return output.ToString();
        }

        /// <summary>
        /// Clear all entries in the Event Viewer
        /// </summary>
        public void ClearAllEvents()
        {
            foreach (var oEvent in EventLog.GetEventLogs())
            {
                oEvent.Clear();
                oEvent.Dispose();

            }
        }       

        #endregion
    }
}
