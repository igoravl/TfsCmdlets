using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Extensions
{
    internal static class CollectionExtensions
    {
        internal static VssConnection GetCollection(this PSCmdlet cmdlet)
        {
            var parms = cmdlet.GetParameters();

            var collection = parms.Get<object>("Collection");

            while (true)
            {
                switch (collection)
                {
                    case VssConnection tpc:
                        {
                            cmdlet.ItemIs<VssConnection>(tpc);
                            return tpc;
                        }
                    case null:
                        {
                            cmdlet.Log("Get currently connected collection");
                            return CurrentConnections.Collection;
                        }
                    case Uri uri:
                        {
                            cmdlet.Log($"Get collection referenced by URL '{uri}'");
                            var tpc = new VssConnection(uri, cmdlet.GetCredential());

                            return tpc;
                        }
                    case string uri when Uri.IsWellFormedUriString(uri, UriKind.Absolute):
                        {
                            collection = new Uri(uri);

                            continue;
                        }
                    case string name:
                    {
                        collection = VssConnectionHelper.GetOrganizationUrlAsync(name).Result;

                        continue;
                    }
                    default:
                        {
                            throw new Exception($"Invalid or non-existent team project collection {collection}.");
                        }
                }
            }
        }

        internal static IEnumerable<VssConnection> GetCollections(this PSCmdlet cmdlet)
        {
            var parms = cmdlet.GetParameters();

            if (parms.Get<object>("Collection") is string collectionStr)
            {
                if (!WildcardPattern.ContainsWildcardCharacters(collectionStr))
                {
                    yield return GetCollection(cmdlet);
                }
            }

            // TODO: Enumerate collections
        }

        internal static bool IsHosted(this VssConnection connection)
        {
            return (
                connection.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase) ||
                connection.Uri.Host.EndsWith(".visualstudio.com", StringComparison.OrdinalIgnoreCase));
        }

        internal static VssConnection GetConfigurationServer(this VssConnection connection)
        {
            if (connection.IsHosted())
                return connection;

            return connection.ParentConnection ?? connection;
        }
    }
}