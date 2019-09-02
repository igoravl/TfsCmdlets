[CmdletBinding()]
Param
(
    $SolutionDir,
    $Configuration,
    $BranchName,
    $ModuleName,
    $ModuleAuthor,
    $ModuleDescription,
    $Commit,
    $Version,
    $NuGetVersion,
    $PreRelease,
    $BuildName,
    $SemVer,
    $VersionMetadata,
    $SkipTests
)

# Variable initialization

# Source information
$script:RepoCreationDate = Get-Date '2014-10-24'
$script:ProjectDir = Join-Path $SolutionDir 'Module'
$script:TestsDir = Join-Path $SolutionDir 'Tests'
$script:ProjectBuildNumber = ((Get-Date) - $RepoCreationDate).Days
$script:ProjectMetadataInfo = "$(Get-Date -Format 'yyyyMMdd').$ProjectBuildNumber"

# Output destination
$script:OutDir = Join-Path $SolutionDir 'out'
$script:ChocolateyDir = Join-Path $OutDir 'chocolatey'
$script:MSIDir = Join-Path $OutDir 'msi'
$script:NugetDir = Join-Path $OutDir 'nuget'
$script:DocsDir = Join-Path $OutDir 'docs'
$script:ModuleDir = Join-Path $OutDir 'module'
$script:PortableDir = Join-Path $OutDir 'portable'
$script:ModuleBinDir = (Join-Path $ModuleDir 'bin')

# Module generation
$script:ModuleManifestPath = Join-Path $ModuleDir 'TfsCmdlets.psd1'
$script:CompatiblePSEditions = @('Desktop') #, 'Core')
$script:TfsPackageNames = @('Microsoft.TeamFoundationServer.ExtendedClient','Microsoft.VisualStudio.Services.ServiceHooks.WebApi')
$script:Copyright = "(c) 2014 ${ModuleAuthor}. All rights reserved."

# Nuget packaging
$script:NugetExePath = Join-Path $SolutionDir 'BuildTools/nuget.exe'
$script:NugetPackagesDir = Join-Path $SolutionDir 'Packages'
$script:NugetToolsDir = Join-Path $NugetDir 'Tools'
$script:NugetSpecPath = Join-Path $NugetDir "TfsCmdlets.nuspec"

# Chocolatey packaging
$script:ChocolateyToolsDir = Join-Path $ChocolateyDir 'tools'
$script:ChocolateyInstallDir = Join-Path $NugetPackagesDir 'Chocolatey\tools\chocolateyInstall'
$script:ChocolateyPath = Join-Path $ChocolateyInstallDir 'choco.exe'
$script:ChocolateySpecPath = Join-Path $ChocolateyDir "TfsCmdlets.nuspec"

# Wix packaging
$script:WixVersion = "$Version"
$script:WixOutputPath = Join-Path $SolutionDir "Setup\bin\$Configuration"

#7zip
$script:7zipExepath = Join-Path $SolutionDir 'BuildTools/7za.exe'

#gpp
$script:gppExePath =  Join-Path $SolutionDir 'BuildTools/gpp.exe'

# Meta-tasks

Task . Package

Task Rebuild Clean, Build

Task Package Build, Test, PackageNuget, PackageChocolatey, PackageMSI, PackageDocs, PackageModule

Task Build CleanModuleOutputDir, DownloadTfsNugetPackage, CopyContents, UpdateModuleManifest

Task CopyContents BuildLibrary, CopyFiles, CopyLibraries

# Actual tasks

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

Task CleanModuleOutputDir {

    Write-Verbose "Cleaning output path $ModuleDir"
    
    if (Test-Path $ModuleDir -PathType Container)
    { 
        Remove-Item $ModuleDir -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
    }

    New-Item $ModuleDir -ItemType Directory -Force | Out-Null
}

Task DownloadTfsNugetPackage {

    Write-Verbose "Restoring Azure DevOps Client API Nuget packages"

    $packageDir = (Join-Path $NugetPackagesDir $package)

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

Task BuildLibrary {

    $LibSolutionPath = (Join-Path $SolutionDir 'Lib/TfsCmdletsLib.sln')
    $TargetDir = (Join-Path $ModuleDir 'Lib')

    exec { msbuild $LibSolutionPath /t:Rebuild /p:Configuration=$Configuration /p:Version=$Version /p:AssemblyVersion=$Version /v:d | Write-Verbose }

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

            $LibBinDir = (Join-Path $SolutionDir "Lib/TfsCmdletsLib/bin/$Configuration")

            $f = Get-ChildItem $LibBinDir -Include TfsCmdletsLib.dll -Recurse

            Copy-Item $f -Destination $TargetDir
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

Task Test -If { -not $SkipTests } Build, {

    exec {Invoke-Pester -Path $TestsDir -OutputFile (Join-Path $OutDir TestResults.xml) -OutputFormat NUnitXml `
        -PesterOption (New-PesterOption -IncludeVSCodeMarker ) -Strict}
}

# Helper functions

Function _EnsureDir($dir)
{
    if(Test-Path $dir)
    {
        return
    }

    New-Item -ItemType Directory $dir | Write-Verbose
}