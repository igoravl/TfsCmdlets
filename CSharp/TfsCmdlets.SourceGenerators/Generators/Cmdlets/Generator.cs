using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    [Generator]
    public class CmdletGenerator : BaseGenerator<Filter, TypeProcessor>
    {
        protected override string GeneratorName => nameof(CmdletGenerator);

        protected override void GenerateCmdletParameters(CmdletInfo cmdletInfo)
        {
            base.GenerateCmdletParameters(cmdletInfo);

            if (cmdletInfo.Name.StartsWith("Connect-"))
            {
                cmdletInfo.Parameters.Add(new CmdletParameter
                {
                    Name = "AzCli",
                    Type = "SwitchParameter",
                    Mandatory = false,
                    Position = -1
                });

                cmdletInfo.Parameters.Add(new CmdletParameter
                {
                    Name = "UseMSI",
                    Type = "SwitchParameter",
                    Mandatory = false,
                    Position = -1
                });
            }
        }
    }
}
