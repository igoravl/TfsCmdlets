using System.Collections.Generic;

namespace TfsCmdlets.Cmdlets
{
    public abstract class BaseCmdlet<T>: BaseCmdlet where T: class
    {
        protected T GetOne(ParameterDictionary parameters = null, object userState = null)
        {
            return Provider.GetOne<T>(this, parameters, userState);
        }

        protected IEnumerable<T> GetMany(ParameterDictionary parameters = null, object userState = null)
        {
            return Provider.GetMany<T>(this, parameters, userState);
        }

        protected override void ProcessRecord()
        {
            WriteObject(GetMany(), true);
        }
    }
}