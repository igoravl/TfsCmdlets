Function Get-TfsProcessTemplate
{
    Param
    (
        [Parameter(Position=0)]
        [string]
        $Name = "*",

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $processTemplateSvc = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.IProcessTemplates")
        $templateHeaders = $processTemplateSvc.TemplateHeaders() | ? Name -Like $Name

        foreach($templateHeader in $templateHeaders)
        {
            $templateHeader | Add-Member Collection $tpc.DisplayName -PassThru
        }
    }
}
