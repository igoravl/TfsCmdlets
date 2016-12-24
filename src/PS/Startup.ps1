$binDir = (Join-Path $PSScriptRoot 'lib')
$assemblyList = ''

foreach($a in Get-ChildItem $binDir)
{
     $assemblyList += "{""$($a.BaseName)"", @""$($a.FullName)""},`r`n"
}

if (-not ([System.Management.Automation.PSTypeName]'TfsCmdlets.AssemblyResolver').Type)
{
    Add-Type -ErrorAction SilentlyContinue -Language CSharp -TypeDefinition @"
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;

    namespace TfsCmdlets
    {
        public class AssemblyResolver
        {
            private static bool IsVerbose = ("$VerbosePreference" == "Continue");

            public static readonly Dictionary<string, string> PrivateAssemblies = new Dictionary<string, string>
            {
                $assemblyList
            };

            public static readonly Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();
            public static readonly Dictionary<string, object> LogEntries = new Dictionary<string, object>();

            public static void Register()
            {
                AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs e)
                {
                    try
                    {
                        var assemblyName = e.Name.Split(',')[0];
                        var isInternal = PrivateAssemblies.ContainsKey(assemblyName);

                        if (IsVerbose) Log("[INFO ] [" + (isInternal? "Internal": "External") + "] " + assemblyName, e);

                        return PrivateAssemblies.ContainsKey(assemblyName)
                            ? LoadAssembly(assemblyName)
                            : null;
                    }
                    catch(Exception ex)
                    {
                        LogError(ex);
                        return null;
                    }
                };
            }

            private static Assembly LoadAssembly(string assemblyName)
            {
                var assembly = Assembly.LoadFrom(PrivateAssemblies[assemblyName]);

                LoadedAssemblies.Add(assemblyName, assembly);

                return assembly;
            }

            private static void Log(string message, object data)
            {
                message = "[" + (LogEntries.Count+1).ToString("00000") + "] [" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + message;

                LogEntries.Add(message, data);
            }

            private static void LogError(Exception ex)
            {
                Log("[ERROR] " + ex.Message, ex);
            }
        }
    }
"@
}

[TfsCmdlets.AssemblyResolver]::Register()

# Initialize Shell

if ($Host.UI.RawUI.WindowTitle -eq "Team Foundation Server Shell")
{
    # SetConsoleColors
    $Host.UI.RawUI.BackgroundColor = "DarkMagenta"
    $Host.UI.RawUI.ForegroundColor = "White"
    Clear-Host

    # ShowBanner
    $module = Test-ModuleManifest -Path (Join-Path $PSScriptRoot 'TfsCmdlets.psd1')
    Write-Host "TfsCmdlets: $($module.Description)"
    Write-Host "Version $($module.PrivateData.Build)"
    Write-Host ""

    @'
Function Prompt
{
    Process
    {
        $tfsPrompt = ''

        if ($global:TfsServerConnection)
        {
            $tfsPrompt = $global:TfsServerConnection.Name

            if ($global:TfsTpcConnection)
            {
                $tfsPrompt += "/$($global:TfsTpcConnection.Name)"
            }

            if ($global:TfsProjectConnection)
            {
                $tfsPrompt += "/$($global:TfsProjectConnection.Name)"
            }

            if ($global:TfsTeamConnection)
            {
                $tfsPrompt += "/$($global:TfsTeamConnection.Name)"
            }

            $tfsPrompt = "[$tfsPrompt] "
        }

        "TFS $($tfsPrompt)$($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "
    }
}
'@ | Invoke-Expression

}

# Load basic TFS client assemblies
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Common.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.WorkItemTracking.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Build.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Git.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.VersionControl.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Core.WebApi.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.SourceControl.WebApi.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.VisualStudio.Services.WebApi.dll')
