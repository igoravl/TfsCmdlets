using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace TfsCmdlets.Services
{
    public interface IWorkItemPatchBuilder
    {
        JsonPatchDocument GetJson(WebApiWorkItem wi);
    }
}