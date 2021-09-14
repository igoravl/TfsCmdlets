using System;

namespace TfsCmdlets.Services
{
    public interface IFactory
    {
        void SetContext(InjectAttribute injectAttribute);

        object Create();
    }
}