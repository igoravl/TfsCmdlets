using System.Management.Automation;
using System.Reflection;
using TfsCmdlets.Util;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IParameterManager)), Shared]
    public class ParameterManagerImpl : IParameterManager, IDisposable
    {
        private Cmdlet _cmdlet;
        private IDictionary<string, object> _parameterValues;
        private IList<string> _boundParameters;
        private readonly Stack<(string, Cmdlet, IDictionary<string, object>, IList<string>)> _contextStack = new();

        /// <summary>
        /// Creates a new dictionary, copying the properties of supplied object
        /// </summary>
        public void Initialize(Cmdlet cmdlet)
        {
            _parameterValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            _boundParameters = new List<string>(PowerShell.GetBoundParameters().Keys ?? Enumerable.Empty<string>());

            _contextStack.Clear();

            var props = cmdlet
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(pi => pi.GetCustomAttributes<ParameterAttribute>(true).Any()).ToList();

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(cmdlet);
                value = value is PSObject psObject ? psObject.BaseObject : value;

                if (value != null) _parameterValues.Add(name, value);
            }

            if (cmdlet is PSCmdlet psCmdlet)
            {
                _parameterValues.Add("ParameterSetName", psCmdlet.ParameterSetName);
            }

            _cmdlet = cmdlet;
        }

        /// <summary>
        /// Returns the value of a property. When the property is missing, returns an
        /// optionally supplied default value.
        /// </summary>
        public T Get<T>(string name, T defaultValue = default)
        {
            CheckIsInitialized();

            if (!_parameterValues.ContainsKey(name)) return defaultValue;

            var val = _parameterValues[name] switch
            {
                PSObject obj => obj.BaseObject,
                IEnumerable<PSObject> objs => objs.Select(o => o.BaseObject).ToList(),
                SwitchParameter sw => sw.ToBool(),
                _ => _parameterValues[name]
            };

            return (val is T tVal)? tVal : defaultValue;
        }

        /// <summary>
        /// Returns the raw value of a property. When the property is missing, returns an
        /// optionally supplied default value.
        /// </summary>
        public T GetRaw<T>(string name, T defaultValue = default)
        {
            CheckIsInitialized();

            if (!_parameterValues.ContainsKey(name)) return defaultValue;

            return (T)_parameterValues[name];
        }

        public object this[string name]
        {
            get => _parameterValues[name];
            set => throw new InvalidOperationException("Cannot set parameter values directly. Use Override() method instead.");
        }

        public IDisposable PushContext(object overridingParameters, string contextName)
        {
            _contextStack.Push(new(contextName, _cmdlet, _parameterValues, _boundParameters));
            (_parameterValues, _boundParameters) = Override(overridingParameters);

            return this;
        }

        public void PopContext(string contextName = null)
        {
            if (_contextStack.Count > 0)
            {
                (var _, _cmdlet, _parameterValues, _boundParameters) = _contextStack.Pop();
                return;
            }

            _cmdlet = null;
            _parameterValues = null;
            _boundParameters = null;
        }

        private (IDictionary<string, object>, IList<string>) Override(object overridingParameters)
        {
            if (overridingParameters == null) return (_parameterValues, _boundParameters);

            var overridden = new Dictionary<string, object>(_parameterValues, StringComparer.OrdinalIgnoreCase);
            var boundParams = new List<string>(_boundParameters);

            switch (overridingParameters)
            {
                case IEnumerable<KeyValuePair<string, object>> dict:
                    {
                        foreach (var kvp in dict)
                        {
                            var value = kvp.Value is PSObject psObject ? psObject.BaseObject : kvp.Value;

                            if (value != null)
                            {
                                overridden[kvp.Key] = value;
                                if (!boundParams.Contains(kvp.Key)) boundParams.Add(kvp.Key);
                            }
                        }
                        break;
                    }
                case IDictionary dict:
                    foreach (var key in dict.Keys)
                    {
                        var value = dict[key] is PSObject psObject ? psObject.BaseObject : dict[key];

                        if (value != null)
                        {
                            overridden[key.ToString()] = value;
                            if (!boundParams.Contains(key.ToString())) boundParams.Add(key.ToString());
                        }
                    }
                    break;
                default:
                    {
                        foreach (var prop in overridingParameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            var name = prop.Name;
                            var value = prop.GetValue(overridingParameters);
                            value = value is PSObject psObject ? psObject.BaseObject : value;

                            if (value != null)
                            {
                                overridden[name] = value;
                                if (!boundParams.Contains(name)) boundParams.Add(name);
                            }
                        }
                        break;
                    }
            }

            return (overridden, boundParams);
        }

        public void Reset()
        {
            _cmdlet = null;
            _parameterValues = null;
            _boundParameters = null;

            _contextStack.Clear();
        }

        private void CheckIsInitialized() => ErrorUtil.ThrowIfNull(_cmdlet, "ParameterManager is not initialized");

        private IPowerShellService PowerShell { get; set; }

        public void Remove(string name)
            => _parameterValues.Remove(name);

        public bool HasParameter(string parameter)
            => _boundParameters.Contains(parameter, StringComparer.OrdinalIgnoreCase);

        public IEnumerable<string> Keys => _parameterValues.Keys;

        public Stack<(string, Cmdlet, IDictionary<string, object>, IList<string>)> ContextStack => _contextStack;

        [ImportingConstructor]
        public ParameterManagerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }

        public void Dispose()
        {
            PopContext();
        }
    }
}