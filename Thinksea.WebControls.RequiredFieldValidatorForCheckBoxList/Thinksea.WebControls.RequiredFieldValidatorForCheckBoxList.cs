using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thinksea.WebControls
{
    /// <summary>
    /// 对复选框列表控件“CheckBoxList”执行至少选中一项的验证。
    /// </summary>
    [DefaultProperty("ErrorMessage")]
    [ToolboxData("<{0}:RequiredFieldValidatorForCheckBoxList runat=server ErrorMessage=\"RequiredFieldValidatorForCheckBoxList\"></{0}:RequiredFieldValidatorForCheckBoxList>")]
    public class RequiredFieldValidatorForCheckBoxList : BaseValidator
    {
        /// <summary>
        /// 获取或设置要验证的输入控件。
        /// </summary>
        [TypeConverter(typeof(ValidatedControlConverterForCheckBoxList)), Description("获取或设置要验证的输入控件。"), Category("Behavior"), Themeable(false), DefaultValue(""), IDReferenceProperty]
        public new string ControlToValidate
        {
            get
            {
                return base.ControlToValidate;
            }
            set
            {
                base.ControlToValidate = value;
            }
        }

        /// <summary>
        /// 验证控件是否有效。
        /// </summary>
        /// <returns></returns>
        protected override bool ControlPropertiesValid()
        {
            Control c = FindControl(ControlToValidate);
            if (c != null)
            {
                return c is CheckBoxList;
            }
            return false;
        }

        /// <summary>
        /// 验证用户输入控件的值是否有效。
        /// </summary>
        /// <returns></returns>
        protected override bool EvaluateIsValid()
        {
            CheckBoxList c = (CheckBoxList)FindControl(ControlToValidate);
            foreach (ListItem li in c.Items)
            {
                if (li.Selected)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.EnableClientScript)
            {
                string s_Script = @"
function RequiredFieldValidatorForCheckBoxListEvaluateIsValid(sender) {
    var val = document.getElementById(document.getElementById(sender.id).controltovalidate);
    var col = val.getElementsByTagName(""*"");
    if ( col != null ) {
        for ( i = 0; i < col.length; i++ ) {
            if (col.item(i).tagName == ""INPUT"") {
                if ( col.item(i).checked ) {
                    return true;
                }
            }
        }
        return false;
    }
}
";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RequiredFieldValidatorForCheckBoxListEvaluateIsValid", s_Script, true);
                Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "RequiredFieldValidatorForCheckBoxListEvaluateIsValid");
            }

        }

    }

}
