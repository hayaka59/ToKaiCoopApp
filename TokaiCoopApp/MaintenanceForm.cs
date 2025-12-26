using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class MaintenanceForm : Form
    {
        public MaintenanceForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MaintenanceForm_Load(Object sender, EventArgs e)
        {
            int intLoopCnt;
            string[] strArray;

            try
            {
                LblTitle.Text = "保守画面";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                // システム定義ファイル読込
                CommonModule.GetSystemDefinition();
                // ブザー設定名称読込
                BuzzerSetName();
                // ブザー音色名称読込
                BuzzerToneName();
                if (PubConstClass.pblToneName1 != "")
                    CmbBuzzer1.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName1) - 1;
                if (PubConstClass.pblToneName2 != "")
                    CmbBuzzer2.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName2) - 1;
                if (PubConstClass.pblToneName3 != "")
                    CmbBuzzer3.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName3) - 1;
                if (PubConstClass.pblToneName4 != "")
                    CmbBuzzer4.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName4) - 1;
                if (PubConstClass.pblToneName5 != "")
                    CmbBuzzer5.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName5) - 1;
                if (PubConstClass.pblToneName6 != "")
                    CmbBuzzer6.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName6) - 1;
                if (PubConstClass.pblToneName7 != "")
                    CmbBuzzer7.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName7) - 1;
                if (PubConstClass.pblToneName8 != "")
                    CmbBuzzer8.SelectedIndex = Convert.ToInt32(PubConstClass.pblToneName8) - 1;

                // 号機名称
                TxtMachineName.Text = PubConstClass.pblMachineName;

                // ライン番号の設定
                CmbLineNo.Items.Clear();
                CmbLineNo.Items.Add("1.TP古賀");
                CmbLineNo.Items.Add("2.サンシャインワークス");
                CmbLineNo.Items.Add("3.予備");
                CmbLineNo.SelectedIndex = int.Parse(PubConstClass.pblLineNumber) - 1;

                // ディスク空き容量
                TxtHddSpace.Text = PubConstClass.pblHddSpace;

                // パスワード
                TxtPassword.Text = PubConstClass.pblPassWord;

                // 自動判定モード
                if (PubConstClass.pblIsAutoJudge == "1")
                    ChkAutoJudgeMode.Checked = true;
                else
                    ChkAutoJudgeMode.Checked = false;

                // エラー表示時間
                TxtErrorDispTime.Text = PubConstClass.pblErrorDispTime;

                // 運転画面表示位置（Ｘ座標）
                TxtXPosition.Text = PubConstClass.intXPosition.ToString();

                // CONTEC DIOｰ0808LYのデバイスID
                TxtDioName.Text = PubConstClass.sDioName;

                #region ブザー発音時間コンボボックスの設定
                // ブザー発音時間（秒）コンボボックスのクリア
                CmbHour1.Items.Clear();
                CmbHour2.Items.Clear();
                CmbHour3.Items.Clear();
                CmbHour4.Items.Clear();
                CmbHour5.Items.Clear();
                CmbHour6.Items.Clear();
                CmbHour7.Items.Clear();
                CmbHour8.Items.Clear();
                // ブザー発音時間（msec）コンボボックスのクリア
                CmbSecond1.Items.Clear();
                CmbSecond2.Items.Clear();
                CmbSecond3.Items.Clear();
                CmbSecond4.Items.Clear();
                CmbSecond5.Items.Clear();
                CmbSecond6.Items.Clear();
                CmbSecond7.Items.Clear();
                CmbSecond8.Items.Clear();
                for (intLoopCnt = 0; intLoopCnt <= 9; intLoopCnt++)
                {
                    // ブザー発音時間（秒）の設定コンボボックスの設定
                    CmbHour1.Items.Add(intLoopCnt.ToString("0"));
                    CmbHour2.Items.Add(intLoopCnt.ToString("0"));
                    CmbHour3.Items.Add(intLoopCnt.ToString("0"));
                    CmbHour4.Items.Add(intLoopCnt.ToString("0"));
                    CmbHour5.Items.Add(intLoopCnt.ToString("0"));
                    CmbHour6.Items.Add(intLoopCnt.ToString("0"));
                    CmbHour7.Items.Add(intLoopCnt.ToString("0"));
                    CmbHour8.Items.Add(intLoopCnt.ToString("0"));
                    // ブザー発音時間（msec）の設定コンボボックスの設定
                    CmbSecond1.Items.Add(intLoopCnt.ToString("0"));
                    CmbSecond2.Items.Add(intLoopCnt.ToString("0"));
                    CmbSecond3.Items.Add(intLoopCnt.ToString("0"));
                    CmbSecond4.Items.Add(intLoopCnt.ToString("0"));
                    CmbSecond5.Items.Add(intLoopCnt.ToString("0"));
                    CmbSecond6.Items.Add(intLoopCnt.ToString("0"));
                    CmbSecond7.Items.Add(intLoopCnt.ToString("0"));
                    CmbSecond8.Items.Add(intLoopCnt.ToString("0"));
                }
                CmbHour1.SelectedIndex = 2;
                CmbHour2.SelectedIndex = 2;
                CmbHour3.SelectedIndex = 2;
                CmbHour4.SelectedIndex = 2;
                CmbHour5.SelectedIndex = 2;
                CmbHour6.SelectedIndex = 2;
                CmbHour7.SelectedIndex = 2;
                CmbHour8.SelectedIndex = 2;
                CmbSecond1.SelectedIndex = 0;
                CmbSecond2.SelectedIndex = 0;
                CmbSecond3.SelectedIndex = 0;
                CmbSecond4.SelectedIndex = 0;
                CmbSecond5.SelectedIndex = 0;
                CmbSecond6.SelectedIndex = 0;
                CmbSecond7.SelectedIndex = 0;
                CmbSecond8.SelectedIndex = 0;
                #endregion

                #region ブザーの発音時間の設定
                if (PubConstClass.pblToneTime1 != "")
                {
                    strArray = PubConstClass.pblToneTime1.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour1.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond1.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                if (PubConstClass.pblToneTime2 != "")
                {
                    strArray = PubConstClass.pblToneTime2.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour2.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond2.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                if (PubConstClass.pblToneTime3 != "")
                {
                    strArray = PubConstClass.pblToneTime3.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour3.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond3.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                if (PubConstClass.pblToneTime4 != "")
                {
                    strArray = PubConstClass.pblToneTime4.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour4.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond4.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                if (PubConstClass.pblToneTime5 != "")
                {
                    strArray = PubConstClass.pblToneTime5.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour5.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond5.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                if (PubConstClass.pblToneTime6 != "")
                {
                    strArray = PubConstClass.pblToneTime6.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour6.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond6.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                if (PubConstClass.pblToneTime7 != "")
                {
                    strArray = PubConstClass.pblToneTime7.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour7.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond7.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                if (PubConstClass.pblToneTime8 != "")
                {
                    strArray = PubConstClass.pblToneTime8.Split('.');
                    if (strArray.Length > 1)
                    {
                        CmbHour8.SelectedIndex = Convert.ToInt32(strArray[0]);
                        CmbSecond8.SelectedIndex = Convert.ToInt32(strArray[1]);
                    }
                }
                #endregion

                #region ログ保存
                CmbSaveMonth.Items.Clear();
                for (int N=1; N <= 36; N++)
                {
                    CmbSaveMonth.Items.Add(N.ToString() + "ヶ月");
                }
                if (PubConstClass.pblSaveLogMonth != "")
                {
                    CmbSaveMonth.SelectedIndex = Convert.ToInt32(PubConstClass.pblSaveLogMonth) - 1;
                }
                else
                {
                    CmbSaveMonth.SelectedIndex = 0;
                }
                #endregion

                #region Ａ４レーザープリンタ名称取得
                intLoopCnt = 0;
                int intIndex = 0;
                CmbReportPrinter.Items.Clear();
                foreach (string p in PrinterSettings.InstalledPrinters)
                {
                    CmbReportPrinter.Items.Add(p);
                    if (PubConstClass.pblReportPrinterName != "")
                    {
                        if (p.StartsWith(PubConstClass.pblReportPrinterName) == true)
                            intIndex = intLoopCnt;
                    }
                    intLoopCnt += 1;
                }
                CmbReportPrinter.SelectedIndex = intIndex;
                #endregion

                #region ジャーナルプリンタ名称取得
                intLoopCnt = 0;
                CmbJournalPrinter.Items.Clear();
                foreach (string p in PrinterSettings.InstalledPrinters)
                {
                    CmbJournalPrinter.Items.Add(p);
                    if (PubConstClass.pblJournalPrinterName != "")
                    {
                        if (p.StartsWith(PubConstClass.pblJournalPrinterName) == true)
                            intIndex = intLoopCnt;
                    }
                    intLoopCnt += 1;
                }
                CmbJournalPrinter.SelectedIndex = intIndex;
                #endregion

                #region フォルダ設定
                TxtChoaiData.Text = PubConstClass.pblChoaiFolder;
                TxtControlData.Text = PubConstClass.pblControlFolder;
                TxtSeisan.Text = PubConstClass.pblSeisanFolder;
                TxtFinalSeisan.Text = PubConstClass.pblFinalSeisanFolder;
                TxtEvent.Text = PubConstClass.pblEventFolder;
                TxtFinalEvent.Text = PubConstClass.pblFinalEventFolder;
                TxtOutPutFolder.Text = PubConstClass.pblFormOutPutFolder;
                TxtOperationPcTotallingFolder.Text = PubConstClass.pblOperationPcTotallingFolder;
                TxtOperationPcEventFolder.Text = PubConstClass.pblOperationPcEventFolder;
                TxtInternalTran.Text = PubConstClass.pblInternalTranFolder;
                #endregion

                // 結束テーブル内容の取得
                DGViewUnityTable.DataSource = GetDataTable();
                // 結束テーブルの各ヘッダーの並び替え不可とする
                foreach (DataGridViewColumn c in DGViewUnityTable.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //インデックス１と４の列のセルの背景色を薄い緑色にする
                DGViewUnityTable.Columns[1].DefaultCellStyle.BackColor = Color.LightGreen;
                DGViewUnityTable.Columns[4].DefaultCellStyle.BackColor = Color.LightGreen;

                // 結束テーブル読込
                LoadBindingTableFile();
                #region 結束テーブル読込
                //string sFileName;
                //string sReadData;
                //sFileName = CommonModule.IncludeTrailingPathDelimiter(Environment.CurrentDirectory) + "結束テーブル.txt";
                //using (StreamReader sr = new StreamReader(sFileName, Encoding.Default))
                //{
                //    PubConstClass.sListUnityTable.Clear();
                //    while (!sr.EndOfStream)
                //    {
                //        sReadData = sr.ReadLine();
                //        PubConstClass.sListUnityTable.Add(sReadData);
                //        string[] sAry = sReadData.Split(',');
                //        CmbUnityTable.Items.Add("結束数：" + sAry[1] + "／積載数：" + sAry[4]);
                //    }
                //}
                //CmbUnityTable.SelectedIndex = int.Parse(PubConstClass.pblMaxOfBundle) - 1;
                #endregion

                string[] sDummy = new string[4] { "１束", "２束", "３束", "４束" };
                CmbDummy1.Items.AddRange(sDummy);
                CmbDummy2.Items.AddRange(sDummy);
                CmbDummy3.Items.AddRange(sDummy);
                CmbDummy4.Items.AddRange(sDummy);
                CmbDummy1.SelectedIndex = PubConstClass.iDummyCreateCount1 - 1;
                CmbDummy2.SelectedIndex = PubConstClass.iDummyCreateCount2 - 1;
                CmbDummy3.SelectedIndex = PubConstClass.iDummyCreateCount3 - 1;
                CmbDummy4.SelectedIndex = PubConstClass.iDummyCreateCount4 - 1;

                // 厚物冊子使用フィーダー
                CmbThickBookletUseFeeder.Items.Clear();
                CmbThickBookletUseFeeder.Items.Add("使用しない");
                for (int iIndex = 1; iIndex <= 21; iIndex++)
                {
                    CmbThickBookletUseFeeder.Items.Add(iIndex.ToString());
                }
                CmbThickBookletUseFeeder.SelectedIndex = PubConstClass.iThickBookletUseFeeder;

                // ボール紙使用フィーダー
                CmbCardboard.Items.Clear();
                CmbCardboard.Items.Add("使用しない");
                for (int iIndex = 1; iIndex <= 21; iIndex++)
                {
                    CmbCardboard.Items.Add(iIndex.ToString());
                }
                CmbCardboard.SelectedIndex = PubConstClass.iCardboard;

                // 厚物冊子（N）冊以上
                CmbNumberOfBooks.Items.Clear();
                for (int iIndex = 1; iIndex <= 99; iIndex++)
                {
                    CmbNumberOfBooks.Items.Add(iIndex.ToString());
                }
                CmbNumberOfBooks.SelectedIndex = PubConstClass.iNumberOfBooks - 1;

                // COMポート名
                CmbComPort.Items.Clear();
                for (intLoopCnt = 1; intLoopCnt <= 15; intLoopCnt++)
                    CmbComPort.Items.Add("COM" + Convert.ToString(intLoopCnt));
                CmbComPort.SelectedIndex = Convert.ToInt32(PubConstClass.pblComPort.Substring(3, 1)) - 1;
                // COM通信速度
                CmbComSpeed.Items.Clear();
                CmbComSpeed.Items.Add("4800");
                CmbComSpeed.Items.Add("9600");
                CmbComSpeed.Items.Add("19200");
                CmbComSpeed.Items.Add("38400");
                CmbComSpeed.Items.Add("57600");
                CmbComSpeed.Items.Add("115200");
                CmbComSpeed.SelectedIndex = Convert.ToInt32(PubConstClass.pblComSpeed);
                // COMデータ長
                CmbComDataLength.Items.Clear();
                CmbComDataLength.Items.Add("8bit");
                CmbComDataLength.Items.Add("7bit");
                CmbComDataLength.SelectedIndex = Convert.ToInt32(PubConstClass.pblComDataLength);
                // COMパリティの有無
                CmbComIsParty.Items.Clear();
                CmbComIsParty.Items.Add("無効");
                CmbComIsParty.Items.Add("有効");
                CmbComIsParty.SelectedIndex = Convert.ToInt32(PubConstClass.pblComIsParity);
                // COMパリティ種別
                CmbComParityVar.Items.Clear();
                CmbComParityVar.Items.Add("奇数");
                CmbComParityVar.Items.Add("偶数");
                CmbComParityVar.SelectedIndex = Convert.ToInt32(PubConstClass.pblComParityVar);
                // COMストップビット
                CmbComStopBit.Items.Clear();
                CmbComStopBit.Items.Add("1bit");
                CmbComStopBit.Items.Add("2bit");
                CmbComStopBit.SelectedIndex = Convert.ToInt32(PubConstClass.pblComStopBit);

                // COMポート名
                CmbComPort2.Items.Clear();
                for (intLoopCnt = 1; intLoopCnt <= 15; intLoopCnt++)
                    CmbComPort2.Items.Add("COM" + Convert.ToString(intLoopCnt));
                if (PubConstClass.pblComPort2 == "")
                    CmbComPort2.SelectedIndex = 0;
                else
                    CmbComPort2.SelectedIndex = Convert.ToInt32(PubConstClass.pblComPort2.Substring(3, 1)) - 1;

                // COM通信速度
                CmbComSpeed2.Items.Clear();
                CmbComSpeed2.Items.Add("4800");
                CmbComSpeed2.Items.Add("9600");
                CmbComSpeed2.Items.Add("19200");
                CmbComSpeed2.Items.Add("38400");
                CmbComSpeed2.Items.Add("57600");
                CmbComSpeed2.Items.Add("115200");
                if (PubConstClass.pblComSpeed2 == "")
                    CmbComSpeed2.SelectedIndex = 0;
                else
                    CmbComSpeed2.SelectedIndex = Convert.ToInt32(PubConstClass.pblComSpeed2);

                // COMデータ長
                CmbComDataLength2.Items.Clear();
                CmbComDataLength2.Items.Add("8bit");
                CmbComDataLength2.Items.Add("7bit");
                if (PubConstClass.pblComDataLength2 == "")
                    CmbComDataLength2.SelectedIndex = 0;
                else
                    CmbComDataLength2.SelectedIndex = Convert.ToInt32(PubConstClass.pblComDataLength2);

                // COMパリティの有無
                CmbComIsParty2.Items.Clear();
                CmbComIsParty2.Items.Add("無効");
                CmbComIsParty2.Items.Add("有効");
                if (PubConstClass.pblComIsParity2 == "")
                    CmbComIsParty2.SelectedIndex = 0;
                else
                    CmbComIsParty2.SelectedIndex = Convert.ToInt32(PubConstClass.pblComIsParity2);

                // COMパリティ種別
                CmbComParityVar2.Items.Clear();
                CmbComParityVar2.Items.Add("奇数");
                CmbComParityVar2.Items.Add("偶数");
                if (PubConstClass.pblComParityVar2 == "")
                    CmbComParityVar2.SelectedIndex = 0;
                else
                    CmbComParityVar2.SelectedIndex = Convert.ToInt32(PubConstClass.pblComParityVar2);

                // COMストップビット
                CmbComStopBit2.Items.Clear();
                CmbComStopBit2.Items.Add("1bit");
                CmbComStopBit2.Items.Add("2bit");
                if (PubConstClass.pblComStopBit2 == "")
                    CmbComStopBit2.SelectedIndex = 0;
                else
                    CmbComStopBit2.SelectedIndex = Convert.ToInt32(PubConstClass.pblComStopBit2);

                // シリアルポートにデータ送信（メンテナンス画面遷移コマンド）
                byte[] dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(PubConstClass.CMD_SEND_n + Constants.vbCr);
                ((MainForm)Owner).SerialPort.Write(dat, 0, dat.GetLength(0));
                LoggingSerialSendData(PubConstClass.CMD_SEND_n);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message, "【maintenanceForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 結束テーブルファイルの読込
        /// </summary>
        private void LoadBindingTableFile()
        {
            try
            {
                string sFileName;
                string sReadData;
                sFileName = CommonModule.IncludeTrailingPathDelimiter(Environment.CurrentDirectory) + "結束テーブル.txt";
                using (StreamReader sr = new StreamReader(sFileName, Encoding.Default))
                {
                    PubConstClass.sListUnityTable.Clear();
                    CmbUnityTable.Items.Clear();
                    while (!sr.EndOfStream)
                    {
                        sReadData = sr.ReadLine();
                        PubConstClass.sListUnityTable.Add(sReadData);

                        string[] sAry = sReadData.Split(',');
                        CmbUnityTable.Items.Add("結束数：" + sAry[1] + "／積載数：" + sAry[4]);
                    }
                }
                if(CmbUnityTable.Items.Count >= int.Parse(PubConstClass.pblMaxOfBundle))
                {
                    CmbUnityTable.SelectedIndex = int.Parse(PubConstClass.pblMaxOfBundle) - 1;
                }
                else
                {
                    CmbUnityTable.SelectedIndex = CmbUnityTable.Items.Count - 1;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【LoadBindingTableFile】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 結束テーブル内容の取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("結束数");
            dt.Columns.Add("段数");
            dt.Columns.Add("面数");
            dt.Columns.Add("積載数");
            dt.Columns.Add("タイプ");

            string sFileName;
            string sReadData;
            string[] sArray;

            try
            {
                sFileName = CommonModule.IncludeTrailingPathDelimiter(Environment.CurrentDirectory) + "結束テーブル.txt";
                using (StreamReader sr = new StreamReader(sFileName, Encoding.Default))
                {
                    PubConstClass.sListUnityTable.Clear();
                    while (!sr.EndOfStream)
                    {
                        sReadData = sr.ReadLine();
                        PubConstClass.sListUnityTable.Add(sReadData);
                        sArray = sReadData.Split(',');
                        dt.Rows.Add(new object[] { sArray[0], sArray[1], sArray[2], sArray[3], sArray[4], sArray[5] });
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetDataTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.Rows.Add(new object[] { 0, 0, 0, 0, 0, "X" });
                return dt;
            }            
        }

        /// <summary>
        /// 結束テーブル保存処理（タイトル行は出力対象外とする）
        /// </summary>
        private void SaveDataGridViewContent()
        {
            string sFileName;
            string[] sArray;
            string sCsvData;

            try
            {
                sFileName = CommonModule.IncludeTrailingPathDelimiter(Environment.CurrentDirectory) + "結束テーブル.txt";

                using (StreamWriter writer = new StreamWriter(sFileName, false, Encoding.GetEncoding("shift_Jis")))
                {
                    int row = DGViewUnityTable.RowCount;
                    int colunms = DGViewUnityTable.ColumnCount;

                    List<string> sList = new List<string>();
                    PubConstClass.sListUnityTable.Clear();

                    for (int i = 0; i < row - 1; i++)
                    {
                        sList = new List<string>();

                        for (int j = 0; j < colunms; j++)
                        {
                            sList.Add(DGViewUnityTable[j, i].Value.ToString());
                        }

                        sArray = sList.ToArray();

                        //sCsvData = "," + sArray;
                        sCsvData = string.Join(",", sArray);
                        writer.WriteLine(sCsvData);
                        PubConstClass.sListUnityTable.Add(sCsvData);
                    }
                }
                MessageBox.Show("結束テーブル内容を保存しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【SaveDataGridViewContent】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 通信ログの保存処理（送信データ用）
        /// </summary>
        /// <param name="strWriteData"></param>
        /// <remarks></remarks>
        private void LoggingSerialSendData(string strWriteData)
        {
            CommonModule.OutPutLogFile("【メンテ画面】送信：" + strWriteData + "<CR>");
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
        /// 「適用」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnApply_Click(Object sender, EventArgs e)
        {
            DialogResult dialogResult;

            try
            {                
                dialogResult = MessageBox.Show("設定を保存しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    // 【システム設定】
                    PubConstClass.pblMachineName = TxtMachineName.Text;                             // 号機名称
                    PubConstClass.pblLineNumber = (CmbLineNo.SelectedIndex + 1).ToString();         // ライン番号
                    PubConstClass.pblHddSpace = TxtHddSpace.Text;                                   // ディスク空き容量
                    PubConstClass.pblSaveLogMonth = (CmbSaveMonth.SelectedIndex + 1).ToString();    // ログの保存期間
                    PubConstClass.pblPassWord = TxtPassword.Text;                                   // パスワード

                    PubConstClass.pblComPort = CmbComPort.SelectedItem.ToString();
                    PubConstClass.pblComSpeed = CmbComSpeed.SelectedIndex.ToString();
                    PubConstClass.pblComDataLength = CmbComDataLength.SelectedIndex.ToString();
                    PubConstClass.pblComIsParity = CmbComIsParty.SelectedIndex.ToString();
                    PubConstClass.pblComParityVar = CmbComParityVar.SelectedIndex.ToString();
                    PubConstClass.pblComStopBit = CmbComStopBit.SelectedIndex.ToString();

                    PubConstClass.pblComPort2 = CmbComPort2.SelectedItem.ToString();
                    PubConstClass.pblComSpeed2 = CmbComSpeed2.SelectedIndex.ToString();
                    PubConstClass.pblComDataLength2 = CmbComDataLength2.SelectedIndex.ToString();
                    PubConstClass.pblComIsParity2 = CmbComIsParty2.SelectedIndex.ToString();
                    PubConstClass.pblComParityVar2 = CmbComParityVar2.SelectedIndex.ToString();
                    PubConstClass.pblComStopBit2 = CmbComStopBit2.SelectedIndex.ToString();

                    PubConstClass.pblReportPrinterName = CmbReportPrinter.SelectedItem.ToString();
                    PubConstClass.pblJournalPrinterName = CmbJournalPrinter.SelectedItem.ToString();

                    // 【フォルダ設定】
                    PubConstClass.pblChoaiFolder = TxtChoaiData.Text;
                    PubConstClass.pblControlFolder = TxtControlData.Text;
                    PubConstClass.pblSeisanFolder = TxtSeisan.Text;
                    PubConstClass.pblFinalSeisanFolder = TxtFinalSeisan.Text;
                    PubConstClass.pblEventFolder = TxtEvent.Text;
                    PubConstClass.pblFinalEventFolder = TxtFinalEvent.Text;
                    PubConstClass.pblFormOutPutFolder = TxtOutPutFolder.Text;
                    PubConstClass.pblOperationPcTotallingFolder = TxtOperationPcTotallingFolder.Text;
                    PubConstClass.pblOperationPcEventFolder = TxtOperationPcEventFolder.Text;
                    PubConstClass.pblInternalTranFolder = TxtInternalTran.Text;

                    // 【ブザー設定】
                    PubConstClass.pblToneName1 = (CmbBuzzer1.SelectedIndex + 1).ToString();
                    PubConstClass.pblToneName2 = (CmbBuzzer2.SelectedIndex + 1).ToString();
                    PubConstClass.pblToneName3 = (CmbBuzzer3.SelectedIndex + 1).ToString();
                    PubConstClass.pblToneName4 = (CmbBuzzer4.SelectedIndex + 1).ToString();
                    PubConstClass.pblToneName5 = (CmbBuzzer5.SelectedIndex + 1).ToString();
                    PubConstClass.pblToneName6 = (CmbBuzzer6.SelectedIndex + 1).ToString();
                    PubConstClass.pblToneName7 = (CmbBuzzer7.SelectedIndex + 1).ToString();
                    PubConstClass.pblToneName8 = (CmbBuzzer8.SelectedIndex + 1).ToString();

                    PubConstClass.pblToneTime1 = (CmbHour1.SelectedIndex).ToString() + "." + (CmbSecond1.SelectedIndex).ToString();
                    PubConstClass.pblToneTime2 = (CmbHour2.SelectedIndex).ToString() + "." + (CmbSecond2.SelectedIndex).ToString();
                    PubConstClass.pblToneTime3 = (CmbHour3.SelectedIndex).ToString() + "." + (CmbSecond3.SelectedIndex).ToString();
                    PubConstClass.pblToneTime4 = (CmbHour4.SelectedIndex).ToString() + "." + (CmbSecond4.SelectedIndex).ToString();
                    PubConstClass.pblToneTime5 = (CmbHour5.SelectedIndex).ToString() + "." + (CmbSecond5.SelectedIndex).ToString();
                    PubConstClass.pblToneTime6 = (CmbHour6.SelectedIndex).ToString() + "." + (CmbSecond6.SelectedIndex).ToString();
                    PubConstClass.pblToneTime7 = (CmbHour7.SelectedIndex).ToString() + "." + (CmbSecond7.SelectedIndex).ToString();
                    PubConstClass.pblToneTime8 = (CmbHour8.SelectedIndex).ToString() + "." + (CmbSecond8.SelectedIndex).ToString();

                    // １箱最大束数
                    PubConstClass.pblMaxOfBundle = (CmbUnityTable.SelectedIndex + 1).ToString();

                    // 自動判定モード
                    if (ChkAutoJudgeMode.Checked == true)
                        PubConstClass.pblIsAutoJudge = "1";
                    else
                        PubConstClass.pblIsAutoJudge = "0";
                    PubConstClass.pblErrorDispTime = TxtErrorDispTime.Text;

                    // エラー表示時間
                    PubConstClass.pblErrorDispTime = TxtErrorDispTime.Text;

                    // 運転画面表示位置（Ｘ座標）
                    PubConstClass.intXPosition = Convert.ToInt32(TxtXPosition.Text);

                    // CONTEC DIOｰ0808LYのデバイスID
                    PubConstClass.sDioName = TxtDioName.Text;

                    // 作成ダミー束数の設定
                    PubConstClass.iDummyCreateCount1 = CmbDummy1.SelectedIndex + 1;
                    PubConstClass.iDummyCreateCount2 = CmbDummy2.SelectedIndex + 1;
                    PubConstClass.iDummyCreateCount3 = CmbDummy3.SelectedIndex + 1;
                    PubConstClass.iDummyCreateCount4 = CmbDummy4.SelectedIndex + 1;
                    // 厚物冊子使用フィーダー
                    PubConstClass.iThickBookletUseFeeder = CmbThickBookletUseFeeder.SelectedIndex;
                    // 厚物冊子（N）冊以上
                    PubConstClass.iNumberOfBooks = CmbNumberOfBooks.SelectedIndex + 1;
                    // ボード紙使用フィーダー
                    PubConstClass.iCardboard = CmbCardboard.SelectedIndex;

                    // システム定義ファイルの書き込み処理
                    CommonModule.PutSystemDefinition();

                    // ディスクの空き領域をチェック
                    CommonModule.CheckAvairableFreeSpace();

                    //SaveDataGridViewContent();
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnApply_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnChoaiData_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "丁合指示データ読込フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblChoaiFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    // 選択されたフォルダを表示する
                    TxtChoaiData.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnChoaiData_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnControlData_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "制御データ格納フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblControlFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtControlData.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnControlData_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnSeisan_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "生産ログ格納フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblSeisanFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtSeisan.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnSeisan_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnFinalSeisan_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "最終生産ログ格納フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblFinalSeisanFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtFinalSeisan.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnFinalSeisan_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnEvent_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "イベントログ格納フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblEventFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtEvent.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnEvent_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnFinalEvent_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "最終イベントログ格納フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblFinalEventFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtFinalEvent.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnFinalEvent_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnInternalTran_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "内部実績ログ格納フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblInternalTranFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtInternalTran.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnInternalTran_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        private void BuzzerSetName()
        {
            string strReadDataPath;
            string strBuzzerSetName;
            int intIndex;

            try
            {
                strReadDataPath = CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_BUZZER_SET_NAME;
                intIndex = 1;
                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strBuzzerSetName = sr.ReadLine();
                        switch (intIndex)
                        {
                            case 1:
                                {
                                    GrpBuzzer1.Text = strBuzzerSetName;
                                    break;
                                }

                            case 2:
                                {
                                    GrpBuzzer2.Text = strBuzzerSetName;
                                    break;
                                }

                            case 3:
                                {
                                    GrpBuzzer3.Text = strBuzzerSetName;
                                    break;
                                }

                            case 4:
                                {
                                    GrpBuzzer4.Text = strBuzzerSetName;
                                    break;
                                }

                            case 5:
                                {
                                    GrpBuzzer5.Text = strBuzzerSetName;
                                    break;
                                }

                            case 6:
                                {
                                    GrpBuzzer6.Text = strBuzzerSetName;
                                    break;
                                }

                            case 7:
                                {
                                    GrpBuzzer7.Text = strBuzzerSetName;
                                    break;
                                }

                            case 8:
                                {
                                    GrpBuzzer8.Text = strBuzzerSetName;
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }
                        intIndex += 1;
                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BuzzerSetName】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        private void BuzzerToneName()
        {
            string strReadDataPath;
            string strBuzzerToneName;

            try
            {
                CmbBuzzer1.Items.Clear();
                CmbBuzzer2.Items.Clear();
                CmbBuzzer3.Items.Clear();
                CmbBuzzer4.Items.Clear();
                CmbBuzzer5.Items.Clear();
                CmbBuzzer6.Items.Clear();
                CmbBuzzer7.Items.Clear();
                CmbBuzzer8.Items.Clear();

                strReadDataPath = CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_BUZZER_TONE_NAME;

                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strBuzzerToneName = sr.ReadLine();
                        CmbBuzzer1.Items.Add(strBuzzerToneName);
                        CmbBuzzer2.Items.Add(strBuzzerToneName);
                        CmbBuzzer3.Items.Add(strBuzzerToneName);
                        CmbBuzzer4.Items.Add(strBuzzerToneName);
                        CmbBuzzer5.Items.Add(strBuzzerToneName);
                        CmbBuzzer6.Items.Add(strBuzzerToneName);
                        CmbBuzzer7.Items.Add(strBuzzerToneName);
                        CmbBuzzer8.Items.Add(strBuzzerToneName);
                    }
                }
                CmbBuzzer1.SelectedIndex = 0;
                CmbBuzzer2.SelectedIndex = 0;
                CmbBuzzer3.SelectedIndex = 0;
                CmbBuzzer4.SelectedIndex = 0;
                CmbBuzzer5.SelectedIndex = 0;
                CmbBuzzer6.SelectedIndex = 0;
                CmbBuzzer7.SelectedIndex = 0;
                CmbBuzzer8.SelectedIndex = 0;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BuzzerToneName】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// インターフェースコンバータ（PHC-D08）へのコマンド送信
        /// </summary>
        /// <param name="CmbBuzzer"></param>
        /// <param name="CmbHour"></param>
        /// <param name="CmbSecond"></param>
        private void SendPHCD08Command(ComboBox CmbBuzzer, ComboBox CmbHour, ComboBox CmbSecond)
        {
            string strSendData;
            string sToneNum1;
            string sToneNum2;
            int iToneNum;

            try
            {
                strSendData = "1";   // 指定の出力端子をONにする

                switch (CmbBuzzer.SelectedIndex)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                        {
                            iToneNum = CmbBuzzer.SelectedIndex + 1;
                            sToneNum2 = Strings.Chr(48 + iToneNum).ToString();
                            sToneNum1 = "0";
                            break;
                        }

                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                        {
                            iToneNum = CmbBuzzer.SelectedIndex - 15;
                            sToneNum2 = Strings.Chr(48 + iToneNum).ToString();
                            sToneNum1 = "1";
                            break;
                        }

                    case 31:
                        {
                            sToneNum2 = "0";
                            sToneNum1 = "2";
                            break;
                        }

                    default:
                        {
                            sToneNum2 = "0";
                            sToneNum1 = "0";
                            break;
                        }
                }

                strSendData += sToneNum1 + sToneNum2;
                ((MainForm)Owner).SendBuzzerData(strSendData);

                TimBuzzer.Interval = CmbHour.SelectedIndex * 1000 + CmbSecond.SelectedIndex * 100;
                TimBuzzer.Enabled = true;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【sendPHCD08Command】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「テスト」ボタン処理（コース区分けコマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest1_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer1, CmbHour1, CmbSecond1);
        }

        /// <summary>
        /// 「テスト」ボタン処理（センター区分けコマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest2_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer2, CmbHour2, CmbSecond2);
        }

        /// <summary>
        /// 「テスト」ボタン処理（単協区分けコマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest3_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer3, CmbHour3, CmbSecond3);
        }

        /// <summary>
        /// 「テスト」ボタン処理（排出ゲート開放コマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest4_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer4, CmbHour4, CmbSecond4);
        }

        /// <summary>
        /// 「テスト」ボタン処理（ＯＫ束コマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest5_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer5, CmbHour5, CmbSecond5);
        }

        /// <summary>
        /// 「テスト」ボタン処理（ＮＧ束コマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest6_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer6, CmbHour6, CmbSecond6);
        }

        /// <summary>
        /// 「テスト」ボタン処理（予備①コマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest7_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer7, CmbHour7, CmbSecond7);
        }

        /// <summary>
        /// 「テスト」ボタン処理（予備②コマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnTest8_Click(Object sender, EventArgs e)
        {
            SendPHCD08Command(CmbBuzzer8, CmbHour8, CmbSecond8);
        }

        /// <summary>
        /// 「登録」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnEntry_Click(Object sender, EventArgs e)
        {
            MsgBoxResult RetValMsgResult;

            try
            {
                RetValMsgResult = Interaction.MsgBox("ブザー情報を登録しますか？", (MsgBoxStyle)MsgBoxStyle.Question | MsgBoxStyle.OkCancel, "確認");
                if (RetValMsgResult == MsgBoxResult.Cancel)
                    return;
            }

            // ブザー設定情報の送信
            // mainForm.SendBuzzerSetInfomation()

            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnEntry_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 「ログデータ手動削除」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnDeleteLogData_Click(Object sender, EventArgs e)
        {
            try
            {
                // 現在の日付（年月日）を求める
                DateTime dtCurrent = DateTime.Now;

                int intMinusMonth = CmbSaveMonth.SelectedIndex;
                // 現在日付から１ヶ月を減算
                DateTime dtPassDate = dtCurrent.AddMonths(-(intMinusMonth + 1));

                MsgBoxResult retVal = Interaction.MsgBox("現在の日付から" + (intMinusMonth + 1).ToString() + "ヶ月前は、" + dtPassDate, (MsgBoxStyle)MsgBoxStyle.Information | MsgBoxStyle.OkCancel, "【確認】");

                if (retVal == MsgBoxResult.Cancel)
                    return;

                CommonModule.DeleteLogFiles(intMinusMonth + 1);
                
                MessageBox.Show("削除処理が完了しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【メンテンス画面】【BtnDeleteLogData_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PHC-D08 にコマンド送信（出力オールクリア）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimBuzzer_Tick(object sender, EventArgs e)
        {
            try
            {
                TimBuzzer.Enabled = false;
                ((MainForm)Owner).SendBuzzerStopData();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【TimBuzzer_Tick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「結束テーブル内容保存」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveUnityTable_Click(object sender, EventArgs e)
        {
            SaveDataGridViewContent();

            LoadBindingTableFile();

            //// 結束テーブル内容の取得
            //DGViewUnityTable.DataSource = GetDataTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOperationPcTotalling_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "稼働管理PC集計フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblOperationPcTotallingFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtOperationPcTotallingFolder.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnFinalEvent_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOutPutFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "帳票出力フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblOperationPcTotallingFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtOutPutFolder.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnOutPutFolder_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOperationPcEvent_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try
            {
                fbd.Description = "稼働管理PC集計フォルダを選択してください。";

                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = PubConstClass.pblOperationPcEventFolder;

                // 新規フォルダ作成を表示
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                    // 選択されたフォルダを表示する
                    TxtOperationPcEventFolder.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnOperationPcEvent_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// ディスク容量チェックは数字のみ入力とする（最大4桁）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtHddSpace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && '\b' != e.KeyChar)
            {
                //押されたキーが 0～9でない場合はイベントキャンセル
                e.Handled = true;
            }
        }

        /// <summary>
        /// パスワード設定は数字のみ入力とする（最大10桁）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && '\b' != e.KeyChar)
            {
                //押されたキーが 0～9でない場合はイベントキャンセル
                e.Handled = true;
            }
        }
    }
}
