using System;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Common.ServiceProvider.Factories
{
    [Exports(typeof(VssConnection))]
    internal class ConnectionService : BaseService<VssConnection>
    {
        public override VssConnection Get(object userState = null)
        {
            var parms = Cmdlet.GetParameters();
            string connectionType;

            switch (userState)
            {
                case null: throw new ArgumentNullException(nameof(userState));
                case string s when s.Equals("Collection", StringComparison.OrdinalIgnoreCase) ||
                                   s.Equals("Server", StringComparison.OrdinalIgnoreCase):
                    {
                        connectionType = s;
                        break;
                    }
                default: throw new ArgumentException(nameof(userState));
            }

            var connection = parms.Get<object>(connectionType);
            VssConnection result = null;

            while (result == null)
                switch (connection)
                {
                    case VssConnection conn:
                        {
                            result = conn;
                            break;
                        }
                    case null:
                        {
                            Logger.Log($"Get currently connected {userState}");
                            result = CurrentConnections.Collection;
                            break;
                        }
                    case Uri uri:
                        {
                            Logger.Log($"Get {userState} referenced by URL '{uri}'");
                            result = new VssConnection(uri, Provider.Get<VssClientCredentials>(Cmdlet));
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
                            throw new Exception($"Invalid or non-existent {userState} {connection}.");
                        }
                }

            if (connectionType.Equals("Server") && result.ParentConnection != null && !result.IsHosted())
            {
                result = result.ParentConnection;
            }

            return result;
        }
    }
}