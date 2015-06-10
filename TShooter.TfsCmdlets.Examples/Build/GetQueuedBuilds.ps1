#
# GetQueuedBuilds.ps1
#

Connect-TfsTeamProjectCollection "http://my-tfs:8080/tfs/DefaultCollection"

# Get all queued builds given a definition name and a team project name

Get-TfsBuildQueue -BuildDefinition "My Build Definition" -Project "My Team Project"

# Get all queued builds, regardless of definition name or team project name

Get-TfsBuildQueue

#
#
#
#
