// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Management.Automation;
// using System.Reflection;
// using TfsCmdlets.Cmdlets;
// using TfsCmdlets.Extensions;
// using TfsCmdlets.Services;
// using Xunit;

// namespace TfsCmdlets.Tests.UnitTests
// {
//     public class Correctness_Tests
//     {
//         [Fact]
//         public void All_Cmdlets_Have_Controllers()
//         {
//             var asm = typeof(AssemblyResolver).Assembly;

//             var cmdletTypes = asm.GetTypes()
//                 .Where(t => t.GetCustomAttributes<TfsCmdletAttribute>().Any())
//                 .ToList();

//             var controllerTypes = asm.GetTypes()
//                 .Where(t => t.GetCustomAttributes<CmdletControllerAttribute>().Any()).ToList();

//             var missingControllers = new SortedSet<string>();

//             foreach (var cmdletType in cmdletTypes)
//             {
//                 var cmdletAttr = cmdletType.GetCustomAttribute<CmdletAttribute>();
//                 var tfsCmdletAttr = cmdletType.GetCustomAttribute<TfsCmdletAttribute>();

//                 var commandName = tfsCmdletAttr.CustomControllerName ?? cmdletAttr.VerbName + cmdletAttr.NounName.Substring(3);
//                 var dataType = tfsCmdletAttr.OutputType;

//                 // Find by name

//                 if (controllerTypes.Any(t => t.Name.Equals(commandName) || t.Name.Equals(commandName + "Controller")))
//                 {
//                     continue;
//                 }

//                 // Find by verb + data type

//                 if (controllerTypes.Any(t =>
//                      t.GetCustomAttribute<CmdletControllerAttribute>().DataType == dataType &&
//                      cmdletAttr.VerbName.Equals(t.Name.Substring(0, t.Name.FindIndex(c => char.IsUpper(c), 1)))))
//                 {
//                     continue;
//                 }

//                 missingControllers.Add(cmdletType.FullName);
//             }

//             Assert.True(0 == missingControllers.Count, $"Found cmdlet(s) without a controller: {string.Join(", ", missingControllers.OrderBy(s=>s).ToArray())}");
//         }
//     }
// }