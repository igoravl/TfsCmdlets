using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface IDataManager
    {
        T GetItem<T>(object overridingParameters = null);

        IEnumerable<T> GetItems<T>(object overridingParameters = null);

        T AddItem<T>(object overridingParameters = null);

        T NewItem<T>(object overridingParameters = null);

        T SetItem<T>(object overridingParameters = null);

        void RemoveItem<T>(object overridingParameters = null);

        T RenameItem<T>(object overridingParameters = null);

        bool TestItem<T>(object overridingParameters = null);

        IEnumerable<T> Invoke<T>(string verb, object overridingParameters = null);

        IEnumerable Invoke(string verb, string noun, object overridingParameters = null);

        Connection GetServer(object overridingParameters = null);
        
        bool TryGetServer(out Models.Connection server, object overridingParameters = null);

        Connection GetCollection(object overridingParameters = null);
        
        bool TryGetCollection(out Models.Connection collection, object overridingParameters = null);

        WebApiTeamProject GetProject(object overridingParameters = null, string contextValue = null);
        
        bool TryGetProject(out WebApiTeamProject project, object overridingParameters = null);

        Models.Team GetTeam(object overridingParameters = null, string contextValue = null);

        bool TryGetTeam(out WebApiTeam team, object overridingParameters = null);

        T GetClient<T>(object overridingParameters = null);

        T GetService<T>(object overridingParameters = null);
    }
}