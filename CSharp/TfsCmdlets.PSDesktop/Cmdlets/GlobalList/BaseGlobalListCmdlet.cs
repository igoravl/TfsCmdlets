using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using TfsCmdlets.Extensions;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    partial class BaseGlobalListCmdlet
    {
        private static readonly XNamespace _glNs = "http://schemas.microsoft.com/VisualStudio/2005/workitemtracking/globallists";

        protected virtual XDocument CreateDocument(params object[] content)
        {

            return new XDocument(
                new XElement(_glNs + "GLOBALLISTS",
                    new XAttribute(XNamespace.Xmlns + "gl", _glNs.NamespaceName), content));
        }

        protected virtual void Import(XDocument listsDocument)
        {
            var tpc = GetCollection();
            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
            store.ImportGlobalLists(listsDocument.ToString());
        }

        protected virtual void Import(XElement listElement)
        {
            Import(CreateDocument(listElement));
        }

        protected virtual void Import(GlobalList list)
        {
            Import(list.ToXml());
        }

        protected virtual void Remove(string listName, bool force = false)
        {
            Remove(new[]{listName});
        }

        protected virtual void Remove(IEnumerable<string> listNames, bool force = false)
        {
            var doc = new XDocument(
                new XElement("Package", 
                    listNames.Select(s => new XElement("DestroyGlobalList",
                        new XAttribute("ListName", $"*{s}"),
                        new XAttribute("ForceDelete", $"{force}")))
                )
            );

            var tpc = GetCollection();
            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
            
            store.SendUpdatePackage(doc.ToXmlDocument().DocumentElement, out var _, false);
        }
    }
}

/*
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        $lists = Get-TfsGlobalList -Name $GlobalList -Collection $Collection
        $listsToRemove = @()

        foreach($list in $lists)
        {
            if ($PSCmdlet.ShouldProcess($list.Name, "Remove global list"))
            {
                $listsToRemove += $list
            }
        }

        if ($listsToRemove.Length -eq 0)
        {
            return
        }

        $xml = [xml] "<Package />"

        foreach($list in $listsToRemove)
        {
            $elem = $xml.CreateElement("DestroyGlobalList");
            $elem.SetAttribute("ListName", "*" + $list.Name);
            $elem.SetAttribute("ForceDelete", "true");
            [void]$xml.DocumentElement.AppendChild($elem);
        }

        $returnElem = $null

        $store.SendUpdatePackage($xml.DocumentElement, [ref] $returnElem, $false)
*/