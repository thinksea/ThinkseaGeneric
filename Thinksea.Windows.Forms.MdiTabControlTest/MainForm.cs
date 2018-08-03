// VBConversions Note: VB project level imports
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
// End of VB project level imports


namespace MdiTabControlTest
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        // VBConversions Note: Former VB local static variables moved to class level.
        private int AddToolStripMenuItem_Click_i = 0;
        private int AddToolStripMenuItem_Click_count = 0;

        public void AddToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            // static int i = 0; VBConversions Note: Static variable moved to class level and renamed AddToolStripMenuItem_Click_i. Local static variables are not supported in C#.
            // static int count = 0; VBConversions Note: Static variable moved to class level and renamed AddToolStripMenuItem_Click_count. Local static variables are not supported in C#.
            Form f = default(Form);
            f = new Form();
            f.Text = "Tab " + AddToolStripMenuItem_Click_i.ToString();
            TabControl5.TabPages.Add(f);
            f = new SearchForm();
            ((SearchForm)f).Activated += new System.EventHandler(((SearchForm)f).SearchForm_Activated);
            ((SearchForm)f).Enter += new System.EventHandler(((SearchForm)f).SearchForm_Enter);
            ((SearchForm)f).FormClosing += new System.Windows.Forms.FormClosingEventHandler(((SearchForm)f).SearchForm_FormClosing);
            AddToolStripMenuItem_Click_i++;
            f.Text = "My MDI Form #" + AddToolStripMenuItem_Click_i.ToString();
            if (AddToolStripMenuItem_Click_i == 2)
            {
                f.Text += f.Text;
            }
            TabControl4.TabPages.Add(f);
            f = new Form();
            f.Text = "Tab " + AddToolStripMenuItem_Click_i.ToString();
            TabControl3.TabPages.Add(f);
            f = new Form();
            f.Text = "Tab " + AddToolStripMenuItem_Click_i.ToString();
            Thinksea.Windows.Forms.MdiTabControl.TabPage x = TabControl2.TabPages.Add(f);
            x.MouseClick += new System.Windows.Forms.MouseEventHandler(TabMouseClick);
            AddToolStripMenuItem_Click_count++;
            if (AddToolStripMenuItem_Click_count == 5)
            {
                AddToolStripMenuItem_Click_count = 1;
            }
            if (AddToolStripMenuItem_Click_count == 1)
            {
                x.BackLowColorDisabled = Color.Red;
                x.BackLowColor = Color.Red;
            }
            else if (AddToolStripMenuItem_Click_count == 2)
            {
                x.BackLowColorDisabled = Color.Yellow;
                x.BackLowColor = Color.Yellow;
            }
            else if (AddToolStripMenuItem_Click_count == 3)
            {
                x.BackLowColorDisabled = Color.Green;
                x.BackLowColor = Color.Green;
            }
            else
            {
                x.BackLowColorDisabled = Color.Blue;
                x.BackLowColor = Color.Blue;
            }
            f.BackColor = x.BackLowColor;
            f = new Form();
            f.Text = "Tab " + AddToolStripMenuItem_Click_i.ToString() + " has a long text and uses elipses";
            TabControl1.TabPages.Add(f);
            f = new Form();
            f.Text = "Tab " + AddToolStripMenuItem_Click_i.ToString();
            TabControl6.TabPages.Add(f);
            f = new Form();
            f.Text = "Tab " + AddToolStripMenuItem_Click_i.ToString();
            TabControl7.TabPages.Add(f);
        }

        public void TabMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip x = new ContextMenuStrip();
                ToolStripItem t = x.Items.Add("Close it", null, Closeit);
                t.Tag = sender;
                x.Show((Thinksea.Windows.Forms.MdiTabControl.TabPage)sender, e.Location);
            }
        }

        public void Closeit(object sender, EventArgs e)
        {
            ((Thinksea.Windows.Forms.MdiTabControl.TabPage)(((ToolStripItem)sender).Tag)).Form.Close();
        }

        public void RemoveToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            if (TabControl7.TabPages.Count > 0)
            {
                TabControl7.TabPages.Remove(TabControl7.TabPages.SelectedTab());
            }
            if (TabControl6.TabPages.Count > 0)
            {
                TabControl6.TabPages.Remove(TabControl6.TabPages.SelectedTab());
            }
            if (TabControl5.TabPages.Count > 0)
            {
                TabControl5.TabPages.Remove(TabControl5.TabPages.SelectedTab());
            }
            if (TabControl4.TabPages.Count > 0)
            {
                TabControl4.TabPages.Remove(TabControl4.TabPages.SelectedTab());
            }
            if (TabControl3.TabPages.Count > 0)
            {
                TabControl3.TabPages.Remove(TabControl3.TabPages.SelectedTab());
            }
            if (TabControl2.TabPages.Count > 0)
            {
                TabControl2.TabPages.Remove(TabControl2.TabPages.SelectedTab());
            }
            if (TabControl1.TabPages.Count > 0)
            {
                TabControl1.TabPages.Remove(TabControl1.TabPages.SelectedTab());
            }
        }

        public void TabControl1_GetTabRegion(object sender, Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventArgs e)
        {
            System.Drawing.Point[] temp_array = e.Points;
            Array.Resize(ref temp_array, 4);
            e.Points = temp_array;
            if (e.Selected)
            {
                e.Points[0] = new Point(0, e.TabHeight);
                e.Points[1] = new Point(0, 0);
                e.Points[2] = new Point(e.TabWidth, 0);
                e.Points[3] = new Point(e.TabWidth, e.TabHeight);
            }
            else
            {
                e.Points[0] = new Point(0, e.TabHeight - 2);
                e.Points[1] = new Point(0, -1);
                e.Points[2] = new Point(e.TabWidth, -1);
                e.Points[3] = new Point(e.TabWidth, e.TabHeight - 2);
            }
        }

        public void TabControl2_GetTabRegion(object sender, Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventArgs e)
        {
            e.Points[1] = new Point(e.TabHeight - 2, 2);
            e.Points[2] = new Point(e.TabHeight + 2, 0);
        }

        public void TabControl5_GetTabRegion(object sender, Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventArgs e)
        {
            System.Drawing.Point[] temp_array = e.Points;
            Array.Resize(ref temp_array, 8);
            e.Points = temp_array;
            e.Points[0] = new Point(0, 19);
            e.Points[1] = new Point(7, 5);
            e.Points[2] = new Point(10, 2);
            e.Points[3] = new Point(13, 0);
            e.Points[4] = new Point(e.TabWidth - 13, 0);
            e.Points[5] = new Point(e.TabWidth - 10, 2);
            e.Points[6] = new Point(e.TabWidth - 7, 5);
            e.Points[7] = new Point(e.TabWidth, 19);
        }

        public void TabControl7_GetTabRegion(object sender, Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath x = new System.Drawing.Drawing2D.GraphicsPath();
            x.AddArc(new Rectangle(0, 0, e.TabWidth, e.TabHeight * 2 / 3), 0, -180);
            x.Flatten();
            System.Drawing.Point[] temp_array = e.Points;
            Array.Resize(ref temp_array, x.PointCount);
            e.Points = temp_array;
            for (int i = 0; i <= x.PointCount - 1; i++)
            {
                e.Points[i] = new Point(System.Convert.ToInt32(x.PathPoints[i].X), System.Convert.ToInt32(x.PathPoints[i].Y));
            }
        }

        public void TabControl3_TabPaintBorder(object sender, Thinksea.Windows.Forms.MdiTabControl.TabControl.TabPaintEventArgs e)
        {
            if (!e.Selected)
            {
                e.Handled = true;
                e.Graphics.DrawLine(Pens.Azure, e.ClipRectangle.Width - 1, 2, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 3);
            }
        }

        public void TabControl6_TabPaintBorder(object sender, Thinksea.Windows.Forms.MdiTabControl.TabControl.TabPaintEventArgs e)
        {
            if (e.Selected || e.Hot)
            {
                e.Handled = false;
                e.Graphics.FillRectangle(Brushes.Orange, 0, 0, e.TabWidth, 3);
            }
        }

        public void ChangePropertiesToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            TabControl1.ForeColor = Color.Green;
            TabControl1.ForeColorDisabled = Color.GreenYellow;
            TabControl1.BackHighColor = Color.Bisque;
            TabControl1.TabCloseButtonVisible = false;
            TabControl1.TabPadLeft = 1;
            TabControl1.TabPadRight = 1;
            TabControl1.TabTop = 0;
            TabControl2.TopSeparator = false;
            TabControl2.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Bottom;
            TabControl2.BackHighColor = Color.Gray;
            TabControl2.BackLowColor = Color.AliceBlue;
            TabControl2.TabCloseButtonVisible = true;
            TabControl3.BorderColor = Color.Aquamarine;
            TabControl4.CloseButtonVisible = true;
            TabControl4.TabCloseButtonImage = TabControl1.TabCloseButtonImage;
            TabControl4.TabCloseButtonImageDisabled = TabControl1.TabCloseButtonImageDisabled;
            TabControl4.TabCloseButtonImageHot = TabControl1.TabCloseButtonImageHot;
            TabControl4.TabCloseButtonSize = new Size(10, 10);
            TabControl4.TabHeight = 12;
            TabControl4.TabIconSize = new Size(10, 10);
            TabControl4.TabOffset = 10;
            TabControl5.BorderColorDisabled = Color.BlueViolet;
            TabControl5.ControlButtonBackHighColor = Color.RosyBrown;
            TabControl5.ControlButtonBackHighColor = Color.RosyBrown;
            TabControl5.ControlButtonBorderColor = Color.Red;
            TabControl5.DropButtonVisible = true;
            TabControl5.Font = new Font(FontFamily.GenericSerif, 12);
            TabControl5.FontBoldOnSelect = false;
            TabControl5.HotTrack = false;
            TabControl5.BackColor = Color.Crimson;
            TabControl5.Padding = new Padding(5, 5, 5, 5);
            TabControl5.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            TabControl5.TabBackHighColor = Color.Coral;
            TabControl5.TabBackLowColor = Color.Red;
            TabControl5.TabBackHighColorDisabled = Color.Aqua;
            TabControl5.TabBackLowColorDisabled = Color.Yellow;
            TabControl5.TabMaximumWidth = 350;
            TabControl5.TabMinimumWidth = 100;
            TabControl5.ControlButtonForeColor = Color.Red;
        }

        private Icon[] Icons = new Icon[8];

        public void MainForm_Load(object sender, System.EventArgs e)
        {
            //ToolStripManager.VisualStylesEnabled = False
            for (int i = 0; i <= 7; i++)
            {
                using (Image img = ImageList1.Images[i])
                {
                    using (Bitmap b = new System.Drawing.Bitmap(img))
                    {
                        Icons[i] = System.Drawing.Icon.FromHandle(b.GetHicon());
                        b.Dispose();
                        //b = null;
                    }
                }
            }
            TabControl7.SetColors(new ProfessionalColorTable());
        }

        public void AnimateIconToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            if (AnimateIconToolStripMenuItem.Checked)
            {
                Timer1.Stop();
                AnimateIconToolStripMenuItem.Checked = false;
                AnimateIconToolStripMenuItem.Text = "Animate Icon";
            }
            else
            {
                Timer1.Start();
                AnimateIconToolStripMenuItem.Checked = true;
                AnimateIconToolStripMenuItem.Text = "Stop Animation";
            }
        }

        // VBConversions Note: Former VB local static variables moved to class level.
        private bool Timer1_Tick_Loading = false;
        private int Timer1_Tick_i = 0;

        public void Timer1_Tick(System.Object sender, System.EventArgs e)
        {
            // static bool Loading = false; VBConversions Note: Static variable moved to class level and renamed Timer1_Tick_Loading. Local static variables are not supported in C#.
            try
            {
                if (Timer1_Tick_Loading)
                {
                    return;
                }
                Timer1_Tick_Loading = true;
                // static int i = 0; VBConversions Note: Static variable moved to class level and renamed Timer1_Tick_i. Local static variables are not supported in C#.
                foreach (Thinksea.Windows.Forms.MdiTabControl.TabPage t in TabControl4.TabPages)
                {
                    t.Icon = Icons[Timer1_Tick_i];
                }
                Timer1_Tick_i++;
                if (Timer1_Tick_i >= 8)
                {
                    Timer1_Tick_i = 0;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Timer1_Tick_Loading = false;
            }
        }

        public void UntabToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            TabControl4.TabPages.TearOff(TabControl4.TabPages.SelectedTab());


            //Dim f As Form = TabControl4.TabPages.SelectedTab.Form
            //f.Parent.Controls.Remove(f)
            //f.TopLevel = True
            //f.Dock = DockStyle.None
            //f.Top = 10
            //f.Left = 10
            //f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            //f.ShowInTaskbar = True
            //f.Show()
            //Dim T As MdiTabControl.TabPage = TabControl4.TabPages.SelectedTab
            //T.Dispose()
            //TabControl4.TabPages.Remove(TabControl4.TabPages.SelectedTab)
        }
    }
}
