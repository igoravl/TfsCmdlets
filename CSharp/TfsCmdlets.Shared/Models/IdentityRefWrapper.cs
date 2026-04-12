using System.Xml;
using System.Xml.Schema;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Models
{
    public class IdentityRefWrapper: ModelBase<WebApiIdentityRef>
    {
        public static explicit operator WebApiIdentityRef(IdentityRefWrapper i) => i.InnerObject;
        public static explicit operator IdentityRefWrapper(WebApiIdentityRef i) => new(i);

        public IdentityRefWrapper(WebApiIdentityRef item)
            : base(item)
        {
        }

        public XmlSchema GetSchema()
        {
            return ((System.Xml.Serialization.IXmlSerializable)InnerObject).GetSchema();
        }

        public void ReadXml(XmlReader reader)
        {
            ((System.Xml.Serialization.IXmlSerializable)InnerObject).ReadXml(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            ((System.Xml.Serialization.IXmlSerializable)InnerObject).WriteXml(writer);
        }

        public string GetToken()
        {
            return ((Microsoft.VisualStudio.Services.WebApi.ISecuredObject)InnerObject).GetToken();
        }

        public Guid NamespaceId => ((Microsoft.VisualStudio.Services.WebApi.ISecuredObject)InnerObject).NamespaceId;

        public int RequiredPermissions => ((Microsoft.VisualStudio.Services.WebApi.ISecuredObject)InnerObject).RequiredPermissions;

        public SubjectDescriptor Descriptor
        {
            get => InnerObject.Descriptor;
            set => InnerObject.Descriptor = value;
        }

        public string DisplayName
        {
            get => InnerObject.DisplayName;
            set => InnerObject.DisplayName = value;
        }

        public string Url
        {
            get => InnerObject.Url;
            set => InnerObject.Url = value;
        }

        public ReferenceLinks Links
        {
            get => InnerObject.Links;
            set => InnerObject.Links = value;
        }

        public string Id
        {
            get => InnerObject.Id;
            set => InnerObject.Id = value;
        }

        public string UniqueName
        {
            get => InnerObject.UniqueName;
            set => InnerObject.UniqueName = value;
        }

        public string DirectoryAlias
        {
            get => InnerObject.DirectoryAlias;
            set => InnerObject.DirectoryAlias = value;
        }

        public string ProfileUrl
        {
            get => InnerObject.ProfileUrl;
            set => InnerObject.ProfileUrl = value;
        }

        public string ImageUrl
        {
            get => InnerObject.ImageUrl;
            set => InnerObject.ImageUrl = value;
        }

        public bool IsContainer
        {
            get => InnerObject.IsContainer;
            set => InnerObject.IsContainer = value;
        }

        public bool IsAadIdentity
        {
            get => InnerObject.IsAadIdentity;
            set => InnerObject.IsAadIdentity = value;
        }

        public bool Inactive
        {
            get => InnerObject.Inactive;
            set => InnerObject.Inactive = value;
        }

        public bool IsDeletedInOrigin
        {
            get => InnerObject.IsDeletedInOrigin;
            set => InnerObject.IsDeletedInOrigin = value;
        }

        public override string ToString()
        {
            return InnerObject?.DisplayName ?? string.Empty;
        }
    }
}