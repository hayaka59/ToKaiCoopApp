
namespace TokaiCoopApp
{
    partial class ProductionDetailForm
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
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.LblTankyo1 = new System.Windows.Forms.Label();
            this.LblDate = new System.Windows.Forms.Label();
            this.LblFromTo = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.LblWorkTime = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.LblSeisanCount = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.LblRunTime = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.LblStop5MinUnder = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.LblStop5MinOver = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.LblMaxRunTime = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.LsvTimeDetail = new System.Windows.Forms.ListView();
            this.Label17 = new System.Windows.Forms.Label();
            this.LsvChokoDetail = new System.Windows.Forms.ListView();
            this.Label18 = new System.Windows.Forms.Label();
            this.LstFeederDetail = new System.Windows.Forms.ListView();
            this.BtnPrintScreen = new System.Windows.Forms.Button();
            this.TimerPrint = new System.Windows.Forms.Timer(this.components);
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1903, 50);
            this.LblTitle.TabIndex = 132;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(1750, 1000);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(130, 25);
            this.LblVersion.TabIndex = 166;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Image = global::TokaiCoopApp.Properties.Resources.exit_icon_small;
            this.BtnClose.Location = new System.Drawing.Point(721, 965);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(173, 50);
            this.BtnClose.TabIndex = 165;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LblTankyo1
            // 
            this.LblTankyo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.LblTankyo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblTankyo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTankyo1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTankyo1.Location = new System.Drawing.Point(363, 54);
            this.LblTankyo1.Name = "LblTankyo1";
            this.LblTankyo1.Size = new System.Drawing.Size(165, 34);
            this.LblTankyo1.TabIndex = 168;
            this.LblTankyo1.Text = "日付";
            this.LblTankyo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDate
            // 
            this.LblDate.BackColor = System.Drawing.Color.Silver;
            this.LblDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblDate.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblDate.Location = new System.Drawing.Point(529, 54);
            this.LblDate.Name = "LblDate";
            this.LblDate.Size = new System.Drawing.Size(275, 34);
            this.LblDate.TabIndex = 169;
            this.LblDate.Text = "****/**/**";
            this.LblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblFromTo
            // 
            this.LblFromTo.BackColor = System.Drawing.Color.Silver;
            this.LblFromTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblFromTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblFromTo.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblFromTo.Location = new System.Drawing.Point(529, 88);
            this.LblFromTo.Name = "LblFromTo";
            this.LblFromTo.Size = new System.Drawing.Size(275, 34);
            this.LblFromTo.TabIndex = 171;
            this.LblFromTo.Text = "**:**:** ～ **:**:**";
            this.LblFromTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label3.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label3.Location = new System.Drawing.Point(363, 88);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(165, 34);
            this.Label3.TabIndex = 170;
            this.Label3.Text = "開始～終了";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblWorkTime
            // 
            this.LblWorkTime.BackColor = System.Drawing.Color.Silver;
            this.LblWorkTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblWorkTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblWorkTime.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblWorkTime.Location = new System.Drawing.Point(529, 122);
            this.LblWorkTime.Name = "LblWorkTime";
            this.LblWorkTime.Size = new System.Drawing.Size(275, 34);
            this.LblWorkTime.TabIndex = 173;
            this.LblWorkTime.Text = "**:**:**（***分）";
            this.LblWorkTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label5.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label5.Location = new System.Drawing.Point(363, 122);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(165, 34);
            this.Label5.TabIndex = 172;
            this.Label5.Text = "作業時間";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblSeisanCount
            // 
            this.LblSeisanCount.BackColor = System.Drawing.Color.Silver;
            this.LblSeisanCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblSeisanCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblSeisanCount.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblSeisanCount.Location = new System.Drawing.Point(529, 156);
            this.LblSeisanCount.Name = "LblSeisanCount";
            this.LblSeisanCount.Size = new System.Drawing.Size(275, 34);
            this.LblSeisanCount.TabIndex = 175;
            this.LblSeisanCount.Text = "*****";
            this.LblSeisanCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label7.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label7.Location = new System.Drawing.Point(363, 156);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(165, 34);
            this.Label7.TabIndex = 174;
            this.Label7.Text = "生産数";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblRunTime
            // 
            this.LblRunTime.BackColor = System.Drawing.Color.Silver;
            this.LblRunTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblRunTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblRunTime.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblRunTime.Location = new System.Drawing.Point(1178, 54);
            this.LblRunTime.Name = "LblRunTime";
            this.LblRunTime.Size = new System.Drawing.Size(374, 34);
            this.LblRunTime.TabIndex = 177;
            this.LblRunTime.Text = "**:**:**（***分）";
            this.LblRunTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label9.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label9.Location = new System.Drawing.Point(832, 54);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(345, 34);
            this.Label9.TabIndex = 176;
            this.Label9.Text = "稼働時間";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblStop5MinUnder
            // 
            this.LblStop5MinUnder.BackColor = System.Drawing.Color.Silver;
            this.LblStop5MinUnder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblStop5MinUnder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblStop5MinUnder.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblStop5MinUnder.Location = new System.Drawing.Point(1178, 88);
            this.LblStop5MinUnder.Name = "LblStop5MinUnder";
            this.LblStop5MinUnder.Size = new System.Drawing.Size(374, 34);
            this.LblStop5MinUnder.TabIndex = 179;
            this.LblStop5MinUnder.Text = "**:**:**（***分）";
            this.LblStop5MinUnder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label11.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label11.Location = new System.Drawing.Point(832, 88);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(345, 34);
            this.Label11.TabIndex = 178;
            this.Label11.Text = "チョコ停（５分以内の停止）";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblStop5MinOver
            // 
            this.LblStop5MinOver.BackColor = System.Drawing.Color.Silver;
            this.LblStop5MinOver.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblStop5MinOver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblStop5MinOver.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblStop5MinOver.Location = new System.Drawing.Point(1178, 122);
            this.LblStop5MinOver.Name = "LblStop5MinOver";
            this.LblStop5MinOver.Size = new System.Drawing.Size(374, 34);
            this.LblStop5MinOver.TabIndex = 181;
            this.LblStop5MinOver.Text = "**:**:**（***分）";
            this.LblStop5MinOver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label13.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label13.Location = new System.Drawing.Point(832, 122);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(345, 34);
            this.Label13.TabIndex = 180;
            this.Label13.Text = "再調整（５分以上の停止）";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMaxRunTime
            // 
            this.LblMaxRunTime.BackColor = System.Drawing.Color.Silver;
            this.LblMaxRunTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblMaxRunTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblMaxRunTime.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblMaxRunTime.Location = new System.Drawing.Point(1178, 156);
            this.LblMaxRunTime.Name = "LblMaxRunTime";
            this.LblMaxRunTime.Size = new System.Drawing.Size(374, 34);
            this.LblMaxRunTime.TabIndex = 183;
            this.LblMaxRunTime.Text = "**:**:**（**:**:**～**:**:**）";
            this.LblMaxRunTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label15.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label15.Location = new System.Drawing.Point(832, 156);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(345, 34);
            this.Label15.TabIndex = 182;
            this.Label15.Text = "最大連続稼働時間";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label16.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label16.Location = new System.Drawing.Point(363, 196);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(531, 34);
            this.Label16.TabIndex = 185;
            this.Label16.Text = "■時間帯別内訳";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LsvTimeDetail
            // 
            this.LsvTimeDetail.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LsvTimeDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LsvTimeDetail.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LsvTimeDetail.FullRowSelect = true;
            this.LsvTimeDetail.GridLines = true;
            this.LsvTimeDetail.HideSelection = false;
            this.LsvTimeDetail.Location = new System.Drawing.Point(363, 232);
            this.LsvTimeDetail.MultiSelect = false;
            this.LsvTimeDetail.Name = "LsvTimeDetail";
            this.LsvTimeDetail.Size = new System.Drawing.Size(531, 331);
            this.LsvTimeDetail.TabIndex = 184;
            this.LsvTimeDetail.UseCompatibleStateImageBehavior = false;
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label17.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label17.Location = new System.Drawing.Point(363, 570);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(531, 32);
            this.Label17.TabIndex = 187;
            this.Label17.Text = "■チョコ停内訳";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LsvChokoDetail
            // 
            this.LsvChokoDetail.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LsvChokoDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LsvChokoDetail.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LsvChokoDetail.FullRowSelect = true;
            this.LsvChokoDetail.GridLines = true;
            this.LsvChokoDetail.HideSelection = false;
            this.LsvChokoDetail.Location = new System.Drawing.Point(363, 603);
            this.LsvChokoDetail.MultiSelect = false;
            this.LsvChokoDetail.Name = "LsvChokoDetail";
            this.LsvChokoDetail.Size = new System.Drawing.Size(531, 340);
            this.LsvChokoDetail.TabIndex = 186;
            this.LsvChokoDetail.UseCompatibleStateImageBehavior = false;
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label18.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label18.Location = new System.Drawing.Point(915, 196);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(636, 34);
            this.Label18.TabIndex = 189;
            this.Label18.Text = "■フィーダー別内訳";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LstFeederDetail
            // 
            this.LstFeederDetail.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LstFeederDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstFeederDetail.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstFeederDetail.FullRowSelect = true;
            this.LstFeederDetail.GridLines = true;
            this.LstFeederDetail.HideSelection = false;
            this.LstFeederDetail.Location = new System.Drawing.Point(915, 232);
            this.LstFeederDetail.MultiSelect = false;
            this.LstFeederDetail.Name = "LstFeederDetail";
            this.LstFeederDetail.Size = new System.Drawing.Size(636, 783);
            this.LstFeederDetail.TabIndex = 188;
            this.LstFeederDetail.UseCompatibleStateImageBehavior = false;
            // 
            // BtnPrintScreen
            // 
            this.BtnPrintScreen.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnPrintScreen.Image = global::TokaiCoopApp.Properties.Resources.printer;
            this.BtnPrintScreen.Location = new System.Drawing.Point(363, 965);
            this.BtnPrintScreen.Name = "BtnPrintScreen";
            this.BtnPrintScreen.Size = new System.Drawing.Size(173, 50);
            this.BtnPrintScreen.TabIndex = 195;
            this.BtnPrintScreen.Text = "画面印刷";
            this.BtnPrintScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnPrintScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPrintScreen.UseVisualStyleBackColor = true;
            this.BtnPrintScreen.Click += new System.EventHandler(this.BtnPrintScreen_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.ErrorImage = null;
            this.PictureBox1.Image = global::TokaiCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.InitialImage = global::TokaiCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.Location = new System.Drawing.Point(1600, 900);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(275, 62);
            this.PictureBox1.TabIndex = 270;
            this.PictureBox1.TabStop = false;
            // 
            // ProductionDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1900, 1037);
            this.ControlBox = false;
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.BtnPrintScreen);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.LstFeederDetail);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.LsvChokoDetail);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.LsvTimeDetail);
            this.Controls.Add(this.LblMaxRunTime);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.LblStop5MinOver);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.LblStop5MinUnder);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.LblRunTime);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.LblSeisanCount);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.LblWorkTime);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.LblFromTo);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.LblDate);
            this.Controls.Add(this.LblTankyo1);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ProductionDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生産実績詳細画面";
            this.Load += new System.EventHandler(this.ProductionDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.Label LblTankyo1;
        internal System.Windows.Forms.Label LblDate;
        internal System.Windows.Forms.Label LblFromTo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label LblWorkTime;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label LblSeisanCount;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label LblRunTime;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label LblStop5MinUnder;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label LblStop5MinOver;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label LblMaxRunTime;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.ListView LsvTimeDetail;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.ListView LsvChokoDetail;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.ListView LstFeederDetail;
        internal System.Windows.Forms.Button BtnPrintScreen;
        internal System.Windows.Forms.Timer TimerPrint;

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox1;
    }
}