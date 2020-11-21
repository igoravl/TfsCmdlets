# This script is a psake script file and should not be called directly. Use Build.ps1 instead.
Framework '4.6.2'

Properties {

    # Source information
    $RepoCreationDate = Get-Date '2014-10-24'
    $PSDir = Join-Path $RootProjectDir 'PS'
    $SolutionDir = Join-Path $RootProjectDir 'CSharp'
    $TestsDir = Join-Path $RootProjectDir 'Tests'
    $ProjectBuildNumber = ((Get-Date) - $RepoCreationDate).Days
    $ProjectMetadataInfo = "$(Get-Date -Format 'yyyyMMdd').$ProjectBuildNumber"

    # Output destination
    $OutDir = Join-Path $RootProjectDir 'out'
    $ChocolateyDir = Join-Path $OutDir 'chocolatey'
    $MSIDir = Join-Path $OutDir 'msi'
    $NugetDir = Join-Path $OutDir 'nuget'
    $DocsDir = Join-Path $OutDir 'docs'
    $ModuleDir = Join-Path $OutDir 'module'
    $PortableDir = Join-Path $OutDir 'portable'
    $ModuleBinDir = Join-Path $ModuleDir 'bin'

    # Assembly generation
    $TargetFrameworks = @{DesktopTargetFramework = 'net462'; CoreTargetFramework = 'netcoreapp3.1'}

    # Module generation
    $ModuleManifestPath = Join-Path $ModuleDir 'TfsCmdlets.psd1'
    $CompatiblePSEditions = @('Desktop', 'Core')
    $TfsPackageNames = @('Microsoft.TeamFoundationServer.ExtendedClient', 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi', 'Microsoft.VisualStudio.Services.Release.Client')
    $Copyright = "(c) 2014 ${ModuleAuthor}. All rights reserved."
    
    # Nuget packaging
    $NugetExePath = Join-Path $RootProjectDir 'BuildTools/nuget.exe'
    $NugetPackagesDir = Join-Path $RootProjectDir 'Packages'
    $NugetToolsDir = Join-Path $NugetDir 'Tools'
    $NugetSpecPath = Join-Path $NugetDir "TfsCmdlets.nuspec"

    # Chocolatey packaging
    $ChocolateyToolsDir = Join-Path $ChocolateyDir 'tools'
    $ChocolateyInstallDir = Join-Path $NugetPackagesDir 'Chocolatey\tools\chocolateyInstall'
    $ChocolateyPath = Join-Path $ChocolateyInstallDir 'choco.exe'
    $ChocolateySpecPath = Join-Path $ChocolateyDir "TfsCmdlets.nuspec"

    # Wix packaging
    $ThreePartVersion = $VersionMetadata.MajorMinorPatch
    $FourPartVersion = "$($VersionMetadata.MajorMinorPatch).$BuildNumber"
    $WixOutputPath = Join-Path $RootProjectDir "Setup\bin\$Configuration"

    # Documentation generation
    $RootUrl = 'https://tfscmdlets.dev/Cmdlets'
}

Task Rebuild -Depends Clean, Build {
}

Task Package -Depends Build, AllTests, RemoveEmptyFolders, PackageNuget, PackageChocolatey, PackageMSI, PackageDocs, PackageModule {
}

Task Build -Depends CleanOutputDir, CreateOutputDir, BuildLibrary, GenerateHelp, CopyFiles, GenerateTypesXml, GenerateFormatXml, UpdateModuleManifest, UnitTests {
}

Task Test -Depends Build, UnitTests, AllTests {
}

Task CleanOutputDir -PreCondition { -not $Incremental } {

    Write-Verbose "Cleaning output path $ModuleDir"
    
    if (Test-Path $ModuleDir -PathType Container)
    { 
        Remove-Item $ModuleDir -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
    }

    New-Item $ModuleDir -ItemType Directory -Force | Out-Null
}

Task CreateOutputDir {

    if(-not (Test-Path $OutDir -PathType Container))
    {
        New-Item $OutDir -ItemType Directory | Write-Verbose
    }
}

Task BuildLibrary {

    foreach($p in @('Core', 'Desktop'))
    {
        Write-Verbose "Build TfsCmdlets.PS$p"
        try
        {
            Exec { dotnet publish "$SolutionDir/TfsCmdlets.PS$p/TfsCmdlets.PS$p.csproj" --self-contained true -c $Configuration -p:PublishDir="../../out/Module/Lib/$p" /p:Version=$FourPartVersion /p:AssemblyVersion=$FourPartVersion /p:AssemblyInformationalVersion=$BuildName > $OutDir/MSBuild.log }
        }
        catch
        {
            Get-Content $OutDir/MSBuild.log 
            throw "Error building solution"
        }
    }

    Copy-Item (Join-Path $SolutionDir "TfsCmdlets.PSDesktop/bin/$Configuration/net462/Microsoft.WITDataStore*.dll") (Join-Path $ModuleDir 'Lib/Desktop/')
}

Task GenerateHelp {

    $xmldocExePath = Join-Path $RootProjectDir 'BuildTools/XmlDoc2CmdletDoc/XmlDoc2CmdletDoc.exe'
    $helpFile = Join-Path $SolutionDir "TfsCmdlets.PSDesktop/bin/$Configuration/net462/TfsCmdlets.PSDesktop.dll-Help.xml"

    exec { & $xmldocExePath `
        "`"$SolutionDir\TfsCmdlets.PSDesktop\bin\$Configuration\net462\TfsCmdlets.PSDesktop.dll`"" `
        -out "`"$helpFile`"" -rootUrl `"$RootUrl`" | Write-Verbose }

    $helpContents = (Get-Content $helpFile -Raw -Encoding utf8)
    $helpTokens = (Invoke-Expression (Get-Content (Join-Path $RootProjectDir 'Docs/CommonHelpText.psd1') -Raw -Encoding utf8))

    foreach($token in $helpTokens.GetEnumerator())
    {
        $helpContents = $helpContents -replace $token.Key, $token.Value
    }

    $helpContents | Out-File $helpFile -Encoding utf8

    if($helpContents -match 'HELP_[A-Z_]+')
    {
        Write-Warning 'Undefined tokens found in documentation:'
        Get-Content $helpFile -Encoding utf8 | Select-String -Pattern 'HELP_[A-Z_]+' | Write-Warning
    }
}

Task CopyFiles -Depends CleanOutputDir, CopyStaticFiles {

}

Task CopyStaticFiles {

    Write-Verbose "Copying module files to output folder"

    Copy-Item -Path $PSDir\* -Destination $ModuleDir -Recurse -Force -Exclude _*
    Copy-Item -Path $RootProjectDir\*.md -Destination $ModuleDir -Force

    foreach($p in @('Core', 'Desktop'))
    {
        Get-ChildItem -Path (Join-Path $SolutionDir "TfsCmdlets.PSDesktop/bin/$Configuration/net462/TfsCmdlets.PSDesktop.dll-Help.xml") -Recurse | Copy-Item -Destination (Join-Path $ModuleDir "TfsCmdlets.PS${p}.dll-Help.xml") -Force
    }
}

Task GenerateTypesXml {

    $outputFile = (Join-Path $ModuleDir 'TfsCmdlets.Types.ps1xml')
    $inputFiles = (Get-ChildItem (Join-Path $PSDir '_Types') -Include '*.yml')

    if (_IsUpToDate $inputFiles $outputFile)
    {
        Write-Verbose "Output file is up-to-date; skipping"
        return
    }

    Export-PsTypesXml -InputDirectory (Join-Path $PSDir '_Types') -DestinationFile $outputFile | Write-Verbose
}

Task GenerateFormatXml {

    $outputFile = (Join-Path $ModuleDir 'TfsCmdlets.Format.ps1xml')
    $inputFiles = (Get-ChildItem (Join-Path $PSDir '_Formats') -Recurse -Include '*.yml')

    if (_IsUpToDate $inputFiles $outputFile)
    {
        Write-Verbose "Output file is up-to-date; skipping"
        return
    }

    Export-PsFormatXml -InputDirectory (Join-Path $PSDir '_Formats') -DestinationFile $outputFile | Write-Verbose
}

Task UpdateModuleManifest {

    # $fileList = (Get-ChildItem -Path $ModuleDir -File -Recurse -Exclude *.dll | Select-Object -ExpandProperty FullName | ForEach-Object { "$($_.SubString($ModuleDir.Length+1))" })
    # $functionList = (Get-ChildItem -Path $PSDir -Directory | ForEach-Object { Get-ChildItem $_.FullName -Include *-*.ps1 -Recurse } | Select-Object -ExpandProperty BaseName | Sort-Object)
    # $nestedModuleList = (Get-ChildItem -Path $ModuleDir -Directory | ForEach-Object { Get-ChildItem $_.FullName -Include *.ps1 -Recurse } | Select-Object -ExpandProperty FullName | ForEach-Object { "$($_.SubString($ModuleDir.Length+1))" })
    $depsJson = (Get-Content -Raw -Encoding Utf8 -Path (Get-ChildItem (Join-Path $ModuleDir 'Lib/Core/TfsCmdlets.PSCore.deps.json') -Recurse)[0] | ConvertFrom-Json)
    $tfsOmNugetVersion = (($depsJson.libraries | Get-Member | Where-Object Name -Like 'Microsoft.VisualStudio.Services.Client/*').Name -split '/')[1]

    $PrivateData = @{
        Branch           = $VersionMetadata.BranchName
        Build            = $BuildName
        Commit           = $VersionMetadata.Sha
        TfsClientVersion = $tfsOmNugetVersion
        PreRelease       = $VersionMetadata.NugetPrereleaseTag
        Version          = $VersionMetadata.FullSemVer
    } + $TargetFrameworks

    $manifestArgs = @{
        Path                 = $ModuleManifestPath
        # FileList             = $fileList
        FunctionsToExport    = @()
        ModuleVersion        = $ThreePartVersion
        CompatiblePSEditions = $CompatiblePSEditions
        PrivateData          = $PrivateData
        ReleaseNotes     = "For release notes, see https://github.com/igoravl/TfsCmdlets/blob/master/RELEASENOTES.md"
    }

    if ($nestedModuleList)
    {
        # Won't be available when building in Release
        $manifestArgs['NestedModules'] = $nestedModuleList
    }

    Update-ModuleManifest @manifestArgs

    # Set RootModule manually to inject conditional loading logic

    $rootModuleExpr = 'RootModule = if($PSEdition -eq "Core") { "Lib/Core/TfsCmdlets.PSCore.dll" } else { "Lib/Desktop/TfsCmdlets.PSDesktop.dll" }'
    $manifestText = (Get-Content $ModuleManifestPath -Raw) -replace "RootModule = '.+?'", $rootModuleExpr
    $manifestText | Out-File $ModuleManifestPath -Encoding utf8

    Write-Verbose $manifestText
}

Task UnitTests -PreCondition { -not $SkipTests }  {

    #Exec { Invoke-Pester -Path $TestsDir -OutputFile (Join-Path $OutDir TestResults-Unit.xml) -OutputFormat NUnitXml -PesterOption (New-PesterOption -IncludeVSCodeMarker ) -Strict -Show ([Pester.OutputTypes]'Failed,Summary') -EnableExit:$IsCI -ExcludeTag Correctness, Integration, PSCore, PSDesktop }
}

Task AllTests -PreCondition { -not $SkipTests } {

    # Write-Output '= PowerShell Core ='
    # Exec { pwsh.exe -NoLogo -Command "Invoke-Pester -Path $TestsDir -OutputFile $(Join-Path $OutDir TestResults-Core.xml) -OutputFormat NUnitXml -PesterOption (New-PesterOption -IncludeVSCodeMarker) -Strict -Show ([Pester.OutputTypes]'Failed,Summary') $(if($IsCI) {return '-EnableExit'}) -ExcludeTag PSDesktop" }

    # Write-Output '= Windows PowerShell ='
    # Exec { powershell.exe -NoLogo -Command "Invoke-Pester -Path $TestsDir -OutputFile $(Join-Path $OutDir TestResults-Desktop.xml) -OutputFormat NUnitXml  -PesterOption (New-PesterOption -IncludeVSCodeMarker) -Strict -Show ([Pester.OutputTypes]'Failed,Summary') $(if($IsCI) {return '-EnableExit'}) -ExcludeTag PSCore" }
}

Task RemoveEmptyFolders {

    Get-ChildItem $ModuleDir -Recurse -Force -Directory | 
    Sort-Object -Property FullName -Descending |
    Where-Object { $($_ | Get-ChildItem -Force | Select-Object -First 1).Count -eq 0 } |
    Remove-Item | Write-Verbose
}

Task Clean {

    if (Test-Path $OutDir -PathType Container)
    {
        Write-Verbose "Removing $OutDir..."
        Remove-Item $OutDir -Recurse -Force -ErrorAction SilentlyContinue
    }

    if (Test-Path $NugetPackagesDir -PathType Container)
    {
        Write-Verbose "Removing $NugetPackagesDir..."
        Remove-Item $NugetPackagesDir -Recurse -Force -ErrorAction SilentlyContinue
    }

    Get-ChildItem (Join-Path $SolutionDir '*/bin') -Directory | Remove-Item -Recurse
    Get-ChildItem (Join-Path $SolutionDir '*/obj') -Directory | Remove-Item -Recurse

} 

Task PackageModule -Depends Build {

    if (-not (Test-Path $PortableDir -PathType Container)) { New-Item $PortableDir -ItemType Directory -Force | Out-Null }

    Compress-Archive -Path (Join-Path $OutDir 'Module\*') -DestinationPath (Join-Path $PortableDir "TfsCmdlets-Portable-$($VersionMetadata.NugetVersion).zip") -Force | Write-Verbose
}

Task PackageNuget -Depends Build, GenerateNuspec {

    Copy-Item $ModuleDir $NugetToolsDir\TfsCmdlets -Recurse -Exclude *.ps1 -Force

    $cmdLine = "$NugetExePath Pack $NugetSpecPath -OutputDirectory $NugetDir -Verbosity Detailed -NonInteractive -Version $($VersionMetadata.NugetVersion)"

    Write-Verbose "Command line: [$cmdLine]"

    Invoke-Expression $cmdLine *>&1 | Write-Verbose
}

Task PackageChocolatey -Depends PackageNuget, GenerateLicenseFile, GenerateVerificationFile {

    if (-not (Test-Path $ChocolateyPath))
    {
        & $NugetExePath Install Chocolatey -ExcludeVersion -OutputDirectory packages -Verbosity Detailed *>&1 | Write-Verbose
    }

    Copy-Item $ModuleDir $ChocolateyToolsDir\TfsCmdlets -Recurse -Force
    Copy-Item $NugetSpecPath -Destination $ChocolateyDir -Force

    $NugetSpecPath = (Join-Path $ChocolateyDir (Split-Path $NugetSpecPath -Leaf))
    $nuspec = (Get-Content $NugetSpecPath -Encoding utf8 -Raw)
    $nuspecXml = ([xml] $nuspec)

    $chocoExtras = @"
    <docsUrl>https://tfscmdlets.dev</docsUrl> 
    <bugTrackerUrl>https://github.com/igoravl/TfsCmdlets/issues</bugTrackerUrl>
    <projectSourceUrl>$($nuspecXml.package.metadata.projectUrl)</projectSourceUrl>
    <packageSourceUrl>$($nuspecXml.package.metadata.projectUrl)</packageSourceUrl>
"@
    
    Set-Content $NugetSpecPath -Encoding utf8 -Value ($nuspec.Replace('<!-- choco-extras -->', $chocoExtras))

    $cmdLine = "$ChocolateyPath Pack $ChocolateySpecPath -OutputDirectory $ChocolateyDir --Version $($VersionMetadata.NugetVersion)"

    Write-Verbose "Command line: [$cmdLine]"

    Invoke-Expression $cmdLine *>&1 | Write-Verbose
}

Task PackageMsi -Depends Build {

    $WixProjectName = 'TfsCmdlets.Setup'
    $WixProjectFileName = "$WixProjectName.wixproj"
    $WixProjectDir = Join-Path $RootProjectDir 'Setup'
    $WixToolsDir = Join-Path $NugetPackagesDir 'Wix\Tools'
    $WixObjDir = (Join-Path $WixProjectDir 'obj\Release')
    $WixBinDir = (Join-Path $WixProjectDir 'bin\Release')
    $WixSuppressedWarnings = '1076'

    Write-Verbose "Restoring WiX Nuget package"

    if (-not (Test-Path $WixToolsDir))
    {
        & $NugetExePath Install Wix -ExcludeVersion -OutputDirectory packages -Verbosity Detailed *>&1 | Write-Verbose
    }

    if (-not (Test-Path $WixObjDir)) { New-Item $WixObjDir -ItemType Directory | Write-Verbose }
    if (-not (Test-Path $WixBinDir)) { New-Item $WixBinDir -ItemType Directory | Write-Verbose }

    Copy-Item -Path $RootProjectDir\License.rtf -Destination $ModuleDir -Force
    Copy-Item -Path $RootProjectDir\Assets\*.bmp -Destination $ModuleDir -Force
    
    $HeatArgs = @(
        'dir', $ModuleDir,
        "-cg", "ModuleComponent",
        "-dr", "ModuleFolder",
        "-scom",
        "-sreg",
        "-srd",
        "-var", "var.SourceDir",
        "-v",
        "-ag",
        "-sfrag",
        "-sw$WixSuppressedWarnings",
        "-out", "$WixObjDir\_ModuleComponent_dir.wxs"
    )
    Write-Verbose "Heat.exe $($HeatArgs -join ' ')"
    & (Join-Path $WixToolsDir 'Heat.exe') $HeatArgs *>&1 | Write-Verbose

    $CandleArgs = @(
        "-sw$WixSuppressedWarnings",
        "-dPRODUCTVERSION=$FourPartVersion",
        "-d`"PRODUCTNAME=$ModuleName`"",
        "-d`"DESCRIPTION=$ModuleDescription`"",
        "-d`"AUTHOR=$ModuleAuthor`"",
        "-dSourceDir=$ModuleDir\",
        "-dSolutionDir=$RootProjectDir\",
        "-dConfiguration=$Configuration"
        "-dOutDir=$WixBinDir\"
        "-dPlatform=x86",
        "-dProjectDir=$WixProjectDir\",
        "-dProjectExt=.wixproj",
        "-dProjectFileName=$WixProjectFileName",
        "-dProjectName=$WixProjectName"
        "-dProjectPath=$WixProjectDir\$WixProjectFileName",
        "-dTargetDir=$WixBinDir\",
        "-dTargetExt=.msi"
        "-dTargetFileName=$ModuleName-$($VersionMetadata.NugetVersion).msi",
        "-dTargetName=$ModuleName-$($VersionMetadata.NugetVersion)",
        "-dTargetPath=$WixBinDir\$ModuleName-$($VersionMetadata.NugetVersion).msi",
        "-I$WixProjectDir",
        "-out", "$WixObjDir\",
        "-arch", "x86",
        "-ext", "$WixToolsDir\WixUtilExtension.dll",
        "-ext", "$WixToolsDir\WixUIExtension.dll",
        "$WixProjectDir\Product.wxs",
        "$WixObjDir\_ModuleComponent_dir.wxs"
    ) 
    Write-Verbose "Candle.exe $($CandleArgs -join ' ')"
    Exec { & (Join-Path $WixToolsDir 'Candle.exe') $CandleArgs *>&1 | Write-Verbose }

    $LightArgs = @(
        "-out", "$WixBinDir\$ModuleName-$($VersionMetadata.NugetVersion).msi",
        "-pdbout", "$WixBinDir\$ModuleName-$($VersionMetadata.NugetVersion).wixpdb",
        "-sw1076",
        "-cultures:null", 
        "-ext", "$WixToolsDir\WixUtilExtension.dll",
        "-ext", "$WixToolsDir\WixUIExtension.dll",
        "-sval"
        "-contentsfile", "$WixObjDir\$WixProjectFileName.BindContentsFileListnull.txt",
        "-outputsfile", "$WixObjDir\$WixProjectFileName.BindOutputsFileListnull.txt"
        "-builtoutputsfile", "$WixObjDir\$WixProjectFileName.BindBuiltOutputsFileListnull.txt"
        "-wixprojectfile", "$WixProjectDir$WixProjectFileName", "$WixObjDir\Product.wixobj", "$WixObjDir\_ModuleComponent_dir.wixobj"
    )
    Write-Verbose "Light.exe $($LightArgs -join ' ')"
    Exec { & (Join-Path $WixToolsDir 'Light.exe') $LightArgs *>&1 | Write-Verbose }
    
    if (-not (Test-Path $MSIDir)) { New-Item $MSIDir -ItemType Directory | Out-Null }

    Copy-Item "$WixOutputPath\*.msi" -Destination $MSIDir -Force
}

Task PackageDocs -Depends GenerateDocs {

    Compress-Archive -DestinationPath (Join-Path $DocsDir "TfsCmdlets-Docs-$($VersionMetadata.NugetVersion).zip") -Path $DocsDir -Force | Write-Verbose
}

Task GenerateDocs {

    exec { powershell.exe -NoProfile -File (Join-Path $RootProjectDir 'BuildDoc.ps1')}

}

Task GenerateNuspec {

    if (-not (Test-Path $NugetDir)) { New-Item $NugetDir -ItemType Directory | Out-Null }

    $SourceManifest = Test-ModuleManifest -Path $ModuleManifestPath

    $nuspec = @"
<?xml version="1.0"?>
<package>
    <metadata>
        <id>$($SourceManifest.Name)</id>
        <title>$($SourceManifest.Name)</title>
        <summary>TfsCmdlets is a PowerShell module which provides many commands ("cmdlets" in PowerShell parlance) to simplify automated interaction with Azure DevOps (Server 2019+ and Services) and Team Foundation Server.</summary>
        <version>0.0.0</version>
        <authors>$($SourceManifest.Author)</authors>
        <owners>$($SourceManifest.Author)</owners>
        <licenseUrl>$($SourceManifest.LicenseUri)</licenseUrl>
        <projectUrl>$($SourceManifest.ProjectUri)</projectUrl>
        <iconUrl>$($SourceManifest.IconUri)</iconUrl>
        <requireLicenseAcceptance>false</requireLicenseAcceptance>
        <description>$($SourceManifest.Description)</description>
        <releaseNotes><![CDATA[$($SourceManifest.ReleaseNotes)]]></releaseNotes>
        <copyright>$($SourceManifest.Copyright)</copyright>
        <tags>$($SourceManifest.Tags -Join ' ')</tags>
        <!-- choco-extras -->
    </metadata>
</package>
"@

    Set-Content -Path $NugetSpecPath -Value $nuspec
}

Task GenerateLicenseFile {
 
    $outLicenseFile = Join-Path $ChocolateyToolsDir 'LICENSE.txt'

    if (-not (Test-Path $ChocolateyToolsDir -PathType Container))
    {
        New-Item $ChocolateyToolsDir -Force -ItemType Directory | Write-Verbose
    }
    
    Copy-Item $RootProjectDir\LICENSE.md $outLicenseFile -Force -Recurse

    $specFiles = Get-ChildItem $NugetPackagesDir -Include *.nuspec -Recurse

    foreach ($f in $specFiles)
    {
        $spec = [xml] (Get-Content $f -Raw -Encoding UTF8)
        $packageUrl = "https://nuget.org/packages/$($spec.package.metadata.id)/$($spec.package.metadata.version)"

        if ($spec.package.metadata.license)
        {
            if ($spec.package.metadata.license.type -eq 'file')
            {
                $licenseFile = Join-Path $f.Directory $spec.package.metadata.license.'#text'
                $licenseText = Get-Content $licenseFile -Raw -Encoding Utf8
            }
            else
            {
                $licenseText = "Please refer to https://spdx.org/licenses/$($spec.package.metadata.license.type).html for license information for this package."
            }
        }
        else
        {
            $licenseUrl = $spec.package.metadata.licenseUrl
            $licenseText = "Please refer to $licenseUrl for license information for this package."
        }

        @"
=============================

FROM $packageUrl

LICENSE

$licenseText

"@ | Add-Content -Path $outLicenseFile -Encoding Utf8

    }
}

Task GenerateVerificationFile {

    $outVerifyFile = Join-Path $ChocolateyToolsDir 'VERIFICATION.txt'

    $packageUrls = Get-ChildItem $NugetPackagesDir -Include *.nuspec -Recurse | ForEach-Object {
        $spec = [xml] (Get-Content $_ -Raw -Encoding UTF8); `
            Write-Output "https://nuget.org/packages/$($spec.package.metadata.id)/$($spec.package.metadata.version)"
    }

    @"
VERIFICATION
============

Verification is intended to assist the Chocolatey moderators and community
in verifying that this package's contents are trustworthy.

Binary files contained in this package can be compared against their respective NuGet source packages, listed below:

$($packageUrls -join "`r`n")

"@ | Out-File $outVerifyFile -Encoding Utf8

}

Function _IsUpToDate($Inputs, $Output)
{
    if (-not $Incremental -or (-not (Test-Path $Output)))
    {
        return $false
    }

    if ($Output -isnot [System.IO.FileSystemInfo])
    {
        $Output = Get-ChildItem $Output
    }
    
    foreach ($input in $Inputs)
    {
        if ($input -isnot [System.IO.FileSystemInfo])
        {
            $input = Get-ChildItem $input
        }

        if ($input.LastWriteTimeUtc -gt $Output.LastWriteTimeUtc)
        {
            return $false
        }
    }

    return $true
}