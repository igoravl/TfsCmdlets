using System.Collections.Generic;

namespace TfsCmdlets.Cmdlets
{
    public abstract class BaseCmdlet<T>: BaseCmdlet where T: class
    {
        private protected T GetItem(ParameterDictionary parameters = null)
        {
            return Provider.GetItem<T>(this, parameters);
        }

        private protected IEnumerable<T> GetItems(ParameterDictionary parameters = null)
        {
            return Provider.GetItems<T>(this, parameters);
        }

        protected override void ProcessRecord()
        {
            WriteObject(GetItems(), true);
        }
    }
}