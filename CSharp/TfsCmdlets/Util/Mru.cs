using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TfsCmdlets.Util
{
    //internal class Mru
    //{
    //    private readonly string _path;
    //    private readonly MruList<string> _Entries;

    //    private Mru(string name, int capacity = 10)
    //    {
    //        _path = Path.Combine(
    //            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
    //            $".TfsCmdlets/{name}.json");
    //        Load();
    //    }

    //    internal static Mru Server { get; } = Create(nameof(Server));

    //    internal static Mru Collection { get; } = Create(nameof(Collection));

    //    internal static Mru Create(string listName, int capacity = 10)
    //    {
    //        return new Mru(listName, capacity);
    //    }

    //    internal string Get()
    //    {
    //        if (!_Entries.TryGetValue
    //    }

    //    internal void Set(string entry)
    //    {
    //        while (_Entries.Remove(entry)) { }

    //        _Entries.Insert(0, entry);

    //        Save();
    //    }

    //    private void Load()
    //    {
    //        try
    //        {
    //            _Entries = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(_path)).ToList();
    //        }
    //        catch (Exception ex)
    //        {
    //            if (_Entries == null) _Entries = new List<string>();
    //            Debug.WriteLine(ex);
    //        }
    //    }

    //    private void Save()
    //    {
    //        try
    //        {
    //            EnsureDirectory();
    //            File.WriteAllText(_path, JsonConvert.SerializeObject(_Entries.ToArray()));
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine(ex);
    //        }
    //    }

    //    private void EnsureDirectory()
    //    {
    //        var dir = Path.GetDirectoryName(_path);

    //        if (Directory.Exists(dir)) return;

    //        Directory.CreateDirectory(dir);
    //    }
    //}

    internal class MruList<T>: IEnumerable<T>
        where T : class, IEquatable<T>
    {
        private readonly LinkedList<T> _Items;
        private readonly Dictionary<T, LinkedListNode<T>> _ItemMap;
        private readonly int _MaxCapacity;

        public int Count => _ItemMap.Count;

        public MruList(int cap = UInt16.MaxValue)
        {
            _MaxCapacity = cap;
            _Items = new LinkedList<T>();
            _ItemMap = new Dictionary<T, LinkedListNode<T>>(_MaxCapacity);
        }

        public MruList(IEnumerable<T> initialItems, int cap = UInt16.MaxValue)
            :this(cap)
        {
            Append(initialItems);
        }

        public void Append(T value)
        {
            AddItem(value, true);
        }

        public void Append(IEnumerable<T> items)
        {
            foreach (var i in items)
            {
                Append(i);
                if (_Items.Count == _MaxCapacity) break;
            }
        }

        public void Insert(T value)
        {
            AddItem(value, false);
        }

        private void AddItem(T value, bool append = false)
        {
            if (_ItemMap.ContainsKey(value))
            {
                // Move item to the top and exit
                TryGet(value);
                return;
            }

            if (_Items.Count == _MaxCapacity)
            {
                if (append) throw new Exception("MRU list is full. Try adding items to the top of the list or removing existing items prior to calling this method");

                LinkedListNode<T> node = _Items.Last;
                _Items.RemoveLast();
                _ItemMap.Remove(node.Value);
            }

            var newNode = new LinkedListNode<T>(value);

            if (append)
            {
                _Items.AddLast(newNode);
            }
            else
            {
                _Items.AddFirst(newNode);
            }
            _ItemMap.Add(value, newNode);
        }

        public bool TryGet(T value)
        {
            if (!_ItemMap.TryGetValue(value, out var node))
            {
                return false;
            }

            _Items.Remove(node);
            _Items.AddFirst(node);

            return true;
        }

        public T Get() => _Items.First();

        public T Get(Func<T, bool> predicate)
        {
            var item = _Items.Where(predicate).FirstOrDefault();

            if (item != default) TryGet(item);

            return item;
        }

        public IEnumerator<T> GetEnumerator() => _Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _Items.GetEnumerator();
    }

    internal static class MruListExtensions
    {
        public static void Add<T>(this MruList<T> list, T value)
            where T: class, IEquatable<T>
        {
            list.Append(value);
        }
    }
}