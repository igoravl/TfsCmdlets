using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.Client;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    [Exports(typeof(Connection))]
    internal class ConnectionService : BaseDataService<Connection>
    {
        protected override IEnumerable<Connection> GetItems(object userState)
        {
            if (userState == null) throw new ArgumentNullException(nameof(userState));

            var connectionType = (string) userState;
            var connection = Parameters.Get<object>(connectionType);

            TfsConnection result = null;

            while (result == null)
                switch (connection)
                {
                    case Connection conn:
                    {
                        result = conn.InnerConnection;
                        break;
                    }
                    case TfsConnection conn:
                    {
                        result = conn;
                        break;
                    }
                    case null:
                    {
                        Logger.Log($"Get currently connected {connectionType}");
                        result = ((Connection) CurrentConnections.Get(connectionType));
                        break;
                    }
                    case Uri uri:
                    {
                        Logger.Log($"Get {connectionType} referenced by URL '{uri}'");

                        if(uri.LocalPath.Equals("/"))
                        {
                            Cmdlet.WriteWarning("Connecting to a Team Foundation Server instance without " +
                                $"specifying a collection name may lead to errors. Instead of using {uri} " +
                                "(without a collection name), consider supplying one in the URL, as in e.g. " +
                                $"{uri}DefaultCollection");
                        }

                        if (connectionType.Equals("Server"))
                            result = new TfsConfigurationServer(uri, Provider.GetOne<VssClientCredentials>(Cmdlet));
                        else
                            result = new TfsTeamProjectCollection(uri, Provider.GetOne<VssClientCredentials>(Cmdlet));
                        break;
                    }
                    case string uri when Uri.IsWellFormedUriString(uri, UriKind.Absolute):
                    {
                        connection = new Uri(uri);
                        continue;
                    }
                    case string _:
                    {
                        throw new NotImplementedException("Connect to server by name is currently not supported.");
                    }
                    default:
                    {
                        throw new Exception($"Invalid or non-existent {connectionType} {connection}.");
                    }
                }

            yield return new Connection(result);
        }

        protected override string ItemName => "Connection";
    }
}