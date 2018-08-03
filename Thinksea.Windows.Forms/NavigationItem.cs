using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 导航事件的代理。
    /// </summary>
    /// <param name="sender">引发此事件的对象。</param>
    /// <param name="e">导航项目数据。</param>
    public delegate void NavigationEventHandler(object sender, NavigationEventArgs e);

    /// <summary>
    /// 导航菜单项目。
    /// </summary>
    public class NavigationItem
    {
        internal NavigationBar _NavigationBar;
        /// <summary>
        /// 当前导航菜单项目所属的导航菜单控件。
        /// </summary>
        [Browsable(false)]
        public NavigationBar NavigationBar
        {
            get
            {
                return this._NavigationBar;
            }
        }

        private string _ID;
        /// <summary>
        /// 导航项目编号。
        /// </summary>
        public string ID
        {
            get
            {
                if (this._ID == null)
                {
                    return this._Text;
                }
                else
                {
                    return this._ID;
                }
            }
            set
            {
                this._ID = value;
            }
        }

        private string _Text;
        /// <summary>
        /// 可显示文本。
        /// </summary>
        public string Text
        {
            get
            {
                if (this._Text == null)
                {
                    return this._ID;
                }
                else
                {
                    return this._Text;
                }
            }
            set
            {
                this._Text = value;
                if (this.NavigationBar != null)
                {
                    this.NavigationBar.RefreshData();
                }
            }
        }

        private bool _EnableLink;
        /// <summary>
        /// 获取或设置一个值，用于指示是否起用此项目的导航功能。（如果为 false，则不为用户提供点击导航功能。）
        /// </summary>
        public bool EnableLink
        {
            get
            {
                return this._EnableLink;
            }
            set
            {
                this._EnableLink = value;
                if (this.NavigationBar != null)
                {
                    this.NavigationBar.RefreshData();
                }
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NavigationItem()
        {
            this.EnableLink = true;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="navigationBar">此导航项目所属的导航菜单控件。</param>
        public NavigationItem(NavigationBar navigationBar)
        {
            this.EnableLink = true;
            this._NavigationBar = navigationBar;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="ID">导航项目编号。</param>
        /// <param name="Text">可显示文本。</param>
        /// <param name="EnableLink">指示是否起用此项目的导航功能。</param>
        public NavigationItem(string ID, string Text, bool EnableLink)
        {
            this.ID = ID;
            this.Text = Text;
            this.EnableLink = EnableLink;
        }

        /// <summary>
        /// 制作此实例的一个浅表副本。
        /// </summary>
        /// <returns></returns>
        public NavigationItem Clone()
        {
            return new NavigationItem(this.ID, this.Text, this.EnableLink);
        }

    }

    /// <summary>
    /// 导航项目集合。
    /// </summary>
    public class NavigationItemCollections: System.Collections.Generic.IList<NavigationItem>
    {
        internal NavigationBar _NavigationBar;
        /// <summary>
        /// 当前导航菜单项目所属的导航菜单控件。
        /// </summary>
        [Browsable(false)]
        public NavigationBar NavigationBar
        {
            get
            {
                return this._NavigationBar;
            }
        }

        /// <summary>
        /// 元素集合。
        /// </summary>
        private System.Collections.Generic.List<NavigationItem> items;

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NavigationItemCollections()
        {
            this.items = new List<NavigationItem>();
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="navigationBar">此实例所属的导航菜单控件。</param>
        public NavigationItemCollections(NavigationBar navigationBar)
            : this()
        {
            this._NavigationBar = navigationBar;
        }

        #region IList<NavigationItem> 成员

        /// <summary>
        /// 获取指定导航菜单项的索引。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(NavigationItem item)
        {
            return this.items.IndexOf(item);
        }

        /// <summary>
        /// 将导航菜单项插入到集合中的指定位置。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, NavigationItem item)
        {
            item._NavigationBar = this.NavigationBar;
            this.items.Insert(index, item);
            if (this.NavigationBar != null)
            {
                this.NavigationBar.RefreshData();
            }
        }

        /// <summary>
        /// 从集合中移除指定索引处的菜单项。
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            NavigationItem ni = this.items[index];
            this.items.Remove(ni);
            ni._NavigationBar = null;
            if (this.NavigationBar != null)
            {
                this.NavigationBar.RefreshData();
            }
        }

        /// <summary>
        /// 对集合的索引。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public NavigationItem this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                this.items[index] = value;
            }
        }

        #endregion

        #region ICollection<NavigationItem> 成员

        /// <summary>
        /// 添加一个导航菜单项到集合末尾。
        /// </summary>
        /// <param name="item"></param>
        public void Add(NavigationItem item)
        {
            item._NavigationBar = this.NavigationBar;
            this.items.Add(item);
            if (this.NavigationBar != null)
            {
                this.NavigationBar.RefreshData();
            }
        }

        /// <summary>
        /// 从集合中清除所有的导航菜单项。
        /// </summary>
        public void Clear()
        {
            while (this.items.Count > 0)
            {
                NavigationItem ni = this.items[this.items.Count - 1];
                if (this.items.Remove(ni))
                {
                    ni._NavigationBar = null;
                }
            }
            if (this.NavigationBar != null)
            {
                this.NavigationBar.RefreshData();
            }
        }

        /// <summary>
        /// 判断指定的导航菜单项目是否在集合中。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(NavigationItem item)
        {
            return this.items.Contains(item);
        }

        /// <summary>
        /// 将整个集合中的项目复制到指定的数组中，从目标数组的指定索引位置开始放置。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(NavigationItem[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i]._NavigationBar = null;
            }
        }

        /// <summary>
        /// 获取集合中导航菜单项的数量
        /// </summary>
        public int Count
        {
            get {
                return this.items.Count;
            }
        }

        /// <summary>
        /// 获取一个值，判断集合是否为只读状态。
        /// </summary>
        public bool IsReadOnly
        {
            get {
                return false;
            }
        }

        /// <summary>
        /// 从集合中移除一个导航菜单项目。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(NavigationItem item)
        {
            bool rc = this.items.Remove(item);
            item._NavigationBar = null;
            if (this.NavigationBar != null)
            {
                this.NavigationBar.RefreshData();
            }
            return rc;
        }

        #endregion

        #region IEnumerable<NavigationItem> 成员

        /// <summary>
        /// 提供对集合的枚举。
        /// </summary>
        /// <returns></returns>
        public IEnumerator<NavigationItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        /// <summary>
        /// 提供对集合的枚举。
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 将指定集合中的元素添加到这个集合的末尾。
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(System.Collections.Generic.IEnumerable<NavigationItem> collection)
        {
            foreach (var item in collection)
            {
                item._NavigationBar = this.NavigationBar;
                this.items.Add(item);
            }
            if (this.NavigationBar != null)
            {
                this.NavigationBar.RefreshData();
            }
        }

        /// <summary>
        /// 将集合元素复制到新数组中。
        /// </summary>
        /// <returns></returns>
        public NavigationItem[] ToArray()
        {
            return this.items.ToArray();
        }
    }

    /// <summary>
    /// 包含导航事件数据的类。
    /// </summary>
    public class NavigationEventArgs : System.EventArgs
    {
        /// <summary>
        /// 导航项目。
        /// </summary>
        public NavigationItem Item
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NavigationEventArgs()
        {
            this.Item = null;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="item">导航项目。</param>
        public NavigationEventArgs(NavigationItem item)
        {
            this.Item = item;
        }
    }

}
