using System.Collections.Generic;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface IGlobalListService : IService
    {
        void Import(GlobalList list);

        void Import(GlobalListCollection lists);

        GlobalListCollection Export();

        void Remove(IEnumerable<string> listName);
    }
}