using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Collections;
using System.Collections.Specialized;
using System.IO;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Interop.Common;

using cad = Autodesk.AutoCAD.ApplicationServices.Application;
using PCM = Autodesk.AutoCAD.PlottingServices.PlotConfigManager;
using HomeDesignCad.Plot.Util;

[assembly: CommandClass(typeof(HomeDesignCad.Plot.Dialog.BatchPlotForm))]

namespace HomeDesignCad.Plot.Dialog
{
	
	public class BatchPlotForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button LoListBtn;
		private System.Windows.Forms.GroupBox LtScGrp;
		private System.Windows.Forms.ComboBox ScaleComboBox;
		private System.Windows.Forms.CheckBox Lo1Ckbox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ContextMenu BpContextMenu;
		private System.Windows.Forms.TabPage LoSelTab;
		private System.Windows.Forms.Button CpCurSettingsBtn;
		private System.Windows.Forms.ComboBox LtScModel;
		private System.Windows.Forms.ComboBox AmountComboBox;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Button RemoveSettingsBtn;
		private System.Windows.Forms.CheckBox SelLoCkbox;
		private System.Windows.Forms.GroupBox PlotTabGrp;
		private System.Windows.Forms.ContextMenu LoTreeviewCtMenu;
		private System.Windows.Forms.ComboBox ctbFileComboBox;
		private System.Windows.Forms.TreeView LoTreeview;
		private System.Windows.Forms.TabPage PlotTab;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button ApplySettingsBtn;
		private System.Windows.Forms.ComboBox PlotterComboBox;
		private System.Windows.Forms.Button PlotBtn;
		private System.Windows.Forms.CheckBox VpChkBox;
		private System.Windows.Forms.CheckBox CurCkbox;
		private System.Windows.Forms.TabControl PlotTabs;
		private System.Windows.Forms.ListView DrawingListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.CheckBox ScaleLwChkBox;
//		private System.Windows.Forms.CheckBox PltStpChkBox;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.CheckBox LtScChkBox;
		private System.Windows.Forms.ComboBox PaperComboBox;
		private System.Windows.Forms.Button PlotDirBtn;
		private System.Windows.Forms.Button SelectDrawingsBtn;
		private System.Windows.Forms.CheckBox Plot2FileChkBox;
		private System.Windows.Forms.ComboBox LtScPaper;
		private System.Windows.Forms.TabPage MiscTab;
		private System.Windows.Forms.CheckBox LandscapeChkBox;
		private static string[,] ScaleValueArray;
		private static object[] PlotObjectsArray;
        private static string GlbDeviceName = "PDF-XChange Printer 2012.pc3";
		private static string GlbPaper = "A1";
		private static string GlbctbFile = "G-STANDARD.ctb";
		private static string GlbScale = "1:1";
		private static string[] GlbCanonicalArray;
        private Button btnPickdrawing;
        private TreeView tvpapers;
		private string PlotDate = DateTime.Now.Date.ToShortDateString();
		public BatchPlotForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            this.LandscapeChkBox = new System.Windows.Forms.CheckBox();
            this.MiscTab = new System.Windows.Forms.TabPage();
            this.LtScGrp = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LtScModel = new System.Windows.Forms.ComboBox();
            this.LtScPaper = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Plot2FileChkBox = new System.Windows.Forms.CheckBox();
            this.PlotDirBtn = new System.Windows.Forms.Button();
            this.LtScChkBox = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.VpChkBox = new System.Windows.Forms.CheckBox();
            this.SelectDrawingsBtn = new System.Windows.Forms.Button();
            this.PaperComboBox = new System.Windows.Forms.ComboBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.ScaleLwChkBox = new System.Windows.Forms.CheckBox();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DrawingListView = new System.Windows.Forms.ListView();
            this.BpContextMenu = new System.Windows.Forms.ContextMenu();
            this.PlotTabs = new System.Windows.Forms.TabControl();
            this.PlotTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.AmountComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ScaleComboBox = new System.Windows.Forms.ComboBox();
            this.ctbFileComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PlotterComboBox = new System.Windows.Forms.ComboBox();
            this.PlotTabGrp = new System.Windows.Forms.GroupBox();
            this.Lo1Ckbox = new System.Windows.Forms.CheckBox();
            this.SelLoCkbox = new System.Windows.Forms.CheckBox();
            this.CurCkbox = new System.Windows.Forms.CheckBox();
            this.LoSelTab = new System.Windows.Forms.TabPage();
            this.LoListBtn = new System.Windows.Forms.Button();
            this.LoTreeview = new System.Windows.Forms.TreeView();
            this.LoTreeviewCtMenu = new System.Windows.Forms.ContextMenu();
            this.PlotBtn = new System.Windows.Forms.Button();
            this.ApplySettingsBtn = new System.Windows.Forms.Button();
            this.RemoveSettingsBtn = new System.Windows.Forms.Button();
            this.CpCurSettingsBtn = new System.Windows.Forms.Button();
            this.btnPickdrawing = new System.Windows.Forms.Button();
            this.tvpapers = new System.Windows.Forms.TreeView();
            this.MiscTab.SuspendLayout();
            this.LtScGrp.SuspendLayout();
            this.PlotTabs.SuspendLayout();
            this.PlotTab.SuspendLayout();
            this.PlotTabGrp.SuspendLayout();
            this.LoSelTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // LandscapeChkBox
            // 
            this.LandscapeChkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LandscapeChkBox.BackColor = System.Drawing.Color.SteelBlue;
            this.LandscapeChkBox.Checked = true;
            this.LandscapeChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LandscapeChkBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LandscapeChkBox.Location = new System.Drawing.Point(7, 181);
            this.LandscapeChkBox.Name = "LandscapeChkBox";
            this.LandscapeChkBox.Size = new System.Drawing.Size(96, 26);
            this.LandscapeChkBox.TabIndex = 7;
            this.LandscapeChkBox.Text = "Landscape";
            this.LandscapeChkBox.UseVisualStyleBackColor = false;
            // 
            // MiscTab
            // 
            this.MiscTab.BackColor = System.Drawing.Color.SteelBlue;
            this.MiscTab.Controls.Add(this.LtScGrp);
            this.MiscTab.Controls.Add(this.Plot2FileChkBox);
            this.MiscTab.Controls.Add(this.PlotDirBtn);
            this.MiscTab.Controls.Add(this.LtScChkBox);
            this.MiscTab.Controls.Add(this.comboBox2);
            this.MiscTab.Controls.Add(this.VpChkBox);
            this.MiscTab.Location = new System.Drawing.Point(4, 22);
            this.MiscTab.Name = "MiscTab";
            this.MiscTab.Size = new System.Drawing.Size(379, 262);
            this.MiscTab.TabIndex = 1;
            this.MiscTab.Text = "Misc. settings";
            // 
            // LtScGrp
            // 
            this.LtScGrp.Controls.Add(this.label6);
            this.LtScGrp.Controls.Add(this.LtScModel);
            this.LtScGrp.Controls.Add(this.LtScPaper);
            this.LtScGrp.Controls.Add(this.label7);
            this.LtScGrp.Enabled = false;
            this.LtScGrp.Location = new System.Drawing.Point(10, 43);
            this.LtScGrp.Name = "LtScGrp";
            this.LtScGrp.Size = new System.Drawing.Size(326, 69);
            this.LtScGrp.TabIndex = 9;
            this.LtScGrp.TabStop = false;
            this.LtScGrp.Text = "Linetype scale values";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(86, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 8;
            this.label6.Text = "Model";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LtScModel
            // 
            this.LtScModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LtScModel.BackColor = System.Drawing.Color.Silver;
            this.LtScModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.LtScModel.ForeColor = System.Drawing.Color.Navy;
            this.LtScModel.Location = new System.Drawing.Point(19, 25);
            this.LtScModel.MaxDropDownItems = 12;
            this.LtScModel.Name = "LtScModel";
            this.LtScModel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LtScModel.Size = new System.Drawing.Size(58, 22);
            this.LtScModel.Sorted = true;
            this.LtScModel.TabIndex = 7;
            this.LtScModel.Text = "1";
            // 
            // LtScPaper
            // 
            this.LtScPaper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LtScPaper.BackColor = System.Drawing.Color.Silver;
            this.LtScPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.LtScPaper.ForeColor = System.Drawing.Color.Navy;
            this.LtScPaper.Location = new System.Drawing.Point(163, 25);
            this.LtScPaper.MaxDropDownItems = 12;
            this.LtScPaper.Name = "LtScPaper";
            this.LtScPaper.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LtScPaper.Size = new System.Drawing.Size(58, 22);
            this.LtScPaper.Sorted = true;
            this.LtScPaper.TabIndex = 7;
            this.LtScPaper.Text = "1";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(230, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "Paper size";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Plot2FileChkBox
            // 
            this.Plot2FileChkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Plot2FileChkBox.BackColor = System.Drawing.Color.SteelBlue;
            this.Plot2FileChkBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Plot2FileChkBox.Location = new System.Drawing.Point(10, 155);
            this.Plot2FileChkBox.Name = "Plot2FileChkBox";
            this.Plot2FileChkBox.Size = new System.Drawing.Size(94, 26);
            this.Plot2FileChkBox.TabIndex = 8;
            this.Plot2FileChkBox.Text = "Plot to file";
            this.Plot2FileChkBox.UseVisualStyleBackColor = false;
            this.Plot2FileChkBox.CheckedChanged += new System.EventHandler(this.Plot2FileChkBoxCheckedChanged);
            // 
            // PlotDirBtn
            // 
            this.PlotDirBtn.BackColor = System.Drawing.Color.Silver;
            this.PlotDirBtn.Enabled = false;
            this.PlotDirBtn.ForeColor = System.Drawing.Color.Navy;
            this.PlotDirBtn.Location = new System.Drawing.Point(115, 155);
            this.PlotDirBtn.Name = "PlotDirBtn";
            this.PlotDirBtn.Size = new System.Drawing.Size(125, 25);
            this.PlotDirBtn.TabIndex = 4;
            this.PlotDirBtn.Text = "Plot to directory";
            this.PlotDirBtn.UseVisualStyleBackColor = false;
            this.PlotDirBtn.Click += new System.EventHandler(this.PlotDirBtnClick);
            // 
            // LtScChkBox
            // 
            this.LtScChkBox.Location = new System.Drawing.Point(10, 17);
            this.LtScChkBox.Name = "LtScChkBox";
            this.LtScChkBox.Size = new System.Drawing.Size(163, 26);
            this.LtScChkBox.TabIndex = 0;
            this.LtScChkBox.Text = "Set lintype scale?";
            this.LtScChkBox.CheckedChanged += new System.EventHandler(this.LtScChkBoxCheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(403, 17);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(145, 20);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.Text = "comboBox1";
            // 
            // VpChkBox
            // 
            this.VpChkBox.Location = new System.Drawing.Point(10, 121);
            this.VpChkBox.Name = "VpChkBox";
            this.VpChkBox.Size = new System.Drawing.Size(163, 25);
            this.VpChkBox.TabIndex = 0;
            this.VpChkBox.Text = "Turn on all viewports?";
            // 
            // SelectDrawingsBtn
            // 
            this.SelectDrawingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectDrawingsBtn.BackColor = System.Drawing.Color.Silver;
            this.SelectDrawingsBtn.ForeColor = System.Drawing.Color.Navy;
            this.SelectDrawingsBtn.Location = new System.Drawing.Point(663, 296);
            this.SelectDrawingsBtn.Name = "SelectDrawingsBtn";
            this.SelectDrawingsBtn.Size = new System.Drawing.Size(150, 25);
            this.SelectDrawingsBtn.TabIndex = 4;
            this.SelectDrawingsBtn.Text = "Select drawings";
            this.SelectDrawingsBtn.UseVisualStyleBackColor = false;
            this.SelectDrawingsBtn.Click += new System.EventHandler(this.SelectDrawingsBtnClick);
            // 
            // PaperComboBox
            // 
            this.PaperComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PaperComboBox.BackColor = System.Drawing.Color.Silver;
            this.PaperComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaperComboBox.ForeColor = System.Drawing.Color.Navy;
            this.PaperComboBox.Location = new System.Drawing.Point(7, 43);
            this.PaperComboBox.MaxDropDownItems = 12;
            this.PaperComboBox.Name = "PaperComboBox";
            this.PaperComboBox.Size = new System.Drawing.Size(269, 20);
            this.PaperComboBox.TabIndex = 6;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.BackColor = System.Drawing.Color.Silver;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.ForeColor = System.Drawing.Color.Navy;
            this.CancelBtn.Location = new System.Drawing.Point(591, 374);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(90, 25);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
            // 
            // ScaleLwChkBox
            // 
            this.ScaleLwChkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScaleLwChkBox.BackColor = System.Drawing.Color.SteelBlue;
            this.ScaleLwChkBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ScaleLwChkBox.Location = new System.Drawing.Point(119, 181);
            this.ScaleLwChkBox.Name = "ScaleLwChkBox";
            this.ScaleLwChkBox.Size = new System.Drawing.Size(134, 26);
            this.ScaleLwChkBox.TabIndex = 7;
            this.ScaleLwChkBox.Text = "Scale lineweights";
            this.ScaleLwChkBox.UseVisualStyleBackColor = false;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Plot?";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "List of Drawings";
            this.columnHeader1.Width = 300;
            // 
            // DrawingListView
            // 
            this.DrawingListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawingListView.AutoArrange = false;
            this.DrawingListView.BackColor = System.Drawing.Color.Silver;
            this.DrawingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.DrawingListView.ContextMenu = this.BpContextMenu;
            this.DrawingListView.ForeColor = System.Drawing.Color.Navy;
            this.DrawingListView.GridLines = true;
            this.DrawingListView.HideSelection = false;
            this.DrawingListView.Location = new System.Drawing.Point(0, 0);
            this.DrawingListView.Name = "DrawingListView";
            this.DrawingListView.Size = new System.Drawing.Size(303, 459);
            this.DrawingListView.TabIndex = 5;
            this.DrawingListView.UseCompatibleStateImageBehavior = false;
            this.DrawingListView.View = System.Windows.Forms.View.Details;
            this.DrawingListView.SelectedIndexChanged += new System.EventHandler(this.DrawingListView_SelectedIndexChanged);
            // 
            // BpContextMenu
            // 
            this.BpContextMenu.Popup += new System.EventHandler(this.DrawingListPopUp);
            // 
            // PlotTabs
            // 
            this.PlotTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlotTabs.Controls.Add(this.PlotTab);
            this.PlotTabs.Controls.Add(this.MiscTab);
            this.PlotTabs.Controls.Add(this.LoSelTab);
            this.PlotTabs.Location = new System.Drawing.Point(303, 0);
            this.PlotTabs.Name = "PlotTabs";
            this.PlotTabs.SelectedIndex = 0;
            this.PlotTabs.Size = new System.Drawing.Size(387, 288);
            this.PlotTabs.TabIndex = 9;
            // 
            // PlotTab
            // 
            this.PlotTab.BackColor = System.Drawing.Color.SteelBlue;
            this.PlotTab.Controls.Add(this.label1);
            this.PlotTab.Controls.Add(this.AmountComboBox);
            this.PlotTab.Controls.Add(this.label4);
            this.PlotTab.Controls.Add(this.LandscapeChkBox);
            this.PlotTab.Controls.Add(this.ScaleComboBox);
            this.PlotTab.Controls.Add(this.ctbFileComboBox);
            this.PlotTab.Controls.Add(this.PaperComboBox);
            this.PlotTab.Controls.Add(this.label5);
            this.PlotTab.Controls.Add(this.label3);
            this.PlotTab.Controls.Add(this.label2);
            this.PlotTab.Controls.Add(this.PlotterComboBox);
            this.PlotTab.Controls.Add(this.ScaleLwChkBox);
            this.PlotTab.Controls.Add(this.PlotTabGrp);
            this.PlotTab.Location = new System.Drawing.Point(4, 22);
            this.PlotTab.Name = "PlotTab";
            this.PlotTab.Size = new System.Drawing.Size(379, 262);
            this.PlotTab.TabIndex = 0;
            this.PlotTab.Text = "Plot settings";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(286, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Printer/plotter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AmountComboBox
            // 
            this.AmountComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AmountComboBox.BackColor = System.Drawing.Color.Silver;
            this.AmountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.AmountComboBox.ForeColor = System.Drawing.Color.Navy;
            this.AmountComboBox.Location = new System.Drawing.Point(7, 146);
            this.AmountComboBox.MaxDropDownItems = 12;
            this.AmountComboBox.Name = "AmountComboBox";
            this.AmountComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AmountComboBox.Size = new System.Drawing.Size(269, 23);
            this.AmountComboBox.Sorted = true;
            this.AmountComboBox.TabIndex = 6;
            this.AmountComboBox.Text = "1";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(286, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "No. of copies";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScaleComboBox
            // 
            this.ScaleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScaleComboBox.BackColor = System.Drawing.Color.Silver;
            this.ScaleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScaleComboBox.ForeColor = System.Drawing.Color.Navy;
            this.ScaleComboBox.Location = new System.Drawing.Point(7, 112);
            this.ScaleComboBox.MaxDropDownItems = 12;
            this.ScaleComboBox.Name = "ScaleComboBox";
            this.ScaleComboBox.Size = new System.Drawing.Size(269, 20);
            this.ScaleComboBox.Sorted = true;
            this.ScaleComboBox.TabIndex = 6;
            // 
            // ctbFileComboBox
            // 
            this.ctbFileComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctbFileComboBox.BackColor = System.Drawing.Color.Silver;
            this.ctbFileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctbFileComboBox.ForeColor = System.Drawing.Color.Navy;
            this.ctbFileComboBox.Location = new System.Drawing.Point(7, 78);
            this.ctbFileComboBox.MaxDropDownItems = 12;
            this.ctbFileComboBox.Name = "ctbFileComboBox";
            this.ctbFileComboBox.Size = new System.Drawing.Size(269, 20);
            this.ctbFileComboBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(286, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "Plot scale";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(286, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = ".ctb File";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(286, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Paper size";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlotterComboBox
            // 
            this.PlotterComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlotterComboBox.BackColor = System.Drawing.Color.Silver;
            this.PlotterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlotterComboBox.ForeColor = System.Drawing.Color.Navy;
            this.PlotterComboBox.Location = new System.Drawing.Point(7, 9);
            this.PlotterComboBox.MaxDropDownItems = 12;
            this.PlotterComboBox.Name = "PlotterComboBox";
            this.PlotterComboBox.Size = new System.Drawing.Size(269, 20);
            this.PlotterComboBox.TabIndex = 6;
            this.PlotterComboBox.SelectedIndexChanged += new System.EventHandler(this.DeviceNameChanged);
            // 
            // PlotTabGrp
            // 
            this.PlotTabGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlotTabGrp.Controls.Add(this.Lo1Ckbox);
            this.PlotTabGrp.Controls.Add(this.SelLoCkbox);
            this.PlotTabGrp.Controls.Add(this.CurCkbox);
            this.PlotTabGrp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PlotTabGrp.Location = new System.Drawing.Point(4, 215);
            this.PlotTabGrp.Name = "PlotTabGrp";
            this.PlotTabGrp.Size = new System.Drawing.Size(327, 67);
            this.PlotTabGrp.TabIndex = 8;
            this.PlotTabGrp.TabStop = false;
            this.PlotTabGrp.Text = "Plot layout options";
            // 
            // Lo1Ckbox
            // 
            this.Lo1Ckbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lo1Ckbox.BackColor = System.Drawing.Color.SteelBlue;
            this.Lo1Ckbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Lo1Ckbox.Location = new System.Drawing.Point(17, 24);
            this.Lo1Ckbox.Name = "Lo1Ckbox";
            this.Lo1Ckbox.Size = new System.Drawing.Size(84, 26);
            this.Lo1Ckbox.TabIndex = 7;
            this.Lo1Ckbox.Text = "Layout1";
            this.Lo1Ckbox.UseVisualStyleBackColor = false;
            this.Lo1Ckbox.CheckedChanged += new System.EventHandler(this.Lo1CkboxCheckedChanged);
            // 
            // SelLoCkbox
            // 
            this.SelLoCkbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SelLoCkbox.BackColor = System.Drawing.Color.SteelBlue;
            this.SelLoCkbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelLoCkbox.Location = new System.Drawing.Point(106, 25);
            this.SelLoCkbox.Name = "SelLoCkbox";
            this.SelLoCkbox.Size = new System.Drawing.Size(136, 26);
            this.SelLoCkbox.TabIndex = 7;
            this.SelLoCkbox.Text = "Selected layouts";
            this.SelLoCkbox.UseVisualStyleBackColor = false;
            this.SelLoCkbox.CheckedChanged += new System.EventHandler(this.AllLosCkboxCheckedChanged);
            // 
            // CurCkbox
            // 
            this.CurCkbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CurCkbox.BackColor = System.Drawing.Color.SteelBlue;
            this.CurCkbox.Checked = true;
            this.CurCkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CurCkbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CurCkbox.Location = new System.Drawing.Point(250, 25);
            this.CurCkbox.Name = "CurCkbox";
            this.CurCkbox.Size = new System.Drawing.Size(74, 26);
            this.CurCkbox.TabIndex = 7;
            this.CurCkbox.Text = "Current";
            this.CurCkbox.UseVisualStyleBackColor = false;
            this.CurCkbox.CheckedChanged += new System.EventHandler(this.CurCkboxCheckedChanged);
            // 
            // LoSelTab
            // 
            this.LoSelTab.BackColor = System.Drawing.Color.SteelBlue;
            this.LoSelTab.Controls.Add(this.LoListBtn);
            this.LoSelTab.Controls.Add(this.LoTreeview);
            this.LoSelTab.Location = new System.Drawing.Point(4, 22);
            this.LoSelTab.Name = "LoSelTab";
            this.LoSelTab.Size = new System.Drawing.Size(379, 262);
            this.LoSelTab.TabIndex = 2;
            this.LoSelTab.Text = "Layout selection";
            // 
            // LoListBtn
            // 
            this.LoListBtn.BackColor = System.Drawing.Color.SteelBlue;
            this.LoListBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LoListBtn.ForeColor = System.Drawing.Color.Black;
            this.LoListBtn.Location = new System.Drawing.Point(0, 237);
            this.LoListBtn.Name = "LoListBtn";
            this.LoListBtn.Size = new System.Drawing.Size(379, 25);
            this.LoListBtn.TabIndex = 1;
            this.LoListBtn.Text = "List layouts of selected drawings";
            this.LoListBtn.UseVisualStyleBackColor = false;
            this.LoListBtn.Click += new System.EventHandler(this.LoListBtnClick);
            // 
            // LoTreeview
            // 
            this.LoTreeview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoTreeview.BackColor = System.Drawing.Color.Silver;
            this.LoTreeview.CheckBoxes = true;
            this.LoTreeview.ContextMenu = this.LoTreeviewCtMenu;
            this.LoTreeview.ForeColor = System.Drawing.Color.Navy;
            this.LoTreeview.HideSelection = false;
            this.LoTreeview.Location = new System.Drawing.Point(0, 0);
            this.LoTreeview.Name = "LoTreeview";
            this.LoTreeview.Size = new System.Drawing.Size(378, 236);
            this.LoTreeview.TabIndex = 0;
            // 
            // LoTreeviewCtMenu
            // 
            this.LoTreeviewCtMenu.Popup += new System.EventHandler(this.LoTreeviewPopUp);
            // 
            // PlotBtn
            // 
            this.PlotBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlotBtn.BackColor = System.Drawing.Color.Silver;
            this.PlotBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.PlotBtn.ForeColor = System.Drawing.Color.Navy;
            this.PlotBtn.Location = new System.Drawing.Point(466, 374);
            this.PlotBtn.Name = "PlotBtn";
            this.PlotBtn.Size = new System.Drawing.Size(90, 25);
            this.PlotBtn.TabIndex = 3;
            this.PlotBtn.Text = "Plot";
            this.PlotBtn.UseVisualStyleBackColor = false;
            this.PlotBtn.Click += new System.EventHandler(this.PlotBtnClick);
            // 
            // ApplySettingsBtn
            // 
            this.ApplySettingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplySettingsBtn.BackColor = System.Drawing.Color.Silver;
            this.ApplySettingsBtn.ForeColor = System.Drawing.Color.Navy;
            this.ApplySettingsBtn.Location = new System.Drawing.Point(491, 331);
            this.ApplySettingsBtn.Name = "ApplySettingsBtn";
            this.ApplySettingsBtn.Size = new System.Drawing.Size(150, 25);
            this.ApplySettingsBtn.TabIndex = 4;
            this.ApplySettingsBtn.Text = "Apply settings";
            this.ApplySettingsBtn.UseVisualStyleBackColor = false;
            this.ApplySettingsBtn.Click += new System.EventHandler(this.ApplySettingsBtnClick);
            // 
            // RemoveSettingsBtn
            // 
            this.RemoveSettingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveSettingsBtn.BackColor = System.Drawing.Color.Silver;
            this.RemoveSettingsBtn.ForeColor = System.Drawing.Color.Navy;
            this.RemoveSettingsBtn.Location = new System.Drawing.Point(317, 331);
            this.RemoveSettingsBtn.Name = "RemoveSettingsBtn";
            this.RemoveSettingsBtn.Size = new System.Drawing.Size(150, 25);
            this.RemoveSettingsBtn.TabIndex = 3;
            this.RemoveSettingsBtn.Text = "Remove settings";
            this.RemoveSettingsBtn.UseVisualStyleBackColor = false;
            this.RemoveSettingsBtn.Click += new System.EventHandler(this.RemoveSettingsBtnClick);
            // 
            // CpCurSettingsBtn
            // 
            this.CpCurSettingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CpCurSettingsBtn.BackColor = System.Drawing.Color.Silver;
            this.CpCurSettingsBtn.ForeColor = System.Drawing.Color.Navy;
            this.CpCurSettingsBtn.Location = new System.Drawing.Point(491, 296);
            this.CpCurSettingsBtn.Name = "CpCurSettingsBtn";
            this.CpCurSettingsBtn.Size = new System.Drawing.Size(150, 25);
            this.CpCurSettingsBtn.TabIndex = 3;
            this.CpCurSettingsBtn.Text = "Get current settings";
            this.CpCurSettingsBtn.UseVisualStyleBackColor = false;
            this.CpCurSettingsBtn.Click += new System.EventHandler(this.CpCurSettingsBtnClick);
            // 
            // btnPickdrawing
            // 
            this.btnPickdrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickdrawing.BackColor = System.Drawing.Color.Silver;
            this.btnPickdrawing.ForeColor = System.Drawing.Color.Navy;
            this.btnPickdrawing.Location = new System.Drawing.Point(317, 296);
            this.btnPickdrawing.Name = "btnPickdrawing";
            this.btnPickdrawing.Size = new System.Drawing.Size(150, 25);
            this.btnPickdrawing.TabIndex = 10;
            this.btnPickdrawing.Text = "选择打印图";
            this.btnPickdrawing.UseVisualStyleBackColor = false;
            this.btnPickdrawing.Click += new System.EventHandler(this.btnPickdrawing_Click);
            // 
            // tvpapers
            // 
            this.tvpapers.Location = new System.Drawing.Point(0, 0);
            this.tvpapers.Name = "tvpapers";
            this.tvpapers.Size = new System.Drawing.Size(305, 442);
            this.tvpapers.TabIndex = 11;
            // 
            // BatchPlotForm
            // 
            this.AcceptButton = this.PlotBtn;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(825, 436);
            this.Controls.Add(this.tvpapers);
            this.Controls.Add(this.btnPickdrawing);
            this.Controls.Add(this.PlotTabs);
            this.Controls.Add(this.DrawingListView);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.PlotBtn);
            this.Controls.Add(this.SelectDrawingsBtn);
            this.Controls.Add(this.ApplySettingsBtn);
            this.Controls.Add(this.RemoveSettingsBtn);
            this.Controls.Add(this.CpCurSettingsBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(841, 474);
            this.Name = "BatchPlotForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批处理打印Pdf软件(V0.9) by 游舞人间 2019";
            this.Load += new System.EventHandler(this.MyPlotLoad);
            this.Resize += new System.EventHandler(this.FormResized);
            this.MiscTab.ResumeLayout(false);
            this.LtScGrp.ResumeLayout(false);
            this.PlotTabs.ResumeLayout(false);
            this.PlotTab.ResumeLayout(false);
            this.PlotTabGrp.ResumeLayout(false);
            this.LoSelTab.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		void MyPlotLoad(object sender, System.EventArgs e)
		{
			string tempStr;
			bool tempTest = false;
			PlotConfigInfoCollection PCIC = PCM.Devices;
			foreach (PlotConfigInfo pci in PCIC) {
				PlotterComboBox.Items.Add(pci.DeviceName);
				if (string.Compare(pci.DeviceName, GlbDeviceName) == 0) tempTest = true;
			}
			if (tempTest) {
				PlotterComboBox.Text = GlbDeviceName;
				PaperComboBox.Text = GlbPaper;
				tempTest = false;
			}
			else {
				PlotterComboBox.Text = PlotterComboBox.Items[0].ToString();
				UpdatePaperListbox(PlotterComboBox.Items[0].ToString());
			}
			StringCollection ctbNames = PCM.ColorDependentPlotStyles;
			foreach (string str in ctbNames) {
				string[] tempStrArray = str.Split(new char[] {'\\'});
				ctbFileComboBox.Items.Add(tempStrArray[tempStrArray.Length - 1]);
				if (string.Compare(tempStrArray[tempStrArray.Length - 1], GlbctbFile) == 0) tempTest = true;
			}
			if (tempTest) {
				ctbFileComboBox.Text = GlbctbFile;
				tempTest = false;
			}
			else ctbFileComboBox.Text = ctbFileComboBox.Items[0].ToString();
			Type Scales = typeof(StdScaleType);
			string[] ScaleArray = Enum.GetNames(Scales);
			int i = 0;
			ScaleValueArray = new string[ScaleArray.Length, 2];
			foreach (string str in Enum.GetNames(Scales)) {
				tempStr = FormatStandardScale(str);
				ScaleComboBox.Items.Add(tempStr);
				ScaleValueArray[i, 0] = tempStr;
				ScaleValueArray[i, 1] = str;
				++i;
				if (string.Compare(tempStr, GlbScale) == 0) tempTest = true;
			}
			if (tempTest) {
				ScaleComboBox.Text = GlbScale;
			}
			else ScaleComboBox.Text = ScaleComboBox.Items[0].ToString();
		}
		
		public string FormatStandardScale (string str) {
			if (IsInString(str, "StdScale")) {
				str = str.Substring(8);
				if (IsInString(str, "Millimeter")) {
					str = str.Replace("To", "/");
                    str = str.Replace("Millimeter", "\"");
					str = str.Replace("Is", " = ");
					str = str.Replace("m", "\'");
					return str;
				}
				else if (string.Compare(str, "1mIs1m") == 0) {
					return "1\' = 1\'";
				}
				else {
					str = str.Replace("To", ":");
					return str;
				}
			}
			else return "Scale to Fit";
		}
		
		public bool IsInString (string ToCheck, string InQuestion) {
			for (int i = 0; i + InQuestion.Length < ToCheck.Length; ++i) {
				if (string.Compare(ToCheck.Substring(i, InQuestion.Length), InQuestion) == 0) {
					return true;
				}
			}
			return false;
		}
		
		public void UpdatePaperListbox (string DeviceName) {
			PaperComboBox.Items.Clear();
			PlotConfig pc = PCM.SetCurrentConfig(DeviceName);
			GlbCanonicalArray = new string[pc.CanonicalMediaNames.Count];
			int i = 0;
			foreach (string str in pc.CanonicalMediaNames) {
				PaperComboBox.Items.Add(pc.GetLocalMediaName(str));
				GlbCanonicalArray[i] = str;
				++i;
			}
			PaperComboBox.Text = PaperComboBox.Items[0].ToString();
		}
		
		void DeviceNameChanged (object sender, System.EventArgs e)
		{
			UpdatePaperListbox(PlotterComboBox.SelectedItem.ToString());
		}
		
		void CancelBtnClick(object sender, System.EventArgs e)
		{
			Close();
		}
		
		void PlotBtnClick(object sender, System.EventArgs e)
		{
			DrawingListView.Sort();			GlbDeviceName = PlotterComboBox.Text;
			GlbPaper = PaperComboBox.Text;
			GlbctbFile = ctbFileComboBox.Text;
			GlbScale = ScaleComboBox.Text;
			ListView.ListViewItemCollection lvic = DrawingListView.Items;
			PlotObjectsArray = new object[lvic.Count];
			for (int i = 0; i < lvic.Count; ++i) {
                Log4NetHelper.WriteInfoLog(lvic[i].Tag+"\n");
				PlotObjectsArray[i] = lvic[i].Tag as HdCadPlotParams;
			}
			this.Close();
		}
		
		void SelectDrawingsBtnClick(object sender, System.EventArgs e)
		{
			Autodesk.AutoCAD.Windows.OpenFileDialog Dia =
				new Autodesk.AutoCAD.Windows.OpenFileDialog("Select Drawings to Plot",
				                                            "",
				                                            "dwg",
				                                            "",
				                                            Autodesk.AutoCAD.Windows.OpenFileDialog.OpenFileDialogFlags.AllowMultiple
				                                           );
			if (Dia.ShowDialog() == DialogResult.OK) {
				string[] FileNames = Dia.GetFilenames();
				Array.Sort(FileNames);
				foreach (string str in FileNames) {
					DrawingListView.Items.Add(str);
				}
			}
		}
		
		void ApplySettingsBtnClick(object sender, System.EventArgs e)
		{
			string RealScale = "";
			string DeviceName = PlotterComboBox.SelectedItem.ToString();
			string PaperSize = PaperComboBox.SelectedItem.ToString();
			string ctbFile = ctbFileComboBox.SelectedItem.ToString();
			string Scale = ScaleComboBox.SelectedItem.ToString();
			int Amount = Convert.ToInt16(AmountComboBox.Text);
			Autodesk.AutoCAD.DatabaseServices.PlotRotation PltRot = Autodesk.AutoCAD.DatabaseServices.PlotRotation.Degrees000;
			if (
			    string.Compare(DeviceName, string.Empty) != 0
			    &&
			    string.Compare(PaperSize, string.Empty) != 0
			    &&
			    string.Compare(ctbFile, string.Empty) !=0
			    &&
			    string.Compare(Scale, string.Empty) !=0
			   )
			{
				for (int i = 0; i < ScaleValueArray.Length; ++i) {
					if (string.Compare(Scale, ScaleValueArray[i,0]) == 0) {
						RealScale = ScaleValueArray[i,1];
						i = ScaleValueArray.Length;
					}
				}
				StdScaleType ScaleType = (StdScaleType) Enum.Parse(typeof(StdScaleType), RealScale, false);
				foreach (ListViewItem lvi in DrawingListView.SelectedItems) {
					lvi.SubItems.Add("Yes");
					if (!LandscapeChkBox.Checked) PltRot = Autodesk.AutoCAD.DatabaseServices.PlotRotation.Degrees000;
					HdCadPlotParams mpp = new HdCadPlotParams(
					                           lvi.Text,
					                           DeviceName,
					                           PaperSize,
					                           ctbFile,
					                           ScaleLwChkBox.Checked,
					                           Amount,
					                           ScaleType,
					                           PltRot,
					                           GlbCanonicalArray[PaperComboBox.SelectedIndex]
					                          );
					mpp.PlotCurrentLayout = CurCkbox.Checked;
//					mpp.ApplyStamp = PltStpChkBox.Checked;
					mpp.PlotToFile = Plot2FileChkBox.Checked;
					if (Plot2FileChkBox.Checked) {
						FileInfo fi = new FileInfo(lvi.Text);
						string[] tempArray = fi.Name.Split('.');
						string PltFileDir = PlotDirBtn.Tag as string;
						mpp.PlotFileLocation = PltFileDir + "\\" + tempArray[0] + ".plt";
					}
					mpp.TurnOnViewports = VpChkBox.Checked;
					mpp.ChangeLinetypeScale = LtScChkBox.Checked;
					mpp.LinetypeScaleModel = Convert.ToDouble(LtScModel.Text);
					mpp.LinetypeScalePaper = Convert.ToDouble(LtScPaper.Text);
					if (SelLoCkbox.Checked) {
						try {
							string[] DrawingNames = LoTreeview.Tag as string[];
							TreeNode MainNode = LoTreeview.Nodes[Array.IndexOf(DrawingNames, lvi.Text as object)];
							string[] LoNames = new string[MainNode.Nodes.Count];
							int cnt = 0;
							foreach (TreeNode tn in MainNode.Nodes) {
								if (tn.Checked) {
									LoNames[cnt] = tn.Text;
									++cnt;
								}
							}
							mpp.LayoutsToPlot = LoNames;
						}
						catch (System.Exception ex) { MessageBox.Show(ex.Message); }
					}
					else if (Lo1Ckbox.Checked) mpp.LayoutsToPlot = new string[1]{"Layout1"};
					lvi.Tag = mpp;
				}
			}
		}
		
		void RemoveSettingsBtnClick(object sender, System.EventArgs e)
		{
			string tempStr;
			foreach (ListViewItem lvi in DrawingListView.SelectedItems) {
				tempStr = lvi.Text;
				lvi.SubItems.Clear();
				lvi.Text = tempStr;
				lvi.Tag = null;
			}
			
		}
		
		void ViewSettingsCtMenu (object sender, System.EventArgs e)
		{
			string Message = string.Empty;
			HdCadPlotParams tempPltParams;
			foreach (ListViewItem lvi in DrawingListView.SelectedItems) {
				tempPltParams = lvi.Tag as HdCadPlotParams;
				if (tempPltParams != null) {
					if (Message != string.Empty) Message = Message + "\n\n";
					Message = Message +
						"Drawing path: " + tempPltParams.DrawingPath +
						"\n    Printer/plotter: " + tempPltParams.Device +
						"\n    Paper size: " + tempPltParams.Paper + 
						"\n    .ctb File: " + tempPltParams.ctbFile +
						"\n    Plot scale: " + tempPltParams.AcScaleType.ToString() +
						"\n    No. of copies: " + tempPltParams.Amount.ToString() +
						"\n    Landscape: " + tempPltParams.AcPlotRotation.ToString() +
						"\n    Scale lineweights: " + tempPltParams.ScaleLineweight.ToString() +
//					    "\n    Apply plot stamp: " + tempPltParams.ApplyStamp +
						"\n    Turn on viewports: " + tempPltParams.TurnOnViewports.ToString() +
						"\n    Use current linetype scale: " + tempPltParams.ChangeLinetypeScale.ToString();
				}
			}
			if (Message != string.Empty) MessageBox.Show(Message, "Plot settings.");
		}
		
		void DrawingListPopUp (object sender, System.EventArgs e)
		{
			System.Windows.Forms.Menu.MenuItemCollection mic = BpContextMenu.MenuItems;
			mic.Clear();
			System.Windows.Forms.MenuItem mi1 = new System.Windows.Forms.MenuItem("&Apply settings");
			mi1.Click += new EventHandler(ApplySettingsBtnClick);
			mic.Add(mi1);
			mi1.Enabled = new EventHandler(ApplySettingsBtnClick) != null;
			System.Windows.Forms.MenuItem mi2 = new System.Windows.Forms.MenuItem("&Remove settings");
			mi2.Click += new EventHandler(RemoveSettingsBtnClick);
			mic.Add(mi2);
			mi2.Enabled = new EventHandler(RemoveSettingsBtnClick) != null;
			System.Windows.Forms.MenuItem mi3 = new System.Windows.Forms.MenuItem("&View settings");
			mi3.Click += new EventHandler(ViewSettingsCtMenu);
			mic.Add(mi3);
			mi3.Enabled = new EventHandler(ViewSettingsCtMenu) != null;
		}
				
		public void MyPlottingPart (HdCadPlotParams PltParams, bool IsModel) {
			object OldBkGdPlt = cad.GetSystemVariable("BackGroundPlot");
			cad.SetSystemVariable("BackGroundPlot", 0);
			Database db = HostApplicationServices.WorkingDatabase;
			PlotInfo PltInfo = new PlotInfo();
			PltInfo.Layout = LayoutManager.Current.GetLayoutId(LayoutManager.Current.CurrentLayout);
			PlotSettings PltSet = new PlotSettings(IsModel);
			PlotSettingsValidator PltSetVald = PlotSettingsValidator.Current;
			PlotPageInfo PltPgInfo = new PlotPageInfo();
			PlotProgressDialog PltPrgDia = new PlotProgressDialog(false, 1, true);
			PlotEngine PltEng = PlotFactory.CreatePublishEngine();
			PCM.SetCurrentConfig(PltParams.Device);
			PCM.RefreshList(RefreshCode.All);
			PlotConfig pc = PCM.SetCurrentConfig(PltParams.Device);
			try {
				PltSetVald.SetPlotConfigurationName(PltSet, PltParams.Device, PltParams.CanonicalPaper);
                /////////////////

				PltSetVald.RefreshLists(PltSet);

				PltSetVald.SetCurrentStyleSheet(PltSet, PltParams.ctbFile);


				PltSetVald.SetPlotOrigin(PltSet, new Point2d(0.0, 0.0));
				PltSetVald.SetPlotPaperUnits(PltSet, PlotPaperUnit.Millimeters);
                PltSetVald.SetPlotType(PltSet, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);

                //////////////////////////////////////////////

				PltSetVald.SetPlotRotation(PltSet, PltParams.AcPlotRotation);



				PltSetVald.SetUseStandardScale(PltSet, true);
				PltSetVald.SetStdScaleType(PltSet, PltParams.AcScaleType);
                PltSetVald.SetPlotCentered(PltSet, true);
				PltSet.ScaleLineweights = PltParams.ScaleLineweight;
				PltSetVald.SetZoomToPaperOnUpdate(PltSet, false);
				PltInfo.OverrideSettings = PltSet;
				PlotInfoValidator PltInfoVald = new PlotInfoValidator();
				PltInfoVald.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
				PltInfoVald.Validate(PltInfo);
				PltPrgDia.OnBeginPlot();
				PltPrgDia.IsVisible = true;
				PltEng.BeginPlot(PltPrgDia, null);
				if (PltParams.PlotToFile) PltEng.BeginDocument(PltInfo, db.Filename, null, PltParams.Amount, true, PltParams.PlotFileLocation);
				else PltEng.BeginDocument(PltInfo, db.Filename, null, PltParams.Amount, false, string.Empty);
				PltEng.BeginPage(PltPgInfo, PltInfo, true, null);
				PltEng.BeginGenerateGraphics(null);
				PltEng.EndGenerateGraphics(null);
				PltEng.EndPage(null);
				PltEng.EndDocument(null);
				PltEng.EndPlot(null);
				PltPrgDia.OnEndPlot();
			}
			catch (Autodesk.AutoCAD.Runtime.Exception AcadEr) {
				MessageBox.Show(AcadEr.Message, "Printing error (AutoCAD).");
			}
			catch (System.Exception ex) {
				MessageBox.Show(ex.Message, "Printing error (System).");
			}
			PltPrgDia.Destroy();
			PltEng.Destroy();
			cad.SetSystemVariable("BackGroundPlot", OldBkGdPlt);
		}
		
		[CommandMethod("MyBPlot", CommandFlags.Session)]
		public void MethodCall () {
			DialogResult DiaRslt;
			using (BatchPlotForm modalForm = new BatchPlotForm()) {
				DiaRslt = Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(modalForm);
			}
			if (DiaRslt == DialogResult.OK) {
				Document tempDoc = null;
                Log4NetHelper.WriteInfoLog("11111111111111111111111111\n");
				DocumentCollection DocCol = cad.DocumentManager;
				foreach (HdCadPlotParams mpp in PlotObjectsArray) {
                    Log4NetHelper.WriteInfoLog("222222222222222222222222222\n");
					if (mpp != null) {
						try {
                            Log4NetHelper.WriteInfoLog("33333333333333333333333\n");
							tempDoc = DocCol.Open(mpp.DrawingPath, true);
                            Log4NetHelper.WriteInfoLog("44444444444444444444444\n");
							Database tempDb = tempDoc.Database;
							LayoutManager tempLoMan = LayoutManager.Current;
							using (DocumentLock DocLock = tempDoc.LockDocument()) {
                                Log4NetHelper.WriteInfoLog("555555555555555555555555\n");
								if (mpp.TurnOnViewports) TurnOnViewports(tempDoc, tempDb);
								if (mpp.PlotCurrentLayout) {
									if (mpp.ApplyStamp) AddPlotText(tempDb, tempLoMan.GetLayoutId(tempLoMan.CurrentLayout));
                                    Log4NetHelper.WriteInfoLog("6666666666666666666666666666666\n");
									MyPlottingPart(mpp, CheckSpaceSetLtScale(tempLoMan.CurrentLayout, mpp));
                                    Log4NetHelper.WriteInfoLog("777777777777777777777777777777777\n");
								}
								else if (!mpp.LayoutsToPlot.Length.Equals(0)) {

                                    Log4NetHelper.WriteInfoLog("8888888888888888888888888888\n");
									foreach (string LoName in mpp.LayoutsToPlot) {
										try {

                                            Log4NetHelper.WriteInfoLog("9999999999999999999999999999999999999\n");
											tempLoMan.CurrentLayout = LoName;
											if (mpp.ApplyStamp) AddPlotText(tempDb, tempLoMan.GetLayoutId(tempLoMan.CurrentLayout));
                                            Log4NetHelper.WriteInfoLog("000000000000000000000000000000000000000\n");
											MyPlottingPart(mpp, CheckSpaceSetLtScale(LoName, mpp));
										}
										catch {}
									}
								}
							}
						}
						catch (Autodesk.AutoCAD.Runtime.Exception AcadEr) {
							MessageBox.Show(AcadEr.Message, "Drawing error (AutoCAD).");
						}
						catch (System.Exception ex){
							MessageBox.Show(ex.Message, "Drawing error (System).");
						}
						finally {
							if (tempDoc != null) tempDoc.CloseAndDiscard();
						}
					}
				}
			}
			try {
				Array.Clear(ScaleValueArray, 0, ScaleValueArray.Length);
				Array.Clear(PlotObjectsArray, 0, PlotObjectsArray.Length);
			}
			catch {}
		}
		
		void FormResized (object sender, System.EventArgs e)
		{
			ListView.ColumnHeaderCollection HdrCol = DrawingListView.Columns;
			int Wth = DrawingListView.Width;
			if (Wth < 364) HdrCol[1].Width = 60;
			else HdrCol[0].Width = Wth - 64;
		}
		void Lo1CkboxCheckedChanged(object sender, System.EventArgs e)
		{
			if (Lo1Ckbox.Checked == true) {
				SelLoCkbox.Checked = false;
				CurCkbox.Checked = false;
			}
		}
		
		void AllLosCkboxCheckedChanged(object sender, System.EventArgs e)
		{
			if (SelLoCkbox.Checked == true) {
				Lo1Ckbox.Checked = false;
				CurCkbox.Checked = false;
			}
		}
		
		void CurCkboxCheckedChanged(object sender, System.EventArgs e)
		{
			if (CurCkbox.Checked == true) {
				SelLoCkbox.Checked = false;
				Lo1Ckbox.Checked = false;
			}
		}
		
		public void AddPlotText(Database db, ObjectId LoId) {
			using (Transaction Trans = db.TransactionManager.StartTransaction()) {
				Layout Lo = (Layout)Trans.GetObject(LoId, OpenMode.ForRead);
			BlockTableRecord BlkTblRec = (BlockTableRecord)Trans.GetObject(Lo.BlockTableRecordId, OpenMode.ForRead);
				foreach (ObjectId ObjId in BlkTblRec) {
					BlockReference BlkRef = Trans.GetObject(ObjId, OpenMode.ForRead) as BlockReference;
					if (BlkRef != null) {
						BlockTableRecord tempBlkTblRec = (BlockTableRecord)Trans.GetObject(BlkRef.BlockTableRecord, OpenMode.ForRead);
						string BlkName = tempBlkTblRec.Name;
					if (
                            string.Compare(BlkName, "G-TTLB-ZN00-LINE", true) == 0
//						    ||
//						    string.Compare(BlkName, "3M-BORDER-B", true) == 0
//						    ||
//						    string.Compare(BlkName, "3M-BORDER-C", true) == 0
//						    ||
//						    string.Compare(BlkName, "3M-BORDER-D", true) == 0
//						    ||
//						    string.Compare(BlkName, "3M-BORDER-E", true) == 0
//                            ||
//						    string.Compare(BlkName, "3M-BORDER-E1", true) == 0
						   ) {
							Autodesk.AutoCAD.DatabaseServices.AttributeCollection AttCol = BlkRef.AttributeCollection;
							AttributeReference AttRef = (AttributeReference)Trans.GetObject(AttCol[64], OpenMode.ForWrite);
							AttRef.TextString = PlotDate + " - IRR -" + db.Filename;
							Trans.Commit();
							return;
						}
					}
				}
				DBText TextObj = new DBText();
				TextObj.TextString = PlotDate + " - IRR -" + db.Filename;
				TextObj.HorizontalMode = TextHorizontalMode.TextRight;
			    TextObj.VerticalMode = TextVerticalMode.TextTop;
				Point3d MinPt = (Point3d)cad.GetSystemVariable("ExtMin");
				Point3d MaxPt = (Point3d)cad.GetSystemVariable("ExtMax");
				TextObj.AlignmentPoint = new Point3d(MaxPt.X, MinPt.Y, MinPt.X);
				double TxtHt = (2.5) * (double)cad.GetSystemVariable("DimScale");
				if (TxtHt.Equals(0.0)) TxtHt = 2.5;
				TextObj.Height = TxtHt;
				BlkTblRec.UpgradeOpen();
				BlkTblRec.AppendEntity(TextObj);
				Trans.AddNewlyCreatedDBObject(TextObj, true);
				Trans.Commit();
				return;
			}
		}
		
		void CpCurSettingsBtnClick(object sender, System.EventArgs e)
		{
			Database db = HostApplicationServices.WorkingDatabase;
			using (Transaction Trans = db.TransactionManager.StartTransaction()) {
				try {
					string CurLoName = LayoutManager.Current.CurrentLayout;
					DBDictionary LoDict = (DBDictionary)Trans.GetObject(db.LayoutDictionaryId, OpenMode.ForRead);
					ObjectId LoId = (ObjectId)LoDict[CurLoName];
					Layout Lo = (Layout)Trans.GetObject(LoId, OpenMode.ForRead);
					if (PlotterComboBox.Items.Contains(Lo.PlotConfigurationName)) {
						PlotterComboBox.Text = Lo.PlotConfigurationName;
						PlotConfig pc = PCM.SetCurrentConfig(Lo.PlotConfigurationName);
						string LocalPaperName = pc.GetLocalMediaName(Lo.CanonicalMediaName);
						if (PaperComboBox.Items.Contains(LocalPaperName)) PaperComboBox.Text = LocalPaperName;
					}
					if (ctbFileComboBox.Items.Contains(Lo.PlotSettingsName)) ctbFileComboBox.Text = Lo.PlotSettingsName;
					string StdScStr = FormatStandardScale(Lo.StdScaleType.ToString());
					if (ScaleComboBox.Items.Contains(StdScStr)) ScaleComboBox.Text = StdScStr;
					if (Lo.ScaleLineweights) ScaleLwChkBox.Checked = true;
					else ScaleLwChkBox.Checked = false;
					if (Lo.PlotRotation == PlotRotation.Degrees090) LandscapeChkBox.Checked = true;
					else LandscapeChkBox.Checked = false;
				}
				catch (System.Exception ex) { MessageBox.Show(ex.Message); }
			}
		}
		
		void PlotDirBtnClick(object sender, System.EventArgs e)
		{
			Autodesk.AutoCAD.Windows.OpenFileDialog Dia =
				new Autodesk.AutoCAD.Windows.OpenFileDialog("Select directory to place .plt files:",
				                                            "",
				                                            "dwg",
				                                            "",
				                                            Autodesk.AutoCAD.Windows.OpenFileDialog.OpenFileDialogFlags.AllowFoldersOnly
				                                           );
			if (Dia.ShowDialog() == DialogResult.OK) PlotDirBtn.Tag = Dia.Filename;
		}
		
		private void TurnOnViewports(Document Doc, Database db) {
			Editor ed = Doc.Editor;
			TypedValue[] FltrList = { new TypedValue((int)DxfCode.Start, "Viewport") };
			PromptSelectionResult psr = ed.SelectAll(new SelectionFilter(FltrList));
			if (psr.Value.Count.Equals(0)) return;
			SelectionSet ss = psr.Value as SelectionSet;
			using (Transaction Trans = db.TransactionManager.StartTransaction()) {
				foreach (ObjectId ObjId in ss.GetObjectIds()) {
					Viewport vp = (Viewport)Trans.GetObject(ObjId, OpenMode.ForWrite);
					vp.On = true;
					vp.UpdateDisplay();
				}
				Trans.Commit();
			}
		}
		
		void Plot2FileChkBoxCheckedChanged(object sender, System.EventArgs e)
		{
			if (Plot2FileChkBox.Checked) PlotDirBtn.Enabled = true;
			else PlotDirBtn.Enabled = false;
		}
		
		void LtScChkBoxCheckedChanged(object sender, System.EventArgs e)
		{
			if (LtScChkBox.Checked) LtScGrp.Enabled = true;
			else LtScGrp.Enabled = false;
		}
		
		void LoListBtnClick(object sender, System.EventArgs e)
		{
			LoTreeview.Nodes.Clear();
			string[] DrawingNames = new string[DrawingListView.SelectedItems.Count];
			int cnt = 0;
			foreach (ListViewItem lvi in DrawingListView.SelectedItems) {
				try {
					using (Database db = new Database(false, true)) {
						db.ReadDwgFile (lvi.Text, System.IO.FileShare.Read, true, null);
						using (Transaction Trans = db.TransactionManager.StartTransaction()) {
							DBDictionary LoDict = (DBDictionary)Trans.GetObject(db.LayoutDictionaryId, OpenMode.ForRead);
							TreeNode MainNode = new TreeNode(lvi.Text);
							foreach (DictionaryEntry de in LoDict) {
								MainNode.Nodes.Add(de.Key as string);
							}
							LoTreeview.Nodes.Add(MainNode);
						}
					}
					DrawingNames[cnt] = lvi.Text;
					++cnt;
				}
				catch (System.Exception ex) { MessageBox.Show(ex.Message); }
			}
			LoTreeview.Tag = DrawingNames;
		}
		
		void LoTreeviewPopUp(object sender, System.EventArgs e) {
			System.Windows.Forms.Menu.MenuItemCollection mic = LoTreeviewCtMenu.MenuItems;
			mic.Clear();
			System.Windows.Forms.MenuItem mi1 = new System.Windows.Forms.MenuItem("&Select all layouts (non-Model)");
			mi1.Click += new EventHandler(SelectAllLayouts);
			mic.Add(mi1);
			mi1.Enabled = new EventHandler(SelectAllLayouts) != null;
			System.Windows.Forms.MenuItem mi1a = new System.Windows.Forms.MenuItem("&Select all layouts (include Model)");
			mi1a.Click += new EventHandler(SelectAllLayouts);
			mic.Add(mi1a);
			mi1a.Enabled = new EventHandler(SelectAllLayouts) != null;
			mic.Add("----------------------");
			System.Windows.Forms.MenuItem mi2 = new System.Windows.Forms.MenuItem("&Expand all");
			mi2.Click += new EventHandler(AllExpand);
			mic.Add(mi2);
			mi2.Enabled = new EventHandler(AllExpand) != null;
			System.Windows.Forms.MenuItem mi3 = new System.Windows.Forms.MenuItem("&Collapse all");
			mi3.Click += new EventHandler(AllCollapse);
			mic.Add(mi3);
			mi3.Enabled = new EventHandler(AllCollapse) != null;
		}
		
		void SelectAllLayouts(object sender, System.EventArgs e) {
			string tempStr = sender.ToString();
			string[] tempStrAr = tempStr.Split(' ');
			tempStr = tempStrAr[tempStrAr.Length - 1];
			if (string.Compare(tempStr, "(non-Model)").Equals(0)) {
				foreach (TreeNode MainNode in LoTreeview.Nodes) {
					foreach (TreeNode SubNode in MainNode.Nodes) {
						if (!string.Compare(SubNode.Text, "Model", true).Equals(0)) SubNode.Checked = true;
						else SubNode.Checked = false;
					}
				}
			}
			else {
				foreach (TreeNode MainNode in LoTreeview.Nodes) {
					foreach (TreeNode SubNode in MainNode.Nodes) {
						SubNode.Checked = true;
					}
				}
			}
		}
		
		void AllExpand (object sender, System.EventArgs e) {
			LoTreeview.ExpandAll();
		}
		
		void AllCollapse (object sender, System.EventArgs e) {
			LoTreeview.CollapseAll();
		}
		
		private bool CheckSpaceSetLtScale (string LoName, HdCadPlotParams mpp) {
			if (string.Compare("Model", LoName) == 0) {
				if (mpp.ChangeLinetypeScale) {
					cad.SetSystemVariable("PsLtScale", 1);
					cad.SetSystemVariable("LtScale", mpp.LinetypeScaleModel);
				}
				return true;
			}
			else {
				if (mpp.ChangeLinetypeScale) {
					cad.SetSystemVariable("LtScale", mpp.LinetypeScalePaper);
				}
				return false;
			}
		}

        private void DrawingListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 拉框选择打印图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPickdrawing_Click(object sender, EventArgs e)
        {

            Document doc =

                cad.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;

            Database db = doc.Database;


            Transaction tr =

              db.TransactionManager.StartTransaction();     

            //var ed = cad.DocumentManager.MdiActiveDocument.Editor;
            using (var eduserinteraction = ed.StartUserInteraction(this.Handle))
            {
                string blockName = "k1";
                TypedValue[] tvs = new TypedValue[] { new TypedValue(0, "INSERT"), new TypedValue(2, blockName) };
                SelectionFilter sf = new SelectionFilter(tvs);

                using (tr)
                {
                    try
                    {
                        PromptPointOptions ppo = new PromptPointOptions("\n\tSpecify a first corner: ");
                        PromptPointResult ppr = ed.GetPoint(ppo);
                        if (ppr.Status != PromptStatus.OK) return;
                        PromptCornerOptions pco = new PromptCornerOptions("\n\tOther corner: ", ppr.Value);
                        PromptPointResult pcr = ed.GetCorner(pco);
                        if (pcr.Status != PromptStatus.OK) return;
                        Point3d pt1 = ppr.Value;
                        Point3d pt2 = pcr.Value;
                        if (pt1.X == pt2.X || pt1.Y == pt2.Y)
                        {
                            ed.WriteMessage("\nInvalid point specification");
                            return;
                        }

                        PromptSelectionResult res;
                        res = ed.SelectWindow(pt1, pt2, sf);
                        Log4NetHelper.WriteInfoLog("pt1 pt2位置\n");
                        Log4NetHelper.WriteInfoLog(pt1 + "\n");
                        Log4NetHelper.WriteInfoLog(pt2 + "\n");
                        if (res.Status != PromptStatus.OK)
                            return;
                        SelectionSet sset = res.Value;
                        if (sset.Count == 0)
                            return;
                        BlockReference br = null;
                        HdCadPlotParams hacadpp = null;

                        foreach (SelectedObject obj in sset)
                        {
                            // ed.WriteMessage("\nhas data");
                            br = GetBlockReference(obj, tr);
                            hacadpp = new HdCadPlotParams();

                            hacadpp.MinPt = br.GeometricExtents.MinPoint;
                            //hacadpp.MinPt = br.GeometricExtents.MinPoint.Y * (1 - 0.0001);

                            hacadpp.MaxPt = br.GeometricExtents.MaxPoint;

                            //Log4NetHelper.WriteInfoLog(br.BlockName + "\n");
                            //Log4NetHelper.WriteInfoLog(br.Name+"\n");
                            //Log4NetHelper.WriteInfoLog(br.Bounds + "\n");
                            //Log4NetHelper.WriteInfoLog(br.Position + "\n");
                            //Log4NetHelper.WriteInfoLog(br.BlockTransform + "\n");
                            //Log4NetHelper.WriteInfoLog(br.BlockUnit + "\n");
                            //Log4NetHelper.WriteInfoLog("***********************\n");
                            //Log4NetHelper.WriteInfoLog(br.GeometricExtents.MinPoint + "\n");
                            //Log4NetHelper.WriteInfoLog("66666666666666666666666666\n");
                            //Log4NetHelper.WriteInfoLog(br.GeometricExtents.MaxPoint + "\n");
                            //Log4NetHelper.WriteInfoLog("***********************\n");
                            //Log4NetHelper.WriteInfoLog("-----------------------------\n");
                            //PlotOnePaper(db, doc, br, "DWG To PDF.pc3",
                     // "ISO_A4_(210.00_x_297.00_MM)", "111.pdf");


                        }
                    }
                    catch (System.Exception ex)
                    {
                        ed.WriteMessage(ex.Message + "\n" + ex.StackTrace);
                    }
                }
            
            
            }
            cad.MainWindow.Focus();

            //{

            //    PromptDistanceOptions opt1 = new PromptDistanceOptions("量取高度：");

            //    opt1.AllowNegative = false;
            //    opt1.AllowZero = false;
            //    opt1.AllowNone = false;
            //    opt1.UseDashedLine = true;

            //    PromptDoubleResult res = ed.GetDistance(opt1);
            //    ed.WriteMessage("\n高度是..." + res.Value);
            //    if (res != null)
            //        length = res.Value;
            //}
            //Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.Focus();
            //return length;


        }

        private BlockReference GetBlockReference(SelectedObject obj, Transaction tr)
        {
            BlockReference br = null;
            br = (BlockReference)tr.GetObject(obj.ObjectId, OpenMode.ForRead);

            return br;
        }
		
	}



    /// <summary>
    /// Description of MyPlot.
    /// </summary>
    /// 
    public class HdCadPlotParams
    {
        private string DwgPath;
        private string DeviceName;
        private string PaperSize;
        private string ctbName;
        private bool ScLw;
        private int Cnt;
        private Autodesk.AutoCAD.DatabaseServices.StdScaleType ScTyp;
        private Autodesk.AutoCAD.DatabaseServices.PlotRotation PltRot;
        private string CanonicalPaperName;
        private bool CurLo;
        private bool ShouldStamp;
        private bool Plt2File;
        private string PltFileName;
        private double LtScModel;
        private double LtScPaper;
        private bool VpOn;
        private bool CurLtSc;
        private string[] LoNames;
        private Point3d MinPoints;
        private Point3d MaxPoints;


        public HdCadPlotParams() {
            this.MinPoints = new Point3d();
            this.MaxPoints = new Point3d();
        }
        public HdCadPlotParams(string DwgPath, string DeviceName, string PaperSize, string ctbName, bool ScLw, int Cnt, Autodesk.AutoCAD.DatabaseServices.StdScaleType ScTyp, Autodesk.AutoCAD.DatabaseServices.PlotRotation PltRot, string CanonicalMedia)
        {
            this.DwgPath = DwgPath;
            this.DeviceName = DeviceName;
            this.Paper = PaperSize;
            this.ctbName = ctbName;
            this.ScLw = ScLw;
            this.Cnt = Cnt;
            this.ScTyp = ScTyp;
            this.PltRot = PltRot;
            this.CanonicalPaperName = CanonicalMedia;
            this.MinPoints = new Point3d();
            this.MaxPoints = new Point3d();
        }

        public string DrawingPath
        {
            get { return DwgPath; }
            set { DwgPath = value; }
        }
        /// <summary>
        /// DWG To PDF.pc3  打印设备文件
        /// </summary>
        public string Device
        {
            get { return DeviceName; }
            set { DeviceName = value; }
        }

        public string Paper
        {
            get { return PaperSize; }
            set { PaperSize = value; }
        }

        public string ctbFile
        {
            get { return ctbName; }
            set { ctbName = value; }
        }

        public bool ScaleLineweight
        {
            get { return ScLw; }
            set { ScLw = value; }
        }

        public int Amount
        {
            get { return Cnt; }
            set { Cnt = value; }
        }

        public Autodesk.AutoCAD.DatabaseServices.StdScaleType AcScaleType
        {
            get { return ScTyp; }
            set { ScTyp = value; }
        }

        public Autodesk.AutoCAD.DatabaseServices.PlotRotation AcPlotRotation
        {
            get { return PltRot; }
            set { PltRot = value; }
        }
        /// <summary>
        /// ISO_A4_(210.00_x_297.00_MM)  打印纸张定义
        /// </summary>
        public string CanonicalPaper
        {
            get { return CanonicalPaperName; }
            set { CanonicalPaperName = value; }
        }

        public bool PlotCurrentLayout
        {
            get { return CurLo; }
            set { CurLo = value; }
        }

        public bool ApplyStamp
        {
            get { return ShouldStamp; }
            set { ShouldStamp = value; }
        }
        /// <summary>
        /// 图框左下角坐标
        /// </summary>
        public Point3d MinPt
        {
            get { return MinPoints; }
            set { MinPoints = value; }
        }
        /// <summary>
        /// 图框右上角坐标
        /// </summary>
        public Point3d MaxPt
        {
            get { return MaxPoints; }
            set { MaxPoints = value; }
        }
        /// <summary>
        /// 是否打印到文件
        /// </summary>
        public bool PlotToFile
        {
            get { return Plt2File; }
            set { Plt2File = value; }
        }
        /// <summary>
        /// 输出的pdf文件
        /// </summary>
        public string PlotFileLocation
        {
            get { return PltFileName; }
            set { PltFileName = value; }
        }

        public bool ChangeLinetypeScale
        {
            get { return CurLtSc; }
            set { CurLtSc = value; }
        }

        public double LinetypeScaleModel
        {
            get { return LtScModel; }
            set { LtScModel = value; }
        }

        public double LinetypeScalePaper
        {
            get { return LtScPaper; }
            set { LtScPaper = value; }
        }

        public bool TurnOnViewports
        {
            get { return VpOn; }
            set { VpOn = value; }
        }

        public string[] LayoutsToPlot
        {
            get { return LoNames; }
            set { LoNames = value; }
        }
    }
	
}
