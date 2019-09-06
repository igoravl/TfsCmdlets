Function _IsWildcard($Item)
{
    return $Item -match '\\*|\\[.+\\]'
}