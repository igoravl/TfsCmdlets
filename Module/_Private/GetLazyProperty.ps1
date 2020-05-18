Function _GetLazyProperty
{
    [CmdletBinding()]
    Param
    (
        [Parameter(ValueFromPipeline=$true, Position=0)]
        [object]
        $InputObject,

        [Parameter(Position=1)]
        [string]
        $Name,

        [Parameter(Position=2)]
        [ScriptBlock]
        $ScriptBlock
    )


    Process
    {
        _LogParams

        if ($InputObject.__PropertyBag[$Name])
        { 
            return $InputObject.__PropertyBag[$Name] 
        }

        if (-not (Get-Member -InputObject $InputObject __PropertyBag))
        {
            $InputObject | Add-Member -Name __PropertyBag -MemberType NoteProperty -Value @{ }
        }
      
        $result = $InputObject | ForEach-Object $ScriptBlock.GetNewClosure()
        $InputObject.__PropertyBag[$Name] = $result

        return $result
    }
}