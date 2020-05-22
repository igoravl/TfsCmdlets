@{
    Author = 'Igor Abade V. Leite'
    CompanyName = 'Igor Abade V. Leite'
    Copyright = '(c) 2014 Igor Abade V. Leite. All rights reserved.'
    Description = 'PowerShell Cmdlets for Azure DevOps and Team Foundation Server'
    RootModule = if($PSEdition -eq 'Core') { 'Lib/Core/TfsCmdlets.PSCore.dll' } else { 'Lib/Desktop/TfsCmdlets.PSDesktop.dll' }
    DefaultCommandPrefix = 'Tfs'
    GUID = 'bd4390dc-a8ad-4bce-8d69-f53ccf8e4163'
    HelpInfoURI = 'https://github.com/igoravl/tfscmdlets/wiki/'
    ModuleVersion = '1.0.0.0'
    PowerShellVersion = '5.1'
    TypesToProcess = "TfsCmdlets.Types.ps1xml"
    FormatsToProcess = "TfsCmdlets.Format.ps1xml"
    AliasesToExport = '*'
    VariablesToExport = @()
    CmdletsToExport = @()
    FunctionsToExport = @()
    FileList = @() 
    NestedModules = @() 
    PrivateData = @{ 
        PSData = @{
            Tags = @('TfsCmdlets', 'TFS', 'VSTS', 'PowerShell', 'Azure', 'AzureDevOps', 'DevOps', 'ALM', 'TeamFoundationServer')
            Branch = 'BranchName'
            Commit = 'Commit'
            Build = 'BuildName'
            PreRelease = 'PreRelease'
            LicenseUri = 'https://raw.githubusercontent.com/igoravl/tfscmdlets/master/LICENSE.md'
            ProjectUri = 'https://github.com/igoravl/tfscmdlets/'
            IconUri = 'https://raw.githubusercontent.com/igoravl/tfscmdlets/master/TfsCmdlets/resources/TfsCmdlets_Icon_32.png'
            ReleaseNotes = 'See https://github.com/igoravl/tfscmdlets/wiki/ReleaseNotes' 
        }
        TfsClientVersion = 'TfsOmNugetVersion'
    }
}
