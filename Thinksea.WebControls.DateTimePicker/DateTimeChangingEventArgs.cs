using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.WebControls.DateTimePicker
{
    /// <summary>
    /// 时间更改事件代理。
    /// </summary>
    /// <param name="sender">引发事件的对象。</param>
    /// <param name="e">引发的事件的数据。</param>
    public delegate void DateTimeChangingEventHandler(object sender, DateTimeChangingEventArgs e);

    /// <summary>
    /// 为时间更改事件提供数据。
    /// </summary>
    public class DateTimeChangingEventArgs: System.EventArgs
    {
        /// <summary>
        /// 获取或设置一个值，用于指示是否应该取消操作。
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// 获取新值。
        /// </summary>
        public System.DateTime NewValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public DateTimeChangingEventArgs()
        {
            this.Cancel = false;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="NewValue">新值。</param>
        public DateTimeChangingEventArgs(System.DateTime NewValue)
        {
            this.Cancel = false;
            this.NewValue = NewValue;

        }

    }
}
