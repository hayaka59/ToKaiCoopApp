
namespace TokaiCoopApp
{
    partial class ReadCollatingDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LstCollatingData = new System.Windows.Forms.ListView();
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblCollatingFile = new System.Windows.Forms.Label();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.LblWaiting = new System.Windows.Forms.Label();
            this.ListResult = new System.Windows.Forms.ListBox();
            this.ListOrderList = new System.Windows.Forms.ListBox();
            this.ListShipping = new System.Windows.Forms.ListBox();
            this.BtnTestForSeisan = new System.Windows.Forms.Button();
            this.TimTestForSeisan = new System.Windows.Forms.Timer(this.components);
            this.CmbShipping = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.BtnLastSeisanLog = new System.Windows.Forms.Button();
            this.CmbShippingRePrint = new System.Windows.Forms.ComboBox();
            this.LblStartRow = new System.Windows.Forms.Label();
            this.ChkNgProduct = new System.Windows.Forms.CheckBox();
            this.CmbDummyCheck = new System.Windows.Forms.ComboBox();
            this.PrintQrWrite = new System.Drawing.Printing.PrintDocument();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.CmbOrderRePrint = new System.Windows.Forms.ComboBox();
            this.CmbUnityTable = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LblFoldingCon = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LblMaxBundle = new System.Windows.Forms.Label();
            this.LblStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DTPDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChkDataReadFromUSB = new System.Windows.Forms.CheckBox();
            this.LblDummyCreateCounter = new System.Windows.Forms.Label();
            this.LblProgress = new System.Windows.Forms.Label();
            this.CmbPrintType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.LsvSelectiveData = new System.Windows.Forms.ListView();
            this.LblSchedule = new System.Windows.Forms.Label();
            this.LstFileContent = new System.Windows.Forms.ListBox();
            this.LblConter = new System.Windows.Forms.Label();
            this.PicWaitList = new System.Windows.Forms.PictureBox();
            this.BtnCheckNumberOfForm = new System.Windows.Forms.Button();
            this.BtnDepotOrderSetting = new System.Windows.Forms.Button();
            this.BtnBasketCarCheck = new System.Windows.Forms.Button();
            this.BtnShelfChange = new System.Windows.Forms.Button();
            this.BtnDriving = new System.Windows.Forms.Button();
            this.BtnQrWrite = new System.Windows.Forms.Button();
            this.BtnQrRead = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.BtnAdjust = new System.Windows.Forms.Button();
            this.BtnMakeControlDataFile = new System.Windows.Forms.Button();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.CmbFileType = new System.Windows.Forms.ComboBox();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicWaitList)).BeginInit();
            this.SuspendLayout();
            // 
            // LstCollatingData
            // 
            this.LstCollatingData.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstCollatingData.FullRowSelect = true;
            this.LstCollatingData.GridLines = true;
            this.LstCollatingData.HideSelection = false;
            this.LstCollatingData.Location = new System.Drawing.Point(4, 372);
            this.LstCollatingData.MultiSelect = false;
            this.LstCollatingData.Name = "LstCollatingData";
            this.LstCollatingData.Size = new System.Drawing.Size(1895, 464);
            this.LstCollatingData.TabIndex = 129;
            this.LstCollatingData.UseCompatibleStateImageBehavior = false;
            this.LstCollatingData.DoubleClick += new System.EventHandler(this.LstCollatingData_DoubleClick);
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(-1, -2);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1907, 50);
            this.LblTitle.TabIndex = 130;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblTitle.DoubleClick += new System.EventHandler(this.LblTitle_DoubleClick);
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(1737, 1012);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(150, 25);
            this.LblVersion.TabIndex = 132;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblVersion.DoubleClick += new System.EventHandler(this.LblVersion_DoubleClick);
            // 
            // LblCollatingFile
            // 
            this.LblCollatingFile.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblCollatingFile.ForeColor = System.Drawing.Color.Blue;
            this.LblCollatingFile.Location = new System.Drawing.Point(346, 119);
            this.LblCollatingFile.Name = "LblCollatingFile";
            this.LblCollatingFile.Size = new System.Drawing.Size(684, 19);
            this.LblCollatingFile.TabIndex = 134;
            this.LblCollatingFile.Text = "LblCollatingFile";
            this.LblCollatingFile.DoubleClick += new System.EventHandler(this.LblCollatingFile_DoubleClick);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ProgressBar1.Location = new System.Drawing.Point(740, 546);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(438, 43);
            this.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar1.TabIndex = 153;
            // 
            // LblWaiting
            // 
            this.LblWaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LblWaiting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblWaiting.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LblWaiting.Font = new System.Drawing.Font("メイリオ", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblWaiting.ForeColor = System.Drawing.Color.Blue;
            this.LblWaiting.Location = new System.Drawing.Point(729, 495);
            this.LblWaiting.Name = "LblWaiting";
            this.LblWaiting.Size = new System.Drawing.Size(460, 152);
            this.LblWaiting.TabIndex = 152;
            this.LblWaiting.Text = "表示処理中です";
            this.LblWaiting.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ListResult
            // 
            this.ListResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ListResult.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ListResult.FormattingEnabled = true;
            this.ListResult.HorizontalScrollbar = true;
            this.ListResult.ItemHeight = 24;
            this.ListResult.Location = new System.Drawing.Point(6, 381);
            this.ListResult.Name = "ListResult";
            this.ListResult.Size = new System.Drawing.Size(712, 460);
            this.ListResult.TabIndex = 160;
            // 
            // ListOrderList
            // 
            this.ListOrderList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ListOrderList.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ListOrderList.FormattingEnabled = true;
            this.ListOrderList.ItemHeight = 24;
            this.ListOrderList.Location = new System.Drawing.Point(724, 381);
            this.ListOrderList.Name = "ListOrderList";
            this.ListOrderList.Size = new System.Drawing.Size(397, 460);
            this.ListOrderList.TabIndex = 161;
            // 
            // ListShipping
            // 
            this.ListShipping.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ListShipping.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ListShipping.FormattingEnabled = true;
            this.ListShipping.ItemHeight = 24;
            this.ListShipping.Location = new System.Drawing.Point(1128, 381);
            this.ListShipping.Name = "ListShipping";
            this.ListShipping.Size = new System.Drawing.Size(644, 460);
            this.ListShipping.TabIndex = 162;
            // 
            // BtnTestForSeisan
            // 
            this.BtnTestForSeisan.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnTestForSeisan.ForeColor = System.Drawing.Color.Red;
            this.BtnTestForSeisan.Location = new System.Drawing.Point(1475, 1003);
            this.BtnTestForSeisan.Name = "BtnTestForSeisan";
            this.BtnTestForSeisan.Size = new System.Drawing.Size(159, 33);
            this.BtnTestForSeisan.TabIndex = 163;
            this.BtnTestForSeisan.Text = "生産ログ作成開始";
            this.BtnTestForSeisan.UseVisualStyleBackColor = true;
            this.BtnTestForSeisan.Visible = false;
            this.BtnTestForSeisan.Click += new System.EventHandler(this.BtnTestForSeisan_Click);
            // 
            // TimTestForSeisan
            // 
            this.TimTestForSeisan.Tick += new System.EventHandler(this.TimTestForSeisan_Tick);
            // 
            // CmbShipping
            // 
            this.CmbShipping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbShipping.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbShipping.FormattingEnabled = true;
            this.CmbShipping.IntegralHeight = false;
            this.CmbShipping.Location = new System.Drawing.Point(1092, 851);
            this.CmbShipping.MaxDropDownItems = 20;
            this.CmbShipping.Name = "CmbShipping";
            this.CmbShipping.Size = new System.Drawing.Size(538, 32);
            this.CmbShipping.TabIndex = 164;
            this.CmbShipping.Visible = false;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label2.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label2.Location = new System.Drawing.Point(1092, 889);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(538, 20);
            this.Label2.TabIndex = 166;
            this.Label2.Text = "束かんばんリスト　再印刷";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label2.Visible = false;
            // 
            // BtnLastSeisanLog
            // 
            this.BtnLastSeisanLog.Location = new System.Drawing.Point(1396, 1002);
            this.BtnLastSeisanLog.Name = "BtnLastSeisanLog";
            this.BtnLastSeisanLog.Size = new System.Drawing.Size(73, 34);
            this.BtnLastSeisanLog.TabIndex = 168;
            this.BtnLastSeisanLog.Text = "最終生産";
            this.BtnLastSeisanLog.UseVisualStyleBackColor = true;
            this.BtnLastSeisanLog.Visible = false;
            this.BtnLastSeisanLog.Click += new System.EventHandler(this.BtnLastSeisanLog_Click);
            // 
            // CmbShippingRePrint
            // 
            this.CmbShippingRePrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbShippingRePrint.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbShippingRePrint.FormattingEnabled = true;
            this.CmbShippingRePrint.IntegralHeight = false;
            this.CmbShippingRePrint.Location = new System.Drawing.Point(1092, 909);
            this.CmbShippingRePrint.MaxDropDownItems = 20;
            this.CmbShippingRePrint.Name = "CmbShippingRePrint";
            this.CmbShippingRePrint.Size = new System.Drawing.Size(538, 32);
            this.CmbShippingRePrint.TabIndex = 170;
            this.CmbShippingRePrint.Visible = false;
            this.CmbShippingRePrint.SelectedIndexChanged += new System.EventHandler(this.CmbShippingRePrint_SelectedIndexChanged);
            // 
            // LblStartRow
            // 
            this.LblStartRow.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblStartRow.ForeColor = System.Drawing.Color.Red;
            this.LblStartRow.Location = new System.Drawing.Point(350, 72);
            this.LblStartRow.Name = "LblStartRow";
            this.LblStartRow.Size = new System.Drawing.Size(686, 29);
            this.LblStartRow.TabIndex = 173;
            this.LblStartRow.Text = "LblStartRow";
            // 
            // ChkNgProduct
            // 
            this.ChkNgProduct.AutoSize = true;
            this.ChkNgProduct.Location = new System.Drawing.Point(1645, 1020);
            this.ChkNgProduct.Name = "ChkNgProduct";
            this.ChkNgProduct.Size = new System.Drawing.Size(66, 16);
            this.ChkNgProduct.TabIndex = 244;
            this.ChkNgProduct.Text = "ＮＧ製品";
            this.ChkNgProduct.UseVisualStyleBackColor = true;
            this.ChkNgProduct.Visible = false;
            // 
            // CmbDummyCheck
            // 
            this.CmbDummyCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDummyCheck.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbDummyCheck.FormattingEnabled = true;
            this.CmbDummyCheck.Location = new System.Drawing.Point(13, 997);
            this.CmbDummyCheck.Name = "CmbDummyCheck";
            this.CmbDummyCheck.Size = new System.Drawing.Size(287, 32);
            this.CmbDummyCheck.TabIndex = 248;
            // 
            // PrintQrWrite
            // 
            this.PrintQrWrite.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintQrWrite_PrintPage);
            // 
            // PrintDocument
            // 
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
            // 
            // CmbOrderRePrint
            // 
            this.CmbOrderRePrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbOrderRePrint.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbOrderRePrint.FormattingEnabled = true;
            this.CmbOrderRePrint.IntegralHeight = false;
            this.CmbOrderRePrint.Location = new System.Drawing.Point(821, 1003);
            this.CmbOrderRePrint.MaxDropDownItems = 15;
            this.CmbOrderRePrint.Name = "CmbOrderRePrint";
            this.CmbOrderRePrint.Size = new System.Drawing.Size(542, 32);
            this.CmbOrderRePrint.TabIndex = 259;
            this.CmbOrderRePrint.Visible = false;
            // 
            // CmbUnityTable
            // 
            this.CmbUnityTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUnityTable.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbUnityTable.FormattingEnabled = true;
            this.CmbUnityTable.Location = new System.Drawing.Point(13, 873);
            this.CmbUnityTable.Name = "CmbUnityTable";
            this.CmbUnityTable.Size = new System.Drawing.Size(287, 39);
            this.CmbUnityTable.TabIndex = 260;
            this.CmbUnityTable.SelectedIndexChanged += new System.EventHandler(this.CmbUnityTable_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.LblFoldingCon);
            this.groupBox6.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox6.ForeColor = System.Drawing.Color.Red;
            this.groupBox6.Location = new System.Drawing.Point(445, 856);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(130, 64);
            this.groupBox6.TabIndex = 262;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "カゴ車への積載数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(98, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "束";
            // 
            // LblFoldingCon
            // 
            this.LblFoldingCon.BackColor = System.Drawing.Color.LightGray;
            this.LblFoldingCon.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblFoldingCon.Location = new System.Drawing.Point(6, 25);
            this.LblFoldingCon.Name = "LblFoldingCon";
            this.LblFoldingCon.Size = new System.Drawing.Size(88, 24);
            this.LblFoldingCon.TabIndex = 2;
            this.LblFoldingCon.Text = "束";
            this.LblFoldingCon.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.LblMaxBundle);
            this.groupBox7.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(307, 856);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(130, 64);
            this.groupBox7.TabIndex = 261;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "結束最大束数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(99, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "束";
            // 
            // LblMaxBundle
            // 
            this.LblMaxBundle.BackColor = System.Drawing.Color.LightGray;
            this.LblMaxBundle.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblMaxBundle.Location = new System.Drawing.Point(6, 25);
            this.LblMaxBundle.Name = "LblMaxBundle";
            this.LblMaxBundle.Size = new System.Drawing.Size(88, 24);
            this.LblMaxBundle.TabIndex = 1;
            this.LblMaxBundle.Text = "束";
            this.LblMaxBundle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.Color.LightCoral;
            this.LblStatus.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblStatus.ForeColor = System.Drawing.Color.Blue;
            this.LblStatus.Location = new System.Drawing.Point(889, 941);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(741, 62);
            this.LblStatus.TabIndex = 263;
            this.LblStatus.Text = "LblStatus";
            this.LblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblStatus.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DTPDeliveryDate);
            this.groupBox1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(307, 923);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 64);
            this.groupBox1.TabIndex = 266;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "組合員納品日";
            // 
            // DTPDeliveryDate
            // 
            this.DTPDeliveryDate.Location = new System.Drawing.Point(6, 25);
            this.DTPDeliveryDate.Name = "DTPDeliveryDate";
            this.DTPDeliveryDate.Size = new System.Drawing.Size(165, 27);
            this.DTPDeliveryDate.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChkDataReadFromUSB);
            this.groupBox2.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(307, 981);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(181, 55);
            this.groupBox2.TabIndex = 269;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "障害時処理";
            // 
            // ChkDataReadFromUSB
            // 
            this.ChkDataReadFromUSB.AutoSize = true;
            this.ChkDataReadFromUSB.Location = new System.Drawing.Point(9, 21);
            this.ChkDataReadFromUSB.Name = "ChkDataReadFromUSB";
            this.ChkDataReadFromUSB.Size = new System.Drawing.Size(132, 24);
            this.ChkDataReadFromUSB.TabIndex = 245;
            this.ChkDataReadFromUSB.Text = "障害時データ読取";
            this.ChkDataReadFromUSB.UseVisualStyleBackColor = true;
            // 
            // LblDummyCreateCounter
            // 
            this.LblDummyCreateCounter.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblDummyCreateCounter.ForeColor = System.Drawing.Color.Red;
            this.LblDummyCreateCounter.Location = new System.Drawing.Point(513, 1003);
            this.LblDummyCreateCounter.Name = "LblDummyCreateCounter";
            this.LblDummyCreateCounter.Size = new System.Drawing.Size(203, 24);
            this.LblDummyCreateCounter.TabIndex = 4;
            this.LblDummyCreateCounter.Text = "ダミー作成数：0 件";
            // 
            // LblProgress
            // 
            this.LblProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LblProgress.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblProgress.ForeColor = System.Drawing.Color.Red;
            this.LblProgress.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.LblProgress.Location = new System.Drawing.Point(784, 597);
            this.LblProgress.Name = "LblProgress";
            this.LblProgress.Size = new System.Drawing.Size(234, 35);
            this.LblProgress.TabIndex = 270;
            this.LblProgress.Text = "LblProgress";
            this.LblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CmbPrintType
            // 
            this.CmbPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPrintType.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbPrintType.FormattingEnabled = true;
            this.CmbPrintType.Location = new System.Drawing.Point(14, 30);
            this.CmbPrintType.Name = "CmbPrintType";
            this.CmbPrintType.Size = new System.Drawing.Size(268, 39);
            this.CmbPrintType.TabIndex = 271;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CmbPrintType);
            this.groupBox3.Controls.Add(this.BtnPrint);
            this.groupBox3.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(602, 856);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(484, 85);
            this.groupBox3.TabIndex = 272;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "印刷種類";
            // 
            // BtnPrint
            // 
            this.BtnPrint.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnPrint.ForeColor = System.Drawing.Color.Black;
            this.BtnPrint.Image = global::TokaiCoopApp.Properties.Resources.printer;
            this.BtnPrint.Location = new System.Drawing.Point(288, 20);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(185, 55);
            this.BtnPrint.TabIndex = 254;
            this.BtnPrint.Text = "印刷";
            this.BtnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // LsvSelectiveData
            // 
            this.LsvSelectiveData.BackColor = System.Drawing.SystemColors.Window;
            this.LsvSelectiveData.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LsvSelectiveData.FullRowSelect = true;
            this.LsvSelectiveData.GridLines = true;
            this.LsvSelectiveData.HideSelection = false;
            this.LsvSelectiveData.Location = new System.Drawing.Point(5, 180);
            this.LsvSelectiveData.Name = "LsvSelectiveData";
            this.LsvSelectiveData.Size = new System.Drawing.Size(1082, 182);
            this.LsvSelectiveData.TabIndex = 276;
            this.LsvSelectiveData.UseCompatibleStateImageBehavior = false;
            this.LsvSelectiveData.DoubleClick += new System.EventHandler(this.LsvSelectiveData_DoubleClick);
            // 
            // LblSchedule
            // 
            this.LblSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblSchedule.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblSchedule.ForeColor = System.Drawing.Color.White;
            this.LblSchedule.Location = new System.Drawing.Point(4, 148);
            this.LblSchedule.Name = "LblSchedule";
            this.LblSchedule.Size = new System.Drawing.Size(1083, 33);
            this.LblSchedule.TabIndex = 277;
            this.LblSchedule.Text = "スケジュールを下記より選択してください";
            this.LblSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LstFileContent
            // 
            this.LstFileContent.BackColor = System.Drawing.SystemColors.Window;
            this.LstFileContent.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstFileContent.FormattingEnabled = true;
            this.LstFileContent.HorizontalScrollbar = true;
            this.LstFileContent.ItemHeight = 12;
            this.LstFileContent.Location = new System.Drawing.Point(1091, 176);
            this.LstFileContent.Name = "LstFileContent";
            this.LstFileContent.Size = new System.Drawing.Size(806, 184);
            this.LstFileContent.TabIndex = 278;
            // 
            // LblConter
            // 
            this.LblConter.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblConter.Location = new System.Drawing.Point(1093, 153);
            this.LblConter.Name = "LblConter";
            this.LblConter.Size = new System.Drawing.Size(134, 17);
            this.LblConter.TabIndex = 279;
            this.LblConter.Text = "LblConter";
            this.LblConter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicWaitList
            // 
            this.PicWaitList.Image = global::TokaiCoopApp.Properties.Resources.waiting1;
            this.PicWaitList.Location = new System.Drawing.Point(1456, 224);
            this.PicWaitList.Name = "PicWaitList";
            this.PicWaitList.Size = new System.Drawing.Size(100, 100);
            this.PicWaitList.TabIndex = 349;
            this.PicWaitList.TabStop = false;
            this.PicWaitList.Visible = false;
            // 
            // BtnCheckNumberOfForm
            // 
            this.BtnCheckNumberOfForm.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnCheckNumberOfForm.ForeColor = System.Drawing.Color.Black;
            this.BtnCheckNumberOfForm.Image = global::TokaiCoopApp.Properties.Resources.check_small;
            this.BtnCheckNumberOfForm.Location = new System.Drawing.Point(1081, 945);
            this.BtnCheckNumberOfForm.Name = "BtnCheckNumberOfForm";
            this.BtnCheckNumberOfForm.Size = new System.Drawing.Size(185, 55);
            this.BtnCheckNumberOfForm.TabIndex = 274;
            this.BtnCheckNumberOfForm.Text = "帳票枚数確認";
            this.BtnCheckNumberOfForm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCheckNumberOfForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCheckNumberOfForm.UseVisualStyleBackColor = true;
            this.BtnCheckNumberOfForm.Click += new System.EventHandler(this.BtnCheckNumberOfForm_Click);
            // 
            // BtnDepotOrderSetting
            // 
            this.BtnDepotOrderSetting.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnDepotOrderSetting.ForeColor = System.Drawing.Color.Black;
            this.BtnDepotOrderSetting.Image = global::TokaiCoopApp.Properties.Resources.personal_connections2;
            this.BtnDepotOrderSetting.Location = new System.Drawing.Point(494, 945);
            this.BtnDepotOrderSetting.Name = "BtnDepotOrderSetting";
            this.BtnDepotOrderSetting.Size = new System.Drawing.Size(200, 55);
            this.BtnDepotOrderSetting.TabIndex = 273;
            this.BtnDepotOrderSetting.Text = "デポ順序設定";
            this.BtnDepotOrderSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnDepotOrderSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnDepotOrderSetting.UseVisualStyleBackColor = true;
            this.BtnDepotOrderSetting.Click += new System.EventHandler(this.BtnDepotOrderSetting_Click);
            // 
            // BtnBasketCarCheck
            // 
            this.BtnBasketCarCheck.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnBasketCarCheck.ForeColor = System.Drawing.Color.Black;
            this.BtnBasketCarCheck.Image = global::TokaiCoopApp.Properties.Resources.check_icon;
            this.BtnBasketCarCheck.Location = new System.Drawing.Point(890, 945);
            this.BtnBasketCarCheck.Name = "BtnBasketCarCheck";
            this.BtnBasketCarCheck.Size = new System.Drawing.Size(185, 55);
            this.BtnBasketCarCheck.TabIndex = 272;
            this.BtnBasketCarCheck.Text = "カゴ車確認";
            this.BtnBasketCarCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnBasketCarCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnBasketCarCheck.UseVisualStyleBackColor = true;
            this.BtnBasketCarCheck.Click += new System.EventHandler(this.BtnBasketCarCheck_Click);
            // 
            // BtnShelfChange
            // 
            this.BtnShelfChange.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnShelfChange.Image = global::TokaiCoopApp.Properties.Resources.tana_icon;
            this.BtnShelfChange.Location = new System.Drawing.Point(700, 945);
            this.BtnShelfChange.Name = "BtnShelfChange";
            this.BtnShelfChange.Size = new System.Drawing.Size(185, 55);
            this.BtnShelfChange.TabIndex = 265;
            this.BtnShelfChange.Text = "棚替え設定";
            this.BtnShelfChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnShelfChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnShelfChange.UseVisualStyleBackColor = true;
            this.BtnShelfChange.Visible = false;
            this.BtnShelfChange.Click += new System.EventHandler(this.BtnShelfChange_Click);
            // 
            // BtnDriving
            // 
            this.BtnDriving.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnDriving.Image = global::TokaiCoopApp.Properties.Resources.running_icon;
            this.BtnDriving.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnDriving.Location = new System.Drawing.Point(1636, 860);
            this.BtnDriving.Name = "BtnDriving";
            this.BtnDriving.Size = new System.Drawing.Size(245, 79);
            this.BtnDriving.TabIndex = 157;
            this.BtnDriving.Text = "運転開始";
            this.BtnDriving.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnDriving.UseVisualStyleBackColor = true;
            this.BtnDriving.Click += new System.EventHandler(this.BtnDriving_Click);
            // 
            // BtnQrWrite
            // 
            this.BtnQrWrite.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnQrWrite.Image = global::TokaiCoopApp.Properties.Resources.qrcode_icon;
            this.BtnQrWrite.Location = new System.Drawing.Point(1233, 64);
            this.BtnQrWrite.Name = "BtnQrWrite";
            this.BtnQrWrite.Size = new System.Drawing.Size(190, 50);
            this.BtnQrWrite.TabIndex = 250;
            this.BtnQrWrite.Text = "ＱＲ書込";
            this.BtnQrWrite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnQrWrite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnQrWrite.UseVisualStyleBackColor = true;
            this.BtnQrWrite.Click += new System.EventHandler(this.BtnQrWrite_Click);
            // 
            // BtnQrRead
            // 
            this.BtnQrRead.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnQrRead.Image = global::TokaiCoopApp.Properties.Resources.scanner;
            this.BtnQrRead.Location = new System.Drawing.Point(1037, 64);
            this.BtnQrRead.Name = "BtnQrRead";
            this.BtnQrRead.Size = new System.Drawing.Size(190, 50);
            this.BtnQrRead.TabIndex = 249;
            this.BtnQrRead.Text = "ＱＲ読込";
            this.BtnQrRead.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnQrRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnQrRead.UseVisualStyleBackColor = true;
            this.BtnQrRead.Click += new System.EventHandler(this.BtnQrRead_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnCancel.Image = global::TokaiCoopApp.Properties.Resources.cancel;
            this.BtnCancel.Location = new System.Drawing.Point(1036, 593);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(141, 48);
            this.BtnCancel.TabIndex = 154;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnSearch.Image = global::TokaiCoopApp.Properties.Resources.search2;
            this.BtnSearch.Location = new System.Drawing.Point(1438, 64);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(220, 50);
            this.BtnSearch.TabIndex = 172;
            this.BtnSearch.Text = "検索";
            this.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnAdjust
            // 
            this.BtnAdjust.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnAdjust.Image = global::TokaiCoopApp.Properties.Resources.rework_icon2;
            this.BtnAdjust.Location = new System.Drawing.Point(1667, 64);
            this.BtnAdjust.Name = "BtnAdjust";
            this.BtnAdjust.Size = new System.Drawing.Size(220, 50);
            this.BtnAdjust.TabIndex = 169;
            this.BtnAdjust.Text = "手直し品登録";
            this.BtnAdjust.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnAdjust.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnAdjust.UseVisualStyleBackColor = true;
            this.BtnAdjust.Click += new System.EventHandler(this.BtnAdjust_Click);
            // 
            // BtnMakeControlDataFile
            // 
            this.BtnMakeControlDataFile.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnMakeControlDataFile.Image = global::TokaiCoopApp.Properties.Resources.write1;
            this.BtnMakeControlDataFile.Location = new System.Drawing.Point(13, 931);
            this.BtnMakeControlDataFile.Name = "BtnMakeControlDataFile";
            this.BtnMakeControlDataFile.Size = new System.Drawing.Size(287, 56);
            this.BtnMakeControlDataFile.TabIndex = 155;
            this.BtnMakeControlDataFile.Text = "制御データ作成";
            this.BtnMakeControlDataFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnMakeControlDataFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnMakeControlDataFile.UseVisualStyleBackColor = true;
            this.BtnMakeControlDataFile.Click += new System.EventHandler(this.BtnMakeControlDataFile_Click);
            // 
            // BtnSelect
            // 
            this.BtnSelect.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnSelect.Image = global::TokaiCoopApp.Properties.Resources.read_data;
            this.BtnSelect.Location = new System.Drawing.Point(12, 56);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(332, 50);
            this.BtnSelect.TabIndex = 133;
            this.BtnSelect.Text = "丁合指示データ取込";
            this.BtnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnBack.Image = global::TokaiCoopApp.Properties.Resources.back_arrow4;
            this.BtnBack.Location = new System.Drawing.Point(1636, 953);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(245, 56);
            this.BtnBack.TabIndex = 131;
            this.BtnBack.Text = "戻る";
            this.BtnBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // CmbFileType
            // 
            this.CmbFileType.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbFileType.FormattingEnabled = true;
            this.CmbFileType.Location = new System.Drawing.Point(13, 111);
            this.CmbFileType.Name = "CmbFileType";
            this.CmbFileType.Size = new System.Drawing.Size(191, 32);
            this.CmbFileType.TabIndex = 350;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnUpdate.Location = new System.Drawing.Point(245, 111);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(96, 35);
            this.BtnUpdate.TabIndex = 351;
            this.BtnUpdate.Text = "更新";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // ReadCollatingDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.ControlBox = false;
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.CmbFileType);
            this.Controls.Add(this.PicWaitList);
            this.Controls.Add(this.LblConter);
            this.Controls.Add(this.LstFileContent);
            this.Controls.Add(this.LblSchedule);
            this.Controls.Add(this.LsvSelectiveData);
            this.Controls.Add(this.BtnCheckNumberOfForm);
            this.Controls.Add(this.BtnDepotOrderSetting);
            this.Controls.Add(this.BtnBasketCarCheck);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.LblProgress);
            this.Controls.Add(this.LblDummyCreateCounter);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnShelfChange);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.CmbUnityTable);
            this.Controls.Add(this.CmbOrderRePrint);
            this.Controls.Add(this.BtnDriving);
            this.Controls.Add(this.BtnQrWrite);
            this.Controls.Add(this.BtnQrRead);
            this.Controls.Add(this.CmbDummyCheck);
            this.Controls.Add(this.ChkNgProduct);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblWaiting);
            this.Controls.Add(this.LblStartRow);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.CmbShippingRePrint);
            this.Controls.Add(this.BtnAdjust);
            this.Controls.Add(this.BtnLastSeisanLog);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.CmbShipping);
            this.Controls.Add(this.BtnMakeControlDataFile);
            this.Controls.Add(this.BtnTestForSeisan);
            this.Controls.Add(this.ListShipping);
            this.Controls.Add(this.ListOrderList);
            this.Controls.Add(this.ListResult);
            this.Controls.Add(this.LblCollatingFile);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.LstCollatingData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadCollatingDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "丁合指示データ読込画面";
            this.Activated += new System.EventHandler(this.ReadCollatingDataForm_Activated);
            this.Load += new System.EventHandler(this.ReadCollatingDataForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadCollatingDataForm_KeyDown);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicWaitList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.ListView LstCollatingData;
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Button BtnBack;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.Button BtnSelect;
        internal System.Windows.Forms.Label LblCollatingFile;
        internal System.Windows.Forms.Button BtnCancel;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.Label LblWaiting;
        internal System.Windows.Forms.Button BtnMakeControlDataFile;
        internal System.Windows.Forms.Button BtnDriving;
        internal System.Windows.Forms.ListBox ListResult;
        internal System.Windows.Forms.ListBox ListOrderList;
        internal System.Windows.Forms.ListBox ListShipping;
        internal System.Windows.Forms.Button BtnTestForSeisan;
        internal System.Windows.Forms.Timer TimTestForSeisan;
        internal System.Windows.Forms.ComboBox CmbShipping;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button BtnLastSeisanLog;
        internal System.Windows.Forms.Button BtnAdjust;
        internal System.Windows.Forms.ComboBox CmbShippingRePrint;
        internal System.Windows.Forms.Button BtnSearch;
        internal System.Windows.Forms.Label LblStartRow;
        internal System.Windows.Forms.CheckBox ChkNgProduct;

        #endregion
        internal System.Windows.Forms.ComboBox CmbDummyCheck;
        internal System.Windows.Forms.Button BtnQrRead;
        internal System.Windows.Forms.Button BtnQrWrite;
        internal System.Drawing.Printing.PrintDocument PrintQrWrite;
        internal System.Windows.Forms.Button BtnPrint;
        private System.Drawing.Printing.PrintDocument PrintDocument;
        internal System.Windows.Forms.ComboBox CmbOrderRePrint;
        internal System.Windows.Forms.ComboBox CmbUnityTable;
        internal System.Windows.Forms.GroupBox groupBox6;
        internal System.Windows.Forms.Label LblFoldingCon;
        internal System.Windows.Forms.GroupBox groupBox7;
        internal System.Windows.Forms.Label LblMaxBundle;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label LblStatus;
        internal System.Windows.Forms.Button BtnShelfChange;
        internal System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker DTPDeliveryDate;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.CheckBox ChkDataReadFromUSB;
        internal System.Windows.Forms.Label LblDummyCreateCounter;
        private System.Windows.Forms.Label LblProgress;
        internal System.Windows.Forms.ComboBox CmbPrintType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Diagnostics.EventLog eventLog1;
        internal System.Windows.Forms.Button BtnBasketCarCheck;
        internal System.Windows.Forms.Button BtnDepotOrderSetting;
        internal System.Windows.Forms.Button BtnCheckNumberOfForm;
        private System.Windows.Forms.ListView LsvSelectiveData;
        internal System.Windows.Forms.Label LblSchedule;
        internal System.Windows.Forms.ListBox LstFileContent;
        private System.Windows.Forms.Label LblConter;
        private System.Windows.Forms.PictureBox PicWaitList;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.ComboBox CmbFileType;
    }
}