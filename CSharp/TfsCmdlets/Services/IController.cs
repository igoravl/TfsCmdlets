using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IController<T> : IController where T : class
    {
        object InvokeVerb(string verb, object overridingParameters = null);

        T GetItem(object overridingParameters = null);
        IEnumerable<T> GetItems(object overridingParameters = null);
        T AddItem(object overridingParameters = null);
        T ConnectItem(object overridingParameters = null);
        T CopyItem(object overridingParameters = null);
        T DisableItem(object overridingParameters = null);
        void DisconnectItem(object overridingParameters = null);
        void DismountItem(object overridingParameters = null);
        T EnableItem(object overridingParameters = null);
        void EnterItem(object overridingParameters = null);
        void ExitItem(object overridingParameters = null);
        void ExportItem(object overridingParameters = null);
        void ImportItem(object overridingParameters = null);
        void InvokeItem(object overridingParameters = null);
        void MountItem(object overridingParameters = null);
        T MoveItem(object overridingParameters = null);
        T NewItem(object overridingParameters = null);
        void RemoveItem(object overridingParameters = null);
        T RenameItem(object overridingParameters = null);
        T ResumeItem(object overridingParameters = null);
        void StartItem(object overridingParameters = null);
        IEnumerable<T> SearchItems(object overridingParameters = null);
        T SetItem(object overridingParameters = null);
        void StopItem(object overridingParameters = null);
        T SuspendItem(object overridingParameters = null);
        bool TestItem(object overridingParameters = null);
        void UndoItem(object overridingParameters = null);
    }
    public interface IController
    {
        
    }
}