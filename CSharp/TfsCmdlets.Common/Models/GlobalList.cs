using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents a Team Foundation Server global lists
    /// </summary>
    public class GlobalList
    {
        /// <summary>
        /// Converts a GlobalList to an XElement
        /// </summary>
        public static implicit operator XElement(GlobalList gl) => gl.ToXml();
        
        /// <summary>
        /// Converts an XElement to a GlobalList
        /// </summary>
        public static implicit operator GlobalList(XElement el) => new GlobalList(el);

        public GlobalList() {}

        /// <summary>
        /// Creates an instance of GlobalList from an XElement containing a &lt;GLOBALLIST&gt; element
        /// </summary>
        /// <param name="el">An XML element containing a global list definition</param>
        /// <returns>An instance of GlobalList</returns>
        public GlobalList(XElement el)
        {
            Name = el.Attribute("name").Value;
            Items = el.Descendants("LISTITEM").Select(li => li.Attribute("value").Value).ToList();
        }

        /// <summary>
        /// Gets the name of the global list.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the contents (items) of the global list.
        /// </summary>
        public IList<string> Items { get; internal set;} = new List<string>();
        
        internal GlobalList(string name, IEnumerable<string> items)
        {
            Name = name;
            Items = new List<string>(items);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ToXml().ToString();
        }

        /// <summary>
        /// Returns this global list in its original XML representation
        /// </summary>
        /// <returns>A XElement object representing a &lt;GLOBALLIST&gt; element</returns>
        public XElement ToXml()
        {
            return new XElement("GLOBALLIST", new XAttribute("name", Name), Items.Select(i => new XElement("LISTITEM", new XAttribute("value", i))));
        }
    }

    public class GlobalListCollection: List<GlobalList>
    {
        private static readonly XNamespace _glNs = "http://schemas.microsoft.com/VisualStudio/2005/workitemtracking/globallists";

        public static implicit operator XDocument(GlobalListCollection list) => list.ToXml();

        public static implicit operator GlobalListCollection(XDocument doc) => new GlobalListCollection(doc);

        public GlobalListCollection(): base() {}

        public GlobalListCollection(IEnumerable<GlobalList> items): base(items) {}

        public GlobalListCollection(GlobalList item): base() => Add(item);

        public GlobalListCollection(string xml): this(XDocument.Parse(xml)) {}

        public GlobalListCollection(XDocument doc)
        {
            AddRange(doc.Descendants("GLOBALLIST").Select(el => new GlobalList(el)));
        }

        public override string ToString()
        {
            return ToXml().ToString();
        }

        public XDocument ToXml()
        {
            return CreateDocument(this.Select(gl => gl.ToXml()));
        }

        private XDocument CreateDocument(params object[] content)
        {
            return new XDocument(
                new XElement(_glNs + "GLOBALLISTS",
                    new XAttribute(XNamespace.Xmlns + "gl", _glNs.NamespaceName), content));
        }
    }

/*




*/
}