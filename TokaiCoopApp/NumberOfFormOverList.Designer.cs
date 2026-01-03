namespace TokaiCoopApp
{
    partial class NumberOfFormOverList
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
            this.LstNumberOverList = new System.Windows.Forms.ListView();
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LstNumberOverList
            // 
            this.LstNumberOverList.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LstNumberOverList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LstNumberOverList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstNumberOverList.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstNumberOverList.ForeColor = System.Drawing.Color.Black;
            this.LstNumberOverList.FullRowSelect = true;
            this.LstNumberOverList.GridLines = true;
            this.LstNumberOverList.HideSelection = false;
            this.LstNumberOverList.Location = new System.Drawing.Point(1, 51);
            this.LstNumberOverList.MultiSelect = false;
            this.LstNumberOverList.Name = "LstNumberOverList";
            this.LstNumberOverList.Size = new System.Drawing.Size(783, 434);
            this.LstNumberOverList.TabIndex = 270;
            this.LstNumberOverList.UseCompatibleStateImageBehavior = false;
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(784, 50);
            this.LblTitle.TabIndex = 271;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(463, 524);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(142, 24);
            this.LblVersion.TabIndex = 272;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnPrint.Image = global::TokaiCoopApp.Properties.Resources.printer;
            this.BtnPrint.Location = new System.Drawing.Point(11, 503);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(160, 45);
            this.BtnPrint.TabIndex = 196;
            this.BtnPrint.Text = "画面印字";
            this.BtnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Image = global::TokaiCoopApp.Properties.Resources.exit_icon_small;
            this.BtnClose.Location = new System.Drawing.Point(611, 503);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(160, 45);
            this.BtnClose.TabIndex = 164;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // NumberOfFormOverList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.ControlBox = false;
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.LstNumberOverList);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.BtnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumberOfFormOverList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帳票枚数オーバーリスト";
            this.Load += new System.EventHandler(this.NumberOfFormOverList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.Button BtnPrint;
        internal System.Windows.Forms.ListView LstNumberOverList;
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Label LblVersion;
    }
}