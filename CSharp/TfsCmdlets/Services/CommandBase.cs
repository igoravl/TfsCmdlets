using System;
using System.Composition;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    internal abstract class CommandBase : ICommand
    {
        public string Verb => GetType().Name.Substring(0, GetType().Name.FindIndex(c => char.IsUpper(c), 1));

        public string Noun => GetType().Name.Substring(Verb.Length);

        public string DisplayName => $"{Verb}-Tfs{Noun}";

        public string CommandName => $"{Verb}{Noun}";

        public virtual Type DataType => GetType();

        public ILogger Logger { get; }

        public abstract object InvokeCommand(ParameterDictionary parameters);

        [ImportingConstructor]
        public CommandBase(ILogger logger)
        {
            Logger = logger;
        }
    }
}