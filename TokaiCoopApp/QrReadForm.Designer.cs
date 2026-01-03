
namespace TokaiCoopApp
{
    partial class QrReadForm
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
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TxtQrRead = new System.Windows.Forms.TextBox();
            this.LblQrRead = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CmbCoopName = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CmbDataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LblFoldingCon = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LblMaxBundle = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnConfirm.Image = global::TokaiCoopApp.Properties.Resources.check;
            this.BtnConfirm.Location = new System.Drawing.Point(641, 357);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(130, 52);
            this.BtnConfirm.TabIndex = 2;
            this.BtnConfirm.Text = "確認";
            this.BtnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnCancel.Image = global::TokaiCoopApp.Properties.Resources.cancel;
            this.BtnCancel.Location = new System.Drawing.Point(777, 357);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(141, 52);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // TxtQrRead
            // 
            this.TxtQrRead.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtQrRead.Location = new System.Drawing.Point(21, 27);
            this.TxtQrRead.Name = "TxtQrRead";
            this.TxtQrRead.Size = new System.Drawing.Size(888, 31);
            this.TxtQrRead.TabIndex = 1;
            // 
            // LblQrRead
            // 
            this.LblQrRead.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblQrRead.Location = new System.Drawing.Point(24, 73);
            this.LblQrRead.Name = "LblQrRead";
            this.LblQrRead.Size = new System.Drawing.Size(879, 266);
            this.LblQrRead.TabIndex = 3;
            this.LblQrRead.Text = "LblQrRead";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightGray;
            this.groupBox3.Controls.Add(this.CmbCoopName);
            this.groupBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(6, 357);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(215, 62);
            this.groupBox3.TabIndex = 253;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "処理生協";
            // 
            // CmbCoopName
            // 
            this.CmbCoopName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCoopName.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbCoopName.FormattingEnabled = true;
            this.CmbCoopName.IntegralHeight = false;
            this.CmbCoopName.Location = new System.Drawing.Point(14, 18);
            this.CmbCoopName.MaxDropDownItems = 10;
            this.CmbCoopName.Name = "CmbCoopName";
            this.CmbCoopName.Size = new System.Drawing.Size(193, 32);
            this.CmbCoopName.TabIndex = 251;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.LightGray;
            this.groupBox5.Controls.Add(this.CmbDataType);
            this.groupBox5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox5.ForeColor = System.Drawing.Color.Red;
            this.groupBox5.Location = new System.Drawing.Point(224, 356);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(137, 62);
            this.groupBox5.TabIndex = 258;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "データ種別";
            // 
            // CmbDataType
            // 
            this.CmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDataType.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CmbDataType.FormattingEnabled = true;
            this.CmbDataType.IntegralHeight = false;
            this.CmbDataType.Location = new System.Drawing.Point(16, 18);
            this.CmbDataType.MaxDropDownItems = 10;
            this.CmbDataType.Name = "CmbDataType";
            this.CmbDataType.Size = new System.Drawing.Size(104, 32);
            this.CmbDataType.TabIndex = 251;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(0, 345);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(934, 83);
            this.label2.TabIndex = 261;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.LightGray;
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.LblFoldingCon);
            this.groupBox6.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox6.ForeColor = System.Drawing.Color.Red;
            this.groupBox6.Location = new System.Drawing.Point(505, 354);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(130, 64);
            this.groupBox6.TabIndex = 264;
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
            this.LblFoldingCon.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LblFoldingCon.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblFoldingCon.Location = new System.Drawing.Point(6, 25);
            this.LblFoldingCon.Name = "LblFoldingCon";
            this.LblFoldingCon.Size = new System.Drawing.Size(88, 24);
            this.LblFoldingCon.TabIndex = 2;
            this.LblFoldingCon.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.LightGray;
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.LblMaxBundle);
            this.groupBox7.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(367, 354);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(130, 64);
            this.groupBox7.TabIndex = 263;
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
            this.LblMaxBundle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LblMaxBundle.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblMaxBundle.Location = new System.Drawing.Point(6, 25);
            this.LblMaxBundle.Name = "LblMaxBundle";
            this.LblMaxBundle.Size = new System.Drawing.Size(88, 24);
            this.LblMaxBundle.TabIndex = 1;
            this.LblMaxBundle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // QrReadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 422);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.LblQrRead);
            this.Controls.Add(this.TxtQrRead);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "QrReadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ＱＲ読込画面";
            this.Load += new System.EventHandler(this.QrReadForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QrReadForm_KeyPress);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.TextBox TxtQrRead;
        private System.Windows.Forms.Label LblQrRead;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.ComboBox CmbCoopName;
        private System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.ComboBox CmbDataType;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.GroupBox groupBox6;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label LblFoldingCon;
        internal System.Windows.Forms.GroupBox groupBox7;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label LblMaxBundle;
    }
}