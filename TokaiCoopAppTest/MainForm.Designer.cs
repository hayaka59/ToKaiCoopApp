namespace GreenCoopAppTest
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SerialPortQr = new System.IO.Ports.SerialPort(this.components);
            this.TxtQrReadData = new System.Windows.Forms.TextBox();
            this.LsbQrReadDataZ = new System.Windows.Forms.ListBox();
            this.LblDateTime = new System.Windows.Forms.Label();
            this.TimDateTime = new System.Windows.Forms.Timer(this.components);
            this.LsbQrReadDataY = new System.Windows.Forms.ListBox();
            this.BtnConversion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtQrReadData
            // 
            this.TxtQrReadData.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtQrReadData.Location = new System.Drawing.Point(21, 382);
            this.TxtQrReadData.Name = "TxtQrReadData";
            this.TxtQrReadData.Size = new System.Drawing.Size(750, 31);
            this.TxtQrReadData.TabIndex = 0;
            this.TxtQrReadData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtQrReadData_KeyPress);
            // 
            // LsbQrReadDataZ
            // 
            this.LsbQrReadDataZ.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LsbQrReadDataZ.FormattingEnabled = true;
            this.LsbQrReadDataZ.ItemHeight = 24;
            this.LsbQrReadDataZ.Location = new System.Drawing.Point(21, 73);
            this.LsbQrReadDataZ.Name = "LsbQrReadDataZ";
            this.LsbQrReadDataZ.Size = new System.Drawing.Size(362, 292);
            this.LsbQrReadDataZ.TabIndex = 1;
            // 
            // LblDateTime
            // 
            this.LblDateTime.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblDateTime.Location = new System.Drawing.Point(427, 19);
            this.LblDateTime.Name = "LblDateTime";
            this.LblDateTime.Size = new System.Drawing.Size(344, 34);
            this.LblDateTime.TabIndex = 2;
            this.LblDateTime.Text = "LblDateTime";
            this.LblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimDateTime
            // 
            this.TimDateTime.Tick += new System.EventHandler(this.TimDateTime_Tick);
            // 
            // LsbQrReadDataY
            // 
            this.LsbQrReadDataY.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LsbQrReadDataY.FormattingEnabled = true;
            this.LsbQrReadDataY.ItemHeight = 24;
            this.LsbQrReadDataY.Location = new System.Drawing.Point(399, 73);
            this.LsbQrReadDataY.Name = "LsbQrReadDataY";
            this.LsbQrReadDataY.Size = new System.Drawing.Size(372, 292);
            this.LsbQrReadDataY.TabIndex = 3;
            // 
            // BtnConversion
            // 
            this.BtnConversion.Location = new System.Drawing.Point(676, 419);
            this.BtnConversion.Name = "BtnConversion";
            this.BtnConversion.Size = new System.Drawing.Size(95, 23);
            this.BtnConversion.TabIndex = 4;
            this.BtnConversion.Text = "画像変換";
            this.BtnConversion.UseVisualStyleBackColor = true;
            this.BtnConversion.Click += new System.EventHandler(this.BtnConversion_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnConversion);
            this.Controls.Add(this.LsbQrReadDataY);
            this.Controls.Add(this.LblDateTime);
            this.Controls.Add(this.LsbQrReadDataZ);
            this.Controls.Add(this.TxtQrReadData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "グリーンコープアプリ用テストツール";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.IO.Ports.SerialPort SerialPortQr;
        private System.Windows.Forms.TextBox TxtQrReadData;
        private System.Windows.Forms.ListBox LsbQrReadDataZ;
        private System.Windows.Forms.Label LblDateTime;
        private System.Windows.Forms.Timer TimDateTime;
        private System.Windows.Forms.ListBox LsbQrReadDataY;
        private System.Windows.Forms.Button BtnConversion;
    }
}

