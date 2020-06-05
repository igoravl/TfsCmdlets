---
layout: cmdlet
title: Disconnect-TfsTeamProject
description: Disconnects from the currently connected team project.
parent: Connection
breadcrumbs: [Connection]
---
## Disconnect-TfsTeamProject
{: .no_toc}

Disconnects from the currently connected team project.

```powershell

Disconnect-TfsTeamProject
```

### Table of Contents
{: .no_toc .text-delta}

1. TOC
{:toc}

-----

### Detailed Description 

The Disconnect-TfsTeamProject cmdlet removes the connection previously set by its counterpart [Connect-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProject). Therefore, cmdlets relying on a "default team project" as provided by "[Get-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/TeamProject/Get-TfsTeamProject) -Current" will no longer work after a call to this cmdlet, unless their -Project argument is provided or a new call to [Connect-TfsTeamProject](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsTeamProject) is made.

[Go to top](#disconnect-tfsteamproject)

### Related Topics



[Go to top](#disconnect-tfsteamproject)

