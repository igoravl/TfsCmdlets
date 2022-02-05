namespace TfsCmdlets.Services
{
    public interface IRegistryService
    {
        object GetValue(string key, string name);

        bool HasValue(string key, string name);
    }
}