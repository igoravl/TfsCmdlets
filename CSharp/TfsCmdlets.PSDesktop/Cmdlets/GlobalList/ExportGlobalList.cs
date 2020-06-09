using System.Linq;
using System.Xml.Linq;
using TfsCmdlets.Extensions;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    partial class ExportGlobalList
    {
        private static readonly XNamespace _glNs = "http://schemas.microsoft.com/VisualStudio/2005/workitemtracking/globallists";

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var lists = this.GetCollectionOf<TfsGlobalList>();
            var root = CreateDocument(lists.Select(l=>l.ToXml()));

            WriteObject(root.ToString());
        }
    }
}