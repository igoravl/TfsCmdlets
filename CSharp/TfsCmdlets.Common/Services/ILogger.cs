using System;
using System.Management.Automation;

namespace TfsCmdlets.Services
{
    public interface ILogger
    {
        void Log(string message, string commandName = null);

        void LogWarn(string message);

        void LogError(string message);

        void LogError(Exception ex, string errorId = null, ErrorCategory category = ErrorCategory.NotSpecified, object targetObject = null);

        void LogParameters(IParameterManager parameters);
    }
}