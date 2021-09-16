using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using TfsCmdlets.Cmdlets;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ICmdletContextManager))]
    internal class CmdletContextManagerImpl: ICmdletContextManager
    {
        private static Stack<CmdletBase> _contextStack = new Stack<CmdletBase>();

        public void Enter(CmdletBase cmdlet)
        {
            _contextStack.Push(cmdlet);
        }

        public CmdletBase Current => _contextStack.Peek();

        public void Exit()
        {
            _contextStack.Pop();
        }
    }
}
