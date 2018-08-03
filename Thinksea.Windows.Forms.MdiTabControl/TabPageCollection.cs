using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms.MdiTabControl
{
    /// <summary>
    /// Contains a collection of MdiTabControl.TabPage objects.
    /// </summary>
    [Description("Contains a collection of MdiTabControl.TabPage objects.")]
    public class TabPageCollection : System.Collections.CollectionBase
    {
        private TabControl TabControl;
        private bool IsReorder = false;

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

        [Description("Occurs when the Tab Background has been painted.")]
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

        [Description("Occurs when the Tab Border has been painted.")]
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

        private EventHandler SelectedChangedEvent;
        internal event EventHandler SelectedChanged
        {
            add
            {
                SelectedChangedEvent = (EventHandler)System.Delegate.Combine(SelectedChangedEvent, value);
            }
            remove
            {
                SelectedChangedEvent = (EventHandler)System.Delegate.Remove(SelectedChangedEvent, value);
            }
        }


        internal TabPageCollection(TabControl Owner)
        {
            TabControl = Owner;
        }

        internal TabPageCollection(UserControl c)
        {
        }

        /// <summary>
        /// Create a new TabPage and adds it to the collection whit the Form associated and returns the created TabPage.
        /// </summary>
        /// <param name="Form"></param>
        /// <returns></returns>
        [Description("Create a new TabPage and adds it to the collection whit the Form associated and returns the created TabPage.")]
        public TabPage Add(Form Form)
        {
            TabPage TabPage = new TabPage(Form);
            TabPage.SuspendLayout();
            TabControl.SuspendLayout();

            // Initialize all the tabpage defaults values
            TabControl.AddingPage = true;
            TabPage.BackHighColor = TabControl.TabBackHighColor;
            TabPage.BackHighColorDisabled = TabControl.TabBackHighColorDisabled;
            TabPage.BackLowColor = TabControl.TabBackLowColor;
            TabPage.BackLowColorDisabled = TabControl.TabBackLowColorDisabled;
            TabPage.BorderColor = TabControl.BorderColor;
            TabPage.BorderColorDisabled = TabControl.BorderColorDisabled;
            TabPage.ForeColor = TabControl.ForeColor;
            TabPage.ForeColorDisabled = TabControl.ForeColorDisabled;
            TabPage.MaximumWidth = TabControl.TabMaximumWidth;
            TabPage.MinimumWidth = TabControl.TabMinimumWidth;
            TabPage.PadLeft = TabControl.TabPadLeft;
            TabPage.PadRight = TabControl.TabPadRight;
            TabPage.CloseButtonVisible = TabControl.TabCloseButtonVisible;
            TabPage.CloseButtonImage = TabControl.TabCloseButtonImage;
            TabPage.CloseButtonImageHot = TabControl.TabCloseButtonImageHot;
            TabPage.CloseButtonImageDisabled = TabControl.TabCloseButtonImageDisabled;
            TabPage.CloseButtonSize = TabControl.TabCloseButtonSize;
            TabPage.CloseButtonBackHighColor = TabControl.TabCloseButtonBackHighColor;
            TabPage.CloseButtonBackLowColor = TabControl.TabCloseButtonBackLowColor;
            TabPage.CloseButtonBorderColor = TabControl.TabCloseButtonBorderColor;
            TabPage.CloseButtonForeColor = TabControl.TabCloseButtonForeColor;
            TabPage.CloseButtonBackHighColorDisabled = TabControl.TabCloseButtonBackHighColorDisabled;
            TabPage.CloseButtonBackLowColorDisabled = TabControl.TabCloseButtonBackLowColorDisabled;
            TabPage.CloseButtonBorderColorDisabled = TabControl.TabCloseButtonBorderColorDisabled;
            TabPage.CloseButtonForeColorDisabled = TabControl.TabCloseButtonForeColorDisabled;
            TabPage.CloseButtonBackHighColorHot = TabControl.TabCloseButtonBackHighColorHot;
            TabPage.CloseButtonBackLowColorHot = TabControl.TabCloseButtonBackLowColorHot;
            TabPage.CloseButtonBorderColorHot = TabControl.TabCloseButtonBorderColorHot;
            TabPage.CloseButtonForeColorHot = TabControl.TabCloseButtonForeColorHot;
            TabPage.HotTrack = TabControl.HotTrack;
            TabPage.Font = TabControl.Font;
            TabPage.FontBoldOnSelect = TabControl.FontBoldOnSelect;
            TabPage.IconSize = TabControl.TabIconSize;
            TabPage.SmoothingMode = TabControl.SmoothingMode;
            TabPage.Alignment = TabControl.Alignment;
            TabPage.GlassGradient = TabControl.TabGlassGradient;
            TabPage.BorderEnhanced = TabControl.m_TabBorderEnhanced;
            TabPage.RenderMode = TabControl.RenderMode;
            TabPage.BorderEnhanceWeight = TabControl.TabBorderEnhanceWeight;

            TabPage.Top = 0;
            TabPage.Left = TabControl.LeftOffset;
            TabPage.Height = TabControl.TabHeight;

            TabControl.TabToolTip.SetToolTip(TabPage, TabPage.m_Form.Text);

            // Create the event handles
            TabPage.Click += new MdiTabControl.TabPage.ClickEventHandler(TabPage_Clicked);
            TabPage.Close += new MdiTabControl.TabPage.CloseEventHandler(TabPage_Closed);
            TabPage.GetTabRegion += new MdiTabControl.TabPage.GetTabRegionEventHandler(TabPage_GetTabRegion);
            TabPage.TabPaintBackground += new MdiTabControl.TabPage.TabPaintBackgroundEventHandler(TabPage_TabPaintBackground);
            TabPage.TabPaintBorder += new MdiTabControl.TabPage.TabPaintBorderEventHandler(TabPage_TabPaintBorder);
            TabPage.SizeChanged += new System.EventHandler(TabPage_SizeChanged);
            TabPage.Dragging += new MdiTabControl.TabPage.DraggingEventHandler(TabPage_Dragging);
            TabPage.EndDrag += new MdiTabControl.TabPage.EndDragEventHandler(TabPage_EndDrag);
            TabPage.EnterForm += new EventHandler(TabPage_Enter);
            TabPage.LeaveForm += new EventHandler(TabPage_Leave);

            // Insert the tabpage in the collection
            List.Add(TabPage);
            TabControl.ResumeLayout();
            TabPage.ResumeLayout();
            return TabPage;
        }

        /// <summary>
        /// Removes a TabPage from the collection.
        /// </summary>
        /// <param name="TabPage">一个 <see cref="TabPage"/> 实例。</param>
        [Description("Removes a TabPage from the collection.")]
        public void Remove(TabPage TabPage)
        {
            try
            {
                TabControl.IsDelete = true;
                if (TabControl.pnlBottom.Controls.Count > 1)
                {
                    // brings the next top tab
                    // first dock the form in the body then display it
                    TabControl.pnlBottom.Controls[1].Dock = DockStyle.Fill;
                    TabControl.pnlBottom.Controls[1].Visible = true;
                }
                List.Remove(TabPage);
            }
            catch (Exception)
            {
            }
        }

        public Form TearOff(TabPage TabPage)
        {
            Form f = TabPage.m_Form;
            TabControl.pnlBottom.Controls.Remove(f);
            TabPage.m_Form = new Form();
            TabPage.m_Form.Enter += new System.EventHandler(TabPage.TabContents_Enter);
            TabPage.m_Form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(TabPage.TabContent_FormClosed);
            TabPage.m_Form.Leave += new System.EventHandler(TabPage.m_Form_Leave);
            TabPage.m_Form.TextChanged += new System.EventHandler(TabPage.TabContent_TextChanged);
            Remove(TabPage);
            f.TopLevel = true;
            f.Dock = DockStyle.None;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            return f;
        }

        /// <summary>
        /// Gets a TabPage in the position Index from the collection.
        /// </summary>
        /// <param name="Index">一个从 0 开始的索引值。</param>
        /// <returns></returns>
        [Description("Gets a TabPage in the position Index from the collection.")]
        public TabPage this[int Index]
        {
            get
            {
                return (TabPage)(List[Index]);
            }
        }

        /// <summary>
        /// Gets a TabPage associated with the Form from the collection.
        /// </summary>
        /// <param name="Form"></param>
        /// <returns></returns>
        [Description("Gets a TabPage associated with the Form from the collection.")]
        public TabPage this[Form Form]
        {
            get
            {
                int x = IndexOf(Form);
                if (x == -1)
                {
                    return null;
                }
                else
                {
                    return (TabPage)(List[x]);
                }
            }
        }

        public int IndexOf(TabPage TabPage)
        {
            return List.IndexOf(TabPage);
        }
        public void SetIndexOf(int value, TabPage TabPage)
        {
            IsReorder = true;
            List.Remove(TabPage);
            List.Insert(value, TabPage);
            TabControl.ArrangeItems();
            IsReorder = false;
        }

        public int IndexOf(Form Form)
        {
            int ret = -1;
            for (int i = 0; i <= List.Count - 1; i++)
            {
                if (((TabPage)(List[i])).m_Form.Equals(Form))
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }

        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            if (IsReorder)
            {
                return;
            }
            // insert the controls in the respective containers
            TabControl.pnlBottom.Controls.Add(((TabPage)value).m_Form);
            TabControl.pnlTabs.Controls.Add((TabPage)value);
            // select the inserted tabpage
            ((TabPage)value).Select();
            TabControl.AddingPage = false;
            TabControl.ArrangeItems();
            TabControl.Background.Visible = false;
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            if (IsReorder)
            {
                return;
            }
            if (List.Count == 0)
            {
                TabControl.Background.Visible = true;
            }
            TabControl.ArrangeItems();
            TabControl.pnlBottom.Controls.Remove(((TabPage)value).m_Form);
            ((TabPage)value).m_Form.Dispose();
            TabControl.pnlTabs.Controls.Remove((TabPage)value);
            ((TabPage)value).Dispose();
            TabControl.SelectItem(null);
        }

        protected override void OnClear()
        {
            base.OnClear();
            TabControl.Background.Visible = true;
        }

        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            TabControl.pnlBottom.Controls.Clear();
            TabControl.pnlTabs.Controls.Clear();
        }

        /// <summary>
        /// Returns the selected TabPage.
        /// </summary>
        /// <returns></returns>
        [Description("Returns the selected TabPage.")]
        public TabPage SelectedTab()
        {
            foreach (TabPage T in List)
            {
                if (T.IsSelected)
                {
                    return T;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the index of the selected TabPage.
        /// </summary>
        /// <returns></returns>
        [Description("Returns the index of the selected TabPage.")]
        public int SelectedIndex()
        {
            foreach (TabPage T in List)
            {
                if (T.IsSelected)
                {
                    return List.IndexOf(T);
                }
            }
            return default(int);
        }

        private void TabPage_Clicked(object sender, EventArgs e)
        {
            TabControl.SelectItem((TabPage)sender);
            if (SelectedChangedEvent != null)
                SelectedChangedEvent(sender, e);
        }

        private void TabPage_Closed(object sender, EventArgs e)
        {
            Remove((TabPage)sender);
        }

        private void TabPage_GetTabRegion(object sender, TabControl.GetTabRegionEventArgs e)
        {
            if (GetTabRegionEvent != null)
                GetTabRegionEvent(sender, e);
        }

        private void TabPage_TabPaintBackground(object sender, TabControl.TabPaintEventArgs e)
        {
            if (TabPaintBackgroundEvent != null)
                TabPaintBackgroundEvent(sender, e);
        }

        private void TabPage_TabPaintBorder(object sender, TabControl.TabPaintEventArgs e)
        {
            if (TabPaintBorderEvent != null)
                TabPaintBorderEvent(sender, e);
        }

        private void TabPage_SizeChanged(object sender, EventArgs e)
        {
            TabControl.ArrangeItems();
        }

        private TabPage CurrentTab = null;

        private void TabPage_Dragging(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (TabControl.AllowTabReorder && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                TabPage t = GetTabPage((TabPage)sender, e.X, e.Y);
                if (t == null)
                {
                    CurrentTab = null;
                }
                else if (t != CurrentTab)
                {
                    // swap the tabpages
                    SetIndexOf(IndexOf((TabPage)sender), t);
                    CurrentTab = t;
                }
            }
        }

        private void TabPage_EndDrag(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CurrentTab = null;
        }

        private TabPage GetTabPage(TabPage TabPage, int x, int y)
        {
            for (int i = 0; i <= List.Count - 1; i++)
            {
                if (((TabPage)(List[i])) != TabPage && ((TabPage)(List[i])).TabVisible)
                {
                    if (((TabPage)(List[i])).RectangleToScreen(((TabPage)(List[i])).ClientRectangle).Contains(TabPage.PointToScreen(new Point(x, y))))
                    {
                        return ((TabPage)(List[i]));
                    }
                }
            }
            return null;
        }

        private void TabPage_Enter(object sender, EventArgs e)
        {
            if (!TabControl.m_Focused)
            {
                TabControl.SetFocus = true;
            }
        }

        private void TabPage_Leave(object sender, EventArgs e)
        {
            if (TabControl.m_Focused)
            {
                TabControl.SetFocus = false;
            }
        }

    }

}
