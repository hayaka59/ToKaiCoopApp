namespace TokaiCoopApp
{
    partial class ShelfChangeForm
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
            this.ChkdLstShelfChange = new System.Windows.Forms.CheckedListBox();
            this.LblMessage = new System.Windows.Forms.Label();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChkdLstShelfChange
            // 
            this.ChkdLstShelfChange.CheckOnClick = true;
            this.ChkdLstShelfChange.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ChkdLstShelfChange.FormattingEnabled = true;
            this.ChkdLstShelfChange.HorizontalScrollbar = true;
            this.ChkdLstShelfChange.Location = new System.Drawing.Point(27, 63);
            this.ChkdLstShelfChange.Name = "ChkdLstShelfChange";
            this.ChkdLstShelfChange.Size = new System.Drawing.Size(429, 290);
            this.ChkdLstShelfChange.TabIndex = 0;
            // 
            // LblMessage
            // 
            this.LblMessage.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblMessage.Location = new System.Drawing.Point(23, 9);
            this.LblMessage.Name = "LblMessage";
            this.LblMessage.Size = new System.Drawing.Size(449, 51);
            this.LblMessage.TabIndex = 2;
            this.LblMessage.Text = "下記のセンターコード（仮倉庫コード）が見つかりました。棚替えするセンターコードにチェックを入れてください。";
            this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnConfirm.Image = global::TokaiCoopApp.Properties.Resources.check;
            this.BtnConfirm.Location = new System.Drawing.Point(342, 359);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(114, 45);
            this.BtnConfirm.TabIndex = 1;
            this.BtnConfirm.Text = "確定";
            this.BtnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // ShelfChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.ControlBox = false;
            this.Controls.Add(this.LblMessage);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.ChkdLstShelfChange);
            this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShelfChangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "棚替えフラグの設定";
            this.Load += new System.EventHandler(this.ShelfChangeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ChkdLstShelfChange;
        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.Label LblMessage;
    }
}