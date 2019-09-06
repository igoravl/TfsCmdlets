function _NewDictionary
{
    #requires -Version 2.0

    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, Position=1)]
        [ValidateNotNull()]
        [hashtable]
        $InputObject,

        [Parameter(Position=0)]
        [ValidateCount(2,2)]
        [Type[]]
        $Types = @([string], [object])
    )

    process
    {
        $KeyType = $Types[0]
        $ValueType = $Types[1]

        $outputObject = New-Object "System.Collections.Generic.Dictionary[[$($KeyType.FullName)],[$($ValueType.FullName)]]"

        foreach ($entry in $InputObject.GetEnumerator())
        {
            $newKey = $entry.Key -as $KeyType
            
            if ($null -eq $newKey)
            {
                throw 'Could not convert key "{0}" of type "{1}" to type "{2}"' -f
                      $entry.Key,
                      $entry.Key.GetType().FullName,
                      $KeyType.FullName
            }
            elseif ($outputObject.ContainsKey($newKey))
            {
                throw "Duplicate key `"$newKey`" detected in input object."
            }

            $outputObject.Add($newKey, $entry.Value -as $ValueType)
        }

        Write-Output $outputObject
    }
}
