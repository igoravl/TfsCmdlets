using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents a collection of cmdlet arguments
    /// </summary>
    public class ParameterDictionary: IEnumerable<KeyValuePair<string,object>>
    {
        private readonly Dictionary<string, object> _innerDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public ParameterDictionary()
        {
        }

        /// <summary>
        /// Creates a new dictionary, copying the properties of supplied object
        /// </summary>
        public ParameterDictionary(object original)
            : this()
        {
            switch (original)
            {
                case null: return;
                case IEnumerable<KeyValuePair<string, object>> dict:
                    {
                        dict.ForEach(kvp => _innerDictionary.Add(kvp.Key, kvp.Value));
                        break;
                    }
                case Cmdlet cmdlet:
                    {
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
                        break;
                    }
                default:
                    {
                        original.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ForEach(prop =>
                            _innerDictionary.Add(prop.Name, prop.GetValue(original)));
                        break;
                    }
            }
        }

        public ParameterDictionary(object original, object overridingParameters)
            : this(original)
        {
            if (overridingParameters == null) return;

            var newParms = new ParameterDictionary(overridingParameters);

            foreach (var parm in newParms)
            {
                _innerDictionary[parm.Key] = parm.Value;
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

        public ParameterDictionary Override(object overridingParameters)
            => new ParameterDictionary(this, overridingParameters);

        public void Remove(string name)
            => _innerDictionary.Remove(name);

        public bool HasParameter(string parameter)
            => _innerDictionary.ContainsKey(parameter);

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
            => _innerDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable) _innerDictionary).GetEnumerator();
    }
}