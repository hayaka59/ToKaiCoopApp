namespace GreenCoopApp
{
    partial class BasketCarCheckForm
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
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblTitle = new System.Windows.Forms.Label();
            this.LstBasketCarCheck = new System.Windows.Forms.ListView();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(1083, 717);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(130, 25);
            this.LblVersion.TabIndex = 200;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblVersion.Click += new System.EventHandler(this.LblVersion_Click);
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(1, -2);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1383, 32);
            this.LblTitle.TabIndex = 199;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LstBasketCarCheck
            // 
            this.LstBasketCarCheck.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.LstBasketCarCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstBasketCarCheck.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstBasketCarCheck.FullRowSelect = true;
            this.LstBasketCarCheck.GridLines = true;
            this.LstBasketCarCheck.HideSelection = false;
            this.LstBasketCarCheck.Location = new System.Drawing.Point(12, 64);
            this.LstBasketCarCheck.MultiSelect = false;
            this.LstBasketCarCheck.Name = "LstBasketCarCheck";
            this.LstBasketCarCheck.Size = new System.Drawing.Size(1360, 582);
            this.LstBasketCarCheck.TabIndex = 202;
            this.LstBasketCarCheck.UseCompatibleStateImageBehavior = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.ErrorImage = null;
            this.PictureBox1.Image = global::GreenCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.InitialImage = global::GreenCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.Location = new System.Drawing.Point(6, 697);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(275, 62);
            this.PictureBox1.TabIndex = 201;
            this.PictureBox1.TabStop = false;
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Image = global::GreenCoopApp.Properties.Resources.exit_icon_small;
            this.BtnClose.Location = new System.Drawing.Point(1219, 705);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(153, 44);
            this.BtnClose.TabIndex = 168;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BasketCarCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Controls.Add(this.LstBasketCarCheck);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.BtnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasketCarCheckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "カゴ車セットチェック画面";
            this.Load += new System.EventHandler(this.BasketCarCheckForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.ListView LstBasketCarCheck;
    }
}