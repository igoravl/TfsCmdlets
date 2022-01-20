using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TfsCmdlets.Services;
using TfsCmdlets.Services.Impl;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Services
{
    public class DateService_Tests
    {
        [Fact]
        public void Can_Skip_Weekends()
        {
            var startDate = new DateTime(2022, 01, 10);
            var finishDate = new DateTime(2022, 01, 28);
            const int expected = 4;

            IDateService svc = new DateServiceImpl();

            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }));
        }

        [Fact]
        public void Can_Handle_Non_Standard_Weeks()
        {
            var startDate = new DateTime(2022, 01, 12);
            var finishDate = new DateTime(2022, 02, 01);
            const int expected = 6;

            IDateService svc = new DateServiceImpl();

            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }));
        }

        [Fact]
        public void Can_Start_On_Saturday()
        {
            var startDate = new DateTime(2022, 01, 15);
            var finishDate = new DateTime(2022, 01, 17);
            const int expected = 2;

            IDateService svc = new DateServiceImpl();

            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }));
        }

        [Fact]
        public void Can_Start_On_Sunday()
        {
            var startDate = new DateTime(2022, 01, 16);
            var finishDate = new DateTime(2022, 01, 17);
            const int expected = 1;

            IDateService svc = new DateServiceImpl();

            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }));
        }

        [Fact]
        public void Can_Finish_On_Saturday()
        {
            var startDate = new DateTime(2022, 01, 10);
            var finishDate = new DateTime(2022, 01, 15);
            const int expected = 1;

            IDateService svc = new DateServiceImpl();

            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }));
        }

        [Fact]
        public void Can_Finish_On_Sunday()
        {
            var startDate = new DateTime(2022, 01, 10);
            var finishDate = new DateTime(2022, 01, 16);
            const int expected = 2;

            IDateService svc = new DateServiceImpl();

            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }));
        }

        [Fact]
        public void Can_Handle_Empty_Arrays()
        {
            var startDate = new DateTime(2022, 01, 10);
            var finishDate = new DateTime(2022, 01, 28);
            const int expected = 0;

            IDateService svc = new DateServiceImpl();

            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, null));
            Assert.Equal(expected, svc.GetWeekendDays(startDate, finishDate, Enumerable.Empty<DayOfWeek>().ToArray()));
        }
    }
}
