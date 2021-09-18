using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface ICurrentConnections
    {
        Connection Server { get; set; }

        Models.Connection Collection { get; set; }

        TeamProject Project { get; set; }

        WebApiTeam Team { get; set; }

        T Get<T>(string name);

        object Get(string name);

        void Reset();

        void Set(Connection server);
        void Set(Connection server, Models.Connection collection);
        void Set(Connection server, Models.Connection collection, TeamProject project);
        void Set(Connection server, Models.Connection collection, TeamProject project, WebApiTeam team);
    }
}
