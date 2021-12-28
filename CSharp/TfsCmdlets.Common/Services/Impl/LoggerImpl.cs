using System.Management.Automation;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ILogger)), Shared]
    public class LoggerImpl : ILogger
    {
        private IPowerShellService PowerShell { get; set; }

        private readonly string[] _hiddenParameters = new[] { "Password", "PersonalAccessToken" };

        public void Log(string message, string commandName = null)
        {
            if (!PowerShell.IsVerbose) return;

            PowerShell.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{(commandName ?? PowerShell.CurrentCommand)}] {message}");
        }

        public void LogError(string message)
        {
            LogError(new Exception(message));
        }

        public void LogError(Exception ex, string errorId = null, ErrorCategory category = ErrorCategory.NotSpecified, object targetObject = null)
        {
            var innerException = ex.InnerException ?? ex;
            var id = errorId ?? innerException.GetType().Name;

            PowerShell.WriteError(new ErrorRecord(ex, id, category, targetObject));
        }

        public void LogWarn(string message)
        {
            PowerShell.WriteWarning(message);
        }


        public void LogParameters(IParameterManager parameters)
        {
            if (!PowerShell.IsVerbose) return;

            var parms = new Dictionary<string, object>();
            var parameterSetName = parameters["ParameterSetName"]?? "__AllParameterSets";
            var hasParameterSetName = !parameterSetName.Equals("__AllParameterSets");

            foreach (var key in parameters.Keys.Where(key => !key.Equals("ParameterSetName")))
            {
                var value = parameters[key];

                switch (value)
                {
                    case { } when (_hiddenParameters.Any(p => p.Equals(key, StringComparison.OrdinalIgnoreCase))):
                        {
                            value = "*****";
                            break;
                        }
                    case SwitchParameter switchParameter:
                        {
                            value = switchParameter.IsPresent;
                            break;
                        }
                    case Models.Connection conn: {
                        value = $"{{Connection Url={conn.Uri}}}";
                        break;
                    }
                    case WebApiTeamProject tp: {
                        value = $"{{Project Name='{tp.Name}' Id={tp.Id}}}";
                        break;
                    }
                    case WebApiTeam t: {
                        value = $"{{Team Name='{t.Name}' Id={t.Id}}}";
                        break;
                    }
                }
                parms[key] = value;
            }

            var args = Newtonsoft.Json.Linq.JObject.FromObject(parms)
                    .ToString(Newtonsoft.Json.Formatting.None)
                    .Replace("\":", "\" = ")
                    .Replace(",\"", "; \"");

            Log($"<START> Running cmdlet with {(hasParameterSetName ? $"parameter set '{parameterSetName}' and " : "")}the following implicit and explicit arguments:");
            Log(args);

        }

        [ImportingConstructor]
        public LoggerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }
    }
}
