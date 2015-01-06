$projectPath = (Split-Path -Parent $MyInvocation.MyCommand.Path).Replace(".UnitTests", "")
$rootModulePath = $projectPath + "\TfsCmdlets.psd1"

Import-Module $rootModulePath

InModuleScope TfsCmdlets {

	Describe "New-GlobalList" {

		Mock Import-GlobalLists { }

		Context "When the list does not exist" {

			Mock Export-GlobalLists {
				return [xml]'<GLOBALLISTS>
		<GLOBALLIST name="List1">
			<LISTITEM value="List item 1" />
		</GLOBALLIST>
		<GLOBALLIST name="List2">
			<LISTITEM value="List item 1" />
		</GLOBALLIST>
		</GLOBALLISTS>'
			}

			It "Creates a new list" {
				[xml]$result = New-GlobalList -Name "New List" -Items "Item 1"
				$attrValue = $result.SelectSingleNode("/GLOBALLISTS/GLOBALLIST[@name='$Name']/@value")
				$attrValue | Should Be "New List"
			}
		}
	}
}