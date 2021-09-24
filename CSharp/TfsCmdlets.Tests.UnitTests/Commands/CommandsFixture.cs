using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets.Tests.UnitTests.Commands
{
    internal class CommandsFixture
    {
        public Connection GetTfsCollection()
        {
            return new Connection(
                new VssConnection(
                    new Uri("http://tfs.local:8080/tfs/SampleCollection"),
                    new VssCredentials()));
        }

        //public Connection GetTfsServer()
        //{
        //    return new Connection(
        //        new VssConnection(
        //            new Uri("http://tfs.local:8080/tfs/"),
        //            new VssCredentials()));
        //}

        public Connection GetAzdoCollection()
        {
            return new Connection(
                new VssConnection(
                    new Uri("https://dev.azure.com/tfscmdlets-sample-org"),
                    new VssCredentials()));
        }

    }
}
