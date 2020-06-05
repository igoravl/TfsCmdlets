---
layout: cmdlet
title: Disconnect-TfsTeam
description: Disconnects from the currently connected team.
parent: Connection
breadcrumbs: [Connection]
---
## Disconnect-TfsTeam
{: .no_toc}

Disconnects from the currently connected team.

```powershell

Disconnect-TfsTeam
```

### Table of Contents
{: .no_toc .text-delta}

1. TOC
{:toc}

-----

### Detailed Description 

The Disconnect-TfsTeam cmdlet removes the connection previously set by its counterpart [Connect-TfsTeam](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeam). Therefore, cmdlets relying on a "default team" as provided by "[Get-TfsTeam](https://tfscmdlets.dev/Cmdlets/Team/Get-TfsTeam) -Current" will no longer work after a call to this cmdlet, unless their -Team argument is provided or a new call to [Connect-TfsTeam](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeam) is made.

[Go to top](#disconnect-tfsteam)

### Related Topics



[Go to top](#disconnect-tfsteam)

