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
        private string _connectionType;

        protected override string ItemName => _connectionType;

        protected override IEnumerable<Connection> GetItems(object userState)
        {
            _connectionType = (string) userState;
            var connection = ItemFilter = Parameters.Get<object>(_connectionType);

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
                        Logger.Log($"Get currently connected {_connectionType}");
                        result = ((Connection) CurrentConnections.Get(_connectionType))?.InnerConnection;

                        if(result == null) yield break;

                        break;
                    }
                    case Uri uri:
                    {
                        Logger.Log($"Get {_connectionType} referenced by URL '{uri}'");
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
                        throw new Exception($"Invalid or non-existent {_connectionType} {connection}.");
                    }
                }

            if (_connectionType.Equals("Server"))
            {
                result = (new Connection(result)).ConfigurationServer;
            }

            yield return new Connection(result);
        }
    }
}