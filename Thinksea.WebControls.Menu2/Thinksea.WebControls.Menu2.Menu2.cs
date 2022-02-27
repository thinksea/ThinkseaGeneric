using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Diagnostics;

namespace Thinksea.WebControls.Menu2
{
	/// <summary>
	/// 两级菜单控件。
	/// </summary>
	[ToolboxData("<{0}:Menu2 runat=server></{0}:Menu2>"),
	DefaultProperty("MenuXML"),
	System.ComponentModel.DefaultEvent("ItemSelectedCommand"),
	ValidationPropertyAttribute("Text"),
	Designer(typeof(Thinksea.WebControls.Menu2.Menu2Designer))
	]
	public class Menu2 : System.Web.UI.WebControls.WebControl, IPostBackEventHandler, INamingContainer
	{
		/// <summary>
		/// 初始化此实例。
		/// </summary>
		public Menu2()
		{

		}


		/// <summary>
		/// 菜单文件名。
		/// </summary>
		private string _MenuXML = "";
		/// <summary>
		/// 获取或设置菜单文件名，XML格式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(""),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("菜单文件名（XML格式）"),
		System.ComponentModel.NotifyParentProperty(true)
		]
		public string MenuXML
		{
			get
			{
				if (this.IsTrackingViewState)
				{
					object savedState = this.ViewState["MenuXML"];
					if(savedState != null) this._MenuXML = (string)savedState;
				}
				return this._MenuXML;
			}
			set
			{
				this._MenuXML = value;
				if (this.IsTrackingViewState)
					ViewState["MenuXML"] = value;
			}
		}


		/// <summary>
		/// 允许访问菜单的权限列表。
		/// </summary>
		private System.Collections.ArrayList _Powers = null;
		/// <summary>
		/// 获取或设置允许访问菜单的权限列表。
		/// </summary>
		public string [] Powers
		{
			get
			{
				if(this._Powers == null) 
				{
					this._Powers = new System.Collections.ArrayList();
					if (this.IsTrackingViewState)
					{
						object savedState = this.ViewState["Powers"];
						if(savedState != null)
						{
							string [] pwsl = ((System.String)savedState).Split(';');
							foreach( string tmp in pwsl )
							{
								if( tmp.Length > 0 )
								{
									this._Powers.Add( tmp );
								}
							}
						}
					}
				}
				return (string [])(this._Powers.ToArray(typeof(string)));
			}
			set
			{
				if( this._Powers == null )
				{
					this._Powers = new System.Collections.ArrayList();
				}
				this._Powers.Clear();
				this._Powers.AddRange( value );
				if (this.IsTrackingViewState)
					ViewState["Powers"] = string.Join( ";", value );
			}
		}


