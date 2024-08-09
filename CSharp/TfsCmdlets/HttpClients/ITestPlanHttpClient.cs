using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(TestPlanHttpClient))]
    partial interface ITestPlanHttpClient
    {
    }
}