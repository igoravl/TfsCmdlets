using System.Management.Automation;

namespace TfsCmdlets.Services
{
    public interface IParameterManager //: IDictionary<string,object>
    {
        void Initialize(Cmdlet cmdlet);

        T Get<T>(string name, T defaultValue = default);

        T GetRaw<T>(string name, T defaultValue = default);

        bool HasParameter(string parameter);

        IDisposable PushContext(object overridingParameters, string contextName);

        void PopContext(string contextName = null);

        void Reset();

        IEnumerable<string> Keys { get; }

        object this[string key] { get; set; }
    }
}