		/// <summary>
		/// 菜单标题样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuTitleStyle = null;
		/// <summary>
		/// 获取或设置菜单标题样式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("菜单标题样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuTitleStyle
		{
			get
			{
				if(this._MenuTitleStyle == null) 
				{
					this._MenuTitleStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuTitleStyle).TrackViewState();
				}
				return this._MenuTitleStyle;
			}
			set
			{
				this._MenuTitleStyle = value;
			}
		}


		/// <summary>
		/// 鼠标移入菜单标题的样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuTitleOnMouseOverStyle = null;
		/// <summary>
		/// 获取或设置鼠标移入菜单标题的样式。
		/// </summary>
		[ 
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("鼠标移入菜单标题的样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuTitleOnMouseOverStyle
		{
			get
			{
				if(this._MenuTitleOnMouseOverStyle == null) 
				{
					this._MenuTitleOnMouseOverStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuTitleOnMouseOverStyle).TrackViewState();
				}
				return this._MenuTitleOnMouseOverStyle;
			}
			set
			{
				this._MenuTitleOnMouseOverStyle = value;
			}
		}


		/// <summary>
		/// 鼠标移出菜单标题的样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuTitleOnMouseOutStyle = null;
		/// <summary>
		/// 获取或设置鼠标移出菜单标题的样式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("鼠标移出菜单标题的样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuTitleOnMouseOutStyle
		{
			get
			{
				if(this._MenuTitleOnMouseOutStyle == null) 
				{
					this._MenuTitleOnMouseOutStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuTitleOnMouseOutStyle).TrackViewState();
				}
				return this._MenuTitleOnMouseOutStyle;
			}
			set
			{
				this._MenuTitleOnMouseOutStyle = value;
			}
		}


		/// <summary>
		/// 折叠的菜单标题样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuTitleCollapseStyle = null;
		/// <summary>
		/// 获取或设置折叠的菜单标题样式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("折叠的菜单标题样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuTitleCollapseStyle
		{
			get
			{
				if(this._MenuTitleCollapseStyle == null) 
				{
					this._MenuTitleCollapseStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuTitleCollapseStyle).TrackViewState();
				}
				return this._MenuTitleCollapseStyle;
			}
			set
			{
				this._MenuTitleCollapseStyle = value;
			}
		}


		/// <summary>
		/// 菜单项样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuItemStyle = null;
		/// <summary>
		/// 获取或设置菜单项样式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("菜单项样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuItemStyle
		{
			get
			{
				if(this._MenuItemStyle == null) 
				{
					this._MenuItemStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuItemStyle).TrackViewState();
				}
				return this._MenuItemStyle;
			}
			set
			{
				this._MenuItemStyle = value;
			}
		}


		/// <summary>
		/// 指示是否显示菜单项分隔符。
		/// </summary>
		private bool _ShowMenuItemSeparator = true;
		/// <summary>
		/// 获取或设置一个值，指示是否显示菜单项分隔符。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("指示是否显示菜单项分隔符"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public bool ShowMenuItemSeparator
		{
			get
			{
				if (this.IsTrackingViewState)
				{
					object savedState = this.ViewState["ShowMenuItemSeparator"];
					if(savedState != null) this._ShowMenuItemSeparator = (bool)savedState;
				}
				return this._ShowMenuItemSeparator;
			}
			set
			{
				this._ShowMenuItemSeparator = value;
				if (this.IsTrackingViewState)
					ViewState["ShowMenuItemSeparator"] = value;
			}
		}


		/// <summary>
		/// 菜单项分隔符样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuItemSeparatorStyle = null;
		/// <summary>
		/// 获取或设置菜单项分隔符样式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("菜单项分隔符样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuItemSeparatorStyle
		{
			get
			{
				if(this._MenuItemSeparatorStyle == null) 
				{
					this._MenuItemSeparatorStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuItemSeparatorStyle).TrackViewState();
				}
				return this._MenuItemSeparatorStyle;
			}
			set
			{
				this._MenuItemSeparatorStyle = value;
			}
		}


		/// <summary>
		/// 鼠标移入菜单组样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuGroupStyle = null;
		/// <summary>
		/// 获取或设置鼠标移入菜单组样式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("鼠标移入菜单组样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuGroupStyle
		{
			get
			{
				if(this._MenuGroupStyle == null)
				{
					this._MenuGroupStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuGroupStyle).TrackViewState();
				}
				return this._MenuGroupStyle;
			}
			set
			{
				this._MenuGroupStyle = value;
			}
		}


		/// <summary>
		/// 指示是否显示菜单组分隔符。
		/// </summary>
		private bool _ShowMenuGroupSeparator = true;
		/// <summary>
		/// 获取或设置一个值，指示是否显示菜单组分隔符。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("指示是否显示菜单组分隔符"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public bool ShowMenuGroupSeparator
		{
			get
			{
				if (this.IsTrackingViewState)
				{
					object savedState = this.ViewState["ShowMenuGroupSeparator"];
					if(savedState != null) this._ShowMenuGroupSeparator = (bool)savedState;
				}
				return this._ShowMenuGroupSeparator;
			}
			set
			{
				this._ShowMenuGroupSeparator = value;
				if (this.IsTrackingViewState)
					ViewState["ShowMenuGroupSeparator"] = value;
			}
		}


		/// <summary>
		/// 菜单组分隔符样式。
		/// </summary>
		private System.Web.UI.WebControls.TableItemStyle _MenuGroupSeparatorStyle = null;
		/// <summary>
		/// 获取或设置菜单组分隔符样式。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("菜单组分隔符样式"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.ComponentModel.NotifyParentProperty(true),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		public System.Web.UI.WebControls.TableItemStyle MenuGroupSeparatorStyle
		{
			get
			{
				if(this._MenuGroupSeparatorStyle == null)
				{
					this._MenuGroupSeparatorStyle = new System.Web.UI.WebControls.TableItemStyle();
					if (this.IsTrackingViewState)
						((System.Web.UI.IStateManager)this._MenuGroupSeparatorStyle).TrackViewState();
				}
				return this._MenuGroupSeparatorStyle;
			}
			set
			{
				this._MenuGroupSeparatorStyle = value;
			}
		}


		/// <summary>
		/// 每行最多显示的菜单项数量。
		/// </summary>
		private int _MaxColumn = 1;
		/// <summary>
		/// 获取或设置每行最多显示的菜单项数量。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(1),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("每行最多显示的菜单项数量"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public int MaxColumn
		{
			get
			{
				if (this.IsTrackingViewState)
				{
					object savedState = this.ViewState["MaxColumn"];
					if(savedState != null) this._MaxColumn = (int)savedState;
				}
				return this._MaxColumn;
			}
			set
			{
				if( value < 1 )
				{
					throw new System.ArgumentOutOfRangeException(nameof(value), value, "指定的参数已超出有效取值的范围，该参数取值不能小于 1。");
				}
				this._MaxColumn = value;
				if (this.IsTrackingViewState)
					ViewState["MaxColumn"] = value;
			}
		}



		/// <summary>
		/// 当前选择的项目编号。
		/// </summary>
		private System.String _SelectedItemID = "";
		/// <summary>
		/// 获取或设置当前选择的项目编号。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(0),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("当前选择的项目编号"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public System.String SelectedItemID
		{
			get
			{
				if (this.IsTrackingViewState)
				{
					object savedState = this.ViewState["SelectedItemID"];
					if(savedState != null) this._SelectedItemID = (System.String)savedState;
				}
				return this._SelectedItemID;
			}
			set
			{
				this._SelectedItemID = value;
				if (this.IsTrackingViewState)
					ViewState["SelectedItemID"] = this._SelectedItemID;
			}
		}


		/// <summary>
		/// 处理控件回发事件。
		/// </summary>
		/// <param name="eventArgument"></param>
		public void RaisePostBackEvent(string eventArgument)
		{
			if( eventArgument.StartsWith("ItemID_") )
			{
				string NewItemID = eventArgument.Substring( "ItemID_".Length );
				this.SelectedItemID = NewItemID;
				if( this.ItemSelectedCommand != null )
				{
					this.ItemSelectedCommand( this, new System.Web.UI.WebControls.CommandEventArgs("ItemSelected", NewItemID));
				}
			}
		}

		/// <summary>
		/// 菜单项事件代理。
		/// </summary>
		public delegate void ItemCommandEventHandler(object source, System.Web.UI.WebControls.CommandEventArgs e);
		/// <summary>
		/// 当用户选择了新的菜单项引发此事件。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("当用户选择了新的菜单项引发此事件"),
		]
		public event ItemCommandEventHandler ItemSelectedCommand = null;
		/// <summary>
		/// 菜单项绑定事件代理。
		/// </summary>
		public delegate void ItemEventHandler(object source, Thinksea.WebControls.Menu2.ItemEventArgs e);
		/// <summary>
		/// 在菜单项被绑定后发生。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(null),
		System.ComponentModel.Category("Data"), 
		System.ComponentModel.Description("在分页被绑定后发生"),
		]
		public event ItemEventHandler ItemDataBound = null;


		/// <summary>
		/// 根据服务器样式生成客户端 html 代码style="...."格式的引号中的内容。
		/// </summary>
		/// <param name="a"></param>
		private string StyleToCssString( System.Web.UI.WebControls.TableItemStyle a )
		{
			string result = "";
			if( ! a.BackColor.IsEmpty )
			{
				result += " background-color: " + System.Drawing.ColorTranslator.ToHtml(a.BackColor) + ";";
			}
		{
			string tmp = "";
			if( a.BorderStyle != System.Web.UI.WebControls.BorderStyle.NotSet )
			{
				tmp += " " + a.BorderStyle.ToString();
			}
			if( ! a.BorderWidth.IsEmpty )
			{
				tmp += " " + a.BorderWidth.ToString();
			}
			if( ! a.BorderColor.IsEmpty )
			{
				tmp += " " + System.Drawing.ColorTranslator.ToHtml(a.BorderColor);
			}
			if( tmp != "" )
			{
				result += " border:" + tmp + ";";
			}
		}
			if( a.Font != null )
			{
				if( a.Font.Bold )
				{
					result += " font-weight: bold;";
				}
				if( a.Font.Italic )
				{
					result += " font-style: italic;";
				}
				if( a.Font.Names.Length > 0 )
				{
					result += " font-family: " + System.String.Join(", ", a.Font.Names) + ";";
				}
			{
				string tmp = "";
				if( a.Font.Underline )
				{
					tmp += " underline";
				}
				if( a.Font.Overline )
				{
					tmp += " overline";
				}
				if( a.Font.Strikeout )
				{
					tmp += " line-through";
				}
				if( tmp != "" )
				{
					result += " text-decoration:" + tmp + ";";
				}
			}
				if( ! a.Font.Size.IsEmpty )
				{
					result += " font-size: " + a.Font.Size.ToString() + ";";
				}
			}
			if( ! a.ForeColor.IsEmpty )
			{
				result += " color: " + System.Drawing.ColorTranslator.ToHtml(a.ForeColor) + ";";
			}
			if( a.HorizontalAlign != System.Web.UI.WebControls.HorizontalAlign.NotSet )
			{
				result += " text-align: " + a.HorizontalAlign.ToString() + ";";
			}
			if( a.VerticalAlign != System.Web.UI.WebControls.VerticalAlign.NotSet )
			{
				result += " vertical-align: " + a.VerticalAlign.ToString() + ";";
			}
			if( ! a.Wrap )
			{
				result += " white-space: nowrap;";
			}
			if( ! a.Height.IsEmpty )
			{
				result += " height: " + a.Height.ToString() + ";";
			}
			if( ! a.Width.IsEmpty )
			{
				result += " width: " + a.Width.ToString() + ";";
			}
			return result;

		}


		/// <summary>
		/// 将此控件呈现给指定的输出参数，并使用指定的数据初始化控件。
		/// </summary>
		/// <param name="writer"> 接收控件内容的 HtmlTextWriter 编写器 </param>
		/// <param name="MenuXML">菜单文件名。</param>
		public void RenderControl( HtmlTextWriter writer, string MenuXML )
		{
			Thinksea.WebControls.Menu2.Menu menuConnection = new Thinksea.WebControls.Menu2.Menu( MenuXML );// 菜单数据库联接。

			string OutMenuHtmlText = // 输出菜单 HTML 文本。
				@"<script type='text/javascript' id='clientEventHandlersJS'>
<!--
function Thinksea_WebControls_Menu2_menuChange_" + this.ID + @"(obj,menu)
{
	var controls = document.getElementById('" + this.ID + @"').all;
	for (var i=0; i < controls.length; i++)
	{
		if( controls[i].id == menu )
		{
			menu = controls[i];
		}
	}
	if(menu.style.display=='')
	{";
			if(this.MenuTitleCollapseStyle != null )
			{
				if(this.MenuTitleCollapseStyle.CssClass != null && this.MenuTitleCollapseStyle.CssClass != "" )
				{
					OutMenuHtmlText = OutMenuHtmlText + @"
		obj.className='" + this.MenuTitleCollapseStyle.CssClass + @"';";
				}
				string styleStr = this.StyleToCssString( this.MenuTitleCollapseStyle );
				if( styleStr != "" )
				{
					OutMenuHtmlText = OutMenuHtmlText + @"
		obj.style.cssText='" + styleStr + @"';";
				}
			}
			OutMenuHtmlText += @"
		menu.style.display='none';
	}else{";
			if(this.MenuTitleStyle != null )
			{
				if(this.MenuTitleStyle.CssClass != null && this.MenuTitleStyle.CssClass != "" )
				{
					OutMenuHtmlText = OutMenuHtmlText + @"
		obj.className='" + this.MenuTitleStyle.CssClass + @"';";
				}
				string styleStr = this.StyleToCssString( this.MenuTitleStyle );
				if( styleStr != "" )
				{
					OutMenuHtmlText = OutMenuHtmlText + @"
		obj.style.cssText='" + styleStr + @"';";
				}
			}
			OutMenuHtmlText += @"
		menu.style.display='';
	}
}
//-->
		</script>";

			System.Web.UI.WebControls.Panel MenuPanel = new System.Web.UI.WebControls.Panel();
			MenuPanel.ID = this.ID;
			if( this.Style["Z-INDEX"] != null ) MenuPanel.Style["Z-INDEX"] = this.Style["Z-INDEX"];
			if( this.Style["POSITION"] != null ) MenuPanel.Style["POSITION"] = this.Style["POSITION"];
			if( this.Style["LEFT"] != null ) MenuPanel.Style["LEFT"] = this.Style["LEFT"];
			if( this.Style["TOP"] != null ) MenuPanel.Style["TOP"] = this.Style["TOP"];
			MenuPanel.ApplyStyle(this.ControlStyle);
			MenuPanel.Enabled = this.Enabled;
			MenuPanel.Visible = this.Visible;

			int menuGroupCount = 0;//菜单组数量
			int menuItemCount;//菜单组中菜单项数量

			Thinksea.WebControls.Menu2.MenuGroup [] mgis = menuConnection.GetMenuGroup( );
			foreach( Thinksea.WebControls.Menu2.MenuGroup tmpmgis in mgis )
			{
				Thinksea.WebControls.Menu2.MenuItem [] miis = menuConnection.GetMenuItemOfMenuGroupIDWithAccessFilter( tmpmgis.ID, this.Powers );
				if( miis.Length > 0 )
				{
					menuItemCount = 0;

					#region 菜单集合。
					System.Web.UI.WebControls.Table MenuGroup = new System.Web.UI.WebControls.Table();
					MenuGroup.Width = new System.Web.UI.WebControls.Unit("100%");
					MenuGroup.CellSpacing = 0;
					MenuGroup.CellPadding = 0;

					#region 菜单组标题
					System.Web.UI.WebControls.TableRow MenuGroupTitleRow = new System.Web.UI.WebControls.TableRow();
					MenuGroupTitleRow.Style["CURSOR"] = "hand";
					#region
					System.Web.UI.WebControls.TableCell MenuGroupTitleCell = new System.Web.UI.WebControls.TableCell();
					if( tmpmgis.Expand )
					{
						MenuGroupTitleCell.ApplyStyle( this.MenuTitleStyle );
					}
					else
					{
						MenuGroupTitleCell.ApplyStyle( this.MenuTitleCollapseStyle );
					}
					MenuGroupTitleCell.Attributes["onclick"] = "Thinksea_WebControls_Menu2_menuChange_" + this.ID + "(this,'" + tmpmgis.ID.Replace("\"", "\\\"") + "');";

					#region 填充菜单组标题
					System.Web.UI.WebControls.Label TitleText = new System.Web.UI.WebControls.Label();
					TitleText.ApplyStyle( this.MenuTitleOnMouseOutStyle );
					if(this.MenuTitleOnMouseOverStyle != null)
					{
						string styleStr = this.StyleToCssString(this.MenuTitleOnMouseOverStyle);
						if( styleStr != "" )
						{
							TitleText.Attributes["onmouseover"] = "this.style.cssText='" + styleStr + "';";
						}
						else
						{
							if( this.MenuTitleOnMouseOverStyle.CssClass != null && this.MenuTitleOnMouseOverStyle.CssClass != "" )
							{
								TitleText.Attributes["onmouseover"] = "this.className='" + this.MenuTitleOnMouseOverStyle.CssClass + "';";
							}
						}
					}
					if(this.MenuTitleOnMouseOutStyle != null)
					{
						string styleStr = this.StyleToCssString(this.MenuTitleOnMouseOutStyle);
						if( styleStr != "" )
						{
							TitleText.Attributes["onmouseout"] = "this.style.cssText='" + styleStr + "';";
						}
						else
						{
							if(this.MenuTitleOnMouseOutStyle.CssClass != null && this.MenuTitleOnMouseOutStyle.CssClass != "" )
							{
								TitleText.Attributes["onmouseout"] = "this.className='" + this.MenuTitleOnMouseOutStyle.CssClass + "';";
							}
						}
					}
					TitleText.Text = tmpmgis.Text;

					MenuGroupTitleCell.Controls.Add( TitleText );
					#endregion

					MenuGroupTitleRow.Cells.Add( MenuGroupTitleCell );
					#endregion
					MenuGroup.Rows.Add(MenuGroupTitleRow);
					#endregion
					
					#region 菜单项集合。
					System.Web.UI.WebControls.TableRow MenuGroupRow = new System.Web.UI.WebControls.TableRow();
					#region
					System.Web.UI.WebControls.TableCell MenuGroupCell = new System.Web.UI.WebControls.TableCell();

					#region
					System.Web.UI.WebControls.Panel MenuGroupPanel = new System.Web.UI.WebControls.Panel();
					MenuGroupPanel.ApplyStyle( this.MenuGroupStyle );
					MenuGroupPanel.ID = tmpmgis.ID;
					if( ! tmpmgis.Expand ) MenuGroupPanel.Style["DISPLAY"] = "none";

					#region 填充菜单项
					int CellMaxCount = this.MaxColumn;//用来控制每行最多显示的菜单项数量
					int CellIndex = 0;//当前显示的菜单项行索引。用来辅助CellMaxCount完成控制每行显示的菜单项数量
					foreach( Thinksea.WebControls.Menu2.MenuItem tmpmiis in miis )
					{
						#region 插入菜单项分隔符
						if( this.ShowMenuItemSeparator && CellIndex > 0 )
						{
							System.Web.UI.WebControls.Label menuItemSeparator = new System.Web.UI.WebControls.Label();
							menuItemSeparator.ApplyStyle( this.MenuItemSeparatorStyle );
							MenuGroupPanel.Controls.Add( menuItemSeparator );
						}
						#endregion

						#region 菜单项
						System.Web.UI.WebControls.HyperLink MenuItem = new System.Web.UI.WebControls.HyperLink();
						MenuItem.ApplyStyle( this.MenuItemStyle );
						MenuItem.ID = tmpmiis.ID;
						MenuItem.Text = tmpmiis.Text;
						if( this.Enabled )
						{
							if( tmpmiis.URL.Length == 0 )
							{
								MenuItem.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "ItemID_" + tmpmiis.ID);
							}
							else
							{
								MenuItem.Target = tmpmiis.Target;
								MenuItem.NavigateUrl = tmpmiis.URL;
							}
							if( this.ItemDataBound != null )
							{
								this.ItemDataBound(this, new Thinksea.WebControls.Menu2.ItemEventArgs( MenuItem, tmpmiis ) );
							}
						}

						MenuGroupPanel.Controls.Add( MenuItem );
						#endregion

						menuItemCount ++;
						CellIndex ++;
						if( CellIndex >= CellMaxCount )
						{
							CellIndex = 0;
							System.Web.UI.WebControls.Literal MenuItemSplit = new System.Web.UI.WebControls.Literal();
							MenuItemSplit.Text = "<br>";
							MenuGroupPanel.Controls.Add( MenuItemSplit );

						}

					}
					#endregion

					MenuGroupCell.Controls.Add(MenuGroupPanel);
					#endregion

					MenuGroupRow.Cells.Add(MenuGroupCell);
					#endregion
					MenuGroup.Rows.Add(MenuGroupRow);
					#endregion

					if( menuItemCount > 0 )
					{
						if( this.ShowMenuGroupSeparator && menuGroupCount > 0 )
						{
							System.Web.UI.WebControls.Panel menuGroupSeparator = new System.Web.UI.WebControls.Panel();
							menuGroupSeparator.ApplyStyle( this.MenuGroupSeparatorStyle );
							MenuPanel.Controls.Add( menuGroupSeparator );
						}
						MenuPanel.Controls.Add( MenuGroup );
						menuGroupCount ++;
					}
					#endregion

				}
			}
			writer.Write(OutMenuHtmlText);
			MenuPanel.RenderControl( writer );

		}


		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
			this.RenderControl(output, System.Web.HttpContext.Current.Server.MapPath(this.MenuXML));

		}


		/// <summary>
		/// 加载视图状态。
		/// </summary>
		/// <param name="savedState"></param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null) 
			{
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					((System.Web.UI.IStateManager)this.MenuTitleStyle).LoadViewState(myState[1]);
				if (myState[2] != null)
					((System.Web.UI.IStateManager)this.MenuTitleOnMouseOverStyle).LoadViewState(myState[2]);
				if (myState[3] != null)
					((System.Web.UI.IStateManager)this.MenuTitleOnMouseOutStyle).LoadViewState(myState[3]);
				if (myState[4] != null)
					((System.Web.UI.IStateManager)this.MenuTitleCollapseStyle).LoadViewState(myState[4]);
				if (myState[5] != null)
					((System.Web.UI.IStateManager)this.MenuItemStyle).LoadViewState(myState[5]);
				if (myState[6] != null)
					((System.Web.UI.IStateManager)this.MenuItemSeparatorStyle).LoadViewState(myState[6]);
				if (myState[7] != null)
					((System.Web.UI.IStateManager)this.MenuGroupStyle).LoadViewState(myState[7]);
				if (myState[8] != null)
					((System.Web.UI.IStateManager)this.MenuGroupSeparatorStyle).LoadViewState(myState[8]);
			}
		}

		/// <summary>
		/// 保存视图状态。
		/// </summary>
		/// <returns></returns>
		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();

			object MenuTitleStyleState = (this.MenuTitleStyle != null) ? ((System.Web.UI.IStateManager)this.MenuTitleStyle).SaveViewState() : null;
			object MenuTitleOnMouseOverStyleState = (this.MenuTitleOnMouseOverStyle != null) ? ((System.Web.UI.IStateManager)this.MenuTitleOnMouseOverStyle).SaveViewState() : null;
			object MenuTitleOnMouseOutStyleState = (this.MenuTitleOnMouseOutStyle != null) ? ((System.Web.UI.IStateManager)this.MenuTitleOnMouseOutStyle).SaveViewState() : null;
			object MenuTitleCollapseStyleState = (this.MenuTitleCollapseStyle != null) ? ((System.Web.UI.IStateManager)this.MenuTitleCollapseStyle).SaveViewState() : null;
			object MenuItemStyleState = (this.MenuItemStyle != null) ? ((System.Web.UI.IStateManager)this.MenuItemStyle).SaveViewState() : null;
			object MenuItemSeparatorStyleState = (this.MenuItemSeparatorStyle != null) ? ((System.Web.UI.IStateManager)this.MenuItemSeparatorStyle).SaveViewState() : null;
			object MenuGroupStyleState = (this.MenuGroupStyle != null) ? ((System.Web.UI.IStateManager)this.MenuGroupStyle).SaveViewState() : null;
			object MenuGroupSeparatorStyleState = (this.MenuGroupSeparatorStyle != null) ? ((System.Web.UI.IStateManager)this.MenuGroupSeparatorStyle).SaveViewState() : null;

			object[] myState = new object[9];
			myState[0] = baseState;
			myState[1] = MenuTitleStyleState;
			myState[2] = MenuTitleOnMouseOverStyleState;
			myState[3] = MenuTitleOnMouseOutStyleState;
			myState[4] = MenuTitleCollapseStyleState;
			myState[5] = MenuItemStyleState;
			myState[6] = MenuItemSeparatorStyleState;
			myState[7] = MenuGroupStyleState;
			myState[8] = MenuGroupSeparatorStyleState;

			return myState;

		}

	}
}
