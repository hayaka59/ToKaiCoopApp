using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    public partial class ProductionDetailForm : Form
    {
        public ProductionDetailForm()
        {
            InitializeComponent();
        }

        // ハードコピーオブジェクト
        private NonHCopyNet.HardCopyClass objHardCopy = new NonHCopyNet.HardCopyClass();

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ProductionDetailForm_Load(Object sender, EventArgs e)
        {
            try
            {
                // LblTitle.Text = "生産実績（神奈川 02週 0508 月）"
                LblTitle.Text = "生産実績（" + ((ActualProductionForm)Owner).strTankyo + " " + 
                                               ((ActualProductionForm)Owner).strKikaku + "週 " + 
                                               ((ActualProductionForm)Owner).strSeisanDate + " " + 
                                               ((ActualProductionForm)Owner).strWeek + "曜日）";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                // 「日付」の設定
                LblDate.Text = PubConstClass.strStartTime.Substring(0, 4) + "/" + 
                               PubConstClass.strStartTime.Substring(4, 2) + "/" + 
                               PubConstClass.strStartTime.Substring(6, 2);
                // 「開始 ～ 終了」の設定
                LblFromTo.Text = PubConstClass.strStartTime.Substring(9, 2) + ":" + 
                                 PubConstClass.strStartTime.Substring(11, 2) + ":" + 
                                 PubConstClass.strStartTime.Substring(13, 2) + " ～ " + 
                                 PubConstClass.strEndTime.Substring(0, 2) + ":" + 
                                 PubConstClass.strEndTime.Substring(2, 2) + ":" + 
                                 PubConstClass.strEndTime.Substring(4, 2);

                if (PubConstClass.strStartTime != "00000000 000000")
                {
                    // 「開始 ～ 終了」の時間（分）を取得
                    DateTime dt1 = new DateTime(Convert.ToInt32(PubConstClass.strStartTime.Substring(0, 4)),
                                                Convert.ToInt32(PubConstClass.strStartTime.Substring(4, 2)),
                                                Convert.ToInt32(PubConstClass.strStartTime.Substring(6, 2)),
                                                Convert.ToInt32(PubConstClass.strStartTime.Substring(9, 2)),
                                                Convert.ToInt32(PubConstClass.strStartTime.Substring(11, 2)),
                                                Convert.ToInt32(PubConstClass.strStartTime.Substring(13, 2)));

                    DateTime dt2 = new DateTime(Convert.ToInt32(PubConstClass.strStartTime.Substring(0, 4)),
                                                Convert.ToInt32(PubConstClass.strStartTime.Substring(4, 2)),
                                                Convert.ToInt32(PubConstClass.strStartTime.Substring(6, 2)),
                                                Convert.ToInt32(PubConstClass.strEndTime.Substring(0, 2)),
                                                Convert.ToInt32(PubConstClass.strEndTime.Substring(2, 2)),
                                                Convert.ToInt32(PubConstClass.strEndTime.Substring(4, 2)));
                    // 作業時間（分）を取得
                    TimeSpan ts0 = dt2 - dt1;
                    LblWorkTime.Text = ts0.ToString() + "（" + ts0.TotalMinutes.ToString("0") + "分）";
                }

                // 「生産数」の設定
                LblSeisanCount.Text = PubConstClass.strSeisanCount;

                if (PubConstClass.strStartTime != "00000000 000000")
                {
                    // 「稼働時間」の設定
                    DateTime dt1Run = new DateTime(Convert.ToInt32(PubConstClass.strStartTime.Substring(0, 4)),
                                                   Convert.ToInt32(PubConstClass.strStartTime.Substring(4, 2)),
                                                   Convert.ToInt32(PubConstClass.strStartTime.Substring(6, 2)),
                                                   Convert.ToInt32(PubConstClass.strStartTime.Substring(9, 2)),
                                                   Convert.ToInt32(PubConstClass.strStartTime.Substring(11, 2)),
                                                   Convert.ToInt32(PubConstClass.strStartTime.Substring(13, 2)));

                    DateTime dt2Run = new DateTime(Convert.ToInt32(PubConstClass.strStartTime.Substring(0, 4)),
                                                   Convert.ToInt32(PubConstClass.strStartTime.Substring(4, 2)),
                                                   Convert.ToInt32(PubConstClass.strStartTime.Substring(6, 2)),
                                                   Convert.ToInt32(PubConstClass.strEndTime.Substring(0, 2)),
                                                   Convert.ToInt32(PubConstClass.strEndTime.Substring(2, 2)),
                                                   Convert.ToInt32(PubConstClass.strEndTime.Substring(4, 2)));

                    TimeSpan tsRun = dt2Run - dt1Run;
                    LblRunTime.Text = tsRun.ToString() + "（" + tsRun.TotalMinutes.ToString("0") + "分）";
                }

                // チョコ停（５分以内の停止）
                LblStop5MinUnder.Text = PubConstClass.tsStop5MinUnder.ToString() + "（" + PubConstClass.tsStop5MinUnder.TotalMinutes.ToString("0") + "分）";

                // 再調整（５分以上の停止）
                LblStop5MinOver.Text = PubConstClass.tsStop5MinOver.ToString() + "（" + PubConstClass.tsStop5MinOver.TotalMinutes.ToString("0") + "分）";

                // 最大連続稼働時間（デバッグの為、一時的にコメントアウト）
                LblMaxRunTime.Text = PubConstClass.strMaxRunTime + "（" + 
                                     PubConstClass.strMaxRunFromTime.Substring(0, 2) + ":" + 
                                     PubConstClass.strMaxRunFromTime.Substring(2, 2) + ":" + 
                                     PubConstClass.strMaxRunFromTime.Substring(4, 2) + "～" + 
                                     PubConstClass.strMaxRunToTime.Substring(0, 2) + ":" + 
                                     PubConstClass.strMaxRunToTime.Substring(2, 2) + ":" + 
                                     PubConstClass.strMaxRunToTime.Substring(4, 2) + "）";

                // 「時間帯内訳」表示処理
                DisplayTimeDetail();

                // 「チョコ停内訳」表示処理
                DisplayChokoDetail();

                // 「フィーダー別内訳」表示処理
                DisplayFeederDetail();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【ProductionDetailForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                Owner.Show();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("【BtnClose_Click】" + ex.Message);
            }
        }

        /// <summary>
        /// 「時間帯内訳」表示処理
        /// </summary>
        /// <remarks></remarks>
        private void DisplayTimeDetail()
        {
            int intLoopCnt;
            string[] col = new string[6];
            ListViewItem itm;
            string[] strArray;

            try
            {
                // 時間帯別内訳表示ListViewのカラムヘッダー設定
                LsvTimeDetail.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                ColumnHeader col4 = new ColumnHeader();
                ColumnHeader col5 = new ColumnHeader();
                col1.Text = "日付";
                col2.Text = "時間";
                col3.Text = "稼働時間";
                col4.Text = "停止時間";
                col5.Text = "生産数量";
                col1.Width = 130;        // 日付
                col2.Width = 50;         // 時間
                col3.Width = 100;        // 稼働時間
                col4.Width = 100;        // 停止時間
                col5.Width = 100;        // 生産数量
                col1.TextAlign = HorizontalAlignment.Left;
                col2.TextAlign = HorizontalAlignment.Right;
                col3.TextAlign = HorizontalAlignment.Right;
                col4.TextAlign = HorizontalAlignment.Right;
                col5.TextAlign = HorizontalAlignment.Right;
                ColumnHeader[] colHeader = new[] { col1, col2, col3, col4, col5 };
                LsvTimeDetail.Columns.AddRange(colHeader);

                var tsKadouSum = new TimeSpan(0, 0, 0);
                var tsTeisiSum = new TimeSpan(0, 0, 0);

                strArray = " , , ".Split(',');
                for (intLoopCnt = 0; intLoopCnt <= 24; intLoopCnt++)
                {
                    strArray = ((ActualProductionForm)Owner).strTranCountOfTime[intLoopCnt].Split(',');
                    if (strArray[1] == "合計")
                    {
                        break;
                    }                        
                    strArray = ((ActualProductionForm)Owner).strTranCountOfTime[intLoopCnt].Split(',');
                    // 年月日
                    col[0] = strArray[0];
                    // 時間
                    col[1] = strArray[1];
                    // 稼働時間
                    col[2] = PubConstClass.tsKadouTime[Convert.ToInt32(strArray[1])].ToString();
                    tsKadouSum += PubConstClass.tsKadouTime[Convert.ToInt32(strArray[1])];
                    // 停止時間
                    col[3] = PubConstClass.tsTeisiTime[Convert.ToInt32(strArray[1])].ToString();
                    tsTeisiSum += PubConstClass.tsTeisiTime[Convert.ToInt32(strArray[1])];
                    // 生産数
                    col[4] = int.Parse(strArray[2]).ToString("#,###,##0");
                    // データの表示
                    itm = new ListViewItem(col);
                    LsvTimeDetail.Items.Add(itm);
                }
                // 合計
                col[0] = "";
                col[1] = strArray[1];
                col[2] = tsKadouSum.ToString();
                col[3] = tsTeisiSum.ToString();
                col[4] = int.Parse(strArray[2]).ToString("#,###,##0");
                // データの表示
                itm = new ListViewItem(col);
                LsvTimeDetail.Items.Add(itm);

                LsvTimeDetail.Items[LsvTimeDetail.Items.Count - 1].SubItems[0].BackColor = Color.FromArgb(200, 200, 230);
                LsvTimeDetail.Items[LsvTimeDetail.Items.Count - 1].SubItems[1].BackColor = Color.FromArgb(200, 200, 230);
                LsvTimeDetail.Items[LsvTimeDetail.Items.Count - 1].SubItems[2].BackColor = Color.FromArgb(200, 200, 230);
                LsvTimeDetail.Items[LsvTimeDetail.Items.Count - 1].SubItems[3].BackColor = Color.FromArgb(200, 200, 230);
                LsvTimeDetail.Items[LsvTimeDetail.Items.Count - 1].SubItems[4].BackColor = Color.FromArgb(200, 200, 230);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DisplayTimeDetail】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「チョコ停内訳」表示処理
        /// </summary>
        /// <remarks></remarks>
        private void DisplayChokoDetail()
        {
            int intLoopCnt;
            string[] col = new string[5];
            ListViewItem itm;
            string strYYYYMMDD;
            string strReadData;
            string[] strArray;
            string[] strAlarmInfo = new string[501];  // アラーム情報格納配列
            int intAlarmInfoIndex;

            try
            {
                // チョコ停内訳表示ListViewのカラムヘッダー設定
                LsvChokoDetail.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                col1.Text = "エラー要因";
                col2.Text = "停止時間";
                col3.Text = "停止回数";
                col1.Width = 310;        // エラー要因
                col2.Width = 90;         // 停止時間
                col3.Width = 90;         // 停止回数
                col1.TextAlign = HorizontalAlignment.Left;
                col2.TextAlign = HorizontalAlignment.Right;
                col3.TextAlign = HorizontalAlignment.Right;
                ColumnHeader[] colHeader = new[] { col1, col2, col3 };
                LsvChokoDetail.Columns.AddRange(colHeader);
                // アラーム情報のクリア
                for (var N = 0; N <= 499; N++)
                    // アラームID,アラームメッセージ,発生時間,発生回数
                    strAlarmInfo[N] = "★★★0";
                intAlarmInfoIndex = 0;

                string strAlarmID;
                string strAlarmMes;
                string strAlarmTime;
                string[] strCmpArray;

                strYYYYMMDD = ((ActualProductionForm)Owner).DTPicForm.Value.ToString("yyyyMMdd") + @"\";
                // アラーム停止時間ログファイルの設定
                string strAlarmStopLogPath;
                strAlarmStopLogPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + "アラーム停止時間ログ.txt";
                using (StreamReader sr = new StreamReader(strAlarmStopLogPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strReadData = sr.ReadLine();
                        if (strReadData != "")
                        {
                            // アラーム停止時間ログの格納形式
                            //        0      1                           2     3                           4
                            // 20220311,195608,                           ,02214,STACKER : MOTORS PROTECTION
                            strArray = strReadData.Split('■');
                            string[] strAlarmCodeArray = strReadData.Split(',');
                            string[] strAlarmNameArray = strAlarmCodeArray[4].Split('■');
                            strAlarmID = "[" + strAlarmCodeArray[3] + "]";
                            strAlarmMes = strAlarmNameArray[0];
                            
                            strAlarmTime = strArray[1];
                            // 稼働時間が取得できないデータは対象外とする
                            if (strAlarmTime != "")
                            {
                                // 停止時間が５分以内のデータを対象とする
                                var ts0 = new TimeSpan(Convert.ToInt32(strAlarmTime.Substring(0, 2)), Convert.ToInt32(strAlarmTime.Substring(3, 2)), Convert.ToInt32(strAlarmTime.Substring(6, 2)));
                                if (ts0.TotalMinutes < 5)
                                {
                                    for (intLoopCnt = 0; intLoopCnt <= intAlarmInfoIndex; intLoopCnt++)
                                    {
                                        if (intLoopCnt == intAlarmInfoIndex)
                                        {
                                            strAlarmInfo[intLoopCnt] = strAlarmID + "★" + strAlarmMes + "★" + strAlarmTime + "★" + "1";
                                            intAlarmInfoIndex += 1;
                                            break;
                                        }
                                        else
                                        {
                                            strCmpArray = strAlarmInfo[intLoopCnt].Split('★');
                                            if (strAlarmID == strCmpArray[0])
                                            {
                                                strAlarmInfo[intLoopCnt] = strAlarmID + "★" + strAlarmMes + "★" + AddStopTime(strAlarmTime, ref strCmpArray[2]) + "★" + (Convert.ToInt32(strCmpArray[3]) + 1).ToString();
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Application.DoEvents();
                    }
                }

                string strStopTimeSum = "00:00:00";
                string strStopCountSum = "0";
                for (intLoopCnt = 0; intLoopCnt < intAlarmInfoIndex; intLoopCnt++)
                {
                    strArray = strAlarmInfo[intLoopCnt].Split('★');
                    // アラームID＋アラームメッセージ
                    col[0] = strArray[0] + strArray[1];
                    // 停止時間
                    col[1] = strArray[2];
                    // 発生回数
                    col[2] = strArray[3];
                    // データの表示
                    itm = new ListViewItem(col);
                    LsvChokoDetail.Items.Add(itm);
                    strStopCountSum = (Convert.ToInt32(strStopCountSum) + Convert.ToInt32(strArray[3])).ToString();
                    strStopTimeSum = AddStopTime(strStopTimeSum, ref strArray[2]);
                }

                col[0] = "　　　合計";
                col[1] = strStopTimeSum;
                col[2] = strStopCountSum;
                // データの表示
                itm = new ListViewItem(col);
                LsvChokoDetail.Items.Add(itm);
                for (var N = 0; N <= 2; N++)
                {
                    LsvChokoDetail.Items[LsvChokoDetail.Items.Count - 1].SubItems[N].BackColor = Color.FromArgb(200, 200, 230);
                }                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DisplayChokoDetail】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTime1"></param>
        /// <param name="strTime2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string AddStopTime(string strTime1, ref string strTime2)
        {
            string[] strArray;
            string strDate;
            int intYYYY;
            int intMM;
            int intDD;

            string AddStopTime = "00:00:00";

            try
            {
                strDate = DateTime.Now.ToString();
                // 「YYYY/MM/DD hh:mm:ss」形式から年月日を切り出す
                intYYYY = Convert.ToInt32(strDate.Substring(0, 4));
                intMM = Convert.ToInt32(strDate.Substring(5, 2));
                intDD = Convert.ToInt32(strDate.Substring(8, 2));

                if (strTime1 == "")
                {
                    strTime1 = "00:00:00";
                }
                strArray = strTime1.Split(':');
                DateTime dt1 = new DateTime(intYYYY, intMM, intDD, Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]));

                if (strTime2 == "")
                {
                    strTime2 = "00:00:00";
                }
                strArray = strTime2.Split(':');
                TimeSpan ts2 = new TimeSpan(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]));
                // 停止時間（分）を取得
                DateTime ds0 = dt1 + ts2;
                strArray = ds0.ToString().Split(' ');
                if (strArray[1].Length == 7)
                {
                    AddStopTime = "0" + strArray[1];
                }
                else
                {
                    AddStopTime = strArray[1];
                }                   

                Debug.Print(strTime1 + " + " + strTime2 + " = " + AddStopTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【AddStopTime】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AddStopTime;
        }

        /// <summary>
        /// 「フィーダー別内訳」処理
        /// </summary>
        /// <remarks></remarks>
        private void DisplayFeederDetail()
        {
            int intLoopCnt;
            string[] col = new string[7];
            ListViewItem itm;

            try
            {
                // フィーダー別内訳表示ListViewのカラムヘッダー設定
                LstFeederDetail.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                ColumnHeader col4 = new ColumnHeader();
                ColumnHeader col5 = new ColumnHeader();
                ColumnHeader col6 = new ColumnHeader();
                ColumnHeader col7 = new ColumnHeader();
                col1.Text = "";
                col2.Text = "ＯＫ";
                col3.Text = "ミス";
                col4.Text = "ダブル";
                col5.Text = "イリーガル";
                col6.Text = "カメラエラー";
                col7.Text = "その他";
                col1.Width = 60;         // 
                col2.Width = 80;         // ＯＫ
                col3.Width = 80;         // ミス
                col4.Width = 80;         // ダブル
                col5.Width = 100;        // イリーガル
                col6.Width = 120;        // カメラエラー
                col7.Width = 80;         // その他
                col1.TextAlign = HorizontalAlignment.Center;
                col2.TextAlign = HorizontalAlignment.Right;
                col3.TextAlign = HorizontalAlignment.Right;
                col4.TextAlign = HorizontalAlignment.Right;
                col5.TextAlign = HorizontalAlignment.Right;
                col6.TextAlign = HorizontalAlignment.Right;
                col7.TextAlign = HorizontalAlignment.Right;
                ColumnHeader[] colHeader = new[] { col1, col2, col3, col4, col5, col6, col7 };
                LstFeederDetail.Columns.AddRange(colHeader);

                string strYYYYMMDD;
                string strReadData;
                int[,] intFeederInfo = new int[27, 6];
                int[] intFeederSum = new int[6];
                string strFeedNoData;
                // 時間の合計値クリア
                for (intLoopCnt = 0; intLoopCnt <= 5; intLoopCnt++)
                    intFeederSum[intLoopCnt] = 0;
                for (intLoopCnt = 0; intLoopCnt < 27; intLoopCnt++)
                {
                    intFeederInfo[intLoopCnt, 0] = 0;
                    intFeederInfo[intLoopCnt, 1] = 0;
                    intFeederInfo[intLoopCnt, 2] = 0;
                    intFeederInfo[intLoopCnt, 3] = 0;
                    intFeederInfo[intLoopCnt, 4] = 0;
                    intFeederInfo[intLoopCnt, 5] = 0;
                }

                strYYYYMMDD = ((ActualProductionForm)Owner).DTPicForm.Value.ToString("yyyyMMdd") + @"\";
                // 最終生産ログファイルの設定
                string strFinalTranLogPath;
                strFinalTranLogPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + 
                                      strYYYYMMDD + ((ActualProductionForm)Owner).CmbSeisanFileName.Text;
                using (StreamReader sr = new StreamReader(strFinalTranLogPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strReadData = sr.ReadLine();
                        // OutPutLogFile("読取り内容：" & strReadData)
                        if (strReadData != "")
                        {
                            // 最終生産ログの格納形式
                            // 0         1         2         3         4         5         6
                            // 0123456789012345678901234567890123456789012345678901234567890123
                            // 131000020 20150129 061954 013 0000000000-00000000000-       -00-
                            //for (intLoopCnt = 0; intLoopCnt <= 27; intLoopCnt++)
                            for (intLoopCnt = 0; intLoopCnt < 21; intLoopCnt++)
                            {
                                strFeedNoData = strReadData.Substring(intLoopCnt + 68, 1);
                                switch (strFeedNoData)
                                {
                                    case "0":    // ＯＫ
                                        {
                                            intFeederInfo[intLoopCnt, 0] += 1;
                                            intFeederSum[0] += 1;
                                            break;
                                        }

                                    case "1":    // ミス
                                        {
                                            intFeederInfo[intLoopCnt, 1] += 1;
                                            intFeederSum[1] += 1;
                                            break;
                                        }

                                    case "2":    // ダブル
                                        {
                                            intFeederInfo[intLoopCnt, 2] += 1;
                                            intFeederSum[2] += 1;
                                            break;
                                        }

                                    case "3":    // イリーガル
                                        {
                                            intFeederInfo[intLoopCnt, 3] += 1;
                                            intFeederSum[3] += 1;
                                            break;
                                        }

                                    case "4":    // カメラエラー
                                        {
                                            intFeederInfo[intLoopCnt, 4] += 1;
                                            intFeederSum[4] += 1;
                                            break;
                                        }

                                    case "5":    // その他
                                        {
                                            intFeederInfo[intLoopCnt, 5] += 1;
                                            intFeederSum[5] += 1;
                                            break;
                                        }

                                    case "-":    // データ無し
                                        {
                                            break;
                                        }

                                    default:
                                        {
                                            break;
                                        }
                                }
                            }
                        }
                        Application.DoEvents();
                    }
                }
                // 各件数示処理
                // For intLoopCnt = 0 To 23
                for (intLoopCnt = 0; intLoopCnt < 27; intLoopCnt++)
                {
                    col[0] = " " + "F" + (intLoopCnt + 1).ToString("0");
                    col[1] = intFeederInfo[intLoopCnt, 0].ToString("#,###,##0");  // ＯＫ
                    col[2] = intFeederInfo[intLoopCnt, 1].ToString("#,###,##0");  // ミス
                    col[3] = intFeederInfo[intLoopCnt, 2].ToString("#,###,##0");  // ダブル
                    col[4] = intFeederInfo[intLoopCnt, 3].ToString("#,###,##0");  // イリーガル
                    col[5] = intFeederInfo[intLoopCnt, 4].ToString("#,###,##0");  // カメラエラー
                    col[6] = intFeederInfo[intLoopCnt, 5].ToString("#,###,##0");  // その他
                    // データの表示
                    itm = new ListViewItem(col);
                    LstFeederDetail.Items.Add(itm);
                }
                // 合計表示処理
                col[0] = " 合計";
                col[1] = intFeederSum[0].ToString("#,###,##0");
                col[2] = intFeederSum[1].ToString("#,###,##0");
                col[3] = intFeederSum[2].ToString("#,###,##0");
                col[4] = intFeederSum[3].ToString("#,###,##0");
                col[5] = intFeederSum[4].ToString("#,###,##0");
                col[6] = intFeederSum[5].ToString("#,###,##0");
                // データの表示
                itm = new ListViewItem(col);
                LstFeederDetail.Items.Add(itm);
                LstFeederDetail.Items[LstFeederDetail.Items.Count - 1].SubItems[0].BackColor = Color.FromArgb(200, 200, 230);
                LstFeederDetail.Items[LstFeederDetail.Items.Count - 1].SubItems[1].BackColor = Color.FromArgb(200, 200, 230);
                LstFeederDetail.Items[LstFeederDetail.Items.Count - 1].SubItems[2].BackColor = Color.FromArgb(200, 200, 230);
                LstFeederDetail.Items[LstFeederDetail.Items.Count - 1].SubItems[3].BackColor = Color.FromArgb(200, 200, 230);
                LstFeederDetail.Items[LstFeederDetail.Items.Count - 1].SubItems[4].BackColor = Color.FromArgb(200, 200, 230);
                LstFeederDetail.Items[LstFeederDetail.Items.Count - 1].SubItems[5].BackColor = Color.FromArgb(200, 200, 230);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【DisplayFeederDetail】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「画面印刷」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnPrintScreen_Click(Object sender, EventArgs e)
        {
            try
            {
                // ［Alt］キー＋［Print Screen］キーの送信
                SendKeys.SendWait("%{PRTSC}");

                // 用紙方向を横向きに設定
                PrintDocument objPrinter = new PrintDocument();
                objPrinter.DefaultPageSettings.Landscape = true;

                // 用紙サイズをＡ４に設定
                foreach (PaperSize psz in objPrinter.PrinterSettings.PaperSizes)
                {
                    if (psz.Kind == PaperKind.A4)
                    {
                        objPrinter.DefaultPageSettings.PaperSize = psz;
                        break;
                    }
                }

                // 通常使うプリンタへのアクティブウィンドウ（アクティブウィンドウ）のハードコピー
                objHardCopy.HardCopy(true, objPrinter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnPrintScreen_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
