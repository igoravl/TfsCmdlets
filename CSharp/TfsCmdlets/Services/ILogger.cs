using System;

namespace TfsCmdlets.Services
{
    public interface ILogger
    {
        void Log(string message, string commandName = null);

        void LogWarn(string message);

        void LogError(string message);

        void LogError(Exception ex);

        void LogParameters();
    }
}