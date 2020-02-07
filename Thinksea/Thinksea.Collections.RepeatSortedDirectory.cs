using System;
using System.Collections.Generic;

namespace Thinksea.Collections
{
    /// <summary>
    /// 按键排序的键/值对集合（支持重复键）。
    /// </summary>
    /// <typeparam name="TKey">键类型。</typeparam>
    /// <typeparam name="TValue">值类型。</typeparam>
    public class RepeatSortedDictionary<TKey, TValue>
    {
        /// <summary>
        /// 一个键/值对描述。
        /// </summary>
        [Serializable]
        public class KeyValuePair
        {
            /// <summary>
            /// 键。
            /// </summary>
            public TKey Key
            {
                get;
                set;
            }

            /// <summary>
            /// 值。
            /// </summary>
            public TValue Value
            {
                get;
                set;
            }

            /// <summary>
            /// 一个构造方法。
            /// </summary>
            public KeyValuePair()
            {
            }

            /// <summary>
            /// 用指定的数据初始化此实例。
            /// </summary>
            /// <param name="key">键。</param>
            /// <param name="value">值。</param>
            public KeyValuePair(TKey key, TValue value)
            {
                this.Key = key;
                this.Value = value;
            }

        }

        /// <summary>
        /// 用于包装指定项目的类。
        /// </summary>
        [Serializable]
        public class Item
        {
            private TValue _Value;
            /// <summary>
            /// 值。
            /// </summary>
            public TValue Value
            {
                get
                {
                    return this._Value;
                }
                set
                {
                    this._Value = value;
                }
            }
            /// <summary>
            /// 一个集合，用于存储子项目。
            /// </summary>
            protected internal System.Collections.Generic.LinkedList<TValue> _SubItems;
            /// <summary>
            /// 一个集合，用于存储子项目。
            /// </summary>
            protected internal System.Collections.Generic.LinkedList<TValue> SubItems
            {
                get
                {
                    if (this._SubItems == null)
                    {
                        this._SubItems = new LinkedList<TValue>();
                    }
                    return this._SubItems;
                }
            }
            /// <summary>
            /// 获取一个值指示是否有子项目。
            /// </summary>
            public bool HasSubItems
            {
                get
                {
                    return this._SubItems != null && this._SubItems.Count > 0;
                }
            }

            /// <summary>
            /// 一个构造方法。
            /// </summary>
            public Item()
            {
            }

            /// <summary>
            /// 用指定的数据初始化此实例。
            /// </summary>
            /// <param name="value">值。</param>
            public Item(TValue value)
            {
                this._Value = value;
            }

            /// <summary>
            /// 添加一个值到集合末尾。
            /// </summary>
            /// <param name="value">值。</param>
            public void AddSubItem(TValue value)
            {
                this.SubItems.AddLast(new LinkedListNode<TValue>(value));
            }

        }

        /// <summary>
        /// 项目集合。
        /// </summary>
        private System.Collections.Generic.SortedDictionary<TKey, Item> Items;

