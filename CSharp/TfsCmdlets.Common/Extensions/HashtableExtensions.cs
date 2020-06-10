using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TfsCmdlets.Extensions
{
    internal static class HashtableExtensions
    {
        internal static Dictionary<K, V> ToDictionary<K, V>(this Hashtable table)
        {
            return table?
              .Cast<DictionaryEntry>()
              .ToDictionary(kvp => (K)kvp.Key, kvp => (V)kvp.Value);
        }
    }
}