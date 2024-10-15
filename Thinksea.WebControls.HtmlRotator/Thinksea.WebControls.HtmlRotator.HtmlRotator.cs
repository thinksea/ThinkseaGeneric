/*
利用属性扩展元数据：ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpguide/html/cpconextendingmetadatausingattributes.htm
<a href="#"><img src='adv/1.jpg' width="100%" height="100%" border=0/></a>|
<a href="#"><img src='adv/2.jpg' width="100%" height="100%" border=0/></a>|
<a href="#"><img src='adv/3.jpg' width="100%" height="100%" border=0/></a>|
<a href="#"><img src='adv/4.jpg' width="100%" height="100%" border=0/></a>|
<table width="100%" height="100%"><tr><td width="100%" height="100%"><img src='adv/5.jpg' width="100%" height="100%"/></td></tr><tr><td>eeeeeeeeeee</td></tr></table>|
<table width="100%" height="100%"><tr><td width="100%" height="100%"><img src='adv/6.jpg' width="100%" height="100%"/></td></tr><tr><td>fffffffffff</td></tr></table>|
<table width="100%" height="100%"><tr><td width="100%" height="100%"><img src='adv/7.jpg' width="100%" height="100%"/></td></tr><tr><td>ggggggggggg</td></tr></table>|
<table width="100%" height="100%"><tr><td width="100%" height="100%"><img src='adv/8.jpg' width="100%" height="100%"/></td></tr><tr><td>hhhhhhhhhhh</td></tr></table>
*/
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Thinksea.WebControls.HtmlRotator
{
	/// <summary>
	/// 轮换显示方法。
	/// </summary>
	public enum HtmlRotatorType
	{
		/// <summary>
		/// 从右向左滚动
		/// </summary>
		RightToLeft = 1,
		/// <summary>
		/// 从左向右滚动
		/// </summary>
		LeftToRight = 2,
		/// <summary>
		/// 从上向下滚动
		/// </summary>
		TopToBottom = 3,
		/// <summary>
		/// 从下向上滚动
		/// </summary>
		BottomToTop = 4,
		/// <summary>
		/// 随机应用多种样式轮换显示
		/// </summary>
		Multi = 5,
	}

	/// <summary>
	///	一个用于轮换显示HTML内容的控件.
	/// </summary>
	[
	System.ComponentModel.Description("一个用于轮换显示HTML内容的控件"),
	System.ComponentModel.DefaultProperty("HtmlsArr"),
	System.ComponentModel.Designer(typeof(Thinksea.WebControls.HtmlRotator.HtmlRotatorDesigner)),
	System.Web.UI.ToolboxData("<{0}:HtmlRotator runat=server></{0}:HtmlRotator>")
	]

	public class HtmlRotator : System.Web.UI.WebControls.CompositeControl //, INamingContainer//, IPostBackDataHandler, IPostBackEventHandler
	{
        /// <summary>
        /// 获取与此 Web 服务器控件相对应的 System.Web.UI.HtmlTextWriterTag 值。此属性主要由控件开发人员使用。
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        #region 预定义数据。
        /// <summary>
		/// 基本效果滤镜列表。
		/// </summary>
		private string [] _FiltersArr = new string []{
														//24种转换效果拆分开是为了避免由于使用属性transition=23带来的弊端，此弊端描述为因为嵌套的两次随机产生效果造成分布不均匀。
														"RevealTrans(duration=2,transition=0)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=1)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=2)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=3)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=4)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=5)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=6)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=7)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=8)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=9)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=10)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=11)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=12)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=13)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=14)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=15)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=16)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=17)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=18)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=19)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=20)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=21)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=22)"	//提供了24种转换对象内容的效果
														, "RevealTrans(duration=2,transition=23)"	//提供了24种转换对象内容的效果

														, "BlendTrans(duration=2)"	//用渐隐效果转换对象内容

													};
		/// <summary>
		/// 获取或设置基本效果滤镜列表。
		/// </summary>
		[
		System.ComponentModel.Browsable(false),
		System.ComponentModel.ReadOnly(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("基本效果滤镜列表。"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		private string [] FiltersArr
		{
			get
			{
				return this._FiltersArr;
			}
			set
			{
				this._FiltersArr = value;
			}
        }

		/// <summary>
		/// 当IE浏览器版本号IE5.5+时执行的效果滤镜列表。
		/// </summary>
		private string [] _FiltersArrIE55 = new string []{
															"progid:DXImageTransform.Microsoft.Pixelate(,enabled=false,duration=2,maxSquare=25)"	//
															, "progid:DXImageTransform.Microsoft.Fade(duration=2,overlap=0)"	//用渐隐效果转换对象内容

															, "progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=forward )"	//用滚动渐隐效果转换对象内容（转换在水平方向上自左至右）
															, "progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=reverse )"	//用滚动渐隐效果转换对象内容（转换在水平方向上自右至左）
															, "progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=forward,WipeStyle=1 )"	//用滚动渐隐效果转换对象内容（转换在垂直方向上自上至下）
															, "progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=reverse,WipeStyle=1 )"	//用滚动渐隐效果转换对象内容（转换在垂直方向上自下至上）

															, "progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=SPIN)"	//用拉伸(缩)变形效果转换对象内容（在旧内容上自中心向左右两边拉伸新内容）
															, "progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=PUSH)"	//用拉伸(缩)变形效果转换对象内容（自左向右拉伸新内容近来同时挤压旧内容出去。这种转换方式的视觉效果类似立方体从一面转到另一面）
															, "progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=HIDE)"	//用拉伸(缩)变形效果转换对象内容（在旧内容上自左向右拉伸(缩)新内容）

															, "progid:DXImageTransform.Microsoft.Wheel(duration=2,spokes=16)"	//用风车叶轮旋转效果转换对象内容
															, "progid:DXImageTransform.Microsoft.RandomDissolve(duration=2)"	//用随机像素溶解效果转换对象内容
															, "progid:DXImageTransform.Microsoft.Spiral(duration=2,gridSizeX=50,gridSizeY=50)"	//用矩形螺旋方式转换对象内容

															, "progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=SWAP)"	//用滑条抽离效果转换对象内容（在抽离的同时交换新旧内容）
															, "progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=PUSH)"	//用滑条抽离效果转换对象内容（抽离旧内容时同步同向拉进新内容）
															, "progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=HIDE)"	//用滑条抽离效果转换对象内容（在新内容上抽离旧内容）

															, "progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=CLOCK)"	//用放射状擦除效果转换对象内容。效果类似汽车挡风玻璃的刮雨刀（围绕对象的中心，自上方开始，顺时针旋转擦除）
															, "progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=WEDGE)"	//用放射状擦除效果转换对象内容。效果类似汽车挡风玻璃的刮雨刀（围绕对象的中心，自上方开始，同时向两边旋转擦除）
															, "progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=RADIAL)"	//用放射状擦除效果转换对象内容。效果类似汽车挡风玻璃的刮雨刀（以对象的左上角为圆心旋转擦除）

															, "progid:DXImageTransform.Microsoft.Pixelate(Duration=1,MaxSquare=15)"	//这个转换滤镜是一个复杂的视觉效果。在转换的前半段，对象内容先显示为矩形色块拼贴，然后矩形的宽度由一个像素增加至 MaxSquare 属性所设置的值。每个矩形的颜色由其所覆盖区域的像素的颜色平均值决定。接下来的转换的后半段，矩形被还原为新内容具体的图像像素，显示出新的内容。在使用此转换滤镜前设置此滤镜的 Enabled 特性值为 false 。这将预防在转换发生前彩色拼贴效果的静态滤镜先在对象内容上发生作用
														};
		/// <summary>
		/// 获取或设置当IE浏览器版本号IE5.5+时执行的效果滤镜列表。
		/// </summary>
		[
		System.ComponentModel.Browsable(false),
		System.ComponentModel.ReadOnly(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("当IE浏览器版本号IE5.5+时执行的效果滤镜列表。"),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
		System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
		]
		private string [] FiltersArrIE55
		{
			get
			{
				return this._FiltersArrIE55;
			}
			set
			{
				this._FiltersArrIE55 = value;
			}
		}

        #endregion

		/// <summary>
		/// 添加 HTML 代码片断。
		/// </summary>
		/// <param name="Html">HTML 代码片断</param>
		public void AddHtml( string Html )
		{
			this.Htmls.Add( Html );
		}

		/// <summary>
		/// 添加多个 HTML 代码片断。
		/// </summary>
		/// <param name="Htmls">HTML 代码片断数组。</param>
		public void AddHtmlRange( string [] Htmls )
		{
			this.Htmls.AddRange( Htmls );
		}

		/// <summary>
		/// 删除 HTML 代码片断。
		/// </summary>
		/// <param name="Html">HTML 代码片断</param>
		public void RemoveHtml( string Html )
		{
			this.Htmls.Remove( Html );
		}

		/// <summary>
		/// HTML 代码块集合。
		/// </summary>
		private System.Collections.Specialized.StringCollection _Htmls = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// 获取 HTML 代码块集合。
		/// </summary>
		[
		System.ComponentModel.Browsable(false),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("获取 HTML 代码块集合。"),
		System.ComponentModel.DesignOnlyAttribute(true),
		]
		public System.Collections.Specialized.StringCollection Htmls
		{
			get
			{
				return this._Htmls;
			}
        }

        /// <summary>
		/// 获取或设置基本效果滤镜列表（以管道符号“|”分隔）。
		/// </summary>
		/// <remarks>
		/// 可以使用的 CSS 滤镜列表：（关于 CSS 滤镜的更多内容请参考 CSS 帮助文档。）
		/// RevealTrans(duration=2,transition=0)|
		/// RevealTrans(duration=2,transition=1)|
		/// RevealTrans(duration=2,transition=2)|
		/// RevealTrans(duration=2,transition=3)|
		/// RevealTrans(duration=2,transition=4)|
		/// RevealTrans(duration=2,transition=5)|
		/// RevealTrans(duration=2,transition=6)|
		/// RevealTrans(duration=2,transition=7)|
		/// RevealTrans(duration=2,transition=8)|
		/// RevealTrans(duration=2,transition=9)|
		/// RevealTrans(duration=2,transition=10)|
		/// RevealTrans(duration=2,transition=11)|
		/// RevealTrans(duration=2,transition=12)|
		/// RevealTrans(duration=2,transition=13)|
		/// RevealTrans(duration=2,transition=14)|
		/// RevealTrans(duration=2,transition=15)|
		/// RevealTrans(duration=2,transition=16)|
		/// RevealTrans(duration=2,transition=17)|
		/// RevealTrans(duration=2,transition=18)|
		/// RevealTrans(duration=2,transition=19)|
		/// RevealTrans(duration=2,transition=20)|
		/// RevealTrans(duration=2,transition=21)|
		/// RevealTrans(duration=2,transition=22)|
		/// RevealTrans(duration=2,transition=23)|
		/// BlendTrans(duration=2)
		/// </remarks>
		[
		System.ComponentModel.Browsable(true),
		System.ComponentModel.DefaultValue("RevealTrans(duration=2,transition=0)|RevealTrans(duration=2,transition=1)|RevealTrans(duration=2,transition=2)|RevealTrans(duration=2,transition=3)|RevealTrans(duration=2,transition=4)|RevealTrans(duration=2,transition=5)|RevealTrans(duration=2,transition=6)|RevealTrans(duration=2,transition=7)|RevealTrans(duration=2,transition=8)|RevealTrans(duration=2,transition=9)|RevealTrans(duration=2,transition=10)|RevealTrans(duration=2,transition=11)|RevealTrans(duration=2,transition=12)|RevealTrans(duration=2,transition=13)|RevealTrans(duration=2,transition=14)|RevealTrans(duration=2,transition=15)|RevealTrans(duration=2,transition=16)|RevealTrans(duration=2,transition=17)|RevealTrans(duration=2,transition=18)|RevealTrans(duration=2,transition=19)|RevealTrans(duration=2,transition=20)|RevealTrans(duration=2,transition=21)|RevealTrans(duration=2,transition=22)|RevealTrans(duration=2,transition=23)|BlendTrans(duration=2)"),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("基本效果滤镜列表（以管道符号“|”分隔）。"),
		System.ComponentModel.DesignOnlyAttribute(true),
		]
		public string Filter
		{
			get
			{
				if( this.FiltersArr == null )
				{
					return null;
				}
				else
				{
					return System.String.Join( "|", this.FiltersArr );
				}
			}
			set
			{
				if( value == null )
				{
					this.FiltersArr = null;
				}
				else
				{
					value = value.Replace("\r", "").Replace("\n", "");
					System.Collections.ArrayList al = new System.Collections.ArrayList();
					al.AddRange(value.Split('|'));
					for( int i = al.Count - 1; i >= 0; i-- )
					{
						if( al[i] == null || al[i].ToString() == "" )
						{
							al.RemoveAt( i );
						}
					}
					if( al.Count > 0 )
					{
						this.FiltersArr = (string [])(al.ToArray(typeof(string)));
					}
					else
					{
						this.FiltersArr = null;
					}
				}
			}
		}


		/// <summary>
		/// 获取或设置当IE浏览器版本号IE5.5+时执行的效果滤镜列表（以管道符号“|”分隔）。
		/// </summary>
		/// <remarks>
		/// 可以使用的 CSS 滤镜列表：（关于 CSS 滤镜的更多内容请参考 CSS 帮助文档。）
		/// progid:DXImageTransform.Microsoft.Pixelate(,enabled=false,duration=2,maxSquare=25)|
		/// progid:DXImageTransform.Microsoft.Fade(duration=2,overlap=0)|
		/// progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=forward )|
		/// progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=reverse )|
		/// progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=forward,WipeStyle=1 )|
		/// progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=reverse,WipeStyle=1 )|
		/// progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=SPIN)|
		/// progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=PUSH)|
		/// progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=HIDE)|
		/// progid:DXImageTransform.Microsoft.Wheel(duration=2,spokes=16)|
		/// progid:DXImageTransform.Microsoft.RandomDissolve(duration=2)|
		/// progid:DXImageTransform.Microsoft.Spiral(duration=2,gridSizeX=50,gridSizeY=50)|
		/// progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=SWAP)|
		/// progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=PUSH)|
		/// progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=HIDE)|
		/// progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=CLOCK)|
		/// progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=WEDGE)|
		/// progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=RADIAL)|
		/// progid:DXImageTransform.Microsoft.Pixelate(Duration=1,MaxSquare=15)
		/// </remarks>
		[
		System.ComponentModel.Browsable(true),
		System.ComponentModel.DefaultValue("progid:DXImageTransform.Microsoft.Pixelate(,enabled=false,duration=2,maxSquare=25)|progid:DXImageTransform.Microsoft.Fade(duration=2,overlap=0)|progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=forward )|progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=reverse )|progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=forward,WipeStyle=1 )|progid:DXImageTransform.Microsoft.GradientWipe(duration=2,gradientSize=0.25,motion=reverse,WipeStyle=1 )|progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=SPIN)|progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=PUSH)|progid:DXImageTransform.Microsoft.Stretch(duration=2,stretchStyle=HIDE)|progid:DXImageTransform.Microsoft.Wheel(duration=2,spokes=16)|progid:DXImageTransform.Microsoft.RandomDissolve(duration=2)|progid:DXImageTransform.Microsoft.Spiral(duration=2,gridSizeX=50,gridSizeY=50)|progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=SWAP)|progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=PUSH)|progid:DXImageTransform.Microsoft.Slide(duration=2,bands=1,slideStyle=HIDE)|progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=CLOCK)|progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=WEDGE)|progid:DXImageTransform.Microsoft.RadialWipe(duration=2,wipeStyle=RADIAL)|progid:DXImageTransform.Microsoft.Pixelate(Duration=1,MaxSquare=15)"),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("当IE浏览器版本号IE5.5+时执行的效果滤镜列表（以管道符号“|”分隔）。"),
		System.ComponentModel.DesignOnlyAttribute(true),
		]
		public string FiltersIE55
		{
			get
			{
				if( this.FiltersArrIE55 == null )
				{
					return null;
				}
				else
				{
					return System.String.Join( "|", this.FiltersArrIE55 );
				}
			}
			set
			{
				if( value == null )
				{
					this.FiltersArrIE55 = null;
				}
				else
				{
					value = value.Replace("\r", "").Replace("\n", "");
					System.Collections.ArrayList al = new System.Collections.ArrayList();
					al.AddRange(value.Split('|'));
					for( int i = al.Count - 1; i >= 0; i-- )
					{
						if( al[i] == null || al[i].ToString() == "" )
						{
							al.RemoveAt( i );
						}
					}
					if( al.Count > 0 )
					{
						this.FiltersArrIE55 = (string [])(al.ToArray(typeof(string)));
					}
					else
					{
						this.FiltersArrIE55 = null;
					}
				}
			}
		}


		/// <summary>
        /// 播放速度。
		/// </summary>
		private int _PlaySpeed = 3000;
		/// <summary>
        /// 获取或设置播放速度。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(3000),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("播放速度。"),
		]
		public int PlaySpeed
		{
			get
			{
				return this._PlaySpeed;
			}
			set
			{
				this._PlaySpeed = value;
			}
		}


		/// <summary>
        /// 切换相邻页时停顿时间（注意：在这个版本中，次属性只对于滚动效果有效）。
		/// </summary>
        private int _PauseTime = -1;
		/// <summary>
        /// 获取或设置切换相邻页时停顿时间（注意：在这个版本中，次属性只对于滚动效果有效）。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(-1),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("切换相邻页时停顿时间（注意：在这个版本中，次属性只对于滚动效果有效）。"),
		]
        public int PauseTime
		{
			get
			{
                return this._PauseTime;
			}
			set
			{
                this._PauseTime = value;
			}
		}


		/// <summary>
		/// 停止图片交换当鼠标移入显示区域。
		/// </summary>
		private bool _StopOnMouseOver = false;
		/// <summary>
		/// 获取或设置停止图片交换当鼠标移入显示区域。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(false),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("停止图片交换当鼠标移入显示区域。"),
		]
		public bool StopOnMouseOver
		{
			get
			{
				return this._StopOnMouseOver;
			}
			set
			{
				this._StopOnMouseOver = value;
			}
		}


		/// <summary>
		/// 指示轮换显示方法。
		/// </summary>
		private HtmlRotatorType _HtmlRotatorType = HtmlRotatorType.Multi;
		/// <summary>
		/// 获取或设置轮换显示方法。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(HtmlRotatorType.Multi),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("指示轮换显示方法。"),
		]
		public HtmlRotatorType HtmlRotatorType
		{
			get
			{
				return this._HtmlRotatorType;
			}
			set
			{
				this._HtmlRotatorType = value;
			}
		}


		/// <summary>
		/// 指示当单方向滚动效果时 HTML 代码块 之间的间隔距离。
		/// </summary>
		private System.Web.UI.WebControls.Unit _Interval = System.Web.UI.WebControls.Unit.Empty;
		/// <summary>
		/// 获取或设置当单方向滚动效果时 HTML 代码块 之间的间隔距离。
		/// </summary>
		[
		System.ComponentModel.DefaultValue( typeof(System.Web.UI.WebControls.Unit), "" ),
		System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"), 
		System.ComponentModel.Description("指示当单方向滚动效果时 HTML 代码块 之间的间隔距离。"),
		]
		public System.Web.UI.WebControls.Unit Interval
		{
			get
			{
				return this._Interval;
			}
			set
			{
				this._Interval = value;
			}
		}


        /// <summary> 
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="writer"> 要写出到的 HTML 编写器 </param>
        private void Render_Multi(HtmlTextWriter writer)
        {
            this.Attributes.Add("HtmlIndex", "0");
            //this.Style.Add("OVERFLOW", "hidden");
            //if (string.IsNullOrEmpty(this.Style["width"])) this.Style.Add("width", this.Width.ToString());
            //if (string.IsNullOrEmpty(this.Style["height"])) this.Style.Add("height", this.Height.ToString());
            if (this.StopOnMouseOver)
            {
                this.Attributes.Add("onmouseover", this.ClientID + "_Start = false;");
                this.Attributes.Add("onmouseout", this.ClientID + "_Start = true;");
            }
            this.RenderBeginTag(writer);

            this.RenderEndTag(writer);

            string html = @"<SCRIPT language=javascript>
var " + this.ClientID + @"_HtmlsArr = new Array();";
            if (this.Htmls.Count > 0)
            {
                foreach (string tmp in this.Htmls)
                {
                    html += "\r\n" + this.ClientID + "_HtmlsArr.push(\"" + tmp.Replace("\r", "").Replace("\n", "").Replace("\"", "\\\"") + "\");";
                }
            }
            html += @"
var " + this.ClientID + @"_Start = true;
function " + this.ClientID + @"_Show( msec, panelControlID, tempPanelControlID )
{
	if( ! " + this.ClientID + @"_Start )
	{
		window.setTimeout(""" + this.ClientID + @"_Show(""+msec+"", \""""+panelControlID+""\"", \""""+tempPanelControlID+""\"")"", 500, ""JavaScript"");
		return;
	}
	var CanPlay = parseFloat(navigator.appVersion.split("";"")[1].split("" "")[2]) > 5;//检测浏览器版本号
	var HtmlsArr = " + this.ClientID + @"_HtmlsArr;
	var FiltersArr = new Array();";
            foreach (string tmp in this.FiltersArr)
            {
                html += "\r\nFiltersArr.push(\"" + tmp + "\");";
            }

            html += @"
	if( CanPlay )//只有IE5.5+版本才能执行如下滤镜效果
	{";
            foreach (string tmp in this.FiltersArrIE55)
            {
                html += "\r\nFiltersArr.push(\"" + tmp + "\");";
            }
            html += @"
	}

	var panelControl = document.getElementById(panelControlID);
	var tempPanelControl = document.getElementById(tempPanelControlID);
	var HtmlIndex = panelControl.HtmlIndex;
	if( HtmlIndex>=HtmlsArr.length )
	{
		HtmlIndex = 0;
	}
	if( HtmlIndex < HtmlsArr.length )
	{
		while( HtmlIndex<HtmlsArr.length && HtmlsArr[HtmlIndex]=="""")
		{
			HtmlIndex ++;
		}
		if( HtmlIndex>=HtmlsArr.length )
		{
			HtmlIndex = 0;
		}
		if(HtmlsArr[HtmlIndex]!="""")
		{
			if( FiltersArr.length > 0 )
			{
				var FilterIndex = parseInt( Math.random() * FiltersArr.length);
				panelControl.style.filter = FiltersArr[FilterIndex];
				panelControl.filters[0].Apply();
				panelControl.innerHTML = HtmlsArr[HtmlIndex];
				panelControl.filters[0].play();
			}
			else
			{
				panelControl.innerHTML = HtmlsArr[HtmlIndex];
			}
			HtmlIndex ++;
			if( HtmlIndex>=HtmlsArr.length )
			{
				HtmlIndex = 0;
			}
			tempPanelControl.innerHTML = HtmlsArr[HtmlIndex];
		}
	}
	panelControl.HtmlIndex = HtmlIndex;
	window.setTimeout(""" + this.ClientID + @"_Show(""+msec+"", \""""+panelControlID+""\"", \""""+tempPanelControlID+""\"")"", msec, ""JavaScript"");
}
</SCRIPT>
<div id=""" + this.ClientID + @"_TempPanel"" style=""DISPLAY: none;"" ></div>
<SCRIPT language=javascript>" + this.ClientID + @"_Show(" + this.PlaySpeed + @", """ + this.ClientID + @""", """ + this.ClientID + @"_TempPanel"");</SCRIPT>";
            writer.Write(html);

        }

        /// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="writer"> 要写出到的 HTML 编写器 </param>
		private void Render_RightToLeft(HtmlTextWriter writer)
		{
            this.RenderBeginTag(writer);
            if (this.Htmls.Count > 0)
			{
				System.Web.UI.WebControls.Table tableArray = new System.Web.UI.WebControls.Table();
				tableArray.Height = System.Web.UI.WebControls.Unit.Parse("100%");
				tableArray.BorderWidth = System.Web.UI.WebControls.Unit.Parse("0px");
				tableArray.CellPadding = 0;
				tableArray.CellSpacing = 0;
				System.Web.UI.WebControls.TableRow trowArray = new System.Web.UI.WebControls.TableRow();
				tableArray.Rows.Add( trowArray );
				for( int i = 0; i < this.Htmls.Count; i++ )
				{
					System.Web.UI.WebControls.TableCell tc = new System.Web.UI.WebControls.TableCell();
					tc.Text = this.Htmls[i];
					trowArray.Cells.Add( tc );
					if( this.Interval != System.Web.UI.WebControls.Unit.Empty )//插入 HTML 代码块 之间的间隔距离。
					{
						System.Web.UI.WebControls.TableCell sptc = new System.Web.UI.WebControls.TableCell();
						sptc.Text = "<div style=\"WIDTH:" + this.Interval.ToString() + "\" />";
						trowArray.Cells.Add( sptc );
					}

				}
				System.IO.StringWriter sw = new System.IO.StringWriter();
				System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter( sw );
				tableArray.RenderControl( htw );
                writer.Write(sw.ToString());

			}

            writer.Write(@"<script type=""text/javascript"">
HtmlRotator('" + this.ClientID + @"', 2, " + this.PlaySpeed.ToString() + @", " + this.PauseTime.ToString() + ", " + this.StopOnMouseOver.ToString().ToLowerInvariant() + @");
</script>
");

            this.RenderEndTag(writer);
		}

		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="writer"> 要写出到的 HTML 编写器 </param>
		private void Render_LeftToRight(HtmlTextWriter writer)
		{
            this.RenderBeginTag(writer);
            if (this.Htmls.Count > 0)
            {
                System.Web.UI.WebControls.Table tableArray = new System.Web.UI.WebControls.Table();
                tableArray.Height = System.Web.UI.WebControls.Unit.Parse("100%");
                tableArray.BorderWidth = System.Web.UI.WebControls.Unit.Parse("0px");
                tableArray.CellPadding = 0;
                tableArray.CellSpacing = 0;
                System.Web.UI.WebControls.TableRow trowArray = new System.Web.UI.WebControls.TableRow();
                tableArray.Rows.Add(trowArray);
                for (int i = 0; i < this.Htmls.Count; i++)
                {
                    System.Web.UI.WebControls.TableCell tc = new System.Web.UI.WebControls.TableCell();
                    tc.Text = this.Htmls[i];
                    trowArray.Cells.Add(tc);
                    if (this.Interval != System.Web.UI.WebControls.Unit.Empty)//插入 HTML 代码块 之间的间隔距离。
                    {
                        System.Web.UI.WebControls.TableCell sptc = new System.Web.UI.WebControls.TableCell();
                        sptc.Text = "<div style=\"WIDTH:" + this.Interval.ToString() + "\" />";
                        trowArray.Cells.Add(sptc);
                    }

                }
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                tableArray.RenderControl(htw);
                writer.Write(sw.ToString());

            }

            writer.Write(@"<script type=""text/javascript"">
HtmlRotator('" + this.ClientID + @"', 4, " + this.PlaySpeed.ToString() + @", " + this.PauseTime.ToString() + ", " + this.StopOnMouseOver.ToString().ToLowerInvariant() + @");
</script>
");

            this.RenderEndTag(writer);
        }

		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="writer"> 要写出到的 HTML 编写器 </param>
		private void Render_BottomToTop(HtmlTextWriter writer)
		{
            this.RenderBeginTag(writer);
            if (this.Htmls.Count > 0)
            {
                System.Web.UI.WebControls.Table tableArray = new System.Web.UI.WebControls.Table();
                tableArray.Width = System.Web.UI.WebControls.Unit.Parse("100%");
                tableArray.BorderWidth = System.Web.UI.WebControls.Unit.Parse("0px");
                tableArray.CellPadding = 0;
                tableArray.CellSpacing = 0;
                for (int i = 0; i < this.Htmls.Count; i++)
                {
                    System.Web.UI.WebControls.TableRow trowArray = new System.Web.UI.WebControls.TableRow();
                    tableArray.Rows.Add(trowArray);
                    System.Web.UI.WebControls.TableCell tc = new System.Web.UI.WebControls.TableCell();
                    tc.Text = this.Htmls[i];
                    trowArray.Cells.Add(tc);
                    if (this.Interval != System.Web.UI.WebControls.Unit.Empty)//插入 HTML 代码块 之间的间隔距离。
                    {
                        System.Web.UI.WebControls.TableRow sptrowArray = new System.Web.UI.WebControls.TableRow();
                        tableArray.Rows.Add(sptrowArray);
                        System.Web.UI.WebControls.TableCell sptc = new System.Web.UI.WebControls.TableCell();
                        sptc.Text = "<div style=\"HEIGHT:" + this.Interval.ToString() + "\" />";
                        sptrowArray.Cells.Add(sptc);
                    }

                }
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                tableArray.RenderControl(htw);
                writer.Write( sw.ToString());

            }

            writer.Write(@"<script type=""text/javascript"">
HtmlRotator('" + this.ClientID + @"', 1, " + this.PlaySpeed.ToString() + @", " + this.PauseTime.ToString() + ", " + this.StopOnMouseOver.ToString().ToLowerInvariant() + @");
</script>
");

            this.RenderEndTag(writer);
		}

		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="writer"> 要写出到的 HTML 编写器 </param>
		private void Render_TopToBottom(HtmlTextWriter writer)
		{
            this.RenderBeginTag(writer);
            if (this.Htmls.Count > 0)
            {
                System.Web.UI.WebControls.Table tableArray = new System.Web.UI.WebControls.Table();
                tableArray.Width = System.Web.UI.WebControls.Unit.Parse("100%");
                tableArray.BorderWidth = System.Web.UI.WebControls.Unit.Parse("0px");
                tableArray.CellPadding = 0;
                tableArray.CellSpacing = 0;
                for (int i = 0; i < this.Htmls.Count; i++)
                {
                    System.Web.UI.WebControls.TableRow trowArray = new System.Web.UI.WebControls.TableRow();
                    tableArray.Rows.Add(trowArray);
                    System.Web.UI.WebControls.TableCell tc = new System.Web.UI.WebControls.TableCell();
                    tc.Text = this.Htmls[i];
                    trowArray.Cells.Add(tc);
                    if (this.Interval != System.Web.UI.WebControls.Unit.Empty)//插入 HTML 代码块 之间的间隔距离。
                    {
                        System.Web.UI.WebControls.TableRow sptrowArray = new System.Web.UI.WebControls.TableRow();
                        tableArray.Rows.Add(sptrowArray);
                        System.Web.UI.WebControls.TableCell sptc = new System.Web.UI.WebControls.TableCell();
                        sptc.Text = "<div style=\"HEIGHT:" + this.Interval.ToString() + "\" />";
                        sptrowArray.Cells.Add(sptc);
                    }

                }
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                tableArray.RenderControl(htw);
                writer.Write(sw.ToString());

            }

            writer.Write(@"<script type=""text/javascript"">
HtmlRotator('" + this.ClientID + @"', 3, " + this.PlaySpeed.ToString() + @", " + this.PauseTime.ToString() + ", " + this.StopOnMouseOver.ToString().ToLowerInvariant() + @");
</script>
");

            this.RenderEndTag(writer);
        }

        /// <summary>
        /// 由 ASP.NET 页面框架调用，以通知使用基于合成的实现的服务器控件创建它们包含的任何子控件，以便为回发或呈现做准备。
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Style.Add("OVERFLOW", "hidden");

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "HtmlRotator"))
            {
/*
功能：HTML 滚动显示。
参数：
    ctlid：控件 ID
    arrow：滚动方向{1=向上滚动；2=向左滚动；3=向下滚动；4=向右滚动}
    playSpeed：滚动速度{取值范围：非负数}
    pauseTime：切换相邻页时停顿时间{取值范围：-1表示忽略此参数；非负数表示等待时间}
    stopOnMouseOver：指示当鼠标移动到显示区域上时是否停止滚动。{true=停止；false=不停止}
*/
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "HtmlRotator", @"
function HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver) {
    if (pauseTime == -1) {
        pauseTime = playSpeed;
    }
    var pan = document.getElementById(ctlid);
    switch (arrow) {
        case 2: //向左滚动
            {
                var marqueesWidth = pan.clientWidth;

                if (!pan.init) {
                    if (pan.scrollWidth < marqueesWidth) {
                        return false;
                    }
                    if (stopOnMouseOver) {
                        pan.onmouseover = function() { this.stopscroll = true; };
                        pan.onmouseout = function() { this.stopscroll = false; };
                    }
                    pan.style.overflow = ""hidden"";
                    if (pan.childNodes[0].clientWidth >= pan.clientWidth) {
                        pan.innerHTML = ""<table cellpadding='0' cellspacing='0' border='0'><tr><td>"" + pan.innerHTML + ""</td><td>"" + pan.innerHTML + ""</td></tr></table>"";
                    }
                    pan.scrollLeft = 0;
                    pan.currentScroll = 0;
                    pan.init = true;
                    if (pauseTime > 0) {
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                }

                if (pan.stopscroll != true) {
                    if (pan.currentScroll >= marqueesWidth) {
                        pan.currentScroll = 0;
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                    pan.scrollLeft += 1;
                    pan.currentScroll += 1;
                    if (pan.scrollLeft >= pan.scrollWidth / 2)
                        pan.scrollLeft = 0;
                }
            }
            break;
        case 4: //向右滚动
            {
                var marqueesWidth = pan.clientWidth;

                if (!pan.init) {
                    if (pan.scrollWidth < marqueesWidth) {
                        return false;
                    }
                    if (stopOnMouseOver) {
                        pan.onmouseover = function() { this.stopscroll = true; };
                        pan.onmouseout = function() { this.stopscroll = false; };
                    }
                    pan.style.overflow = ""hidden"";
                    if (pan.childNodes[0].clientWidth >= pan.clientWidth) {
                        pan.innerHTML = ""<table cellpadding='0' cellspacing='0' border='0'><tr><td>"" + pan.innerHTML + ""</td><td>"" + pan.innerHTML + ""</td></tr></table>"";
                    }
                    pan.scrollLeft = pan.scrollWidth / 2;
                    pan.currentScroll = 0;
                    pan.init = true;
                    if (pauseTime > 0) {
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                }

                if (pan.stopscroll != true) {
                    if (pan.currentScroll >= marqueesWidth) {
                        pan.currentScroll = 0;
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                    pan.scrollLeft -= 1;
                    pan.currentScroll += 1;
                    if (pan.scrollLeft <= 0)
                        pan.scrollLeft = pan.scrollWidth / 2;
                }
            }
            break;
        case 3: //向下滚动
            {
                var marqueesHeight = pan.clientHeight;

                if (!pan.init) {
                    if (pan.scrollHeight < marqueesHeight) {
                        return false;
                    }
                    if (stopOnMouseOver) {
                        pan.onmouseover = function() { this.stopscroll = true; };
                        pan.onmouseout = function() { this.stopscroll = false; };
                    }
                    pan.style.overflow = ""hidden"";
                    if (pan.childNodes[0].clientHeight >= pan.clientHeight) {
                        pan.innerHTML = ""<table cellpadding='0' cellspacing='0' border='0'><tr><td>"" + pan.innerHTML + ""</td></tr><tr><td>"" + pan.innerHTML + ""</td></tr></table>"";
                    }
                    pan.scrollTop = pan.scrollHeight / 2;
                    pan.currentScroll = 0;
                    pan.init = true;
                    if (pauseTime > 0) {
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                }

                if (pan.stopscroll != true) {
                    if (pan.currentScroll >= marqueesHeight) {
                        pan.currentScroll = 0;
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                    pan.scrollTop -= 1;
                    pan.currentScroll += 1;
                    if (pan.scrollTop <= 0)
                        pan.scrollTop = pan.scrollHeight / 2;
                }
            }
            break;
        case 1://向上滚动
        default:
            {
                var marqueesHeight = pan.clientHeight;

                if (!pan.init) {
                    if (pan.scrollHeight < marqueesHeight) {
                        return false;
                    }
                    if (stopOnMouseOver) {
                        pan.onmouseover = function() { this.stopscroll = true; };
                        pan.onmouseout = function() { this.stopscroll = false; };
                    }
                    pan.style.overflow = ""hidden"";
                    if (pan.childNodes[0].clientHeight >= pan.clientHeight) {
                        pan.innerHTML = ""<table cellpadding='0' cellspacing='0' border='0'><tr><td>"" + pan.innerHTML + ""</td></tr><tr><td>"" + pan.innerHTML + ""</td></tr></table>"";
                    }
                    pan.scrollTop = 0;
                    pan.currentScroll = 0;
                    pan.init = true;
                    if (pauseTime > 0) {
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                }

                if (pan.stopscroll != true) {
                    if (pan.currentScroll >= marqueesHeight) {
                        pan.currentScroll = 0;
                        setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, pauseTime);
                        return;
                    }
                    pan.scrollTop += 1;
                    pan.currentScroll += 1;
                    if (pan.scrollTop >= pan.scrollHeight / 2)
                        pan.scrollTop = 0;
                }
            }
            break;
    }

    setTimeout(function() { HtmlRotator(ctlid, arrow, playSpeed, pauseTime, stopOnMouseOver); }, playSpeed);
}
", true);
            }

            base.CreateChildControls();

        }

		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="writer"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter writer)
		{
			//加入自己的版权信息！	
//            writer.Write(@"
//<!------------------------------ HtmlRotator Start -------------------------------
//Copyright:2005-2007 Thinksea (thinksea@163.com , www.thinksea.com)
//----------->
//");
			switch( this.HtmlRotatorType )
			{
				case HtmlRotatorType.LeftToRight:
                    this.Render_LeftToRight(writer);
					break;
				case HtmlRotatorType.RightToLeft:
                    this.Render_RightToLeft(writer);
                    break;
				case HtmlRotatorType.TopToBottom:
                    this.Render_TopToBottom(writer);
					break;
				case HtmlRotatorType.BottomToTop:
					this.Render_BottomToTop( writer );
                    break;
				case HtmlRotatorType.Multi:
					this.Render_Multi( writer );
                    break;
			}

//            writer.Write(@"
//<!-------------------------------HtmlRotator End--------------------------------->
//");
            base.RenderContents(writer);
        }

	}
}
