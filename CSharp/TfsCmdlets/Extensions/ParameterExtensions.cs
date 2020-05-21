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

            return parms;
        }

        internal static ParameterDictionary GetParameters(this PSCmdlet caller)
        {
            var parms = GetParameters((Cmdlet) caller);

            parms.Add("ParameterSetName", caller.ParameterSetName);

            return parms;
        }
    }

    internal class ParameterDictionary: Dictionary<string, object>
    {
        public T Get<T>(string name, T defaultValue = default)
        {
            return ContainsKey(name) ? (T) this[name] : defaultValue;
        }
    }
}