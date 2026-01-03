
namespace TokaiCoopApp
{
    partial class EntryAdjustForm
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
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblTankyo1 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtBcrRead = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.TxtBcrInput = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.LstReadBcrData = new System.Windows.Forms.ListView();
            this.TxtReadBcr = new System.Windows.Forms.TextBox();
            this.LstBoxWorkCode = new System.Windows.Forms.ListBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.LblForPrint = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.LstFormList = new System.Windows.Forms.ListView();
            this.PnlFormList = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.BtnCancelForFormList = new System.Windows.Forms.Button();
            this.BtnOkForFormList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.PnlFormList.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1338, 32);
            this.LblTitle.TabIndex = 132;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(1048, 587);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(130, 25);
            this.LblVersion.TabIndex = 134;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblVersion.DoubleClick += new System.EventHandler(this.LblVersion_DoubleClick);
            // 
            // LblTankyo1
            // 
            this.LblTankyo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.LblTankyo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblTankyo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTankyo1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTankyo1.Location = new System.Drawing.Point(13, 98);
            this.LblTankyo1.Name = "LblTankyo1";
            this.LblTankyo1.Size = new System.Drawing.Size(179, 43);
            this.LblTankyo1.TabIndex = 169;
            this.LblTankyo1.Text = "ＱＲコード読取";
            this.LblTankyo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.Location = new System.Drawing.Point(13, 147);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(179, 43);
            this.Label1.TabIndex = 170;
            this.Label1.Text = "組合員コード入力";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBcrRead
            // 
            this.TxtBcrRead.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBcrRead.Location = new System.Drawing.Point(193, 98);
            this.TxtBcrRead.MaxLength = 27;
            this.TxtBcrRead.Name = "TxtBcrRead";
            this.TxtBcrRead.Size = new System.Drawing.Size(487, 43);
            this.TxtBcrRead.TabIndex = 171;
            this.TxtBcrRead.Text = "12345678901234567890123456789";
            this.TxtBcrRead.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtBcrRead.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBcrRead_KeyPress);
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label6.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label6.ForeColor = System.Drawing.Color.Blue;
            this.Label6.Location = new System.Drawing.Point(26, 37);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(317, 29);
            this.Label6.TabIndex = 176;
            this.Label6.Text = "「バーコードエラー」で登録します。";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label2.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label2.Location = new System.Drawing.Point(459, 156);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(235, 34);
            this.Label2.TabIndex = 177;
            this.Label2.Text = "（入力例）123456789";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label3.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label3.ForeColor = System.Drawing.Color.Red;
            this.Label3.Location = new System.Drawing.Point(8, 193);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(711, 35);
            this.Label3.TabIndex = 178;
            this.Label3.Text = "※ＱＲコードを読み取れない場合は、シトマアイに表示された８桁のコードを入力して下さい。";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label3.Visible = false;
            // 
            // TxtBcrInput
            // 
            this.TxtBcrInput.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBcrInput.Location = new System.Drawing.Point(193, 146);
            this.TxtBcrInput.MaxLength = 10;
            this.TxtBcrInput.Name = "TxtBcrInput";
            this.TxtBcrInput.Size = new System.Drawing.Size(260, 43);
            this.TxtBcrInput.TabIndex = 179;
            this.TxtBcrInput.Text = "123456789";
            this.TxtBcrInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtBcrInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBcrInput_KeyPress);
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label4.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label4.ForeColor = System.Drawing.Color.Blue;
            this.Label4.Location = new System.Drawing.Point(26, 66);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(389, 29);
            this.Label4.TabIndex = 180;
            this.Label4.Text = "　手直し商品のバーコードを読み取ってください。";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label5.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label5.ForeColor = System.Drawing.Color.Red;
            this.Label5.Location = new System.Drawing.Point(24, 228);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(424, 33);
            this.Label5.TabIndex = 181;
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label16.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label16.Location = new System.Drawing.Point(13, 231);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(179, 35);
            this.Label16.TabIndex = 187;
            this.Label16.Text = "読取りＱＲコード";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LstReadBcrData
            // 
            this.LstReadBcrData.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LstReadBcrData.BackColor = System.Drawing.Color.White;
            this.LstReadBcrData.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstReadBcrData.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LstReadBcrData.FullRowSelect = true;
            this.LstReadBcrData.GridLines = true;
            this.LstReadBcrData.HideSelection = false;
            this.LstReadBcrData.Location = new System.Drawing.Point(4, 282);
            this.LstReadBcrData.MultiSelect = false;
            this.LstReadBcrData.Name = "LstReadBcrData";
            this.LstReadBcrData.Size = new System.Drawing.Size(837, 286);
            this.LstReadBcrData.TabIndex = 192;
            this.LstReadBcrData.UseCompatibleStateImageBehavior = false;
            this.LstReadBcrData.DoubleClick += new System.EventHandler(this.LstReadBcrData_DoubleClick);
            // 
            // TxtReadBcr
            // 
            this.TxtReadBcr.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtReadBcr.Location = new System.Drawing.Point(193, 231);
            this.TxtReadBcr.MaxLength = 9;
            this.TxtReadBcr.Name = "TxtReadBcr";
            this.TxtReadBcr.Size = new System.Drawing.Size(260, 36);
            this.TxtReadBcr.TabIndex = 193;
            this.TxtReadBcr.Text = "210000011";
            this.TxtReadBcr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LstBoxWorkCode
            // 
            this.LstBoxWorkCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LstBoxWorkCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstBoxWorkCode.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstBoxWorkCode.FormattingEnabled = true;
            this.LstBoxWorkCode.ItemHeight = 24;
            this.LstBoxWorkCode.Location = new System.Drawing.Point(842, 63);
            this.LstBoxWorkCode.Name = "LstBoxWorkCode";
            this.LstBoxWorkCode.Size = new System.Drawing.Size(495, 506);
            this.LstBoxWorkCode.TabIndex = 194;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label7.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label7.Location = new System.Drawing.Point(842, 33);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(495, 29);
            this.Label7.TabIndex = 195;
            this.Label7.Text = "束連番：ｾｯﾄ順序番号：組合員ｺｰﾄﾞ：組合員名（判定）";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblForPrint
            // 
            this.LblForPrint.BackColor = System.Drawing.Color.Red;
            this.LblForPrint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblForPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblForPrint.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblForPrint.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblForPrint.Location = new System.Drawing.Point(421, 37);
            this.LblForPrint.Name = "LblForPrint";
            this.LblForPrint.Size = new System.Drawing.Size(420, 47);
            this.LblForPrint.TabIndex = 197;
            this.LblForPrint.Text = "インクジェット印字有り【99/99】";
            this.LblForPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            this.PictureBox1.ErrorImage = null;
            this.PictureBox1.Image = global::TokaiCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.InitialImage = global::TokaiCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.Location = new System.Drawing.Point(4, 568);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(275, 62);
            this.PictureBox1.TabIndex = 198;
            this.PictureBox1.TabStop = false;
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Image = global::TokaiCoopApp.Properties.Resources.exit_icon_small;
            this.BtnClose.Location = new System.Drawing.Point(1184, 575);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(153, 44);
            this.BtnClose.TabIndex = 167;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(292, 582);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(556, 35);
            this.label8.TabIndex = 199;
            this.label8.Text = "※項目をダブルクリックすると選択した束内の帳票一覧を表示します";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LstFormList
            // 
            this.LstFormList.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LstFormList.BackColor = System.Drawing.Color.White;
            this.LstFormList.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstFormList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LstFormList.FullRowSelect = true;
            this.LstFormList.GridLines = true;
            this.LstFormList.HideSelection = false;
            this.LstFormList.Location = new System.Drawing.Point(4, 37);
            this.LstFormList.MultiSelect = false;
            this.LstFormList.Name = "LstFormList";
            this.LstFormList.Size = new System.Drawing.Size(859, 286);
            this.LstFormList.TabIndex = 200;
            this.LstFormList.UseCompatibleStateImageBehavior = false;
            this.LstFormList.DoubleClick += new System.EventHandler(this.LstFormList_DoubleClick);
            // 
            // PnlFormList
            // 
            this.PnlFormList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFormList.Controls.Add(this.BtnOkForFormList);
            this.PnlFormList.Controls.Add(this.BtnCancelForFormList);
            this.PnlFormList.Controls.Add(this.LstFormList);
            this.PnlFormList.Controls.Add(this.label10);
            this.PnlFormList.Controls.Add(this.label9);
            this.PnlFormList.Location = new System.Drawing.Point(241, 126);
            this.PnlFormList.Name = "PnlFormList";
            this.PnlFormList.Size = new System.Drawing.Size(868, 378);
            this.PnlFormList.TabIndex = 201;
            this.PnlFormList.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label9.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(2, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(863, 32);
            this.label9.TabIndex = 133;
            this.label9.Text = "帳票一覧";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(230, 333);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(416, 35);
            this.label10.TabIndex = 200;
            this.label10.Text = "※項目をダブルクリックすると強制的にOKとします";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnCancelForFormList
            // 
            this.BtnCancelForFormList.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnCancelForFormList.Image = global::TokaiCoopApp.Properties.Resources.cancel;
            this.BtnCancelForFormList.Location = new System.Drawing.Point(705, 328);
            this.BtnCancelForFormList.Name = "BtnCancelForFormList";
            this.BtnCancelForFormList.Size = new System.Drawing.Size(153, 44);
            this.BtnCancelForFormList.TabIndex = 201;
            this.BtnCancelForFormList.Text = "ｷｬﾝｾﾙ";
            this.BtnCancelForFormList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancelForFormList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancelForFormList.UseVisualStyleBackColor = true;
            this.BtnCancelForFormList.Click += new System.EventHandler(this.BtnCancelForFormList_Click);
            // 
            // BtnOkForFormList
            // 
            this.BtnOkForFormList.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnOkForFormList.Image = global::TokaiCoopApp.Properties.Resources.check;
            this.BtnOkForFormList.Location = new System.Drawing.Point(7, 326);
            this.BtnOkForFormList.Name = "BtnOkForFormList";
            this.BtnOkForFormList.Size = new System.Drawing.Size(153, 44);
            this.BtnOkForFormList.TabIndex = 202;
            this.BtnOkForFormList.Text = "OK";
            this.BtnOkForFormList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnOkForFormList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnOkForFormList.UseVisualStyleBackColor = true;
            this.BtnOkForFormList.Click += new System.EventHandler(this.BtnOkForFormList_Click);
            // 
            // EntryAdjustForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 631);
            this.ControlBox = false;
            this.Controls.Add(this.PnlFormList);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.LblForPrint);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.LstBoxWorkCode);
            this.Controls.Add(this.TxtReadBcr);
            this.Controls.Add(this.LstReadBcrData);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.TxtBcrInput);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.TxtBcrRead);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.LblTankyo1);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.LblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EntryAdjustForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QＲ手直し商品登録";
            this.Activated += new System.EventHandler(this.EntryAdjustForm_Activated);
            this.Load += new System.EventHandler(this.EntryAdjustForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.PnlFormList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.Label LblTankyo1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TxtBcrRead;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox TxtBcrInput;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.ListView LstReadBcrData;
        internal System.Windows.Forms.TextBox TxtReadBcr;
        internal System.Windows.Forms.ListBox LstBoxWorkCode;
        internal System.Windows.Forms.Label Label7;

        #endregion

        internal System.Windows.Forms.Label LblForPrint;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ListView LstFormList;
        private System.Windows.Forms.Panel PnlFormList;
        internal System.Windows.Forms.Button BtnOkForFormList;
        internal System.Windows.Forms.Button BtnCancelForFormList;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label9;
    }
}