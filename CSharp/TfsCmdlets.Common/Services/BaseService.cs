using System.Management.Automation;

namespace TfsCmdlets.Services
{
    internal abstract class BaseService<T>: IService, IService<T>
    {
        private ILogService _logger;

        public ICmdletServiceProvider Provider { get; set; }

        public Cmdlet Cmdlet { get; set; }

        public abstract T Get(object userState = null);

        protected ILogService Logger => _logger ??= Provider.GetService<ILogService>(Cmdlet).Get();
    }
}
