
namespace GreenCoopApp
{
    partial class EndForm
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

        // メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
        // Windows フォーム デザイナーを使用して変更できます。  
        // コード エディターを使って変更しないでください。
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkShutDown = new System.Windows.Forms.CheckBox();
            this.BtnEnd = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ChkShutDown);
            this.GroupBox1.Location = new System.Drawing.Point(63, 88);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(454, 86);
            this.GroupBox1.TabIndex = 96;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "終了オプション";
            // 
            // ChkShutDown
            // 
            this.ChkShutDown.Location = new System.Drawing.Point(22, 30);
            this.ChkShutDown.Name = "ChkShutDown";
            this.ChkShutDown.Size = new System.Drawing.Size(408, 37);
            this.ChkShutDown.TabIndex = 96;
            this.ChkShutDown.Text = "ソフト終了後、Windowsをシャットダウンする。";
            this.ChkShutDown.UseVisualStyleBackColor = true;
            // 
            // BtnEnd
            // 
            this.BtnEnd.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnEnd.Image = global::GreenCoopApp.Properties.Resources.running_icon;
            this.BtnEnd.Location = new System.Drawing.Point(63, 193);
            this.BtnEnd.Name = "BtnEnd";
            this.BtnEnd.Size = new System.Drawing.Size(220, 50);
            this.BtnEnd.TabIndex = 121;
            this.BtnEnd.Text = "終了実行";
            this.BtnEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnEnd.UseVisualStyleBackColor = true;
            this.BtnEnd.Click += new System.EventHandler(this.BtnEnd_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnCancel.Image = global::GreenCoopApp.Properties.Resources.cancel;
            this.BtnCancel.Location = new System.Drawing.Point(322, 193);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(220, 50);
            this.BtnCancel.TabIndex = 122;
            this.BtnCancel.Text = "キャンセル";
            this.BtnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Image = global::GreenCoopApp.Properties.Resources.poweroff_icon;
            this.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label7.Location = new System.Drawing.Point(23, 19);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(534, 53);
            this.Label7.TabIndex = 94;
            this.Label7.Text = "終了実行してもよろしいでしょうか？";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.ControlBox = false;
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnEnd);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label7);
            this.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "終了メニュー";
            this.Load += new System.EventHandler(this.EndForm_Load);
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.CheckBox ChkShutDown;
        internal System.Windows.Forms.Button BtnEnd;
        internal System.Windows.Forms.Button BtnCancel;

        #endregion
    }
}

