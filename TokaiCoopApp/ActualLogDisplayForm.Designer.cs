namespace GreenCoopApp
{
    partial class ActualLogDisplayForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.LblOperationStatus = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.TimProductionRecordLog = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblOperationStatus
            // 
            this.LblOperationStatus.BackColor = System.Drawing.SystemColors.Control;
            this.LblOperationStatus.Font = new System.Drawing.Font("メイリオ", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblOperationStatus.ForeColor = System.Drawing.Color.Blue;
            this.LblOperationStatus.Location = new System.Drawing.Point(8, 336);
            this.LblOperationStatus.Name = "LblOperationStatus";
            this.LblOperationStatus.Size = new System.Drawing.Size(784, 70);
            this.LblOperationStatus.TabIndex = 271;
            this.LblOperationStatus.Text = "LblOperationStatus";
            this.LblOperationStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(20, 45);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(766, 293);
            this.chart1.TabIndex = 270;
            this.chart1.Text = "chart1";
            // 
            // TimProductionRecordLog
            // 
            this.TimProductionRecordLog.Tick += new System.EventHandler(this.TimProductionRecordLog_Tick);
            // 
            // ActualLogDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.LblOperationStatus);
            this.Controls.Add(this.chart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActualLogDisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "実績ログ表示画面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActualLogDisplayForm_Closing);
            this.Load += new System.EventHandler(this.ActualLogDisplayForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblOperationStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        internal System.Windows.Forms.Timer TimProductionRecordLog;
    }
}