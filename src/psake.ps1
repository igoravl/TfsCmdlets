# This script is a psake script file and should not be called directly. Use ..\Build.ps1 instead.

Properties {

    Function Get-EscapedMSBuildArgument($arg)
    {
        return '"' + $arg.Replace('"', '\"') + '"'
    }

    # Source information
    $ProjectDir = Join-Path $SolutionDir 'PS'

    # Output destination
    $OutDir = Join-Path (Split-Path $SolutionDir -Parent) 'out'
    $ChocolateyDir = Join-Path $OutDir 'chocolatey'
    $MSIDir = Join-Path $OutDir 'msi'
    $NugetDir = Join-Path $OutDir 'nuget'
    $DocsDir = Join-Path $OutDir 'docs'
    $ModuleDir = Join-Path $OutDir 'module'
    $PortableDir = Join-Path $OutDir 'portable'
    $ModuleBinDir = (Join-Path $ModuleDir 'bin')

    # Module generation
    $ModuleManifestPath = Join-Path $ModuleDir 'TfsCmdlets.psd1'

    # Nuget packaging
    $NugetExePath = Join-Path $SolutionDir 'nuget.exe'
    $NugetPackagesDir = Join-Path $SolutionDir 'Packages'
    $NugetToolsDir = Join-Path $NugetDir 'Tools'
    $NugetSpecPath = Join-Path $NugetDir "TfsCmdlets.nuspec"
    $NugetPackageVersion = $VersionMetadata.LegacySemVer

    # Chocolatey packaging
    $ChocolateyToolsDir = Join-Path $ChocolateyDir 'tools'
    $ChocolateyInstallDir = Join-Path $NugetPackagesDir 'Chocolatey\tools\chocolateyInstall'
    $ChocolateyPath = Join-Path $ChocolateyInstallDir 'choco.exe'
    $ChocolateySpecPath = Join-Path $ChocolateyDir "TfsCmdlets.nuspec"

    # Wix packaging
    $WixVersion = "$Version"
    $WixOutputPath = Join-Path $SolutionDir "Setup\bin\$Configuration"

    #7zip
    $7zipExepath = Join-Path $SolutionDir '7za.exe'

}

Task Build -Depends GenerateModule {
    
}

Task Rebuild -Depends Clean, Build {

}

Task Package -Depends Build, PackageNuget, PackageChocolatey, PackageMSI, PackageDocs, PackageModule {

}

Task GenerateModule -Depends DownloadTfsNugetPackage {

    if (-not (Test-Path $ModuleDir -PathType Container)) { New-Item $ModuleDir -ItemType Directory -Force | Out-Null }

    $NestedModules = (Get-ChildItem $ProjectDir -Directory | % { "'$($_.Name)\$($_.Name).psm1'" }) -join ','
    $FileList = (Get-ChildItem $ProjectDir\*.* -Exclude '*.pssproj' | % { "'$($_.FullName.SubString($ProjectDir.Length+1))'" }) -join ','
    $TfsOmNugetVersion = (& $NugetExePath list -Source (Join-Path $NugetPackagesDir 'Microsoft.TeamFoundationServer.ExtendedClient'))

    # Copy root files to output dir
    foreach($f in (Get-ChildItem $ProjectDir\*.ps* -Exclude *.pssproj))
    {
        $f | Get-Content | Out-String | Replace-Token | Out-File (Join-Path $ModuleDir $f.Name) -Encoding Default
    }

    Copy-Item $ProjectDir\*.* -Destination $ModuleDir -Exclude *.pssproj, *.ps* 
   
    # Create sub-modules
    foreach($d in (Get-ChildItem $ProjectDir -Directory -Exclude bin))
    {
        $subModuleName = $d.Name
        $subModuleSrcDir = Join-Path $ProjectDir $subModuleName
        $subModuleOutDir = Join-Path $ModuleDir $subModuleName
        $subModuleOutFile = Join-Path $subModuleOutDir "$subModuleName.psm1"

        if (-not (Test-Path $subModuleOutDir -PathType Container)) { New-Item $subModuleOutDir -ItemType Directory -Force | Out-Null }

        if ($Configuration -eq 'Release')
        {
            # Merge individual files in a single module file
            Get-ChildItem $subModuleSrcDir\*.ps1 | Sort | Get-Content | Out-String | Replace-Token | Out-File $subModuleOutFile -Encoding Default
        }
        else
        {
            # Dot-source individual files in the module file
            Copy-Item $subModuleSrcDir\*.ps1 -Destination $subModuleOutDir -Container
            Get-ChildItem $subModuleOutDir\*.ps1 | Sort | % { ". $($_.FullName)`r`n" } | Out-File $subModuleOutFile -Encoding Default
        }
    }
}

