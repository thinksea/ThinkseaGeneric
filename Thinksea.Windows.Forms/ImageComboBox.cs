using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 一个带图标的下拉列表控件。
    /// </summary>
    public class ImageComboBox : ComboBox
    {
        private ImageList _ImageList = null;
        /// <summary>
        /// 获取或设置与此控件关联的图标列表组件，为下拉列表项提供图标。
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public ImageList ImageList
        {
            get
            {
                return this._ImageList;
            }
            set
            {
                this._ImageList = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是由您的代码还是由操作系统来处理列表中的元素的绘制。
        /// </summary>
        /// <value>System.Windows.Forms.DrawMode 枚举值之一。默认值为 System.Windows.Forms.DrawMode.Normal。</value>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">该值不是有效的 System.Windows.Forms.DrawMode 枚举值。</exception>
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.DefaultValue(typeof(System.Windows.Forms.DrawMode), "OwnerDrawFixed")]
        public new DrawMode DrawMode
        {
            get
            {
                return base.DrawMode;
            }
            set
            {
                base.DrawMode = value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public ImageComboBox()
        {
            // set draw mode to owner draw
            this.DrawMode = DrawMode.OwnerDrawFixed;

        }

        /// <summary>
        /// 绘制下拉列表项。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            // draw background & focus rect
            e.DrawBackground();

            //// check if it is an item from the Items collection
            //if (e.Index < 0)
            //{
            //    // not an item, draw the text (indented)
            //    e.Graphics.DrawString(this.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top);
            //}
            //else

            string text;
            Font font;
            SolidBrush solidBrush;
            System.Drawing.Rectangle textBound;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near; //水平左对齐.
            stringFormat.LineAlignment = StringAlignment.Center; //垂直居中对齐。

            if (this._ImageList != null)
            {
                textBound = Rectangle.FromLTRB(e.Bounds.Left + _ImageList.ImageSize.Width, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);

                // check if item is an ImageComboItem
                if (this.Items[e.Index] is ImageComboBoxItem)
                {
                    // get item to draw
                    ImageComboBoxItem item = (ImageComboBoxItem)this.Items[e.Index];

                    int imageIndex = item.ImageIndex;
                    if (item.ImageIndex == -1 && item.ImageKey != null) // -1: no image
                    {
                        imageIndex = this.ImageList.Images.IndexOfKey(item.ImageKey);
                    }
                    if (imageIndex != -1)
                    {
                        int imgTop, imgHeight;
                        if (this.ImageList.ImageSize.Height < this.ItemHeight)
                        {
                            imgHeight = this.ImageList.ImageSize.Height;
                            imgTop = e.Bounds.Top + (this.ItemHeight - imgHeight) / 2 - 1;
                        }
                        else
                        {
                            imgHeight = this.ItemHeight;
                            imgTop = e.Bounds.Top;
                        }
                        this.ImageList.Draw(e.Graphics, e.Bounds.Left, imgTop, this.ImageList.ImageSize.Width, imgHeight, imageIndex);
                    }

                    Color foreColor = (item.ForeColor != Color.FromKnownColor(KnownColor.Transparent)) ? item.ForeColor : e.ForeColor;

                    text = item.Text;
                    font = item.Bold ? new Font(e.Font, FontStyle.Bold) : e.Font;
                    solidBrush = new SolidBrush(foreColor);

                    //System.Drawing.Size textSize = System.Windows.Forms.TextRenderer.MeasureText(e.Graphics, item.Text, font);
                    // draw text (indented)
                    //e.Graphics.DrawString(item.Text, font, new SolidBrush(foreColor), e.Bounds.Left + _ImageList.ImageSize.Width, e.Bounds.Top);
                }
                else
                {
                    text = System.Convert.ToString(this.Items[e.Index]);
                    font = e.Font;
                    solidBrush = new SolidBrush(e.ForeColor);

                    // it is not an ImageComboItem, draw it
                    //e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + _ImageList.ImageSize.Width, e.Bounds.Top);
                }
                //e.Graphics.DrawString(text, font, solidBrush, textBound, stringFormat);
            }
            else
            {
                text = System.Convert.ToString(this.Items[e.Index]);
                font = e.Font;
                solidBrush = new SolidBrush(e.ForeColor);
                textBound = e.Bounds;
                //e.Graphics.DrawString(System.Convert.ToString(this.Items[e.Index]), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top);
            }
            e.Graphics.DrawString(text, font, solidBrush, textBound, stringFormat);

            e.DrawFocusRectangle();

        }

    }

    /// <summary>
    /// 带图标的下拉列表控件选项。
    /// </summary>
    public class ImageComboBoxItem
    {
        // forecolor: transparent = inherit
        private Color _ForeColor = Color.FromKnownColor(KnownColor.Transparent);
        /// <summary>
        /// 获取或设置下拉列表项的文本颜色。
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return this._ForeColor;
            }
            set
            {
                this._ForeColor = value;
            }

        }

        private bool _Bold = false;
        /// <summary>
        /// 获取或设置一个值，指示是否以加粗的字体绘制下拉列表项的文本。
        /// </summary>
        public bool Bold
        {
            get
            {
                return this._Bold;
            }
            set
            {
                this._Bold = value;
            }

        }

        private int _ImageIndex = -1;
        /// <summary>
        /// 获取或设置下拉列表项的图标索引。
        /// </summary>
        public int ImageIndex
        {
            get
            {
                return this._ImageIndex;
            }
            set
            {
                this._ImageKey = null;
                this._ImageIndex = value;
            }

        }

        private string _ImageKey = null;
        /// <summary>
        /// 获取或设置下拉列表项的图标键。
        /// </summary>
        public string ImageKey
        {
            get
            {
                return this._ImageKey;
            }
            set
            {
                this._ImageIndex = -1;
                this._ImageKey = value;
            }

        }

        private object _Value = null;
        /// <summary>
        /// 获取或设置下拉列表项的值。
        /// </summary>
        /// <value>一个 <see cref="System.Object"/>。</value>
        public object Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }

        }

        private string _Text = null;
        /// <summary>
        /// 获取或设置下拉列表项的文本。
        /// </summary>
        public string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
            }

        }


        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public ImageComboBoxItem()
        {

        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="Text"></param>
        public ImageComboBoxItem(string Text)
        {
            this._Text = Text;

        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="ImageIndex"></param>
        public ImageComboBoxItem(string Text, int ImageIndex)
        {
            this._Text = Text;
            this._ImageIndex = ImageIndex;

        }
        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="ImageIndex"></param>
        /// <param name="Mark"></param>
        public ImageComboBoxItem(string Text, int ImageIndex, bool Mark)
        {
            this._Text = Text;
            this._ImageIndex = ImageIndex;
            this._Bold = Mark;

        }
        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="ImageIndex"></param>
        /// <param name="Mark"></param>
        /// <param name="ForeColor"></param>
        public ImageComboBoxItem(string Text, int ImageIndex, bool Mark, Color ForeColor)
        {
            this._Text = Text;
            this._ImageIndex = ImageIndex;
            this._Bold = Mark;
            this._ForeColor = ForeColor;

        }
        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="ImageIndex"></param>
        /// <param name="Mark"></param>
        /// <param name="ForeColor"></param>
        /// <param name="Value"></param>
        public ImageComboBoxItem(string Text, int ImageIndex, bool Mark, Color ForeColor, object Value)
        {
            this._Text = Text;
            this._ImageIndex = ImageIndex;
            this._Bold = Mark;
            this._ForeColor = ForeColor;
            this._Value = Value;

        }

        /// <summary>
        /// 返回表示此对象的文本属性值。
        /// </summary>
        /// <returns>一个 String 对象。</returns>
        public override string ToString()
        {
            return this._Text;

        }
    }

}
