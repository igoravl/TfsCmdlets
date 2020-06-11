using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    [Exports(typeof(Connection))]
    internal class ConnectionService : BaseDataService<Connection>
    {
        protected override IEnumerable<Connection> DoGetItems()
        {
            var connectionType = GetParameter<string>("ConnectionType");
            var connection = GetParameter<object>(connectionType);

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
                        result = ((Connection) CurrentConnections.Get(connectionType))?.InnerConnection;

                        if(result == null) yield break;

                        break;
                    }
                    case Uri uri:
                    {
                        Logger.Log($"Get {connectionType} referenced by URL '{uri}'");
                        result = new VssConnection(uri, Provider.GetInstanceOf<VssClientCredentials>(Cmdlet));
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