Task DownloadTfsNugetPackage {

    & $NugetExePath Install Microsoft.TeamFoundationServer.ExtendedClient -ExcludeVersion -OutputDirectory packages -Verbosity Detailed | Write-Verbose

    $TargetDir = (Join-Path $ModuleDir 'Lib\')
   
    if (-not (Test-Path $TargetDir -PathType Container)) { New-Item $TargetDir -ItemType Directory -Force | Out-Null }
    
    foreach($d in (Get-ChildItem net4*, native -Directory -Recurse))
    {
        try
        { 
            foreach ($f in (Get-ChildItem $d\*.dll -Recurse -Exclude *.resources.dll))
            {
                $SrcPath = $f.FullName
                $DstPath = Join-Path $TargetDir $f.Name

                if (-not (Test-Path $DstPath))
                {
                    Copy-Item $SrcPath $DstPath
                }
            }
        } 
        finally 
        {}
    }
}

Task Clean {

    if (Test-Path $OutDir -PathType Container)
    {
        Remove-Item $OutDir -Recurse -Force -ErrorAction SilentlyContinue
    }
} 

Task PackageModule -Depends GenerateModule {

    if (-not (Test-Path $PortableDir -PathType Container)) { New-Item $PortableDir -ItemType Directory -Force | Out-Null }

    & $7zipExePath a (Join-Path $PortableDir "TfsCmdlets-Portable-$NugetPackageVersion.zip") (Join-Path $OutDir 'Module\*') | Write-Verbose
}

Task PackageNuget -Depends GenerateModule, GenerateNuspec {

    Copy-Item $ModuleDir $NugetToolsDir\TfsCmdlets -Recurse -Exclude *.ps1 -Force
    & $NugetExePath @('Pack', $NugetSpecPath, '-OutputDirectory', $NugetDir, '-Verbosity', 'Detailed', '-NonInteractive') | Write-Verbose
}

Task PackageChocolatey -Depends GenerateModule {

    if (-not (Test-Path $ChocolateyPath))
    {
        & $NugetExePath Install Chocolatey -ExcludeVersion -OutputDirectory packages -Verbosity Detailed | Write-Verbose
    }

    Copy-Item $ModuleDir $ChocolateyToolsDir\TfsCmdlets -Recurse -Force
    Copy-Item $NugetSpecPath -Destination $ChocolateyDir -Force
    & $ChocolateyPath Pack $ChocolateySpecPath -OutputDirectory $ChocolateyDir | Write-Verbose
}

Task BuildMSI {

    $WixProjectPath = Join-Path $SolutionDir 'Setup\TfsCmdlets.Setup.wixproj'
    $WixPackagesConfigFile = Join-Path $SolutionDir 'Setup\packages.config'
    $MSBuildArgs = """$WixProjectPath"" /p:WixProductVersion=$Version /p:WixFileVersion=$SemVer ""/p:WixProductName=$ModuleName - $ModuleDescription"" ""/p:WixAuthor='$ModuleAuthor"" /p:SourceDir=$ModuleDir\ /tv:$VisualStudioVersion.0"

    Write-Verbose "Restoring WiX Nuget package"

    & $NugetExePath Restore $WixPackagesConfigFile -PackagesDirectory $NugetPackagesDir | Write-Verbose

    Write-Verbose "Running MSBuild.exe with arguments [ $MSBuildArgs ]"

    exec { MSBuild.exe '--%' $MSBuildArgs } | Write-Verbose
}

Task PackageMSI -Depends BuildMSI {

    if(-not (Test-Path $MSIDir)) { New-Item $MSIDir -ItemType Directory | Out-Null }

    Copy-Item "$WixOutputPath\*.msi" -Destination $MSIDir -Force
}

Task PackageDocs -Depends GenerateDocs {

    #Compress-Archive -Path $DocsDir -CompressionLevel Optimal -DestinationPath (Join-Path $DocsDir "TfsCmdlets-docs-$NugetPackageVersion.zip") 
    & $7zipExePath a (Join-Path $DocsDir "TfsCmdlets-Docs-$NugetPackageVersion.zip") $DocsDir | Write-Verbose
}

Task GenerateDocs -Depends GenerateModule {

    if(-not (Test-Path $DocsDir)) { New-Item $DocsDir -ItemType Directory | Out-Null }

    ..\BuildDoc.ps1 -SourceDir $ModuleDir -OutputDir $DocsDir
}

Task GenerateNuspec {

    if(-not (Test-Path $NugetDir)) { New-Item $NugetDir -ItemType Directory | Out-Null }

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

Function Replace-Token
{
    [CmdletBinding()]
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [string]
        $InputObject
    )

    Begin
    {
        $Tokens = (Get-Content (Join-Path $SolutionDir 'Tokens.json') | ConvertFrom-Json).Tokens
    }

    Process
    {
        $m = $InputObject | Select-String -Pattern '\${(?<VarName>.+?)}' -AllMatches

        if (-not $m)
        {
            return $InputObject
        }

        $foundTokens = $m.Matches | % { $_.Groups[1].Value }
        $result = $InputObject

        foreach($t in $foundTokens)
        {
            if ($Tokens.$t)
            {
                $result = $result.Replace("$`{$t}", $Tokens.$t)
            }
            elseif ($VersionMetadata.$t)
            {
                $result = $result.Replace("`${$t}", $VersionMetadata.$t)
            }
            elseif (Get-Variable -Name $t)
            {
                $result = $result.Replace("`${$t}", (Get-Variable $t).Value)
            }
            else
            {
                throw "Invalid token ${$t}"
            }
        }

        return $result
    }
}
