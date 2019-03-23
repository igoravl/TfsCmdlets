@{
    Author = '${ModuleAuthor}'
    CompanyName = '${ModuleAuthor}'
    Copyright = '(c) 2014 ${ModuleAuthor}. All rights reserved.'
    Description = '${ModuleDescription}'
    RootModule = 'TfsCmdlets.psm1'
    GUID = 'bd4390dc-a8ad-4bce-8d69-f53ccf8e4163'
    HelpInfoURI = 'https://github.com/igoravl/tfscmdlets/wiki/'
    ModuleVersion = '1.0.0.0' #${Version}
    PowerShellVersion = '3.0'
    TypesToProcess = "TfsCmdlets.Types.ps1xml"
    FormatsToProcess = "TfsCmdlets.Format.ps1xml"
    ScriptsToProcess = 'Startup.ps1'
    AliasesToExport = @('tfsrv', 'tftpc', 'tftp', 'gtftpc', 'gtftp')
    VariablesToExport = @()
    CmdletsToExport = @()
    FunctionsToExport = @()
    FileList = @() 
    NestedModules = @() 

    PrivateData = @{ 
        Tags = @('TfsCmdlets', 'TFS', 'VSTS', 'PowerShell', 'Azure', 'Azure DevOps', 'DevOps', 'ALM', 'Team Foundation Server')
        Branch = '${BranchName}'
        Commit = '${Commit}'
        Build = '${BuildName}'
        PreRelease = '${PreRelease}'
        LicenseUri = 'https://raw.githubusercontent.com/igoravl/tfscmdlets/master/LICENSE.md'
        ProjectUri = 'https://github.com/igoravl/tfscmdlets/'
        IconUri = 'https://raw.githubusercontent.com/igoravl/tfscmdlets/master/TfsCmdlets/resources/TfsCmdlets_Icon_32.png'
        ReleaseNotes = 'See https://github.com/igoravl/tfscmdlets/wiki/ReleaseNotes' 
        TfsClientVersion = '${TfsOmNugetVersion}'
    }
}
