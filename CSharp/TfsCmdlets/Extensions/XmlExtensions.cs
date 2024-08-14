using System.Xml;
using System.Xml.Linq;

namespace TfsCmdlets.Extensions
{
    /// <summary>
    /// Linq to XML extension methods
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Converts a LINQ-based XDocument to a "legacy" XmlDocument
        /// </summary>
        /// <param name="xDocument">The document to be converted</param>
        /// <returns>The converted document</returns>
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        /// <summary>
        /// Converts a "legacy" XmlDocument to a LINQ-based XDocument
        /// </summary>
        /// <param name="xmlDocument">The document to be converted</param>
        /// <returns>The converted document</returns>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using var nodeReader = new XmlNodeReader(xmlDocument);

            nodeReader.MoveToContent();
            
            return XDocument.Load(nodeReader);
        }
    }
}