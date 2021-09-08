using System;
using System.Collections;
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
        #region Factory methods
        
        /// <summary>
        /// Creates a new instance of the <see cref="ParameterDictionary"/> class from the specified parameter collection.
        /// </summary>
        public static ParameterDictionary From(object data)
        {
            switch (data)
            {
                case ParameterDictionary pd: return pd;
                case object o: return new ParameterDictionary(o);
                default: return new ParameterDictionary();
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ParameterDictionary"/> class from the specified parameter collection, optionally overriding its values.
        /// </summary>
        public static ParameterDictionary From(object data, object overridingParameters)
        {
            switch (data)
            {
                case null when overridingParameters == null: throw new ArgumentNullException(nameof(data));
                case null: return new ParameterDictionary(overridingParameters);
                case ParameterDictionary pd when overridingParameters == null: return pd;
                default: return new ParameterDictionary(data, overridingParameters);
            }
        }

        #endregion

        /// <summary>
        /// Creates an empty dictionary
        /// </summary>
        private ParameterDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Creates a new dictionary, copying the properties of supplied object
        /// </summary>
        private ParameterDictionary(object original)
            : this()
        {
            switch (original)
            {
                case null: return;
                case ParameterDictionary pd:
                    {
                        pd.ForEach(kvp => Add(kvp.Key, kvp.Value));
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

        /// <summary>
        /// Creates a new dictionary, copying the properties of supplied object and merging it
        /// with another. 
        /// </summary>
        /// <see cref="ParameterDictionary.OverrideWith(IDictionary{string, object})"/>
        private ParameterDictionary(object original, object mergeWith)
            : this(original)
        {
            OverrideWith(ParameterDictionary.From(mergeWith));
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
        /// Overrides this instance with another one. Existing properties are overwritten.
        /// </summary>
        public void OverrideWith(IDictionary<string,object> other)
        {
            if (other == null || other.Count == 0) return;

            foreach (var kvp in other)
            {
                this[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// Merges this instance with another one. Existing properties are skipped.
        /// </summary>
        public void MergeWith(IDictionary<string,object> other)
       {
            if (other == null || other.Count == 0) return;

            foreach (var kvp in other.Where(kvp => !ContainsKey(kvp.Key)))
            {
                Add(kvp.Key, kvp.Value);
            }
        }
    }
}