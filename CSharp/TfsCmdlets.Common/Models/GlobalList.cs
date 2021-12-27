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

        /// <summary>
        /// Creates an empty instance of GlobalList
        /// </summary>
        public GlobalList() {}

        /// <summary>
        /// Creates a new instance from the given name and items
        /// </summary>
        /// <param name="name">The name of the global list</param>
        /// <param name="items">The items (content) of the global list</param>
        public GlobalList(string name, IEnumerable<string> items)
        {
            Name = name;
            Items = new List<string>(items);
        }

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

    /// <summary>
    /// Represents a collection of global lists.
    /// </summary>
    public class GlobalListCollection: List<GlobalList>
    {
        private static readonly XNamespace _glNs = "http://schemas.microsoft.com/VisualStudio/2005/workitemtracking/globallists";

        /// <summary>
        /// Converts a collection of global lists to a well-formed &lt;gl:GLOBALLISTS&gt; document
        /// </summary>
        public static implicit operator XDocument(GlobalListCollection list) => list.ToXml();

        /// <summary>
        /// Converts a well-formed &lt;gl:GLOBALLISTS&gt; document to a collection of global lists
        /// </summary>
        public static implicit operator GlobalListCollection(XDocument doc) => new GlobalListCollection(doc);

        /// <summary>
        /// Creates an empty collection
        /// </summary>
        public GlobalListCollection(): base() {}

        /// <summary>
        /// Creates a collection and adds the given global lists to it
        /// </summary>
        /// <param name="items">Collection of global lists to add to this instance</param>
        public GlobalListCollection(IEnumerable<GlobalList> items): base(items) {}

        /// <summary>
        /// Creates a collection and adds the given global list to it
        /// </summary>
        /// <param name="item">A single global list to add to this instance</param>
        public GlobalListCollection(GlobalList item): base() => Add(item);

        /// <summary>
        /// Creates a collection from the given &lt;gl:GLOBALLISTS&gt; document
        /// </summary>
        /// <param name="xml">A well-formed &lt;gl:GLOBALLISTS&gt; document</param>
        public GlobalListCollection(string xml): this(XDocument.Parse(xml)) {}

        /// <summary>
        /// Creates a collection from the given &lt;gl:GLOBALLISTS&gt; document
        /// </summary>
        /// <param name="doc">A well-formed &lt;gl:GLOBALLISTS&gt; document</param>
        public GlobalListCollection(XDocument doc)
        {
            AddRange(doc.Descendants("GLOBALLIST").Select(el => new GlobalList(el)));
        }

        /// <summary>
        /// Returns the XML representation of the contents of this collection, in the form of a 
        /// &lt;gl:GLOBALLISTS&gt; document
        /// </summary>
        public override string ToString()
        {
            return ToXml().ToString();
        }

        /// <summary>
        /// Returns the XML representation of the contents of this collection, in the form of a 
        /// &lt;gl:GLOBALLISTS&gt; document, as a XDocument object
        /// </summary>
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
}