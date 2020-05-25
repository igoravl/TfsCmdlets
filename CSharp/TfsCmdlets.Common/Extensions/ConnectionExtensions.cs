using System;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Extensions
{
    internal static class ConnectionExtensions
    {
        //        internal static IEnumerable<VssConnection> GetCollections(this PSCmdlet cmdlet)
        //        {
        //            var parms = cmdlet.GetParameters();
        //            var collection = parms.Get<object>("Collection");

        //            switch (collection)
        //            {
        //                case null:
        //                case string collectionStr when !WildcardPattern.ContainsWildcardCharacters(collectionStr):
        //                {
        //                    yield return GetCollection(cmdlet);
        //                    break;
        //                }
        //                default:
        //                {
        //                    // TODO
        //                    break;
        //                }
        //            }
        //        }

        //        internal static VssConnection Get(PSCmdlet cmdlet, string argument)
        //            {
        //                var parms = cmdlet.GetParameters();

        //                var argumentValue = parms.Get<object>(argument);

        //                while (true)
        //                {
        //                    switch (argumentValue)
        //                    {
        //                        case VssConnection tpc:
        //                            {
        //                                cmdlet.ItemIs<VssConnection>(tpc);
        //                                return tpc;
        //                            }
        //                        case null:
        //                            {
        //                                cmdlet.Log($"Get currently connected {argument}");
        //                                return CurrentConnections.Collection;
        //                            }
        //                        case Uri uri:
        //                            {
        //                                cmdlet.Log($"Get {argument} referenced by URL '{uri}'");
        //                                var tpc = new VssConnection(uri, cmdlet.GetCredential());

        //                                return tpc;
        //                            }
        //                        case string uri when Uri.IsWellFormedUriString(uri, UriKind.Absolute):
        //                            {
        //                                argumentValue = new Uri(uri);

        //                                continue;
        //                            }
        //                        case string name:
        //                            {
        //                                argumentValue = VssConnectionHelper.GetOrganizationUrlAsync(name).Result;

        //                                continue;
        //                            }
        //                        default:
        //                            {
        //                                throw new Exception($"Invalid or non-existent {argument} {argumentValue}.");
        //                            }
        //                    }
        //                }
        //            }

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