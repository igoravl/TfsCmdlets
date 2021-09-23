using System;
using System.Composition;
using System.IO;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Xml.Linq;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.Admin
{
    [Command]
    internal class GetConfigurationConnectionString : CommandBase
    {
        private IDataManager Data { get; }

        public override object InvokeCommand(ParameterDictionary parameters)
        {
            if (parameters.HasParameter("Session"))
            {
                throw new NotImplementedException("Remote sessions are currently not supported");
            }

            if (!parameters.Get<string>("ComputerName").Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException("Remote computers are currently not supported");
            }

            var installationPath = Data.GetItem<TfsInstallationPath>(parameters.Override(new {
                Component = TfsComponent.ApplicationTier
            }));

            var webConfigPath = Path.Combine(installationPath.InnerObject, "Web Services/Web.config");
            var webConfig = XDocument.Load(webConfigPath);

            var connString = webConfig
                .Element("configuration")
                .Element("appSettings")
                .Descendants("add")
                .Where(el => el.Attribute("key").Value == "applicationDatabase")
                .Select(el => el.Attribute("value").Value)
                .FirstOrDefault();

            return connString;
        }

        [ImportingConstructor]
        public GetConfigurationConnectionString(IDataManager data, ILogger logger) : base(logger)
        {
            Data = data;
        }
    }
}
