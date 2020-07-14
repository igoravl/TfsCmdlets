. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_TfsCmdletsPrompt' {

        $defaultPsPrompt = "$($ExecutionContext.SessionState.Path.CurrentLocation)$('>' * ($NestedPromptLevel + 1)) "

        $contexts = @(
            @{Server = $null; 
                Expected = '[Not connected]'},
            @{Server = 'https://dev.azure.com/orgname'   ; Name = 'dev.azure.com'; Collection = 'DefaultCollection'; Project = $null     ; Team = $null     ; User = 'foo@bar.com'; 
                Expected = '[AzDev:/orgname (foo@bar.com)]'},
            @{Server = 'https://dev.azure.com/orgname'   ; Name = 'dev.azure.com'; Collection = 'DefaultCollection'; Project = 'projname'; Team = $null     ; User = 'foo@bar.com'; 
                Expected = '[AzDev:/orgname/projname (foo@bar.com)]'},
            @{Server = 'https://dev.azure.com/orgname'   ; Name = 'dev.azure.com'; Collection = 'DefaultCollection'; Project = 'projname'; Team = 'teamname'; User = 'foo@bar.com'; 
                Expected = '[AzDev:/orgname/projname/teamname (foo@bar.com)]'},
            @{Server = 'https://orgname.visualstudio.com'; Name = 'orgname.visualstudio.com'; Collection = 'DefaultCollection'; Project = $null     ; Team = $null     ; User = 'foo@bar.com'; 
                Expected = '[AzDev:/orgname (foo@bar.com)]'},
            @{Server = 'https://orgname.visualstudio.com'; Name = 'orgname.visualstudio.com'; Collection = 'DefaultCollection'; Project = 'projname'; Team = $null     ; User = 'foo@bar.com'; 
                Expected = '[AzDev:/orgname/projname (foo@bar.com)]'},
            @{Server = 'https://orgname.visualstudio.com'; Name = 'orgname.visualstudio.com'; Collection = 'DefaultCollection'; Project = 'projname'; Team = 'teamname'; User = 'foo@bar.com'; 
                Expected = '[AzDev:/orgname/projname/teamname (foo@bar.com)]'},
            @{Server = 'http://tfs:8080/collectioname'   ; Name = 'tfs'; Collection = 'collectionname'   ; Project = $null     ; Team = $null     ; User = 'BAR\foo'    ; 
                Expected = '[TFS:/tfs/collectionname (BAR\foo)]'},
            @{Server = 'http://tfs:8080/collectioname'   ; Name = 'tfs'; Collection = 'collectionname'   ; Project = 'projname'; Team = $null     ; User = 'BAR\foo'    ; 
                Expected = '[TFS:/tfs/collectionname/projname (BAR\foo)]'},
            @{Server = 'http://tfs:8080/collectioname'   ; Name = 'tfs'; Collection = 'collectionname'   ; Project = 'projname'; Team = 'teamname'; User = 'BAR\foo'    ; 
                Expected = '[TFS:/tfs/collectionname/projname/teamname (BAR\foo)]'},
            @{Server = 'http://tfs:8080/'                ; Name = 'tfs'; Collection = 'collectionname'   ; Project = $null     ; Team = $null     ; User = 'BAR\foo'    ; 
                Expected = '[TFS:/tfs/collectionname (BAR\foo)]'},
            @{Server = 'http://tfs:8080/'                ; Name = 'tfs'; Collection = 'collectionname'   ; Project = 'projname'; Team = $null     ; User = 'BAR\foo'    ; 
                Expected = '[TFS:/tfs/collectionname/projname (BAR\foo)]'}
            @{Server = 'http://tfs:8080/'                ; Name = 'tfs'; Collection = 'collectionname'   ; Project = 'projname'; Team = 'teamname'; User = 'BAR\foo'    ; 
                Expected = '[TFS:/tfs/collectionname/projname/teamname (BAR\foo)]'}
        )

        foreach ($ctxData in $contexts) 
        {
            $script:server = $null
            $script:tpc = $null
            $script:tp = $null
            $script:team = $null
            $context = 'Not connected'         
            $expected = $ctxData.Expected   

            if($ctxData.Server)
            {
                $script:server = @{ Uri = [uri] $ctxData.Server; Name = $ctxData.Name; AuthorizedIdentity = @{ UniqueName = $ctxData.User } }

                if($ctxData.Collection)
                {
                    $script:tpc = @{ Name = $ctxData.Collection }

                    if($ctxData.Project)
                    {
                        $script:tp = @{ Name = $ctxData.Project }

                        if($ctxData.Team)
                        {
                            $script:team = @{ Name = $ctxData.Team }
                        }
                    }
                }

                $context = "Connected to [$($server.Uri)], collection [$($tpc.Name)], project [$($tp.Name)], team [$($team.Name)]"
            }

            Context $context {

                $script:actual = $null

                Mock 'Get-TfsConfigurationServer'   -MockWith { return $script:server }
                Mock 'Get-TfsTeamProjectCollection' -MockWith { return $script:tpc }
                Mock 'Get-TfsTeamProject'           -MockWith { return $script:tp }
                Mock 'Get-TfsTeam'                  -MockWith { return $script:team }
                Mock 'Write-Host'                   -MockWith { $script:actual = $Object }

                It 'Should return default PS prompt' {
                    _TfsCmdletsPrompt | Should Be $defaultPsPrompt
                }

                It 'Should write correct prompt to Write-Host' {
                    $script:actual | Should Be $expected
                }
            }
        }
    }
}
