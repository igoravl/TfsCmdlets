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

            var returnValues = _PowerShell.AddScript(@"
                Function PropertyBag_Is_Created_On_First_Call
                {
                    $pso = [PSCustomObject] @{Foo='Bar'}

                    $pso | Add-Member -Name MyLazy -MemberType ScriptProperty `
                        -Value { [TfsCmdlets.Util.LazyProperty]::Get($pso, 'MyLazy', {[TfsCmdlets.Tests.UnitTests.Util.LazyPropertySimulator]::Next()}) }

                    [TfsCmdlets.Tests.UnitTests.Util.LazyPropertySimulator]::Reset()

                    $val1 = $pso.MyLazy

                    echo $val1, $pso
                }
                
                PropertyBag_Is_Created_On_First_Call"
            ).Invoke().ToList();

            Assert.Equal(1, returnValues[0]);
            Assert.True(((PSObject) returnValues[1]).Properties.Match(propName, PSMemberTypes.NoteProperty).Any(), $"Property '{propName}' not found");
        }

        [Fact]
        public void Lazy_Property_Is_Called_Only_Once()
        {
            var returnValues = _PowerShell.AddScript(@"
                Function Lazy_Property_Is_Called_Only_Once
                {
                    $pso = [PSCustomObject]@{Foo='Bar'}
                    $pso | Add-Member -Name MyLazy -MemberType ScriptProperty `
                        -Value { [TfsCmdlets.Util.LazyProperty]::Get($pso, 'MyLazy', {[TfsCmdlets.Tests.UnitTests.Util.LazyPropertySimulator]::Next()}) }
                        
                    [TfsCmdlets.Tests.UnitTests.Util.LazyPropertySimulator]::Reset()

                    $val1 = $pso.MyLazy
                    $val2 = $pso.MyLazy
                    
                    echo $val1, $val2
                }
                
                Lazy_Property_Is_Called_Only_Once"
            ).Invoke().ToList();

            Assert.Equal(1, returnValues[0].BaseObject);
            Assert.Equal(1, returnValues[1].BaseObject);
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