using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.ComponentModel.Design;


namespace Thinksea.WebControls.DateTimePicker
{
    /// <summary>
    /// 服务器控件设计器
    /// </summary>
    public class DateTimePickerDesigner : System.Web.UI.Design.ControlDesigner
    {
        /// <summary>
        /// 获取一个值，该值指示控件的大小是否可以调整。
        /// </summary>
        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 初始化CalendarDesigner新实例
        /// </summary>
        public DateTimePickerDesigner()
        {
            //
            // TODO: Add constructor logic here
            //
            //this.ReadOnly=true;

        }


        private Thinksea.WebControls.DateTimePicker.DateTimePicker DateTimePicker = null;

        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="component"></param>
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            this.DateTimePicker = (Thinksea.WebControls.DateTimePicker.DateTimePicker)component;
            base.Initialize(component);

        }

        /// <summary>
        /// 获取用于在设计时表示关联控件的 HTML
        /// </summary>
        /// <returns>用于在设计时表示控件的 HTML</returns>
        public override string GetDesignTimeHtml()
        {
            try
            {
                string imageUrl;
                string DateTimePickerUpImage;
                string DateTimePickerDownImage;
                if (DateTimePicker.UseIncludeResource)
                {
                    this.DateTimePicker.Page.ClientScript.RegisterClientScriptResource(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.DateTimePicker.js");
                    if (!this.DateTimePicker.Enabled || this.DateTimePicker.ReadOnly)
                    {
                        imageUrl = this.DateTimePicker.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.disDateTimePicker.bmp");
                    }
                    else
                    {
                        imageUrl = this.DateTimePicker.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.DateTimePicker.bmp");
                    }
                    DateTimePickerUpImage = this.DateTimePicker.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.DateTimePickerUp.gif");
                    DateTimePickerDownImage = this.DateTimePicker.Page.ClientScript.GetWebResourceUrl(typeof(DateTimePicker), "Thinksea.WebControls.DateTimePicker.images.DateTimePickerDown.gif");
                    //imageUrl = DateTimePicker.ImageUrl + "DateTimePicker.bmp";
                }
                else
                {
                    if (!this.DateTimePicker.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "Thinksea.WebControls.DateTimePicker"))
                    {
                        this.DateTimePicker.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Thinksea.WebControls.DateTimePicker", this.DateTimePicker.ImageUrl + "DateTimePicker.js");
                    }
                    if (!this.DateTimePicker.Enabled || this.DateTimePicker.ReadOnly)
                    {
                        imageUrl = this.DateTimePicker.ImageUrl + "disDateTimePicker.bmp";
                    }
                    else
                    {
                        imageUrl = this.DateTimePicker.ImageUrl + "DateTimePicker.bmp";
                    }
                    DateTimePickerUpImage = this.DateTimePicker.ImageUrl + "DateTimePickerUp.gif";
                    DateTimePickerDownImage = this.DateTimePicker.ImageUrl + "DateTimePickerDown.gif";
                    //imageUrl = DateTimePicker.Page.ClientScript.GetWebResourceUrl(DateTimePicker.GetType(), "Thinksea.WebControls.DateTimePicker.images.DateTimePicker.bmp");
                }

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                DateTimePicker.Width = System.Web.UI.WebControls.Unit.Empty;
                DateTimePicker.Height = System.Web.UI.WebControls.Unit.Empty;

                DateTimePicker.RenderBeginTag(htw);
                string str = "";
                //if(!enabled)
                if (!DateTimePicker.Enabled)
                {
                    str = @"<table cellSpacing='0' cellPadding='0' border='0' width='0'>
<tr>
<td nowrap>";// disabled='disabled'
                    if (DateTimePicker.ShowDate)
                    {
                        str += DateTimePicker.DateTime.Year + "年" + DateTimePicker.DateTime.Month + "月" + DateTimePicker.DateTime.Day + "日";
                    }
                    if (DateTimePicker.ShowHour)
                    {
                        str += DateTimePicker.DateTime.Hour;
                    }
                    if (DateTimePicker.ShowMinute)
                    {
                        str += ":" + DateTimePicker.DateTime.Minute;
                    }
                    if (DateTimePicker.ShowSecond)
                    {
                        str += ":" + DateTimePicker.DateTime.Second;
                    }

                    str += "</td>";
                    if (DateTimePicker.ShowDate)
                    {
                        str += @"<td>
<button disabled='disabled' style='BORDER: black 0px solid; MARGIN: 0px; WIDTH: 20px; HEIGHT: 20px' type=button >
<IMG src=""" + imageUrl + @""" />
</button>
</td>";
                    }
                    str += @"</tr>
</table>";
                }
                else
                {
                    str = @"<table cellSpacing='0' cellPadding='0' border='0' width='0'>
<tr>
<td nowrap>";
                    if (DateTimePicker.ShowDate)
                    {
                        str += DateTimePicker.DateTime.Year + "年" + DateTimePicker.DateTime.Month + "月" + DateTimePicker.DateTime.Day + "日";
                    }
                    if (DateTimePicker.ShowHour)
                    {
                        str += DateTimePicker.DateTime.Hour;
                    }
                    if (DateTimePicker.ShowMinute)
                    {
                        str += ":" + DateTimePicker.DateTime.Minute;
                    }
                    if (DateTimePicker.ShowSecond)
                    {
                        str += ":" + DateTimePicker.DateTime.Second;
                    }

                    str += "</td>";
                    if (DateTimePicker.ShowDate)
                    {
                        str += @"<td>
<button style='BORDER: black 0px solid; MARGIN: 0px; WIDTH: 20px; HEIGHT: 20px' type='button'>
<IMG src=""" + imageUrl + @""" />
</button>
</td>";
                    }
                    str += @"</tr>
</table>";
                }

                htw.Write(str);
                DateTimePicker.RenderEndTag(htw);

                return sw.ToString();

            }
            catch (Exception e)
            {
                return GetErrorDesignTimeHtml(e);
            }
        }

        /// <summary>
        /// 获取在呈现控件时遇到错误后在设计时为指定的异常显示的 HTML
        /// </summary>
        /// <param name="e">要为其显示错误信息的异常</param>
        /// <returns>设计时为指定的异常显示的 HTML</returns>
        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string errorstr = "创建控件时出错！" + e.Message;
            return CreatePlaceHolderDesignTimeHtml(errorstr);

        }


    }

}
