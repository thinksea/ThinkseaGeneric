namespace Thinksea.Collections
{
    /// <summary>
    /// 允许按照指定的键进行排序的等待模式任务队列。（允许添加重复键）
    /// </summary>
    /// <remarks>
    /// 当有新的任务添加到队列中时，用于处理队列的方法将得到通知，否则处于等待状态。
    /// 这与轮询方式不同，可以占用更少的系统资源，更及时的得到通知。
    /// 适用于多线程添加任务单线程处理任务，单线程添加任务多线程处理任务，多线程添加任务多线程处理任务。
    /// 方法对跨线程操作是安全的。
    /// </remarks>
    public class RepeatSortedWaitQueue<TKey, TValue>
    {
        /// <summary>
        /// 一个事件通知，用于通知消费者有新的数据到来。
        /// </summary>
        private System.Threading.ManualResetEvent Event = new System.Threading.ManualResetEvent(false);
        /// <summary>
        /// 一个集合，用于存储数据。
        /// </summary>
        private RepeatSortedDictionary<TKey, TValue> datas;
        /// <summary>
        /// 队列锁。
        /// </summary>
        private System.Threading.ReaderWriterLock datasRWLock = new System.Threading.ReaderWriterLock();

        private volatile int _MaxSize = -1;
        /// <summary>
        /// 队列最大长度上限。取值为 -1 表示无限制。
        /// </summary>
        public int MaxSize
        {
            get
            {
                return _MaxSize;
            }
            set
            {
                _MaxSize = value;
            }
        }

        /// <summary>
        /// 获取集合中实际包含的对象数。
        /// </summary>
        public int Count
        {
            get
            {
                return this.datas.Count;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public RepeatSortedWaitQueue()
        {
            this.datas = new RepeatSortedDictionary<TKey, TValue>();
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="maxSize">队列最大长度上限。</param>
        public RepeatSortedWaitQueue(int maxSize)
        {
            this.MaxSize = maxSize;
            this.datas = new RepeatSortedDictionary<TKey,TValue>();
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="comparer">在比较键时要使用的 System.Collections.Generic.IComparer&lt;T&gt; 实现；或者为 null，表示为键类型使用默认的 System.Collections.Generic.Comparer&lt;T&gt;。</param>
        public RepeatSortedWaitQueue(System.Collections.Generic.IComparer<TKey> comparer)
        {
            this.datas = new RepeatSortedDictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="maxSize">队列最大长度上限。</param>
        /// <param name="comparer">在比较键时要使用的 System.Collections.Generic.IComparer&lt;T&gt; 实现；或者为 null，表示为键类型使用默认的 System.Collections.Generic.Comparer&lt;T&gt;。</param>
        public RepeatSortedWaitQueue(int maxSize, System.Collections.Generic.IComparer<TKey> comparer)
        {
            this.MaxSize = maxSize;
            this.datas = new RepeatSortedDictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// 将对象添加到集合的结尾处。
        /// </summary>
        /// <param name="key">用其进行排序的键。</param>
        /// <param name="value">待添加对象。</param>
        /// <returns>添加成功返回 true；否则返回 false。</returns>
        public bool Add(TKey key, TValue value)
        {
            this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
            try
            {
                if (this.MaxSize == -1 || this.datas.Count < MaxSize)
                {
                    this.datas.Add(key, value);
                    Event.Set();
                    return true;
                }
            }
            finally
            {
                this.datasRWLock.ReleaseWriterLock();
            }
            return false;
        }

        /// <summary>
        /// 从集合中清除全部对象。
        /// </summary>
        public void Clear()
        {
            this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
            try
            {
                this.datas.Clear();
                Event.Reset();
            }
            finally
            {
                this.datasRWLock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// 从集合中移除指定的对象。
        /// </summary>
        /// <param name="key">用其进行排序的键。</param>
        /// <param name="value">待移除对象。</param>
        public void Remove(TKey key, TValue value)
        {
            this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
            try
            {
                this.datas.Remove(key, value);
                if (this.datas.Count == 0)
                {
                    Event.Reset();
                }
            }
            finally
            {
                this.datasRWLock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// 移除并返回位于集合开始处的对象，如果没有可用的对象返回则导致当前线程无限期等待，直到有可用数据为止。
        /// </summary>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public TValue Get()
        {
            return this.Get(System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// 移除并返回位于集合开始处的对象，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到超时或者有可用数据为止。
        /// </summary>
        /// <param name="millisecondsTimeout">等待超时时间量。</param>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public TValue Get(int millisecondsTimeout)
        {
            while (true)
            {
                this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
                try
                {
                    if (this.datas.Count > 0)
                    {
                        //var tmp = this.datas.GetEnumerator();
                        //tmp.MoveNext();
                        //TValue r = tmp.Current.Value;
                        //this.datas.Remove(tmp.Current.Key);
                        TValue r;
                        bool result = this.datas.TryGetFirstValue(out r);
                        if (result)
                        {
                            this.datas.RemoveFirst();
                            return r;
                        }
                    }
                    Event.Reset();
                }
                finally
                {
                    this.datasRWLock.ReleaseWriterLock();
                }

                if (!Event.WaitOne(millisecondsTimeout, false))
                {
                    return default(TValue);
                }
            }

        }

        /// <summary>
        /// 返回位于集合开始处的对象，但不将其移除，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到有可用数据为止。
        /// </summary>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public TValue Watching()
        {
            return this.Watching(System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// 返回位于集合开始处的对象，但不将其移除，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到超时或者有可用数据为止。
        /// </summary>
        /// <param name="millisecondsTimeout">等待超时时间量。</param>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public TValue Watching(int millisecondsTimeout)
        {
            while (true)
            {
                this.datasRWLock.AcquireReaderLock(System.Threading.Timeout.Infinite);
                try
                {
                    if (this.datas.Count > 0)
                    {
                        TValue r;
                        bool result = this.datas.TryGetFirstValue(out r);
                        if (result)
                        {
                            return r;
                        }
                    }

                    Event.Reset();
                }
                finally
                {
                    this.datasRWLock.ReleaseReaderLock();
                }

                if (!Event.WaitOne(millisecondsTimeout, false))
                {
                    return default(TValue);
                }
            }

        }

        /// <summary>
        /// 确定某元素是否在集合中。
        /// </summary>
        /// <param name="key">用其进行排序的键。</param>
        /// <returns>找不到返回 false；否则返回 true。</returns>
        public bool Contains(TKey key)
        {
            this.datasRWLock.AcquireReaderLock(System.Threading.Timeout.Infinite);
            try
            {
                return this.datas.ContainsKey(key);
            }
            finally
            {
                this.datasRWLock.ReleaseReaderLock();
            }

        }

        ///// <summary>
        ///// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个集合中的相匹配的第一个元素。
        ///// </summary>
        ///// <param name="match">用于定义要搜索的元素的条件。</param>
        ///// <param name="value">返回找到的项目。</param>
        ///// <returns>如果找到与指定谓词定义的条件匹配的第一个元素，则为 true；否则为 false。</returns>
        //public bool Find(System.Predicate<TKey> match, out TValue value)
        //{
        //    this.datasRWLock.AcquireReaderLock(System.Threading.Timeout.Infinite);
        //    try
        //    {
        //        foreach (var tmp in this.datas)
        //        {
        //            if (match.Invoke(tmp.Key))
        //            {
        //                value = tmp.Value;
        //                return true;
        //            }
        //        }
        //        //return this.datas.Find(match);
        //    }
        //    finally
        //    {
        //        this.datasRWLock.ReleaseReaderLock();
        //    }
        //    value = default(TValue);
        //    return false;

        //}

    }

}
