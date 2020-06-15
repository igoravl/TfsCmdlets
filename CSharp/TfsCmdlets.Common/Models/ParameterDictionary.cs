using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Cmdlets
{
    public class ParameterDictionary : Dictionary<string, object>
    {
        public ParameterDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

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
                        .Where(pi => pi.GetCustomAttribute<ParameterAttribute>(true) != null)
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

        public ParameterDictionary(object original, object mergeWith)
            : this(original)
        {
            Merge(new ParameterDictionary(mergeWith));
        }

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