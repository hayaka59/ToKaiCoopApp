using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TokaiCoopApp
{
    public partial class ActualLogDisplayForm : Form
    {
        public ActualLogDisplayForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォーム画面ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActualLogDisplayForm_Load(object sender, EventArgs e)
        {
            TimProductionRecordLog.Interval = 1000;
            TimProductionRecordLog.Enabled = true;
        }

        /// <summary>
        /// 実績ログ表示用のタイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimProductionRecordLog_Tick(object sender, EventArgs e)
        {
            DisplayChart1();
            LblOperationStatus.Text = "生産数：" + PubConstClass.iNumberOfProcesses.ToString("#,###,##0") + "／" +
                                      PubConstClass.pblRunTotalCount +
                                      "（ＮＧ：" + PubConstClass.iNumberOfNGProcesses.ToString("0") + "）";
        }

        /// <summary>
        /// 実績ロググラフの表示
        /// </summary>
        private void DisplayChart1()
        {
            try
            {
                // フォームをロードするときの処理
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();

                // ChartにChartAreaを追加します
                string chart_area1 = "Area1";
                chart1.ChartAreas.Add(new ChartArea(chart_area1));
                // ChartにSeriesを追加します
                string legend1 = "実績数";
                chart1.Series.Add(legend1);
                chart1.ChartAreas[0].AxisX.Interval = 1;

                // グラフの種別（棒グラフ）を指定
                chart1.Series[legend1].ChartType = SeriesChartType.Column;

                // データをシリーズにセットします
                for (int i = 1; i < PubConstClass.dblCountOfProcesses.Length; i++)
                {
                    chart1.Series[legend1].Points.AddY(PubConstClass.dblCountOfProcesses[i]);
                }
                // 目盛表示
                chart1.Series[legend1].IsValueShownAsLabel = true;
                // 縦軸の非表示
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ActualLogDisplayForm.DisplayChart1】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// フォームのクロースをキャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActualLogDisplayForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }                
        }
    }
}
