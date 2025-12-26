namespace GreenCoopApp
{
    partial class DepotOrderSettingForm
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
            this.LblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GrpDepo = new System.Windows.Forms.GroupBox();
            this.LsvDepo = new System.Windows.Forms.ListView();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.BtnUpDepo = new System.Windows.Forms.Button();
            this.BtnDownDepo = new System.Windows.Forms.Button();
            this.GrpDepo.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblMessage
            // 
            this.LblMessage.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblMessage.Location = new System.Drawing.Point(27, 9);
            this.LblMessage.Name = "LblMessage";
            this.LblMessage.Size = new System.Drawing.Size(308, 29);
            this.LblMessage.TabIndex = 5;
            this.LblMessage.Text = "下記のデポコードが見つかりました。";
            this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(27, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "デポ順序を変更して下さい。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GrpDepo
            // 
            this.GrpDepo.Controls.Add(this.LsvDepo);
            this.GrpDepo.Controls.Add(this.BtnConfirm);
            this.GrpDepo.Controls.Add(this.BtnUpDepo);
            this.GrpDepo.Controls.Add(this.BtnDownDepo);
            this.GrpDepo.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GrpDepo.Location = new System.Drawing.Point(31, 75);
            this.GrpDepo.Name = "GrpDepo";
            this.GrpDepo.Size = new System.Drawing.Size(468, 358);
            this.GrpDepo.TabIndex = 15;
            this.GrpDepo.TabStop = false;
            this.GrpDepo.Text = "デポコード";
            // 
            // LsvDepo
            // 
            this.LsvDepo.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LsvDepo.FullRowSelect = true;
            this.LsvDepo.GridLines = true;
            this.LsvDepo.HideSelection = false;
            this.LsvDepo.Location = new System.Drawing.Point(13, 26);
            this.LsvDepo.Name = "LsvDepo";
            this.LsvDepo.Size = new System.Drawing.Size(315, 315);
            this.LsvDepo.TabIndex = 16;
            this.LsvDepo.UseCompatibleStateImageBehavior = false;
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnConfirm.Image = global::GreenCoopApp.Properties.Resources.check;
            this.BtnConfirm.Location = new System.Drawing.Point(343, 297);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(114, 45);
            this.BtnConfirm.TabIndex = 4;
            this.BtnConfirm.Text = "確定";
            this.BtnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // BtnUpDepo
            // 
            this.BtnUpDepo.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnUpDepo.Image = global::GreenCoopApp.Properties.Resources.up2;
            this.BtnUpDepo.Location = new System.Drawing.Point(343, 26);
            this.BtnUpDepo.Name = "BtnUpDepo";
            this.BtnUpDepo.Size = new System.Drawing.Size(114, 45);
            this.BtnUpDepo.TabIndex = 12;
            this.BtnUpDepo.Text = "上へ";
            this.BtnUpDepo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnUpDepo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnUpDepo.UseVisualStyleBackColor = true;
            this.BtnUpDepo.Click += new System.EventHandler(this.BtnUpDepo_Click);
            // 
            // BtnDownDepo
            // 
            this.BtnDownDepo.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnDownDepo.Image = global::GreenCoopApp.Properties.Resources.down2;
            this.BtnDownDepo.Location = new System.Drawing.Point(343, 91);
            this.BtnDownDepo.Name = "BtnDownDepo";
            this.BtnDownDepo.Size = new System.Drawing.Size(114, 45);
            this.BtnDownDepo.TabIndex = 13;
            this.BtnDownDepo.Text = "下へ";
            this.BtnDownDepo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnDownDepo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnDownDepo.UseVisualStyleBackColor = true;
            this.BtnDownDepo.Click += new System.EventHandler(this.BtnDownDepo_Click);
            // 
            // DepotOrderSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 445);
            this.ControlBox = false;
            this.Controls.Add(this.GrpDepo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DepotOrderSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "デポ順の設定";
            this.Load += new System.EventHandler(this.DepotOrderSettingForm_Load);
            this.GrpDepo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblMessage;
        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnDownDepo;
        private System.Windows.Forms.Button BtnUpDepo;
        private System.Windows.Forms.GroupBox GrpDepo;
        private System.Windows.Forms.ListView LsvDepo;
    }
}