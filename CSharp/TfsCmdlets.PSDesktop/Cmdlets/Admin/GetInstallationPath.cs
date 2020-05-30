using System;

namespace TfsCmdlets.Cmdlets.Admin
{
    partial class GetInstallationPath
    {
        partial void DoProcessRecord()
        {
            throw new NotImplementedException();

            //scriptBlock = _NewScriptBlock -EntryPoint "_GetInstallationPath" -Dependency "_TestRegistryValue", "_GetRegistryValue"oi

            //WriteObject(_InvokeScriptBlock -ScriptBlock scriptBlock -Computer Computer -Credential Credential -ArgumentList Version, Component); return;
        }
    }
}
