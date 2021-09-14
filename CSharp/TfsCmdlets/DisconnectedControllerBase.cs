using System;
using System.Collections.Generic;
using System.Linq;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    internal abstract class DisconnectedControllerBase<T> : IController<T> where T : class
    {
        protected ILogger Logger { get; private set; }
        protected IParameterManager ParameterManager { get; private set; }
        protected IPowerShellService PowerShell { get; private set; }
        protected string Verb { get; private set; }

        public DisconnectedControllerBase(ILogger logger, IParameterManager parameterManager, IPowerShellService powerShell)
        {
            Logger = logger;
            ParameterManager = parameterManager;
            PowerShell = powerShell;
        }

        public object InvokeVerb(string verb, object overridingParameters = null)
        {
            switch (verb.ToLowerInvariant())
            {
                case "add": { return AddItem(overridingParameters); }
                case "connect": { return ConnectItem(overridingParameters); }
                case "copy": { return CopyItem(overridingParameters); }
                case "disable": { return DisableItem(overridingParameters); }
                case "disconnect": { DisconnectItem(overridingParameters); return null; }
                case "dismount": { DismountItem(overridingParameters); return null; }
                case "enable": { return EnableItem(overridingParameters); }
                case "enter": { EnterItem(overridingParameters); return null; }
                case "exit": { ExitItem(overridingParameters); return null; }
                case "export": { ExportItem(overridingParameters); return null; }
                case "get": { return GetItems(overridingParameters); }
                case "import": { ImportItem(overridingParameters); return null; }
                case "invoke": { InvokeItem(overridingParameters); return null; }
                case "mount": { MountItem(overridingParameters); return null; }
                case "move": { return MoveItem(overridingParameters); }
                case "new": { return NewItem(overridingParameters); }
                case "remove": { RemoveItem(overridingParameters); return null; }
                case "rename": { return RenameItem(overridingParameters); }
                case "search": { return SearchItems(overridingParameters); }
                case "set": { return SetItem(overridingParameters); }
                case "start": { StartItem(overridingParameters); return null; }
                case "stop": { StopItem(overridingParameters); return null; }
                case "test": { return TestItem(overridingParameters); }
                case "undo": { UndoItem(overridingParameters); return null; }
                default: { throw new ArgumentException($"Unknown verb '{verb}'"); }
            }
        }

        public T GetItem(object overridingParameters) => GetItems(overridingParameters)?.FirstOrDefault();

        public IEnumerable<T> GetItems(object overridingParameters) => DoGetItems(ParameterManager.Get(overridingParameters));

        public T NewItem(object overridingParameters) => DoNewItem(ParameterManager.Get(overridingParameters));

        public void RemoveItem(object overridingParameters) => DoRemoveItem(ParameterManager.Get(overridingParameters));

        public T RenameItem(object overridingParameters) => DoRenameItem(ParameterManager.Get(overridingParameters));

        public T SuspendItem(object overridingParameters) => DoSuspendItem(ParameterManager.Get(overridingParameters));

        public T ResumeItem(object overridingParameters) => DoResumeItem(ParameterManager.Get(overridingParameters));

        public T DisableItem(object overridingParameters) => DoDisableItem(ParameterManager.Get(overridingParameters));

        public T EnableItem(object overridingParameters) => DoEnableItem(ParameterManager.Get(overridingParameters));

        public T SetItem(object overridingParameters) => DoSetItem(ParameterManager.Get(overridingParameters));

        public T AddItem(object overridingParameters = null) => DoAddItem(ParameterManager.Get(overridingParameters));

        public T ConnectItem(object overridingParameters = null) => DoConnectItem(ParameterManager.Get(overridingParameters));

        public T CopyItem(object overridingParameters = null) => DoCopyItem(ParameterManager.Get(overridingParameters));

        public void DisconnectItem(object overridingParameters = null) => DoDisconnectItem(ParameterManager.Get(overridingParameters));

        public void DismountItem(object overridingParameters = null) => DoDismountItem(ParameterManager.Get(overridingParameters));

        public void EnterItem(object overridingParameters = null) => DoEnterItem(ParameterManager.Get(overridingParameters));

        public void ExitItem(object overridingParameters = null) => DoExitItem(ParameterManager.Get(overridingParameters));

        public void ExportItem(object overridingParameters = null) => DoExportItem(ParameterManager.Get(overridingParameters));

        public void ImportItem(object overridingParameters = null) => DoImportItem(ParameterManager.Get(overridingParameters));

        public void InvokeItem(object overridingParameters = null) => DoInvokeItem(ParameterManager.Get(overridingParameters));

        public void MountItem(object overridingParameters = null) => DoMountItem(ParameterManager.Get(overridingParameters));

        public T MoveItem(object overridingParameters = null) => DoMoveItem(ParameterManager.Get(overridingParameters));

        public void StartItem(object overridingParameters = null) => DoStartItem(ParameterManager.Get(overridingParameters));

        public IEnumerable<T> SearchItems(object overridingParameters = null) => DoSearchItems(ParameterManager.Get(overridingParameters));

        public void StopItem(object overridingParameters = null) => DoStopItem(ParameterManager.Get(overridingParameters));

        public void UndoItem(object overridingParameters = null) => DoUndoItem(ParameterManager.Get(overridingParameters));

        public bool TestItem(object overridingParameters)
        {
            try { return GetItem(overridingParameters) != null; }
            catch { return false; }
        }

        // Protected members

        protected abstract IEnumerable<T> DoGetItems(ParameterDictionary parameters);
        protected virtual T DoNewItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoNewItem));
        protected virtual T DoRenameItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoRenameItem));
        protected virtual void DoRemoveItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoRemoveItem));
        protected virtual T DoSetItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoSetItem));
        protected virtual T DoSuspendItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoSuspendItem));
        protected virtual T DoResumeItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoResumeItem));
        protected virtual T DoDisableItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoDisableItem));
        protected virtual T DoEnableItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoEnableItem));
        protected virtual T DoAddItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoAddItem));
        protected virtual T DoConnectItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoConnectItem));
        protected virtual T DoCopyItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoCopyItem));
        protected virtual void DoDisconnectItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoDisconnectItem));
        protected virtual void DoDismountItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoDismountItem));
        protected virtual void DoEnterItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoEnterItem));
        protected virtual void DoExitItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoExitItem));
        protected virtual void DoExportItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoExportItem));
        protected virtual void DoImportItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoImportItem));
        protected virtual void DoInvokeItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoInvokeItem));
        protected virtual void DoMountItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoMountItem));
        protected virtual T DoMoveItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoMoveItem));
        protected virtual void DoStartItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoStartItem));
        protected virtual IEnumerable<T> DoSearchItems(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoSearchItems));
        protected virtual void DoStopItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoStopItem));
        protected virtual void DoUndoItem(ParameterDictionary parameters) => throw new NotImplementedException(nameof(DoUndoItem));
    }
}