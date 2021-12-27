using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiIdentityRef = Microsoft.VisualStudio.Services.WebApi.IdentityRef;

namespace TfsCmdlets.Extensions
{
    public static class WorkItemExtensions
    {
        public static object GetField(this WebApiWorkItem wi, string referenceName, object defaultValue = null)
           => wi.Fields.ContainsKey(referenceName) ? wi.Fields[referenceName] : defaultValue;

        public static T GetField<T>(this WebApiWorkItem wi, string referenceName, T defaultValue = default)
           => (T)(wi.Fields.ContainsKey(referenceName) ? wi.Fields[referenceName] : defaultValue);

      //   private static T WrapField<T>(object fieldValue)
      //       => (T)WrapField(fieldValue);

      //   private static object WrapField(object fieldValue)
      //   {
      //       switch (fieldValue)
      //       {
      //           case WebApiIdentityRef identityRef:
      //               {
      //                   return (Models.IdentityRefWrapper)identityRef;
      //               }
      //       }

      //       return fieldValue;
      //   }
    }
}