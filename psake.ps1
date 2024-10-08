# This script is a psake script file and should not be called directly. Use Build.ps1 instead.
Framework '4.7.1'

Properties {

    # Source information
    $RepoCreationDate = Get-Date '2014-10-24'
    $PSDir = Join-Path $RootProjectDir 'PS'
    $PSTestsDir = Join-Path $PSDir '_Tests'
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
    $TargetFrameworks = @{Desktop = 'net471'; Core = 'netcoreapp3.1' }

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
    $WixProjectDir = Join-Path $RootProjectDir 'Setup'
    $ThreePartVersion = $VersionMetadata.MajorMinorPatch
    $FourPartVersion = "$($VersionMetadata.MajorMinorPatch).$BuildNumber"
    $WixOutputPath = Join-Path $RootProjectDir "Setup\bin\$Configuration"

    # WinGet packaging
    $WinGetProjectDir = Join-Path $WixProjectDir 'winget'

    # Documentation generation
    $RootUrl = 'https://tfscmdlets.dev/docs/cmdlets'
}

Task Rebuild -Depends Clean, Build {
}

Task Package -Depends Build, GenerateDocs, AllTests, RemoveEmptyFolders, ValidateReleaseNotes, PackageNuget, PackageChocolatey, PackageMSI, PackageWinget, PackageDocs, PackageModule {
}

Task Build -Depends CleanOutputDir, CreateOutputDir, BuildLibrary, UnitTests, GenerateHelp, CopyFiles, GenerateTypesXml, GenerateFormatXml, GenerateNestedModule, UpdateModuleManifest {
}

Task Test -Depends UnitTests, AllTests {
}

