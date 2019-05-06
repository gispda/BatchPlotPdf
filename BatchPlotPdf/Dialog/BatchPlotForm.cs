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
using Db = Autodesk.AutoCAD.DatabaseServices;
using Us = Autodesk.AutoCAD.DatabaseServices.SymbolUtilityServices;
using PCM = Autodesk.AutoCAD.PlottingServices.PlotConfigManager;
using HomeDesignCad.Plot.Util;
using System.Text;
using Sys = System.Windows.Forms;

[assembly: CommandClass(typeof(HomeDesignCad.Plot.Dialog.BatchPlotForm))]

namespace HomeDesignCad.Plot.Dialog
{
	
	public class BatchPlotForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ContextMenu BpContextMenu;
        private System.Windows.Forms.ContextMenu LoTreeviewCtMenu;
        private System.Windows.Forms.Button PlotBtn;
//		private System.Windows.Forms.CheckBox PltStpChkBox;
        private System.Windows.Forms.Button CancelBtn;
		private static string[,] ScaleValueArray;
        private static HdCadPlotParams[] PlotObjectsArray;
        private static string GlbDeviceName = "PDF-XChange Printer 2012.pc3";
		private static string GlbPaper = "A1";
		private static string GlbctbFile = "G-STANDARD.ctb";
		private static string GlbScale = "1:1";
		private static string[] GlbCanonicalArray;
        private Button btnPickdrawing;
        private TreeView tvpapers;
        private Label label7;
        private ComboBox LtScPaper;
        private ComboBox LtScModel;
        private Label label6;
        private CheckBox CurCkbox;
        private CheckBox SelLoCkbox;
        private CheckBox Lo1Ckbox;
        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox tbprogname;
        private GroupBox groupBox4;
        private TextBox tbpdfname;
        private GroupBox groupBox3;
        private TextBox tbdrawingname;
        private ComboBox cmbprogtype;
        private Button btnapply;

        private int papercount;
        private Button btnDir;
        private TextBox tbplotdir;
        private FolderBrowserDialog folderPlotDlg;
        private string oldpdfname;
  
