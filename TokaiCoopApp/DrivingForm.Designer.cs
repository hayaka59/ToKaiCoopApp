
namespace TokaiCoopApp
{
    partial class DrivingForm
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
            this.components = new System.ComponentModel.Container();
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.LstDrivingData = new System.Windows.Forms.ListView();
            this.BtnClose = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtWorkCode = new System.Windows.Forms.TextBox();
            this.TxtBCRCode = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.LblDateTime = new System.Windows.Forms.Label();
            this.TimDateTime = new System.Windows.Forms.Timer(this.components);
            this.TimCheckSeisanLog = new System.Windows.Forms.Timer(this.components);
            this.LblReadStatus = new System.Windows.Forms.Label();
            this.SerialPortBcr = new System.IO.Ports.SerialPort(this.components);
            this.LstDrivingError = new System.Windows.Forms.ListView();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.LblAutoMode = new System.Windows.Forms.Label();
            this.LblErrorMessage = new System.Windows.Forms.Label();
            this.TimErrorDispTime = new System.Windows.Forms.Timer(this.components);
            this.TimBuzzer = new System.Windows.Forms.Timer(this.components);
            this.LblDown = new System.Windows.Forms.Label();
            this.LblUp = new System.Windows.Forms.Label();
            this.LsbRet = new System.Windows.Forms.ListBox();
            this.BgEjectThread = new System.ComponentModel.BackgroundWorker();
            this.TimBgEjectThread = new System.Windows.Forms.Timer(this.components);
            this.LblOperationStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1911, 50);
            this.LblTitle.TabIndex = 131;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(1773, 1021);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(112, 24);
            this.LblVersion.TabIndex = 132;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblVersion.DoubleClick += new System.EventHandler(this.LblVersion_DoubleClick);
            // 
            // LstDrivingData
            // 
            this.LstDrivingData.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstDrivingData.FullRowSelect = true;
            this.LstDrivingData.GridLines = true;
            this.LstDrivingData.HideSelection = false;
            this.LstDrivingData.Location = new System.Drawing.Point(47, 154);
            this.LstDrivingData.MultiSelect = false;
            this.LstDrivingData.Name = "LstDrivingData";
            this.LstDrivingData.Size = new System.Drawing.Size(1864, 538);
            this.LstDrivingData.TabIndex = 133;
            this.LstDrivingData.UseCompatibleStateImageBehavior = false;
            this.LstDrivingData.DoubleClick += new System.EventHandler(this.LstDrivingData_DoubleClick);
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Location = new System.Drawing.Point(1783, 94);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(114, 47);
            this.BtnClose.TabIndex = 136;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label1.Font = new System.Drawing.Font("メイリオ", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.Location = new System.Drawing.Point(33, 61);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(222, 79);
            this.Label1.TabIndex = 137;
            this.Label1.Text = "ｾｯﾄ順序番号";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtWorkCode
            // 
            this.TxtWorkCode.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtWorkCode.Location = new System.Drawing.Point(256, 62);
            this.TxtWorkCode.Name = "TxtWorkCode";
            this.TxtWorkCode.Size = new System.Drawing.Size(315, 79);
            this.TxtWorkCode.TabIndex = 138;
            this.TxtWorkCode.Text = "123456789";
            this.TxtWorkCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtWorkCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtWorkCode_KeyPress);
            // 
            // TxtBCRCode
            // 
            this.TxtBCRCode.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBCRCode.Location = new System.Drawing.Point(751, 61);
            this.TxtBCRCode.Name = "TxtBCRCode";
            this.TxtBCRCode.Size = new System.Drawing.Size(397, 79);
            this.TxtBCRCode.TabIndex = 140;
            this.TxtBCRCode.Text = "a450529451-d";
            this.TxtBCRCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtBCRCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBCRCode_KeyPress);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label2.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.Label2.Location = new System.Drawing.Point(585, 61);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(165, 79);
            this.Label2.TabIndex = 139;
            this.Label2.Text = "QR";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label3.Font = new System.Drawing.Font("メイリオ", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label3.Location = new System.Drawing.Point(1154, 61);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(254, 79);
            this.Label3.TabIndex = 141;
            this.Label3.Text = "QR読取可能";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label5.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label5.Location = new System.Drawing.Point(1414, 94);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(226, 46);
            this.Label5.TabIndex = 143;
            this.Label5.Text = "生産ログ読取状況";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDateTime
            // 
            this.LblDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblDateTime.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblDateTime.ForeColor = System.Drawing.Color.Yellow;
            this.LblDateTime.Location = new System.Drawing.Point(1462, 2);
            this.LblDateTime.Name = "LblDateTime";
            this.LblDateTime.Size = new System.Drawing.Size(436, 46);
            this.LblDateTime.TabIndex = 144;
            this.LblDateTime.Text = "年月日時分秒";
            this.LblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimDateTime
            // 
            this.TimDateTime.Tick += new System.EventHandler(this.TimDateTime_Tick);
            // 
            // TimCheckSeisanLog
            // 
            this.TimCheckSeisanLog.Tick += new System.EventHandler(this.TimCheckSeisanLog_Tick);
            // 
            // LblReadStatus
            // 
            this.LblReadStatus.BackColor = System.Drawing.Color.White;
            this.LblReadStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblReadStatus.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblReadStatus.Location = new System.Drawing.Point(1641, 94);
            this.LblReadStatus.Name = "LblReadStatus";
            this.LblReadStatus.Size = new System.Drawing.Size(136, 46);
            this.LblReadStatus.TabIndex = 145;
            this.LblReadStatus.Text = "正常";
            this.LblReadStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SerialPortBcr
            // 
            this.SerialPortBcr.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPortBcr_DataReceived);
            // 
            // LstDrivingError
            // 
            this.LstDrivingError.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstDrivingError.FullRowSelect = true;
            this.LstDrivingError.GridLines = true;
            this.LstDrivingError.HideSelection = false;
            this.LstDrivingError.Location = new System.Drawing.Point(47, 705);
            this.LstDrivingError.MultiSelect = false;
            this.LstDrivingError.Name = "LstDrivingError";
            this.LstDrivingError.Size = new System.Drawing.Size(1864, 308);
            this.LstDrivingError.TabIndex = 146;
            this.LstDrivingError.UseCompatibleStateImageBehavior = false;
            this.LstDrivingError.DoubleClick += new System.EventHandler(this.LstDrivingError_DoubleClick);
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label4.Font = new System.Drawing.Font("メイリオ", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label4.Location = new System.Drawing.Point(11, 154);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(35, 538);
            this.Label4.TabIndex = 147;
            this.Label4.Text = "ＯＫ束";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label6.Font = new System.Drawing.Font("メイリオ", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label6.Location = new System.Drawing.Point(11, 705);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(35, 308);
            this.Label6.TabIndex = 148;
            this.Label6.Text = "ＮＧ束";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblAutoMode
            // 
            this.LblAutoMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LblAutoMode.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblAutoMode.Location = new System.Drawing.Point(1169, 9);
            this.LblAutoMode.Name = "LblAutoMode";
            this.LblAutoMode.Size = new System.Drawing.Size(226, 29);
            this.LblAutoMode.TabIndex = 149;
            this.LblAutoMode.Text = "LblAutoMode";
            this.LblAutoMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblErrorMessage
            // 
            this.LblErrorMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.LblErrorMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LblErrorMessage.Font = new System.Drawing.Font("メイリオ", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblErrorMessage.ForeColor = System.Drawing.Color.Black;
            this.LblErrorMessage.Location = new System.Drawing.Point(136, 322);
            this.LblErrorMessage.Name = "LblErrorMessage";
            this.LblErrorMessage.Size = new System.Drawing.Size(1659, 204);
            this.LblErrorMessage.TabIndex = 150;
            this.LblErrorMessage.Text = "LblErrorMessage";
            this.LblErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimErrorDispTime
            // 
            this.TimErrorDispTime.Tick += new System.EventHandler(this.TimErrorDispTime_Tick);
            // 
            // TimBuzzer
            // 
            this.TimBuzzer.Tick += new System.EventHandler(this.TimBuzzer_Tick);
            // 
            // LblDown
            // 
            this.LblDown.BackColor = System.Drawing.Color.LightGreen;
            this.LblDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDown.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblDown.Location = new System.Drawing.Point(297, 1001);
            this.LblDown.Name = "LblDown";
            this.LblDown.Size = new System.Drawing.Size(85, 41);
            this.LblDown.TabIndex = 265;
            this.LblDown.Text = "123";
            this.LblDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblUp
            // 
            this.LblUp.BackColor = System.Drawing.Color.LightGreen;
            this.LblUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblUp.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblUp.Location = new System.Drawing.Point(206, 1001);
            this.LblUp.Name = "LblUp";
            this.LblUp.Size = new System.Drawing.Size(85, 41);
            this.LblUp.TabIndex = 264;
            this.LblUp.Text = "123";
            this.LblUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LsbRet
            // 
            this.LsbRet.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LsbRet.FormattingEnabled = true;
            this.LsbRet.ItemHeight = 20;
            this.LsbRet.Location = new System.Drawing.Point(388, 958);
            this.LsbRet.Name = "LsbRet";
            this.LsbRet.Size = new System.Drawing.Size(612, 84);
            this.LsbRet.TabIndex = 263;
            // 
            // BgEjectThread
            // 
            this.BgEjectThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgEjectThread_DoWork);
            // 
            // TimBgEjectThread
            // 
            this.TimBgEjectThread.Tick += new System.EventHandler(this.TimBgEjectThread_Tick);
            // 
            // LblOperationStatus
            // 
            this.LblOperationStatus.BackColor = System.Drawing.SystemColors.Control;
            this.LblOperationStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblOperationStatus.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblOperationStatus.ForeColor = System.Drawing.Color.Blue;
            this.LblOperationStatus.Location = new System.Drawing.Point(1414, 55);
            this.LblOperationStatus.Name = "LblOperationStatus";
            this.LblOperationStatus.Size = new System.Drawing.Size(484, 37);
            this.LblOperationStatus.TabIndex = 266;
            this.LblOperationStatus.Text = "LblOperationStatus";
            this.LblOperationStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DrivingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1910, 1048);
            this.ControlBox = false;
            this.Controls.Add(this.LblOperationStatus);
            this.Controls.Add(this.LblDown);
            this.Controls.Add(this.LblUp);
            this.Controls.Add(this.LsbRet);
            this.Controls.Add(this.LblErrorMessage);
            this.Controls.Add(this.LblAutoMode);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.LstDrivingError);
            this.Controls.Add(this.LblReadStatus);
            this.Controls.Add(this.LblDateTime);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.TxtBCRCode);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.TxtWorkCode);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LstDrivingData);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.LblTitle);
            this.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(640, 0);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DrivingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "運転画面";
            this.Load += new System.EventHandler(this.DrivingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.ListView LstDrivingData;
        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TxtWorkCode;
        internal System.Windows.Forms.TextBox TxtBCRCode;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label LblDateTime;
        internal System.Windows.Forms.Timer TimDateTime;
        internal System.Windows.Forms.Timer TimCheckSeisanLog;
        internal System.Windows.Forms.Label LblReadStatus;
        internal System.IO.Ports.SerialPort SerialPortBcr;
        internal System.Windows.Forms.ListView LstDrivingError;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label LblAutoMode;
        internal System.Windows.Forms.Label LblErrorMessage;
        internal System.Windows.Forms.Timer TimErrorDispTime;
        internal System.Windows.Forms.Timer TimBuzzer;
        internal System.Windows.Forms.Label LblDown;
        internal System.Windows.Forms.Label LblUp;
        internal System.Windows.Forms.ListBox LsbRet;
        internal System.ComponentModel.BackgroundWorker BgEjectThread;
        internal System.Windows.Forms.Timer TimBgEjectThread;

        #endregion

        private System.Windows.Forms.Label LblOperationStatus;
    }
}