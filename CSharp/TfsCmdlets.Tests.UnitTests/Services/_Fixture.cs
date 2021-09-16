// using System;
// using System.Collections.Generic;
// using System.Composition;
// using System.Management.Automation;
// using System.Text;
// using Microsoft.TeamFoundation.Core.WebApi;
// using TfsCmdlets.Models;
// using TfsCmdlets.Services;

// namespace TfsCmdlets.Tests.UnitTests.Services
// {

//     [Export(typeof(ILogger))]
//     public class LoggerSample : ILogger
//     {
//         public void Log(string message) => throw new NotImplementedException();
//         public void LogWarn(string message) => throw new NotImplementedException();
//         public void LogError(string message) => throw new NotImplementedException();
//         public void LogError(Exception ex) => throw new NotImplementedException();
//     }

//     [Export(typeof(ICommand))]
//     public class CommandSample1 : ICommand
//     {
//         public object InvokeCommand(ParameterDictionary parameters) => throw new NotImplementedException();

//         public string Verb => throw new NotImplementedException();

//         public string Noun => throw new NotImplementedException();

//         public string CommandName => throw new NotImplementedException();

//         public Type DataType => throw new NotImplementedException();
//     }

//     [Export(typeof(ICommand))]
//     public class CommandSample2 : ICommand
//     {
//         public object InvokeCommand(ParameterDictionary parameters) => throw new NotImplementedException();

//         public string Verb => throw new NotImplementedException();

//         public string Noun => throw new NotImplementedException();

//         public string CommandName => throw new NotImplementedException();

//         public Type DataType => throw new NotImplementedException();
//     }


//     [Export(typeof(ICommand))]
//     public class CommandSample3: ICommand, ILogger
//     {
//         public void Log(string message) => throw new NotImplementedException();

//         public void LogWarn(string message) => throw new NotImplementedException();

//         public void LogError(string message) => throw new NotImplementedException();

//         public void LogError(Exception ex) => throw new NotImplementedException();

//         public object InvokeCommand(ParameterDictionary parameters) => throw new NotImplementedException();

//         public string Verb => throw new NotImplementedException();

//         public string Noun => throw new NotImplementedException();

//         public string CommandName => throw new NotImplementedException();

//         public Type DataType => throw new NotImplementedException();
//     }

//     [Export(typeof(IPowerShellService))]
//     public class PowerShellSample: IPowerShellService
//     {
//         [Import] public ILogger Logger { get; set; }

//         public void WriteObject(object item, bool enumerateCollection = true)
//         {
//             throw new NotImplementedException();
//         }

//         public void WriteVerbose(string message)
//         {
//             throw new NotImplementedException();
//         }

//         public void WriteWarning(string message)
//         {
//             throw new NotImplementedException();
//         }

//         public void WriteError(string message)
//         {
//             throw new NotImplementedException();
//         }

//         public void WriteError(Exception ex)
//         {
//             throw new NotImplementedException();
//         }

//         public void WriteError(ErrorRecord errorRecord)
//         {
//             throw new NotImplementedException();
//         }

//         public bool IsVerbose { get; }
//         public IReadOnlyDictionary<string, object> GetBoundParameters()
//         {
//             throw new NotImplementedException();
//         }

//         public object GetVariableValue(string name)
//         {
//             throw new NotImplementedException();
//         }

//         public void SetVariableValue(string name, object value)
//         {
//             throw new NotImplementedException();
//         }

//         public string Edition { get; }
//         public bool ShouldProcess(string target, string action)
//         {
//             throw new NotImplementedException();
//         }

//         public bool ShouldProcess(TeamProject tp, string action)
//         {
//             throw new NotImplementedException();
//         }

//         public bool ShouldProcess(TpcConnection collection, string action)
//         {
//             throw new NotImplementedException();
//         }

//         public bool ShouldContinue(string query, string caption = null)
//         {
//             throw new NotImplementedException();
//         }
//     }

//     public class CmdletSample1
//     {
//         [Import] private ILogger Logger { get; set; }


//         public ILogger GetLogger() => Logger;
//     }

//     public class CmdletSample2
//     {
//         [Import] public ILogger Logger { get; set; }
//         [ImportMany] public IEnumerable<ICommand> Commands { get; set; }
//     }

//     public class CmdletSample3
//     {
//         [Import] public IPowerShellService PowerShellService {  get; set; }

//     }
// }