		private string PlotDate = DateTime.Now.Date.ToShortDateString();
		public BatchPlotForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            papercount = 0;
        
           
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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.BpContextMenu = new System.Windows.Forms.ContextMenu();
            this.LoTreeviewCtMenu = new System.Windows.Forms.ContextMenu();
            this.PlotBtn = new System.Windows.Forms.Button();
            this.btnPickdrawing = new System.Windows.Forms.Button();
            this.tvpapers = new System.Windows.Forms.TreeView();
            this.label7 = new System.Windows.Forms.Label();
            this.LtScPaper = new System.Windows.Forms.ComboBox();
            this.LtScModel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CurCkbox = new System.Windows.Forms.CheckBox();
            this.SelLoCkbox = new System.Windows.Forms.CheckBox();
            this.Lo1Ckbox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbplotdir = new System.Windows.Forms.TextBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnapply = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbpdfname = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbdrawingname = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbprogname = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbprogtype = new System.Windows.Forms.ComboBox();
            this.folderPlotDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.BackColor = System.Drawing.Color.Silver;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.ForeColor = System.Drawing.Color.Navy;
            this.CancelBtn.Location = new System.Drawing.Point(722, 399);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(90, 25);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
            // 
            // BpContextMenu
            // 
            this.BpContextMenu.Popup += new System.EventHandler(this.DrawingListPopUp);
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
            this.PlotBtn.Enabled = false;
            this.PlotBtn.ForeColor = System.Drawing.Color.Navy;
            this.PlotBtn.Location = new System.Drawing.Point(566, 399);
            this.PlotBtn.Name = "PlotBtn";
            this.PlotBtn.Size = new System.Drawing.Size(90, 25);
            this.PlotBtn.TabIndex = 3;
            this.PlotBtn.Text = "打印";
            this.PlotBtn.UseVisualStyleBackColor = false;
            this.PlotBtn.Click += new System.EventHandler(this.PlotBtnClick);
            // 
            // btnPickdrawing
            // 
            this.btnPickdrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickdrawing.BackColor = System.Drawing.Color.Silver;
            this.btnPickdrawing.Enabled = false;
            this.btnPickdrawing.ForeColor = System.Drawing.Color.Navy;
            this.btnPickdrawing.Location = new System.Drawing.Point(329, 399);
            this.btnPickdrawing.Name = "btnPickdrawing";
            this.btnPickdrawing.Size = new System.Drawing.Size(150, 25);
            this.btnPickdrawing.TabIndex = 10;
            this.btnPickdrawing.Text = "选择打印图";
            this.btnPickdrawing.UseVisualStyleBackColor = false;
            this.btnPickdrawing.Click += new System.EventHandler(this.btnPickdrawing_Click);
            // 
            // tvpapers
            // 
            this.tvpapers.Location = new System.Drawing.Point(-2, -1);
            this.tvpapers.Name = "tvpapers";
            this.tvpapers.Size = new System.Drawing.Size(305, 442);
            this.tvpapers.TabIndex = 11;
            this.tvpapers.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvpapers_NodeMouseDoubleClick);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(230, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 23);
            this.label7.TabIndex = 8;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(86, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 8;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.tbplotdir);
            this.panel1.Controls.Add(this.btnDir);
            this.panel1.Controls.Add(this.btnapply);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(300, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 380);
            this.panel1.TabIndex = 12;
            // 
            // tbplotdir
            // 
            this.tbplotdir.Location = new System.Drawing.Point(149, 338);
            this.tbplotdir.Name = "tbplotdir";
            this.tbplotdir.Size = new System.Drawing.Size(363, 21);
            this.tbplotdir.TabIndex = 13;
            this.tbplotdir.ModifiedChanged += new System.EventHandler(this.tbplotdir_ModifiedChanged);
            this.tbplotdir.TextChanged += new System.EventHandler(this.tbplotdir_TextChanged);
            // 
            // btnDir
            // 
            this.btnDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDir.BackColor = System.Drawing.Color.Silver;
            this.btnDir.ForeColor = System.Drawing.Color.Navy;
            this.btnDir.Location = new System.Drawing.Point(28, 338);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(90, 25);
            this.btnDir.TabIndex = 12;
            this.btnDir.Text = "选择保存目录";
            this.btnDir.UseVisualStyleBackColor = false;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnapply
            // 
            this.btnapply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnapply.BackColor = System.Drawing.Color.Silver;
            this.btnapply.ForeColor = System.Drawing.Color.Navy;
            this.btnapply.Location = new System.Drawing.Point(28, 251);
            this.btnapply.Name = "btnapply";
            this.btnapply.Size = new System.Drawing.Size(93, 25);
            this.btnapply.TabIndex = 11;
            this.btnapply.Text = "应用";
            this.btnapply.UseVisualStyleBackColor = false;
            this.btnapply.Click += new System.EventHandler(this.btnapply_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbpdfname);
            this.groupBox4.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox4.Location = new System.Drawing.Point(2, 181);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(510, 55);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "pdf文件名";
            // 
            // tbpdfname
            // 
            this.tbpdfname.Location = new System.Drawing.Point(26, 21);
            this.tbpdfname.Name = "tbpdfname";
            this.tbpdfname.Size = new System.Drawing.Size(454, 21);
            this.tbpdfname.TabIndex = 0;
            this.tbpdfname.Click += new System.EventHandler(this.tbpdfname_Click);
            this.tbpdfname.TextChanged += new System.EventHandler(this.tbpdfname_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbdrawingname);
            this.groupBox3.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox3.Location = new System.Drawing.Point(3, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 55);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图名";
            // 
            // tbdrawingname
            // 
            this.tbdrawingname.Location = new System.Drawing.Point(26, 21);
            this.tbdrawingname.Name = "tbdrawingname";
            this.tbdrawingname.Size = new System.Drawing.Size(454, 21);
            this.tbdrawingname.TabIndex = 0;
            this.tbdrawingname.Text = "一层平面图";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbprogname);
            this.groupBox2.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox2.Location = new System.Drawing.Point(3, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(510, 55);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工程名称";
            // 
            // tbprogname
            // 
            this.tbprogname.Location = new System.Drawing.Point(26, 21);
            this.tbprogname.Name = "tbprogname";
            this.tbprogname.Size = new System.Drawing.Size(454, 21);
            this.tbprogname.TabIndex = 0;
            this.tbprogname.Text = "景茂.誉府一、二、三期";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbprogtype);
            this.groupBox1.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工程种类";
            // 
            // cmbprogtype
            // 
            this.cmbprogtype.FormattingEnabled = true;
            this.cmbprogtype.Items.AddRange(new object[] {
            "幕施",
            "家装"});
            this.cmbprogtype.Location = new System.Drawing.Point(26, 21);
            this.cmbprogtype.Name = "cmbprogtype";
            this.cmbprogtype.Size = new System.Drawing.Size(453, 20);
            this.cmbprogtype.TabIndex = 0;
            this.cmbprogtype.Text = "幕施";
            // 
            // BatchPlotForm
            // 
            this.AcceptButton = this.PlotBtn;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(825, 436);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvpapers);
            this.Controls.Add(this.btnPickdrawing);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.PlotBtn);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		void MyPlotLoad(object sender, System.EventArgs e)
		{
            PdfUtil.Init();

            string projtype = PdfUtil.getEngineering();

            if (projtype != "")
                this.cmbprogtype.Text = projtype;
            string projname = PdfUtil.getProject();
            if (projname != "")
                this.tbprogname.Text = projname;

            string plotdir = PdfUtil.getPlotDir();
            if (plotdir != "")
            {
                this.tbplotdir.Text = plotdir;
                this.btnPickdrawing.Enabled = true;
                this.PlotBtn.Enabled = true;
            }

            //string tempStr;
            //bool tempTest = false;
            //PlotConfigInfoCollection PCIC = PCM.Devices;
            //foreach (PlotConfigInfo pci in PCIC) {
            //    //PlotterComboBox.Items.Add(pci.DeviceName);
            //    if (string.Compare(pci.DeviceName, GlbDeviceName) == 0) tempTest = true;
            //}
            //if (tempTest) {
            //    //PlotterComboBox.Text = GlbDeviceName;
            //    //PaperComboBox.Text = GlbPaper;
            //    tempTest = false;
            //}
            //else {
            //    PlotterComboBox.Text = PlotterComboBox.Items[0].ToString();
            //    UpdatePaperListbox(PlotterComboBox.Items[0].ToString());
            //}
            //StringCollection ctbNames = PCM.ColorDependentPlotStyles;
            //foreach (string str in ctbNames) {
            //    string[] tempStrArray = str.Split(new char[] {'\\'});
            //    //ctbFileComboBox.Items.Add(tempStrArray[tempStrArray.Length - 1]);
            //    if (string.Compare(tempStrArray[tempStrArray.Length - 1], GlbctbFile) == 0) tempTest = true;
            //}
            //if (tempTest) {
            //    //ctbFileComboBox.Text = GlbctbFile;
            //    tempTest = false;
            //}
            //else ctbFileComboBox.Text = ctbFileComboBox.Items[0].ToString();
            //Type Scales = typeof(StdScaleType);
            //string[] ScaleArray = Enum.GetNames(Scales);
            //int i = 0;
            //ScaleValueArray = new string[ScaleArray.Length, 2];
            //foreach (string str in Enum.GetNames(Scales)) {
            //    tempStr = FormatStandardScale(str);
            //    ScaleComboBox.Items.Add(tempStr);
            //    ScaleValueArray[i, 0] = tempStr;
            //    ScaleValueArray[i, 1] = str;
            //    ++i;
            //    if (string.Compare(tempStr, GlbScale) == 0) tempTest = true;
            //}
            //if (tempTest) {
            //    ScaleComboBox.Text = GlbScale;
            //}
            //else ScaleComboBox.Text = ScaleComboBox.Items[0].ToString();

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
		
		}
		
		void DeviceNameChanged (object sender, System.EventArgs e)
		{
			
		}
		
		void CancelBtnClick(object sender, System.EventArgs e)
		{
			Close();
		}
		
		void PlotBtnClick(object sender, System.EventArgs e)
		{


			//DrawingListView.Sort();			GlbDeviceName = PlotterComboBox.Text;
		//	GlbPaper = PaperComboBox.Text;
			//GlbctbFile = ctbFileComboBox.Text;
		//	GlbScale = ScaleComboBox.Text;
		//	ListView.ListViewItemCollection lvic = DrawingListView.Items;
            //PlotObjectsArray = new HdCadPlotParams[lvic.Count];
            //for (int i = 0; i < lvic.Count; ++i) {
            //    Log4NetHelper.WriteInfoLog(lvic[i].Tag+"\n");
            //    PlotObjectsArray[i] = lvic[i].Tag as HdCadPlotParams;
            //}
            if(cmbprogtype.Text!="")
            PdfUtil.setEngineering(cmbprogtype.Text);
            if (tbprogname.Text != "")
            PdfUtil.setProject(tbprogname.Text);
            if (tbplotdir.Text != "")
            PdfUtil.setPlotDir(tbplotdir.Text);
            PdfUtil.writeIniData();
			this.Close();
		}
		
		void SelectDrawingsBtnClick(object sender, System.EventArgs e)
		{
		
		}
		
	
		
		void DrawingListPopUp (object sender, System.EventArgs e)
		{
			System.Windows.Forms.Menu.MenuItemCollection mic = BpContextMenu.MenuItems;
			mic.Clear();
			System.Windows.Forms.MenuItem mi1 = new System.Windows.Forms.MenuItem("&Apply settings");
			
			mic.Add(mi1);
			
			System.Windows.Forms.MenuItem mi2 = new System.Windows.Forms.MenuItem("&Remove settings");
			
			mic.Add(mi2);
			
			System.Windows.Forms.MenuItem mi3 = new System.Windows.Forms.MenuItem("&View settings");
			
			mic.Add(mi3);
			
		}
        private static Db.Layout GetCurrentLayout(Database db, Db.Transaction tr)
        {
            Db.ObjectId modelId = Us.GetBlockModelSpaceId(db);
            Db.BlockTableRecord model = tr.GetObject(modelId,
            Db.OpenMode.ForRead) as Db.BlockTableRecord;
            Db.Layout layout = tr.GetObject(model.LayoutId,
Db.OpenMode.ForRead) as Db.Layout;
            return layout;
        }		
		public void MyPlottingPart (HdCadPlotParams PltParams, bool IsModel) {
			object OldBkGdPlt = cad.GetSystemVariable("BackGroundPlot");
			cad.SetSystemVariable("BackGroundPlot", 0);
			Database db = HostApplicationServices.WorkingDatabase;
            Db.Extents2d extents;


            
            using (Db.Transaction tr = db.TransactionManager.StartTransaction())
            {
                PlotInfo PltInfo = new PlotInfo();


                Db.ObjectId modelId = Us.GetBlockModelSpaceId(db);
                Db.BlockTableRecord model = tr.GetObject(modelId,
                Db.OpenMode.ForRead) as Db.BlockTableRecord;

                Db.Layout layout = tr.GetObject(model.LayoutId,
                Db.OpenMode.ForWrite) as Db.Layout;

                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead, false) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite, false) as BlockTableRecord;
                Circle acCirc = null;


                Point3d pcen;
                PlotSettings PltSet = new PlotSettings(IsModel);
                PltSet.CopyFrom(layout);
                PltInfo.Layout = model.LayoutId;
                //PltInfo.Layout = LayoutManager.Current.GetLayoutId(LayoutManager.Current.CurrentLayout);
                PlotSettingsValidator PltSetVald = PlotSettingsValidator.Current;
                PlotPageInfo PltPgInfo = new PlotPageInfo();
                PlotProgressDialog PltPrgDia = new PlotProgressDialog(false, 1, true);
                PlotEngine PltEng = PlotFactory.CreatePublishEngine();
               // PCM.SetCurrentConfig(PltParams.Device);
               
                PlotConfig pc = PCM.SetCurrentConfig(PltParams.Device);
                PCM.RefreshList(RefreshCode.All);
                try
                {


                    /////////////////
                    Log4NetHelper.WriteInfoLog("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\n");


                    //PlotConfigManager.RefreshList(RefreshCode.RefreshDevicesList);
                    //foreach(PlotConfigInfo pd in PlotConfigManager.Devices)
                    //{
                    //    Log4NetHelper.WriteInfoLog("\n"+pd.DeviceType+":"+pd.DeviceName+","+":"+pd.FullPath);
                    //}
                    //PltSetVald.RefreshLists(PltSet);
                    //Log4NetHelper.WriteInfoLog("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbb\n");
                    //PltSetVald.SetCurrentStyleSheet(PltSet, PltParams.ctbFile);
                    PltSetVald.SetCurrentStyleSheet(layout, PltParams.ctbFile);
                    //PltSetVald.SetCanonicalMediaName
                        
                    //Log4NetHelper.WriteInfoLog("ccccccccccccccccccccccccccccccccc\n");
                    //PltSetVald.SetPlotOrigin(PltSet, new Point2d(0.0, 0.0));
                    //PltSetVald.SetPlotPaperUnits(PltSet, PlotPaperUnit.Millimeters);
                    //PltSetVald.SetPlotType(PltSet, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                    //Log4NetHelper.WriteInfoLog("ddddddddddddddddddddddddddddddddd\n");
                    ////////////////////////////////////////////////

                    //PltSetVald.SetPlotRotation(PltSet, PltParams.AcPlotRotation);

                    //Log4NetHelper.WriteInfoLog("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee\n");

                    //PltSetVald.SetUseStandardScale(PltSet, true);
                    //Log4NetHelper.WriteInfoLog("fffffffffffffffffffffffffffffffff\n");
                    //PltSetVald.SetStdScaleType(PltSet, PltParams.AcScaleType);
                    //Log4NetHelper.WriteInfoLog("gggggggggggggggggggggggggggggggggg\n");
                    //PltSetVald.SetPlotCentered(PltSet, true);
                    //Log4NetHelper.WriteInfoLog("hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh\n");
                    //PltSet.ScaleLineweights = PltParams.ScaleLineweight;
                    //Log4NetHelper.WriteInfoLog("iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii\n");
                    //PltSetVald.SetZoomToPaperOnUpdate(PltSet, false);
                    extents = GetPlotRegion(PltParams);

                    Log4NetHelper.WriteInfoLog("左下角坐标:" + extents.MinPoint.X + "," + extents.MinPoint.Y + "\n");
                    Log4NetHelper.WriteInfoLog("右上角角坐标:" + extents.MaxPoint.X + "," + extents.MaxPoint.Y + "\n");
                    if (PltParams.IsFindPaper == false)
                    {
                        Log4NetHelper.WriteInfoLog("开始画红色圆\n");
                        acCirc = new Circle();
                        pcen = new Point3d(PltParams.MaxPt.X, PltParams.MinPt.Y, 0);

                        acCirc.Center = pcen;
                        acCirc.Radius = 800;
                        acCirc.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 0, 0); ;
                        // Add the new object to the block table record and the transaction
                        btr.AppendEntity(acCirc);

                        tr.AddNewlyCreatedDBObject(acCirc, true);
                    }
                    else
                    {
                        Log4NetHelper.WriteInfoLog("开始准备打印参数\n");
                        PltSetVald.SetZoomToPaperOnUpdate(PltSet, true);
                       // Log4NetHelper.WriteInfoLog("开始准备打印参数111111111111\n");
                        PltSetVald.SetPlotWindowArea(PltSet, extents);
                      ///  Log4NetHelper.WriteInfoLog("开始准备打印参数22222222222222\n");
                        PltSetVald.SetPlotType(PltSet, Db.PlotType.Window);
                     //   Log4NetHelper.WriteInfoLog("开始准备打印参数3333333333333333\n");
                        PltSetVald.SetUseStandardScale(PltSet, true);
                    //    Log4NetHelper.WriteInfoLog("开始准备打印参数4444444444444444\n");
                        PltSetVald.SetStdScaleType(PltSet, Db.StdScaleType.ScaleToFit);
                    //    Log4NetHelper.WriteInfoLog("开始准备打印参数555555555555555555555555\n");
                        PltSetVald.SetPlotCentered(PltSet, true);
                    //    Log4NetHelper.WriteInfoLog("开始准备打印参数666666666666666666\n");
                        PltSetVald.SetPlotRotation(PltSet, Db.PlotRotation.Degrees000);
                     //   Log4NetHelper.WriteInfoLog("开始准备打印参数77777777777777777\n");
                        // We'll use the standard DWF PC3, as
                        // for today we're just plotting to file
                        Log4NetHelper.WriteInfoLog("Device is "+ PltParams.Device+ "\n");
                        Log4NetHelper.WriteInfoLog("CanonicalPaper is " + PltParams.CanonicalPaper + "\n");

                        PltSetVald.SetPlotConfigurationName(PltSet, PltParams.Device, null);
                        PltSetVald.RefreshLists(PltSet);


                        StringCollection sl = PltSetVald.GetCanonicalMediaNameList(PltSet);

                        foreach (string s in sl)
                       {
                            Log4NetHelper.WriteInfoLog("开始准备打印参数"+s+"\n");
                           
                       }

                        PltSetVald.SetPlotConfigurationName(PltSet, PltParams.Device, PltParams.CanonicalPaper);

                        Log4NetHelper.WriteInfoLog("jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj\n");
                        PltInfo.OverrideSettings = PltSet;
                        PltInfo.DeviceOverride = pc;
                    
                        Log4NetHelper.WriteInfoLog("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk\n");
                        PlotInfoValidator PltInfoVald = new PlotInfoValidator();
                        Log4NetHelper.WriteInfoLog("lllllllllllllllllllllllllllllllllllll\n");
                        PltInfoVald.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                        Log4NetHelper.WriteInfoLog("mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm\n");
                        PltInfoVald.Validate(PltInfo);
                        Log4NetHelper.WriteInfoLog("nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn\n");
                        PltPrgDia.OnBeginPlot();
                        Log4NetHelper.WriteInfoLog("oooooooooooooooooooooooooooooooooooooo\n");
                        PltPrgDia.IsVisible = true;
                        PltEng.BeginPlot(PltPrgDia, null);
                        Log4NetHelper.WriteInfoLog("pppppppppppppppppppppppppppppppppppppppp\n");


                        PltEng.BeginDocument(PltInfo, db.Filename, null, PltParams.Amount, true, PltParams.PlotFileLocation);

                        Log4NetHelper.WriteInfoLog("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq\n");

                        //if (PltParams.PlotToFile)
                        //{
                        //    PltEng.BeginDocument(PltInfo, db.Filename, null, PltParams.Amount, true, PltParams.PlotFileLocation);
                        //    Log4NetHelper.WriteInfoLog("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq\n");
                        //}
                        //else
                        //{
                        //    PltEng.BeginDocument(PltInfo, db.Filename, null, PltParams.Amount, false, string.Empty);
                        //    Log4NetHelper.WriteInfoLog("rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr\n");
                        //}
                        PltEng.BeginPage(PltPgInfo, PltInfo, true, null);
                        Log4NetHelper.WriteInfoLog("ssssssssssssssssssssssssssssssssssssssss\n");
                        PltEng.BeginGenerateGraphics(null);
                        Log4NetHelper.WriteInfoLog("ttttttttttttttttttttttttttttttttttttttttt\n");
                        PltEng.EndGenerateGraphics(null);
                        Log4NetHelper.WriteInfoLog("uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu\n");
                        PltEng.EndPage(null);
                        Log4NetHelper.WriteInfoLog("vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv\n");
                        PltEng.EndDocument(null);
                        Log4NetHelper.WriteInfoLog("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww\n");
                        PltEng.EndPlot(null);
                        Log4NetHelper.WriteInfoLog("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\n");
                        PltPrgDia.OnEndPlot();
                        Log4NetHelper.WriteInfoLog("yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy\n");
                    }
                }
                catch (Autodesk.AutoCAD.Runtime.Exception AcadEr)
                {
                    MessageBox.Show(AcadEr.Message, "Printing error (AutoCAD).");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Printing error (System).");
                }
                finally
                { 
                 
                }
                PltPrgDia.Destroy();
                Log4NetHelper.WriteInfoLog("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz\n");
                PltEng.Destroy();
                Log4NetHelper.WriteInfoLog("00000000000000000000000000000000000000\n");
                cad.SetSystemVariable("BackGroundPlot", OldBkGdPlt);
                Log4NetHelper.WriteInfoLog("11111111111111111111111111111111111\n");
                tr.Commit();
            }
		}

        private Extents2d GetPlotRegion(HdCadPlotParams PltParams)
        {
            Db.Extents2d extents;

            double xmin, ymin, xmax, ymax;
            double xpmin, ypmin;
            double xpmax, ypmax;
            xmin = PltParams.MinPt.X;
            ymin = PltParams.MinPt.Y;
            xmax = PltParams.MaxPt.X;
            ymax = PltParams.MaxPt.Y;

            xpmax = 0;
            ypmax = 0;
            if (PltParams.IsRotate == true)
            {
                xpmin = (xmax - xmin) / (50 * PltParams.PaperScale);
                ypmin = (ymax - ymin) / (100 * PltParams.PaperScale);
            }
            else
            {
                xpmin = (xmax - xmin) / (70 * PltParams.PaperScale);
                ypmin = (ymax - ymin) / (50 * PltParams.PaperScale);
            
            }

            xpmax = 0.5 * xpmin;
            ypmax = 0.5 * ypmin;

            extents = new Db.Extents2d(
                            xmin-xpmin,
                            ymin-ypmin,
                            xmax-xpmax,
                            ymax-ypmax
                          );
            return extents;
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
                int pii = 0;
                Editor ed = GetActiveDoc().Editor;

				foreach (HdCadPlotParams mpp in PlotObjectsArray) {
                    Log4NetHelper.WriteInfoLog("222222222222222222222222222\n");
                    if (mpp.IsFindPaper == true)
                        pii = pii + 1;
					if (mpp != null) {
						try {
                            Log4NetHelper.WriteInfoLog("33333333333333333333333\n");
							//tempDoc = DocCol.Open(mpp.DrawingPath, true);
                            tempDoc = GetActiveDoc();

                            if(tempDoc==null)
                            Log4NetHelper.WriteInfoLog("44444444444444444444444\n");
                            else
                                Log4NetHelper.WriteInfoLog("已经打开cad dwg file\n");
                            Database tempDb = GetCurrentDb(tempDoc);

							LayoutManager tempLoMan = LayoutManager.Current;
							using (DocumentLock DocLock = tempDoc.LockDocument()) {
                                Log4NetHelper.WriteInfoLog("555555555555555555555555\n");

                                //if (mpp.ApplyStamp) AddPlotText(tempDb, tempLoMan.GetLayoutId(tempLoMan.CurrentLayout));
                                    Log4NetHelper.WriteInfoLog("6666666666666666666666666666666\n");
                                    MyPlottingPart(mpp, CheckSpaceSetLtScale(tempLoMan.CurrentLayout, mpp));
                                    Log4NetHelper.WriteInfoLog("777777777777777777777777777777777\n");

                                //if (mpp.TurnOnViewports) TurnOnViewports(tempDoc, tempDb);
                                //if (mpp.PlotCurrentLayout) {
                                //    if (mpp.ApplyStamp) AddPlotText(tempDb, tempLoMan.GetLayoutId(tempLoMan.CurrentLayout));
                                //    Log4NetHelper.WriteInfoLog("6666666666666666666666666666666\n");
                                //    MyPlottingPart(mpp, CheckSpaceSetLtScale(tempLoMan.CurrentLayout, mpp));
                                //    Log4NetHelper.WriteInfoLog("777777777777777777777777777777777\n");
                                //}
                                //else if (!mpp.LayoutsToPlot.Length.Equals(0)) {

                                //    Log4NetHelper.WriteInfoLog("8888888888888888888888888888\n");
                                //    foreach (string LoName in mpp.LayoutsToPlot) {
                                //        try {

                                //            Log4NetHelper.WriteInfoLog("9999999999999999999999999999999999999\n");
                                //            tempLoMan.CurrentLayout = LoName;
                                //            if (mpp.ApplyStamp) AddPlotText(tempDb, tempLoMan.GetLayoutId(tempLoMan.CurrentLayout));
                                //            Log4NetHelper.WriteInfoLog("000000000000000000000000000000000000000\n");
                                //            MyPlottingPart(mpp, CheckSpaceSetLtScale(LoName, mpp));
                                //        }
                                //        catch {}
                                //    }
                                //}
							}
						}
						catch (Autodesk.AutoCAD.Runtime.Exception AcadEr) {
							MessageBox.Show(AcadEr.Message, "Drawing error (AutoCAD).");
						}
						catch (System.Exception ex){
							MessageBox.Show(ex.Message, "Drawing error (System).");
						}
						finally {


                            Log4NetHelper.WriteInfoLog("最后清理....................\n");
							//if (tempDoc != null) tempDoc.CloseAndDiscard();
						}
					}
				}

                ed.WriteMessage("共打印了"+pii+"份pdf文件.\n");

			}
			try {
				//Array.Clear(ScaleValueArray, 0, ScaleValueArray.Length);
                for (int i = 0; i < papercount;i++ )
                {
                    PlotObjectsArray[i] = null;
                   
                }

				Array.Clear(PlotObjectsArray, 0, PlotObjectsArray.Length);

			}
			catch {}
		}
		
		void FormResized (object sender, System.EventArgs e)
		{
		//	ListView.ColumnHeaderCollection HdrCol = DrawingListView.Columns;
            //int Wth = DrawingListView.Width;
            //if (Wth < 364) HdrCol[1].Width = 60;
            //else HdrCol[0].Width = Wth - 64;
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
		
		}
		
		void PlotDirBtnClick(object sender, System.EventArgs e)
		{
			
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
			
		}
		
		void LtScChkBoxCheckedChanged(object sender, System.EventArgs e)
		{
		
		}
		
		void LoListBtnClick(object sender, System.EventArgs e)
		{
		
		}
		
		void LoTreeviewPopUp(object sender, System.EventArgs e) {
			
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
        private string GetBlockAttribute(BlockReference br, string attributename, Transaction tr)
        {
            string svalue = null;

            Db.AttributeCollection attCol =

            br.AttributeCollection;

            foreach (ObjectId attId in attCol)
            {

                AttributeReference attRef =

                  (AttributeReference)tr.GetObject(attId,

                    OpenMode.ForRead);


                string str =

                  ("\n  Attribute Tag: "

                    + attRef.Tag

                    + "\n    Attribute String: "

                    + attRef.TextString

                  );
                if (attRef.Tag.CompareTo(attributename) == 0)
                {
                    svalue = attRef.TextString;
                    Log4NetHelper.WriteInfoLog("找到属性值" + svalue);
                    break;
                }
                else
               Log4NetHelper.WriteInfoLog(str);

            }
            return svalue;
        }
        private void AddTreePdf(string pdfname)
        {
            tvpapers.Nodes.Add(pdfname);

        
        }
        private bool GetPdfname(BlockReference br, out string pdfname,Transaction tr)
        {
            bool isfind = true;
            StringBuilder spdfname =new StringBuilder();
            spdfname.Append(this.cmbprogtype.Text);
            spdfname.Append("-");
            spdfname.Append(this.tbprogname.Text);
            spdfname.Append("-");
            string avalue = GetBlockAttribute(br, "001", tr);
            if (avalue != null)
            {
                //this.tbdrawingname.Text = avalue;

                spdfname.Append(avalue);
                
            }
            else
            {
                isfind = false;
                spdfname.Append("自定义下");
            }
            avalue = GetBlockAttribute(br, "一层平面图", tr);
            if (avalue != null)
            {
                this.tbdrawingname.Text = avalue;


                
                spdfname.Append("-");
                spdfname.Append(avalue);
            }
            else
            {
                spdfname.Append("-");
                spdfname.Append("一层平面图");
            }
            spdfname.Append(".pdf");

            AddTreePdf(spdfname.ToString());
            pdfname = spdfname.ToString();

            PdfUtil.addPdfAttribDict(spdfname.ToString(), avalue);
            return isfind;
           // return spdfname.ToString();
        }

        
        /// <summary>
        /// 拉框选择打印图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPickdrawing_Click(object sender, EventArgs e)
        {

            Document doc = GetActiveDoc();

            Editor ed = doc.Editor;


            Database db = GetCurrentDb(doc);

            PdfUtil.clearPdfDict();

            string avalue = null;
            double pscale = 1;
            Transaction tr =

              db.TransactionManager.StartTransaction();     

            //var ed = cad.DocumentManager.MdiActiveDocument.Editor;
            Extents3d extents;
            using (var eduserinteraction = ed.StartUserInteraction(this.Handle))
            {
                string blockName = "k1";
                //TypedValue[] tvs = new TypedValue[] { new TypedValue(0, "INSERT"), new TypedValue(2, blockName) };

               TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.Start, "INSERT") };

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
                 
                       
                            //if (res != null)
                            //    Log4NetHelper.WriteInfoLog(res.Status+"ffffffffffffffffffff");
                            //else
                            //    Log4NetHelper.WriteInfoLog("wwwwwwwwwwwwwwwwwwwwwwwwww");
                            //Log4NetHelper.WriteInfoLog("PromptStatus.OK" + PromptStatus.OK);
                            if (res.Status != PromptStatus.OK)
                                return;
                           // Log4NetHelper.WriteInfoLog("选择的实体状态不正常\n");
                       
                        SelectionSet sset = res.Value;




                        //Log4NetHelper.WriteInfoLog("sfsdfdsfdsfsdfsdfsdfsdfsdfsdfsdf\n");
                        if (sset.Count == 0)
                            return;
                        else
                            Log4NetHelper.WriteInfoLog("选择的实体为"+sset.Count+"\n");
                        BlockReference br = null;
                        string svalue = null;

                        //SelectionSet bset;

                        ObjectId[] objids = sset.GetObjectIds();

                        ObjectIdCollection aselobjids = new ObjectIdCollection();
                        foreach (ObjectId x in objids)
                        {
                           // br = GetBlockReference(obj, tr);
                            br = (BlockReference)tr.GetObject(x, OpenMode.ForRead);
                           svalue = this.GetBlockAttribute(br, "001", tr);
                           if (svalue != null)
                           {

                               aselobjids.Add(x);
                               Log4NetHelper.WriteInfoLog("复制objids"+x+"\n");
                           }

                        }


                        ObjectId[] aobjids = new ObjectId[aselobjids.Count];
                        int i=0;
                        foreach (ObjectId x in aselobjids)
                        {
                            aobjids[i] = x;
                            i = i+1;
                        }
                        SelectionSet aset = SelectionSet.FromObjectIds(aobjids);
                        Log4NetHelper.WriteInfoLog("cccccccccccc3333333333333333333\n");
                        PlotObjectsArray = new HdCadPlotParams[aset.Count];

                        papercount = aset.Count;

                        int ppi = 0;
                        string paperparams;
                        bool isfind = true;
                      
                //        Dim bt As BlockTable = tm.GetObject(db.BlockTableId, OpenMode.ForRead, False)
                //Dim btr As BlockTableRecord = tm.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite, False)
                //Circid = btr.AppendEntity(pcirc)
                //tm.AddNewlyCreatedDBObject(pcirc, True)
                        string pdfname = null;
                        foreach (SelectedObject obj in aset)
                        {

                            ppi=ppi+1;

                           
                            // ed.WriteMessage("\nhas data");
                            br = GetBlockReference(obj, tr);


                   
                           // hacadpp = new HdCadPlotParams();
                            PlotObjectsArray[ppi - 1] = new HdCadPlotParams();
                            Log4NetHelper.WriteInfoLog("22222222222222sdfdsfsdfsdfsdf\n");
                           
                            //Log4NetHelper.WriteInfoLog(br+"\n");


                            //try
                            //{

                            //    extents = br.GeometricExtents;
                            //    //PlotObjectsArray[ppi - 1].MinPt = br.GeometricExtents.MinPoint;
                            //    ////hacadpp.MinPt = br.GeometricExtents.MinPoint.Y * (1 - 0.0001);
                            //    //Log4NetHelper.WriteInfoLog(br.GeometricExtents.MinPoint + "\n");

                            //    //PlotObjectsArray[ppi - 1].MaxPt = br.GeometricExtents.MaxPoint;
                            //    //Log4NetHelper.WriteInfoLog(br.GeometricExtents.MaxPoint + "\n");
                            //}
                            //catch (Autodesk.AutoCAD.Runtime.Exception ex)
                            //{
                              
                            //}
                            extents = br.GeometryExtentsBestFit();

                             PlotObjectsArray[ppi - 1].MinPt = extents.MinPoint;
                             PlotObjectsArray[ppi - 1].MaxPt = extents.MaxPoint;

                            

                            Log4NetHelper.WriteInfoLog("333333333333333333333333sfsdfdsfsfds\n");
                            PlotObjectsArray[ppi - 1].Device = "DWG To PDF.pc3";
                            PlotObjectsArray[ppi - 1].ctbFile = "acad_幕墙.ctb";
                            Log4NetHelper.WriteInfoLog(br.Name + "\n");
                            Log4NetHelper.WriteInfoLog(PlotObjectsArray[ppi - 1].Device+"\n");
                            if ((Math.Abs(br.Rotation - Math.PI / 2) < 0.001 || (Math.Abs(br.Rotation - Math.PI * 3 / 2) < 0.001)))
                            {
                                Log4NetHelper.WriteInfoLog("图框旋转了！！！\n");
                                PlotObjectsArray[ppi - 1].IsRotate = true;
                                isfind = PdfUtil.getIPaperParamsR(br.Name, out paperparams);
                            }
                            else
                                isfind = PdfUtil.getIPaperParams(br.Name, out paperparams);
                            Log4NetHelper.WriteInfoLog("图框的旋转角度是" + br.Rotation + "\n");

                            

                            PlotObjectsArray[ppi - 1].CanonicalPaper = paperparams;
                            Log4NetHelper.WriteInfoLog("图框纸张尺寸是"+PlotObjectsArray[ppi - 1].CanonicalPaper+"\n");
                            if (isfind == false)
                            {
                                //acCirc = new Circle();
                                ////acCirc.Center.X = 
                                //btr.AppendEntity(null);
                                //tr.AddNewlyCreatedDBObject(null, true);

                                Log4NetHelper.WriteInfoLog("图框标号不对，需要手工修改下.\n");
                                PlotObjectsArray[ppi - 1].IsFindPaper = false;
                            }
                            else
                            PlotObjectsArray[ppi - 1].IsFindPaper = true;
                            Log4NetHelper.WriteInfoLog("纸张定义是："+ PlotObjectsArray[ppi - 1].CanonicalPaper + "\n");
                            //PlotObjectsArray[ppi - 1].PlotFileLocation = Convert.ToString(ppi + ".pdf");
                            if(GetPdfname(br, out pdfname, tr)==true)
                            PlotObjectsArray[ppi - 1].PlotFileLocation = tbplotdir.Text +"\\"+ pdfname;
                            else
                            {
                                Log4NetHelper.WriteInfoLog("没有001属性.\n");
                            }
                              avalue = GetBlockAttribute(br, "比例", tr);
                              if (avalue != null)
                              {
                                  string[] arr = System.Text.RegularExpressions.Regex.Split(avalue, ":");
                                  Log4NetHelper.WriteInfoLog("比例尺是"+ arr[arr.Length-1]+"\n");
                                  pscale = Convert.ToDouble(arr[arr.Length-1]);

                                  PlotObjectsArray[ppi - 1].PaperScale = pscale;
                              }
                              else
                                  Log4NetHelper.WriteInfoLog("没有比例属性.\n");
                            PdfUtil.addPdfDict(ppi - 1, PlotObjectsArray[ppi - 1].PlotFileLocation);
                           
                           // PlotObjectsArray[ii - 1] = hacadpp;
                           // Log4NetHelper.WriteInfoLog(br.BlockName + "\n");
                            Log4NetHelper.WriteInfoLog(br.Name + "\n");
                            Log4NetHelper.WriteInfoLog(PlotObjectsArray[ppi - 1] + "\n");
                           
                     

                        }

                        Log4NetHelper.WriteInfoLog("采集图框完毕!\n");
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

        private static Database GetCurrentDb(Document doc)
        {
            Database db = doc.Database;
            return db;
        }

        private static Document GetActiveDoc()
        {
            Document doc =

                cad.DocumentManager.MdiActiveDocument;
            return doc;
        }

        private BlockReference GetBlockReference(SelectedObject obj, Transaction tr)
        {
            BlockReference br = null;
            br = (BlockReference)tr.GetObject(obj.ObjectId, OpenMode.ForRead);

            return br;
        }

        private void tvpapers_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                tvpapers.SelectedNode = e.Node;
                this.tbpdfname.Text = tvpapers.SelectedNode.Text;
                this.tbdrawingname.Text = PdfUtil.getDrawingname(tbpdfname.Text);

            }
        }

        private void btnapply_Click(object sender, EventArgs e)
        {

            int keyidx = -1;
            try
            {
                if (oldpdfname == null)
                {
                    MessageBox.Show("没有修改文件名.");
                    Log4NetHelper.WriteErrorLog("没有修改文件名\n");
                }

                keyidx = PdfUtil.getidxbypdf(this.oldpdfname);

                if (keyidx != -1)
                {
                    PlotObjectsArray[keyidx].PlotFileLocation = tbplotdir.Text + "\\" + tbpdfname.Text;
                    tvpapers.SelectedNode.Text = tbpdfname.Text;
                    Log4NetHelper.WriteInfoLog("正确修改好pdf名称\n");
                }
                else
                {
                   // MessageBox.Show("需要选中左边的树节点.");
                    Log4NetHelper.WriteErrorLog("有错误，没有修改成功\n");
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
               // MessageBox.Show("需要选中左边的树节点.");
                Log4NetHelper.WriteErrorLog("需要选中左边的树节点\n");
            
            }
            
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            //Sys.OpenFileDialog dirdlg = new Sys.OpenFileDialog();
            //if (dirdlg.ShowDialog == DialogResult.OK)
            //{ 
            //    tbplotdir.Text = dirdlg
            //}
            this.folderPlotDlg.Description = "请选择打印Pdf的保存目录";
            if (folderPlotDlg.ShowDialog() == DialogResult.OK)
            {
                this.tbplotdir.Text = folderPlotDlg.SelectedPath;
                this.btnPickdrawing.Enabled = true;
                this.PlotBtn.Enabled = true;

            }
        }

        private void tbplotdir_ModifiedChanged(object sender, EventArgs e)
        {
            if (tbplotdir.Text != "")
            {
                this.btnPickdrawing.Enabled = true;
                this.PlotBtn.Enabled = true;
            }

        }

        private void tbplotdir_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbpdfname_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbpdfname_Click(object sender, EventArgs e)
        {
            this.oldpdfname = tbpdfname.Text;
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
        private double paperScale;
        private bool isRotate;

        public bool IsRotate
        {
            get { return isRotate; }
            set { isRotate = value; }
        }



        /// <summary>
        /// 1:20 记录为20
        /// </summary>
        public double PaperScale
        {
            get { return paperScale; }
            set { paperScale = value; }
        }
        private bool IsFind;

        public bool IsFindPaper
        {
            get { return IsFind; }
            set { IsFind = value; }
        }

        public HdCadPlotParams() {
            this.MinPoints = new Point3d();
            this.MaxPoints = new Point3d();
            this.paperScale = 1.0;
            isRotate = false;
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
            this.paperScale = 1.0;
            isRotate = false;
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
