using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 描述图片选择框的一个图片项目。
    /// </summary>
    [ToolboxItem(false)]
    public partial class PicsBrowserItem : UserControl
    {
		/// <summary>
		/// 获取或设置图片。
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Image Image
        {
            get
            {
                return this.pictureBox1.Image;
            }
            set
            {
                this.pictureBox1.Image = value;
            }
        }

		/// <summary>
		/// 获取或设置图片说明文字。
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Declaration
        {
            get
            {
                return this.lDeclaration.Text;
            }
            set
            {
                this.lDeclaration.Text = value;
                this.toolTip1.SetToolTip(this.lDeclaration, value);
            }
        }

		/// <summary>
		/// 图片名称。
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

		/// <summary>
		/// 获取或设置删除按钮当前是否可见。
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool ShowDeleteButton
        {
            get
            {
                return this.btnDelete.Visible;
            }
            set
            {
                this.btnDelete.Visible = value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PicsBrowserItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="image">图像数据。</param>
        /// <param name="name">图片名称。</param>
        /// <param name="declaration">说明。</param>
        public PicsBrowserItem(Image image, string name, string declaration)
        {
            InitializeComponent();

            this.Image = image;
            this.Name = name;
            this.Declaration = declaration;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            PicsBrowser pb = this.Parent.Parent as PicsBrowser;
            if (pb == null)
            {
                return;
            }
            Point p = pb.AutoScrollPosition;
            pb.Remove(this);
            pb.AutoScrollPosition = new Point(Math.Abs(p.X), Math.Abs(p.Y));

        }

        private void btnDelete_VisibleChanged(object sender, EventArgs e)
        {
            if (this.btnDelete.Visible)
            {
                this.lDeclaration.Width = this.Width - 31;
            }
            else
            {
                this.lDeclaration.Width = this.Width - 4;
            }

        }
    }

}
