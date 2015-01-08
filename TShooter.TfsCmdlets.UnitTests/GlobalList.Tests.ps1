Get-Content "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\_TestSetup.ps1" | iex

InModuleScope TfsCmdlets {

    Describe "New-GlobalList" {
        Mock Import-GlobalLists { } -ModuleName GlobalList
        Mock Export-GlobalLists {
        return [xml]'<GLOBALLISTS>
<GLOBALLIST name="List1">
    <LISTITEM value="Item 1" />
</GLOBALLIST>
<GLOBALLIST name="List2">
    <LISTITEM value="Item 1" />
</GLOBALLIST>
</GLOBALLISTS>'
    } -ModuleName GlobalList

        Context "When the list does not exist" {

            It "Creates a new list" {
                $result = New-GlobalList -Name "New List" -Items "Item 1"
                $result.Name | Should Be "New List"
                $result.Items.Length | Should Be 1
                $result.IsOverwritten | Should Be $false
            }

            It "Creates a new list with many items" {
                $result = New-GlobalList -Name "New List" -Items "Item 1", "Item 2"

                $result.Name | Should Be "New List"
                $result.Items.Length | Should Be 2
                $result.Items[0] | Should Be "Item 1"
                $result.Items[1] | Should Be "Item 2"
                $result.IsOverwritten | Should Be $false
            }
       }

        Context "When the list exists" {

            It "Creates a new list with -Force" {
                $result = New-GlobalList -Name "List1" -Items "Item 1" -Force
                $result.Name | Should Be "List1"
                $result.Items.Length | Should Be 1
                $result.IsOverwritten | Should Be $true
            }

            It "Creates a new list with many items with -Force" {
                $result = New-GlobalList -Name "List1" -Items "Item 1", "Item 2" -Force

                $result.Name | Should Be "List1"
                $result.Items.Length | Should Be 2
                $result.Items[0] | Should Be "Item 1"
                $result.Items[1] | Should Be "Item 2"
                $result.IsOverwritten | Should Be $true
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
    <LISTITEM value="Item 1" />
</GLOBALLIST>
<GLOBALLIST name="List2">
    <LISTITEM value="Item 1" />
</GLOBALLIST>
</GLOBALLISTS>'
    } -ModuleName GlobalList

        Context "When the list doesn't exist" {

            It "Creates the list and the item with -Force" {
                $result = Add-GlobalListItem -Name "New List" -Items "Item 1" -Force

                $result.Name | Should Be "New List"
                $result.Items.Length | Should Be 1
                $result.Items[0] | Should Be "Item 1"
                $result.IsNewList | Should Be $true
            }

            It "Throws without -Force" {
                { Add-GlobalListItem -Name "New List" -Item "Item 1" } | Should Throw
            }
        }

        Context "When the list exists" {
        	
            It "Adds a new item" {
                $result = Add-GlobalListItem -Name "List1" -Items "Item 2"

                $result.Name | Should Be "List1"
                $result.Items.Length | Should Be 2
                $result.Items[1] | Should Be "Item 2"
            }

            It "Adds many items" {
                $result = Add-GlobalListItem -Name "List1" -Items "Item 2", "Item 3"

                $result.Name | Should Be "List1"
                $result.Items.Length | Should Be 3
                $result.Items[0] | Should Be "Item 1"
                $result.Items[1] | Should Be "Item 2"
                $result.Items[2] | Should Be "Item 3"
            }

            It "Ignores an existing item" {
                $result = Add-GlobalListItem -Name "List1" -Items "Item 1"

                $result.Name | Should Be "List1"
                $result.Items.Length | Should Be 1
                $result.Items[0] | Should Be "Item 1"
                $result.IsNewList | Should Be $false
            }

            It "Adds many items but ignores an existing item" {
                $result = Add-GlobalListItem -Name "List1" -Items "Item 1", "Item 2"

                $result.Name | Should Be "List1"
                $result.Items.Length | Should Be 2
                $result.Items[0] | Should Be "Item 1"
                $result.Items[1] | Should Be "Item 2"
            }
        }
    }
}