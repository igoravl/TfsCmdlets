# This script is a psake script file and should not be called directly. Use Build.ps1 instead.

Properties {

    Function Get-EscapedMSBuildArgument($arg)
    {
        return '"' + $arg.Replace('"', '\"') + '"'
    }

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

    # Module generation
    $ModuleManifestPath = Join-Path $ModuleDir 'TfsCmdlets.psd1'
    $CompatiblePSEditions = @('Desktop') #, 'Core')
    $TfsPackageNames = @('Microsoft.TeamFoundationServer.ExtendedClient','Microsoft.VisualStudio.Services.ServiceHooks.WebApi')
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
    $WixVersion = "$Version"
    $WixOutputPath = Join-Path $SolutionDir "Setup\bin\$Configuration"

    #7zip
    $7zipExepath = Join-Path $SolutionDir 'BuildTools/7za.exe'

    #gpp
    $gppExePath =  Join-Path $SolutionDir 'BuildTools/gpp.exe'
}

Task Rebuild -Depends Clean, Build {

}

Task Package -Depends Build, Test, PackageNuget, PackageChocolatey, PackageMSI, PackageDocs, PackageModule {

}

Task Build -Depends CleanOutputDir, DownloadTfsNugetPackage, CopyFiles, CopyLibraries, UpdateModuleManifest {

}

Task CleanOutputDir {

    Write-Verbose "Cleaning output path $ModuleDir"
    
    if (Test-Path $ModuleDir -PathType Container)
    { 
        Remove-Item $ModuleDir -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
    }

    New-Item $ModuleDir -ItemType Directory -Force | Out-Null
}

