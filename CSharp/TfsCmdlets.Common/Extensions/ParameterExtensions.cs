using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Extensions
{
    internal static class ParametersHelper
    {
        internal static ParameterDictionary GetParameters(this Cmdlet cmdlet)
        {
            var parms = new ParameterDictionary();
            
            cmdlet
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(pi => pi.GetCustomAttribute<ParameterAttribute>(true) != null)
                .ForEach(pi => 
                    parms.Add(
                        pi.Name, 
                        pi.PropertyType == typeof(SwitchParameter)? 
                            ((SwitchParameter) pi.GetValue(cmdlet)).ToBool() : 
                            pi.GetValue(cmdlet)));

            if (cmdlet is PSCmdlet psCmdlet)
            {
                parms.Add("ParameterSetName", psCmdlet.ParameterSetName);
            }

            return parms;
        }
    }

    internal class ParameterDictionary: Dictionary<string, object>
    {
        public ParameterDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public T Get<T>(string name, T defaultValue = default)
        {
            if(!ContainsKey(name)) return defaultValue;

            object val = this[name];

            if(val is PSObject obj)
            {
                val = obj.BaseObject;
            }

            return (T) val;
        }
    }
}