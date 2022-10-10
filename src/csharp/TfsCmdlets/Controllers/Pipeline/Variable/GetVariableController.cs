using Microsoft.TeamFoundation.DistributedTask.WebApi;
using WebApiVariableGroup = Microsoft.TeamFoundation.DistributedTask.WebApi.VariableGroup;

namespace TfsCmdlets.Controllers.Pipeline.Variable
{
    [CmdletController]
    partial class GetVariableController
    {
        protected override IEnumerable Run()
        {
            return ParameterSetName switch
            {
                "By Pipeline" => GetByPipeline(),
                "By Release" => GetByRelease(),
                "By Variable Group" => GetByVariableGroup(),
                _ => throw new ArgumentException($"Invalid ParameterSetName '{ParameterSetName}'"),
            };
        }

        private IEnumerable GetByVariableGroup()
        {
            throw new NotImplementedException();
        }

        private IEnumerable GetByRelease()
        {
            throw new NotImplementedException();
        }

        private IEnumerable GetByPipeline()
        {
            throw new NotImplementedException();
        }
    }
}