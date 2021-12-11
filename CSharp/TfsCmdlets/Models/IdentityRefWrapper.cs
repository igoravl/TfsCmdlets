using System.Xml;
using System.Xml.Schema;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using WebApiIdentityRef = Microsoft.VisualStudio.Services.WebApi.IdentityRef;

namespace TfsCmdlets.Models
{
    public class IdentityRefWrapper
    {
        public static explicit operator WebApiIdentityRef(IdentityRefWrapper i) => i._inner;
        public static explicit operator IdentityRefWrapper(WebApiIdentityRef i) => new(i);

        private readonly WebApiIdentityRef _inner;

        internal IdentityRefWrapper(WebApiIdentityRef inner)
        {
            _inner = inner;
        }

        public XmlSchema GetSchema()
        {
            return ((System.Xml.Serialization.IXmlSerializable)_inner).GetSchema();
        }

        public void ReadXml(XmlReader reader)
        {
            ((System.Xml.Serialization.IXmlSerializable)_inner).ReadXml(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            ((System.Xml.Serialization.IXmlSerializable)_inner).WriteXml(writer);
        }

        public string GetToken()
        {
            return ((Microsoft.VisualStudio.Services.WebApi.ISecuredObject)_inner).GetToken();
        }

        public Guid NamespaceId => ((Microsoft.VisualStudio.Services.WebApi.ISecuredObject)_inner).NamespaceId;

        public int RequiredPermissions => ((Microsoft.VisualStudio.Services.WebApi.ISecuredObject)_inner).RequiredPermissions;

        public SubjectDescriptor Descriptor
        {
            get => _inner.Descriptor;
            set => _inner.Descriptor = value;
        }

        public string DisplayName
        {
            get => _inner.DisplayName;
            set => _inner.DisplayName = value;
        }

        public string Url
        {
            get => _inner.Url;
            set => _inner.Url = value;
        }

        public ReferenceLinks Links
        {
            get => _inner.Links;
            set => _inner.Links = value;
        }

        public string Id
        {
            get => _inner.Id;
            set => _inner.Id = value;
        }

        public string UniqueName
        {
            get => _inner.UniqueName;
            set => _inner.UniqueName = value;
        }

        public string DirectoryAlias
        {
            get => _inner.DirectoryAlias;
            set => _inner.DirectoryAlias = value;
        }

        public string ProfileUrl
        {
            get => _inner.ProfileUrl;
            set => _inner.ProfileUrl = value;
        }

        public string ImageUrl
        {
            get => _inner.ImageUrl;
            set => _inner.ImageUrl = value;
        }

        public bool IsContainer
        {
            get => _inner.IsContainer;
            set => _inner.IsContainer = value;
        }

        public bool IsAadIdentity
        {
            get => _inner.IsAadIdentity;
            set => _inner.IsAadIdentity = value;
        }

        public bool Inactive
        {
            get => _inner.Inactive;
            set => _inner.Inactive = value;
        }

        public bool IsDeletedInOrigin
        {
            get => _inner.IsDeletedInOrigin;
            set => _inner.IsDeletedInOrigin = value;
        }

        public override string ToString()
        {
            return _inner?.DisplayName ?? string.Empty;
        }
    }
}