using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;


namespace MdiTabControlTest
{
    partial class MainForm
    {

        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //Required by the Windows Form Designer
        private System.ComponentModel.Container components = null;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Load += new System.EventHandler(MainForm_Load);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ToolStripProfessionalRenderer ToolStripProfessionalRenderer1 = new System.Windows.Forms.ToolStripProfessionalRenderer();
            System.Windows.Forms.ToolStripProfessionalRenderer ToolStripProfessionalRenderer2 = new System.Windows.Forms.ToolStripProfessionalRenderer();
            System.Windows.Forms.ToolStripProfessionalRenderer ToolStripProfessionalRenderer3 = new System.Windows.Forms.ToolStripProfessionalRenderer();
            System.Windows.Forms.ToolStripProfessionalRenderer ToolStripProfessionalRenderer4 = new System.Windows.Forms.ToolStripProfessionalRenderer();
            System.Windows.Forms.ToolStripSystemRenderer ToolStripSystemRenderer1 = new System.Windows.Forms.ToolStripSystemRenderer();
            System.Windows.Forms.ToolStripProfessionalRenderer ToolStripProfessionalRenderer5 = new System.Windows.Forms.ToolStripProfessionalRenderer();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            this.ChangePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangePropertiesToolStripMenuItem.Click += new System.EventHandler(this.ChangePropertiesToolStripMenuItem_Click);
            this.AnimateIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnimateIconToolStripMenuItem.Click += new System.EventHandler(this.AnimateIconToolStripMenuItem_Click);
            this.Option1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TabControl7 = new Thinksea.Windows.Forms.MdiTabControl.TabControl();
            this.TabControl7.GetTabRegion += new Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventHandler(this.TabControl7_GetTabRegion);
            this.TabControl6 = new Thinksea.Windows.Forms.MdiTabControl.TabControl();
            this.TabControl6.TabPaintBorder += new Thinksea.Windows.Forms.MdiTabControl.TabControl.TabPaintBorderEventHandler(this.TabControl6_TabPaintBorder);
            this.TabControl5 = new Thinksea.Windows.Forms.MdiTabControl.TabControl();
            this.TabControl5.GetTabRegion += new Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventHandler(this.TabControl5_GetTabRegion);
            this.TabControl4 = new Thinksea.Windows.Forms.MdiTabControl.TabControl();
            this.TabControl3 = new Thinksea.Windows.Forms.MdiTabControl.TabControl();
            this.TabControl3.TabPaintBorder += new Thinksea.Windows.Forms.MdiTabControl.TabControl.TabPaintBorderEventHandler(this.TabControl3_TabPaintBorder);
            this.TabControl2 = new Thinksea.Windows.Forms.MdiTabControl.TabControl();
            this.TabControl2.GetTabRegion += new Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventHandler(this.TabControl2_GetTabRegion);
            this.TabControl1 = new Thinksea.Windows.Forms.MdiTabControl.TabControl();
            this.TabControl1.GetTabRegion += new Thinksea.Windows.Forms.MdiTabControl.TabControl.GetTabRegionEventHandler(this.TabControl1_GetTabRegion);
            this.UntabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UntabToolStripMenuItem.Click += new System.EventHandler(this.UntabToolStripMenuItem_Click);
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            //MenuStrip1
            //
            this.MenuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.AddToolStripMenuItem, this.RemoveToolStripMenuItem, this.ChangePropertiesToolStripMenuItem, this.AnimateIconToolStripMenuItem, this.UntabToolStripMenuItem });
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(592, 24);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "MenuStrip1";
            //
            //AddToolStripMenuItem
            //
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.AddToolStripMenuItem.Text = "Add";
            //
            //RemoveToolStripMenuItem
            //
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.RemoveToolStripMenuItem.Text = "Remove Selected";
            //
            //ChangePropertiesToolStripMenuItem
            //
            this.ChangePropertiesToolStripMenuItem.Name = "ChangePropertiesToolStripMenuItem";
            this.ChangePropertiesToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.ChangePropertiesToolStripMenuItem.Text = "Change Properties";
            //
            //AnimateIconToolStripMenuItem
            //
            this.AnimateIconToolStripMenuItem.Name = "AnimateIconToolStripMenuItem";
            this.AnimateIconToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.AnimateIconToolStripMenuItem.Text = "Animate Icon";
            //
            //Option1ToolStripMenuItem
            //
            this.Option1ToolStripMenuItem.Name = "Option1ToolStripMenuItem";
            this.Option1ToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.Option1ToolStripMenuItem.Text = "option 1";
            //
            //StatusStrip1
            //
            this.StatusStrip1.Location = new System.Drawing.Point(0, 438);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(592, 22);
            this.StatusStrip1.TabIndex = 3;
            this.StatusStrip1.Text = "StatusStrip1";
            //
            //Timer1
            //
            //
            //ImageList1
            //
            this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream"));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "8.ico");
            this.ImageList1.Images.SetKeyName(1, "1.ico");
            this.ImageList1.Images.SetKeyName(2, "2.ico");
            this.ImageList1.Images.SetKeyName(3, "3.ico");
            this.ImageList1.Images.SetKeyName(4, "4.ico");
            this.ImageList1.Images.SetKeyName(5, "5.ico");
            this.ImageList1.Images.SetKeyName(6, "6.ico");
            this.ImageList1.Images.SetKeyName(7, "7.ico");
            //
            //TabControl7
            //
            this.TabControl7.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Top;
            this.TabControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl7.Location = new System.Drawing.Point(0, 387);
            this.TabControl7.MenuRenderer = null;
            this.TabControl7.Name = "TabControl7";
            this.TabControl7.Size = new System.Drawing.Size(592, 51);
            this.TabControl7.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.TabControl7.TabBorderEnhanceWeight = Thinksea.Windows.Forms.MdiTabControl.TabControl.Weight.Soft;
            this.TabControl7.TabCloseButtonImage = null;
            this.TabControl7.TabCloseButtonImageDisabled = null;
            this.TabControl7.TabCloseButtonImageHot = null;
            this.TabControl7.TabIndex = 10;
            this.TabControl7.TabsDirection = Thinksea.Windows.Forms.MdiTabControl.TabControl.FlowDirection.LeftToRight;
            //
            //TabControl6
            //
            this.TabControl6.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Top;
            this.TabControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl6.Location = new System.Drawing.Point(0, 343);
            ToolStripProfessionalRenderer1.RoundedEdges = true;
            this.TabControl6.MenuRenderer = ToolStripProfessionalRenderer1;
            this.TabControl6.Name = "TabControl6";
            this.TabControl6.Size = new System.Drawing.Size(592, 44);
            this.TabControl6.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.TabControl6.TabBorderEnhanced = true;
            this.TabControl6.TabBorderEnhanceWeight = Thinksea.Windows.Forms.MdiTabControl.TabControl.Weight.Soft;
            this.TabControl6.TabCloseButtonImage = global::MdiTabControlTest.Properties.Resources.Close;
            this.TabControl6.TabCloseButtonImageDisabled = global::MdiTabControlTest.Properties.Resources.CloseDisabled;
            this.TabControl6.TabCloseButtonImageHot = global::MdiTabControlTest.Properties.Resources.CloseHot;
            this.TabControl6.TabCloseButtonSize = new System.Drawing.Size(14, 14);
            this.TabControl6.TabIndex = 9;
            this.TabControl6.TabsDirection = Thinksea.Windows.Forms.MdiTabControl.TabControl.FlowDirection.LeftToRight;
            //
            //TabControl5
            //
            this.TabControl5.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Top;
            this.TabControl5.BackHighColor = System.Drawing.Color.PaleGoldenrod;
            this.TabControl5.BackLowColor = System.Drawing.Color.PaleGoldenrod;
            this.TabControl5.CloseButtonVisible = true;
            this.TabControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl5.DropButtonVisible = false;
            this.TabControl5.Location = new System.Drawing.Point(0, 280);
            ToolStripProfessionalRenderer2.RoundedEdges = true;
            this.TabControl5.MenuRenderer = ToolStripProfessionalRenderer2;
            this.TabControl5.Name = "TabControl5";
            this.TabControl5.Size = new System.Drawing.Size(592, 63);
            this.TabControl5.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.TabControl5.TabBorderEnhanced = true;
            this.TabControl5.TabBorderEnhanceWeight = Thinksea.Windows.Forms.MdiTabControl.TabControl.Weight.Soft;
            this.TabControl5.TabCloseButtonImage = null;
            this.TabControl5.TabCloseButtonImageDisabled = null;
            this.TabControl5.TabCloseButtonImageHot = null;
            this.TabControl5.TabCloseButtonVisible = false;
            this.TabControl5.TabHeight = 16;
            this.TabControl5.TabIconSize = new System.Drawing.Size(14, 14);
            this.TabControl5.TabIndex = 7;
            this.TabControl5.TabMaximumWidth = 80;
            this.TabControl5.TabMinimumWidth = 80;
            this.TabControl5.TabOffset = -2;
            this.TabControl5.TabPadLeft = 15;
            this.TabControl5.TabsDirection = Thinksea.Windows.Forms.MdiTabControl.TabControl.FlowDirection.LeftToRight;
            this.TabControl5.TabTop = 1;
            //
            //TabControl4
            //
            this.TabControl4.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Top;
            this.TabControl4.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.TabControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl4.Location = new System.Drawing.Point(0, 168);
            ToolStripProfessionalRenderer3.RoundedEdges = true;
            this.TabControl4.MenuRenderer = ToolStripProfessionalRenderer3;
            this.TabControl4.Name = "TabControl4";
            this.TabControl4.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.TabControl4.Size = new System.Drawing.Size(592, 112);
            this.TabControl4.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.TabControl4.TabBorderEnhanced = true;
            this.TabControl4.TabBorderEnhanceWeight = Thinksea.Windows.Forms.MdiTabControl.TabControl.Weight.Medium;
            this.TabControl4.TabCloseButtonImage = null;
            this.TabControl4.TabCloseButtonImageDisabled = null;
            this.TabControl4.TabCloseButtonImageHot = null;
            this.TabControl4.TabGlassGradient = true;
            this.TabControl4.TabIndex = 8;
            this.TabControl4.TabOffset = 1;
            this.TabControl4.TabsDirection = Thinksea.Windows.Forms.MdiTabControl.TabControl.FlowDirection.LeftToRight;
            //
            //TabControl3
            //
            this.TabControl3.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Bottom;
            this.TabControl3.AllowTabReorder = false;
            this.TabControl3.BackHighColor = System.Drawing.SystemColors.ControlDark;
            this.TabControl3.BackLowColor = System.Drawing.SystemColors.ControlDark;
            this.TabControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl3.DropButtonVisible = false;
            this.TabControl3.FontBoldOnSelect = false;
            this.TabControl3.ForeColorDisabled = System.Drawing.SystemColors.ControlDarkDark;
            this.TabControl3.HotTrack = false;
            this.TabControl3.Location = new System.Drawing.Point(0, 126);
            ToolStripProfessionalRenderer4.RoundedEdges = true;
            this.TabControl3.MenuRenderer = ToolStripProfessionalRenderer4;
            this.TabControl3.Name = "TabControl3";
            this.TabControl3.Size = new System.Drawing.Size(592, 42);
            this.TabControl3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.TabControl3.TabBackHighColorDisabled = System.Drawing.Color.Transparent;
            this.TabControl3.TabBackLowColor = System.Drawing.SystemColors.Window;
            this.TabControl3.TabBackLowColorDisabled = System.Drawing.Color.Transparent;
            this.TabControl3.TabBorderEnhanceWeight = Thinksea.Windows.Forms.MdiTabControl.TabControl.Weight.Soft;
            this.TabControl3.TabCloseButtonImage = null;
            this.TabControl3.TabCloseButtonImageDisabled = null;
            this.TabControl3.TabCloseButtonImageHot = null;
            this.TabControl3.TabCloseButtonVisible = false;
            this.TabControl3.TabHeight = 18;
            this.TabControl3.TabIconSize = new System.Drawing.Size(12, 12);
            this.TabControl3.TabIndex = 6;
            this.TabControl3.TabOffset = -1;
            this.TabControl3.TabsDirection = Thinksea.Windows.Forms.MdiTabControl.TabControl.FlowDirection.LeftToRight;
            //
            //TabControl2
            //
            this.TabControl2.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Top;
            this.TabControl2.BackHighColor = System.Drawing.Color.Transparent;
            this.TabControl2.BackLowColor = System.Drawing.Color.Transparent;
            this.TabControl2.CloseButtonVisible = true;
            this.TabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl2.Location = new System.Drawing.Point(0, 70);
            this.TabControl2.MenuRenderer = ToolStripSystemRenderer1;
            this.TabControl2.Name = "TabControl2";
            this.TabControl2.Size = new System.Drawing.Size(592, 56);
            this.TabControl2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            this.TabControl2.TabBorderEnhanced = true;
            this.TabControl2.TabBorderEnhanceWeight = Thinksea.Windows.Forms.MdiTabControl.TabControl.Weight.Soft;
            this.TabControl2.TabCloseButtonImage = null;
            this.TabControl2.TabCloseButtonImageDisabled = null;
            this.TabControl2.TabCloseButtonImageHot = null;
            this.TabControl2.TabCloseButtonSize = new System.Drawing.Size(14, 14);
            this.TabControl2.TabCloseButtonVisible = false;
            this.TabControl2.TabHeight = 18;
            this.TabControl2.TabIndex = 5;
            this.TabControl2.TabOffset = -8;
            this.TabControl2.TabPadLeft = 20;
            this.TabControl2.TabsDirection = Thinksea.Windows.Forms.MdiTabControl.TabControl.FlowDirection.LeftToRight;
            this.TabControl2.TabTop = 1;
            //
            //TabControl1
            //
            this.TabControl1.Alignment = (Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment)Thinksea.Windows.Forms.MdiTabControl.TabControl.TabAlignment.Top;
            this.TabControl1.BackHighColor = System.Drawing.Color.Transparent;
            this.TabControl1.BackLowColor = System.Drawing.Color.Transparent;
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabControl1.ForeColor = System.Drawing.Color.Maroon;
            this.TabControl1.ForeColorDisabled = System.Drawing.Color.IndianRed;
            this.TabControl1.Location = new System.Drawing.Point(0, 24);
            ToolStripProfessionalRenderer5.RoundedEdges = true;
            this.TabControl1.MenuRenderer = ToolStripProfessionalRenderer5;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.Size = new System.Drawing.Size(592, 46);
            this.TabControl1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.TabControl1.TabBackHighColor = System.Drawing.SystemColors.Control;
            this.TabControl1.TabBackLowColorDisabled = System.Drawing.SystemColors.Control;
            this.TabControl1.TabBorderEnhanceWeight = Thinksea.Windows.Forms.MdiTabControl.TabControl.Weight.Soft;
            this.TabControl1.TabCloseButtonBackHighColor = System.Drawing.Color.Transparent;
            this.TabControl1.TabCloseButtonBackHighColorDisabled = System.Drawing.Color.Transparent;
            this.TabControl1.TabCloseButtonBackHighColorHot = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TabControl1.TabCloseButtonBackLowColor = System.Drawing.Color.Transparent;
            this.TabControl1.TabCloseButtonBackLowColorDisabled = System.Drawing.Color.Transparent;
            this.TabControl1.TabCloseButtonBackLowColorHot = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TabControl1.TabCloseButtonBorderColor = System.Drawing.SystemColors.ControlDark;
            this.TabControl1.TabCloseButtonBorderColorDisabled = System.Drawing.SystemColors.GrayText;
            this.TabControl1.TabCloseButtonBorderColorHot = System.Drawing.SystemColors.HotTrack;
            this.TabControl1.TabCloseButtonForeColor = System.Drawing.SystemColors.ControlText;
            this.TabControl1.TabCloseButtonForeColorDisabled = System.Drawing.SystemColors.GrayText;
            this.TabControl1.TabCloseButtonForeColorHot = System.Drawing.SystemColors.ControlText;
            this.TabControl1.TabCloseButtonImage = null;
            this.TabControl1.TabCloseButtonImageDisabled = null;
            this.TabControl1.TabCloseButtonImageHot = null;
            this.TabControl1.TabIconSize = new System.Drawing.Size(24, 24);
            this.TabControl1.TabIndex = 4;
            this.TabControl1.TabOffset = 0;
            this.TabControl1.TabsDirection = Thinksea.Windows.Forms.MdiTabControl.TabControl.FlowDirection.LeftToRight;
            //
            //UntabToolStripMenuItem
            //
            this.UntabToolStripMenuItem.Name = "UntabToolStripMenuItem";
            this.UntabToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.UntabToolStripMenuItem.Text = "untab";
            //
            //MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF((float)(6.0F), (float)(13.0F));
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 460);
            this.Controls.Add(this.TabControl7);
            this.Controls.Add(this.TabControl6);
            this.Controls.Add(this.TabControl5);
            this.Controls.Add(this.TabControl4);
            this.Controls.Add(this.TabControl3);
            this.Controls.Add(this.TabControl2);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.MenuStrip1);
            this.Controls.Add(this.StatusStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MDI Tab Control Test";
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem Option1ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal Thinksea.Windows.Forms.MdiTabControl.TabControl TabControl1;
        internal Thinksea.Windows.Forms.MdiTabControl.TabControl TabControl2;
        internal Thinksea.Windows.Forms.MdiTabControl.TabControl TabControl3;
        internal Thinksea.Windows.Forms.MdiTabControl.TabControl TabControl5;
        internal Thinksea.Windows.Forms.MdiTabControl.TabControl TabControl4;
        internal System.Windows.Forms.ToolStripMenuItem ChangePropertiesToolStripMenuItem;
        internal Thinksea.Windows.Forms.MdiTabControl.TabControl TabControl6;
        internal System.Windows.Forms.ToolStripMenuItem AnimateIconToolStripMenuItem;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.ImageList ImageList1;
        internal Thinksea.Windows.Forms.MdiTabControl.TabControl TabControl7;
        internal System.Windows.Forms.ToolStripMenuItem UntabToolStripMenuItem;
    }

}
