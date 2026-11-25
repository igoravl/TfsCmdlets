using System.Net;
using System.Reflection;
using System.Security;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ILogger)), Shared]
    public class LoggerImpl : ILogger
    {
        private IPowerShellService PowerShell { get; set; }

        public void Log(string message, string commandName = null)
        {
            if (!PowerShell.IsVerbose) return;

            PowerShell.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{(commandName ?? PowerShell.CurrentCommand)}] {LogRedactor.ScrubText(message)}");
        }

        public void LogError(string message)
        {
            LogError(new Exception(message));
        }

        public void LogError(Exception ex, string errorId = null, ErrorCategory category = ErrorCategory.NotSpecified, object targetObject = null)
        {
            var innerException = ex.InnerException ?? ex;
            var id = errorId ?? innerException.GetType().Name;

            try
            {
                string details;

                try
                {
                    details = ex.ToJsonString(false);
                }
                catch
                {
                    details = ex.ToString();
                }

                Log($"Detailed Error:\n{details})");
            }
            catch
            {
                // Logging must never prevent the error from being reported
            }
            finally
            {
                PowerShell.WriteError(new ErrorRecord(ex, id, category, targetObject));
            }
        }

        public void LogWarn(string message)
        {
            PowerShell.WriteWarning(message);
        }


        public void LogParameters(string commandName, IParameterManager parameters)
        {
            if (!PowerShell.IsVerbose) return;

            var parms = new Dictionary<string, object>();
            var parameterSetName = parameters["ParameterSetName"] ?? "__AllParameterSets";
            var hasParameterSetName = !parameterSetName.Equals("__AllParameterSets");

            foreach (var key in parameters.Keys.Where(key => !key.Equals("ParameterSetName")))
            {
                object value;

                try
                {
                    value = parameters[key];
                }
                catch
                {
                    value = "<unavailable>";
                }

                switch (value)
                {
                    case { } when LogRedactor.IsSensitiveParameter(key):
                        {
                            value = LogRedactor.RedactedValue;
                            break;
                        }
                    case SwitchParameter switchParameter:
                        {
                            value = switchParameter.IsPresent;
                            break;
                        }
                    case Models.Connection conn:
                        {
                            value = $"{{Connection Url={conn.Uri}}}";
                            break;
                        }
                    case VssConnection vssConn:
                        {
                            value = $"{{VssConnection Url={vssConn.Uri}}}";
                            break;
                        }
                    case WebApiTeamProject tp:
                        {
                            value = $"{{Project Name='{tp.Name}' Id={tp.Id}}}";
                            break;
                        }
                    case WebApiTeam t:
                        {
                            value = $"{{Team Name='{t.Name}' Id={t.Id}}}";
                            break;
                        }
                }
                parms[key] = value;
            }

            string[] args;

            try
            {
                args = LogRedactor.Serialize(parms)
                    .Replace("\":", "\" =")
                    .Replace(",\"", "; \"")
                    .Trim('{', '}')
                    .Split(';');
            }
            catch
            {
                args = parms.Keys.Select(k => $"\"{k}\" = <unavailable>").ToArray();
            }

            Log($"Running [{commandName}] with {(hasParameterSetName ? $"parameter set '{parameterSetName}' and " : "")}" +
                $"the following implicit and explicit arguments:\n{string.Join("\n", args)}");
        }

        [ImportingConstructor]
        public LoggerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }
    }

    /// <summary>
    /// Redacts secrets (passwords, tokens, credentials, authorization headers, request bodies)
    /// from values written to the verbose log.
    /// </summary>
    internal static class LogRedactor
    {
        internal const string RedactedValue = "*****";

        private static readonly string[] _sensitiveNamePatterns = new[]
        {
            "*password*", "*passwd*", "*passphrase*", "*token*", "*secret*", "*credential*",
            "*apikey*", "*api-key*", "*api_key*", "*privatekey*", "*private-key*", "*connectionstring*",
            "*cookie*", "Authorization", "Proxy-Authorization", "*-Authorization", "*sessionid*", "*signature*"
        };

        // Parameters whose whole value is redacted even though the name itself isn't generically sensitive
        // (e.g. Invoke-TfsRestApi -Body, which may carry credentials, tokens or other sensitive payloads)
        private static readonly string[] _sensitiveParameterNames = new[] { "Body" };

        private static readonly string[] _sensitiveTypeNamePatterns = new[]
        {
            "*Credential*", "*IssuedToken*", "*AccessToken*", "*SecureString*"
        };

        private static readonly Regex _authSchemeRegex = new Regex(
            @"\b(Bearer|Basic|Negotiate|NTLM)\s+[A-Za-z0-9+/._~\-]{16,}=*",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new RedactingContractResolver(),
            Converters = new List<JsonConverter> { new RedactingConverter() },
            Error = (_, e) => e.ErrorContext.Handled = true
        };

        internal static bool IsSensitiveName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return _sensitiveNamePatterns.Any(p => name.IsLike(p));
        }

        internal static bool IsSensitiveParameter(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return IsSensitiveName(name) ||
                _sensitiveParameterNames.Any(p => p.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        internal static bool IsSensitiveType(Type type)
        {
            if (type == null) return false;

            if (typeof(SecureString).IsAssignableFrom(type) ||
                typeof(PSCredential).IsAssignableFrom(type) ||
                typeof(ICredentials).IsAssignableFrom(type) ||
                typeof(VssCredentials).IsAssignableFrom(type) ||
                typeof(IssuedToken).IsAssignableFrom(type))
            {
                return true;
            }

            for (var t = type; t != null && t != typeof(object); t = t.BaseType)
            {
                var typeName = t.Name;
                if (_sensitiveTypeNamePatterns.Any(p => typeName.IsLike(p))) return true;
            }

            return false;
        }

        internal static string ScrubText(string message)
        {
            if (string.IsNullOrEmpty(message)) return message;

            try
            {
                return _authSchemeRegex.Replace(message, m => $"{m.Groups[1].Value} {RedactedValue}");
            }
            catch
            {
                return message;
            }
        }

        internal static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }

        private class RedactingContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                if (IsSensitiveName(property.PropertyName) || IsSensitiveType(property.PropertyType))
                {
                    property.PropertyType = typeof(string);
                    property.ValueProvider = new RedactedValueProvider();
                    property.Converter = null;
                    property.ShouldSerialize = null;
                    property.Readable = true;
                }

                return property;
            }
        }

        private class RedactedValueProvider : IValueProvider
        {
            public object GetValue(object target) => RedactedValue;

            public void SetValue(object target, object value) { }
        }

        private class RedactingConverter : JsonConverter
        {
            public override bool CanRead => false;

            public override bool CanConvert(Type objectType) =>
                IsSensitiveType(objectType) ||
                typeof(IDictionary).IsAssignableFrom(objectType) ||
                typeof(PSObject).IsAssignableFrom(objectType);

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                switch (value)
                {
                    case null:
                        {
                            writer.WriteNull();
                            break;
                        }
                    case PSObject pso when pso.BaseObject is PSCustomObject:
                        {
                            writer.WriteStartObject();

                            foreach (var prop in pso.Properties)
                            {
                                writer.WritePropertyName(prop.Name);
                                if (IsSensitiveName(prop.Name))
                                {
                                    writer.WriteValue(RedactedValue);
                                    continue;
                                }
                                object propValue;
                                try { propValue = prop.Value; } catch { propValue = null; }
                                serializer.Serialize(writer, propValue);
                            }

                            writer.WriteEndObject();
                            break;
                        }
                    case PSObject pso:
                        {
                            serializer.Serialize(writer, pso.BaseObject);
                            break;
                        }
                    case { } when IsSensitiveType(value.GetType()):
                        {
                            writer.WriteValue(RedactedValue);
                            break;
                        }
                    case IDictionary dict:
                        {
                            writer.WriteStartObject();

                            foreach (DictionaryEntry entry in dict)
                            {
                                var key = Convert.ToString(entry.Key) ?? string.Empty;
                                writer.WritePropertyName(key);

                                if (IsSensitiveName(key))
                                {
                                    writer.WriteValue(RedactedValue);
                                }
                                else
                                {
                                    serializer.Serialize(writer, entry.Value);
                                }
                            }

                            writer.WriteEndObject();
                            break;
                        }
                    default:
                        {
                            writer.WriteValue(RedactedValue);
                            break;
                        }
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotSupportedException();
            }
        }
    }
}
