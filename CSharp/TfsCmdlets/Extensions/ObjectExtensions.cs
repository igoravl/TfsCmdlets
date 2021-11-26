using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.Framework.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Util;

namespace TfsCmdlets.Extensions
{
    internal static class ObjectExtensions
    {
        public static void CopyFrom(this object self, object parent)
        {
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }

        public static T GetHiddenField<T>(this object self, string fieldName)
        {
            var field = self.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return (T) field.GetValue(self);
        }

        public static void SetHiddenField(this object self, string fieldName, object value)
        {
            var field = self.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            field.SetValue(self, value);
        }

        public static object CallHiddenMethod(this object self, string methodName, params object[] parameters)
        {
            var method = self.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return method.Invoke(self, parameters);
        }
    }
}