Task CopyFiles {

    # Copy other module files to output dir

    Write-Verbose "Copying module files to output folder"
    Copy-Item -Path $ProjectDir\* -Destination $ModuleDir -Recurse -Force -Exclude *.ps1 

    # Preprocess and copy PowerShell files to output dir

    Write-Verbose "Preprocessing and copying PowerShell files to output folder"

    Remove-Item -Path $ModuleDir\*.ps1 -Recurse -Force

    Get-ChildItem -Path $ProjectDir\* -Include *.ps1 -Recurse | ForEach-Object {

        $outputPath = (Join-Path $ModuleDir $_.FullName.SubString($ProjectDir.Length+1))
        Write-Verbose "Preprocessing $($_.FullName)"
        
        $data = (& $gppExePath --include HelpText.h --include Defaults.h -I Include +z `"$($_.FullName)`")

        $dirName = $_.Directory.FullName

        if($dirName.Length -gt $ProjectDir.Length)
        {
            $dirName = $dirName.Substring($ProjectDir.Length+1)
        }
        else
        {
            $dirName = 'Module'
        }

        if(($Configuration -eq 'Release') -and ($dirName -ne 'Module'))
        {
            # Merge files (Release)
            $outputPath = (Join-Path $ModuleDir "$dirName\$($dirName.Replace('\', '_')).ps1")
        }

        Write-Verbose "Copying preprocessed contents to $outputPath"

        Add-Content -Path $outputPath -Value $data -Force
    }

    # Mark outputted files as read-only to prevent editing and eventual data loss during debugging sessions

    Get-ChildItem -Path $ModuleDir\* -Include *.ps1 -Recurse | ForEach-Object { $_.Attributes = 'ReadOnly'}
}

Task CopyLibraries {

    Write-Verbose "Copying TFS Client Object Model assemblies to output folder $TargetDir"

    $TargetDir = (Join-Path $ModuleDir 'Lib')

    if (-not (Test-Path $TargetDir -PathType Container)) 
    {
        Write-Verbose "Creating output folder $TargetDir"
        New-Item $TargetDir -ItemType Directory | Out-Null
    }

    foreach($d in (Get-ChildItem net4*, native -Directory -Recurse))
    {
        try
        { 
            foreach ($f in (Get-ChildItem "$d\*.dll" -Recurse -Exclude *.resources.dll))
            {
                $SrcPath = $f.FullName
                $DstPath = Join-Path $TargetDir $f.Name

                if (Test-Path $DstPath)
                {
                    $SrcFileInfo = Get-ChildItem $SrcPath
                    $DstFileInfo = Get-ChildItem $DstPath

                    if($SrcFileInfo.VersionInfo.FileVersion -le $DstFileInfo.VersionInfo.FileVersion)
                    {
                        continue
                    }
                }

                Write-Verbose "Copying file $SrcPath to $DstPath"
                Copy-Item $SrcPath $DstPath -Force 
        }
        } 
        catch
        {
            Write-Warning "Error copying file $f to output folder $TargetDir : $_"
        }
        finally 
        {
        }
    }
}

Task UpdateModuleManifest {

    $fileList = (Get-ChildItem -Path $ModuleDir -File -Recurse | Select-Object -ExpandProperty FullName | ForEach-Object {"$($_.SubString($ModuleDir.Length+1))"})
    $functionList = (Get-ChildItem -Path $ProjectDir -Directory | ForEach-Object { Get-ChildItem $_.FullName -Include *-*.ps1 -Recurse } | Select-Object -ExpandProperty BaseName | Sort-Object)
    $nestedModuleList = (Get-ChildItem -Path $ModuleDir -Directory | ForEach-Object { Get-ChildItem $_.FullName -Include *.ps1 -Recurse } | Select-Object -ExpandProperty FullName | ForEach-Object {"$($_.SubString($ModuleDir.Length+1))"})
    $tfsOmNugetVersion = ((& $NugetExePath List $TfsPackageNames[0] -PreRelease) -split ' ')[1]
    
    Write-Verbose @"
Updating module manifest file $ModuleManifestPath with the following content:

{
    -Author '$ModuleAuthor'
    -CompanyName '$ModuleAuthor'
    -Copyright '$Copyright' 
    -Description '$ModuleDescription'
    -NestedModules @($(($nestedModuleList | ForEach-Object { "'$_'" }) -join ',')) 
    -FileList @($(($fileList | ForEach-Object { "'$_'" }) -join ',')) 
    -FunctionsToExport @($(($functionList | ForEach-Object { "'$_'" }) -join ',')) 
    -ModuleVersion '$Version' 
    -CompatiblePSEditions @($(($CompatiblePSEditions | ForEach-Object { "'$_'" }) -join ',')) 
    -PrivateData @{
        Branch = '$BranchName'
        Build = '$BuildName'
        Commit = '$Commit'
        TfsClientVersion = '$tfsOmNugetVersion'
        PreRelease = '$PreRelease'
    }
}
"@

    Update-ModuleManifest -Path $ModuleManifestPath `
        -NestedModules $nestedModuleList `
        -FileList $fileList `
        -FunctionsToExport $functionList `
        -ModuleVersion $Version `
        -CompatiblePSEditions $CompatiblePSEditions `
        -PrivateData @{
            Branch = $BranchName
            Build = $BuildName
            Commit = $Commit
            TfsClientVersion = $tfsOmNugetVersion
            PreRelease = $PreRelease
        }
}

Task Test -Depends Build {

    exec {Invoke-Pester -Path $TestsDir -OutputFile (Join-Path $OutDir TestResults.xml) -OutputFormat NUnitXml `
        -PesterOption (New-PesterOption -IncludeVSCodeMarker ) -Strict}
}

Task DownloadTfsNugetPackage {

    Write-Verbose "Restoring Nuget packages"

    $packageDir = (Join-Path $NugetPackagesDir $package)
    # $packages = @('Newtonsoft.Json','Microsoft.AspNet.WebApi.Core','Microsoft.AspNet.WebApi.Client') + $TfsPackageNames

    foreach($package in $TfsPackageNames) 
    {
        Write-Verbose "Restoring $package Nuget package (if needed)"

        $packageDir = (Join-Path $NugetPackagesDir $package)

        if (-not (Test-Path "$packageDir.*" -PathType Container))
        {
            Write-Verbose "$package not found. Downloading from Nuget.org"
            & $NugetExePath Install $package -OutputDirectory packages -Verbosity Detailed -PreRelease *>&1 | Write-Verbose
        }
        else
        {
            Write-Verbose "FOUND! Skipping..."
        }
    }
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

    & $7zipExePath a (Join-Path $PortableDir "TfsCmdlets-Portable-$NugetVersion.zip") (Join-Path $OutDir 'Module\*') | Write-Verbose
}

Task PackageNuget -Depends Build, GenerateNuspec {

    Copy-Item $ModuleDir $NugetToolsDir\TfsCmdlets -Recurse -Exclude *.ps1 -Force
    & $NugetExePath @('Pack', $NugetSpecPath, '-OutputDirectory', $NugetDir, '-Verbosity', 'Detailed', '-NonInteractive') *>&1 | Write-Verbose
}

Task PackageChocolatey -Depends Build {

    if (-not (Test-Path $ChocolateyPath))
    {
        & $NugetExePath Install Chocolatey -ExcludeVersion -OutputDirectory packages -Verbosity Detailed *>&1 | Write-Verbose
    }

    Copy-Item $ModuleDir $ChocolateyToolsDir\TfsCmdlets -Recurse -Force
    Copy-Item $NugetSpecPath -Destination $ChocolateyDir -Force
    & $ChocolateyPath Pack $ChocolateySpecPath -OutputDirectory $ChocolateyDir | Write-Verbose
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

    if(-not (Test-Path $WixObjDir)) { New-Item $WixObjDir -ItemType Directory | Write-Verbose }
    if(-not (Test-Path $WixBinDir)) { New-Item $WixBinDir -ItemType Directory | Write-Verbose }
    
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
        "-dPRODUCTVERSION=$WixVersion",
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
        "-dTargetFileName=$ModuleName-$NugetVersion.msi",
        "-dTargetName=$ModuleName-$NugetVersion",
        "-dTargetPath=$WixBinDir\$ModuleName-$NugetVersion.msi",
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
        "-out", "$WixBinDir\$ModuleName-$NugetVersion.msi",
        "-pdbout", "$WixBinDir\$ModuleName-$NugetVersion.wixpdb",
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
    
    if(-not (Test-Path $MSIDir)) { New-Item $MSIDir -ItemType Directory | Out-Null }

    Copy-Item "$WixOutputPath\*.msi" -Destination $MSIDir -Force
}

Task PackageDocs -Depends GenerateDocs {

    #Compress-Archive -Path $DocsDir -CompressionLevel Optimal -DestinationPath (Join-Path $DocsDir "TfsCmdlets-docs-$NugetVersion.zip") 
    & $7zipExePath a (Join-Path $DocsDir "TfsCmdlets-Docs-$NugetVersion.zip") $DocsDir | Write-Verbose
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

    if(-not (Test-Path $NugetDir)) { New-Item $NugetDir -ItemType Directory | Out-Null }

    $SourceManifest = Test-ModuleManifest -Path $ModuleManifestPath

    $nuspec = @"
<?xml version="1.0"?>
<package>
    <metadata>
        <id>$($SourceManifest.Name)</id>
        <title>$($SourceManifest.Name)</title>
        <version>$NugetVersion</version>
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
