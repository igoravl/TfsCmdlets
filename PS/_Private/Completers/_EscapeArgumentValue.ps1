Function _EscapeArgumentValue {
    param
    (
        [Parameter(ValueFromPipeline = $true)]
        [string]
        $InputObject    
    )

    Process {
        if ($InputObject.Contains(' ') -or $InputObject.Contains("'") -or $InputObject.Contains('"')) {
            $InputObject = "'" + $InputObject.Replace("'", "''") + "'"
        }
        
        return $InputObject
    }
}
