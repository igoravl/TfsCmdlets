using Xunit;
using TfsCmdlets.Util;
using TfsCmdlets.Extensions;
using System;
using System.Linq;
using System.Management.Automation;
using System.Collections;
using System.Management.Automation.Runspaces;

namespace TfsCmdlets.Tests.UnitTests.Util
{
    public class LazyProperty_Tests: IClassFixture<PowerShellFixture>
    {
        private PowerShell _PowerShell;

        public LazyProperty_Tests(PowerShellFixture fixture)
        {
            _PowerShell = fixture.PowerShell;
        }

        [Fact]
        public void PropertyBag_Is_Created_On_First_Call()
        {
            const string propName = "__PropertyBag";

            var pso = _PowerShell.AddScript("$global:pso = 0; $pso")
                .Invoke().First();

            Assert.False(pso.Members.Match(propName, PSMemberTypes.NoteProperty).Any(), $"Property '{propName}' should not exist");

            _PowerShell.AddScript(@"$global:pso | `
                Add-Member -Name MyLazy -MemberType ScriptProperty `
                -Value { [TfsCmdlets.Util.LazyProperty]::Get($global:pso, 'MyLazy', {[TfsCmdlets.Tests.UnitTests.Util.LazyPropertySimulator]::Next()}) }")
                .Invoke();

            _PowerShell.AddScript("$pso.MyLazy").Invoke().First();

            Assert.True(pso.Properties.Match(propName, PSMemberTypes.NoteProperty).Any(), $"Property '{propName}' not found");
        }

        [Fact]
        public void Lazy_Property_Is_Called_Only_Once()
        {
            var pso = _PowerShell.AddScript("$global:pso = 0; $pso").Invoke().First();

            _PowerShell.AddScript(@"$global:pso | `
                Add-Member -Name MyLazy -MemberType ScriptProperty `
                -Value { [TfsCmdlets.Util.LazyProperty]::Get($global:pso, 'MyLazy', {[TfsCmdlets.Tests.UnitTests.Util.LazyPropertySimulator]::Next()}) }")
                .Invoke();

            Assert.Equal(1, _PowerShell.AddScript("$pso.MyLazy").Invoke().First());
            Assert.Equal(1, _PowerShell.AddScript("$pso.MyLazy").Invoke().First());
        }
    }

    public static class LazyPropertySimulator
    {
        private static int _Counter;

        static LazyPropertySimulator()
        {
            Reset();
        }

        public static int Next() {
            return _Counter++;
        }

        public static void Reset()
        {
            _Counter = 1;
        }
    }
}