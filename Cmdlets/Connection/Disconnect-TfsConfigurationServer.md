---
layout: cmdlet
title: Disconnect-TfsConfigurationServer
parent: Connection
grand_parent: Cmdlets
---
## Disconnect-TfsConfigurationServer
{: .no_toc}

Disconnects from the currently connected configuration server.

```powershell
Disconnect-TfsConfigurationServer
    [<CommonParameter>]

```

### Table of Contents
{: .no_toc}

1. TOC
{:toc}

-----

### Detailed Description 

The Disconnect-TfsConfigurationServer cmdlet removes the connection previously set by its counterpart [Connect-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsConfigurationServer). Therefore, cmdlets relying on a "default server" as provided by "[Get-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/ConfigServer/Get-TfsConfigurationServer) -Current" will no longer work after a call to this cmdlet, unless their -Server argument is provided or a new call to [Connect-TfsConfigurationServer](https://tfscmdlets.dev/Cmdlets/Connection/Connect-TfsConfigurationServer) is made.

[Go to top](#disconnect-tfsconfigurationserver)

### Related Topics



[Go to top](#disconnect-tfsconfigurationserver)

