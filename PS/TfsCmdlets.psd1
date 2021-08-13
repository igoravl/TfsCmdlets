@{
    Author = 'Igor Abade V. Leite'
    CompanyName = 'Igor Abade V. Leite'
    Copyright = '(c) 2014 Igor Abade V. Leite. All rights reserved.'
    Description = 'PowerShell Cmdlets for Azure DevOps and Team Foundation Server'
    RootModule = 'Lib/Desktop/TfsCmdlets.dll'
    GUID = 'bd4390dc-a8ad-4bce-8d69-f53ccf8e4163'
    ModuleVersion = '1.0.0.0'
    NestedModules = @('Private/Functions.psm1', 'Private/Aliases.psm1', 'Private/ArgumentCompleters.psm1', 'Private/Admin.psm1')
    PowerShellVersion = '5.1'
    DotNetFrameworkVersion = '4.6.2'
    TypesToProcess = "TfsCmdlets.Types.ps1xml"
    FormatsToProcess = "TfsCmdlets.Format.ps1xml"
    AliasesToExport = '*'
    PrivateData = @{ 
        PSData = @{
            Tags = @('TfsCmdlets', 'TFS', 'VSTS', 'PowerShell', 'Azure', 'AzureDevOps', 'DevOps', 'ALM', 'TeamFoundationServer')
            Branch = 'BranchName'
            Commit = 'Commit'
            Build = 'BuildName'
            PreRelease = 'PreRelease'
            LicenseUri = 'https://github.com/igoravl/TfsCmdlets/blob/master/LICENSE.md'
            ProjectUri = 'https://github.com/igoravl/tfscmdlets/'
            IconUri = 'https://raw.githubusercontent.com/igoravl/TfsCmdlets/master/Assets/TfsCmdlets_Icon_32.png'
        }
        TfsClientVersion = 'TfsOmNugetVersion'
    }
}
