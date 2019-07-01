# This script file is used by the Visual Studio Code debugger to automatically load the PowerShell module
#
# DO NOT CHANGE ITS CONTENTS!
#
# Tips
#
# * _AutoRun_: If you want to have user code automatically executed at the start of your debugging session - 
#   e.g. to connect to an Azure DevOps Services organization whenever you hit F5 - you can add it to a file called 
#   TfsCmdlets_Debug_Profile.ps1, in your $<Documents>\WindowsPowerShell folder
#
# * _Function Breakpoints_: To set a breakpoint on a given function/cmdlet, type its name when prompted by VS Code
#   when starting a new debugging session. That gives you the flexibility to test a given function without hardcoding
#   breakpoints. If, on the other hand, you prefer to have hard-coded breakpoint(s) (so you don't need to type them 
#   every time you press F5), set a variable called $FunctionBreakpoints in your autorun file to an array containing a 
#   list of function names. For instance, to set a breakpoint on the functions Connect-TfsTeamProjectCollection every
#   time you start the debugger, add the following code to your TfsCmdlets_Debug_Profile.ps1:
#
#   $FunctionBreakpoints = @('Connect-TfsTeamProjectCollection')

Get-Module TfsCmdlets | Remove-Module; Import-Module (Join-Path $PSScriptRoot 'out\module\TfsCmdlets.psd1')

$autoRunScripts = @(
    "$PSScriptRoot/DebugAutoRun.ps1",
    "$([System.Environment]::GetFolderPath('MyDocuments'))/WindowsPowerShell/TfsCmdlets_Debug_Profile.ps1"
)

foreach($path in $autoRunScripts)
{
    if (Test-Path $path)
    {
        Write-Output "Loading TfsCmdlets autorun script from $path"
        try
        {
        . $path
        }
        catch
        {
            Write-Warning "Error loading autorun script ${path}: $_"
        }
    }
}

if(-not $FunctionBreakpoints)
{
    $FunctionBreakpoints = @()
}

$Args | ForEach-Object { $FunctionBreakpoints += $_ }

foreach($fn in $FunctionBreakpoints)
{
    Write-Output "Setting breakpoint on function $fn"
    [void] (Set-PSBreakpoint -Command $fn)
}

Get-Module TfsCmdlets | Select-Object Name, Version, @{L='Build';E={$_.PrivateData.Build}}
