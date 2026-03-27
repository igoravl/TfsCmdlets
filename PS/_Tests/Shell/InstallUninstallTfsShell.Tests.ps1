Describe "Install-TfsShell and Uninstall-TfsShell" {
    BeforeEach {
        # Create temporary directory for testing
        $global:TestShortcutPath = Join-Path $env:TEMP "TfsCmdletsShellTest"
        if (Test-Path $TestShortcutPath) {
            Remove-Item $TestShortcutPath -Recurse -Force
        }
        New-Item -ItemType Directory -Path $TestShortcutPath -Force | Out-Null
    }
    
    AfterEach {
        # Clean up test directory
        if (Test-Path $TestShortcutPath) {
            Remove-Item $TestShortcutPath -Recurse -Force
        }
    }
    
    Context "Install-TfsShell" {
        It "Should be available as a cmdlet" {
            Get-Command Install-TfsShell -ErrorAction SilentlyContinue | Should -Not -BeNullOrEmpty
        }
        
        It "Should accept Target parameter" {
            $command = Get-Command Install-TfsShell
            $targetParam = $command.Parameters["Target"]
            $targetParam | Should -Not -BeNullOrEmpty
            $targetParam.ParameterType | Should -Be ([string[]])
        }
        
        It "Should accept Force parameter" {
            $command = Get-Command Install-TfsShell
            $forceParam = $command.Parameters["Force"]
            $forceParam | Should -Not -BeNullOrEmpty
            $forceParam.ParameterType | Should -Be ([System.Management.Automation.SwitchParameter])
        }
    }
    
    Context "Uninstall-TfsShell" {
        It "Should be available as a cmdlet" {
            Get-Command Uninstall-TfsShell -ErrorAction SilentlyContinue | Should -Not -BeNullOrEmpty
        }
        
        It "Should accept Target parameter" {
            $command = Get-Command Uninstall-TfsShell
            $targetParam = $command.Parameters["Target"]
            $targetParam | Should -Not -BeNullOrEmpty
            $targetParam.ParameterType | Should -Be ([string[]])
        }
    }
    
    Context "Windows Terminal profile JSON files" {
        It "Should have AzureDevOpsShell-WinPS.json profile" {
            $modulePath = (Get-Module TfsCmdlets).ModuleBase
            $profilePath = Join-Path $modulePath "AzureDevOpsShell-WinPS.json"
            Test-Path $profilePath | Should -BeTrue
            
            $profile = Get-Content $profilePath | ConvertFrom-Json
            $profile.name | Should -Be "Azure DevOps Shell (Windows PowerShell)"
            $profile.commandline | Should -Match "powershell\.exe.*Import-Module TfsCmdlets.*Enter-TfsShell"
        }
        
        It "Should have AzureDevOpsShell-PSCore.json profile" {
            $modulePath = (Get-Module TfsCmdlets).ModuleBase
            $profilePath = Join-Path $modulePath "AzureDevOpsShell-PSCore.json"
            Test-Path $profilePath | Should -BeTrue
            
            $profile = Get-Content $profilePath | ConvertFrom-Json
            $profile.name | Should -Be "Azure DevOps Shell (PowerShell Core)"
            $profile.commandline | Should -Match "pwsh\.exe.*Import-Module TfsCmdlets.*Enter-TfsShell"
        }
    }
}