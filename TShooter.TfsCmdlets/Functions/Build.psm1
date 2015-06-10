<#
.Synopsis
   Sets up an additional build service in the current computer
.DESCRIPTION
   In Team Foundation Server, the Build Service is a Windows Service that is associated with a particular TFS Team Project Collection (since 2010). 
   
   Each Build Service support zero to one (0..1) Build Controllers and zero to n (0..n) Build Agents. 
   
   Each Build Agent is associated to a specific Build Controller but an Agent and its corresponding Controller don’t need to be on the same machine. 
   
   This topology lets you configure a continuous integration build that queues its builds on the Build Controller you’ve specified which then farms out the heavy lifting to any of the n agents it manages. This gives you an easy way to load balance your builds across a set of machines.
   
   There is a potential down-side, however, in that each build service can only service one particular project collection and you cannot install more than one build service in any given computer.

   This script allows you work around that limitation, setting up more than one build service in the same machine.

   IMPORTANT: This is not supported and should not be used in production environments.
#>
Function New-TfsBuildService
{
    Param
    (
        [Parameter(Mandatory=$true)]
        [ValidateStringNotNullOrEmpty()]
        [string]
        $CollectionName,

        [Parameter()]
        [string]
        $BuildServiceName = "TFSBuildServiceHost.2013",

        [Parameter()]
        [string]
        $ComputerName = $env:COMPUTERNAME,

        [Parameter()]
        [ValidateRange(1,65535)]
        [int]
        $Port = 9191,

        [Parameter(Mandatory=$true)]
        [System.Management.Automation.Credential()]
        [System.Management.Automation.PSCredential]
        $ServiceCredential = (Get-Credential),

        [Parameter()]
        [switch]
        $Force
    )

    Begin
    {
        if ($ComputerName -eq "localhost")
        {
            $ComputerName = $env:COMPUTERNAME
        }
        $tpc = Get-TfsTeamProjectCollection -Current
        $buildServer = $tpc.GetService([type]"Microsoft.TeamFoundation.Build.Client.IBuildServer")
    }

    Process
    {
        $ToolsVersion = 12
        $EnvVarName = "TFSBUILDSERVICEHOST.2013"
        $BuildEndpointTemplate = "Build/v5.0/Services"

        if (-not $TfsInstallationPath) 
        {
            $TfsInstallationPath = (Get-ItemProperty -Path "HKLM:\SOFTWARE\Microsoft\TeamFoundationServer\12.0").InstallPath
        }

        if (-not $BuildServiceName)
        {
            $BuildServiceName = "TfsBuildService-$collectionName"
        }

        $ServiceBinPath = "$TfsInstallationPath\Tools\TfsBuildServiceHost.exe /NamedInstance:$BuildServiceName"
        $ServiceDisplayName = "Visual Studio Team Foundation Build Service Host ($collectionName)"


        _CreateService $BuildServiceName $ServiceDisplayName $ServiceBinPath $Force.IsPresent
        _RegisterBuildServiceHost $TfsInstallationPath $Force.IsPresent
        _CreateBuildController
        _CreateBuildAgent
    }
}

Function Start-TfsBuild
{
	Param
	(
		[Parameter(Mandatory=$true, Position=0)]
		[object] 
		$BuildDefinition,

		[Parameter(ValueFromPipeline=$true, Mandatory=$true)]
		[object]
		[ValidateNotNull()]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
		$Project,

		[Parameter()]
        [object]
        $Collection,

		[Parameter()]
		[string]
		[ValidateSet("LatestOnQueue", "LatestOnBuild", "Custom")]
		$GetOption = "LatestOnBuild",

		[Parameter()]
		[string]
		$GetVersion,

		[Parameter()]
		[string]
		$DropLocation,

		[Parameter()]
        [hashtable]
        $Parameters
	)

	Process
	{

		$tp = _GetTeamProject $Project $Collection
		$tpc = $tp.Store.TeamProjectCollection

		$buildServer = $tpc.GetService([type]"Microsoft.TeamFoundation.Build.Client.IBuildServer")

		if ($BuildDefinition -is [Microsoft.TeamFoundation.Build.Client.IBuildDefinition])
		{
			$buildDef = $BuildDefinition
		}
		else
		{
			$buildDef = $buildServer.GetBuildDefinition($tp.Name, $BuildDefinition);
		}

		$req = $buildDef.CreateBuildRequest()
        $req.GetOption = [Microsoft.TeamFoundation.Build.Client.GetOption] $GetOption;

		if ($GetOption -eq "Custom")
		{
	        $req.CustomGetVersion = $GetVersion
		}

		if ($DropLocation)
		{
			$req.DropLocation = $DropLocation
		}

        $buildServer.QueueBuild($req)
	}
}