Task CleanOutputDir -PreCondition { -not $Incremental } {

    Write-Verbose "Cleaning output path $ModuleDir"
    
    if (Test-Path $ModuleDir -PathType Container) { 
        Remove-Item $ModuleDir -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
    }

    Remove-Item $OutDir/*.log -Force -ErrorAction SilentlyContinue | Out-Null

    New-Item $ModuleDir -ItemType Directory -Force | Out-Null
}

Task CreateOutputDir {

    if (-not (Test-Path $OutDir -PathType Container)) {
        New-Item $OutDir -ItemType Directory | Write-Verbose
    }
}

Task BuildLibrary {

    foreach ($tfkey in $TargetFrameworks.Keys) {
        $tf = $TargetFrameworks[$tfkey]

        Remove-Item $OutDir/MSBuild_$tfkey.log -Force -ErrorAction SilentlyContinue
        Exec { dotnet publish "$SolutionDir/TfsCmdlets/TfsCmdlets.csproj" -o $ModuleDir/Lib/$tfkey -f $tf --self-contained true -c $Configuration /p:Version=$FourPartVersion /p:AssemblyVersion=$FourPartVersion /p:AssemblyInformationalVersion=$BuildName > $OutDir/MSBuild_$tfkey.log }
        Remove-Item $OutDir/MSBuild_$tfkey.log -Force -ErrorAction SilentlyContinue
    }

    Copy-Item (Join-Path $SolutionDir "TfsCmdlets/bin/$Configuration/$($TargetFrameworks.Desktop)/Microsoft.WITDataStore*.dll") (Join-Path $ModuleDir 'Lib/Desktop/') | Write-Verbose
}

Task GenerateHelp {

    $xmldocExePath = Join-Path $RootProjectDir 'BuildTools/XmlDoc2CmdletDoc/XmlDoc2CmdletDoc.exe'
    $helpFile = Join-Path $ModuleDir "TfsCmdlets.dll-Help.xml"
    $assemblyFile = Join-Path $ModuleDir "Lib/Desktop/TfsCmdlets.dll"

    exec { & $xmldocExePath $assemblyFile -out $helpFile -rootUrl $RootUrl | Write-Verbose }

    $helpContents = (Get-Content $helpFile -Raw -Encoding utf8)
    $helpTokens = (Invoke-Expression (Get-Content (Join-Path $RootProjectDir 'Docs/CommonHelpText.psd1') -Raw -Encoding utf8))

    foreach ($token in $helpTokens.GetEnumerator()) {
        $helpContents = $helpContents -replace $token.Key, $token.Value
    }

    $helpContents | Out-File $helpFile -Encoding utf8

    if ($helpContents -match 'HELP_[A-Z_]+') {
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
}

Task GenerateTypesXml {

    $outputFile = (Join-Path $ModuleDir 'TfsCmdlets.Types.ps1xml')
    $inputFiles = (Get-ChildItem (Join-Path $PSDir '_Types') -Include '*.yml')

    if (_IsUpToDate $inputFiles $outputFile) {
        Write-Verbose "Output file is up-to-date; skipping"
        return
    }

    Export-PsTypesXml -InputDirectory (Join-Path $PSDir '_Types') -DestinationFile $outputFile | Write-Verbose
}

Task GenerateFormatXml {

    $outputFile = (Join-Path $ModuleDir 'TfsCmdlets.Format.ps1xml')
    $inputFiles = (Get-ChildItem (Join-Path $PSDir '_Formats') -Recurse -Include '*.yml')

    if (_IsUpToDate $inputFiles $outputFile) {
        Write-Verbose "Output file is up-to-date; skipping"
        return
    }

    Export-PsFormatXml -InputDirectory (Join-Path $PSDir '_Formats') -DestinationFile $outputFile | Write-Verbose
}

Task GenerateNestedModule {

    $outputFile = (Join-Path $ModuleDir 'TfsCmdlets.psm1')

    foreach ($m in (Get-ChildItem (Join-Path $PSDir '_Private') -Recurse -Filter *.ps*)) {
        Get-Content $m.FullName -Encoding utf8 | Out-File $outputFile -Encoding utf8 -Append
    }
}

Task UpdateModuleManifest {

    Function GetExportedFunctionsList {
        $modulePath = (Join-Path $SolutionDir "TfsCmdlets/bin/$Configuration/$($TargetFrameworks.Desktop)/TfsCmdlets.dll")
        Get-Module TfsCmdlets | Remove-Module
        Import-Module $modulePath
        return @(
            Get-Command -Module TfsCmdlets -ErrorAction SilentlyContinue | 
            Where-Object { $_.Visibility -eq 'Public' } | 
            Sort-Object -Property Name |
            Select-Object -ExpandProperty Name)

    }

    Function GetFileList {
        return (
            Get-ChildItem -Path $ModuleDir -File -Recurse -Exclude *.resources.dll | 
            Select-Object -ExpandProperty FullName | 
            ForEach-Object { "$($_.SubString($ModuleDir.Length+1))" })
    }

    # $functionList = (Get-ChildItem -Path $PSDir -Directory | ForEach-Object { Get-ChildItem $_.FullName -Include *-*.ps1 -Recurse } | Select-Object -ExpandProperty BaseName | Sort-Object)
    $depsJson = (Get-Content -Raw -Encoding Utf8 -Path (Get-ChildItem (Join-Path $ModuleDir 'Lib/Core/TfsCmdlets.deps.json') -Recurse)[0] | ConvertFrom-Json)
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
        FileList             = (GetFileList)
        NestedModules        = @('TfsCmdlets.psm1')
        FunctionsToExport    = @()
        CmdletsToExport      = (GetExportedFunctionsList)
        ModuleVersion        = $ThreePartVersion
        CompatiblePSEditions = $CompatiblePSEditions
        PrivateData          = $PrivateData
        ReleaseNotes         = "For release notes, see https://github.com/igoravl/TfsCmdlets/blob/master/RELEASENOTES.md"
    }

    Update-ModuleManifest @manifestArgs

    # Set RootModule manually to inject conditional loading logic

    $rootModuleExpr = 'RootModule = if($PSEdition -eq "Core") { "Lib/Core/TfsCmdlets.dll" } else { "Lib/Desktop/TfsCmdlets.dll" }'
    $manifestText = (Get-Content $ModuleManifestPath -Raw) -replace "RootModule = '.+?'", $rootModuleExpr
    $manifestText | Out-File $ModuleManifestPath -Encoding utf8

    Write-Verbose $manifestText
}

Task UnitTests -PreCondition { -not $SkipTests } {

    Remove-Item $OutDir/TfsCmdlets.Tests.UnitTests.log -Force -ErrorAction SilentlyContinue
    #Exec { dotnet test $SolutionDir/TfsCmdlets.Tests.UnitTests/TfsCmdlets.Tests.UnitTests.csproj -f $TargetFrameworks.Core --filter "Platform!=Desktop&Platform!=Core" --logger:"console;verbosity=detailed" --logger "trx;LogFileName=$OutDir/TfsCmdlets.Tests.UnitTests.trx" > $OutDir/TfsCmdlets.Tests.UnitTests.log }
}

Task AllTests -PreCondition { -not $SkipTests } {

    Push-Location $PSTestsDir

    $outputLevel =if($isCi) { "Detailed" } else { "Minimal" }

    try {
        Write-Output ' == PowerShell Core =='
        Exec { pwsh.exe -NonInteractive -NoLogo -Command "Invoke-Pester -CI -Output $outputLevel -ExcludeTagFilter 'Desktop', 'Server'" }
        Move-Item 'testResults.xml' -Destination $OutDir/TestResults-Pester-Core.xml -Force
        Move-Item 'coverage.xml' -Destination $OutDir/Coverage-Pester-Core.xml -Force
    
        Write-Output ' == PowerShell Desktop =='
        Exec { powershell.exe -NonInteractive -NoLogo -Command "Invoke-Pester -CI -Output $outputLevel -ExcludeTagFilter 'Core', 'Server'" }
        Move-Item 'testResults.xml' -Destination $OutDir/TestResults-Pester-Desktop.xml -Force
        Move-Item 'coverage.xml' -Destination $OutDir/Coverage-Pester-Desktop.xml -Force
    }
    finally {
        Pop-Location
    }
}

Task RemoveEmptyFolders {

    Get-ChildItem $ModuleDir -Recurse -Force -Directory | 
    Sort-Object -Property FullName -Descending |
    Where-Object { $($_ | Get-ChildItem -Force | Select-Object -First 1).Count -eq 0 } |
    Remove-Item | Write-Verbose
}

Task Clean {

    if (Test-Path $OutDir -PathType Container) {
        Write-Verbose "Removing $OutDir..."
        Remove-Item $OutDir -Recurse -Force -ErrorAction SilentlyContinue
    }

    if (Test-Path $NugetPackagesDir -PathType Container) {
        Write-Verbose "Removing $NugetPackagesDir..."
        Remove-Item $NugetPackagesDir -Recurse -Force -ErrorAction SilentlyContinue
    }

    Get-ChildItem (Join-Path $SolutionDir '*/bin') -Directory | Remove-Item -Recurse
    Get-ChildItem (Join-Path $SolutionDir '*/obj') -Directory | Remove-Item -Recurse

} 

