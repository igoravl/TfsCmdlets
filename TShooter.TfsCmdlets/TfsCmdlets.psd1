#
# Module manifest for module 'TfsCmdlets'
#
@{

# Script module or binary module file associated with this manifest.
RootModule = 'TfsCmdlets.psm1'

# Version number of this module.
ModuleVersion = '1.0.0'

# ID used to uniquely identify this module
GUID = 'bd4390dc-a8ad-4bce-8d69-f53ccf8e4163'

# Author of this module
Author = 'Igor Abade V. Leite (T-Shooter)'

# Company or vendor of this module
CompanyName = 'T-Shooter'

# Copyright statement for this module
Copyright = '(c) 2014 Igor Abade V. Leite. All rights reserved.'

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '3.0'

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @(
	'Functions\Build.psm1', 
	'Functions\Collection.psm1', 
	'Functions\ConfigServer.psm1', 
	'Functions\Css.psm1', 
	'Functions\GlobalList.psm1', 
	'Functions\ProcessTemplate.psm1', 
	'Functions\Team.psm1', 
	'Functions\TeamProject.psm1', 
	'Functions\WorkItem.psm1' )

# Functions to export from this module
FunctionsToExport = '*-Tfs*'

# Format files to process
FormatsToProcess = "TfsCmdlets.Format.ps1xml"

# Type files to process
TypesToProcess = "TfsCmdlets.Types.ps1xml"

# Aliases to export from this module
AliasesToExport = '*'

# List of assemblies to load
RequiredAssemblies = @(
	'Microsoft.TeamFoundation.Common, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a', 
	'Microsoft.TeamFoundation.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a', 
	'Microsoft.TeamFoundation.WorkItemTracking.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a', 
	'Microsoft.TeamFoundation.Build.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' )

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
# FileList = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess
# PrivateData = ''

# HelpInfo URI of this module
# HelpInfoURI = ''
}
