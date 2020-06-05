---
layout: cmdlet
title: Disconnect-TfsTeamProject
parent: Connection
grand_parent: Cmdlets
---
## Disconnect-TfsTeamProject
{: .no_toc}

Disconnects from the currently connected team project.

```powershell
Disconnect-TfsTeamProject
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

The Disconnect-TfsTeamProject cmdlet removes the connection previously set by its counterpart [Connect-TfsTeamProject](/Cmdlets/Connection/Connect-TfsTeamProject). Therefore, cmdlets relying on a "default team project" as provided by "[Get-TfsTeamProject](/Cmdlets/TeamProject/Get-TfsTeamProject) -Current" will no longer work after a call to this cmdlet, unless their -Project argument is provided or a new call to [Connect-TfsTeamProject](/Cmdlets/Connection/Connect-TfsTeamProject) is made.

[Go to top](#disconnect-tfsteamproject)

### Related Topics



[Go to top](#disconnect-tfsteamproject)
