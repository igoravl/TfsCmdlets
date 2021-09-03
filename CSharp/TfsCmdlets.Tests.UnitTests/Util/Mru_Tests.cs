using System;
using TfsCmdlets.Util;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests
{
    public class Mru_Tests
    {
        private readonly string[] _SampleItems = new[] { "item1", "item2", "item3" };

        [Fact]
        public void Supports_Add_All()
        {
            var mruList = new MruList<string>();

            mruList.Append(_SampleItems);

            Assert.Collection(mruList,
                i => Assert.Equal("item1", i),
                i => Assert.Equal("item2", i),
                i => Assert.Equal("item3", i)
            );
        }

        [Fact]
        public void Supports_Initial_Items()
        {
            var mruList = new MruList<string>(_SampleItems);

            Assert.Collection(mruList,
                i => Assert.Equal("item1", i),
                i => Assert.Equal("item2", i),
                i => Assert.Equal("item3", i)
            );
        }

        [Fact]
        public void Can_Find_Items()
        {
            var mruList = new MruList<string>(_SampleItems);

            Assert.Equal("item3", mruList.Get(i => i.EndsWith("3")));
        }

        [Fact]
        public void Get_Returns_Top_Item()
        {
            var mruList = new MruList<string>(_SampleItems);

            Assert.Equal("item1", mruList.Get());
        }

        [Fact]
        public void Get_Find_Moves_Item_To_Top()
        {
            var mruList = new MruList<string>(_SampleItems);

            var item = mruList.Get(i => i.EndsWith("3"));

            Assert.Equal(item, mruList.Get());
        }

        [Fact]
        public void Can_Limit_Initial_Capacity()
        {
            var mruList = new MruList<string>(_SampleItems, 2);

            Assert.Collection(mruList,
                i => Assert.Equal("item1", i),
                i => Assert.Equal("item2", i)
            );
        }

        [Fact]
        public void Supports_Collection_Initialization()
        {
            var mruList = new MruList<string>()
            {
                "item1",
                "item2", 
                "item3"
            };

            Assert.Collection(mruList,
                i => Assert.Equal("item1", i),
                i => Assert.Equal("item2", i),
                i => Assert.Equal("item3", i)
            );
        }

        [Fact]
        public void Can_Discard_Lru_On_Capacity_Reached()
        {
            var mruList = new MruList<string>(_SampleItems, 3);

            mruList.Insert("item4");

            Assert.Equal(3, mruList.Count);

            Assert.Collection(mruList,
                i => Assert.Equal("item4", i),
                i => Assert.Equal("item1", i),
                i => Assert.Equal("item2", i)
            );
        }

        [Fact]
        public void Throw_On_Append_When_Capacity_Reached()
        {
            var mruList = new MruList<string>(_SampleItems, 3);

            Assert.Throws<Exception>(() => mruList.Append("item4"));
        }
    }
}