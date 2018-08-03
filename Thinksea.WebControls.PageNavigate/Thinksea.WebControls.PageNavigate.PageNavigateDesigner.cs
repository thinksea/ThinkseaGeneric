using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.WebControls.PageNavigate
{
    /// <summary>
    /// 服务器控件设计器
    /// </summary>
    public class PageNavigateDesigner : System.Web.UI.Design.ControlDesigner
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
        /// 初始化PageNavigate新实例
        /// </summary>
        public PageNavigateDesigner()
        {
            //
            // TODO: Add constructor logic here
            //
            //this.ReadOnly=true;

        }

        private Thinksea.WebControls.PageNavigate.PageNavigate pageNavigate = null;

        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="component"></param>
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            this.pageNavigate = (Thinksea.WebControls.PageNavigate.PageNavigate)component;
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
                this.pageNavigate.RecordsCount = 99;

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                //pageNavigate.Width = System.Web.UI.WebControls.Unit.Empty;
                //pageNavigate.Height = System.Web.UI.WebControls.Unit.Empty;

                pageNavigate.RenderControl(htw);
                //pageNavigate.RenderBeginTag(htw);
                //string str = "";

                //htw.Write(str);
                //pageNavigate.RenderEndTag(htw);

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
