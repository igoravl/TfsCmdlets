$scriptPath = Split-Path $MyInvocation.MyCommand.Path -Parent

Get-Module TfsCmdlets | Remove-Module

Import-Module (Join-Path $scriptPath 'out\module\TfsCmdlets.psd1')

$wiql = "SELECT [System.Id], [System.Title], [System.State], [System.AreaPath], [System.IterationPath], [System.AssignedTo] FROM WorkItems Where [System.WorkItemType] = 'Task' AND [System.State] = 'In progress' AND ([System.IterationPath] = 'NorthwindTraders\Release 1\Sprint 1' OR [System.IterationPath] = 'TfsSearch\Release 1\Sprint 1' OR [System.IterationPath] = 'FabrikamFiber\Release 1\Sprint 1' OR [System.IterationPath] = 'PartsUnlimited\Iteration 1' OR [System.IterationPath] = 'Archive\Release 1\Sprint 1' OR [System.IterationPath] = 'Personal\Release 1\Sprint 1' OR [System.IterationPath] = 'SAC\Release 1\Sprint 1' OR [System.IterationPath] = 'Tfs\Release 1\Sprint 1' OR [System.IterationPath] = 'OpenSource\Release 1\Sprint 1' OR [System.IterationPath] = 'NewProject\Sprint 2' OR [System.IterationPath] = 'AgileGit\Iteration 1' OR [System.IterationPath] = 'TDC2016\PI 1\Sprint 1' OR [System.IterationPath] = 'TDC2016\PI 1\Sprint 1' OR [System.IterationPath] = 'TDC2016\PI 1\Sprint 3' OR [System.IterationPath] = 'TDC2016\PI 1\Sprint 1' OR [System.IterationPath] = 'TDC2016\PI 1\Sprint 1' OR [System.IterationPath] = 'TDC2016\PI 1' OR [System.IterationPath] = 'TDC2016\Sprint 1' OR [System.IterationPath] = 'TDC2016\PI 1' OR [System.IterationPath] = 'TDC2016\PI 1\Sprint 1' OR [System.IterationPath] = 'TDC2016\PI 1\Sprint 4' OR [System.IterationPath] = 'BibliotecaRequisitos\Iteration 0')"

Connect-TfsTeamProjectCollection -Collection https://igoravl.visualstudio.com
Connect-TfsTeamProject -Project FabrikamFiber

New-TfsWorkItemQuery -Name 'WIP de todos os times' -Scope Personal -Folder 'My Queries/Teste' -Definition $wiql