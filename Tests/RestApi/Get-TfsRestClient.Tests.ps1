. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_GetRestClient' {

        Context 'Getting collection-scoped client' {

            It '' {

                $client = Get-TfsRestClient -Type '' -Collection $foo
            }
        }

        Context 'Getting server-scoped client' {

            It '' {

                $client = Get-TfsRestClient -Type '' -Server $foo
            }
        }
    }
}
