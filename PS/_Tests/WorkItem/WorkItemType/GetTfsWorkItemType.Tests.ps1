& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Common' {
        It 'Should require mandatory parameters' {
            { Get-TfsWorkItemType } | Should -Throw
            { Get-TfsWorkItemType -Type Task } | Should -Throw
            { Get-TfsWorkItemType -WorkItem '123' } | Should -Throw
        }
    }

    Context 'Get by type' {
        # Get-TfsWorkItemType
        #  [[-Type] <Object>]
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return a work item type' {
            $workItemType = Get-TfsWorkItemType -Type 'Task' -Project $tfsProject
            $workItemType | Should -Not -BeNullOrEmpty
        }

        It 'Should return multiple types from a list' {
            $workItemType = Get-TfsWorkItemType 'Task', 'Epic', 'Feature' -Project $tfsProject
            $workItemType.Name | Sort-Object | Should -Be @('Epic', 'Feature', 'Task')
        }

        It 'Should return all work item types' {
            $workItemTypes = Get-TfsWorkItemType -Project $tfsProject
            $workItemTypes.Name | Sort-Object | Should -Be @('Bug', 'Code Review Request', 'Code Review Response', 'Epic', 'Feature', 'Feedback Request', 'Feedback Response', 'Impediment', 'Product Backlog Item', 'Shared Parameter', 'Shared Steps', 'Task', 'Test Case', 'Test Plan', 'Test Suite')
        }

        It 'Should support wildcards' {
            $workItemTypes = Get-TfsWorkItemType -Type 'T*' -Project $tfsProject
            $workItemTypes.Name | Sort-Object | Should -Be @('Task', 'Test Case', 'Test Plan', 'Test Suite')
        }

        context 'Get by work item' {
            # Get-TfsWorkItemType
            #  -WorkItem <Object>
            #  [-Project <Object>]
            #  [-Collection <Object>]
            #  [-Server <Object>] [<CommonParameters>]' 

            It 'Should return a type given a WI' {
                $wi = (Get-TfsWorkItem -Type 'Task' -Project $tfsProject)[0]
                $workItemType = Get-TfsWorkItemType -WorkItem $wi -Project $tfsProject
                $workItemType.Name | Should -Be 'Task'
            }
        } 
    }
}