using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Util
{
    /// <summary>
    /// Supports the creation of lazy-loaded ScriptProperty properties
    /// </summary>
    public static class LazyProperty
    {
        /// <summary>
        /// Gets the value of the specified lazy-loaded property
        /// </summary>
        /// <param name="obj">The source PSObject that owns the property</param>
        /// <param name="property">The property name</param>
        /// <param name="sb">The scriptblock that is evaluated when the property 
        ///     needs to be loaded. It must return the value to be assigned to the property.</param>
        /// <returns>The value of the property</returns>
        public static object Get(PSObject obj, string property, ScriptBlock sb)
        {
            var propertyBag = obj.Members?.Match("__PropertyBag").FirstOrDefault()?.Value as Dictionary<string, object>;

            if (propertyBag == null)
            {
                propertyBag = new Dictionary<string, object>();
                obj.AddNoteProperty("__PropertyBag", propertyBag);
            }

            if (propertyBag.ContainsKey(property))
            {
                return propertyBag[property];
            }

            object value;

            try
            {
                value = propertyBag[property] = sb.InvokeWithContext(null, new List<PSVariable>(){
                    new PSVariable("_", obj)});
            }
            catch(Exception ex)
            {
                value = ex;
            }

            return value;
        }
    }
}