using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: WebResource("Thinksea.WebControls.DateTimePicker.images.DateTimePicker.bmp", "image/bmp")]
[assembly: WebResource("Thinksea.WebControls.DateTimePicker.images.disDateTimePicker.bmp", "image/bmp")]
[assembly: WebResource("Thinksea.WebControls.DateTimePicker.images.DateTimePickerUp.gif", "image/gif")]
[assembly: WebResource("Thinksea.WebControls.DateTimePicker.images.DateTimePickerDown.gif", "image/gif")]
[assembly: WebResource("Thinksea.WebControls.DateTimePicker.DateTimePicker.js", "application/x-javascript")]
namespace Thinksea.WebControls.DateTimePicker
{
	/*  
	Copyright (C) 2005-2007 Thinksea. All rights reserved. 
	Support .Net Framework 2.0
	*/ 
	/// <summary>
	///	一个ASP.Net Web应用程序的日期选择控件.
	/// </summary>
	[
	System.ComponentModel.Description("一个ASP.Net Web应用程序的日期选择控件"),
	System.ComponentModel.DefaultProperty("DateTime"),
	System.ComponentModel.DefaultEvent("DateTimeChanged"),
	System.ComponentModel.Designer(typeof(Thinksea.WebControls.DateTimePicker.DateTimePickerDesigner)),
	System.Web.UI.ToolboxData("<{0}:DateTimePicker runat=server></{0}:DateTimePicker>")
	]
	public class DateTimePicker : System.Web.UI.WebControls.WebControl, IPostBackDataHandler, INamingContainer, IPostBackEventHandler
	{
        /// <summary>
		/// 日期属性
		/// </summary>
		[
        System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("日期控件的日期"),
		System.ComponentModel.NotifyParentProperty(true),
		]
        //[System.ComponentModel.Bindable(true)]
        //[System.ComponentModel.Localizable(true)]
        public System.DateTime DateTime
		{
			get
			{
                object o = this.ViewState["DateTime"];

                if (o == null)
                {
                    var t = System.DateTime.Now;
                    this.ViewState["DateTime"] = t;
                    return t;
                }
                else
                {
                    return (System.DateTime)o;
                }
			}
			set
			{
                if (this.DateTime != value)
				{
                    if (this._DateTimeChanging != null)
                    {
                        DateTimeChangingEventArgs e = new DateTimeChangingEventArgs(value);
                        this._DateTimeChanging(this, e);
                        if (e.Cancel)
                        {
                            return;
                        }
                    }
                    this.ViewState["DateTime"] = value;
					if( this.DateTimeChanged != null )
					{
						this.DateTimeChanged( this, new System.EventArgs() );
					}
				}

			}

		}


