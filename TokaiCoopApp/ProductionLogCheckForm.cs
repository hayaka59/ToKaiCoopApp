using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GreenCoopApp
{
    public partial class ProductionLogCheckForm : Form
    {

        private string[] strPrdTranArray = new string[25];   // 時間当たりの生産実績件数）
        public string strTankyo;
        public string strKikaku;
        public string strSeisanDate;
        public string strWeek;

        public string[] strTranCountOfTime = new string[25];     // 24時間分

        // ハードコピーオブジェクト
        private NonHCopyNet.HardCopyClass objHardCopy = new NonHCopyNet.HardCopyClass();


        public ProductionLogCheckForm()
        {
            InitializeComponent();
        }

        private void ProductionLogCheckForm_Load(object sender, EventArgs e)
        {
            string strYYYYMMDD;
            string[] strArray;
            string strErrorMessage;

            try
            {
                LblTitle.Text = "生産ログチェック";
                LblVersion.Text = PubConstClass.DEF_VERSION;
                LblMessage.Text = "";

                strErrorMessage = "";

                dblAchievementLog = new double[25];

                strYYYYMMDD = DTPicWorkDay.Value.ToString("yyyyMMdd") + @"\";

                // 最終生産ログファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(
                                                                CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder),
                                                                strYYYYMMDD + "*.plog", SearchOption.TopDirectoryOnly))
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                    {
                        // 「最終生産ログ照合」タブの最終生産ログ
                        CmbFinalSeisanLog.Items.Add(strArray[strArray.Length - 1]);
                    }
                    CmbFinalSeisanLog.SelectedIndex = 0;
                }
                if (CmbFinalSeisanLog.SelectedIndex == -1)
                {
                    strErrorMessage = "本日の最終生産実績ログがありません。";
                }

                // 指示データファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(
                                                                CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder),
                                                                strYYYYMMDD + "YK520P*", SearchOption.TopDirectoryOnly))
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                    {
                        // 「最終生産ログ照合」タブの指示データ
                        CmbInstructionData.Items.Add(strArray[strArray.Length - 1]);
                    }
                    CmbInstructionData.SelectedIndex = 0;
                }
                if (CmbInstructionData.SelectedIndex == -1)
                {
                    strErrorMessage += Environment.NewLine + "本日の指示データがありません。";
                }
                if (strErrorMessage != "")
                {
                    MessageBox.Show(strErrorMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                DgvInstructionData.DataSource = GetDataTable();
                //ヘッダーに合わせ列幅自動調整
                DgvInstructionData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                DgvInstructionData.Refresh();
                // 指示データ表示データグリッドの各ヘッダーの並び替え不可とする
                foreach (DataGridViewColumn c in DgvInstructionData.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                DgvProductionLog.DataSource = GetDataTable();
                // 最終生産ログ表示データグリッドの各ヘッダーの並び替え不可とする
                foreach (DataGridViewColumn c in DgvProductionLog.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                PicWaiting1.Visible = false;
                PicWaiting2.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ActualProductionForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 空のデータをセット
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            string[] sArray;

            var dt = new DataTable();
            SetDataTableHeader(dt);

            try
            {
                sArray = ",,,,,,,,,,,,,,,,,,,,,,,,".Split(',');
                for (int N = 1; N <= 13; N++)
                {
                    dt.Rows.Add(new object[] { sArray[01], sArray[02], sArray[03], sArray[04],
                                               sArray[05], sArray[06], sArray[07], sArray[08], sArray[09],
                                               sArray[10], sArray[11], sArray[12], sArray[13], sArray[14],
                                               sArray[15], sArray[16], sArray[17], sArray[18], sArray[19],
                                               sArray[20], sArray[21], sArray[22], sArray[23], sArray[24],});
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetDataTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.Rows.Add(new object[] { 0, 0, 0, 0, 0, "X" });

                dt.Rows.Add(new object[] {"", "",
                                          "", "", "", "", "", "",
                                          "", "", "", "",
                                          "", "", "", "",
                                          "", "", "", "",
                                          "", "", "", "",});
                return dt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LstResultView"></param>
        private void DispSeisanTran(ListView LstResultView)
        {
            int intLoopCnt;
            string[] col = new string[5];
            ListViewItem itm;
            int intSum;

            try
            {
                LstResultView.Clear();
                // 生産実績表示ListViewのカラムヘッダー設定
                LstResultView.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                col1.Text = "    日  付";
                col2.Text = "時間";
                col3.Text = "数量";
                col1.Width = 120;        // 日付
                col2.Width = 70;         // 時間
                col3.Width = 90;         // 数量
                col1.TextAlign = HorizontalAlignment.Center;
                col2.TextAlign = HorizontalAlignment.Center;
                col3.TextAlign = HorizontalAlignment.Center;
                ColumnHeader[] colHeader = new[] { col1, col2, col3 };
                LstResultView.Columns.AddRange(colHeader);

                intSum = 0;
                LstResultView.Items.Clear();
                for (intLoopCnt = 1; intLoopCnt <= 23; intLoopCnt++)
                {
                    if (strPrdTranArray[intLoopCnt] != "")
                    {
                        col[0] = DateTime.Now.ToString("yyyy/MM/dd");
                        col[1] = intLoopCnt.ToString("00");
                        col[2] = int.Parse(strPrdTranArray[intLoopCnt]).ToString("#,###,##0");

                        dblAchievementLog[intLoopCnt] = double.Parse(strPrdTranArray[intLoopCnt]);

                        intSum += int.Parse(strPrdTranArray[intLoopCnt]);
                        col1.TextAlign = HorizontalAlignment.Left;
                        col2.TextAlign = HorizontalAlignment.Right;
                        col3.TextAlign = HorizontalAlignment.Right;
                        // データの表示
                        itm = new ListViewItem(col);
                        LstResultView.Items.Add(itm);
                    }
                }
                col[0] = "";
                col[1] = "合計";
                col[2] = intSum.ToString("#,###,##0");

                // データの表示
                itm = new ListViewItem(col);
                LstResultView.Items.Add(itm);
                LstResultView.Items[LstResultView.Items.Count - 1].SubItems[0].BackColor = Color.FromArgb(200, 200, 230);
                LstResultView.Items[LstResultView.Items.Count - 1].SubItems[1].BackColor = Color.FromArgb(200, 200, 230);
                LstResultView.Items[LstResultView.Items.Count - 1].SubItems[2].BackColor = Color.FromArgb(200, 200, 230);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DispSeisanTran】", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private double[] dblAchievementLog;

        /// <summary>
        /// 最終生産ログ表示データグリッドのスクロール同期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvInstructionData_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if (DgvProductionLog.RowCount > DgvInstructionData.FirstDisplayedScrollingRowIndex)
                {
                    DgvProductionLog.FirstDisplayedScrollingRowIndex = DgvInstructionData.FirstDisplayedScrollingRowIndex;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DgvInstructionData_Scroll】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 指示データ表示データグリッドのスクロール同期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvProductionLog_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if (DgvInstructionData.RowCount > DgvProductionLog.FirstDisplayedScrollingRowIndex)
                {
                    DgvInstructionData.FirstDisplayedScrollingRowIndex = DgvProductionLog.FirstDisplayedScrollingRowIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DgvProductionLog_Scroll】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 指示データ表示データグリッドのセルクリック処理
        /// </summary>
        private int iPrevUpColumnIndex = -1;
        private void DgvInstructionData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 列ヘッダー、行ヘッダーのチェック
                if (e.RowIndex == -1 || e.ColumnIndex == -1)
                {
                    // 列ヘッダーまたは行ヘッダークリック時は何もしない
                    return;
                }
                // 前回の選択カラムのチェック
                if (iPrevUpColumnIndex != -1)
                {
                    // 前回選択した列の色設定をクリアする
                    DgvInstructionData.Columns[iPrevUpColumnIndex].DefaultCellStyle.BackColor = Color.White;
                }
                // 前回の値として保持する
                iPrevUpColumnIndex = e.ColumnIndex;
                // 今回選択した列の色設定を行う
                DgvInstructionData.Columns[e.ColumnIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                // 今回選択した行を選択状態とする
                DgvInstructionData.Rows[e.RowIndex].Selected = true;
                // 今回選択した列情報を前回値とする
                iPrevUpColumnIndex = e.ColumnIndex;
                // 下のデータグリッド（最終生産ログ）を同期させる
                DownSyncOn(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DgvInstructionData_CellClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 最終生産ログ表示データグリッドのセルクリック処理
        /// </summary>
        private int iPrevDownColumnIndex = -1;
        private void DgvProductionLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 列ヘッダー、行ヘッダーのチェック
                if (e.RowIndex == -1 || e.ColumnIndex == -1)
                {
                    // 列ヘッダーまたは行ヘッダークリック時は何もしない
                    return;
                }
                // 前回の選択カラムのチェック
                if (iPrevDownColumnIndex != -1)
                {
                    // 前回選択した列の色設定をクリアする
                    DgvProductionLog.Columns[iPrevDownColumnIndex].DefaultCellStyle.BackColor = Color.White;
                }
                // 前回の値として保持する
                iPrevDownColumnIndex = e.ColumnIndex;
                // 今回選択した列の色設定を行う
                DgvProductionLog.Columns[e.ColumnIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                // 今回選択した行を選択状態とする
                DgvProductionLog.Rows[e.RowIndex].Selected = true;
                // 今回選択した列情報を前回値とする
                iPrevDownColumnIndex = e.ColumnIndex;
                // 上のデータグリッド（指示データ）を同期させる
                UpSyncOn(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DgvProductionLog_CellClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 上のデータグリッド（最終生産ログ）を同期させる
        /// </summary>
        /// <param name="iRowIndex"></param>
        /// <param name="iColumnIndex"></param>
        private void UpSyncOn(int iRowIndex, int iColumnIndex)
        {
            try
            {
                if (iPrevUpColumnIndex != -1)
                {
                    // 前回選択した列の色設定をクリアする
                    DgvInstructionData.Columns[iPrevUpColumnIndex].DefaultCellStyle.BackColor = Color.White;
                }
                // 今回選択した列の色設定を行う
                DgvInstructionData.Columns[iColumnIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                // 今回選択した行を選択状態とする
                DgvInstructionData.Rows[iRowIndex].Selected = true;
                // 今回選択した列情報を前回値とする
                iPrevUpColumnIndex = iColumnIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【UpSyncOn】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 下のデータグリッド（指示データ）を同期させる
        /// </summary>
        /// <param name="iRowIndex"></param>
        /// <param name="iColumnIndex"></param>
        private void DownSyncOn(int iRowIndex, int iColumnIndex)
        {
            try
            {
                if (iPrevDownColumnIndex != -1)
                {
                    // 前回選択した列の色設定をクリアする
                    DgvProductionLog.Columns[iPrevDownColumnIndex].DefaultCellStyle.BackColor = Color.White;
                }
                // 今回選択した列の色設定を行う
                DgvProductionLog.Columns[iColumnIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                if (DgvProductionLog.Rows.Count > iRowIndex)
                {
                    // 今回選択した行を選択状態とする
                    DgvProductionLog.Rows[iRowIndex].Selected = true;

                }
                // 今回選択した列情報を前回値とする
                iPrevDownColumnIndex = iColumnIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DownSyncOn】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「全抽出」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFullExtraction_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbFinalSeisanLog.Items.Count < 1 && CmbInstructionData.Items.Count < 1)
                {
                    MessageBox.Show("最終生産ログと指示データが選択されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CmbFinalSeisanLog.Items.Count < 1)
                {
                    MessageBox.Show("最終生産ログが選択されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CmbInstructionData.Items.Count < 1)
                {
                    MessageBox.Show("指示データが選択されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LblInstructionData.Text = "指示データ（" + CmbInstructionData.Text + "）";
                LblFinalSeisanLog.Text = "最終生産ログ（" + CmbFinalSeisanLog.Text + "）";

                PicWaiting1.Visible = true;
                PicWaiting1.Refresh();
                PicWaiting2.Visible = true;
                PicWaiting2.Refresh();

                // 指示データファイル読込処理
                DgvInstructionData.DataSource = ReadCollationInstructionDataFile();
                // 最終生産ログファイル読込処理
                DgvProductionLog.DataSource = ReadFinalProductionLogFile();

                // フィーダーの選択状態を取得する
                GetCeckBoxStatus();

                bool result = CompareDataTable(dtCollationInstructionDataFile, dtFinalProductionLogFile, 1, 99999);

                PicWaiting1.Visible = false;
                PicWaiting2.Visible = false;

                if (result)
                {
                    var msg = new ButtonTextCustomizableMessageBox();
                    msg.ButtonText.Cancel = "画面印刷";
                    var dialogResult = msg.Show("不整合なログレコードは存在しません", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Cancel)
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【BtnFullExtraction_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// データテーブルのヘッダー設定
        /// </summary>
        /// <param name="dt"></param>
        private void SetDataTableHeader(DataTable dt)
        {
            try
            {
                if (dt.Columns.Count > 1)
                {
                    return;
                }
                dt.Columns.Add("union", typeof(string)).ColumnName = "   組合員番号   ";
                dt.Columns.Add("set", typeof(string)).ColumnName = "セット順";
                dt.Columns.Add("dt1", typeof(string)).ColumnName = "   DT1  ";
                dt.Columns.Add("dt2", typeof(string)).ColumnName = "   DT2  ";
                dt.Columns.Add("dt3", typeof(string)).ColumnName = "   DT3  ";
                dt.Columns.Add("dt4", typeof(string)).ColumnName = "   DT4  ";
                dt.Columns.Add("dt5", typeof(string)).ColumnName = "   DT5  ";
                dt.Columns.Add("dt6", typeof(string)).ColumnName = "   DT6  ";
                dt.Columns.Add("f01", typeof(string)).ColumnName = "   F01  ";
                dt.Columns.Add("f02", typeof(string)).ColumnName = "   F02  ";
                dt.Columns.Add("f03", typeof(string)).ColumnName = "   F03  ";
                dt.Columns.Add("f04", typeof(string)).ColumnName = "   F04  ";
                dt.Columns.Add("f05", typeof(string)).ColumnName = "   F05  ";
                dt.Columns.Add("f06", typeof(string)).ColumnName = "   F06  ";
                dt.Columns.Add("f07", typeof(string)).ColumnName = "   F07  ";
                dt.Columns.Add("f08", typeof(string)).ColumnName = "   F08  ";
                dt.Columns.Add("f09", typeof(string)).ColumnName = "   F09  ";
                dt.Columns.Add("f10", typeof(string)).ColumnName = "   F10  ";
                dt.Columns.Add("f11", typeof(string)).ColumnName = "   F11  ";
                dt.Columns.Add("f12", typeof(string)).ColumnName = "   F12  ";
                dt.Columns.Add("f13", typeof(string)).ColumnName = "   F13  ";
                dt.Columns.Add("f14", typeof(string)).ColumnName = "   F14  ";
                dt.Columns.Add("f15", typeof(string)).ColumnName = "   F15  ";
                dt.Columns.Add("f16", typeof(string)).ColumnName = "   F16  ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【SetDataGridHeader】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable dtCollationInstructionDataFile = new DataTable();
        private DataTable dtFinalProductionLogFile = new DataTable();

        /// <summary>
        /// 指示データファイル読込処理
        /// </summary>
        /// <returns></returns>
        private DataTable ReadCollationInstructionDataFile()
        {
            string sFileName;
            string sReadData;
            string[] sArray;

            //DataTable dt = new DataTable();
            dtCollationInstructionDataFile.Clear();
            SetDataTableHeader(dtCollationInstructionDataFile);

            try
            {
                string strYYYYMMDD = DTPicWorkDay.Value.ToString("yyyyMMdd");
                sFileName = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + @"\" + CmbInstructionData.Text;
                using (StreamReader sr = new StreamReader(sFileName, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        sReadData = sr.ReadLine();
                        sArray = sReadData.Split(',');
                        dtCollationInstructionDataFile.Rows.Add(new object[] {sArray[17].PadLeft(8).Replace(" ","0"), sArray[43].PadLeft(6).Replace(" ","0"),
                                                  sArray[35], sArray[36], sArray[37], sArray[38], sArray[39], sArray[40],
                                                  sArray[19], sArray[20], sArray[21], sArray[22],
                                                  sArray[23], sArray[24], sArray[25], sArray[26],
                                                  sArray[27], sArray[28], sArray[29], sArray[30],
                                                  sArray[31], sArray[32], sArray[33], sArray[34],});
                    }
                }
                // 昇順で取得します
                dtCollationInstructionDataFile = GetSortedDataTable(dtCollationInstructionDataFile, "セット順 ASC");

                return dtCollationInstructionDataFile;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetDataTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtCollationInstructionDataFile.Rows.Add(new object[] {"", "",
                                                                      "", "", "", "", "", "",
                                                                      "", "", "", "",
                                                                      "", "", "", "",
                                                                      "", "", "", "",
                                                                      "", "", "", "",});
                return dtCollationInstructionDataFile;
            }
        }

        /// <summary>
        /// 最終生産ログファイル読込処理
        /// </summary>
        /// <returns></returns>
        private DataTable ReadFinalProductionLogFile()
        {
            string sFileName;
            string sReadData;
            string[] sArray;

            //DataTable dt = new DataTable();
            dtFinalProductionLogFile.Clear();
            SetDataTableHeader(dtFinalProductionLogFile);

            try
            {
                string strYYYYMMDD = DTPicWorkDay.Value.ToString("yyyyMMdd");
                sFileName = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + @"\" + CmbFinalSeisanLog.Text;
                using (StreamReader sr = new StreamReader(sFileName, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        sReadData = sr.ReadLine();
                        sArray = sReadData.Split(',');
                        dtFinalProductionLogFile.Rows.Add(new object[] { sArray[0].Substring(10,8),
                                                   sArray[0].Substring(18,6),
                                                   sArray[4].Substring(16,1), sArray[4].Substring(17,1), sArray[4].Substring(18,1),
                                                   sArray[4].Substring(19,1), sArray[4].Substring(20,1), sArray[4].Substring(21,1),
                                                   sArray[4].Substring(00,1), sArray[4].Substring(01,1), sArray[4].Substring(02,1), sArray[4].Substring(03,1),
                                                   sArray[4].Substring(04,1), sArray[4].Substring(05,1), sArray[4].Substring(06,1), sArray[4].Substring(07,1),
                                                   sArray[4].Substring(08,1), sArray[4].Substring(09,1), sArray[4].Substring(10,1), sArray[4].Substring(11,1),
                                                   sArray[4].Substring(12,1), sArray[4].Substring(13,1), sArray[4].Substring(14,1), sArray[4].Substring(15,1),});
                    }
                }
                // 昇順で取得します
                dtFinalProductionLogFile = GetSortedDataTable(dtFinalProductionLogFile, "セット順 ASC");
                return dtFinalProductionLogFile;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【GetDataTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtFinalProductionLogFile.Rows.Add(new object[] {"", "",
                                                                "", "", "", "", "", "",
                                                                "", "", "", "",
                                                                "", "", "", "",
                                                                "", "", "", "",
                                                                "", "", "", "",});
                return dtFinalProductionLogFile;
            }
        }

        /// <summary>
        /// 指定したDataTableを対象にsort文字列で並び替えた結果を返します。
        /// </summary>
        /// <param name="dt">並び替え対象となるDataTableです。</param>
        /// <param name="sort">ソート条件</param>
        /// <returns>並び替え後のDataTalbeです。</returns>
        static public DataTable GetSortedDataTable(DataTable dt, string sort)
        {
            // dtのスキーマや制約をコピーしたDataTableを作成します。
            DataTable table = dt.Clone();

            DataRow[] rows = dt.Select(null, sort);

            foreach (DataRow row in rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピーします。
                addRow.ItemArray = row.ItemArray;

                // DataTableに格納します。
                table.Rows.Add(addRow);
            }

            return table;
        }

        /// <summary>
        /// 「差分抽出」ボタン処理（テスト用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDifferenceExtraction_Click(object sender, EventArgs e)
        {
            try
            {
                // 最終生産ログのテーブルと指示データのテーブルの差分を抽出
                DataTable dtDiffForUp = new DataTable();
                DataTable dtDiffForDown = new DataTable();

                var wkDiffsForUp = dtCollationInstructionDataFile.AsEnumerable().Except(dtFinalProductionLogFile.AsEnumerable(), DataRowComparer.Default);
                var wkDiffsForDown = dtFinalProductionLogFile.AsEnumerable().Except(dtCollationInstructionDataFile.AsEnumerable(), DataRowComparer.Default);

                if (wkDiffsForDown.Any())
                {
                    // 最終生産ログの異なるレコードの表示
                    dtDiffForDown = wkDiffsForDown.CopyToDataTable();
                    DgvProductionLog.DataSource = dtDiffForDown;

                    if (wkDiffsForDown.Any())
                    {
                        // 指示データの異なるレコードの表示
                        dtDiffForUp = wkDiffsForUp.CopyToDataTable();
                        DgvInstructionData.DataSource = dtDiffForUp;
                    }
                }
                else
                {
                    MessageBox.Show("不整合なログレコードは存在しません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【BtnDifferenceExtraction_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「作業日付」作業日選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DTPicWorkDay_ValueChanged(object sender, EventArgs e)
        {
            string strYYYYMMDD;
            string[] strArray;
            string strErrorMessage;

            try
            {
                CmbFinalSeisanLog.Items.Clear();
                CmbInstructionData.Items.Clear();

                strYYYYMMDD = DTPicWorkDay.Value.ToString("yyyyMMdd") + @"\";
                // 生産ログ格納フォルダの存在チェック
                if (Directory.Exists(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD) == false)
                {
                    LblMessage.Text = "生産ログ格納フォルダが見つかりません";
                    return;
                }
                LblMessage.Text = "";
                strErrorMessage = "";
                // 最終生産ログファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder),
                                                                                   strYYYYMMDD + "*.plog", SearchOption.TopDirectoryOnly))
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                        CmbFinalSeisanLog.Items.Add(strArray[strArray.Length - 1]);
                    CmbFinalSeisanLog.SelectedIndex = 0;
                }
                if (CmbFinalSeisanLog.SelectedIndex == -1)
                {
                    strErrorMessage = "最終生産実績ログがありません。";
                }

                // 指示データファイル一覧の取得
                foreach (string strInstructionDataFullPathName in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder),
                                                                                     strYYYYMMDD + "YK520P*", SearchOption.TopDirectoryOnly))
                {
                    strArray = strInstructionDataFullPathName.Split('\\');
                    if (strArray.Length > 2)
                        CmbInstructionData.Items.Add(strArray[strArray.Length - 1]);
                    CmbInstructionData.SelectedIndex = 0;
                }
                if (CmbInstructionData.SelectedIndex == -1)
                {
                    strErrorMessage += Environment.NewLine + "指示データがありません。";
                }

                if (strErrorMessage != "")
                {
                    MessageBox.Show(strErrorMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DTPicWorkDay_ValueChanged】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「選択抽出」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectiveExtraction_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbFinalSeisanLog.Items.Count < 1 && CmbInstructionData.Items.Count < 1)
                {
                    MessageBox.Show("最終生産ログと指示データが選択されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CmbFinalSeisanLog.Items.Count < 1)
                {
                    MessageBox.Show("最終生産ログが選択されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CmbInstructionData.Items.Count < 1)
                {
                    MessageBox.Show("指示データが選択されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (TxtSetOrderFrom.Text == "" && TxtSetOrderTo.Text == "")
                {
                    MessageBox.Show("セット順序（From ～ To）を設定して下さい", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (TxtSetOrderFrom.Text == "")
                {
                    TxtSetOrderFrom.Text = "1";
                }
                if (TxtSetOrderTo.Text == "")
                {
                    TxtSetOrderTo.Text = "999999";
                }

                if (int.Parse(TxtSetOrderFrom.Text) > int.Parse(TxtSetOrderTo.Text))
                {
                    MessageBox.Show("セット順序（From <= To）で設定して下さい", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DgvInstructionData.DataSource = GetDataTable();
                DgvProductionLog.DataSource = GetDataTable();

                PicWaiting1.Visible = true;
                PicWaiting1.Refresh();
                PicWaiting2.Visible = true;
                PicWaiting2.Refresh();

                LblInstructionData.Text = "指示データ（" + CmbInstructionData.Text + "）";
                LblFinalSeisanLog.Text = "最終生産ログ（" + CmbFinalSeisanLog.Text + "）";

                // 指示データファイル読込処理
                DgvInstructionData.DataSource = ReadCollationInstructionDataFile();
                // 最終生産ログファイル読込処理
                DgvProductionLog.DataSource = ReadFinalProductionLogFile();

                // フィーダーの選択状態を取得する
                GetCeckBoxStatus();

                int iSetOrderFrom = int.Parse(TxtSetOrderFrom.Text);
                int iSetOrderTo = int.Parse(TxtSetOrderTo.Text);
                bool result = CompareDataTable(dtCollationInstructionDataFile, dtFinalProductionLogFile, iSetOrderFrom, iSetOrderTo);

                PicWaiting1.Visible = false;
                PicWaiting2.Visible = false;

                if (result)
                {
                    var msg = new ButtonTextCustomizableMessageBox();
                    //msg.ButtonText.Yes = "OK";
                    msg.ButtonText.Cancel = "画面印刷";
                    var dialogResult = msg.Show("不整合なログレコードは存在しません", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (dialogResult == DialogResult.Cancel)
                    {
                        //MessageBox.Show("画面印刷中です・・・", "デバッグ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【BtnSelectiveExtraction_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable dt1Work = new DataTable();
        private DataTable dt2Work = new DataTable();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="iSetOrderFrom"></param>
        /// <param name="iSetOrderTo"></param>
        /// <returns></returns>
        private bool CompareDataTable(DataTable dt1, DataTable dt2, int iSetOrderFrom, int iSetOrderTo)
        {
            if (dt1 == null && dt2 == null)
            {
                MessageBox.Show("丁合指示データ及び最終生産ログが空です", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            if (dt1 == null && dt2 != null)
            {
                MessageBox.Show("丁合指示データが空です", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (dt1 != null && dt2 == null)
            {
                MessageBox.Show("最終生産ログが空です", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (dt1.Rows.Count != dt2.Rows.Count)
            {
                //MessageBox.Show("丁合指示データと最終生産ログの件数が異なります", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //return false;
            }

            dt1Work.Clear();
            dt2Work.Clear();
            SetDataTableHeader(dt1Work);
            SetDataTableHeader(dt2Work);
            string[] sArray1 = new string[24];
            string[] sArray2 = new string[24];
            for (int row2 = 0; row2 < dt2.Rows.Count; row2++)
            {

                int iSetOrder = int.Parse(dt2.Rows[row2][1].ToString());
                if (iSetOrder < iSetOrderFrom || iSetOrder > iSetOrderTo)
                {
                    // セット順が抽出対象外
                    continue;
                }

                int iRowIndex = 0;
                bool bFind = false;
                bool bFindDiff = false;
                for (int row1 = 0; row1 < dt1.Rows.Count; row1++)
                {
                    // 最終生産ログのセット順が丁合指示データに存在するかのチェック
                    if (dt2.Rows[row2][1].Equals(dt1.Rows[row1][1]))
                    {
                        iRowIndex = row1;
                        bFind = true;
                        break;
                    }
                }

                if (bFind)
                {
                    // 最終生産ログのセット順が丁合指示データに存在する場合
                    bFindDiff = false;
                    for (int col1 = 0; col1 < dt2.Columns.Count; col1++)
                    {
                        if (dt2.Rows[row2][col1].Equals(dt1.Rows[iRowIndex][col1]))
                        {
                            sArray2[col1] = dt2.Rows[row2][col1].ToString();
                            sArray1[col1] = dt1.Rows[iRowIndex][col1].ToString();
                        }
                        else
                        {
                            // 最終生産ログと丁合指示データの項目が異なる（なおかつ丁合指示データの値が「1」の場合）
                            if (bCheckBoxStatus[col1] == true && dt1.Rows[iRowIndex][col1].ToString() == "1")
                            {
                                sArray2[col1] = dt2.Rows[row2][col1] + "▲";
                                sArray1[col1] = dt1.Rows[iRowIndex][col1] + "▲";
                                bFindDiff = true;
                            }
                            else
                            {
                                sArray2[col1] = dt2.Rows[row2][col1].ToString() + "";
                                sArray1[col1] = dt1.Rows[iRowIndex][col1].ToString() + "";
                                //bFindDiff = true;
                            }
                        }
                    }
                    if (bFindDiff)
                    {
                        // 最終生産ログと丁合指示データの内容が異なる
                        dt1Work.Rows.Add(sArray1);
                        dt2Work.Rows.Add(sArray2);

                        DgvProductionLog.DataSource = dt2Work;
                        DgvInstructionData.DataSource = dt1Work;
                    }
                }
                else
                {
                    //////////////////////////////////////////////////////
                    // 最終生産ログのセット順が丁合指示データに無い場合 //
                    //////////////////////////////////////////////////////
                    for (int col1 = 0; col1 < dt2.Columns.Count; col1++)
                    {
                        if (col1 == 0)
                        {
                            // 組合員番号のデータに「▲」を付加する
                            sArray2[col1] = dt2.Rows[row2][col1] + "▲";
                        }
                        else
                        {
                            sArray2[col1] = dt2.Rows[row2][col1].ToString();
                        }
                    }
                    // 最終生産ログのみ抽出結果をセット
                    dt2Work.Rows.Add(sArray2);
                }
            }
            // 抽出結果の表示
            DgvInstructionData.DataSource = dt1Work;
            DgvProductionLog.DataSource = dt2Work;
            TxtExtractionNumber.Text = (DgvProductionLog.RowCount - 1).ToString();
            if (DgvProductionLog.RowCount > 1)
            {
                // 不整合なログが存在する
                SetBackColor();
                return false;
            }
            else
            {
                // 不整合なログは無し
                return true;
            }
        }

        /// <summary>
        /// 値が異なる項目の背景色をセット
        /// </summary>
        private void SetBackColor()
        {
            string sData;
            try
            {
                for (int N = 0; N < DgvInstructionData.RowCount - 1; N++)
                {
                    for (int K = 0; K < DgvInstructionData.ColumnCount; K++)
                    {
                        sData = DgvInstructionData[K, N].Value.ToString();
                        if (sData.Contains("▲"))
                        {
                            DgvInstructionData[K, N].Value = sData.Replace("▲", "");
                            DgvInstructionData[K, N].Style.BackColor = Color.Yellow;
                        }
                    }
                }

                for (int N = 0; N < DgvProductionLog.RowCount - 1; N++)
                {
                    for (int K = 0; K < DgvProductionLog.ColumnCount; K++)
                    {
                        sData = DgvProductionLog[K, N].Value.ToString();
                        if (sData.Contains("▲"))
                        {
                            DgvProductionLog[K, N].Value = sData.Replace("▲", "");
                            DgvProductionLog[K, N].Style.BackColor = Color.Yellow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【SetBackColor】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool[] bCheckBoxStatus = new bool[24];

        /// <summary>
        /// フィーダーの選択状態を取得する
        /// </summary>
        private void GetCeckBoxStatus()
        {
            try
            {
                bCheckBoxStatus[0] = true;
                bCheckBoxStatus[1] = true;
                // ディタッチャー（DT1～DT6）
                bCheckBoxStatus[2] = ChkDtStatus01.Checked ? true : false;
                bCheckBoxStatus[3] = ChkDtStatus02.Checked ? true : false;
                bCheckBoxStatus[4] = ChkDtStatus03.Checked ? true : false;
                bCheckBoxStatus[5] = ChkDtStatus04.Checked ? true : false;
                bCheckBoxStatus[6] = ChkDtStatus05.Checked ? true : false;
                bCheckBoxStatus[7] = ChkDtStatus06.Checked ? true : false;
                // フィーダー（FD1～FD16）
                bCheckBoxStatus[08] = ChkFdStatus01.Checked ? true : false;
                bCheckBoxStatus[09] = ChkFdStatus02.Checked ? true : false;
                bCheckBoxStatus[10] = ChkFdStatus03.Checked ? true : false;
                bCheckBoxStatus[11] = ChkFdStatus04.Checked ? true : false;
                bCheckBoxStatus[12] = ChkFdStatus05.Checked ? true : false;
                bCheckBoxStatus[13] = ChkFdStatus06.Checked ? true : false;
                bCheckBoxStatus[14] = ChkFdStatus07.Checked ? true : false;
                bCheckBoxStatus[15] = ChkFdStatus08.Checked ? true : false;
                bCheckBoxStatus[16] = ChkFdStatus09.Checked ? true : false;
                bCheckBoxStatus[17] = ChkFdStatus10.Checked ? true : false;
                bCheckBoxStatus[18] = ChkFdStatus11.Checked ? true : false;
                bCheckBoxStatus[19] = ChkFdStatus12.Checked ? true : false;
                bCheckBoxStatus[20] = ChkFdStatus13.Checked ? true : false;
                bCheckBoxStatus[21] = ChkFdStatus14.Checked ? true : false;
                bCheckBoxStatus[22] = ChkFdStatus15.Checked ? true : false;
                bCheckBoxStatus[23] = ChkFdStatus16.Checked ? true : false;

                int iIndex = 1;
                string sData = "";
                foreach (var s in bCheckBoxStatus)
                {
                    if (iIndex == 1)
                    {
                        sData += "組合員：" + s + Environment.NewLine;
                    }
                    else if (iIndex == 2)
                    {
                        sData += "セット順：" + s + Environment.NewLine;
                        sData += "--------------------" + Environment.NewLine;
                    }
                    else if (iIndex > 8)
                    {
                        sData += (iIndex - 8).ToString("00") + "：" + s + Environment.NewLine;
                    }
                    else
                    {
                        sData += (iIndex - 2).ToString("00") + "：" + s + Environment.NewLine;
                        if (iIndex == 8)
                        {
                            sData += "--------------------" + Environment.NewLine;
                        }
                    }
                    iIndex++;
                }
                //MessageBox.Show(sData, "【デバッグ】", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【BtnSelectiveExtraction_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 数字以外のキー入力をキャンセルする（セット順序ＦＲＯＭ）
        /// </summary>
        /// <param name="sender"></param>
        /// 12<param name="e"></param>
        private void TxtSetOrderFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //0～9と、バックスペース以外の時は、イベントをキャンセルする
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【TxtSetOrderFrom_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 数字以外のキー入力をキャンセルする（セット順序ＴＯ）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSetOrderTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //0～9と、バックスペース以外の時は、イベントをキャンセルする
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【TxtSetOrderFrom_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「画面印刷」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScreenPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var dialogResult = MessageBox.Show("画面印字しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

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
                MessageBox.Show(ex.Message, "【BtnScreenPrint_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「ﾌｧｲﾙ保存」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFileSave_Click(object sender, EventArgs e)
        {
            string sFileName;
            string[] sArray;
            string sCsvData;

            try
            {
                string strYYYYMMDD = DTPicWorkDay.Value.ToString("yyyyMMdd");
                string sFinalSeisanLog = CmbFinalSeisanLog.Text.Replace(".plog", "_抽出.plog");
                sFileName = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + @"\" + sFinalSeisanLog;
                // エンコーディングの全てを使用できるようにする
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (StreamWriter writer = new StreamWriter(sFileName, false, Encoding.GetEncoding("shift_Jis")))
                {
                    int row = DgvProductionLog.RowCount;
                    int colunms = DgvProductionLog.ColumnCount;

                    List<string> sList = new List<string>();

                    for (int i = 0; i < row - 1; i++)
                    {
                        sList = new List<string>();

                        for (int j = 0; j < colunms; j++)
                        {
                            sList.Add(DgvProductionLog[j, i].Value.ToString());
                        }

                        sArray = sList.ToArray();
                        sCsvData = string.Join(",", sArray);
                        writer.WriteLine(sCsvData);
                    }
                }

                string sMessage = "CSVファイルを保存しました";
                MessageBox.Show(sMessage, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //DisplayNormalMessage(sMessage, LblStatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【BtnFileSave_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
