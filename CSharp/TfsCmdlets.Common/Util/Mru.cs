using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TfsCmdlets.Util
{
    internal class Mru
    {
        private readonly string _path;
        private IList<string> _entries;

        private Mru(string name)
        {
            _path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                $".TfsCmdlets/{name}.json");
            Load();
        }

        internal static Mru Server { get; } = new Mru(nameof(Server));

        internal static Mru Collection { get; } = new Mru(nameof(Collection));

        internal string Get()
        {
            return _entries.FirstOrDefault();
        }

        internal void Set(string entry)
        {
            while (_entries.Remove(entry)) { }

            _entries.Insert(0, entry);

            Save();
        }

        private void Load()
        {
            try
            {
                _entries = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(_path)).ToList();
            }
            catch (Exception ex)
            {
                if(_entries == null) _entries = new List<string>();
                Debug.WriteLine(ex);
            }
        }

        private void Save()
        {
            try
            {
                EnsureDirectory();
                File.WriteAllText(_path, JsonConvert.SerializeObject(_entries.ToArray()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void EnsureDirectory()
        {
            var dir = Path.GetDirectoryName(_path);

            if (Directory.Exists(dir)) return;

            Directory.CreateDirectory(dir);
        }
    }
}