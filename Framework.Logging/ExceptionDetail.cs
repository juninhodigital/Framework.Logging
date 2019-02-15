using System;

namespace Framework.Logging
{
    /// <summary>
    /// Exception detailed information
    /// </summary>
    public class ExceptionDetail
    {

        internal int Id { get; set; }
        internal string ApplicationName { get; set; }
        internal string ModuleName { get; set; }
        internal LogTypeEnumerator Level { get; set; }
        internal DateTime Date { get; set; }

        internal string EnvServerName { get; set; }
        internal string EnvUser { get; set; }
        internal int EnvProcessId { get; set; }
        internal int EnvThreadId { get; set; }

        internal int? ErrorCode { get; set; }
        internal string ErrorSource { get; set; }
        internal string ErrorMessage { get; set; }
        internal string InnerExceptionMessage { get; set; }

    }
}
