using System;

namespace Thinksea.WebControls.DateTimePicker
{
    /// <summary>
    /// 验证错误事件类。
    /// </summary>
	public class CheckErrorEventArgs : EventArgs
	{
		/// <summary>
		/// 用户输入的导致错误的年份
		/// </summary>
		public string Year;
		/// <summary>
		/// 用户输入的导致错误的月份
		/// </summary>
		public string Month;
		/// <summary>
		/// 用户输入的导致错误的日期
		/// </summary>
		public string Day;
		/// <summary>
		/// 用户输入的导致错误的小时
		/// </summary>
		public string Hour;
		/// <summary>
		/// 用户输入的导致错误的分钟
		/// </summary>
		public string Minute;
		/// <summary>
		/// 用户输入的导致错误的秒
		/// </summary>
		public string Second;

        /// <summary>
        /// 一个构造方法。
        /// </summary>
		public CheckErrorEventArgs ()
		{
			this.Year = "";
			this.Month = "";
			this.Day = "";
			this.Hour = "";
			this.Minute = "";
			this.Second = "";

		}

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <param name="Day">日</param>
        /// <param name="Hour">时</param>
        /// <param name="Minute">分</param>
        /// <param name="Second">秒</param>
		public CheckErrorEventArgs ( string Year, string Month, string Day, string Hour, string Minute, string Second )
		{
			this.Year = Year;
			this.Month = Month;
			this.Day = Day;
			this.Hour = Hour;
			this.Minute = Minute;
			this.Second = Second;

		}

	}
      
    /// <summary>
    /// 一个用于验证错误的事件代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="ce"></param>
	public delegate void CheckErrorEventHandler(object sender, Thinksea.WebControls.DateTimePicker.CheckErrorEventArgs ce);

}
