using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 多图片浏览控件。
    /// </summary>
    public partial class PicsBrowser : UserControl
    {
        /// <summary>
        /// 获取集合中的全部图片信息。
        /// </summary>
        [Browsable(false)]
        public PicsBrowserItem[] Pics
        {
            get
            {
                System.Collections.Generic.List<PicsBrowserItem> l = new List<PicsBrowserItem>();
                foreach (PicsBrowserItem tmp in this.flowLayoutPanel1.Controls)
                {
                    l.Add(tmp);
                }
                return l.ToArray();
            }
        }

        /// <summary>
        /// 获取位于集合中的图片项目数量。
        /// </summary>
        [Browsable(false)]
        public int PicsCount
        {
            get
            {
                return this.flowLayoutPanel1.Controls.Count;
            }
        }

        private Size _PicSize;

        /// <summary>
        /// 图片尺寸。
        /// </summary>
        [DefaultValue(typeof(Size), "100,100")]
        public Size PicSize
        {
            get
            {
                return this._PicSize;
            }
            set
            {
                this._PicSize = value;
                foreach (Control tmp in this.flowLayoutPanel1.Controls)
                {
                    tmp.Size = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置自动滚动定位的位置。
        /// </summary>
        public new Point AutoScrollPosition
        {
            get
            {
                return this.flowLayoutPanel1.AutoScrollPosition;
            }
            set
            {
                this.flowLayoutPanel1.AutoScrollPosition = value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PicsBrowser()
        {
            InitializeComponent();

            this.PicSize = new Size(100, 100);

        }

        /// <summary>
        /// 向集合中添加一个图片。
        /// </summary>
        /// <param name="fileName">图片文件名。</param>
        public void AddPic(string fileName)
        {
            PicsBrowserItem pbi = new PicsBrowserItem(System.Drawing.Image.FromFile(fileName), System.IO.Path.GetFileName(fileName), System.IO.Path.GetFileNameWithoutExtension(fileName));
            pbi.Size = this.PicSize;
            this.flowLayoutPanel1.Controls.Add(pbi);

        }

        ///// <summary>
        ///// 向集合中添加一个图片。
        ///// </summary>
        ///// <param name="fileName">图片文件名。</param>
        ///// <param name="Declaration">说明。</param>
        //public void AddPic(string fileName, string Declaration)
        //{
        //    PicsBrowserItem pbi = new PicsBrowserItem(fileName, System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fileName), Declaration);
        //    pbi.Size = this.PicSize;
        //    this.flowLayoutPanel1.Controls.Add(pbi);
        //}

        /// <summary>
        /// 添加一个图片。
        /// </summary>
        /// <param name="img">图片信息。</param>
        /// <param name="Name">图片名称。</param>
        /// <param name="Declaration">说明。</param>
        public void AddPic(Image img, string Name, string Declaration)
        {
            PicsBrowserItem pbi = new PicsBrowserItem(img, Name, Declaration);
            pbi.Size = this.PicSize;
            this.flowLayoutPanel1.Controls.Add(pbi);

        }

        /// <summary>
        /// 添加一个图片项目。
        /// </summary>
        /// <param name="pbi">图片项目。</param>
        public void AddPic(PicsBrowserItem pbi)
        {
            pbi.Size = this.PicSize;
            this.flowLayoutPanel1.Controls.Add(pbi);

        }

        /// <summary>
        /// 从集合中移除指定的图片信息。
        /// </summary>
        /// <param name="pbi">待移除的图片项目。</param>
        public void Remove(PicsBrowserItem pbi)
        {
            this.flowLayoutPanel1.Controls.Remove(pbi);

        }

        ///// <summary>
        ///// 从集合中移除指定的图片信息。
        ///// </summary>
        ///// <param name="img">待移除的图片。</param>
        //public void Remove(Image img)
        //{
        //    for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
        //    {
        //        PicsBrowserItem pbi = this.flowLayoutPanel1.Controls[i] as PicsBrowserItem;
        //        if (pbi != null && pbi.Image == img)
        //        {
        //            this.flowLayoutPanel1.Controls.Remove(pbi);
        //            return;
        //        }
        //    }

        //}

        /// <summary>
        /// 从集合中移除指定的图片信息。
        /// </summary>
        /// <param name="name">待移除的图片名称。</param>
        public void Remove(string name)
        {
            for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
            {
                PicsBrowserItem pbi = this.flowLayoutPanel1.Controls[i] as PicsBrowserItem;
                if (pbi != null && pbi.Name == name)
                {
                    this.flowLayoutPanel1.Controls.Remove(pbi);
                    return;
                }
            }

        }

        /// <summary>
        /// 从集合中清除所有的图片项目。
        /// </summary>
        public void Clear()
        {
            this.flowLayoutPanel1.Controls.Clear();

        }

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.flowLayoutPanel1.Focus();

        }

    }
}
