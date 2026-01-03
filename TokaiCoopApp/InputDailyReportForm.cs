using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    public partial class InputDailyReportForm : Form
    {
        public InputDailyReportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォーム画面ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void InputDailyReportForm_Load(Object sender, EventArgs e)
        {
            string strYYYYMMDD;
            string[] strArray;
            string strErrorMessage;

            try
            {
                LblTitle.Text = "日報入力（" + PubConstClass.pblMachineName + "）";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                DTPicForm.Value = DateTime.Parse(PubConstClass.pblCurrentDate.Substring(0, 4) + "/" + 
                                  PubConstClass.pblCurrentDate.Substring(4, 2) + "/" + 
                                  PubConstClass.pblCurrentDate.Substring(6, 2));
                strErrorMessage = "";
                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";
                CmbSeisanFileName.Items.Clear();
                // 最終生産ログファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(
                                                                CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder), 
                                                                strYYYYMMDD + "*.plog", 
                                                                SearchOption.TopDirectoryOnly))
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                    {
                        CmbSeisanFileName.Items.Add(strArray[strArray.Length - 1]);
                    }                        
                    CmbSeisanFileName.SelectedIndex = 0;
                }
                if (CmbSeisanFileName.SelectedIndex == -1)
                {
                    strErrorMessage = "本日の最終生産ログがありません。";
                }
                    
                CmbEventLogFileName.Items.Clear();
                // 最終イベントログファイル一覧の取得
                foreach (string strEventLogFileFullPathName in Directory.GetFiles(
                                                               CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder), 
                                                               strYYYYMMDD + "*.elog", 
                                                               SearchOption.TopDirectoryOnly))
                {
                    strArray = strEventLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                    {
                        CmbEventLogFileName.Items.Add(strArray[strArray.Length - 1]);
                    }                        
                    CmbEventLogFileName.SelectedIndex = 0;
                }
                if (CmbEventLogFileName.SelectedIndex == -1)
                {
                    strErrorMessage += Constants.vbCr + "本日の最終イベントログがありません。";
                }

                if (strErrorMessage != "")
                {                    
                    MessageBox.Show(strErrorMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 開始時間と終了時間の「時」
                CmbFromHour.Items.Clear();
                CmbToHour.Items.Clear();
                for (var intLoopCnt = 0; intLoopCnt <= 23; intLoopCnt++)
                {
                    CmbFromHour.Items.Add(intLoopCnt.ToString("00"));
                    CmbToHour.Items.Add(intLoopCnt.ToString("00"));
                }
                CmbFromHour.SelectedIndex = 9;
                CmbToHour.SelectedIndex = 18;

                // 開始時間と終了時間の「分」
                CmbFromMin.Items.Clear();
                CmbToMin.Items.Clear();
                for (var intLoopCnt = 0; intLoopCnt <= 59; intLoopCnt++)
                {
                    CmbFromMin.Items.Add(intLoopCnt.ToString("00"));
                    CmbToMin.Items.Add(intLoopCnt.ToString("00"));
                }
                CmbFromMin.SelectedIndex = 0;
                CmbToMin.SelectedIndex = 0;

                // 休憩時間の「分」
                CmbRestTime.Items.Clear();
                for (int iRestTime=1; iRestTime<=120; iRestTime++)
                {
                    CmbRestTime.Items.Add(iRestTime.ToString("00"));
                }
                // デフォルトを10分とする
                CmbRestTime.SelectedIndex = 9;

                TxtMachine.Text = PubConstClass.pblMachineName;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【InputDailyReportForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// 「閉じる」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnClose_Click(Object sender, EventArgs e)
        {
            Owner.Show();
            this.Dispose();
        }


        /// <summary>
        /// 最終生産ログから生産数を取得する
        /// </summary>
        /// <param name="strYYYYMMDD"></param>
        /// <param name="strSeisanLogFileName"></param>
        /// <remarks></remarks>
        private void GetSeisanCount(string strYYYYMMDD, string strSeisanLogFileName)
        {
            string strSeisanLogFileFullPathName;
            string strReadData;
            int intReadCount=0;

            try
            {
                strSeisanLogFileFullPathName = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + 
                                               strYYYYMMDD + @"\" + strSeisanLogFileName;

                using (StreamReader sr = new StreamReader(strSeisanLogFileFullPathName, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strReadData = sr.ReadLine();
                        if (strReadData.Length < 6)
                        {
                            continue;
                        }
                        // 日付が「00000000」は対象外とする
                        if (strReadData.Substring(10, 8) == "00000000")
                        {
                            continue;
                        }                            
                        intReadCount += 1;
                    }
                    Application.DoEvents();
                }
                PubConstClass.strSeisanCount = intReadCount.ToString();
                CommonModule.OutPutLogFile("■（日報入力）最終生産ログの件数：" + intReadCount.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetSeisanCount】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「作業日報印刷」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnPrint_Click(Object sender, EventArgs e)
        {
            //MsgBoxResult RetValResult;
            string strYYYYMMDD;
            string strErrorMessage;

            try
            {
                strErrorMessage = "";
                if (CmbSeisanFileName.SelectedIndex == -1)
                    strErrorMessage = "最終生産実績ログがありません。";

                if (CmbEventLogFileName.SelectedIndex == -1)
                    strErrorMessage += Constants.vbCr + "最終イベントログがありません。";

                if (strErrorMessage != "")
                {
                    MessageBox.Show(strErrorMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // アラーム除外リスト
                CommonModule.GetOmitAlarmList();

                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";

                // 最終イベントログから稼働時間と停止時間の情報を取得
                CommonModule.GetRunAndStopTime(strYYYYMMDD, CmbEventLogFileName.Text);

                // 最終生産ログから生産数を取得
                GetSeisanCount(strYYYYMMDD, CmbSeisanFileName.Text);

                System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                // PrintPageイベントハンドラの追加
                pd.PrintPage += PrintDocument1_PrintPage;

                // PrintPreviewDialogオブジェクトの作成
                // プレビューするPrintDocumentを設定
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Document = pd,
                    Width = 1000,
                    Height = 800
                };

                // 印刷プレビューダイアログを表示する
                ppd.ShowDialog(this);

                //RetValResult = Interaction.MsgBox("日報を印刷しますか？", (MsgBoxStyle)MsgBoxStyle.Question | MsgBoxStyle.OkCancel, "確認");
                //if (RetValResult == MsgBoxResult.Ok)
                //{
                //    PrintDocument1.DocumentName = "日報の印刷";
                //    PrintDocument1.Print();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnPrint_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「作業日報」または「日次出庫確認表」の印刷処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int intHdrXPos;       // ヘッダーのＸ座標
            int intHdrYPos;       // ヘッダーのＹ座標
            string strWorkData;
            string strTotalWorkTime;  // 総時間
            int intPageNumber;

            try
            {
                PrintDocument1.DocumentName = "作業日報の印刷";
                PrintDocument1.DefaultPageSettings.Landscape = false;

                intHdrXPos = 20;
                intHdrYPos = 0;

                intPageNumber = 1;

                // ヘッダーの印刷
                DateTime dtNow = DateTime.Now;
                Font fu = new Font("ＭＳ ゴシック", 24, FontStyle.Underline);
                Font f0 = new Font("ＭＳ ゴシック", 12, FontStyle.Regular);
                Font f1 = new Font("ＭＳ ゴシック", 16, FontStyle.Regular);
                Font f2 = new Font("ＭＳ ゴシック", 18, FontStyle.Regular);
                Font f3 = new Font("ＭＳ ゴシック", 20, FontStyle.Regular);
                Font f4 = new Font("ＭＳ ゴシック", 24, FontStyle.Regular);
                Font f5 = new Font("ＭＳ ゴシック", 26, FontStyle.Regular);
                Font f6 = new Font("ＭＳ ゴシック", 28, FontStyle.Regular);

                // タイトル印字
                intHdrYPos = 60;
                e.Graphics.DrawString("作業日報", f4, Brushes.Black, intHdrXPos + 300, intHdrYPos);
                e.Graphics.DrawString(dtNow.ToString(), f0, Brushes.Black, intHdrXPos + 550, intHdrYPos);
                int intXpos1;
                int intXpos2;
                int intXpos3;
                int intXpos4;
                int intXpos5;
                int intXpos56;
                int intXpos6;
                int intXpos7;
                intXpos1 = 110;
                intXpos2 = 180;
                intXpos3 = 270;
                intXpos4 = 360;
                intXpos5 = 450;
                intXpos56 = 500;
                intXpos6 = 570;
                intXpos7 = 660;
                int inthgt1;
                int inthgt2;
                inthgt1 = 25;
                inthgt2 = inthgt1 * 3;
                int intWrtPos;
                intWrtPos = 5;
                // 枠（作業日～副機長）の印刷
                intHdrYPos += 60;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos1, inthgt2);    // 作業日
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos2, inthgt2);    // 機械
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt2);    // 企画週
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos4, inthgt2);    // 配達日
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos5, inthgt2);    // 配達曜日
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt2);    // ｵｰﾀﾞｰ番号
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt2);    // 機長　→　副機長
                e.Graphics.DrawString("  作業日", f0, Brushes.Black, intHdrXPos + 10, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("  機械", f0, Brushes.Black, intHdrXPos + intXpos1, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("  企画週", f0, Brushes.Black, intHdrXPos + intXpos2, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("  配達日", f0, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos);
                e.Graphics.DrawString(" 配達曜日", f0, Brushes.Black, intHdrXPos + intXpos4, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("  ｵｰﾀﾞｰ番号", f0, Brushes.Black, intHdrXPos + intXpos5, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("   機長", f0, Brushes.Black, intHdrXPos + intXpos6, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("  副機長", f0, Brushes.Black, intHdrXPos + intXpos7, intHdrYPos + intWrtPos);
                // 「作業日」「機械」「企画週」「配達日」「配達曜日」の値表示
                e.Graphics.DrawString(" " + DTPicForm.Value.ToString("yyyy/MM/dd"), f0, Brushes.Black, intHdrXPos, intHdrYPos + inthgt1 + intWrtPos * 3);
                e.Graphics.DrawString(" " + PubConstClass.pblMachineName, f0, Brushes.Black, intHdrXPos + intXpos1, intHdrYPos + inthgt1 + intWrtPos * 3);
                e.Graphics.DrawString(" " + TxtKikakuSyu.Text, f0, Brushes.Black, intHdrXPos + intXpos2, intHdrYPos + inthgt1 + intWrtPos * 3);
                e.Graphics.DrawString("   " + TxtDeliveryDay.Text, f0, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + inthgt1 + intWrtPos * 3);
                e.Graphics.DrawString("  " + TxtDeliveryWeek.Text, f0, Brushes.Black, intHdrXPos + intXpos4, intHdrYPos + inthgt1 + intWrtPos * 3);

                intHdrYPos += inthgt1 * 4;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt2); // 作業時間（休憩）
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos5, inthgt2); // 総時間
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt2); // 稼働時間　→　機械停止時間
                e.Graphics.DrawString("       作業時間（休憩）", f0, Brushes.Black, intHdrXPos + 10, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("       総時間", f0, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("   稼働時間", f0, Brushes.Black, intHdrXPos + intXpos5, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("    機械停止時間", f0, Brushes.Black, intHdrXPos + intXpos6, intHdrYPos + intWrtPos);
                // 「作業時間」の印字
                strWorkData = "      " + CmbFromHour.Text + ":" + CmbFromMin.Text + "～" + CmbToHour.Text + ":" + CmbToMin.Text + "（" + CmbRestTime.Text + "分）";
                e.Graphics.DrawString(strWorkData, f0, Brushes.Black, intHdrXPos + 10, intHdrYPos + inthgt1 + intWrtPos * 3);
                // 「総時間」の印字
                strTotalWorkTime = (DateAndTime.DateDiff("n", CmbFromHour.Text + ":" + CmbFromMin.Text + ":00", CmbToHour.Text + ":" + CmbToMin.Text + ":00") - Convert.ToInt64(CmbRestTime.Text)).ToString();
                e.Graphics.DrawString("        " + strTotalWorkTime + "分", f0, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + inthgt1 + intWrtPos * 3);
                // 「稼働時間」の印字
                strWorkData = (DateAndTime.DateDiff("n", "00:00:00", PubConstClass.strRunTime) - 0).ToString();
                e.Graphics.DrawString("    " + strWorkData + "分", f0, Brushes.Black, intHdrXPos + intXpos5, intHdrYPos + inthgt1 + intWrtPos * 3);
                // 「機械停止時間」の印字
                strWorkData = (PubConstClass.tsStop5MinUnder + PubConstClass.tsStop5MinOver).TotalMinutes.ToString("0");
                e.Graphics.DrawString("       " + strWorkData + "分", f0, Brushes.Black, intHdrXPos + intXpos6, intHdrYPos + inthgt1 + intWrtPos * 3);

                // 「生産時間」の印字
                var intTranTime = DateAndTime.DateDiff("n", PubConstClass.strStartTime.Substring(9, 2) + ":" + PubConstClass.strStartTime.Substring(11, 2) + ":00", PubConstClass.strEndTime.Substring(0, 2) + ":" + PubConstClass.strEndTime.Substring(2, 2) + ":00");
                strWorkData = "生産時間：" + PubConstClass.strStartTime.Substring(9, 2) + ":" + PubConstClass.strStartTime.Substring(11, 2) + "～" + PubConstClass.strEndTime.Substring(0, 2) + ":" + PubConstClass.strEndTime.Substring(2, 2) + "（" + intTranTime + "分）";
                e.Graphics.DrawString(strWorkData, f0, Brushes.Black, intHdrXPos + 10, intHdrYPos + inthgt1 * 3 + intWrtPos * 1);


                intHdrYPos += inthgt1 * 4;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos2, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos4, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt2);
                e.Graphics.DrawString("      生産数", f0, Brushes.Black, intHdrXPos + 10, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("     能率／時間", f0, Brushes.Black, intHdrXPos + intXpos2, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("   機械回転数", f0, Brushes.Black, intHdrXPos + intXpos4, intHdrYPos + intWrtPos);
                e.Graphics.DrawString("  人員", f0, Brushes.Black, intHdrXPos + intXpos56, intHdrYPos + 5);
                e.Graphics.DrawString("ﾁｮｺ停(5分以内の停止)", f0, Brushes.Black, intHdrXPos + intXpos6, intHdrYPos + intWrtPos);
                // 「生産数」の印字
                e.Graphics.DrawString("      " + int.Parse(PubConstClass.strSeisanCount).ToString("#,###,##0"), f0, Brushes.Black, intHdrXPos + 10, intHdrYPos + inthgt1 + intWrtPos * 3);
                // 「能率／時間」の印字（生産数÷総時間×６０分）
                strWorkData = (Convert.ToDouble(PubConstClass.strSeisanCount) / (double)Convert.ToDouble(strTotalWorkTime) * 60).ToString("#,###,##0");
                e.Graphics.DrawString("      " + strWorkData, f0, Brushes.Black, intHdrXPos + intXpos2, intHdrYPos + inthgt1 + intWrtPos * 3);
                // 「ﾁｮｺ停(5分以内の停止)」の印字
                e.Graphics.DrawString("       " + PubConstClass.tsStop5MinUnder.TotalMinutes.ToString("0") + "分", f0, Brushes.Black, intHdrXPos + intXpos6, intHdrYPos + inthgt1 + intWrtPos * 3);

                intHdrYPos += inthgt1 * 4;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt2);
                e.Graphics.DrawString("機長", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + inthgt1 + intWrtPos * 3);
                intHdrYPos += inthgt1 * 3;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("副機長", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);

                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("フィーダー", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("フィーダー", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("フィーダー", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("フィーダー", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("フィーダー", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("フィーダー", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("デリバリ", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);
                e.Graphics.DrawString("デリバリ", f1, Brushes.Black, intHdrXPos + intXpos3, intHdrYPos + intWrtPos * 3);
                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);

                intHdrYPos += inthgt1 * 2;
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, 750, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos3, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos56, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos6, inthgt1 * 2);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), intHdrXPos, intHdrYPos, intXpos7, inthgt1 * 2);

                // 改頁しない
                intPageNumber -= 1;

                if (intPageNumber > 0)
                {
                    e.HasMorePages = true;
                    intPageNumber += 1;
                }
                else
                    e.HasMorePages = false;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【PrintDocument1_PrintPage】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void DTPicForm_ValueChanged(Object sender, EventArgs e)
        {
            string strYYYYMMDD;
            string[] strArray;
            string strErrorMessage;

            try
            {
                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";

                CmbSeisanFileName.Items.Clear();
                CmbEventLogFileName.Items.Clear();

                // 生産ログ格納フォルダの存在チェック
                if (Directory.Exists(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD) == false)
                {
                    //Interaction.MsgBox("作業フォルダが存在しません。");
                    return;
                }

                strErrorMessage = "";
                // 最終生産ログファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(
                                                                CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder), 
                                                                strYYYYMMDD + "*.plog", 
                                                                SearchOption.TopDirectoryOnly))
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                    {
                        CmbSeisanFileName.Items.Add(strArray[strArray.Length - 1]);
                    }
                    CmbSeisanFileName.SelectedIndex = 0;
                }
                if (CmbSeisanFileName.SelectedIndex == -1)
                {
                    strErrorMessage = "作業日の最終生産ログがありません。";
                }

                // 最終イベントログファイル一覧の取得
                foreach (string strEventLogFileFullPathName in Directory.GetFiles(
                                                               CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder), 
                                                               strYYYYMMDD + "*.elog", 
                                                               SearchOption.TopDirectoryOnly))
                {
                    strArray = strEventLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                    {
                        CmbEventLogFileName.Items.Add(strArray[strArray.Length - 1]);
                    }
                    CmbEventLogFileName.SelectedIndex = 0;
                }
                if (CmbEventLogFileName.SelectedIndex == -1)
                {
                    strErrorMessage += Constants.vbCr + "作業日の最終イベントログがありません。";
                }
                if (strErrorMessage != "")
                {                    
                    MessageBox.Show(strErrorMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                    
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【DTPicForm_ValueChanged】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「最終生産ログ」コンボボックスの値を変更した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void CmbSeisanFileName_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string sYYYYMMDD;
            string sSeisanLogFileName;
            string sSeisanLogFileFullPathName;
            string sReadData;

            try
            {
                sYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";
                sSeisanLogFileName = CmbSeisanFileName.Text;
                sSeisanLogFileFullPathName = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) +
                                             sYYYYMMDD + @"\" + sSeisanLogFileName;
                using (StreamReader sr = new StreamReader(sSeisanLogFileFullPathName, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        sReadData = sr.ReadLine();
                        if (sReadData.Length < 20)
                        {
                            continue;
                        }
                        // 企画週
                        TxtKikakuSyu.Text = sReadData.Substring(0, 5) + "週";
                        // 配達日
                        TxtDeliveryDay.Text = sReadData.Substring(5, 1) + "日目";
                        // 曜日
                        TxtDeliveryWeek.Text = CommonModule.GetDeliveryWeek(sReadData.Substring(5, 1)) + "曜日";
                        // 先頭の一行のみ読み取る
                        break;
                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【DTPicForm_ValueChanged】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
