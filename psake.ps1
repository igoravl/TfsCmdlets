# This script is a psake script file and should not be called directly. Use Build.ps1 instead.
Framework '4.6.2'

Properties {

    # Source information
    $RepoCreationDate = Get-Date '2014-10-24'
    $ProjectDir = Join-Path $SolutionDir 'Module'
    $TestsDir = Join-Path $SolutionDir 'Tests'
    $ProjectBuildNumber = ((Get-Date) - $RepoCreationDate).Days
    $ProjectMetadataInfo = "$(Get-Date -Format 'yyyyMMdd').$ProjectBuildNumber"

    # Output destination
    $OutDir = Join-Path $SolutionDir 'out'
    $ChocolateyDir = Join-Path $OutDir 'chocolatey'
    $MSIDir = Join-Path $OutDir 'msi'
    $NugetDir = Join-Path $OutDir 'nuget'
    $DocsDir = Join-Path $OutDir 'docs'
    $ModuleDir = Join-Path $OutDir 'module'
    $PortableDir = Join-Path $OutDir 'portable'
    $ModuleBinDir = (Join-Path $ModuleDir 'bin')

    # Assembly generation
    $TargetFrameworks = @{DesktopTargetFramework = 'net462'; CoreTargetFramework = 'netcoreapp2.0'}

    # Module generation
    $ModuleManifestPath = Join-Path $ModuleDir 'TfsCmdlets.psd1'
    $CompatiblePSEditions = @('Desktop') #, 'Core')
    $TfsPackageNames = @('Microsoft.TeamFoundationServer.ExtendedClient', 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi', 'Microsoft.VisualStudio.Services.Release.Client')
    $Copyright = "(c) 2014 ${ModuleAuthor}. All rights reserved."
    
    # Nuget packaging
    $NugetExePath = Join-Path $SolutionDir 'BuildTools/nuget.exe'
    $NugetPackagesDir = Join-Path $SolutionDir 'Packages'
    $NugetToolsDir = Join-Path $NugetDir 'Tools'
    $NugetSpecPath = Join-Path $NugetDir "TfsCmdlets.nuspec"

    # Chocolatey packaging
    $ChocolateyToolsDir = Join-Path $ChocolateyDir 'tools'
    $ChocolateyInstallDir = Join-Path $NugetPackagesDir 'Chocolatey\tools\chocolateyInstall'
    $ChocolateyPath = Join-Path $ChocolateyInstallDir 'choco.exe'
    $ChocolateySpecPath = Join-Path $ChocolateyDir "TfsCmdlets.nuspec"

    # Wix packaging
    $FourPartVersion = "$($VersionMetadata.MajorMinorPatch).$BuildNumber"
    $WixOutputPath = Join-Path $SolutionDir "Setup\bin\$Configuration"

    #7zip
    $7zipExepath = Join-Path $SolutionDir 'BuildTools/7za.exe'

    #gpp
    $gppExePath = Join-Path $SolutionDir 'BuildTools/gpp.exe'
}

Task Rebuild -Depends Clean, Build {
}

Task Package -Depends Build, AllTests, RemoveEmptyFolders, PackageNuget, PackageChocolatey, PackageMSI, PackageDocs, PackageModule {
}

Task Build -Depends CleanOutputDir, BuildLibrary, CopyFiles, CopyLibraries, GenerateTypesXml, GenerateFormatXml, UpdateModuleManifest, UnitTests {
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

Task BuildLibrary {

    $LibSolutionDir = (Join-Path $SolutionDir 'Lib')
    $LibSolutionPath = (Join-Path $LibSolutionDir 'TfsCmdletsLib.sln')

    foreach($f in $TargetFrameworks.Values)
    {
        Write-Verbose "Build $f version of TfsCmdletsLib"
        Exec { dotnet publish $LibSolutionPath -c $Configuration -f $f --self-contained true /p:Version=$FourPartVersion /p:AssemblyVersion=$FourPartVersion /p:AssemblyInformationalVersion=$BuildName /v:d | Write-Verbose }
    }
}

Task CopyFiles -Depends CleanOutputDir, CopyStaticFiles, CopyLibraries {

    # Preprocess and copy PowerShell files to output dir

    Write-Verbose "Preprocessing and copying PowerShell files to output folder"

    $includeFiles = (Get-ChildItem (Join-Path $SolutionDir 'Include'))
    $writtenFiles = @()

    foreach ($input in (Get-ChildItem -Path $ProjectDir/* -Include *.ps1 -Recurse))
    {
        $inputs = @($input) + $includeFiles
        $outputPath = (Join-Path $ModuleDir $input.FullName.SubString($ProjectDir.Length + 1))

        if (_IsUpToDate $inputs $outputPath)
        {
            Write-Verbose "$outputPath is up-to-date; skipping"
            continue
        }

        Write-Verbose "Preprocessing $($input.FullName)"
        
        $data = (& $gppExePath --include HelpText.h --include Defaults.h -I Include +z `"$($input.FullName)`")

        $dirName = $input.Directory.FullName

        if ($dirName.Length -gt $ProjectDir.Length)
        {
            $dirName = $dirName.Substring($ProjectDir.Length + 1)
        }
        else
        {
            $dirName = 'Module'
        }

        if (($Configuration -eq 'Release') -and ($dirName -ne 'Module'))
        {
            # Merge files (Release)
            $outputPath = (Join-Path $ModuleDir "TfsCmdlets.psm1")
        }

        if ($writtenFiles -notcontains $outputPath)
        {
            if (Test-Path $outputPath)
            {
                Write-Verbose "Removing file $outputPath before writing"
                Remove-Item $outputPath -Force
            }
            $writtenFiles += $outputPath
        }

        Write-Verbose "Copying preprocessed contents to $outputPath"

        Add-Content -Path $outputPath -Value $data -Force
    }

    # Mark outputted files as read-only to prevent editing and eventual data loss during debugging sessions

    Get-ChildItem -Path $ModuleDir\* -Include *.ps1 -Recurse | ForEach-Object { $_.Attributes = 'ReadOnly' }
}

Task CopyStaticFiles {

    # Copy other module files to output dir

    Write-Verbose "Copying module files to output folder"

    Copy-Item -Path $ProjectDir\* -Destination $ModuleDir -Recurse -Force -Exclude *.ps1, *.yml
    Copy-Item -Path $SolutionDir\*.md -Destination $ModuleDir -Force
}

Task CopyLibraries {

    $TargetDir = (Join-Path $ModuleDir 'Lib')

    foreach($tf in $TargetFrameworks.Values)
    {
        $srcDir = Join-Path $SolutionDir "Lib/TfsCmdletsLib/bin/$Configuration/$tf/publish"
        $dstDir = Join-Path $TargetDir $tf

        if (-not (Test-Path $dstDir -PathType Container)) 
        {
            Write-Verbose "Creating output folder $dstDir"
            New-Item $dstDir -ItemType Directory -Force | Out-Null
        }
    
        Get-ChildItem (Join-Path $srcDir '*') -File | ForEach-Object { 
            try
            {
                $src = $_
                $dst = Join-Path $dstDir $f.Name

                # if(_IsUpToDate $src $dst) { continue }

                Write-Verbose "Copying $($_.Name) to $dstDir"
                Copy-Item $src -Destination $dstDir
            }
            catch
            {
                Write-Warning "$_"
            }
        }

        # Get-ChildItem (Join-Path $srcDir '*') -Directory -Exclude (Get-ChildItem (Join-Path $dstDir '*') -Directory | Select-Object -ExpandProperty Name) | ForEach-Object { 
        #     try
        #     {
        #         Write-Verbose "Copying $($_.Name) to $dstDir recursively"
        #         Copy-Item $_ -Destination $dstDir -Recurse
        #     }
        #     catch
        #     {
        #         Write-Warning "$_"
        #     }
        # }
    }
}
Task GenerateTypesXml {

    $outputFile = (Join-Path $ModuleDir 'TfsCmdlets.Types.ps1xml')
    $inputFiles = (Get-ChildItem (Join-Path $ProjectDir '_Types') -Include '*.yml')

    if (_IsUpToDate $inputFiles $outputFile)
    {
        Write-Verbose "Output file is up-to-date; skipping"
        return
    }

    Export-PsTypesXml -InputDirectory (Join-Path $ProjectDir '_Types') -DestinationFile $outputFile | Write-Verbose
}

Task GenerateFormatXml {

    $outputFile = (Join-Path $ModuleDir 'TfsCmdlets.Format.ps1xml')
    $inputFiles = (Get-ChildItem (Join-Path $ProjectDir '_Formats') -Recurse -Include '*.yml')

    if (_IsUpToDate $inputFiles $outputFile)
    {
        Write-Verbose "Output file is up-to-date; skipping"
        return
    }

    Export-PsFormatXml -InputDirectory (Join-Path $ProjectDir '_Formats') -DestinationFile $outputFile | Write-Verbose
}

Task UpdateModuleManifest {

    $fileList = (Get-ChildItem -Path $ModuleDir -File -Recurse -Exclude *.dll | Select-Object -ExpandProperty FullName | ForEach-Object { "$($_.SubString($ModuleDir.Length+1))" })
    $functionList = (Get-ChildItem -Path $ProjectDir -Directory | ForEach-Object { Get-ChildItem $_.FullName -Include *-*.ps1 -Recurse } | Select-Object -ExpandProperty BaseName | Sort-Object)
    $nestedModuleList = (Get-ChildItem -Path $ModuleDir -Directory | ForEach-Object { Get-ChildItem $_.FullName -Include *.ps1 -Recurse } | Select-Object -ExpandProperty FullName | ForEach-Object { "$($_.SubString($ModuleDir.Length+1))" })
    $depsJson = (Get-Content -Raw -Encoding Utf8 -Path (Get-ChildItem (Join-Path $SolutionDir 'Lib/TfsCmdletsLib.deps.json') -Recurse)[0] | ConvertFrom-Json)
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
        FileList             = $fileList
        FunctionsToExport    = $functionList
        ModuleVersion        = $FourPartVersion
        CompatiblePSEditions = $CompatiblePSEditions
        PrivateData          = $PrivateData
    }

    if ($nestedModuleList)
    {
        # Won't be available when building in Release
        $manifestArgs['NestedModules'] = $nestedModuleList
    }

    Update-ModuleManifest @manifestArgs

    Get-Content $ModuleManifestPath | Write-Verbose
}

Task UnitTests -PreCondition { -not $SkipTests }  {

    Exec { Invoke-Pester -Path $TestsDir -OutputFile (Join-Path $OutDir TestResults-Unit.xml) -OutputFormat NUnitXml -PesterOption (New-PesterOption -IncludeVSCodeMarker ) -Strict -Show ([Pester.OutputTypes]'Failed,Summary') -EnableExit:$IsCI -ExcludeTag Correctness, Integration, PSCore, PSDesktop }
}

Task AllTests -PreCondition { -not $SkipTests } {

    Write-Output '= PowerShell Core ='
    Exec { pwsh.exe -NoLogo -Command "Invoke-Pester -Path $TestsDir -OutputFile $(Join-Path $OutDir TestResults-Core.xml) -OutputFormat NUnitXml -PesterOption (New-PesterOption -IncludeVSCodeMarker) -Strict -Show ([Pester.OutputTypes]'Failed,Summary') $(if($IsCI) {return '-EnableExit'}) -ExcludeTag PSDesktop" }

    Write-Output '= Windows PowerShell ='
    Exec { powershell.exe -NoLogo -Command "Invoke-Pester -Path $TestsDir -OutputFile $(Join-Path $OutDir TestResults-Desktop.xml) -OutputFormat NUnitXml  -PesterOption (New-PesterOption -IncludeVSCodeMarker) -Strict -Show ([Pester.OutputTypes]'Failed,Summary') $(if($IsCI) {return '-EnableExit'}) -ExcludeTag PSCore" }
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
} 

Task PackageModule -Depends Build {

    if (-not (Test-Path $PortableDir -PathType Container)) { New-Item $PortableDir -ItemType Directory -Force | Out-Null }

    & $7zipExePath a (Join-Path $PortableDir "TfsCmdlets-Portable-$($VersionMetadata.NugetVersion).zip") (Join-Path $OutDir 'Module\*') | Write-Verbose
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

    $cmdLine = "$ChocolateyPath Pack $ChocolateySpecPath -OutputDirectory $ChocolateyDir --Version $($VersionMetadata.NugetVersion)"

    Write-Verbose "Command line: [$cmdLine]"

    Invoke-Expression $cmdLine *>&1 | Write-Verbose
}

Task PackageMsi -Depends Build {

    $WixProjectName = 'TfsCmdlets.Setup'
    $WixProjectFileName = "$WixProjectName.wixproj"
    $WixProjectDir = Join-Path $SolutionDir 'Setup'
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
        "-d`"PRODUCTNAME=$ModuleName - $ModuleDescription`"",
        "-d`"AUTHOR=$ModuleAuthor`"",
        "-dSourceDir=$ModuleDir\",
        "-dSolutionDir=$SolutionDir\",
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
    & (Join-Path $WixToolsDir 'Candle.exe') $CandleArgs *>&1 | Write-Verbose

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
    & (Join-Path $WixToolsDir 'Light.exe') $LightArgs *>&1 | Write-Verbose
    
    if (-not (Test-Path $MSIDir)) { New-Item $MSIDir -ItemType Directory | Out-Null }

    Copy-Item "$WixOutputPath\*.msi" -Destination $MSIDir -Force
}

Task PackageDocs -Depends GenerateDocs {

    #Compress-Archive -Path $DocsDir -CompressionLevel Optimal -DestinationPath (Join-Path $DocsDir "TfsCmdlets-docs-$($VersionMetadata.NugetVersion).zip") 
    & $7zipExePath a (Join-Path $DocsDir "TfsCmdlets-Docs-$($VersionMetadata.NugetVersion).zip") $DocsDir | Write-Verbose
}

Task GenerateDocs -Depends Build {

    # # . (Join-Path $SolutionDir '..\BuildDoc.ps1' -Resolve) 

    # if(-not (Test-Path $DocsDir)) { New-Item $DocsDir -ItemType Directory | Out-Null }

    # $subModules = Get-ChildItem $ModuleDir -Directory | Select-Object -ExpandProperty Name

    # # Magic callback that does the munging
    # $callback = {
    #     if ($args[0].Groups[0].Value.StartsWith('\')) {
    #         # Escaped tag; strip escape character and return
    #         $args[0].Groups[0].Value.Remove(0, 1)
    #     } else {
    #         # Look up the help and generate the Markdown
    #         ConvertCommandHelp (Get-Help $args[0].Groups[1].Value) $cmdList
    #     }
    # }

    # $i = 0
    # $re = [Regex]"\\?{%\s*(.*?)\s*%}"
    # $cmds = Get-Command -Module TfsCmdlets
    # $cmdList = $cmds | Select-Object -ExpandProperty Name
    # $cmdCount = $cmds.Count
    # $origBufSize = $Host.UI.RawUI.BufferSize
    # $expandedBufSize = New-Object Management.Automation.Host.Size (1000, 1000)

    # foreach($m in $subModules)
    # {
    #     if (-not (Test-Path $subModuleOutputDir -PathType Container))
    #     {
    #         New-Item $subModuleOutputDir -ItemType Directory | Out-Null
    #     }

    #     $subModuleCommands = Get-ChildItem (Join-Path $ModuleDir $m) -Filter '*-Tfs*.ps1' | Select-Object -ExpandProperty BaseName
    #     $subModuleOutputDir = Join-Path $DocsDir "doc\$m"

    #     foreach($c in $subModuleCommands)
    #     {
    #         $i++ 

    #         $cmd = Get-Command $c -Module TfsCmdlets

    #         Write-Verbose "Generating help for $m/$($cmd.Name) ($i of $cmdCount)"

    #         # $Host.UI.RawUI.BufferSize = $expandedBufSize

    #         # Generate the readme
    #         $readme = "{% $($cmd.Name) %}" | ForEach-Object { $re.Replace($_, $callback) }

    #         # Output to the appropriate stream
    #         $OutputFile = Join-Path $subModuleOutputDir "$c.md" 
    #         $utf8Encoding = New-Object System.Text.UTF8Encoding($false)
    #         [System.IO.File]::WriteAllLines($OutputFile, $readme, $utf8Encoding)

    #         Write-Verbose "Writing $OutputFile"

    #         # $Host.UI.RawUI.BufferSize = $origBufSize
    #     }
    # }
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
    
    Copy-Item $SolutionDir\LICENSE.md $outLicenseFile -Force -Recurse

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