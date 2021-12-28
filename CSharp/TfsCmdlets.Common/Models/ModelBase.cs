using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    public abstract class ModelBase<T>: PSObject
    {
        public ModelBase(T item): base(item) { }

        public T InnerObject => (T)base.BaseObject;

        protected virtual void AddProperty(string name, object value)
        {
            this.AddNoteProperty(name, value);
        }
    }
}