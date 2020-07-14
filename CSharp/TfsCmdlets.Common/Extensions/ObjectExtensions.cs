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
    }
}