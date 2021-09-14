using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets
{
    public interface ICurrentConnections
    {
        ConfigurationServer Server { get; set; }

        Models.TeamProjectCollection Collection { get; set; }

        TeamProject Project { get; set; }

        WebApiTeam Team { get; set; }

        T Get<T>(string name);

        object Get(string name);

        void Reset();

        void Set(ConfigurationServer server);
        void Set(ConfigurationServer server, Models.TeamProjectCollection collection);
        void Set(ConfigurationServer server, Models.TeamProjectCollection collection, TeamProject project);
        void Set(ConfigurationServer server, Models.TeamProjectCollection collection, TeamProject project, WebApiTeam team);
    }
}
