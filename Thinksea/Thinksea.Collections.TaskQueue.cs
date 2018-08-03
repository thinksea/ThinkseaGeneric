namespace Thinksea.Collections
{
    /// <summary>
    /// 任务描述类。
    /// </summary>
    [System.Serializable]
    public class Task<T>
    {
        /// <summary>
        /// 任务。
        /// </summary>
        public T Work
        {
            get;
            set;
        }

        /// <summary>
        /// 任务启动时间。
        /// </summary>
        protected internal System.DateTime _RunTime;
        /// <summary>
        /// 获取或设置任务启动时间。
        /// </summary>
        public System.DateTime RunTime
        {
            get
            {
                return this._RunTime;
            }
            set
            {
                if (this.OwnerSortedTaskQueue != null)
                {
                    this.OwnerSortedTaskQueue.SetTaskRunTime(this, value);
                }
                else
                {
                    this._RunTime = value;
                }
            }
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="work">任务。</param>
        public Task(T work)
        {
            this.RunTime = System.DateTime.Now;
            this.Work = work;
        }
        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="work">任务。</param>
        /// <param name="runTime">任务启动时间。</param>
        public Task(T work, System.DateTime runTime)
        {
            this.RunTime = runTime;
            this.Work = work;
        }

        /// <summary>
        /// 所属的任务队列。
        /// </summary>
        protected internal TaskQueue<T> OwnerSortedTaskQueue = null;

    }

    /// <summary>
    /// 带启动时间的等待模式任务队列。
    /// </summary>
    /// <remarks>
    /// 当有新的任务添加到队列中时，用于处理队列的方法将得到通知，否则处于等待状态。
    /// 这与轮询方式不同，可以占用更少的系统资源，更及时的得到通知。
    /// 适用于多线程添加任务单线程处理任务，单线程添加任务多线程处理任务，多线程添加任务多线程处理任务。
    /// 方法对跨线程操作是安全的。
    /// </remarks>
    public class TaskQueue<T>
    {
        /// <summary>
        /// 一个事件通知，用于通知消费者有新的数据到来。
        /// </summary>
        private System.Threading.ManualResetEvent Event = new System.Threading.ManualResetEvent(false);
        /// <summary>
        /// 一个集合，用于存储数据。
        /// </summary>
        private RepeatSortedDictionary<System.DateTime, Task<T>> datas;
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
        /// 当指定的项目的数据产生变更时，更新指定项目的排序情况。
        /// </summary>
        /// <param name="item">待处理对象。</param>
        /// <param name="newRunTime">新的启动时间。</param>
        protected internal void SetTaskRunTime(Task<T> item, System.DateTime newRunTime)
        {
            this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
            try
            {
                bool r = this.datas.Remove(item.RunTime, item);
                if (r)
                {
                    item._RunTime = newRunTime;
                    this.datas.Add(item.RunTime, item);
                    if (System.DateTime.Now >= item.RunTime)
                    {
                        Event.Set();
                    }
                }
            }
            finally
            {
                this.datasRWLock.ReleaseWriterLock();
            }

        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public TaskQueue()
        {
            this.datas = new RepeatSortedDictionary<System.DateTime, Task<T>>();
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="maxSize">队列最大长度上限。</param>
        public TaskQueue(int maxSize)
        {
            this.MaxSize = maxSize;
            this.datas = new RepeatSortedDictionary<System.DateTime, Task<T>>();
        }

        /// <summary>
        /// 将对象添加到集合的结尾处。
        /// </summary>
        /// <param name="item">待添加对象。</param>
        /// <returns>添加成功返回 true；否则返回 false。</returns>
        public bool Add(Task<T> item)
        {
            this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
            try
            {
                if (this.MaxSize == -1 || this.datas.Count < MaxSize)
                {
                    item.OwnerSortedTaskQueue = this;
                    this.datas.Add(item.RunTime, item);
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

        ///// <summary>
        ///// 当集合中不存在指定的对象时，将对象添加到集合的结尾处。
        ///// </summary>
        ///// <param name="item">待添加对象。</param>
        ///// <returns>添加成功返回 true；否则返回 false。</returns>
        ///// <remarks>
        ///// 此方法在执行时，首先在集合中查找与待添加对象匹配的元素，如果已经存在则结束过程（对于已经存在的情况认为添加成功，返回 true），否则执行添加操作。
        ///// </remarks>
        //public bool AddOnly(Task<T> item)
        //{
        //    this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
        //    try
        //    {
        //        bool r = this.datas.ContainsValue(item.RunTime, item);
        //        if (r)
        //        {
        //            return true;
        //        }
        //        if ((this.MaxSize == -1 || this.datas.Count < MaxSize))
        //        {
        //            item.OwnerSortedTaskQueue = this;
        //            this.datas.Add(item.RunTime, item);
        //            Event.Set();
        //            return true;
        //        }
        //    }
        //    finally
        //    {
        //        this.datasRWLock.ReleaseWriterLock();
        //    }
        //    return false;
        //}

        /// <summary>
        /// 当集合中不存在指定的对象时，将对象添加到集合的结尾处。
        /// </summary>
        /// <param name="item">待添加对象。</param>
        /// <param name="match">用于确定对象是否已经在集合中的匹配器。</param>
        /// <returns>添加成功返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 此方法在执行时，首先在集合中查找与待添加对象匹配的元素（通过执行比较器来确定是否匹配），对于已经存在匹配项的情况认为添加成功，返回 true，否则执行添加操作。
        /// </remarks>
        public bool AddOnly(Task<T> item, System.Predicate<Task<T>> match)
        {
            this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
            try
            {
                foreach (var tmp in this.datas.Values)
                {
                    if (match.Invoke(tmp))
                    {
                        return true;
                    }
                }
                if ((this.MaxSize == -1 || this.datas.Count < MaxSize))
                {
                    item.OwnerSortedTaskQueue = this;
                    this.datas.Add(item.RunTime, item);
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
                foreach (var tmp in this.datas.Values)
                {
                    tmp.OwnerSortedTaskQueue = null;
                }
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
        /// <param name="item">待移除的对象。</param>
        public void Remove(Task<T> item)
        {
            this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
            try
            {
                bool r = this.datas.Remove(item.RunTime, item);
                if (r)
                {
                    item.OwnerSortedTaskQueue = null;
                }
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
        /// <returns>返回可用的对象，如果超时则返回 null。</returns>
        public Task<T> Get()
        {
            return this.Get(System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// 移除并返回位于集合开始处的对象，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到超时或者有可用数据为止。
        /// </summary>
        /// <param name="millisecondsTimeout">等待超时时间量。</param>
        /// <returns>返回可用的对象，如果超时则返回 null。</returns>
        public Task<T> Get(int millisecondsTimeout)
        {
            int msTimeout = millisecondsTimeout;
            while (true)
            {
                this.datasRWLock.AcquireWriterLock(System.Threading.Timeout.Infinite);
                try
                {
                    #region 计算最少需要等待的时间。
                    if (this.datas.Count > 0)
                    {
                        Task<T> minT;
                        this.datas.TryGetFirstValue(out minT); //返回集合中最先执行的任务。
                        if (minT != null && System.DateTime.Now >= minT.RunTime)
                        {
                            this.datas.RemoveFirst();
                            minT.OwnerSortedTaskQueue = null;
                            return minT;
                        }

                        int tmpTimeout = millisecondsTimeout;
                        if (minT != null)
                        {
                            tmpTimeout = System.Convert.ToInt32((minT.RunTime - System.DateTime.Now).TotalMilliseconds);
                        }
                        if (tmpTimeout < 0)
                        {
                            tmpTimeout = 0;
                        }
                        if (tmpTimeout > millisecondsTimeout && millisecondsTimeout != System.Threading.Timeout.Infinite)
                        {
                            tmpTimeout = millisecondsTimeout;
                        }
                        if (msTimeout == System.Threading.Timeout.Infinite || msTimeout > tmpTimeout)
                        {
                            msTimeout = tmpTimeout;
                        }

                    }
                    #endregion

                    Event.Reset();

                }
                finally
                {
                    this.datasRWLock.ReleaseWriterLock();
                }

                if (!Event.WaitOne(msTimeout, false) && msTimeout == millisecondsTimeout)
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// 返回位于集合开始处的对象，但不将其移除，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到有可用数据为止。
        /// </summary>
        /// <returns>返回可用的对象，如果超时则返回 null。</returns>
        public Task<T> Watching()
        {
            return this.Watching(System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// 返回位于集合开始处的对象，但不将其移除，如果没有可用的对象返回则导致当前线程等待指定的时间量，直到超时或者有可用数据为止。
        /// </summary>
        /// <param name="millisecondsTimeout">等待超时时间量。</param>
        /// <returns>返回可用的对象，如果超时则返回 null。</returns>
        public Task<T> Watching(int millisecondsTimeout)
        {
            int msTimeout = millisecondsTimeout;
            while (true)
            {
                this.datasRWLock.AcquireReaderLock(System.Threading.Timeout.Infinite);
                try
                {
                    #region 计算最少需要等待的时间。
                    if (this.datas.Count > 0)
                    {
                        Task<T> minT;
                        bool result = this.datas.TryGetFirstValue(out minT); //返回集合中最先执行的任务。
                        if (result && minT != null && System.DateTime.Now >= minT.RunTime)
                        {
                            return minT;
                        }

                        int tmpTimeout = millisecondsTimeout;
                        if (minT != null)
                        {
                            tmpTimeout = System.Convert.ToInt32((minT.RunTime - System.DateTime.Now).TotalMilliseconds);
                        }
                        if (tmpTimeout < 0)
                        {
                            tmpTimeout = 0;
                        }
                        if (tmpTimeout > millisecondsTimeout && millisecondsTimeout != System.Threading.Timeout.Infinite)
                        {
                            tmpTimeout = millisecondsTimeout;
                        }
                        if (msTimeout == System.Threading.Timeout.Infinite || msTimeout > tmpTimeout)
                        {
                            msTimeout = tmpTimeout;
                        }

                    }
                    #endregion

                    Event.Reset();

                }
                finally
                {
                    this.datasRWLock.ReleaseReaderLock();
                }

                if (!Event.WaitOne(msTimeout, false) && msTimeout == millisecondsTimeout)
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// 确定某元素是否在集合中。
        /// </summary>
        /// <param name="item">要在集合中定位的对象。</param>
        /// <returns>找不到返回 false；否则返回 true。</returns>
        public bool Contains(Task<T> item)
        {
            this.datasRWLock.AcquireReaderLock(System.Threading.Timeout.Infinite);
            try
            {
                this.datas.ContainsValue(item.RunTime, item);
            }
            finally
            {
                this.datasRWLock.ReleaseReaderLock();
            }
            return false;

        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个集合中的相匹配的第一个元素。
        /// </summary>
        /// <param name="match">用于定义要搜索的元素的条件。</param>
        /// <returns>如果找到与指定谓词定义的条件匹配的第一个元素，则为该元素；否则为类型 T 的默认值。</returns>
        public Task<T> Find(System.Predicate<Task<T>> match)
        {
            this.datasRWLock.AcquireReaderLock(System.Threading.Timeout.Infinite);
            try
            {
                foreach (var tmp in this.datas.Values)
                {
                    if (match.Invoke(tmp))
                    {
                        return tmp;
                    }
                }
            }
            finally
            {
                this.datasRWLock.ReleaseReaderLock();
            }
            return null;
        }

    }

}
