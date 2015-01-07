. .\_TestSetup.ps1

InModuleScope TfsCmdlets {

	Describe "New-GlobalList" {
		Mock Import-GlobalLists { } -ModuleName GlobalList
		Mock Export-GlobalLists {
		return [xml]'<GLOBALLISTS>
<GLOBALLIST name="List1">
	<LISTITEM value="List item 1" />
</GLOBALLIST>
<GLOBALLIST name="List2">
	<LISTITEM value="List item 1" />
</GLOBALLIST>
</GLOBALLISTS>'
	} -ModuleName GlobalList

		Context "When the list does not exist" {

			It "Creates a new list" {
				$result = New-GlobalList -Name "New List" -Items "Item 1"
				$attrValue = $result.SelectSingleNode("//GLOBALLIST[@name='New List']/@name")
				$attrValue.Value | Should Be "New List"
			}
		}

		Context "When the list exists" {

			It "Creates a new list with -Force" {
				$result = New-GlobalList -Name "List1" -Items "Item 1" -Force
				$attrValue = $result.SelectSingleNode("//GLOBALLIST[@name='List1']/@name")
				$attrValue.Value | Should Be "List1"
			}

			It "Throws without -Force" {
				{ New-GlobalList -Name "List1" -Items "Item 1" } | Should Throw
			}
		}
	}

	Describe "Add-GlobalListItem" {
		Mock Import-GlobalLists { } -ModuleName GlobalList
		Mock Export-GlobalLists {
		return [xml]'<GLOBALLISTS>
<GLOBALLIST name="List1">
	<LISTITEM value="List item 1" />
</GLOBALLIST>
<GLOBALLIST name="List2">
	<LISTITEM value="List item 1" />
</GLOBALLIST>
</GLOBALLISTS>'
	} -ModuleName GlobalList

		Context "When the list doesn't exist" {

			It "Throws without -Force" {
				{ Add-GlobalListItem -Name "New List" -Item "Item 1" } | Should Throw
			}

			It "Creates the list and the item with -Force" {
				$result = Add-GlobalListItem -Name "New List" -Items "Item 1" -Force

				$attrValue = $result.SelectSingleNode("//GLOBALLIST[@name='New List']/@name")
				$attrValue.Value | Should Be "New List"

				$attrValue = $result.SelectSingleNode("//GLOBALLIST[@name='New List']/LISTITEM/@value")
				$attrValue.Value | Should Be "Item 1"
			}

		}

		#Context "When the list exists" {
		#	It 
		#}
	}
}