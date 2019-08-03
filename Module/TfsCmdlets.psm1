_Log "Loading module TfsCmdlets from $PSScriptRoot"

# Initialize variables

$script:IsDesktop = ($PSEdition -ne 'Core')
$script:IsCore = -not $script:IsDesktop

# Configure assembly resolver

_RegisterAssemblyResolver

# Load essential assemblies

_ImportRequiredAssembly "*"

# _ImportRequiredAssembly 'Newtonsoft.Json'
# _ImportRequiredAssembly 'Microsoft.TeamFoundation.Client'
# _ImportRequiredAssembly 'Microsoft.VisualStudio.Services.WebApi'

# $runspace = [runspacefactory]::CreateRunspace()
# $pipeline = $runspace.CreatePipeline('_ImportRequiredAssembly "*"')
# $pipeline.Input.Close()
# $pipeline.InvokeAsync()

# # Load remaining assemblies asynchronously

# $libPath = Join-Path $PSScriptRoot 'lib'

# # $delegate = [Action[object]] {

# $assemblies = (Get-ChildItem "$libPath/*.dll" -Exclude 'Microsoft.WitDataStore*.*').BaseName
# Write-Verbose "Loading $($assemblies.Count) private assemblies"
        
# foreach($asm in $assemblies)
# {
#     Write-Verbose "Loading assembly $asm from folder $libPath"

#     try
#     {
#         Add-Type -Path (Join-Path $libPath "$asm.dll")
#     }
#     catch
#     {
#         Write-Warning ($_.Exception.LoaderExceptions | ConvertTo-Json -Depth 2)
#     }
# }
