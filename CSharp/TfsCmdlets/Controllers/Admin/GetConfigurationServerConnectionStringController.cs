using System.Xml.Linq;
using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.Admin
{
    [CmdletController]
    partial class GetConfigurationConnectionStringController
    {
        protected override IEnumerable Run()
        {
            if (Parameters.HasParameter("Session"))
            {
                throw new NotImplementedException("Remote sessions are currently not supported");
            }

            if (!Parameters.Get<string>("ComputerName").Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException("Remote computers are currently not supported");
            }

            var installationPath = Data.GetItem<TfsInstallationPath>(new {
                Component = TfsComponent.ApplicationTier
            });

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
    }
}
