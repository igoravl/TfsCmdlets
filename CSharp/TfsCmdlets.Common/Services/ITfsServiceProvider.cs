using System;

namespace TfsCmdlets.Services
{
    public interface ITfsServiceProvider
    {
        object GetService(Type serviceType);
        
        object GetClient(Type clientType);
    }
}