using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface ICurrentConnections
    {
        ServerConnection Server { get; set; }

        Models.TpcConnection Collection { get; set; }

        TeamProject Project { get; set; }

        WebApiTeam Team { get; set; }

        T Get<T>(string name);

        object Get(string name);

        void Reset();

        void Set(ServerConnection server);
        void Set(ServerConnection server, Models.TpcConnection collection);
        void Set(ServerConnection server, Models.TpcConnection collection, TeamProject project);
        void Set(ServerConnection server, Models.TpcConnection collection, TeamProject project, WebApiTeam team);
    }
}
