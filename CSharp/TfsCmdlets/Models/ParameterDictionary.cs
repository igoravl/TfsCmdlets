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
    public class ParameterDictionary : Dictionary<string, object>
    {
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
            switch (original)
            {
                case null: return;
                case IReadOnlyDictionary<string, object> dict:
                    {
                        dict.ForEach(kvp => Add(kvp.Key, kvp.Value));
                        break;
                    }
                case Cmdlet cmdlet:
                    {
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
                default:
                    {
                        original.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ForEach(prop =>
                                Add(prop.Name, prop.GetValue(original)));
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
                this[parm.Key] = parm.Value is PSObject psObject ? psObject.BaseObject : parm.Value;
            }
        }

        /// <summary>
        /// Returns the value of a property. When the property is missing, returns an
        /// optionally supplied default value.
        /// </summary>
        public T Get<T>(string name, T defaultValue = default)
        {
            if (!ContainsKey(name)) return defaultValue;

            var val = this[name];

            switch (val)
            {
                case PSObject obj:
                    {
                        val = obj.BaseObject;
                        break;
                    }
                case SwitchParameter sw:
                    {
                        val = sw.ToBool();
                        break;
                    }
            }

            return (T)val;
        }
    }
}