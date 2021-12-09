using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using TfsCmdlets.Util;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IParameterManager)), Shared]
    internal class ParameterManagerImpl : IParameterManager
    {
        private Cmdlet _cmdlet;
        private IDictionary<string, object> _innerDictionary;
        private readonly Stack<Tuple<Cmdlet, IDictionary<string, object>>> _contextStack = new Stack<Tuple<Cmdlet, IDictionary<string, object>>>();

        /// <summary>
        /// Creates a new dictionary, copying the properties of supplied object
        /// </summary>
        public void Initialize(Cmdlet cmdlet)
        {
            _innerDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            _contextStack.Clear();

            var props = cmdlet
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(pi => pi.GetCustomAttribute<ParameterAttribute>(true) != null);

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(cmdlet);
                value = value is PSObject psObject ? psObject.BaseObject : value;

                if (value != null) _innerDictionary.Add(name, value);
            }

            if (cmdlet is PSCmdlet psCmdlet)
            {
                _innerDictionary.Add("ParameterSetName", psCmdlet.ParameterSetName);
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

            if (!HasParameter(name)) return defaultValue;

            var val = _innerDictionary[name] switch
            {
                PSObject obj => obj.BaseObject,
                SwitchParameter sw => sw.ToBool(),
                _ => _innerDictionary[name]
            };

            return (T)val;
        }

        public object this[string name]
        {
            get => _innerDictionary[name];
            set => _innerDictionary[name] = value;
        }

        public void PushContext(object overridingParameters)
        {
            _contextStack.Push(new Tuple<Cmdlet, IDictionary<string, object>>(_cmdlet, _innerDictionary));
            _innerDictionary = Override(overridingParameters);
        }

        public void PushContext(Cmdlet cmdlet, object overridingParameters)
        {
            _contextStack.Push(new Tuple<Cmdlet, IDictionary<string, object>>(cmdlet, _innerDictionary));
            _innerDictionary = Override(overridingParameters);
        }

        public void PopContext()
        {
            if (_contextStack.Count > 0)
            {
                var context = _contextStack.Pop();
                _cmdlet = context.Item1;
                _innerDictionary = context.Item2;
                return;
            }

            _cmdlet = null;
            _innerDictionary = null;
        }

        private IDictionary<string, object> Override(object overridingParameters)
        {
            if (overridingParameters == null) return _innerDictionary;

            var overridden = new Dictionary<string, object>(_innerDictionary, StringComparer.OrdinalIgnoreCase);

            switch (overridingParameters)
            {
                case IEnumerable<KeyValuePair<string, object>> dict:
                    {
                        foreach (var kvp in dict)
                        {
                            var value = kvp.Value is PSObject psObject ? psObject.BaseObject : kvp.Value;
                            if (value != null) overridden[kvp.Key] = value;
                        }
                        break;
                    }
                case IDictionary dict:
                    foreach (var key in dict.Keys)
                    {
                        var value = dict[key] is PSObject psObject ? psObject.BaseObject : dict[key];
                        if (value != null) overridden[key.ToString()] = value;
                    }
                    break;
                default:
                    {
                        foreach (var prop in overridingParameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            var name = prop.Name;
                            var value = prop.GetValue(overridingParameters);
                            value = value is PSObject psObject ? psObject.BaseObject : value;
                            if (value != null) overridden[name] = value;
                        }
                        break;
                    }
            }

            return overridden;
        }

        public void Reset()
        {
            _cmdlet = null;
            _innerDictionary = null;
            _contextStack.Clear();
        }

        private void CheckIsInitialized() => ErrorUtil.ThrowIfNull(_cmdlet, "ParameterManager is not initialized");

        private IPowerShellService PowerShell { get; set; }

        public void Remove(string name)
            => _innerDictionary.Remove(name);

        public bool HasParameter(string parameter)
            => _innerDictionary.ContainsKey(parameter);

        public IEnumerable<string> Keys => _innerDictionary.Keys;

        [ImportingConstructor]
        public ParameterManagerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }
    }
}