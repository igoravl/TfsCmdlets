//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Management.Automation;
//using System.Reflection;
//using TfsCmdlets.Cmdlets;
//using TfsCmdlets.Services;
//using Xunit;

//namespace TfsCmdlets.Tests.UnitTests
//{
//    public class Correctness_Tests
//    {
//        [Fact]
//        public void All_Cmdlets_Have_Controllers()
//        {
//            var asm = typeof(AssemblyResolver).Assembly;
//            var cmdlets = asm.GetTypes().Where(t => typeof(CmdletBase).IsAssignableFrom(t) && !t.IsAbstract).ToList();
//            var controllers = new List<IController>();

//            foreach (var t in asm.GetTypes()
//                         .Where(t => typeof(IController).IsAssignableFrom(t) && !t.IsAbstract))
//            {
//                var ci = t.GetConstructors()[0];
//                var parms = new object[ci.GetParameters().Length];

//                controllers.Add(ci.Invoke(parms) as IController);
//            }

//            var missingControllers = new SortedSet<string>();

//            foreach (var cmdlet in cmdlets)
//            {
//                if (!controllers.Any(
//                        c => c.CommandName.Equals(cmdlet.Name, StringComparison.OrdinalIgnoreCase) || 
//                        c.CommandName.Equals(cmdlet.Name + "Controller", StringComparison.OrdinalIgnoreCase)))
//                {
//                    missingControllers.Add(cmdlet.Name);
//                }
//            }

//            Assert.Empty(missingControllers);
//        }
//    }
//}