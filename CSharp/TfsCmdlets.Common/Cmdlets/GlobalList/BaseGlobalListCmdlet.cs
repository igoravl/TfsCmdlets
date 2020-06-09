using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    public abstract partial class BaseGlobalListCmdlet : BaseCmdlet<TfsGlobalList>
    {
    }
}