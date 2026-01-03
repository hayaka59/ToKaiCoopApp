
namespace TokaiCoopApp
{
    partial class PasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordForm));
            this.MskTxtPassword = new System.Windows.Forms.MaskedTextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MskTxtPassword
            // 
            this.MskTxtPassword.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MskTxtPassword.Location = new System.Drawing.Point(174, 18);
            this.MskTxtPassword.Name = "MskTxtPassword";
            this.MskTxtPassword.PasswordChar = '*';
            this.MskTxtPassword.Size = new System.Drawing.Size(164, 39);
            this.MskTxtPassword.TabIndex = 0;
            this.MskTxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Blue;
            this.Label12.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label12.ForeColor = System.Drawing.Color.White;
            this.Label12.Location = new System.Drawing.Point(14, 18);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(160, 39);
            this.Label12.TabIndex = 111;
            this.Label12.Text = "パスワード";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnOK
            // 
            this.BtnOK.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnOK.Image = global::TokaiCoopApp.Properties.Resources.check;
            this.BtnOK.Location = new System.Drawing.Point(14, 66);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(160, 50);
            this.BtnOK.TabIndex = 112;
            this.BtnOK.Text = "ＯＫ";
            this.BtnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnCancel.Image = global::TokaiCoopApp.Properties.Resources.cancel;
            this.BtnCancel.Location = new System.Drawing.Point(180, 66);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(160, 50);
            this.BtnCancel.TabIndex = 113;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 131);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.MskTxtPassword);
            this.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "パスワード入力";
            this.Load += new System.EventHandler(this.PasswordForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PasswordForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.MaskedTextBox MskTxtPassword;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Button BtnOK;
        internal System.Windows.Forms.Button BtnCancel;

        #endregion
    }
}
