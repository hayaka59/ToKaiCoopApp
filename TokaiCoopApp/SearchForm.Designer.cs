
namespace TokaiCoopApp
{
    partial class SearchForm
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
            this.BtnClose = new System.Windows.Forms.Button();
            this.LblVersion = new System.Windows.Forms.Label();
            this.CmbSeisanFileName = new System.Windows.Forms.ComboBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.DTPicForm = new System.Windows.Forms.DateTimePicker();
            this.LstSeisanData = new System.Windows.Forms.ListView();
            this.LblSearchResult = new System.Windows.Forms.Label();
            this.TxtSearchData = new System.Windows.Forms.TextBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.LblWaiting = new System.Windows.Forms.Label();
            this.ChkIsDispFeeder = new System.Windows.Forms.CheckBox();
            this.BtnRead = new System.Windows.Forms.Button();
            this.CmbCodeType = new System.Windows.Forms.ComboBox();
            this.CmbJudge = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.BtnAdjust = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(931, 34);
            this.LblTitle.TabIndex = 131;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Image = global::TokaiCoopApp.Properties.Resources.exit_icon_small;
            this.BtnClose.Location = new System.Drawing.Point(785, 518);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(133, 52);
            this.BtnClose.TabIndex = 132;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(800, 598);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(118, 24);
            this.LblVersion.TabIndex = 133;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmbSeisanFileName
            // 
            this.CmbSeisanFileName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSeisanFileName.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbSeisanFileName.FormattingEnabled = true;
            this.CmbSeisanFileName.Location = new System.Drawing.Point(210, 518);
            this.CmbSeisanFileName.Name = "CmbSeisanFileName";
            this.CmbSeisanFileName.Size = new System.Drawing.Size(441, 36);
            this.CmbSeisanFileName.TabIndex = 196;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnSearch.Image = global::TokaiCoopApp.Properties.Resources.search2;
            this.BtnSearch.Location = new System.Drawing.Point(251, 47);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(201, 61);
            this.BtnSearch.TabIndex = 195;
            this.BtnSearch.Text = "検索";
            this.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // DTPicForm
            // 
            this.DTPicForm.CalendarFont = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DTPicForm.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DTPicForm.Location = new System.Drawing.Point(11, 518);
            this.DTPicForm.Name = "DTPicForm";
            this.DTPicForm.Size = new System.Drawing.Size(196, 36);
            this.DTPicForm.TabIndex = 194;
            this.DTPicForm.ValueChanged += new System.EventHandler(this.DTPicForm_ValueChanged);
            // 
            // LstSeisanData
            // 
            this.LstSeisanData.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstSeisanData.FullRowSelect = true;
            this.LstSeisanData.GridLines = true;
            this.LstSeisanData.HideSelection = false;
            this.LstSeisanData.Location = new System.Drawing.Point(12, 115);
            this.LstSeisanData.MultiSelect = false;
            this.LstSeisanData.Name = "LstSeisanData";
            this.LstSeisanData.Size = new System.Drawing.Size(906, 370);
            this.LstSeisanData.TabIndex = 197;
            this.LstSeisanData.UseCompatibleStateImageBehavior = false;
            // 
            // LblSearchResult
            // 
            this.LblSearchResult.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblSearchResult.Location = new System.Drawing.Point(469, 489);
            this.LblSearchResult.Name = "LblSearchResult";
            this.LblSearchResult.Size = new System.Drawing.Size(281, 22);
            this.LblSearchResult.TabIndex = 198;
            this.LblSearchResult.Text = "LblSearchResult";
            this.LblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtSearchData
            // 
            this.TxtSearchData.BackColor = System.Drawing.Color.White;
            this.TxtSearchData.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtSearchData.Location = new System.Drawing.Point(12, 77);
            this.TxtSearchData.MaxLength = 10;
            this.TxtSearchData.Name = "TxtSearchData";
            this.TxtSearchData.Size = new System.Drawing.Size(133, 31);
            this.TxtSearchData.TabIndex = 200;
            this.TxtSearchData.Text = "1234567890";
            this.TxtSearchData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtSearchData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearchData_KeyPress);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnCancel.Location = new System.Drawing.Point(375, 311);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(128, 35);
            this.BtnCancel.TabIndex = 203;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(220, 269);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(438, 33);
            this.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar1.TabIndex = 202;
            // 
            // LblWaiting
            // 
            this.LblWaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LblWaiting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblWaiting.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LblWaiting.Font = new System.Drawing.Font("メイリオ", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblWaiting.ForeColor = System.Drawing.Color.Blue;
            this.LblWaiting.Location = new System.Drawing.Point(210, 218);
            this.LblWaiting.Name = "LblWaiting";
            this.LblWaiting.Size = new System.Drawing.Size(460, 137);
            this.LblWaiting.TabIndex = 201;
            this.LblWaiting.Text = "検索処理中です";
            this.LblWaiting.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChkIsDispFeeder
            // 
            this.ChkIsDispFeeder.AutoSize = true;
            this.ChkIsDispFeeder.Location = new System.Drawing.Point(210, 498);
            this.ChkIsDispFeeder.Name = "ChkIsDispFeeder";
            this.ChkIsDispFeeder.Size = new System.Drawing.Size(72, 16);
            this.ChkIsDispFeeder.TabIndex = 204;
            this.ChkIsDispFeeder.Text = "詳細表示";
            this.ChkIsDispFeeder.UseVisualStyleBackColor = true;
            // 
            // BtnRead
            // 
            this.BtnRead.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnRead.Image = global::TokaiCoopApp.Properties.Resources.read_data;
            this.BtnRead.Location = new System.Drawing.Point(654, 517);
            this.BtnRead.Name = "BtnRead";
            this.BtnRead.Size = new System.Drawing.Size(125, 52);
            this.BtnRead.TabIndex = 205;
            this.BtnRead.Text = "読込";
            this.BtnRead.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnRead.UseVisualStyleBackColor = true;
            this.BtnRead.Click += new System.EventHandler(this.BtnRead_Click);
            // 
            // CmbCodeType
            // 
            this.CmbCodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCodeType.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbCodeType.FormattingEnabled = true;
            this.CmbCodeType.Location = new System.Drawing.Point(12, 47);
            this.CmbCodeType.Name = "CmbCodeType";
            this.CmbCodeType.Size = new System.Drawing.Size(133, 32);
            this.CmbCodeType.TabIndex = 206;
            // 
            // CmbJudge
            // 
            this.CmbJudge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbJudge.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbJudge.FormattingEnabled = true;
            this.CmbJudge.Location = new System.Drawing.Point(151, 76);
            this.CmbJudge.Name = "CmbJudge";
            this.CmbJudge.Size = new System.Drawing.Size(94, 32);
            this.CmbJudge.TabIndex = 207;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Label1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(151, 47);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(94, 28);
            this.Label1.TabIndex = 208;
            this.Label1.Text = "判定";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(380, 492);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(83, 26);
            this.Button1.TabIndex = 209;
            this.Button1.Text = "デバッグボタン";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Visible = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BtnAdjust
            // 
            this.BtnAdjust.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnAdjust.Image = global::TokaiCoopApp.Properties.Resources.rework_icon2;
            this.BtnAdjust.Location = new System.Drawing.Point(717, 47);
            this.BtnAdjust.Name = "BtnAdjust";
            this.BtnAdjust.Size = new System.Drawing.Size(201, 61);
            this.BtnAdjust.TabIndex = 210;
            this.BtnAdjust.Text = "手直し品登録";
            this.BtnAdjust.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnAdjust.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnAdjust.UseVisualStyleBackColor = true;
            this.BtnAdjust.Click += new System.EventHandler(this.BtnAdjust_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.ErrorImage = null;
            this.PictureBox1.Image = global::TokaiCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.InitialImage = global::TokaiCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.Location = new System.Drawing.Point(12, 560);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(275, 62);
            this.PictureBox1.TabIndex = 271;
            this.PictureBox1.TabStop = false;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 627);
            this.ControlBox = false;
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.BtnAdjust);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.CmbCodeType);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.CmbJudge);
            this.Controls.Add(this.BtnRead);
            this.Controls.Add(this.ChkIsDispFeeder);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.LblWaiting);
            this.Controls.Add(this.TxtSearchData);
            this.Controls.Add(this.LblSearchResult);
            this.Controls.Add(this.LstSeisanData);
            this.Controls.Add(this.CmbSeisanFileName);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.DTPicForm);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "検索画面";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.ComboBox CmbSeisanFileName;
        internal System.Windows.Forms.Button BtnSearch;
        internal System.Windows.Forms.DateTimePicker DTPicForm;
        internal System.Windows.Forms.ListView LstSeisanData;
        internal System.Windows.Forms.Label LblSearchResult;
        internal System.Windows.Forms.TextBox TxtSearchData;
        internal System.Windows.Forms.Button BtnCancel;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.Label LblWaiting;
        internal System.Windows.Forms.CheckBox ChkIsDispFeeder;
        internal System.Windows.Forms.Button BtnRead;
        internal System.Windows.Forms.ComboBox CmbCodeType;
        internal System.Windows.Forms.ComboBox CmbJudge;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button BtnAdjust;

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox1;
    }
}
