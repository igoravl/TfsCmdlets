& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    BeforeAll {
        $serverGroups = @('[TEAM FOUNDATION]\Enterprise Invited Users', '[TEAM FOUNDATION]\Enterprise Service Accounts', '[TEAM FOUNDATION]\Team Foundation Proxy Service Accounts')
        $collectionGroups = @('Project Collection Administrators', 'Project Collection Build Administrators', 'Project Collection Build Service Accounts', 'Project Collection Proxy Service Accounts', 'Project Collection Service Accounts', 'Project Collection Test Service Accounts', 'Project Collection Valid Users', 'Project-Scoped Users', 'Security Service Group')
        $projectGroups = @('Build Administrators', 'Contributors', 'Endpoint Administrators', 'Endpoint Creators', 'Project Administrators', 'Project Valid Users', 'PUL', 'PUL-DB', 'Readers', 'Release Administrators', 'TestProject Team')
    }

    Context '__AllParameterSets' {
        # Get-TfsGroup
        # [[-Group] <Object>]
        # [-Scope <GroupScope>]
        # [-Recurse]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]
        
        It 'Should get all server-level groups' {
            Get-TfsGroup -Scope Server | Select-Object -ExpandProperty PrincipalName | Sort-Object | Should -Be $serverGroups
        }
        
        It 'Should get all collection-level groups' {
            Get-TfsGroup | Select-Object -ExpandProperty DisplayName | Sort-Object | Should -Be $collectionGroups
            Get-TfsGroup -Scope Collection | Select-Object -ExpandProperty DisplayName | Sort-Object | Should -Be $collectionGroups
        }

        It 'Should get all project-level groups' {
            Get-TfsGroup -Scope Project -Project $tfsProject | Select-Object -ExpandProperty DisplayName | Sort-Object | Should -Be $projectGroups
        }
    } 
}
