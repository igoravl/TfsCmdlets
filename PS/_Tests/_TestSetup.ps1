BeforeAll { 

    $scriptRoot = $PSScriptRoot
    $solutionDir = Join-Path $scriptRoot '../..' -Resolve
    $outDir = Join-Path $solutionDir 'out' -Resolve
    $modulePath = Join-Path $outDir Module
    $manifestPath = Join-Path $modulePath 'TfsCmdlets.psd1'
    $hasBuild = Test-Path $modulePath

    $tfsAccessToken = $env:TFSCMDLETS_ACCESS_TOKEN
    $tfsCollectionUrl = $env:TFSCMDLETS_COLLECTION_URL

    if((-not $tfsAccessToken) -or (-not $tfsCollectionUrl))
    {
        throw 'Missing credentials. Please provide both TFSCMDLETS_ACCESS_TOKEN and TFSCMDLETS_COLLECTION_URL environment variables'
    }

    $global:tfsProject = 'TestProject'

    if (-not $hasBuild) {
        throw "Module TfsCmdlets not found at $modulePath. Build project prior to running tests."
    }

    $mod = Get-Module TfsCmdlets
    
    if ($mod) {
        $modPath = (Join-Path $mod.ModuleBase 'TfsCmdlets.psd1')
    }

    if ($mod -and ($modPath -ne $manifestPath)) {
        Write-Host "Removing module TfsCmdlets (loaded from $($modPath)) and loading from $manifestPath"
        Get-Module TfsCmdlets | Remove-Module -Force
        $mod = Get-Module TfsCmdlets
    }

    if (-not $mod) {
        Write-Host "Loading module TfsCmdlets from $manifestPath"
        #try { 
            Import-Module $manifestPath -Force #} catch {}
    }

    $conn = Get-TfsTeamProjectCollection -Current

    if(-not $conn -or ($conn.Uri -ne ([uri]$tfsCollectionUrl))) {
        Write-Host "Connecting to $tfsCollectionUrl"
        Connect-TfsTeamProjectCollection -Collection $tfsCollectionUrl -PersonalAccessToken $tfsAccessToken
    }

    $PSDefaultParameterValues['*:ErrorAction'] = 'Stop'
}

AfterAll {
    #Disconnect-TfsTeamProjectCollection
}
