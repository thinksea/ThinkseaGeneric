namespace Thinksea.Collections
{
    /// <summary>
    /// 等待模式任务队列。
    /// </summary>
    /// <remarks>
    /// 当有新的任务添加到队列中时，用于处理队列的方法将得到通知，否则处于等待状态。
    /// 这与轮询方式不同，可以占用更少的系统资源，更及时的得到通知。
    /// 适用于多线程添加任务单线程处理任务，单线程添加任务多线程处理任务，多线程添加任务多线程处理任务。
    /// 方法对跨线程操作是安全的。
    /// </remarks>
    public class WaitQueue<T> : System.Collections.Generic.IEnumerable<T>
    {
        /// <summary>
        /// 一个事件通知，用于通知消费者有新的数据到来。
        /// </summary>
        private System.Threading.ManualResetEvent Event = new System.Threading.ManualResetEvent(false);
        /// <summary>
        /// 一个集合，用于存储数据。
        /// </summary>
        private System.Collections.Generic.LinkedList<T> datas;
        /// <summary>
        /// 队列锁。
        /// </summary>
        private System.Threading.ReaderWriterLockSlim datasRWLock = new System.Threading.ReaderWriterLockSlim();

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
        public WaitQueue()
        {
            this.datas = new System.Collections.Generic.LinkedList<T>();
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="maxSize">队列最大长度上限。</param>
        public WaitQueue(int maxSize)
        {
            this.MaxSize = maxSize;
            this.datas = new System.Collections.Generic.LinkedList<T>();
        }

        /// <summary>
        /// 将对象添加到集合的结尾处。
        /// </summary>
        /// <param name="item">待添加对象。</param>
        /// <returns>添加成功返回 true；否则返回 false。</returns>
        public bool Add(T item)
        {
            this.datasRWLock.EnterWriteLock();
            try
            {
                if (this.MaxSize == -1 || this.datas.Count < MaxSize)
                {
                    this.datas.AddLast(item);
                    Event.Set();
                    return true;
                }
            }
            finally
            {
                this.datasRWLock.ExitWriteLock();
            }
            return false;
        }

        /// <summary>
        /// 当集合中不存在指定的对象时，将对象添加到集合的结尾处。
        /// </summary>
        /// <param name="item">待添加对象。</param>
        /// <returns>添加成功返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 此方法在执行时，首先在集合中查找与待添加对象匹配的元素，对于已经存在匹配项的情况认为添加成功，返回 true，否则执行添加操作。
        /// </remarks>
        public bool AddOnly(T item)
        {
            this.datasRWLock.EnterWriteLock();
            try
            {
                if (this.datas.Contains(item))
                {
                    return true;
                }
                if ((this.MaxSize == -1 || this.datas.Count < MaxSize))
                {
                    this.datas.AddLast(item);
                    Event.Set();
                    return true;
                }
            }
            finally
            {
                this.datasRWLock.ExitWriteLock();
            }
            return false;
        }

        /// <summary>
        /// 当集合中不存在指定的对象时，将对象添加到集合的结尾处。
        /// </summary>
        /// <param name="item">待添加对象。</param>
        /// <param name="match">用于确定对象是否已经在集合中的匹配器。</param>
        /// <returns>添加成功返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 此方法在执行时，首先在集合中查找与待添加对象匹配的元素（通过执行比较器来确定是否匹配），对于已经存在匹配项的情况认为添加成功，返回 true，否则执行添加操作。
        /// </remarks>
        public bool AddOnly(T item, System.Predicate<T> match)
        {
            this.datasRWLock.EnterWriteLock();
            try
            {
                foreach (var tmp in this.datas)
                {
                    if (match.Invoke(tmp))
                    {
                        return true;
                    }
                }
                if ((this.MaxSize == -1 || this.datas.Count < MaxSize))
                {
                    this.datas.AddLast(item);
                    Event.Set();
                    return true;
                }
            }
            finally
            {
                this.datasRWLock.ExitWriteLock();
            }
            return false;
        }

        /// <summary>
        /// 从集合中清除全部对象。
        /// </summary>
        public void Clear()
        {
            this.datasRWLock.EnterWriteLock();
            try
            {
                this.datas.Clear();
                Event.Reset();
            }
            finally
            {
                this.datasRWLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 从集合中移除指定的对象。
        /// </summary>
        /// <param name="item">待移除的对象。</param>
        public void Remove(T item)
        {
            this.datasRWLock.EnterWriteLock();
            try
            {
                this.datas.Remove(item);
                if (this.datas.Count == 0)
                {
                    Event.Reset();
                }
            }
            finally
            {
                this.datasRWLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 移除并返回位于集合开始处的对象，如果没有可用的对象返回则导致当前线程无限期等待，直到有可用数据为止。
        /// </summary>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public T Get()
        {
            return this.Get(System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// 移除并返回位于集合开始处的对象，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到超时或者有可用数据为止。
        /// </summary>
        /// <param name="millisecondsTimeout">等待超时时间量。</param>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public T Get(int millisecondsTimeout)
        {
            while (true)
            {
                this.datasRWLock.EnterWriteLock();
                try
                {
                    if (this.datas.Count > 0)
                    {
                        T r = this.datas.First.Value;
                        this.datas.RemoveFirst();
                        //this.datas.Remove(this.datas.First);
                        return r;
                    }

                    Event.Reset();
                }
                finally
                {
                    this.datasRWLock.ExitWriteLock();
                }

                if (!Event.WaitOne(millisecondsTimeout, false))
                {
                    return default(T);
                }
            }

        }

        /// <summary>
        /// 返回位于集合开始处的对象，但不将其移除，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到有可用数据为止。
        /// </summary>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public T Watching()
        {
            return this.Watching(System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// 返回位于集合开始处的对象，但不将其移除，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到超时或者有可用数据为止。
        /// </summary>
        /// <param name="millisecondsTimeout">等待超时时间量。</param>
        /// <returns>返回可用的对象，如果不存在则返回表示为空的值（这对于引用类型为 null，对于值类型为零）。</returns>
        public T Watching(int millisecondsTimeout)
        {
            while (true)
            {
                this.datasRWLock.EnterReadLock();
                try
                {
                    if (this.datas.Count > 0)
                    {
                        return this.datas.First.Value;
                    }

                    Event.Reset();
                }
                finally
                {
                    this.datasRWLock.ExitReadLock();
                }

                if (!Event.WaitOne(millisecondsTimeout, false))
                {
                    return default(T);
                }
            }

        }

        /// <summary>
        /// 确定某元素是否在集合中。
        /// </summary>
        /// <param name="item">要在集合中定位的对象。对于引用类型，改值可以为 null。</param>
        /// <returns>找不到返回 false；否则返回 true。</returns>
        /// <remarks>
        /// 如果您的代码中频繁调用此方法请考虑使用 SortedWaitQueue 或 RepeatSortedWaitQueue 类实现功能需求或许能够带来更好的性能。
        /// </remarks>
        public bool Contains(T item)
        {
            this.datasRWLock.EnterReadLock();
            try
            {
                return this.datas.Contains(item);
            }
            finally
            {
                this.datasRWLock.ExitReadLock();
            }

        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个集合中的相匹配的第一个元素。
        /// </summary>
        /// <param name="match">用于定义要搜索的元素的条件。</param>
        /// <param name="result">返回找到的项目。</param>
        /// <returns>如果找到与指定谓词定义的条件匹配的第一个元素，则为 true；否则为 false。</returns>
        public bool Find(System.Predicate<T> match, out T result)
        {
            this.datasRWLock.EnterReadLock();
            try
            {
                foreach (var tmp in this.datas)
                {
                    if (match.Invoke(tmp))
                    {
                        result = tmp;
                        return true;
                    }
                }
                //return this.datas.Find(match);
            }
            finally
            {
                this.datasRWLock.ExitReadLock();
            }
            result = default(T);
            return false;

        }

        #region IEnumerable<T> 成员

        /// <summary>
        /// 返回循环访问成员的枚举数。
        /// </summary>
        /// <returns>可供循环访问的枚举接口。</returns>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return this.datas.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.datas.GetEnumerator();
        }

        #endregion

    }

}
