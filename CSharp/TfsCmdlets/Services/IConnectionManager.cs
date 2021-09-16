using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    public interface IConnectionManager
    {
        TpcConnection GetCollection();
        
        ServerConnection GetServer();
    }
}