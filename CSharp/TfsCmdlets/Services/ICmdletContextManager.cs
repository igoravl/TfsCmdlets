using System;
using System.Collections.Generic;
using System.Text;
using TfsCmdlets.Cmdlets;

namespace TfsCmdlets.Services
{
    internal interface ICmdletContextManager
    {
        void Enter(CmdletBase cmdlet);

        CmdletBase Current { get; }

        void Exit();
    }
}
