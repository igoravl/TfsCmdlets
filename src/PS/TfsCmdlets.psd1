@{
    Author = '${ModuleAuthor}'
    CompanyName = '${ModuleAuthor}'
    Copyright = '(c) 2014 ${ModuleAuthor}. All rights reserved.'
    Description = '${ModuleDescription}'
    RootModule = 'TfsCmdlets.psm1'
    FunctionsToExport = '*-Tfs*'
    GUID = 'bd4390dc-a8ad-4bce-8d69-f53ccf8e4163'
    HelpInfoURI = 'https://github.com/igoravl/tfscmdlets/wiki/'
    ModuleVersion = '${Version}'
    PowerShellVersion = '3.0'
    TypesToProcess = "TfsCmdlets.Types.ps1xml"
    FormatsToProcess = "TfsCmdlets.Format.ps1xml"
    ScriptsToProcess = 'Startup.ps1'

    NestedModules = @(${NestedModules})
    FileList = @(${FileList})

    PrivateData = @{ 
        Tags = @('TfsCmdlets', 'TFS', 'VSTS', 'PowerShell')
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
