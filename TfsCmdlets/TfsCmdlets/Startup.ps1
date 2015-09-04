$binDir = (Join-Path $PSScriptRoot 'lib')
$assemblyList = ''

foreach($a in Get-ChildItem $binDir)
{
     $assemblyList += "{""$($a.BaseName)"", @""$($a.FullName)""},`r`n"
}

Add-Type -Language CSharp -TypeDefinition @"
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TfsCmdlets
{
    public class AssemblyResolver
    {
        private static readonly Dictionary<string, string> _PrivateAssemblies = new Dictionary<string, string>
        {
            $assemblyList
        };

        public static void Register()
        {
            AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs e)
            {
                try
                {
                    return _PrivateAssemblies.ContainsKey(e.Name)
                        ? Assembly.LoadFrom(_PrivateAssemblies[e.Name])
                        : null;
                }
                catch
                {
                    return null;
                }
            };
        }
    }
}
"@

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

        "TFS ${tfsPrompt}$($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "
    }
}
'@ | Invoke-Expression

}

# Load basic TFS client assemblies
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Common'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.WorkItemTracking.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Build.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Git.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.VersionControl.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.SourceControl.WebApi'
