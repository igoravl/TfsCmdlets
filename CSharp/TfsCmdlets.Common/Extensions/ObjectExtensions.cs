using System;
using System.Linq;
using System.Reflection;

namespace TfsCmdlets.Extensions
{
    public static class ObjectExtensions
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
            var field = FindMember<FieldInfo>(self, fieldName);
            return (T)field.GetValue(self);
        }

        public static void SetHiddenField(this object self, string fieldName, object value)
        {
            var field = FindMember<FieldInfo>(self, fieldName);
            field.SetValue(self, value);
        }

        public static object CallHiddenMethod(this object self, string methodName, params object[] parameters)
        {
            var method = FindMember<MethodInfo>(self, methodName);
            return method.Invoke(self, parameters);
        }

        public static object ToJsonString(this object self)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(self, Newtonsoft.Json.Formatting.None);
        }

        private static T FindMember<T>(object self, string memberName)
            where T : MemberInfo
        {
            var rootType = self.GetType();

            while (rootType != null)
            {
                var member = rootType
                    .GetMember(memberName, MemberTypes.All, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    .FirstOrDefault(mi => mi is T);

                if (member != null)
                {
                    return (T)member;
                }

                rootType = rootType.BaseType;
            }

            throw new ArgumentException($"Member {memberName} not found");
        }
    }
}