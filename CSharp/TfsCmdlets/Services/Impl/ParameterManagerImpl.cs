using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IParameterManager)), Shared]
    internal class ParameterManagerImpl : IParameterManager
    {
        private Dictionary<string, object> _innerDictionary;
        private readonly Stack<Dictionary<string, object>> _contextStack = new Stack<Dictionary<string, object>>();

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
        }

        /// <summary>
        /// Returns the value of a property. When the property is missing, returns an
        /// optionally supplied default value.
        /// </summary>
        public T Get<T>(string name, T defaultValue = default)
        {
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

        public void Remove(string name)
            => _innerDictionary.Remove(name);

        public bool HasParameter(string parameter)
            => _innerDictionary.ContainsKey(parameter);

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
            => _innerDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable)_innerDictionary).GetEnumerator();

        public void PushContext(object overridingParameters)
        {
            _contextStack.Push(_innerDictionary);
            _innerDictionary = Override(overridingParameters);
        }

        public void PopContext()
        {
            _innerDictionary = _contextStack.Pop();
        }

        private Dictionary<string, object> Override(object overridingParameters)
        {
            var overridden = new Dictionary<string, object>(this, StringComparer.OrdinalIgnoreCase);

            if (overridingParameters == null) return overridden;

            var props = overridingParameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(overridingParameters);
                value = value is PSObject psObject ? psObject.BaseObject : value;

                if (value != null) overridden[name] = value;
            }

            return overridden;
        }

        private IPowerShellService PowerShell { get; set; }

        [ImportingConstructor]
        public ParameterManagerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }
    }
}