Task ValidateReleaseNotes -PreCondition { -not $SkipReleaseNotes }  {

    $path = Join-Path $RootProjectDir "Docs/ReleaseNotes/${ThreePartVersion}.md"

    if(-not (Test-Path $path -PathType Leaf)) {
        throw "Release notes file '$path' not found"
    }
    
    $releaseNotes = Get-Content $path -Encoding utf8
    $topVersionLine = $releaseNotes[2]

    if($topVersionLine -notlike "*$ThreePartVersion*") {
        throw "File '$path' does not contain the release notes for version $threePartVersion."
    }

    $releaseNotes = (Get-Content (Join-Path $RootProjectDir 'RELEASENOTES.md') -Encoding utf8)
    $topVersionLine = $releaseNotes[2]

    if($topVersionLine -notmatch "$ThreePartVersion") {
        throw "File 'RELEASENOTES.md' does not contain the release notes for version $threePartVersion"
    }
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

    if (-not (Test-Path $ChocolateyPath)) {
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

Task PackageMsi { #-Depends Build {

    $WixProjectName = 'TfsCmdlets.Setup'
    $WixProjectFileName = Join-Path $WixProjectDir "$WixProjectName.wixproj"

    Copy-Item -Path $RootProjectDir\License.rtf -Destination $ModuleDir -Force
    Copy-Item -Path $RootProjectDir\Assets\*.bmp -Destination $ModuleDir -Force

    exec { 
        dotnet build $WixProjectFileName -o $WixOutputPath -v d `
            -p WixSourceDir=$ModuleDir\ `
            -p WixProductVersion=$ThreePartVersion `
            -p WixFileVersion=$($VersionMetadata.NugetVersion) `
            -p SolutionDir=$RootProjectDir\ `
            -p Configuration=$Configuration `
            -p OutDir=$WixBinDir `
            -p Platform=x86 `
            -p ProjectDir=$WixProjectDir\ `
            -p ProjectExt=.wixproj `
            -p ProjectFileName=$WixProjectFileName `
            -p ProjectName=$WixProjectNam `
            -p ProjectPath=$WixProjectDir\$WixProjectFileName `
            -p TargetDir=$WixBinDir\ `
            -p TargetExt=.ms `
            -p TargetFileName=$ModuleName-$($VersionMetadata.NugetVersion).msi `
            -p TargetName=$ModuleName-$($VersionMetadata.NugetVersion) `
            -p TargetPath=$WixBinDir\$ModuleName-$($VersionMetadata.NugetVersion).msi `
        *>&1 | Write-Verbose
    }
    
    if (-not (Test-Path $MSIDir)) { New-Item $MSIDir -ItemType Directory | Out-Null }

    Copy-Item "$WixOutputPath\*.msi" -Destination $MSIDir -Force
}

Task PackageWinget -Depends PackageMsi {

    Function GetMsiProperty($Path, $Property) {
        try {
            $windowsInstaller = New-Object -ComObject 'WindowsInstaller.Installer'
            $database = $windowsInstaller.GetType().InvokeMember('OpenDatabase', 'InvokeMethod', $null, $windowsInstaller, @((Get-Item -Path $Path).FullName, 0))
            $view = $database.GetType().InvokeMember('OpenView', 'InvokeMethod', $null, $database, ("SELECT Value FROM Property WHERE Property = '$Property'"))
            $view.GetType().InvokeMember('Execute', 'InvokeMethod', $null, $view, $null)
            $record = $view.GetType().InvokeMember('Fetch', 'InvokeMethod', $null, $view, $null)
            $value = ($record.GetType().InvokeMember('StringData', 'GetProperty', $null, $record, 1) -Join '').Replace(' ', '')

            $view.GetType().InvokeMember('Close', 'InvokeMethod', $null, $view, $null)

            return $value
        } 
        catch {
            Write-Error -Message $_.ToString()
        }
        finally {
            if ($windowsInstaller) {
                [Void][System.Runtime.InteropServices.Marshal]::ReleaseComObject($windowsInstaller)
            }
        }
    }

    $MsiPath = (Join-Path $MSIDir "$ModuleName-$($VersionMetadata.NugetVersion).msi")
    $WinGetOutDir = (Join-Path $OutDir "winget/manifests/i/Igoravl/TfsCmdlets/$ThreePartVersion")
    $MsiHash = (Get-FileHash -Algorithm SHA256 -Path $MsiPath).Hash
    $MsiProductCode = "$(GetMsiProperty -Path $MsiPath -Property 'ProductCode')".Replace(' ', '')

    New-Item -Path $WinGetOutDir -ItemType Directory -Force | Write-Verbose

    foreach ($f in (Get-ChildItem $WinGetProjectDir -File)) {
        $outputPath = (Join-Path $WinGetOutDir $f.Name)
        Get-Content $f.FullName -Raw | Invoke-Expression | Out-File $outputPath -Force
    }
}

Task PackageDocs -Depends GenerateDocs {

    Compress-Archive -DestinationPath (Join-Path $DocsDir "TfsCmdlets-Docs-$($VersionMetadata.NugetVersion).zip") -Path $DocsDir/* -Force | Write-Verbose
}

Task GenerateDocs {

    exec { powershell.exe -NoProfile -File (Join-Path $RootProjectDir 'BuildDoc.ps1') }
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

    if (-not (Test-Path $ChocolateyToolsDir -PathType Container)) {
        New-Item $ChocolateyToolsDir -Force -ItemType Directory | Write-Verbose
    }
    
    Copy-Item $RootProjectDir\LICENSE.md $outLicenseFile -Force -Recurse

    $specFiles = Get-ChildItem $NugetPackagesDir -Include *.nuspec -Recurse

    foreach ($f in $specFiles) {
        $spec = [xml] (Get-Content $f -Raw -Encoding UTF8)
        $packageUrl = "https://nuget.org/packages/$($spec.package.metadata.id)/$($spec.package.metadata.version)"

        if ($spec.package.metadata.license) {
            if ($spec.package.metadata.license.type -eq 'file') {
                $licenseFile = Join-Path $f.Directory $spec.package.metadata.license.'#text'
                $licenseText = Get-Content $licenseFile -Raw -Encoding Utf8
            }
            else {
                $licenseText = "Please refer to https://spdx.org/licenses/$($spec.package.metadata.license.type).html for license information for this package."
            }
        }
        else {
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

This package is published by the TfsCmdlets Project itself. Any binaries will be 
identical to other package types published by the project.

Third-party binary files contained in this package are .NET assembly references 
and can be compared against their respective NuGet source packages.

$($packageUrls -join "`r`n")

"@ | Out-File $outVerifyFile -Encoding Utf8

}

Function _IsUpToDate($Inputs, $Output) {
    if (-not $Incremental -or (-not (Test-Path $Output))) {
        return $false
    }

    if ($Output -isnot [System.IO.FileSystemInfo]) {
        $Output = Get-ChildItem $Output
    }
    
    foreach ($input in $Inputs) {
        if ($input -isnot [System.IO.FileSystemInfo]) {
            $input = Get-ChildItem $input
        }

        if ($input.LastWriteTimeUtc -gt $Output.LastWriteTimeUtc) {
            return $false
        }
    }

    return $true
}