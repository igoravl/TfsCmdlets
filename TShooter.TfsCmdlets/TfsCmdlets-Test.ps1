Import-Module D:\Projects\VSO_Lambda3\ALM\TfsCmdlets\Src\Lambda3.TfsCmdlets\Lambda3.TfsCmdlets\TfsCmdlets.psm1 -Force 

New-TfsGitRepository -CollectionUrl "http://localhost:8080/tfs/DefaultCollection/" -ProjectName "GitProject" -Name "NewRepo" -UseDefaultCredentials