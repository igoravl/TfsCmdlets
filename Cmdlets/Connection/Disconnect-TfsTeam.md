---
layout: cmdlet
title: Disconnect-TfsTeam
parent: Connection
grand_parent: Cmdlets
---
## Disconnect-TfsTeam
{: .no_toc}

Disconnects from the currently connected team.

```powershell
Disconnect-TfsTeam
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

The Disconnect-TfsTeam cmdlet removes the connection previously set by its counterpart [Connect-TfsTeam](/Cmdlets/Connection/Connect-TfsTeam). Therefore, cmdlets relying on a "default team" as provided by "[Get-TfsTeam](/Cmdlets/Team/Get-TfsTeam) -Current" will no longer work after a call to this cmdlet, unless their -Team argument is provided or a new call to [Connect-TfsTeam](/Cmdlets/Connection/Connect-TfsTeam) is made.

[Go to top](#disconnect-tfsteam)

### Related Topics



[Go to top](#disconnect-tfsteam)

