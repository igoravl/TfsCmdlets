using System.Management.Automation;

namespace TfsCmdlets.Models
{
    public abstract class ModelBase<T>: PSObject
    {
        public ModelBase(T item): base(item) { }

        public T InnerObject => (T)base.BaseObject;

        protected void AddProperty(string name, object value)
        {
            this.AddNoteProperty(name, value);
        }
    }
}