        private int _Count = 0;
        /// <summary>
        /// 集合中的项目数量。
        /// </summary>
        public int Count
        {
            get
            {
                return this._Count;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public RepeatSortedDictionary()
        {
            this.Items = new SortedDictionary<TKey, Item>();
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="comparer">在比较键时要使用的 System.Collections.Generic.IComparer&lt;T&gt; 实现；或者为 null，表示为键类型使用默认的 System.Collections.Generic.Comparer&lt;T&gt;。</param>
        public RepeatSortedDictionary(IComparer<TKey> comparer)
        {
            this.Items = new SortedDictionary<TKey, Item>(comparer);
        }

        /// <summary>
        /// 添加指定的键和项目到集合中。
        /// </summary>
        /// <param name="key">键。</param>
        /// <param name="value">项目。</param>
        public void Add(TKey key, TValue value)
        {
            if (this.Items.ContainsKey(key))
            {
                this.Items[key].AddSubItem(value);
            }
            else
            {
                this.Items.Add(key, new RepeatSortedDictionary<TKey, TValue>.Item(value));
            }
            this._Count++;
        }

        /// <summary>
        /// 从集合中清空全部项目。
        /// </summary>
        public void Clear()
        {
            this.Items.Clear();
            this._Count = 0;
        }

        /// <summary>
        /// 判断集合中是否包含指定的键。
        /// </summary>
        /// <param name="key">键。</param>
        /// <returns>包含返回 true；否则返回 false。</returns>
        public bool ContainsKey(TKey key)
        {
            return this.Items.ContainsKey(key);
        }

        /// <summary>
        /// 判断集合中是否包含指定的项目。
        /// </summary>
        /// <param name="value">项目。</param>
        /// <returns>包含返回 true；否则返回 false。</returns>
        public bool ContainsValue(TValue value)
        {
            if (value == null)
            {
                foreach (var tmp in this.Items)
                {
                    if (tmp.Value.Value == null)
                    {
                        return true;
                    }
                    if (tmp.Value.HasSubItems)
                    {
                        if (tmp.Value.SubItems.Contains(default(TValue)))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
                foreach (var tmp in this.Items)
                {
                    if (valueComparer.Equals(tmp.Value.Value, value))
                    {
                        return true;
                    }
                    if (tmp.Value.HasSubItems)
                    {
                        foreach (var tmp2 in tmp.Value.SubItems)
                        {
                            if (valueComparer.Equals(tmp2, value))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断集合中是否包含指定的项目。
        /// </summary>
        /// <param name="key">与之匹配的键。</param>
        /// <param name="value">项目。</param>
        /// <returns>包含返回 true；否则返回 false。</returns>
        public bool ContainsValue(TKey key, TValue value)
        {
            Item kvp;
            bool r = this.Items.TryGetValue(key, out kvp);
            if (r)
            {
                EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
                if (valueComparer.Equals(kvp.Value, value))
                {
                    return true;
                }
                return kvp.SubItems.Contains(value);
            }
            return false;
        }

        /// <summary>
        /// 将集合中的所有内容复制到指定的数组中，第一个项目将被复制到集合中由参数 index 指定的索引位置。
        /// </summary>
        /// <param name="array">数组。</param>
        /// <param name="index">起始索引。</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            foreach (var tmp in this.Items)
            {
                array[index++] = new KeyValuePair<TKey, TValue>(tmp.Key, tmp.Value.Value);
                if(tmp.Value.HasSubItems)
                {
                    foreach (var tmp2 in tmp.Value.SubItems)
                    {
                        array[index++] = new KeyValuePair<TKey, TValue>(tmp.Key, tmp2);
                    }
                }
            }
        }

        /// <summary>
        /// 从集合中移除与指定键匹配的项目。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            if (this.Items.ContainsKey(key))
            {
                RepeatSortedDictionary<TKey, TValue>.Item tmp = this.Items[key];
                bool r = this.Items.Remove(key);
                if (r)
                {
                    if (!tmp.HasSubItems)
                    {
                        this._Count--;
                    }
                    else
                    {
                        this._Count = this._Count - tmp.SubItems.Count - 1;
                    }
                }
                return r;
            }
            return false;
        }

        /// <summary>
        /// 从集合中移除使用指定键的特定项目。
        /// </summary>
        /// <param name="key">键。</param>
        /// <param name="value">项目。</param>
        /// <returns></returns>
        public bool Remove(TKey key, TValue value)
        {
            if (this.Items.ContainsKey(key))
            {
                RepeatSortedDictionary<TKey, TValue>.Item tmp = this.Items[key];
                EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
                bool r;
                if (valueComparer.Equals(tmp.Value, value))
                {
                    r = this.Items.Remove(key);
                    if (tmp.HasSubItems)
                    {
                        this.Items.Add(key, new RepeatSortedDictionary<TKey, TValue>.Item(tmp.SubItems.First.Value));
                        tmp.SubItems.RemoveFirst();
                        RepeatSortedDictionary<TKey, TValue>.Item newItem = this.Items[key];
                        newItem._SubItems = tmp.SubItems;
                    }
                }
                else
                {
                    r = tmp.SubItems.Remove(value);
                }
                if (r)
                {
                    this._Count--;
                }
                return r;
            }
            return false;
        }

        /// <summary>
        /// 从集合中移除第一个项目。
        /// </summary>
        /// <returns></returns>
        public void RemoveFirst()
        {
            if (this.Count > 0)
            {
                SortedDictionary<TKey, RepeatSortedDictionary<TKey, TValue>.Item>.Enumerator tmp2 = this.Items.GetEnumerator();
                tmp2.MoveNext();
                var tmp = tmp2.Current;
                bool r = this.Items.Remove(tmp.Key);
                if (r)
                {
                    if (tmp.Value.HasSubItems)
                    {
                        this.Items.Add(tmp.Key, new RepeatSortedDictionary<TKey, TValue>.Item(tmp.Value.SubItems.First.Value));
                        tmp.Value.SubItems.RemoveFirst();
                        RepeatSortedDictionary<TKey, TValue>.Item newItem = this.Items[tmp.Key];
                        newItem._SubItems = tmp.Value.SubItems;
                    }
                    this._Count--;
                }
            }
        }

        /// <summary>
        /// 获取指定键所对应的项目列表。
        /// </summary>
        /// <param name="key">键。</param>
        /// <returns>找不到返回 null。</returns>
        public TValue[] GetValues(TKey key)
        {
            TValue[] value = null;
            Item kvp;
            bool r = this.Items.TryGetValue(key, out kvp);
            if (r)
            {
                value = new TValue[kvp.SubItems.Count + 1];
                value[0] = kvp.Value;
                int i = 1;
                foreach (var tmp in kvp.SubItems)
                {
                    value[i++] = tmp;
                }
            }
            return value;
        }

        /// <summary>
        /// 尝试获取集合中的第一个项目。
        /// </summary>
        /// <param name="value">返回的项目，找不到则返回此数据类型的默认值。</param>
        /// <returns></returns>
        public bool TryGetFirstValue(out TValue value)
        {
            if (this.Count > 0)
            {
                SortedDictionary<TKey, RepeatSortedDictionary<TKey, TValue>.Item>.Enumerator tmp = this.Items.GetEnumerator();
                tmp.MoveNext();
                value = tmp.Current.Value.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }
        /// <summary>
        /// 获取集合中的第一个项目。
        /// </summary>
        /// <returns>找不到则返回 null。</returns>
        public KeyValuePair GetFirst()
        {
            if (this.Count > 0)
            {
                SortedDictionary<TKey, RepeatSortedDictionary<TKey, TValue>.Item>.Enumerator tmp = this.Items.GetEnumerator();
                tmp.MoveNext();
                return new KeyValuePair(tmp.Current.Key, tmp.Current.Value.Value);
            }
            return null;
        }

        /// <summary>
        /// 键列表。
        /// </summary>
        public SortedDictionary<TKey, RepeatSortedDictionary<TKey, TValue>.Item>.KeyCollection Keys
        {
            get
            {
                return this.Items.Keys;
            }
        }

        /// <summary>
        /// 项目列表。
        /// </summary>
        public System.Collections.Generic.IEnumerable<TValue> Values
        {
            get
            {
                System.Collections.Generic.List<TValue> l = new List<TValue>(this.Count);
                foreach (var tmp in this.Items)
                {
                    l.Add(tmp.Value.Value);
                    if (tmp.Value.HasSubItems)
                    {
                        foreach (var tmp2 in tmp.Value.SubItems)
                        {
                            l.Add(tmp2);
                        }
                    }
                }
                return l;
            }
        }

    }

}
