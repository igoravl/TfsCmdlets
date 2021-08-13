using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Represents a collection of cmdlet arguments
    /// </summary>
    public class ParameterDictionary : Dictionary<string, object>
    {
        /// <summary>
        /// Creates an empty dictionary
        /// </summary>
        public ParameterDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Creates a new dictionary, copying the properties of supplied object
        /// </summary>
        public ParameterDictionary(object original)
            : this()
        {
            switch(original)
            {
                case null: return;
                case ParameterDictionary pd: {
                    pd.ForEach(kvp=>Add(kvp.Key, kvp.Value));
                    break;
                }
                case Cmdlet cmdlet: {
                    cmdlet.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Where(pi => pi.GetCustomAttributes<ParameterAttribute>(true).Any())
                        .ForEach(pi =>
                            Add(pi.Name,
                                pi.PropertyType == typeof(SwitchParameter) ?
                                    ((SwitchParameter)pi.GetValue(cmdlet)).ToBool() :
                                    pi.GetValue(cmdlet)));

                    if (cmdlet is PSCmdlet psCmdlet)
                    {
                        Add("ParameterSetName", psCmdlet.ParameterSetName);
                    }
                    break;
                }
                default: {
                    original.GetType().GetProperties(BindingFlags.Instance|BindingFlags.Public).ForEach(prop=>Add(prop.Name, prop.GetValue(original)));
                    break;
                }
            }
        }

        /// <summary>
        /// Creates a new dictionary, copying the properties of supplied object and merging it
        /// with another. 
        /// </summary>
        /// <see cref="ParameterDictionary.Merge(ParameterDictionary)"/>
        public ParameterDictionary(object original, object mergeWith)
            : this(original)
        {
            Merge(new ParameterDictionary(mergeWith));
        }

        /// <summary>
        /// Returns the value of a property. When the property is missing, returns an
        /// optionally supplied default value.
        /// </summary>
        public T Get<T>(string name, T defaultValue = default)
        {
            if (!ContainsKey(name)) return defaultValue;

            var val = this[name];

            if (val is PSObject obj)
            {
                val = obj.BaseObject;
            }

            return (T)val;
        }

        /// <summary>
        /// Merges this instance with another one. Only parameters present in the other collection
        /// that are also missing from this one are merged, i.e conflicting properties are skipped.
        /// </summary>
        public void Merge(ParameterDictionary other)
        {
            if (other == null || other.Count == 0) return;

            foreach (var kvp in other.Where(kvp => !ContainsKey(kvp.Key)))
            {
                Add(kvp.Key, kvp.Value);
            }
        }
    }
}