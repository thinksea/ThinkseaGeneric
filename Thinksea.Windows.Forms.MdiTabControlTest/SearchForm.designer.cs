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
    partial class SearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.SearchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lvwSearch = new System.Windows.Forms.DataGridView();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.UpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.NewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.NewToolStripButton.Click += new System.EventHandler(this.NewToolStripButton_Click);
            this.EditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CopyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.UpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DownToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.ToolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.lvwSearch).BeginInit();
            this.ContextMenuStrip1.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            //ToolStrip2
            //
            this.ToolStrip2.AutoSize = false;
            this.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.SearchToolStripButton });
            this.ToolStrip2.Location = new System.Drawing.Point(0, 33);
            this.ToolStrip2.Name = "ToolStrip2";
            this.ToolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.ToolStrip2.Size = new System.Drawing.Size(537, 33);
            this.ToolStrip2.Stretch = true;
            this.ToolStrip2.TabIndex = 1;
            this.ToolStrip2.Text = "ToolStrip2";
            //
            //SearchToolStripButton
            //
            this.SearchToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SearchToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("SearchToolStripButton.Image"));
            this.SearchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchToolStripButton.Name = "SearchToolStripButton";
            this.SearchToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.SearchToolStripButton.Size = new System.Drawing.Size(65, 30);
            this.SearchToolStripButton.Text = "&Search";
            //
            //lvwSearch
            //
            this.lvwSearch.AllowUserToAddRows = false;
            this.lvwSearch.AllowUserToDeleteRows = false;
            this.lvwSearch.AllowUserToOrderColumns = true;
            this.lvwSearch.AllowUserToResizeRows = false;
            this.lvwSearch.BackgroundColor = System.Drawing.SystemColors.Window;
            this.lvwSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwSearch.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            DataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lvwSearch.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1;
            this.lvwSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.lvwSearch.ContextMenuStrip = this.ContextMenuStrip1;
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            DataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.lvwSearch.DefaultCellStyle = DataGridViewCellStyle2;
            this.lvwSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSearch.GridColor = System.Drawing.SystemColors.ControlLight;
            this.lvwSearch.Location = new System.Drawing.Point(0, 66);
            this.lvwSearch.Name = "lvwSearch";
            this.lvwSearch.ReadOnly = true;
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            DataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lvwSearch.RowHeadersDefaultCellStyle = DataGridViewCellStyle3;
            this.lvwSearch.RowHeadersVisible = false;
            this.lvwSearch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.lvwSearch.RowTemplate.Height = 17;
            this.lvwSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lvwSearch.Size = new System.Drawing.Size(537, 197);
            this.lvwSearch.TabIndex = 3;
            //
            //ContextMenuStrip1
            //
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.NewToolStripMenuItem, this.EditToolStripMenuItem, this.DeleteToolStripMenuItem, this.ToolStripMenuSeparator1, this.PrintToolStripMenuItem, this.ToolStripMenuSeparator2, this.SelectAllToolStripMenuItem, this.CopyToolStripMenuItem, this.ToolStripMenuSeparator3, this.UpToolStripMenuItem, this.DownToolStripMenuItem });
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(129, 198);
            //
            //NewToolStripMenuItem
            //
            this.NewToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("NewToolStripMenuItem.Image"));
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.NewToolStripMenuItem.Text = "&New";
            //
            //EditToolStripMenuItem
            //
            this.EditToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("EditToolStripMenuItem.Image"));
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.EditToolStripMenuItem.Text = "&Edit";
            //
            //DeleteToolStripMenuItem
            //
            this.DeleteToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("DeleteToolStripMenuItem.Image"));
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.DeleteToolStripMenuItem.Text = "&Delete";
            //
            //ToolStripMenuSeparator1
            //
            this.ToolStripMenuSeparator1.Name = "ToolStripMenuSeparator1";
            this.ToolStripMenuSeparator1.Size = new System.Drawing.Size(125, 6);
            //
            //PrintToolStripMenuItem
            //
            this.PrintToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("PrintToolStripMenuItem.Image"));
            this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            this.PrintToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.PrintToolStripMenuItem.Text = "&Print";
            //
            //ToolStripMenuSeparator2
            //
            this.ToolStripMenuSeparator2.Name = "ToolStripMenuSeparator2";
            this.ToolStripMenuSeparator2.Size = new System.Drawing.Size(125, 6);
            //
            //SelectAllToolStripMenuItem
            //
            this.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem";
            this.SelectAllToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.SelectAllToolStripMenuItem.Text = "&Select All";
            //
            //CopyToolStripMenuItem
            //
            this.CopyToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("CopyToolStripMenuItem.Image"));
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.CopyToolStripMenuItem.Text = "&Copy";
            //
            //ToolStripMenuSeparator3
            //
            this.ToolStripMenuSeparator3.Name = "ToolStripMenuSeparator3";
            this.ToolStripMenuSeparator3.Size = new System.Drawing.Size(125, 6);
            //
            //UpToolStripMenuItem
            //
            this.UpToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("UpToolStripMenuItem.Image"));
            this.UpToolStripMenuItem.Name = "UpToolStripMenuItem";
            this.UpToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.UpToolStripMenuItem.Text = "&Up";
            //
            //DownToolStripMenuItem
            //
            this.DownToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("DownToolStripMenuItem.Image"));
            this.DownToolStripMenuItem.Name = "DownToolStripMenuItem";
            this.DownToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.DownToolStripMenuItem.Text = "&Down";
            //
            //ToolStrip1
            //
            this.ToolStrip1.AutoSize = false;
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.NewToolStripButton, this.EditToolStripButton, this.DeleteToolStripButton, this.ToolStripSeparator1, this.PrintToolStripButton, this.ToolStripSeparator2, this.SelectAllToolStripButton, this.CopyToolStripButton, this.ToolStripSeparator3, this.UpToolStripButton, this.DownToolStripButton });
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.ToolStrip1.Size = new System.Drawing.Size(537, 33);
            this.ToolStrip1.Stretch = true;
            this.ToolStrip1.TabIndex = 2;
            this.ToolStrip1.Text = "0";
            //
            //NewToolStripButton
            //
            this.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("NewToolStripButton.Image"));
            this.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripButton.Name = "NewToolStripButton";
            this.NewToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.NewToolStripButton.Size = new System.Drawing.Size(38, 30);
            this.NewToolStripButton.Text = "&New";
            //
            //EditToolStripButton
            //
            this.EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("EditToolStripButton.Image"));
            this.EditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditToolStripButton.Name = "EditToolStripButton";
            this.EditToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.EditToolStripButton.Size = new System.Drawing.Size(38, 30);
            this.EditToolStripButton.Text = "&Edit";
            //
            //DeleteToolStripButton
            //
            this.DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("DeleteToolStripButton.Image"));
            this.DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteToolStripButton.Name = "DeleteToolStripButton";
            this.DeleteToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.DeleteToolStripButton.Size = new System.Drawing.Size(38, 30);
            this.DeleteToolStripButton.Text = "&Delete";
            //
            //ToolStripSeparator1
            //
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            //
            //PrintToolStripButton
            //
            this.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("PrintToolStripButton.Image"));
            this.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintToolStripButton.Name = "PrintToolStripButton";
            this.PrintToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.PrintToolStripButton.Size = new System.Drawing.Size(38, 30);
            this.PrintToolStripButton.Text = "Print";
            //
            //ToolStripSeparator2
            //
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            //
            //SelectAllToolStripButton
            //
            this.SelectAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SelectAllToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("SelectAllToolStripButton.Image"));
            this.SelectAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectAllToolStripButton.Name = "SelectAllToolStripButton";
            this.SelectAllToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.SelectAllToolStripButton.Size = new System.Drawing.Size(64, 30);
            this.SelectAllToolStripButton.Text = "&Select All";
            //
            //CopyToolStripButton
            //
            this.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("CopyToolStripButton.Image"));
            this.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripButton.Name = "CopyToolStripButton";
            this.CopyToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.CopyToolStripButton.Size = new System.Drawing.Size(38, 30);
            this.CopyToolStripButton.Text = "&Copy";
            //
            //ToolStripSeparator3
            //
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            //
            //UpToolStripButton
            //
            this.UpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UpToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("UpToolStripButton.Image"));
            this.UpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpToolStripButton.Name = "UpToolStripButton";
            this.UpToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.UpToolStripButton.Size = new System.Drawing.Size(38, 30);
            this.UpToolStripButton.Text = "&Up";
            //
            //DownToolStripButton
            //
            this.DownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DownToolStripButton.Image = (System.Drawing.Image)(resources.GetObject("DownToolStripButton.Image"));
            this.DownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DownToolStripButton.Name = "DownToolStripButton";
            this.DownToolStripButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.DownToolStripButton.Size = new System.Drawing.Size(38, 30);
            this.DownToolStripButton.Text = "&Down";
            //
            //txtSearch
            //
            this.txtSearch.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.txtSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearch.Location = new System.Drawing.Point(6, 39);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(456, 20);
            this.txtSearch.TabIndex = 0;
            //
            //txtTotal
            //
            this.txtTotal.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.txtTotal.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(474, 8);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(51, 20);
            this.txtTotal.TabIndex = 4;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //SearchForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(537, 263);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lvwSearch);
            this.Controls.Add(this.ToolStrip2);
            this.Controls.Add(this.ToolStrip1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(545, 0);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SearchForm";
            this.ToolStrip2.ResumeLayout(false);
            this.ToolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.lvwSearch).EndInit();
            this.ContextMenuStrip1.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.ToolStrip ToolStrip2;
        internal System.Windows.Forms.ToolStripButton SearchToolStripButton;
        internal System.Windows.Forms.DataGridView lvwSearch;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripButton NewToolStripButton;
        internal System.Windows.Forms.ToolStripButton EditToolStripButton;
        internal System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.ToolStripButton DeleteToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton PrintToolStripButton;
        internal System.Windows.Forms.ToolStripButton UpToolStripButton;
        internal System.Windows.Forms.ToolStripButton DownToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton SelectAllToolStripButton;
        internal System.Windows.Forms.ToolStripButton CopyToolStripButton;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem PrintToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem UpToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DownToolStripMenuItem;
        internal System.Windows.Forms.TextBox txtTotal;
    }

}
