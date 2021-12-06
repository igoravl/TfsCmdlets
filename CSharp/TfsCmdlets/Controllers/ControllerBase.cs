using System;
using System.Composition;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers
{
    internal abstract class ControllerBase : IController
    {
        public string Verb => GetType().Name.Substring(0, GetType().Name.FindIndex(c => char.IsUpper(c), 1));

        public string Noun => GetType().Name.Substring(Verb.Length);

        public string DisplayName => $"{Verb}-Tfs{Noun}";

        public string CommandName => $"{Verb}{Noun}";

        public virtual Type DataType => GetType();

        protected IParameterManager Parameters { get; }

        protected ILogger Logger { get; }

        public abstract object InvokeCommand();

        [ImportingConstructor]
        public ControllerBase(IParameterManager parameters, ILogger logger)
        {
            Parameters = parameters;
            Logger = logger;
        }
    }
}