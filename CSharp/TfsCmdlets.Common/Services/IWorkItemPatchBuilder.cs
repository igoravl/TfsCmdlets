using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace TfsCmdlets.Services
{
    public interface IWorkItemPatchBuilder
    {
        void Initialize(WebApiWorkItem wi);
        JsonPatchDocument GetJson();
    }
}