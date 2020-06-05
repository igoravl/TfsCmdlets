---
layout: cmdlet
title: Disconnect-TfsTeamProjectCollection
parent: Connection
grand_parent: Cmdlets
---
## Disconnect-TfsTeamProjectCollection
{: .no_toc}

Disconnects from the currently connected TFS team project collection or Azure DevOps organization.

```powershell
Disconnect-TfsTeamProjectCollection
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

The Disconnect-TfsTeamProjectCollection cmdlet removes the connection previously set by its counterpart [Connect-TfsTeamProjectCollection](/Cmdlets/Connection/Connect-TfsTeamProjectCollection). Therefore, cmdlets relying on a "default collection" as provided by "[Get-TfsTeamProjectCollection](/Cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection) -Current" will no longer work after a call to this cmdlet, unless their -Collection argument is provided or a new call to [Connect-TfsTeam](/Cmdlets/Connection/Connect-TfsTeam) is made.

[Go to top](#disconnect-tfsteamprojectcollection)

### Related Topics



[Go to top](#disconnect-tfsteamprojectcollection)

