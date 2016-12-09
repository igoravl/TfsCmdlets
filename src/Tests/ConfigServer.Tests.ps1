. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\_TestSetup.ps1"

InModuleScope TfsCmdlets {

	Describe "Connect-TfsConfigurationServer" {

		Mock _NewConfigServer {
			@{
				Url = $Url;
				Credential = $Cred
			}
		} -ModuleName Connection

		Context "When passing URL w/o creds" {

			$expectedUrl = "http://foo:8080/tfs/"
			$expectedCreds = [System.Net.CredentialCache]::DefaultNetworkCredentials

			It "Connects with default creds" {
				$result = Connect-TfsConfigurationServer $expectedUrl
				$result | Should Be $expectedUrl
			}

		}
	}

	Describe "Disconnect-TfsConfigurationServer" {

		Context "When calling Disconnect-TfsConfigurationServer" {

			It "Removes global variables" {
				$Global:TfsServerConnection = "foo"
				$Global:TfsServerConnectionUrl = "foo"
				$Global:TfsServerConnectionCredential = "foo"
				$Global:TfsServerConnectionUseDefaultCredentials = "foo"

				Disconnect-TfsConfigurationServer

				$Global:TfsServerConnection | Should Be $null
				$Global:TfsServerConnectionUrl | Should Be $null
				$Global:TfsServerConnectionCredential | Should Be $null
				$Global:TfsServerConnectionUseDefaultCredentials | Should Be $null
			}
		}
	}
}