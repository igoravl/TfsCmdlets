# This script is a psake script file and should not be called directly. Use ..\Build.ps1 instead.

Properties {

    Function Get-EscapedMSBuildArgument($arg)
    {
        return '"' + $arg.Replace('"', '\"') + '"'
    }

    # Source information
    $SourceDir = Join-Path $SolutionDir "TfsCmdlets\Bin\$Configuration\"

    # Output destination
    $OutDir = Join-Path $SolutionDir 'Build'
    $ChocolateyDir = Join-Path $OutDir 'chocolatey'
    $MSIDir = Join-Path $OutDir 'msi'
    $NugetDir = Join-Path $OutDir 'nuget'
    $DocsDir = Join-Path $OutDir 'docs'
    $ModuleDir = Join-Path $OutDir 'Module'
    $ModuleBinDir = (Join-Path $ModuleDir 'bin')

    # Module generation
    $ModuleManifestPath = Join-Path $ModuleDir 'TfsCmdlets\TfsCmdlets.psd1'

    # Nuget packaging
    $NugetExePath = Join-Path $SolutionDir '.nuget\nuget.exe'
    $NugetToolsDir = Join-Path $NugetDir 'Tools'
    $NugetSpecPath = Join-Path $NugetDir "TfsCmdlets.nuspec"
    $NugetPackageVersion = $BuildName -replace '\+.+', '' # remove SemVer 2.0 build metadata; not supported by NuGet 2.x

    # Chocolatey packaging
    $ChocolateySpecPath = Join-Path $ChocolateyDir "TfsCmdlets.nuspec"

    # Wix packaging
	$WixVersion = "$Version"
    $WixOutputPath = Join-Path $SolutionDir "TfsCmdlets.Setup\bin\$Configuration"

    # MSBuild-related properties
    $SolutionPath = Join-Path $SolutionDir 'TfsCmdlets.sln'
    $MSBuildArgs = "`"$SolutionPath`" " + `
        "/tv:$VisualStudioVersion.0 " +
        "/Verbosity:Detailed " +
        "/p:Configuration=$Configuration " + `
        "/p:Platform=`"Any CPU`" " + `
        "/p:BranchName=$BranchName " + `
        "/p:ModuleAuthor=`"$ModuleAuthor`" " + `
        "/p:ModuleName=$ModuleName " + `
        "/p:ModuleDescription=`"$ModuleDescription`" " + `
        "/p:Commit=$(Get-EscapedMSBuildArgument $Commit) " + `
        "/p:PreRelease=$PreRelease " + `
        "/p:BuildName=$BuildName " + `
        "/p:Version=$Version " + `
        "/p:WixProductName=`"$ModuleDescription ($ModuleName)`" " + `
        "/p:WixProductVersion=$WixVersion " + `
        "/p:WixFileVersion=$NugetPackageVersion " + `
        "/p:WixAuthor=`"$ModuleAuthor`""

    #7zip
    $7zipExepath = Join-Path $SolutionDir '7za.exe'

}

Task Build -Depends DetectDependencies, CopyModule, PackageNuget, PackageChocolatey, PackageMSI, PackageDocs, PackageModule {

}

Task CopyModule -Depends Clean, MSBuild {

    Copy-Item -Path $SourceDir -Destination $ModuleDir -Recurse 
}

Task MSBuild {

    Write-Output "Running MSBuild.exe with arguments [ $MSBuildArgs ]"
    exec { MSBuild.exe '--%' $MSBuildArgs }
}

Task DetectDependencies {

    if (-not (Test-Path "HKCU:\SOFTWARE\Microsoft\VisualStudio\$VisualStudioVersion.0_Config\Projects\{f5034706-568f-408a-b7b3-4d38c6db8a32}"))
    {
        Write-Warning "PowerShell Tools for Visual Studio (PoShTools) not found. Although not needed for build, it is required to open the solution in Visual Studio. Download PoShTools from https://visualstudiogallery.msdn.microsoft.com/f65f845b-9430-4f72-a182-ae2a7b8999d7 (VS 2013) or https://visualstudiogallery.msdn.microsoft.com/c9eb3ba8-0c59-4944-9a62-6eee37294597 (VS 2015) and install it in the corresponding Visual Studio version."
    }

    if (-not $env:WIX)
    {
        Write-Error "WiX Toolset is not installed. Download WiX from http://www.wixtoolset.org prior to building TfsCmdlets."
        throw "Missing dependency. Breaking build."
    }

    if (-not $env:ChocolateyInstall)
    {
        Write-Error "Chocolatey is not installed. Download Chocolatey from http://www.chocolatey.org prior to building TfsCmdlets."
        throw "Missing dependency. Breaking build."
    }

}

Task Clean {

    if (Test-Path $OutDir -PathType Container)
    {
        Remove-Item $OutDir -Recurse -Force
    }

    if (Test-Path $SourceDir -PathType Container)
    {
        Remove-Item $SourceDir -Recurse -Force
    }

    New-Item $OutDir -ItemType Directory | Out-Null
}

Task PackageModule -Depends CopyModule {
    & $7zipExePath a (Join-Path $ModuleDir "TfsCmdlets-Portable-$NugetPackageVersion.zip") $ModuleDir\TfsCmdlets
}

Task PackageNuget -Depends CopyModule, GenerateNuspec {

    Copy-Item $ModuleDir $NugetToolsDir -Recurse 
    & $NugetExePath @('Pack', $NugetSpecPath, '-OutputDirectory', $NugetDir, '-Verbosity', 'Quiet', '-NonInteractive')
}

Task PackageChocolatey -Depends CopyModule {

    Copy-Item $NugetDir $ChocolateyDir -Recurse
    Push-Location $ChocolateyDir
    choco @('Pack', $ChocolateySpecPath)
    Pop-Location
}

Task PackageMSI {

    New-Item $MSIDir -ItemType Directory | Out-Null
    Copy-Item -Path "$WixOutputPath\*.msi" -Destination $MSIDir
}

Task PackageDocs -Depends CopyModule, GenerateDocs {

    #Compress-Archive -Path $DocsDir -CompressionLevel Optimal -DestinationPath (Join-Path $DocsDir "TfsCmdlets-docs-$NugetPackageVersion.zip") 
}

Task GenerateDocs {

    #Get-Module TfsCmdlets | Remove-Module
    #
    #New-Item $DocsDir -ItemType Directory | Out-Null
    #
    #Get-Module HelpGenerator | Remove-Module
    #Import-Module .\HelpGenerator.psm1 -Scope Local
    #
    #Export-MDHelpDocumentation -ModuleManifest $ModuleManifestPath -Destination $DocsDir
}

Task GenerateNuspec {

    New-Item $NugetDir -ItemType Directory | Out-Null

    $SourceManifest = Test-ModuleManifest -Path $ModuleManifestPath

    $nuspec = @"
<?xml version="1.0"?>
<package>
    <metadata>
        <id>$($SourceManifest.Name)</id>
        <title>$($SourceManifest.Name)</title>
        <version>$NugetPackageVersion</version>
        <authors>$($SourceManifest.Author)</authors>
        <owners>$($SourceManifest.Author)</owners>
        <licenseUrl>$($SourceManifest.PrivateData.LicenseUri)</licenseUrl>
        <projectUrl>$($SourceManifest.PrivateData.ProjectUri)</projectUrl>
        <iconUrl>$($SourceManifest.PrivateData.IconUri)</iconUrl>
        <requireLicenseAcceptance>false</requireLicenseAcceptance>
        <description>$($SourceManifest.Description)</description>
        <releaseNotes><![CDATA[$($SourceManifest.PrivateData.ReleaseNotes)]]></releaseNotes>
        <copyright>$($SourceManifest.Copyright)</copyright>
        <tags>$($SourceManifest.PrivateData.Tags -Join ' ')</tags>
    </metadata>
</package>
"@

    Set-Content -Path $NugetSpecPath -Value $nuspec
}