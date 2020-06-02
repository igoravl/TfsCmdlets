using System.Collections.Generic;

namespace TfsCmdlets.Cmdlets
{
    public abstract class BaseCmdlet<T>: BaseCmdlet where T: class
    {
        protected T GetInstanceOf(ParameterDictionary parameters = null, object userState = null)
        {
            return Provider.GetInstanceOf<T>(this, parameters, userState);
        }

        protected IEnumerable<T> GetCollectionOf(ParameterDictionary parameters = null, object userState = null)
        {
            return Provider.GetCollectionOf<T>(this, parameters, userState);
        }

        protected override void ProcessRecord()
        {
            WriteObject(GetCollectionOf(), true);
        }
    }
}