Function Get-TfsBuildQueue
{
	Param
	(
		[Parameter(Position=0)]
		[object] 
		$BuildDefinition = "*",

		[Parameter(ValueFromPipeline=$true)]
		[object]
		[ValidateNotNull()]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		if ($Project)
		{
			$tp = _GetTeamProject $Project $Collection
			$tpc = $tp.Store.TeamProjectCollection
			$tpName = $tp.Name
		}
		else
		{
			$tpName = "*"
			$tpc = Get-TfsTeamProjectCollection $Collection
		}

		$buildServer = $tpc.GetService([type]"Microsoft.TeamFoundation.Build.Client.IBuildServer")

		if ($BuildDefinition -is [Microsoft.TeamFoundation.Build.Client.IBuildDefinition])
		{
			$BuildDefinition = $BuildDefinition.Name
		}
		else
		{
			$buildDef = $buildServer.GetBuildDefinition($tpName, $BuildDefinition);
		}

		$query = $buildServer.CreateBuildQueueSpec($tpName, $BuildDefinition)
		
		$buildServer.QueryQueuedBuilds($query).QueuedBuilds
 	}
}

#======================
# Helper methods
#======================

Function _CreateService
{
    Param
    (
        $BuildServiceName,
        $ServiceDisplayName,
        $ServiceBinPath,
        $Force
    )

    if (Get-Service $BuildServiceName -ErrorAction SilentlyContinue)
    {
        if (-not $Force)
        {
            throw "Build Service $BuildServiceName is already registered. To re-register a build service, use the -Force switch"
        }

        sc.exe delete $BuildServiceName | Out-Null
    }

    $svc = New-Service -Name $BuildServiceName -DisplayName $ServiceDisplayName -BinaryPathName $ServiceBinPath
    
    sc.exe failure $BuildServiceName reset= 86400 actions= restart/60000 | Out-Null
}

Function _RegisterBuildServiceHost
{
    Param
    (
        $TfsInstallationPath,
        $Force
    )

    _LoadPrivateAssemblies $TfsInstallationPath

    $flags = [System.Reflection.BindingFlags]::NonPublic -bor [System.Reflection.BindingFlags]::SetProperty -bor [System.Reflection.BindingFlags]::Static
    [Microsoft.TeamFoundation.Build.Config.BuildServiceHostProcess].InvokeMember("NamedInstance", $flags, $null, $null, [object[]] $BuildServiceName)

    if ($buildServer.QueryBuildServiceHosts($BuildServiceName).Length -ne 0)
    {
        if (-not $Force)
        {
            throw "Build Service $BuildServiceName is already registered. To re-register a build service, use the -Force switch"
        }
        
        [Microsoft.TeamFoundation.Build.Config.BuildServiceHostUtilities]::Unregister($true, $true)
    }

    $port = _GetNextAvailablePort
    $endpoint = "http://$env:COMPUTERNAME:$port/$BuildEndpointTemplate"
    $serviceHost = $buildServer.CreateBuildServiceHost($BuildServiceName, $endpoint)
    $serviceHost.Save()

    $userName = $ServiceCredential.UserName
    $password = $ServiceCredential.GetNetworkCredential().Password

    [Microsoft.TeamFoundation.Build.Config.BuildServiceHostUtilities]::Register($serviceHost, $userName, $password)
}

Function _GetNextAvailablePort
{
    $RegistryPath = "HKLM:\SOFTWARE\Microsoft\VisualStudio\$ToolsVersion.0\TeamFoundation\Build"
    $PortsTaken = Get-ChildItem -Path $RegistryPath | Get-ItemProperty -Name Endpoint | ForEach { [uri] $_.Endpoint } | Select -ExpandProperty Port

    $StartPort = 9191
    $MaxPorts = $StartPort + $PortsTaken.Length

    for ($i = $StartPort; $i -le $MaxPorts; $i++)
    { 
        if ($PortsTaken -notcontains $i)
        {
            $Port = $i
            break
        }
    }

    return $Port
}

Function _LoadPrivateAssemblies
{
    Param
    (
        $TfsInstallationPath
    )

    $AssemblyPath = "$TfsInstallationPath\Tools\Microsoft.TeamFoundation.Build.Config.dll"
    Add-Type -Path $AssemblyPath
}

Function _GetRegValue
{
    Param
    (
        $KeyPath, $ValueName, $ComputerName
    )

    Process
    {
        if ((-not $ComputerName) -or ($ComputerName -eq $env:COMPUTERNAME))
        {
            return Get-ChildItem -Path $KeyPath | Get-ItemProperty -Name $ValueName
        }

        $KeyMapping = @{
            HKCU = "CurrentUser";
            HKLM = "LocalMachine"
        }

        $RootKey = ($KeyPath -split ":\")[0]
        $Path = ($KeyPath -split ":\")[1]
        
        $Reg = [Microsoft.Win32.RegistryKey]::OpenRemoteBaseKey($KeyMapping[$RootKey], $ComputerName)
        $RegKey= $Reg.OpenSubKey($Path)
        $Value = $RegKey.GetValue($ValueName)

        $RegKey.Close()
        $Reg.Close()

        return $Value
    }
}

Function _GetTeamProject
{
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Project])]
	Param ($Project, $Collection)

	if ($Project -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])
	{
		return $Project
	}

	return Get-TfsTeamProject -Name $Project -Collection $Collection
}
