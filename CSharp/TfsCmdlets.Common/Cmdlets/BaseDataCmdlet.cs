using System.Collections.Generic;

namespace TfsCmdlets.Cmdlets
{
    public abstract class BaseCmdlet<T>: BaseCmdlet where T: class
    {
        private protected T GetInstanceOf(ParameterDictionary parameters = null)
        {
            return Provider.GetInstanceOf<T>(this, parameters);
        }

        private protected IEnumerable<T> GetCollectionOf(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionOf<T>(this, parameters);
        }

        protected override void ProcessRecord()
        {
            WriteObject(GetCollectionOf(), true);
        }
    }
}