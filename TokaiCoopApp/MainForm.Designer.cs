using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace TokaiCoopApp
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
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.SerialPort = new System.IO.Ports.SerialPort(this.components);
            this.TimPcReadyOn = new System.Windows.Forms.Timer(this.components);
            this.BtnDifferent = new System.Windows.Forms.Button();
            this.BtnNG = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnVoice2Start = new System.Windows.Forms.Button();
            this.BtnVoice1Start = new System.Windows.Forms.Button();
            this.BtnEnvironmentalSound = new System.Windows.Forms.Button();
            this.BtnDepot = new System.Windows.Forms.Button();
            this.BtnRootNo = new System.Windows.Forms.Button();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LblDateTime = new System.Windows.Forms.Label();
            this.TimDateTime = new System.Windows.Forms.Timer(this.components);
            this.BtnProductionLogCheck = new System.Windows.Forms.Button();
            this.BtnActualProduction = new System.Windows.Forms.Button();
            this.BtnReport = new System.Windows.Forms.Button();
            this.BtnMaintenance = new System.Windows.Forms.Button();
            this.BtnReadCollatingData = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnAdjust = new System.Windows.Forms.Button();
            this.BtnEnd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1904, 50);
            this.LblTitle.TabIndex = 7;
            this.LblTitle.Text = "LblTitle";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblVersion
            // 
            this.LblVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblVersion.ForeColor = System.Drawing.Color.Blue;
            this.LblVersion.Location = new System.Drawing.Point(1700, 1000);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(150, 25);
            this.LblVersion.TabIndex = 9;
            this.LblVersion.Text = "LblVersion";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblVersion.Click += new System.EventHandler(this.LblVersion_DoubleClick);
            // 
            // SerialPort
            // 
            this.SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort_DataReceived);
            // 
            // TimPcReadyOn
            // 
            this.TimPcReadyOn.Tick += new System.EventHandler(this.TimPcReadyOn_Tick);
            // 
            // BtnDifferent
            // 
            this.BtnDifferent.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnDifferent.Location = new System.Drawing.Point(700, 1001);
            this.BtnDifferent.Name = "BtnDifferent";
            this.BtnDifferent.Size = new System.Drawing.Size(220, 36);
            this.BtnDifferent.TabIndex = 9;
            this.BtnDifferent.Text = "〜と〜が異なります";
            this.BtnDifferent.UseVisualStyleBackColor = true;
            this.BtnDifferent.Visible = false;
            this.BtnDifferent.Click += new System.EventHandler(this.BtnDifferent_Click);
            // 
            // BtnNG
            // 
            this.BtnNG.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnNG.Location = new System.Drawing.Point(474, 1001);
            this.BtnNG.Name = "BtnNG";
            this.BtnNG.Size = new System.Drawing.Size(220, 36);
            this.BtnNG.TabIndex = 10;
            this.BtnNG.Text = "ＮＧ音";
            this.BtnNG.UseVisualStyleBackColor = true;
            this.BtnNG.Visible = false;
            this.BtnNG.Click += new System.EventHandler(this.BtnNG_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnOK.Location = new System.Drawing.Point(248, 1001);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(220, 36);
            this.BtnOK.TabIndex = 7;
            this.BtnOK.Text = "ＯＫ音";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Visible = false;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnVoice2Start
            // 
            this.BtnVoice2Start.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnVoice2Start.Location = new System.Drawing.Point(6, 962);
            this.BtnVoice2Start.Name = "BtnVoice2Start";
            this.BtnVoice2Start.Size = new System.Drawing.Size(147, 36);
            this.BtnVoice2Start.TabIndex = 8;
            this.BtnVoice2Start.Text = "コース変わります";
            this.BtnVoice2Start.UseVisualStyleBackColor = true;
            this.BtnVoice2Start.Visible = false;
            this.BtnVoice2Start.Click += new System.EventHandler(this.BtnVoice2Start_Click);
            // 
            // BtnVoice1Start
            // 
            this.BtnVoice1Start.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnVoice1Start.Location = new System.Drawing.Point(22, 1001);
            this.BtnVoice1Start.Name = "BtnVoice1Start";
            this.BtnVoice1Start.Size = new System.Drawing.Size(220, 36);
            this.BtnVoice1Start.TabIndex = 11;
            this.BtnVoice1Start.Text = "センターが変わりました";
            this.BtnVoice1Start.UseVisualStyleBackColor = true;
            this.BtnVoice1Start.Visible = false;
            this.BtnVoice1Start.Click += new System.EventHandler(this.BtnVoice1Start_Click);
            // 
            // BtnEnvironmentalSound
            // 
            this.BtnEnvironmentalSound.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnEnvironmentalSound.Location = new System.Drawing.Point(926, 1001);
            this.BtnEnvironmentalSound.Name = "BtnEnvironmentalSound";
            this.BtnEnvironmentalSound.Size = new System.Drawing.Size(220, 36);
            this.BtnEnvironmentalSound.TabIndex = 12;
            this.BtnEnvironmentalSound.Text = "環境音";
            this.BtnEnvironmentalSound.UseVisualStyleBackColor = true;
            this.BtnEnvironmentalSound.Visible = false;
            this.BtnEnvironmentalSound.Click += new System.EventHandler(this.BtnEnvironmentalSound_Click);
            // 
            // BtnDepot
            // 
            this.BtnDepot.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnDepot.Location = new System.Drawing.Point(6, 922);
            this.BtnDepot.Name = "BtnDepot";
            this.BtnDepot.Size = new System.Drawing.Size(147, 36);
            this.BtnDepot.TabIndex = 22;
            this.BtnDepot.Text = "デポ変わります";
            this.BtnDepot.UseVisualStyleBackColor = true;
            this.BtnDepot.Visible = false;
            this.BtnDepot.Click += new System.EventHandler(this.BtnDepot_Click);
            // 
            // BtnRootNo
            // 
            this.BtnRootNo.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnRootNo.Location = new System.Drawing.Point(6, 881);
            this.BtnRootNo.Name = "BtnRootNo";
            this.BtnRootNo.Size = new System.Drawing.Size(147, 36);
            this.BtnRootNo.TabIndex = 23;
            this.BtnRootNo.Text = "生協変わります";
            this.BtnRootNo.UseVisualStyleBackColor = true;
            this.BtnRootNo.Visible = false;
            this.BtnRootNo.Click += new System.EventHandler(this.BtnCoop_Click);
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.Color.LightCoral;
            this.LblStatus.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblStatus.ForeColor = System.Drawing.Color.Blue;
            this.LblStatus.Location = new System.Drawing.Point(260, 935);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(1400, 62);
            this.LblStatus.TabIndex = 264;
            this.LblStatus.Text = "LblStatus";
            this.LblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblStatus.Visible = false;
            // 
            // LblDateTime
            // 
            this.LblDateTime.Font = new System.Drawing.Font("メイリオ", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblDateTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.LblDateTime.Location = new System.Drawing.Point(1140, 66);
            this.LblDateTime.Name = "LblDateTime";
            this.LblDateTime.Size = new System.Drawing.Size(643, 46);
            this.LblDateTime.TabIndex = 265;
            this.LblDateTime.Text = "LblDateTime";
            this.LblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TimDateTime
            // 
            this.TimDateTime.Tick += new System.EventHandler(this.TimDateTime_Tick);
            // 
            // BtnProductionLogCheck
            // 
            this.BtnProductionLogCheck.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnProductionLogCheck.Image = global::TokaiCoopApp.Properties.Resources.note_big;
            this.BtnProductionLogCheck.Location = new System.Drawing.Point(121, 649);
            this.BtnProductionLogCheck.Name = "BtnProductionLogCheck";
            this.BtnProductionLogCheck.Size = new System.Drawing.Size(550, 255);
            this.BtnProductionLogCheck.TabIndex = 266;
            this.BtnProductionLogCheck.Text = "生産ログチェック";
            this.BtnProductionLogCheck.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnProductionLogCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnProductionLogCheck.UseVisualStyleBackColor = true;
            this.BtnProductionLogCheck.Click += new System.EventHandler(this.BtnProductionLogCheck_Click);
            // 
            // BtnActualProduction
            // 
            this.BtnActualProduction.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnActualProduction.Image = global::TokaiCoopApp.Properties.Resources.graph_icon;
            this.BtnActualProduction.Location = new System.Drawing.Point(120, 385);
            this.BtnActualProduction.Name = "BtnActualProduction";
            this.BtnActualProduction.Size = new System.Drawing.Size(550, 255);
            this.BtnActualProduction.TabIndex = 3;
            this.BtnActualProduction.Text = "生産実績閲覧";
            this.BtnActualProduction.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnActualProduction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnActualProduction.UseVisualStyleBackColor = true;
            this.BtnActualProduction.Click += new System.EventHandler(this.BtnActualProduction_Click);
            // 
            // BtnReport
            // 
            this.BtnReport.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnReport.Image = global::TokaiCoopApp.Properties.Resources.report_icon;
            this.BtnReport.Location = new System.Drawing.Point(1233, 385);
            this.BtnReport.Name = "BtnReport";
            this.BtnReport.Size = new System.Drawing.Size(550, 255);
            this.BtnReport.TabIndex = 4;
            this.BtnReport.Text = "日報作成";
            this.BtnReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnReport.UseVisualStyleBackColor = true;
            this.BtnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // BtnMaintenance
            // 
            this.BtnMaintenance.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnMaintenance.Image = global::TokaiCoopApp.Properties.Resources.maintenance_icon;
            this.BtnMaintenance.Location = new System.Drawing.Point(677, 649);
            this.BtnMaintenance.Name = "BtnMaintenance";
            this.BtnMaintenance.Size = new System.Drawing.Size(550, 255);
            this.BtnMaintenance.TabIndex = 5;
            this.BtnMaintenance.Text = "保守";
            this.BtnMaintenance.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnMaintenance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnMaintenance.UseVisualStyleBackColor = true;
            this.BtnMaintenance.Click += new System.EventHandler(this.BtnMaintenance_Click);
            // 
            // BtnReadCollatingData
            // 
            this.BtnReadCollatingData.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnReadCollatingData.Image = global::TokaiCoopApp.Properties.Resources.read_data_icon;
            this.BtnReadCollatingData.Location = new System.Drawing.Point(120, 132);
            this.BtnReadCollatingData.Name = "BtnReadCollatingData";
            this.BtnReadCollatingData.Size = new System.Drawing.Size(1663, 245);
            this.BtnReadCollatingData.TabIndex = 1;
            this.BtnReadCollatingData.Text = "丁合指示データ読込";
            this.BtnReadCollatingData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnReadCollatingData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnReadCollatingData.UseVisualStyleBackColor = true;
            this.BtnReadCollatingData.Click += new System.EventHandler(this.BtnReadCollatingData_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.ErrorImage = null;
            this.PictureBox1.Image = global::TokaiCoopApp.Properties.Resources.TokaiCoopLogo;
            this.PictureBox1.InitialImage = global::TokaiCoopApp.Properties.Resources.GreenCoopLogo;
            this.PictureBox1.Location = new System.Drawing.Point(121, 62);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(347, 62);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 21;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.DoubleClick += new System.EventHandler(this.PictureBox1_DoubleClick);
            // 
            // BtnAdjust
            // 
            this.BtnAdjust.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnAdjust.Image = global::TokaiCoopApp.Properties.Resources.Correction_icon;
            this.BtnAdjust.Location = new System.Drawing.Point(676, 385);
            this.BtnAdjust.Name = "BtnAdjust";
            this.BtnAdjust.Size = new System.Drawing.Size(550, 255);
            this.BtnAdjust.TabIndex = 2;
            this.BtnAdjust.Text = "手直し品登録";
            this.BtnAdjust.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnAdjust.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnAdjust.UseVisualStyleBackColor = true;
            this.BtnAdjust.Click += new System.EventHandler(this.BtnAdjust_Click);
            // 
            // BtnEnd
            // 
            this.BtnEnd.Font = new System.Drawing.Font("メイリオ", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnEnd.Image = global::TokaiCoopApp.Properties.Resources.exit_icon;
            this.BtnEnd.Location = new System.Drawing.Point(1233, 649);
            this.BtnEnd.Name = "BtnEnd";
            this.BtnEnd.Size = new System.Drawing.Size(550, 255);
            this.BtnEnd.TabIndex = 6;
            this.BtnEnd.Text = "終了";
            this.BtnEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnEnd.UseVisualStyleBackColor = true;
            this.BtnEnd.Click += new System.EventHandler(this.BtnEnd_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.ControlBox = false;
            this.Controls.Add(this.BtnProductionLogCheck);
            this.Controls.Add(this.LblDateTime);
            this.Controls.Add(this.BtnActualProduction);
            this.Controls.Add(this.BtnReport);
            this.Controls.Add(this.BtnMaintenance);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.BtnRootNo);
            this.Controls.Add(this.BtnDepot);
            this.Controls.Add(this.BtnEnvironmentalSound);
            this.Controls.Add(this.BtnDifferent);
            this.Controls.Add(this.BtnNG);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnVoice2Start);
            this.Controls.Add(this.BtnVoice1Start);
            this.Controls.Add(this.BtnReadCollatingData);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.BtnAdjust);
            this.Controls.Add(this.BtnEnd);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.LblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "メインメニュー画面";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Label LblTitle;
        internal System.Windows.Forms.Label LblVersion;
        internal System.Windows.Forms.Button BtnActualProduction;
        internal System.Windows.Forms.Button BtnReadCollatingData;
        internal System.Windows.Forms.Button BtnMaintenance;
        internal System.Windows.Forms.Button BtnReport;
        internal System.Windows.Forms.Button BtnEnd;
        internal System.Windows.Forms.Button BtnAdjust;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.IO.Ports.SerialPort SerialPort;
        internal System.Windows.Forms.Timer TimPcReadyOn;
        internal Button BtnDifferent;
        internal Button BtnNG;
        internal Button BtnOK;
        internal Button BtnVoice2Start;
        internal Button BtnVoice1Start;
        internal Button BtnEnvironmentalSound;
        internal Button BtnDepot;
        internal Button BtnRootNo;
        internal Label LblStatus;
        internal Label LblDateTime;
        internal Timer TimDateTime;
        internal Button BtnProductionLogCheck;
    }
}
