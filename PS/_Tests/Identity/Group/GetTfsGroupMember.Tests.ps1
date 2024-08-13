& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    BeforeAll {
        $collectionName = (Get-TfsTeamProjectCollection -Current).Name
    }

    Context '__AllParameterSets' {
        # Get-TfsGroupMember
        # [-Group] <Object>
        # [[-Member] <string>]
        # [-Recurse]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should get all members of a server-level group' {
            $members = Get-TfsGroupMember -Group '[TEAM FOUNDATION]\Enterprise Service Accounts' 
            $members | Should -BeOfType [Microsoft.VisualStudio.Services.Identity.Identity]
            $members.AccountName | Should -Be 'TeamFoundationService (TEAM FOUNDATION)'
        }

        It 'Should get all members of a collection-level group' {
            $members = Get-TfsGroupMember -Group "[$collectionName]\Project Collection Service Accounts" 
            $members | Should -BeOfType [Microsoft.VisualStudio.Services.Identity.Identity]
            $members.AccountName | Should -Be 'Enterprise Service Accounts'
        }

        It 'Should get all members of a project-level group' {
            $members = Get-TfsGroupMember -Group "[$tfsProject]\Endpoint Administrators"
            $members | Should -BeOfType [Microsoft.VisualStudio.Services.Identity.Identity]
            $members.AccountName | Should -Be 'Project Administrators'
        }

        It 'Should get all members of a group recursively' {
            $members = Get-TfsGroupMember -Group "[$tfsProject]\Endpoint Administrators" -Recurse
            $members | Should -BeOfType [Microsoft.VisualStudio.Services.Identity.Identity]
            $members.AccountName | Sort-Object | Should -Be @('igor@tshooter.com.br', 'Project Administrators')
        }
    } 
}