using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Models;
using TfsCmdlets.Services.Impl;

namespace TfsCmdlets.Commands
{
    internal abstract class GetCommand<T>: CommandBase<T>
    {
        protected GetCommand(TpcConnection collection) : base(collection)
        {
        }
    }
}