		/// <summary>
		/// 图片和脚本文件路径
		/// </summary>
		[
		System.ComponentModel.DefaultValue("/aspnet_client/DateTimePicker/"),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("图片和脚本文件路径"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public string ImageUrl
		{
			get
			{
                object o = this.ViewState["ImageUrl"];
                return ((o == null) ? "/aspnet_client/DateTimePicker/" : (string)o);
			}
			set
			{
                this.ViewState["ImageUrl"] = value;
			}
		}       


		/// <summary>
		/// 指示是否允许输入日期
		/// </summary>
		[
		System.ComponentModel.DefaultValue(true),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("指示是否允许输入日期"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public bool ShowDate
		{
			get
			{
                object o = this.ViewState["ShowDate"];
                return ((o == null) ? true : (bool)o);
			}
			set
			{
                this.ViewState["ShowDate"] = value;
			}

		}


		/// <summary>
		/// 指示是否允许输入小时
		/// </summary>
		[
		System.ComponentModel.DefaultValue(true),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("指示是否允许输入小时"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public bool ShowHour
		{
			get
			{
                object o = this.ViewState["ShowHour"];
                return ((o == null) ? true : (bool)o);
			}
			set
			{
                this.ViewState["ShowHour"] = value;
			}

		}


		/// <summary>
		/// 指示是否允许输入分钟
		/// </summary>
		[
		System.ComponentModel.DefaultValue(true),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("指示是否允许输入分钟"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public bool ShowMinute
		{
			get
			{
                object o = this.ViewState["ShowMinute"];
                return ((o == null) ? true : (bool)o);
			}
			set
			{
                this.ViewState["ShowMinute"] = value;
			}

		}


		/// <summary>
		/// 指示是否允许输入秒
		/// </summary>
		[
		System.ComponentModel.DefaultValue(true),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("指示是否允许输入秒"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public bool ShowSecond
		{
			get
			{
                object o = this.ViewState["ShowSecond"];
                return ((o == null) ? true : (bool)o);
			}
			set
			{
                this.ViewState["ShowSecond"] = value;
			}

		}

		/// <summary>
		/// 指示是否允许使用日期选择器
		/// </summary>
		[
		System.ComponentModel.DefaultValue(true),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("指示是否允许使用日期选择器"),
		System.ComponentModel.NotifyParentProperty(true),
		]
        public bool ShowDataSelector
		{
			get
			{
                object o = this.ViewState["ShowDataSelector"];
                return ((o == null) ? true : (bool)o);
			}
			set
			{
                this.ViewState["ShowDataSelector"] = value;
			}

		}

        /// <summary>
        /// 指示是否使用内联资源。这将导致使用内置的图片和.js等资源文件。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(false),
        System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("指示是否使用内联资源。这将导致使用内置的图片和.js等资源文件。"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool UseIncludeResource
        {
            get
            {
                object o = this.ViewState["UseIncludeResource"];
                return ((o == null) ? false : (bool)o);
            }
            set
            {
                this.ViewState["UseIncludeResource"] = value;
            }

        }

        /// <summary>
        /// 指示是否允许修改控件的值。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(false),
        System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("指示是否允许修改控件的值。"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ReadOnly
        {
            get
            {
                object o = this.ViewState["ReadOnly"];
                return ((o == null) ? false : (bool)o);
            }
            set
            {
                this.ViewState["ReadOnly"] = value;
            }

        }


        /// <summary>
        /// 一个构造方法。
        /// </summary>
		public DateTimePicker()
		{
            this.Style[HtmlTextWriterStyle.Display] = "inline-block";

		}


        /// <summary>
        /// 处理回发数据事件。
        /// </summary>
        /// <param name="eventArgument"></param>
		public virtual void RaisePostBackEvent(string eventArgument)
		{ 
//			switch (eventArgument)
//			{
//				case "Save":
//					this.OnSaveClick(EventArgs.Empty);
//					break;
//				default:
//					break;
//			}
		}

        /// <summary>
        /// 处理回发数据更改事件。
        /// </summary>
		public void RaisePostDataChangedEvent()
		{
			// nothing happens for text changed

		}


        /// <summary>
        /// 当检查到用户输入错误时引发此事件。
        /// </summary>
        [
        System.ComponentModel.Browsable(true),
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("当检查到用户输入错误时引发此事件"),
        ]
        public event Thinksea.WebControls.DateTimePicker.CheckErrorEventHandler CheckError = null;

        /// <summary>
		/// 当选择的日期发生更改时引发此事件。
		/// </summary>
		[
		System.ComponentModel.Browsable(true),
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("当选择的日期发生更改时引发此事件"),
		]
		public event System.EventHandler DateTimeChanged = null;

        private event DateTimeChangingEventHandler _DateTimeChanging;
        /// <summary>
        /// 当选择的日期准备更改时引发此事件。
        /// </summary>
        [
        System.ComponentModel.Browsable(true),
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("当选择的日期准备更改时引发此事件"),
        ]
        public event DateTimeChangingEventHandler DateTimeChanging
        {
            add
            {
                this._DateTimeChanging += value;
            }
            remove
            {
                this._DateTimeChanging -= value;
            }
        }

        /// <summary>
        /// 加载回发数据。
        /// </summary>
        /// <param name="postDataKey">键</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public bool LoadPostData(String postDataKey, System.Collections.Specialized.NameValueCollection values)
        {
            string sYear = values[this.ClientID + ":txtYear"];
            string sMonth = values[this.ClientID + ":txtMonth"];
            string sDay = values[this.ClientID + ":txtDay"];
            string sHour = values[this.ClientID + ":txtHour"];
            string sMinute = values[this.ClientID + ":txtMinute"];
            string sSecond = values[this.ClientID + ":txtSecond"];
            try
            {
                int iYear = 1;
                int iMonth = 1;
                int iDay = 1;
                int iHour = 0;
                int iMinute = 0;
                int iSecond = 0;

                System.DateTime st = this.DateTime;
                if (this.ShowDate)
                {
                    if (sYear == null)
                    {
                        iYear = st.Year;
                    }
                    else
                    {
                        iYear = System.Convert.ToInt32(sYear);
                    }
                }

                if (this.ShowDate)
                {
                    if (sMonth == null)
                    {
                        iMonth = st.Month;
                    }
                    else
                    {
                        iMonth = System.Convert.ToInt32(sMonth);
                    }
                }

                if (this.ShowDate)
                {
                    if (sDay == null)
                    {
                        iDay = st.Day;
                    }
                    else
                    {
                        iDay = System.Convert.ToInt32(sDay);
                    }
                }

                if (this.ShowHour)
                {
                    if (sHour == null)
                    {
                        iHour = st.Hour;
                    }
                    else
                    {
                        iHour = System.Convert.ToInt32(sHour);
                    }
                }

                if (this.ShowMinute)
                {
                    if (sMinute == null)
                    {
                        iMinute = st.Minute;
                    }
                    else
                    {
                        iMinute = System.Convert.ToInt32(sMinute);
                    }
                }

                if (this.ShowSecond)
                {
                    if (sSecond == null)
                    {
                        iSecond = st.Second;
                    }
                    else
                    {
                        iSecond = System.Convert.ToInt32(sSecond);
                    }
                }

                System.DateTime dt = new System.DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
                if (dt != this.DateTime)
                {
                    this.DateTime = dt;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                if (this.CheckError != null)
                {
                    this.CheckError(this, new Thinksea.WebControls.DateTimePicker.CheckErrorEventArgs(sYear, sMonth, sDay, sHour, sMinute, sSecond));
                }
                return false;
            }

        }


		/// <summary>
		/// 重写System.Web.UI.Control.OnPreRender>方法。
		/// </summary>
		/// <param name="e">包含事件数据的对象。</param>
		protected override void OnPreRender(EventArgs e)
		{
			this.Page.RegisterRequiresPostBack(this);
			this.Page.RegisterRequiresRaiseEvent(this);
            string imageUrl;
            string DateTimePickerUpImage;
            string DateTimePickerDownImage;
            if (this.UseIncludeResource)
            {
                //if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "Thinksea.WebControls.DateTimePicker"))
                //{
                this.Page.ClientScript.RegisterClientScriptResource(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.DateTimePicker.js");
                //}
                if (!this.Enabled || this.ReadOnly)
                {
                    imageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.disDateTimePicker.bmp");
                }
                else
                {
                    imageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.DateTimePicker.bmp");
                }
                DateTimePickerUpImage = this.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.DateTimePickerUp.gif");
                DateTimePickerDownImage = this.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.DateTimePickerDown.gif");
            }
            else
            {
                if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "Thinksea.WebControls.DateTimePicker"))
                {
                    this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Thinksea.WebControls.DateTimePicker", this.ImageUrl + "DateTimePicker.js");
                }
                if (!this.Enabled || this.ReadOnly)
                {
                    imageUrl = this.ImageUrl + "disDateTimePicker.bmp";
                }
                else
                {
                    imageUrl = this.ImageUrl + "DateTimePicker.bmp";
                }
                DateTimePickerUpImage = this.ImageUrl + "DateTimePickerUp.gif";
                DateTimePickerDownImage = this.ImageUrl + "DateTimePickerDown.gif";
            }

            this.Controls.Add(new System.Web.UI.LiteralControl("<script type=\"text/javascript\" language=\"javascript\">DateTimePicker_DrawCalendar(\"" + this.ClientID + "\", " + this.DateTime.Year.ToString() + ",  " + this.DateTime.Month.ToString() + ",  " + this.DateTime.Day.ToString() + ",  " + this.DateTime.Hour.ToString() + ",  " + this.DateTime.Minute.ToString() + ",  " + this.DateTime.Second.ToString() + ", \"" + imageUrl + "\", " + this.ReadOnly.ToString().ToLower() + ", " + this.Enabled.ToString().ToLower() + ", " + this.ShowDate.ToString().ToLower() + ", " + this.ShowHour.ToString().ToLower() + ", " + this.ShowMinute.ToString().ToLower() + ", " + this.ShowSecond.ToString().ToLower() + ", " + this.ShowDataSelector.ToString().ToLower() + ", \"" + DateTimePickerUpImage + "\", \"" + DateTimePickerDownImage + "\");</script>"));
            this.Width = System.Web.UI.WebControls.Unit.Empty;
            this.Height = System.Web.UI.WebControls.Unit.Empty;

            base.OnPreRender(e);

		}

	}

}
