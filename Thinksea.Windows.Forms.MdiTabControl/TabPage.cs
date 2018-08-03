using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms.MdiTabControl
{
    /// <summary>
    /// Represents a single tab page in a MdiTabControl.TabControl.
    /// </summary>
    [System.ComponentModel.DesignTimeVisible(false), System.ComponentModel.Description("Represents a single tab page in a MdiTabControl.TabControl.")]
    public partial class TabPage : Control
    {

        private Color m_BackHighColor;
        private Color m_BackHighColorDisabled;
        private Color m_BackLowColor;
        private Color m_BackLowColorDisabled;
        private Color m_BorderColor;
        private Color m_BorderColorDisabled;
        private Color m_ForeColorDisabled;
        private bool m_Selected = false;
        private bool m_Hot = false;
        private int m_MaximumWidth;
        private int m_MinimumWidth;
        private int m_PadLeft;
        private int m_PadRight;
        private bool m_CloseButtonVisible;
        private Bitmap m_CloseButton;
        private Bitmap m_CloseButtonImageHot;
        private Bitmap m_CloseButtonImageDisabled;
        private Color m_CloseButtonBackHighColor;
        private Color m_CloseButtonBackLowColor;
        private Color m_CloseButtonBorderColor;
        private Color m_CloseButtonForeColor;
        private Color m_CloseButtonBackHighColorDisabled;
        private Color m_CloseButtonBackLowColorDisabled;
        private Color m_CloseButtonBorderColorDisabled;
        private Color m_CloseButtonForeColorDisabled;
        private Color m_CloseButtonBackHighColorHot;
        private Color m_CloseButtonBackLowColorHot;
        private Color m_CloseButtonBorderColorHot;
        private Color m_CloseButtonForeColorHot;
        private bool m_HotTrack;
        private Size m_CloseButtonSize;
        private bool m_FontBoldOnSelect;
        private Size m_IconSize;
        private System.Drawing.Drawing2D.SmoothingMode m_SmoothingMode;
        private TabControl.TabAlignment m_Alignment;
        private bool m_GlassGradient;
        private bool m_BorderEnhanced;
        private ToolStripRenderMode m_RenderMode;
        private TabControl.Weight m_BorderEnhanceWeight;

        internal Form m_Form;
        internal bool TabVisible;
        internal int TabLeft;
        internal ToolStripMenuItem MenuItem = new ToolStripMenuItem();

        private bool MouseOverCloseButton = false;

        /// <summary>
        /// Occurs when the user clicks the Tab Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.ComponentModel.Description("Occurs when the user clicks the Tab Control.")]
        public delegate void ClickEventHandler(object sender, System.EventArgs e);
        private ClickEventHandler ClickEvent;

        /// <summary>
        /// Occurs when the user clicks the Tab Control.
        /// </summary>
        public new event ClickEventHandler Click
        {
            add
            {
                ClickEvent = (ClickEventHandler)System.Delegate.Combine(ClickEvent, value);
            }
            remove
            {
                ClickEvent = (ClickEventHandler)System.Delegate.Remove(ClickEvent, value);
            }
        }

        internal delegate void CloseEventHandler(object sender, EventArgs e);
        private CloseEventHandler CloseEvent;

        internal event CloseEventHandler Close
        {
            add
            {
                CloseEvent = (CloseEventHandler)System.Delegate.Combine(CloseEvent, value);
            }
            remove
            {
                CloseEvent = (CloseEventHandler)System.Delegate.Remove(CloseEvent, value);
            }
        }

        internal delegate void GetTabRegionEventHandler(object sender, TabControl.GetTabRegionEventArgs e);
        private GetTabRegionEventHandler GetTabRegionEvent;

        internal event GetTabRegionEventHandler GetTabRegion
        {
            add
            {
                GetTabRegionEvent = (GetTabRegionEventHandler)System.Delegate.Combine(GetTabRegionEvent, value);
            }
            remove
            {
                GetTabRegionEvent = (GetTabRegionEventHandler)System.Delegate.Remove(GetTabRegionEvent, value);
            }
        }

        internal delegate void TabPaintBackgroundEventHandler(object sender, TabControl.TabPaintEventArgs e);
        private TabPaintBackgroundEventHandler TabPaintBackgroundEvent;

        internal event TabPaintBackgroundEventHandler TabPaintBackground
        {
            add
            {
                TabPaintBackgroundEvent = (TabPaintBackgroundEventHandler)System.Delegate.Combine(TabPaintBackgroundEvent, value);
            }
            remove
            {
                TabPaintBackgroundEvent = (TabPaintBackgroundEventHandler)System.Delegate.Remove(TabPaintBackgroundEvent, value);
            }
        }

        internal delegate void TabPaintBorderEventHandler(object sender, TabControl.TabPaintEventArgs e);
        private TabPaintBorderEventHandler TabPaintBorderEvent;

        internal event TabPaintBorderEventHandler TabPaintBorder
        {
            add
            {
                TabPaintBorderEvent = (TabPaintBorderEventHandler)System.Delegate.Combine(TabPaintBorderEvent, value);
            }
            remove
            {
                TabPaintBorderEvent = (TabPaintBorderEventHandler)System.Delegate.Remove(TabPaintBorderEvent, value);
            }
        }

        internal delegate void DraggingEventHandler(object sender, System.Windows.Forms.MouseEventArgs e);
        private DraggingEventHandler DraggingEvent;

        internal event DraggingEventHandler Dragging
        {
            add
            {
                DraggingEvent = (DraggingEventHandler)System.Delegate.Combine(DraggingEvent, value);
            }
            remove
            {
                DraggingEvent = (DraggingEventHandler)System.Delegate.Remove(DraggingEvent, value);
            }
        }

        internal delegate void EndDragEventHandler(object sender, System.Windows.Forms.MouseEventArgs e);
        private EndDragEventHandler EndDragEvent;

        internal event EndDragEventHandler EndDrag
        {
            add
            {
                EndDragEvent = (EndDragEventHandler)System.Delegate.Combine(EndDragEvent, value);
            }
            remove
            {
                EndDragEvent = (EndDragEventHandler)System.Delegate.Remove(EndDragEvent, value);
            }
        }

        private EventHandler EnterFormEvent;
        internal event EventHandler EnterForm
        {
            add
            {
                EnterFormEvent = (EventHandler)System.Delegate.Combine(EnterFormEvent, value);
            }
            remove
            {
                EnterFormEvent = (EventHandler)System.Delegate.Remove(EnterFormEvent, value);
            }
        }

        private EventHandler LeaveFormEvent;
        internal event EventHandler LeaveForm
        {
            add
            {
                LeaveFormEvent = (EventHandler)System.Delegate.Combine(LeaveFormEvent, value);
            }
            remove
            {
                LeaveFormEvent = (EventHandler)System.Delegate.Remove(LeaveFormEvent, value);
            }
        }


        public TabPage(System.Windows.Forms.Form Form)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            // Add any initialization after the InitializeComponent() call.
            this.SuspendLayout();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.BackColor = Color.Transparent;
            this.Visible = false;
            this.Size = new System.Drawing.Size(1, 1);
            Form.TopLevel = false;
            Form.MdiParent = null;
            Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Form.Dock = DockStyle.Fill;
            this.m_Form = Form;
            this.m_Form.Enter += new System.EventHandler(this.TabContents_Enter);
            this.m_Form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TabContent_FormClosed);
            this.m_Form.Leave += new System.EventHandler(this.m_Form_Leave);
            this.m_Form.TextChanged += new System.EventHandler(this.TabContent_TextChanged);
            MenuItem.Text = Form.Text;
            MenuItem.Image = Form.Icon.ToBitmap();
            MenuItem.Tag = this;
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Gets the form associated with the tab page
        /// </summary>
        [System.ComponentModel.Description("Gets the form associated with the tab page")]
        public Form Form
        {
            get
            {
                return m_Form;
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Color structure that represents the starting color of the Background linear gradient for the tab.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the System.Drawing.Color structure that represents the starting color of the Background linear gradient for the tab.")]
        public Color BackHighColor
        {
            get
            {
                return m_BackHighColor;
            }
            set
            {
                m_BackHighColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Color structure that represents the ending color of the Background linear gradient for the tab.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the System.Drawing.Color structure that represents the ending color of the Background linear gradient for the tab.")]
        public Color BackLowColor
        {
            get
            {
                return m_BackLowColor;
            }
            set
            {
                m_BackLowColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Gets or sets the System.Drawing.Color structure that represents the border color.")]
        internal Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Color structure that represents the starting color of the Background linear gradient for a non selected tab.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the System.Drawing.Color structure that represents the starting color of the Background linear gradient for a non selected tab.")]
        public Color BackHighColorDisabled
        {
            get
            {
                return m_BackHighColorDisabled;
            }
            set
            {
                m_BackHighColorDisabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Color structure that represents the ending color of the Background linear gradient for a non selected tab.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the System.Drawing.Color structure that represents the ending color of the Background linear gradient for a non selected tab.")]
        public Color BackLowColorDisabled
        {
            get
            {
                return m_BackLowColorDisabled;
            }
            set
            {
                m_BackLowColorDisabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Color structure that represents the border color of the tab when not selected.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the System.Drawing.Color structure that represents the border color of the tab when not selected.")]
        public Color BorderColorDisabled
        {
            get
            {
                return m_BorderColorDisabled;
            }
            set
            {
                m_BorderColorDisabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Color structure that represents the fore color of the tab when not selected.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the System.Drawing.Color structure that represents the fore color of the tab when not selected.")]
        public Color ForeColorDisabled
        {
            get
            {
                return m_ForeColorDisabled;
            }
            set
            {
                m_ForeColorDisabled = value;
                Invalidate();
            }
        }

        internal bool IsSelected
        {
            get
            {
                return m_Selected;
            }
            set
            {
                if (m_Selected != value)
                {
                    m_Selected = value;
                    if (m_Selected)
                    {
                        m_Hot = false;
                    }
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Returns whether the tab is selected or not.
        /// </summary>
        [System.ComponentModel.Description("Returns whether the tab is selected or not.")]
        public bool Selected
        {
            get
            {
                return IsSelected;
            }
        }

        internal int MaximumWidth
        {
            get
            {
                return m_MaximumWidth;
            }
            set
            {
                m_MaximumWidth = value;
                CalculateWidth();
                Invalidate();
            }
        }

        internal int MinimumWidth
        {
            get
            {
                return m_MinimumWidth;
            }
            set
            {
                m_MinimumWidth = value;
                CalculateWidth();
                Invalidate();
            }
        }

        internal int PadLeft
        {
            get
            {
                return m_PadLeft;
            }
            set
            {
                m_PadLeft = value;
                CalculateWidth();
                Invalidate();
            }
        }

        internal int PadRight
        {
            get
            {
                return m_PadRight;
            }
            set
            {
                m_PadRight = value;
                CalculateWidth();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets whether the tab close button is visble or not.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets whether the tab close button is visble or not.")]
        public bool CloseButtonVisible
        {
            get
            {
                return m_CloseButtonVisible;
            }
            set
            {
                if (m_CloseButtonVisible != value)
                {
                    m_CloseButtonVisible = value;
                    CalculateWidth();
                    Invalidate();
                }
            }
        }

        public Bitmap CloseButtonImage
        {
            get
            {
                return m_CloseButton;
            }
            set
            {
                m_CloseButton = value;
                Invalidate();
            }
        }

        public Bitmap CloseButtonImageHot
        {
            get
            {
                return m_CloseButtonImageHot;
            }
            set
            {
                m_CloseButtonImageHot = value;
                Invalidate();
            }
        }

        public Bitmap CloseButtonImageDisabled
        {
            get
            {
                return m_CloseButtonImageDisabled;
            }
            set
            {
                m_CloseButtonImageDisabled = value;
                Invalidate();
            }
        }

        public System.Drawing.Color CloseButtonBackHighColor
        {
            get
            {
                return m_CloseButtonBackHighColor;
            }
            set
            {
                m_CloseButtonBackHighColor = value;
            }
        }

        public System.Drawing.Color CloseButtonBackLowColor
        {
            get
            {
                return m_CloseButtonBackLowColor;
            }
            set
            {
                m_CloseButtonBackLowColor = value;
            }
        }

        public System.Drawing.Color CloseButtonBorderColor
        {
            get
            {
                return m_CloseButtonBorderColor;
            }
            set
            {
                m_CloseButtonBorderColor = value;
            }
        }

        public System.Drawing.Color CloseButtonForeColor
        {
            get
            {
                return m_CloseButtonForeColor;
            }
            set
            {
                m_CloseButtonForeColor = value;
            }
        }

        public System.Drawing.Color CloseButtonBackHighColorDisabled
        {
            get
            {
                return m_CloseButtonBackHighColorDisabled;
            }
            set
            {
                m_CloseButtonBackHighColorDisabled = value;
            }
        }

        public System.Drawing.Color CloseButtonBackLowColorDisabled
        {
            get
            {
                return m_CloseButtonBackLowColorDisabled;
            }
            set
            {
                m_CloseButtonBackLowColorDisabled = value;
            }
        }

        public System.Drawing.Color CloseButtonBorderColorDisabled
        {
            get
            {
                return m_CloseButtonBorderColorDisabled;
            }
            set
            {
                m_CloseButtonBorderColorDisabled = value;
            }
        }

        public System.Drawing.Color CloseButtonForeColorDisabled
        {
            get
            {
                return m_CloseButtonForeColorDisabled;
            }
            set
            {
                m_CloseButtonForeColorDisabled = value;
            }
        }

        public System.Drawing.Color CloseButtonBackHighColorHot
        {
            get
            {
                return m_CloseButtonBackHighColorHot;
            }
            set
            {
                m_CloseButtonBackHighColorHot = value;
            }
        }

        public System.Drawing.Color CloseButtonBackLowColorHot
        {
            get
            {
                return m_CloseButtonBackLowColorHot;
            }
            set
            {
                m_CloseButtonBackLowColorHot = value;
            }
        }

        public System.Drawing.Color CloseButtonBorderColorHot
        {
            get
            {
                return m_CloseButtonBorderColorHot;
            }
            set
            {
                m_CloseButtonBorderColorHot = value;
            }
        }

        public System.Drawing.Color CloseButtonForeColorHot
        {
            get
            {
                return m_CloseButtonForeColorHot;
            }
            set
            {
                m_CloseButtonForeColorHot = value;
            }
        }

        internal bool HotTrack
        {
            get
            {
                return m_HotTrack;
            }
            set
            {
                m_HotTrack = value;
                Invalidate();
            }
        }

        internal Size CloseButtonSize
        {
            get
            {
                return m_CloseButtonSize;
            }
            set
            {
                m_CloseButtonSize = value;
                CalculateWidth();
                Invalidate();
            }
        }

        internal bool FontBoldOnSelect
        {
            get
            {
                return m_FontBoldOnSelect;
            }
            set
            {
                m_FontBoldOnSelect = value;
                CalculateWidth();
                Invalidate();
            }
        }

        internal Size IconSize
        {
            get
            {
                return m_IconSize;
            }
            set
            {
                m_IconSize = value;
                CalculateWidth();
                Invalidate();
            }
        }

        internal System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            get
            {
                return m_SmoothingMode;
            }
            set
            {
                m_SmoothingMode = value;
                Invalidate();
            }
        }

        internal TabControl.TabAlignment Alignment
        {
            get
            {
                return m_Alignment;
            }
            set
            {
                m_Alignment = value;
                Invalidate();
            }
        }

        internal bool GlassGradient
        {
            get
            {
                return m_GlassGradient;
            }
            set
            {
                m_GlassGradient = value;
            }
        }

        internal bool BorderEnhanced
        {
            get
            {
                return m_BorderEnhanced;
            }
            set
            {
                m_BorderEnhanced = value;
            }
        }

        internal ToolStripRenderMode RenderMode
        {
            get
            {
                return m_RenderMode;
            }
            set
            {
                m_RenderMode = value;
                Invalidate();
            }
        }

        internal TabControl.Weight BorderEnhanceWeight
        {
            get
            {
                return m_BorderEnhanceWeight;
            }
            set
            {
                m_BorderEnhanceWeight = value;
            }
        }

        public Icon Icon
        {
            get
            {
                return m_Form.Icon;
            }
            set
            {
                m_Form.Icon = value;
                Region r = new Region(new Rectangle(PadLeft, (int)(this.Height / 2 - m_IconSize.Height / 2), m_IconSize.Width, m_IconSize.Height));
                this.Invalidate(r);
                r.Dispose();
                r = null;
                MenuItem.Image = value.ToBitmap();
            }
        }

        /// <summary>
        /// Selects the TabPage.
        /// </summary>
        [System.ComponentModel.Description("Selects the TabPage.")]
        public new void Select()
        {
            if (!IsSelected)
            {
                if (ClickEvent != null)
                    ClickEvent(this, new EventArgs());
            }
        }

        private System.Drawing.Drawing2D.LinearGradientBrush CreateGradientBrush(Rectangle Rectangle, Color Color1, Color Color2)
        {
            if (m_GlassGradient)
            {
                return Helper.CreateGlassGradientBrush(Rectangle, Color1, Color2);
            }
            else
            {
                return new System.Drawing.Drawing2D.LinearGradientBrush(Rectangle, Color1, Color2, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            }
        }

        public void TabContents_Enter(object sender, System.EventArgs e)
        {
            if (EnterFormEvent != null)
                EnterFormEvent(this, e);
        }

        public void TabContent_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            // if the form is closed closes the tabpage
            if (CloseEvent != null)
                CloseEvent(this, new EventArgs());
        }

        public void m_Form_Leave(object sender, System.EventArgs e)
        {
            if (LeaveFormEvent != null)
                LeaveFormEvent(this, e);
        }

        public void TabContent_TextChanged(object sender, System.EventArgs e)
        {
            CalculateWidth();
            Invalidate();
            MenuItem.Text = m_Form.Text;
        }

        public void Tab_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (m_Selected && !(MouseOverCloseButton && m_CloseButtonVisible))
            {
                return;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Close button was clicked
                if (MouseOverCloseButton && m_CloseButtonVisible)
                {
                    // try to close the form
                    m_Form.Close();
                }
                else // tab was clicked
                {
                    // select the tab
                    Select();
                }
            }
        }

        public void Tab_MouseEnter(object sender, System.EventArgs e)
        {
            if (m_Selected)
            {
                return;
            }
            if (m_HotTrack)
            {
                m_Hot = true;
            }
            Invalidate();
        }

        public void Tab_MouseLeave(object sender, System.EventArgs e)
        {
            MouseOverCloseButton = false;
            m_Hot = false;
            Invalidate();
        }

        // VBConversions Note: Former VB local static variables moved to class level.
        private bool Tab_MouseMove_State = false;

        private void Tab_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // static bool State = false; VBConversions Note: Static variable moved to class level and renamed Tab_MouseMove_State. Local static variables are not supported in C#.
            if (m_CloseButtonVisible)
            {
                // verify if the mouse is over the close button
                int x = this.Width - PadRight - m_CloseButtonSize.Width - 2;
                int y = (int)(this.Height / 2 - m_CloseButtonSize.Height / 2);
                MouseOverCloseButton = e.X >= x & e.X <= x + m_CloseButtonSize.Width - 1 & e.Y >= y & e.Y <= y + m_CloseButtonSize.Height - 1;
                if (Tab_MouseMove_State != MouseOverCloseButton && m_CloseButtonVisible)
                {
                    Tab_MouseMove_State = MouseOverCloseButton;
                    Region r = new Region(new Rectangle(x, y, m_CloseButtonSize.Width, m_CloseButtonSize.Height));
                    this.Invalidate(r);
                    r.Dispose();
                    r = null;
                }
            }
            if (this.RectangleToScreen(this.ClientRectangle).Contains(this.PointToScreen(new Point(e.X, e.Y))))
            {
                Cursor = Cursors.Default;
                if (EndDragEvent != null)
                    EndDragEvent(this, e);
            }
            else // the mouse is outside the tab (it happens only when the mouse was pressed on the tab and moved away while pressed)
            {
                if (DraggingEvent != null)
                    DraggingEvent(this, e);
                Cursor = Cursors.No;
            }
        }

        // Draws the tab text (the form text)
        private void DrawText(Graphics g)
        {
            Font f = new Font(Font, m_Selected && m_FontBoldOnSelect ? FontStyle.Bold : FontStyle.Regular);
            Brush b = new SolidBrush(m_Selected || m_Hot ? ForeColor : m_ForeColorDisabled);
            RectangleF bounds = new RectangleF(PadLeft + (m_Form.Icon == null ? 0 : m_IconSize.Width) + 2, 1, Width - PadLeft - (m_Form.Icon == null ? 0 : m_IconSize.Height) - 5 - (m_CloseButtonVisible ? m_CloseButtonSize.Width : 0) - PadRight, this.DisplayRectangle.Height);
            StringFormat MyFormat = new StringFormat();
            MyFormat.FormatFlags = StringFormatFlags.NoWrap;
            MyFormat.LineAlignment = StringAlignment.Center;
            MyFormat.Trimming = StringTrimming.EllipsisCharacter;
            g.DrawString(m_Form.Text, f, b, bounds, MyFormat);
            MyFormat.Dispose();
            b.Dispose();
            f.Dispose();
            MyFormat = null;
            b = null;
            f = null;
        }

        // Draws the tab icon if exists (the form icon)
        private void DrawIcon(Graphics g)
        {
            try
            {
                if (m_Form.Icon == null)
                {
                    return;
                }
                Rectangle r = new Rectangle(PadLeft, this.Height / 2 - m_IconSize.Height / 2, m_IconSize.Width, m_IconSize.Height);
                Icon i = new Icon(m_Form.Icon, m_IconSize);
                g.DrawIcon(i, r);
                DestroyIcon(i.Handle);
                i.Dispose();
                i = null;
            }
            catch (Exception)
            {
            }
        }

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        private static extern bool DestroyIcon(IntPtr handle);

        // Draws the Close Button
        private void DrawCloseButton(Graphics g)
        {
            try
            {
                Bitmap I;
                int x = this.Width - (m_CloseButtonSize.Width + PadRight + 2);
                int y = (int)(this.Height / 2 - m_CloseButtonSize.Height / 2);
                if (MouseOverCloseButton)
                {
                    I = m_CloseButtonImageHot;
                }
                else if (m_Selected)
                {
                    I = m_CloseButton;
                }
                else
                {
                    I = m_CloseButtonImageDisabled;
                }
                bool IsDisposable = false;
                if (I == null)
                {
                    I = GetButton();
                    IsDisposable = true;
                }
                Icon icon = System.Drawing.Icon.FromHandle(I.GetHicon());
                Rectangle r = new Rectangle(x, y, m_CloseButtonSize.Width, m_CloseButtonSize.Height);
                g.DrawIcon(icon, r);
                if (IsDisposable)
                {
                    I.Dispose();
                    I = null;
                }
                DestroyIcon(icon.Handle);
                icon.Dispose();
                icon = null;
            }
            catch (Exception)
            {
            }
        }

        // Generates the close button image
        private Bitmap GetButton()
        {
            System.Drawing.Point[] Points = new System.Drawing.Point[] { new Point(1, 0), new Point(3, 0), new Point(5, 2), new Point(7, 0), new Point(9, 0), new Point(6, 3), new Point(6, 4), new Point(9, 7), new Point(7, 7), new Point(5, 5), new Point(3, 7), new Point(1, 7), new Point(4, 4), new Point(4, 3) };
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            Color bch = new Color();
            Color bcl = new Color();
            Color bc = new Color();
            Color fc = new Color();
            Bitmap B = default(Bitmap);
            System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.Point[] path = new System.Drawing.Point[] { new Point(0, 1), new Point(1, 0), new Point(15, 0), new Point(16, 1), new Point(16, 14), new Point(15, 15), new Point(1, 15), new Point(0, 14) };
            Graphics g = default(Graphics);
            if (MouseOverCloseButton)
            {
                bch = Helper.RenderColors.TabCloseButtonBackHighColorHot(m_RenderMode, CloseButtonBackHighColorHot);
                bcl = Helper.RenderColors.TabCloseButtonBackLowColorHot(m_RenderMode, CloseButtonBackLowColorHot);
                bc = Helper.RenderColors.TabCloseButtonBorderColorHot(m_RenderMode, CloseButtonBorderColorHot);
                fc = Helper.RenderColors.TabCloseButtonForeColorHot(m_RenderMode, CloseButtonForeColorHot);
            }
            else if (m_Selected)
            {
                bch = Helper.RenderColors.TabCloseButtonBackHighColor(m_RenderMode, CloseButtonBackHighColor);
                bcl = Helper.RenderColors.TabCloseButtonBackLowColor(m_RenderMode, CloseButtonBackLowColor);
                bc = Helper.RenderColors.TabCloseButtonBorderColor(m_RenderMode, CloseButtonBorderColor);
                fc = Helper.RenderColors.TabCloseButtonForeColor(m_RenderMode, CloseButtonForeColor);
            }
            else
            {
                bch = Helper.RenderColors.TabCloseButtonBackHighColorDisabled(m_RenderMode, CloseButtonBackHighColorDisabled);
                bcl = Helper.RenderColors.TabCloseButtonBackLowColorDisabled(m_RenderMode, CloseButtonBackLowColorDisabled);
                bc = Helper.RenderColors.TabCloseButtonBorderColorDisabled(m_RenderMode, CloseButtonBorderColorDisabled);
                fc = Helper.RenderColors.TabCloseButtonForeColorDisabled(m_RenderMode, CloseButtonForeColorDisabled);
            }
            B = new Bitmap(17, 17);
            B.MakeTransparent();
            g = Graphics.FromImage(B);
            // draw the border and background
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            System.Drawing.Drawing2D.LinearGradientBrush l = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, 15), bch, bcl);
            g.FillPolygon(l, path);
            Pen p = new Pen(bc);
            g.DrawPolygon(p, path);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            // draw the foreground
            gp.AddPolygon(Points);
            m.Translate(3, 4);
            gp.Transform(m);
            p.Dispose();
            p = new Pen(fc);
            g.DrawPolygon(p, gp.PathPoints);
            SolidBrush sb = new SolidBrush(fc);
            g.FillPolygon(sb, gp.PathPoints);
            sb.Dispose();
            p.Dispose();
            gp.Dispose();
            g.Dispose();
            m.Dispose();
            return B;
        }

        // Calculates the tab width
        private void CalculateWidth()
        {
            Graphics g = this.CreateGraphics();
            int iw = 0;
            int cbw = 0;
            int w = Width;
            if (m_Form.Icon != null)
            {
                iw = m_IconSize.Width;
            }
            if (m_CloseButtonVisible)
            {
                cbw = m_CloseButtonSize.Width;
            }
            Font f = new Font(Font, m_FontBoldOnSelect ? FontStyle.Bold : FontStyle.Regular);
            w = (int)(PadLeft + iw + 3 + g.MeasureString(m_Form.Text, f).Width + 3 + cbw + m_PadRight + 2);
            f.Dispose();
            if (w < m_MinimumWidth + 1)
            {
                w = m_MinimumWidth + 1;
            }
            else if (w > m_MaximumWidth + 1)
            {
                w = m_MaximumWidth + 1;
            }
            if (w != Width)
            {
                Width = w;
            }
            g.Dispose();
        }

        // Get the tab region shape
        private Point[] GetRegion(int W, int H, int H1)
        {
            Point[] R = new Point[] { new Point(0, H), new Point(0, 2), new Point(2, 0), new Point(W - 3, 0), new Point(W - 1, 2), new Point(W - 1, H) };
            TabControl.GetTabRegionEventArgs e = new TabControl.GetTabRegionEventArgs(R, W, H, this.IsSelected);
            if (GetTabRegionEvent != null)
                GetTabRegionEvent(this, e);
            Point[] temp_array = e.Points;
            Array.Resize(ref temp_array, System.Convert.ToInt32(e.Points.Length + 2));
            e.Points = temp_array;
            Array.Copy(e.Points, 0, e.Points, 1, System.Convert.ToInt32(e.Points.Length - 1));
            e.Points[0] = new Point(System.Convert.ToInt32(e.Points[1].X), H1);
            e.Points[e.Points.Length - 1] = new Point(e.Points[e.Points.Length - 2].X, H1);
            return e.Points;
        }

        private void MirrorPath(System.Drawing.Drawing2D.GraphicsPath GraphicPath)
        {
            System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
            m.Translate(0, Height - 1);
            m.Scale(1, -1);
            GraphicPath.Transform(m);
            m.Dispose();
        }

        /// <summary>
        /// Paint the tab
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            bool Painting = false;
            if (Painting)
            {
                return;
            }
            Painting = true;
            this.SuspendLayout();
            Color RenderBorderColor = new Color();
            Color RenderBottomColor = new Color();
            Color RenderHighColor = new Color();
            Color RenderLowColor = new Color();
            System.Drawing.Drawing2D.GraphicsPath GraphicPath = new System.Drawing.Drawing2D.GraphicsPath();

            int w = this.Width;
            CalculateWidth();
            if (w != this.Width)
            {
                GraphicPath.Dispose();
                return;
            }

            if (m_Selected)
            {
                RenderBorderColor = Helper.RenderColors.BorderColor(m_RenderMode, BorderColor);
                RenderHighColor = Helper.RenderColors.TabBackHighColor(m_RenderMode, BackHighColor);
                RenderLowColor = Helper.RenderColors.TabBackLowColor(m_RenderMode, BackLowColor);
                RenderBottomColor = Helper.RenderColors.TabBackLowColor(m_RenderMode, BackLowColor);
            }
            else if (m_Hot)
            {
                RenderBorderColor = Helper.RenderColors.BorderColor(m_RenderMode, BorderColor);
                RenderHighColor = Helper.RenderColors.TabBackHighColor(m_RenderMode, BackHighColor);
                RenderLowColor = Helper.RenderColors.TabBackLowColor(m_RenderMode, BackLowColor);
                RenderBottomColor = Helper.RenderColors.BorderColor(m_RenderMode, BorderColor);
            }
            else
            {
                RenderBorderColor = Helper.RenderColors.BorderColorDisabled(m_RenderMode, BorderColorDisabled);
                RenderHighColor = Helper.RenderColors.TabBackHighColorDisabled(m_RenderMode, BackHighColorDisabled);
                RenderLowColor = Helper.RenderColors.TabBackLowColorDisabled(m_RenderMode, BackLowColorDisabled);
                RenderBottomColor = Helper.RenderColors.BorderColor(m_RenderMode, BorderColor);
            }

            e.Graphics.SmoothingMode = m_SmoothingMode;

            GraphicPath.AddPolygon(GetRegion(Width - 1, Height - 1, System.Convert.ToInt32(this.IsSelected ? Height : Height - 1)));

            // if is bottom mirror the button vertically
            if (m_Alignment == TabControl.TabAlignment.Bottom)
            {
                MirrorPath(GraphicPath);
                Color x = RenderHighColor;
                RenderHighColor = RenderLowColor;
                RenderLowColor = x;
            }

            // Get the correct region including all the borders
            Region R = new Region(GraphicPath);
            Region R1 = new Region(GraphicPath);
            Region R2 = new Region(GraphicPath);
            Region R3 = new Region(GraphicPath);
            System.Drawing.Drawing2D.Matrix M1 = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.Drawing2D.Matrix M2 = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.Drawing2D.Matrix M3 = new System.Drawing.Drawing2D.Matrix();
            M1.Translate(0, -0.5F);
            M2.Translate(0, 0.5F);
            M3.Translate(1, 0);
            R1.Transform(M1);
            R2.Transform(M2);
            R3.Transform(M3);
            R.Union(R1);
            R.Union(R2);
            R.Union(R3);
            this.Region = R;

            RectangleF RF = R.GetBounds(e.Graphics);
            Rectangle rec = new Rectangle(0, 0, (int)RF.Width, (int)RF.Height);
            TabControl.TabPaintEventArgs te = default(TabControl.TabPaintEventArgs);

            te = new TabControl.TabPaintEventArgs(e.Graphics, rec, m_Selected, m_Hot, GraphicPath, Width, Height);
            if (TabPaintBackgroundEvent != null) // try to owner draw
                TabPaintBackgroundEvent(this, te);
            System.Drawing.Drawing2D.LinearGradientBrush gb = CreateGradientBrush(new Rectangle(0, 0, this.Width, this.Height), RenderHighColor, RenderLowColor);
            if (!te.Handled)
            {
                e.Graphics.FillPath(gb, GraphicPath);
            }
            gb.Dispose();
            te.Dispose();

            te = new TabControl.TabPaintEventArgs(e.Graphics, rec, m_Selected, m_Hot, GraphicPath, Width, Height);
            if (TabPaintBorderEvent != null) // try to owner draw
                TabPaintBorderEvent(this, te);
            if (!te.Handled)
            {
                if (m_BorderEnhanced)
                {
                    Color c = m_Alignment == TabControl.TabAlignment.Bottom ? RenderLowColor : RenderHighColor;
                    Pen p = new Pen(c, (float)m_BorderEnhanceWeight);
                    e.Graphics.DrawLines(p, GraphicPath.PathPoints);
                    p.Dispose();
                }
                Pen p1 = new Pen(RenderBorderColor);
                e.Graphics.DrawLines(p1, GraphicPath.PathPoints);
                p1.Dispose();
            }
            te.Dispose();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.DrawLine(new Pen(RenderBottomColor), GraphicPath.PathPoints[0], GraphicPath.PathPoints[GraphicPath.PointCount - 1]);
            e.Graphics.SmoothingMode = m_SmoothingMode;

            DrawIcon(e.Graphics);
            DrawText(e.Graphics);
            if (m_CloseButtonVisible)
            {
                DrawCloseButton(e.Graphics);
            }
            this.ResumeLayout();

            // do the memory cleanup
            GraphicPath.Dispose();
            M1.Dispose();
            M2.Dispose();
            M3.Dispose();
            R1.Dispose();
            R2.Dispose();
            R3.Dispose();
            R.Dispose();
            te.Dispose();
            Painting = false;
        }

        #region  Obsolete properties

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override Size MinimumSize
        {
            get
            {
                return default(Size);
            }
            set
            {
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override Size MaximumSize
        {
            get
            {
                return default(Size);
            }
            set
            {
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public new Padding Padding
        {
            get
            {
                return default(Padding);
            }
            set
            {
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override Color BackColor
        {
            get
            {
                return default(Color);
            }
            set
            {
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override System.Windows.Forms.DockStyle Dock
        {
            get
            {
                return default(System.Windows.Forms.DockStyle);
            }
            set
            {
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override System.Windows.Forms.AnchorStyles Anchor
        {
            get
            {
                return default(System.Windows.Forms.AnchorStyles);
            }
            set
            {
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override string Text
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        #endregion

    }

}
