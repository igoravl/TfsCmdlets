using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    public interface ICommandManager
    {
        ICommand GetCommandByName(string commandName);

        IEnumerable<ICommand> GetCommandByVerb(string verb, string noun = null);

        IEnumerable<ICommand> GetCommandByNoun(string noun);
    }
}