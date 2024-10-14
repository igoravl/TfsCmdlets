& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'By Run' {
        # Get-TfsPipelineRun
        #  [-PipelineRun] <Object>
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return a pipeline run' {
            $result = Get-TfsPipelineRun -PipelineRun 484 -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Build.WebApi.Build'
            $result.Id | Should -Be 484
        }
    } 

    Context 'By Search' {
        # Get-TfsPipelineRun
        # [-Reason <BuildReason>]
        # [-Status <BuildStatus>]
        # [-Result <BuildResult>]
        # [-RequestedFor <Object>]
        # [-MinTime <datetime>]
        # [-MaxTime <datetime>]
        # [-Tag <string[]>]
        # [-Branch <Object>]
        # [-Repository <Object>]
        # [-Definition <Object>]
        # [-BuildNumber <string>]
        # [-QueryOrder <BuildQueryOrder>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return succeeded runs' {
            $result = Get-TfsPipelineRun -Status Completed -Result Succeeded -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Build.WebApi.Build'
            $result | Should -Not -BeNullOrEmpty
            $result.Result | Should -Be 'Succeeded'
        }
    }
}