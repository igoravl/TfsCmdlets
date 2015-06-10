#
# QueueANewBuild.ps1
#


Connect-TfsTeamProjectCollection "http://my-tfs:8080/tfs/DefaultCollection"

Start-TfsBuild -BuildDefinition "My Build Definition" -Project "MyTeamProject"
