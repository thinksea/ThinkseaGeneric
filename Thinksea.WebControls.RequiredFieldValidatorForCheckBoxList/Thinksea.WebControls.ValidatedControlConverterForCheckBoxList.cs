using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thinksea.WebControls
{
    /// <summary>
    /// 提供检索当前容器中控件 ID 的列表的类型转换器。
    /// </summary>
    [AspNetHostingPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(System.Security.Permissions.SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ValidatedControlConverterForCheckBoxList : ControlIDConverter
    {
        /// <summary>
        /// 返回一个值，该值指示是否将指定控件的控件 ID 添加到控件选择器中。
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        protected override bool FilterControl(Control control)
        {
            return control is CheckBoxList;
            //ValidationPropertyAttribute attribute = (ValidationPropertyAttribute)TypeDescriptor.GetAttributes(control)[typeof(ValidationPropertyAttribute)];
            //return ((attribute != null) && (attribute.Name != null));
        }
    }

}
