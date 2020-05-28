using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    [Exports(typeof(Connection))]
    internal class ConnectionService : BaseDataService<Connection>
    {
        protected override string ItemName => "Connection";

        protected override IEnumerable<Connection> GetItems(object userState)
        {
            var connectionType = (string) userState;
            var parms = Cmdlet.GetParameters();
            var connection = parms.Get<object>(connectionType);

            VssConnection result = null;

            while (result == null)
                switch (connection)
                {
                    case Connection conn:
                    {
                        result = conn.InnerConnection;
                        break;
                    }
                    case VssConnection conn:
                    {
                        result = conn;
                        break;
                    }
                    case null:
                    {
                        Logger.Log($"Get currently connected {connectionType}");
                        result = ((Connection) CurrentConnections.Get(connectionType)).InnerConnection;
                        break;
                    }
                    case Uri uri:
                    {
                        Logger.Log($"Get {connectionType} referenced by URL '{uri}'");
                        result = new VssConnection(uri, Provider.GetOne<VssClientCredentials>(Cmdlet));
                        break;
                    }
                    case string uri when Uri.IsWellFormedUriString(uri, UriKind.Absolute):
                    {
                        connection = new Uri(uri);
                        continue;
                    }
                    case string name:
                    {
                        connection = VssConnectionHelper.GetOrganizationUrlAsync(name).Result;
                        continue;
                    }
                    default:
                    {
                        throw new Exception($"Invalid or non-existent {connectionType} {connection}.");
                    }
                }

            if (connectionType.Equals("Server"))
            {
                result = (new Connection(result)).ConfigurationServer;
            }

            yield return new Connection(result);
        }
    }
}