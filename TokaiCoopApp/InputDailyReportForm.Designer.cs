
namespace GreenCoopApp
{
    partial class InputDailyReportForm
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
            this.LblTitle = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.CmbToHour = new System.Windows.Forms.ComboBox();
            this.CmbToMin = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.CmbFromMin = new System.Windows.Forms.ComboBox();
            this.CmbFromHour = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.TxtMachine = new System.Windows.Forms.TextBox();
            this.TxtKikakuSyu = new System.Windows.Forms.TextBox();
            this.TxtDeliveryDay = new System.Windows.Forms.TextBox();
            this.TxtDeliveryWeek = new System.Windows.Forms.TextBox();
            this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            this.CmbSeisanFileName = new System.Windows.Forms.ComboBox();
            this.DTPicForm = new System.Windows.Forms.DateTimePicker();
            this.CmbEventLogFileName = new System.Windows.Forms.ComboBox();
            this.CmbRestTime = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(-1, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1907, 50);
            this.LblTitle.TabIndex = 131;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label1.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.Location = new System.Drawing.Point(564, 224);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(242, 36);
            this.Label1.TabIndex = 132;
            this.Label1.Text = "作業日";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(1750, 1000);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(130, 25);
            this.LblVersion.TabIndex = 133;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnPrint.Image = global::GreenCoopApp.Properties.Resources.printer;
            this.BtnPrint.Location = new System.Drawing.Point(564, 599);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(445, 50);
            this.BtnPrint.TabIndex = 159;
            this.BtnPrint.Text = "作業日報印刷";
            this.BtnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Image = global::GreenCoopApp.Properties.Resources.exit_icon_small;
            this.BtnClose.Location = new System.Drawing.Point(1132, 599);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(218, 50);
            this.BtnClose.TabIndex = 160;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label2.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label2.Location = new System.Drawing.Point(564, 267);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(242, 36);
            this.Label2.TabIndex = 162;
            this.Label2.Text = "開始時間";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label4.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label4.Location = new System.Drawing.Point(564, 309);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(242, 36);
            this.Label4.TabIndex = 164;
            this.Label4.Text = "終了時間";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CmbToHour
            // 
            this.CmbToHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbToHour.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbToHour.FormattingEnabled = true;
            this.CmbToHour.IntegralHeight = false;
            this.CmbToHour.Location = new System.Drawing.Point(807, 310);
            this.CmbToHour.MaxDropDownItems = 10;
            this.CmbToHour.Name = "CmbToHour";
            this.CmbToHour.Size = new System.Drawing.Size(76, 36);
            this.CmbToHour.TabIndex = 165;
            // 
            // CmbToMin
            // 
            this.CmbToMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbToMin.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbToMin.FormattingEnabled = true;
            this.CmbToMin.IntegralHeight = false;
            this.CmbToMin.Location = new System.Drawing.Point(931, 313);
            this.CmbToMin.MaxDropDownItems = 10;
            this.CmbToMin.Name = "CmbToMin";
            this.CmbToMin.Size = new System.Drawing.Size(76, 36);
            this.CmbToMin.TabIndex = 166;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label5.Location = new System.Drawing.Point(886, 314);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(42, 31);
            this.Label5.TabIndex = 167;
            this.Label5.Text = "時";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label6.Location = new System.Drawing.Point(1015, 224);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(42, 31);
            this.Label6.TabIndex = 168;
            this.Label6.Text = "分";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label3.Location = new System.Drawing.Point(1015, 269);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(42, 31);
            this.Label3.TabIndex = 172;
            this.Label3.Text = "分";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label7.Location = new System.Drawing.Point(886, 270);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(42, 31);
            this.Label7.TabIndex = 171;
            this.Label7.Text = "時";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmbFromMin
            // 
            this.CmbFromMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFromMin.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbFromMin.FormattingEnabled = true;
            this.CmbFromMin.IntegralHeight = false;
            this.CmbFromMin.Location = new System.Drawing.Point(931, 267);
            this.CmbFromMin.MaxDropDownItems = 10;
            this.CmbFromMin.Name = "CmbFromMin";
            this.CmbFromMin.Size = new System.Drawing.Size(76, 36);
            this.CmbFromMin.TabIndex = 170;
            // 
            // CmbFromHour
            // 
            this.CmbFromHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFromHour.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbFromHour.FormattingEnabled = true;
            this.CmbFromHour.IntegralHeight = false;
            this.CmbFromHour.Location = new System.Drawing.Point(807, 267);
            this.CmbFromHour.MaxDropDownItems = 10;
            this.CmbFromHour.Name = "CmbFromHour";
            this.CmbFromHour.Size = new System.Drawing.Size(76, 36);
            this.CmbFromHour.TabIndex = 169;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label8.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label8.Location = new System.Drawing.Point(564, 396);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(242, 36);
            this.Label8.TabIndex = 173;
            this.Label8.Text = "機械";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label9.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label9.Location = new System.Drawing.Point(564, 439);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(242, 36);
            this.Label9.TabIndex = 174;
            this.Label9.Text = "企画週";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label10.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label10.Location = new System.Drawing.Point(564, 483);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(242, 36);
            this.Label10.TabIndex = 175;
            this.Label10.Text = "配達日";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label11.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label11.Location = new System.Drawing.Point(564, 525);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(242, 36);
            this.Label11.TabIndex = 176;
            this.Label11.Text = "配達曜日";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtMachine
            // 
            this.TxtMachine.Enabled = false;
            this.TxtMachine.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtMachine.Location = new System.Drawing.Point(807, 396);
            this.TxtMachine.Name = "TxtMachine";
            this.TxtMachine.Size = new System.Drawing.Size(203, 36);
            this.TxtMachine.TabIndex = 177;
            this.TxtMachine.Text = "1号機";
            this.TxtMachine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtKikakuSyu
            // 
            this.TxtKikakuSyu.Enabled = false;
            this.TxtKikakuSyu.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtKikakuSyu.Location = new System.Drawing.Point(807, 439);
            this.TxtKikakuSyu.Name = "TxtKikakuSyu";
            this.TxtKikakuSyu.Size = new System.Drawing.Size(203, 36);
            this.TxtKikakuSyu.TabIndex = 178;
            this.TxtKikakuSyu.Text = "20週";
            this.TxtKikakuSyu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtDeliveryDay
            // 
            this.TxtDeliveryDay.Enabled = false;
            this.TxtDeliveryDay.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtDeliveryDay.Location = new System.Drawing.Point(807, 483);
            this.TxtDeliveryDay.Name = "TxtDeliveryDay";
            this.TxtDeliveryDay.Size = new System.Drawing.Size(203, 36);
            this.TxtDeliveryDay.TabIndex = 179;
            this.TxtDeliveryDay.Text = "1日目";
            this.TxtDeliveryDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtDeliveryWeek
            // 
            this.TxtDeliveryWeek.Enabled = false;
            this.TxtDeliveryWeek.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtDeliveryWeek.Location = new System.Drawing.Point(807, 526);
            this.TxtDeliveryWeek.Name = "TxtDeliveryWeek";
            this.TxtDeliveryWeek.Size = new System.Drawing.Size(203, 36);
            this.TxtDeliveryWeek.TabIndex = 180;
            this.TxtDeliveryWeek.Text = "月曜日";
            this.TxtDeliveryWeek.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PrintDocument1
            // 
            this.PrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // CmbSeisanFileName
            // 
            this.CmbSeisanFileName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSeisanFileName.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbSeisanFileName.FormattingEnabled = true;
            this.CmbSeisanFileName.Location = new System.Drawing.Point(807, 137);
            this.CmbSeisanFileName.Name = "CmbSeisanFileName";
            this.CmbSeisanFileName.Size = new System.Drawing.Size(545, 36);
            this.CmbSeisanFileName.TabIndex = 195;
            this.CmbSeisanFileName.SelectedIndexChanged += new System.EventHandler(this.CmbSeisanFileName_SelectedIndexChanged);
            // 
            // DTPicForm
            // 
            this.DTPicForm.CalendarFont = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DTPicForm.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DTPicForm.Location = new System.Drawing.Point(807, 224);
            this.DTPicForm.Name = "DTPicForm";
            this.DTPicForm.Size = new System.Drawing.Size(202, 36);
            this.DTPicForm.TabIndex = 194;
            this.DTPicForm.ValueChanged += new System.EventHandler(this.DTPicForm_ValueChanged);
            // 
            // CmbEventLogFileName
            // 
            this.CmbEventLogFileName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbEventLogFileName.DropDownWidth = 545;
            this.CmbEventLogFileName.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbEventLogFileName.FormattingEnabled = true;
            this.CmbEventLogFileName.Location = new System.Drawing.Point(807, 181);
            this.CmbEventLogFileName.Name = "CmbEventLogFileName";
            this.CmbEventLogFileName.Size = new System.Drawing.Size(544, 36);
            this.CmbEventLogFileName.TabIndex = 196;
            // 
            // CmbRestTime
            // 
            this.CmbRestTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRestTime.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbRestTime.FormattingEnabled = true;
            this.CmbRestTime.IntegralHeight = false;
            this.CmbRestTime.Location = new System.Drawing.Point(807, 354);
            this.CmbRestTime.MaxDropDownItems = 10;
            this.CmbRestTime.Name = "CmbRestTime";
            this.CmbRestTime.Size = new System.Drawing.Size(76, 36);
            this.CmbRestTime.TabIndex = 198;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label12.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label12.Location = new System.Drawing.Point(564, 353);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(242, 36);
            this.Label12.TabIndex = 197;
            this.Label12.Text = "休憩時間";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.SystemColors.Control;
            this.Label13.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label13.Location = new System.Drawing.Point(889, 357);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(42, 31);
            this.Label13.TabIndex = 199;
            this.Label13.Text = "分";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label14.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label14.Location = new System.Drawing.Point(564, 137);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(242, 36);
            this.Label14.TabIndex = 200;
            this.Label14.Text = "最終生産ログ";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label15.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label15.Location = new System.Drawing.Point(564, 181);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(242, 36);
            this.Label15.TabIndex = 201;
            this.Label15.Text = "最終イベントログ";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBox1
            // 
            this.PictureBox1.ErrorImage = null;
            this.PictureBox1.Image = global::GreenCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.InitialImage = global::GreenCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.Location = new System.Drawing.Point(1600, 900);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(275, 62);
            this.PictureBox1.TabIndex = 202;
            this.PictureBox1.TabStop = false;
            // 
            // InputDailyReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1900, 1037);
            this.ControlBox = false;
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.CmbSeisanFileName);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.CmbRestTime);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.CmbEventLogFileName);
            this.Controls.Add(this.DTPicForm);
            this.Controls.Add(this.TxtDeliveryWeek);
            this.Controls.Add(this.TxtDeliveryDay);
            this.Controls.Add(this.TxtKikakuSyu);
            this.Controls.Add(this.TxtMachine);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.CmbFromMin);
            this.Controls.Add(this.CmbFromHour);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.CmbToMin);
            this.Controls.Add(this.CmbToHour);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.LblTitle);
            this.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputDailyReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日報入力";
            this.Load += new System.EventHandler(this.InputDailyReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.Button BtnPrint;
        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox CmbToHour;
        internal System.Windows.Forms.ComboBox CmbToMin;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox CmbFromMin;
        internal System.Windows.Forms.ComboBox CmbFromHour;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.TextBox TxtMachine;
        internal System.Windows.Forms.TextBox TxtKikakuSyu;
        internal System.Windows.Forms.TextBox TxtDeliveryDay;
        internal System.Windows.Forms.TextBox TxtDeliveryWeek;
        internal System.Drawing.Printing.PrintDocument PrintDocument1;
        internal System.Windows.Forms.ComboBox CmbSeisanFileName;
        internal System.Windows.Forms.DateTimePicker DTPicForm;
        internal System.Windows.Forms.ComboBox CmbEventLogFileName;
        internal System.Windows.Forms.ComboBox CmbRestTime;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label15;

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox1;
    }
}