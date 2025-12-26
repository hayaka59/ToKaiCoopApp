using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class MainForm : Form
    {
        // --------------------------------------------------------------------------
        private delegate void Delegate_RcvDataToTextBox(string data);

        public MainForm()
        {
            InitializeComponent();
        }

        public ReadCollatingDataForm readCollatingDataForm = new ReadCollatingDataForm();
        public DrivingForm drivingForm = new DrivingForm();

        /// <summary>
        /// メインメニュー画面初期ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(Object sender, EventArgs e)
        {
            string strLocalWorkFolder;

            try
            {
                LblTitle.Text = PubConstClass.DEF_TITLE;
                LblVersion.Text = PubConstClass.DEF_VERSION;

                PubConstClass.objSyncHist = new object();
                PubConstClass.objSyncSeri = new object();

                CommonModule.OutPutLogFile("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
                CommonModule.OutPutLogFile("【" + LblTitle.Text + "】を起動しました。");
                CommonModule.OutPutLogFile("■管理ＰＣバージョン「" + PubConstClass.DEF_VERSION + "」");

                // 二重起動のチェック
                if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    // すでに起動していると判断する
                    CommonModule.OutPutLogFile("二重起動はできません。");
                    MessageBox.Show("二重起動はできません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Dispose();
                }

                // マウスカーソルが拡張ディスプレイに有るとメインメニュー画面が
                // 拡張ディスプレイに表示されてしまう不具合を防止する為。
                this.Location = new Point(0, 0);
                
                // 年月日時分秒の表示
                LblDateTime.Text = "";
                TimDateTime.Interval = 1000;
                TimDateTime.Enabled = true;

                PubConstClass.pblIsDriving = false;

                PubConstClass.dicCoopCodeData = new Dictionary<string, string>();
                PubConstClass.dicDepoCodeData = new Dictionary<string, string>();

                // システム定義ファイル読込
                CommonModule.GetSystemDefinition();

                // 内部実績ログ格納フォルダ及び履歴ログファイルの削除処理
                CommonModule.DeleteLogFiles(Convert.ToInt32(PubConstClass.pblSaveLogMonth));

                // ログインファイルの読込
                GetLoginDataFile();
                
                // 単協・デポ一覧の読み込み

                GetCoopAndDepoList();

                // アラームリストファイル読込
                GetAlarmList();

                // 名称変換テーブル読込
                GetNameConvertList();

                // 音色データ設定
                SetToneData();

                // アプリ起動時日付の取得
                PubConstClass.pblCurrentDate = DateTime.Now.ToString("yyyyMMdd");

                #region 内部実績ログ格納フォルダの存在チェックと作成
                strLocalWorkFolder = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + PubConstClass.pblCurrentDate);
                if (Directory.Exists(strLocalWorkFolder) == false)
                {
                    // 内部実績ログ格納フォルダの作成
                    Directory.CreateDirectory(strLocalWorkFolder);
                    CommonModule.OutPutLogFile("【" + strLocalWorkFolder + "】フォルダを作成しました。");
                }
                #endregion

                #region 稼働計PC集計フォルダの存在チェックと作成
                strLocalWorkFolder = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblOperationPcTotallingFolder);
                if (Directory.Exists(strLocalWorkFolder) == false)
                {
                    //Directory.CreateDirectory(strLocalWorkFolder);
                    LblStatus.Text = "下記の稼働管理PC集計フォルダが見つかりません" + Environment.NewLine + "（" + strLocalWorkFolder + "）"; ;
                    LblStatus.Visible = true;
                    CommonModule.OutPutLogFile("稼働管理PC集計フォルダ【" + strLocalWorkFolder + "】が見つかりません。");
                }
                else
                {
                    strLocalWorkFolder += @"\" + PubConstClass.pblCurrentDate;
                    if (Directory.Exists(strLocalWorkFolder) == false)
                    {
                        // 稼働管理PC集計フォルダ配下に「YYYYMMDD」フォルダの作成
                        Directory.CreateDirectory(strLocalWorkFolder);
                        CommonModule.OutPutLogFile("【" + strLocalWorkFolder + "】フォルダを作成しました。");
                    }
                }

                strLocalWorkFolder = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblOperationPcEventFolder);
                if (Directory.Exists(strLocalWorkFolder) == false)
                {
                    //Directory.CreateDirectory(strLocalWorkFolder);
                    LblStatus.Text = "下記の稼働管理PC集計フォルダが見つかりません" + Environment.NewLine + "（" + strLocalWorkFolder + "）"; ;
                    LblStatus.Visible = true;
                    CommonModule.OutPutLogFile("稼働管理PC集計フォルダ【" + strLocalWorkFolder + "】が見つかりません。");
                }
                else
                {
                    strLocalWorkFolder += @"\" + PubConstClass.pblCurrentDate;
                    if (Directory.Exists(strLocalWorkFolder) == false)
                    {
                        // 稼働管理PC集計フォルダ配下に「YYYYMMDD」フォルダの作成
                        Directory.CreateDirectory(strLocalWorkFolder);
                        CommonModule.OutPutLogFile("【" + strLocalWorkFolder + "】フォルダを作成しました。");
                    }
                }
                #endregion

                // 帳票出力フォルダの存在チェックと作成
                CommonModule.CreateOutPutFolder();

                #region シリアルポートの設定とオープン
                // シリアルポート名の設定
                SerialPort.PortName = PubConstClass.pblComPort;
                // シリアルポートの通信速度指定
                switch (PubConstClass.pblComSpeed)
                {
                    case "0":
                            SerialPort.BaudRate = 4800;
                            break;
                    case "1":
                            SerialPort.BaudRate = 9600;
                            break;
                    case "2":
                            SerialPort.BaudRate = 19200;
                            break;
                    case "3":
                            SerialPort.BaudRate = 38400;
                            break;
                    case "4":
                            SerialPort.BaudRate = 57600;
                            break;
                    case "5":
                            SerialPort.BaudRate = 115200;
                            break;
                    default:
                            SerialPort.BaudRate = 38400;
                            break;
                }
                // シリアルポートのパリティ指定
                switch (PubConstClass.pblComParityVar)
                {
                    case "0":
                            SerialPort.Parity = System.IO.Ports.Parity.Odd;
                            break;
                    case "1":
                            SerialPort.Parity = System.IO.Ports.Parity.Even;
                            break;
                    default:
                            SerialPort.Parity = System.IO.Ports.Parity.Even;
                            break;
                }

                // シリアルポートのパリティ有無
                if (PubConstClass.pblComIsParity == "0")
                    SerialPort.Parity = System.IO.Ports.Parity.None;

                // シリアルポートのビット数指定
                switch (PubConstClass.pblComDataLength)
                {
                    case "0":
                            SerialPort.DataBits = 8;
                            break;
                    case "1":
                            SerialPort.DataBits = 7;
                            break;
                    default:
                            SerialPort.DataBits = 8;
                            break;
                }

                // シリアルポートのストップビット指定
                switch (PubConstClass.pblComStopBit)
                {
                    case "0":
                            SerialPort.StopBits = System.IO.Ports.StopBits.One;
                            break;
                    case "1":
                            SerialPort.StopBits = System.IO.Ports.StopBits.Two;
                            break;
                    default:
                            SerialPort.StopBits = System.IO.Ports.StopBits.One;
                            break;
                }

                // シリアルポートのオープン
                SerialPort.Open();
                #endregion

                // ブザー設定情報の送信
                SendBuzzerSetInfomation();

                // シリアルポートにデータ送信（動作可コマンド）
                byte[] dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(PubConstClass.CMD_SEND_a + Constants.vbCr);
                SerialPort.Write(dat, 0, dat.GetLength(0));
                LoggingSerialSendData(PubConstClass.CMD_SEND_a);

                TimPcReadyOn.Interval = 2000;
                TimPcReadyOn.Enabled = true;

                #region 印字データファイルの削除
                //string strLoadingDataPath;
                //strLoadingDataPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) +
                //                     PubConstClass.pblCurrentDate + @"\" + PubConstClass.DEF_LOADING_FILENAME_A;
                //if (File.Exists(strLoadingDataPath))
                //{
                //    // 積付表Ａデータ削除
                //    File.Delete(strLoadingDataPath);
                //}
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【mainForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetToneData()
        {
            string[] sArray;

            try
            {
                sArray = new string[] {
                    "101",
                    "102",
                    "103",
                    "104",
                    "105",
                    "106",
                    "107",
                    "108",
                    "109",
                    "10:",
                    "10;",
                    "10<",
                    "10=",
                    "10>",
                    "10?",
                    "110",
                    "111",
                    "112",
                    "113",
                    "114",
                    "115",
                    "116",
                    "117",
                    "118",
                    "119",
                    "11:",
                    "11;",
                    "11<",
                    "11=",
                    "11>",
                    "11?",
                    "120"
                };

                for (var N = 0; N <= 31; N++)
                {
                    PubConstClass.pblToneData[N] = sArray[N];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【SetToneData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 通信ログの保存処理（送信データ用）
        /// </summary>
        /// <param name="strWriteData"></param>
        private void LoggingSerialSendData(string strWriteData)
        {
            CommonModule.OutPutLogFile("■送信：" + strWriteData + "<CR>");
        }

        /// <summary>
        /// 指定したブザー設定情報の送信
        /// </summary>
        /// <param name="sCommand"></param>
        /// <param name="sSendToneTime"></param>
        /// <param name="sSendToneName"></param>
        private void SendBuzzerSetInfomationData(string sCommand, string sSendToneTime, string sSendToneName)
        {
            string strSendData;
            string[] strArray;
            byte[] dat;

            try
            {
                strArray = sSendToneTime.Split('.');
                strSendData = sCommand + ",";
                strSendData += Convert.ToInt32(sSendToneName).ToString("00") + ",";
                strSendData += Convert.ToInt32(strArray[0]).ToString("0") + Convert.ToInt32(strArray[1]).ToString("0");
                dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(strSendData + Constants.vbCr);
                SerialPort.Write(dat, 0, dat.GetLength(0));
                LoggingSerialSendData(strSendData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【SendBuzzerSetInfomationData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 全てのブザー設定情報の送信
        /// </summary>
        public void SendBuzzerSetInfomation()
        {
            byte[] dat;

            try
            {
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_c, PubConstClass.pblToneTime1, PubConstClass.pblToneName1);
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_d, PubConstClass.pblToneTime2, PubConstClass.pblToneName2);
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_e, PubConstClass.pblToneTime3, PubConstClass.pblToneName3);
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_f, PubConstClass.pblToneTime4, PubConstClass.pblToneName4);
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_g, PubConstClass.pblToneTime5, PubConstClass.pblToneName5);
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_h, PubConstClass.pblToneTime6, PubConstClass.pblToneName6);
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_i, PubConstClass.pblToneTime7, PubConstClass.pblToneName7);
                SendBuzzerSetInfomationData(PubConstClass.CMD_SEND_j, PubConstClass.pblToneTime8, PubConstClass.pblToneName8);

                dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(PubConstClass.CMD_SEND_b + Constants.vbCr);
                SerialPort.Write(dat, 0, dat.GetLength(0));
                LoggingSerialSendData(PubConstClass.CMD_SEND_b);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【SendBuzzerSetInfomation】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ログインファイル（LOGIN.TXT）の読込処理
        /// </summary>
        private void GetLoginDataFile()
        {
            string sReadDataPath;
            string sReadData;
            string[] sArray;

            try
            {
                PubConstClass.lstLoginData.Clear();

                // ログインデータファイルの読込
                sReadDataPath = CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + "LOGIN.TXT";
                using (StreamReader sr = new StreamReader(sReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        sReadData = sr.ReadLine();
                        sArray = sReadData.Split(PubConstClass.DEF_MASTER_DELIMITER);
                        if (sArray.Length >= 5)
                        {
                            PubConstClass.lstLoginData.Add(sReadData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetLoginDataFile】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// アラームリストファイルの読込み
        /// </summary>
        private void GetAlarmList()
        {
            string strReadDataPath;
            string[] strArray;

            try
            {
                PubConstClass.intAlarmListCnt = 0;
                // アラームリストファイルの読込
                strReadDataPath = CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_ALARM_LIST;
                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        if (strArray.Length > 2)
                        {
                            // アラームCD
                            PubConstClass.pblAlarmList[PubConstClass.intAlarmListCnt, 0] = strArray[0];
                            // アラーム名称
                            PubConstClass.pblAlarmList[PubConstClass.intAlarmListCnt, 1] = strArray[2];
                            // OutPutLogFile("■" & PubConstClass.pblAlarmList(PubConstClass.intAlarmListCnt, 0) & _
                            // "：" & PubConstClass.pblAlarmList(PubConstClass.intAlarmListCnt, 1))
                            PubConstClass.intAlarmListCnt += 1;
                        }
                    }
                    // 最後の配列データをクリアする
                    PubConstClass.pblAlarmList[PubConstClass.intAlarmListCnt, 0] = "";
                    PubConstClass.pblAlarmList[PubConstClass.intAlarmListCnt, 1] = ",";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetAlarmList】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetNameConvertList()
        {
            string strReadDataPath;
            string[] strArray;

            try
            {
                PubConstClass.intNameConversionListCnt = 0;
                // アラームリストファイルの読込
                strReadDataPath = CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_NAME_CONVERSION_LIST;
                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        if (strArray.Length > 1)
                        {
                            // アラームCD
                            PubConstClass.pblNameConversionList[PubConstClass.intNameConversionListCnt, 0] = strArray[0];
                            // アラーム名称
                            PubConstClass.pblNameConversionList[PubConstClass.intNameConversionListCnt, 1] = strArray[1];
                            PubConstClass.intNameConversionListCnt += 1;
                        }
                    }
                    // 最後の配列データをクリアする
                    PubConstClass.pblNameConversionList[PubConstClass.intNameConversionListCnt, 0] = "";
                    PubConstClass.pblNameConversionList[PubConstClass.intNameConversionListCnt, 1] = ",";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetAlarmList】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 生協・デポ一覧ファイルの読込
        /// </summary>
        private void GetCoopAndDepoList()
        {
            string strReadDataPath;
            string sData;
            string[] strArray;

            try
            {                
                PubConstClass.lstCoopDepoData.Clear();
                // 生協・デポ一覧ファイルの読込
                strReadDataPath = CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_COOP_AND_DEPO_LIST;
                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    PubConstClass.dicCoopCodeData.Clear();
                    PubConstClass.dicDepoCodeData.Clear();
                    while (!sr.EndOfStream)
                    {
                        sData = sr.ReadLine();
                        strArray = sData.Split(',');
                        if (strArray.Length > 4)
                        {
                            PubConstClass.lstCoopDepoData.Add(sData);
                            //CommonModule.OutPutLogFile("【単協・デポ一覧】" + sData);
                            // 生協コード辞書の登録
                            if (!PubConstClass.dicCoopCodeData.ContainsKey(strArray[0]))
                            {
                                // 存在しない場合は登録する
                                PubConstClass.dicCoopCodeData.Add(strArray[0], strArray[1]);
                                //CommonModule.OutPutLogFile("【生協コード辞書追加】" + strArray[0] + "=" + strArray[1]);
                            }
                            
                            // デポコード辞書の登録
                            if (!PubConstClass.dicDepoCodeData.ContainsKey(strArray[2]))
                            {
                                // 存在しない場合は登録する
                                PubConstClass.dicDepoCodeData.Add(strArray[2], strArray[3]);
                                //CommonModule.OutPutLogFile("【デポコード辞書追加】" + strArray[2] + "=" + strArray[3]);
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetCoopAndDepoList】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「終了」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnd_Click(Object sender, EventArgs e)
        {
            try
            {
                CommonModule.OutPutLogFile("■「終了」ボタンクリック");

                EndForm form = new EndForm();
                form.ShowDialog(this);

                if (PubConstClass.blnReturnFlag == true)
                {

                    // 制御データ削除処理
                    foreach (string strDeleteFile in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblControlFolder), "*.sel", SearchOption.TopDirectoryOnly))
                    {
                        // 制御ファイルの削除
                        File.Delete(strDeleteFile);
                        CommonModule.OutPutLogFile("■制御データ削除：" + strDeleteFile);
                    }

                    // 生産ログ削除処理
                    foreach (string strDeleteFile in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder), "*.plg", SearchOption.TopDirectoryOnly))
                    {
                        // 生産ログの削除
                        File.Delete(strDeleteFile);
                        CommonModule.OutPutLogFile("■生産ログ削除：" + strDeleteFile);
                    }

                    // 最終生産ログ削除処理
                    foreach (string strDeleteFile in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblFinalSeisanFolder), "*.plog", SearchOption.TopDirectoryOnly))
                    {
                        // 最終生産ログの削除
                        File.Delete(strDeleteFile);
                        CommonModule.OutPutLogFile("■最終生産ログ削除：" + strDeleteFile);
                    }

                    // イベントログ削除処理
                    foreach (string strDeleteFile in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblEventFolder), "*.elg", SearchOption.TopDirectoryOnly))
                    {
                        // イベントログの削除
                        File.Delete(strDeleteFile);
                        CommonModule.OutPutLogFile("■イベントログ削除：" + strDeleteFile);
                    }

                    // 最終イベントログ削除処理
                    foreach (string strDeleteFile in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblFinalEventFolder), "*.elog", SearchOption.TopDirectoryOnly))
                    {
                        // 最終イベントログの削除
                        File.Delete(strDeleteFile);
                        CommonModule.OutPutLogFile("■最終イベントログ削除：" + strDeleteFile);
                    }


                    if (PubConstClass.blnShutDownFlag == true)
                    {
                        CommonModule.OutPutLogFile("■シャットダウン処理実行");
                        System.Diagnostics.Process p = System.Diagnostics.Process.Start("shutdown.exe", "/s /t 5");
                        this.Dispose();
                    }
                    else
                    {
                        CommonModule.OutPutLogFile("■【" + this.Text + "】プログラム終了");
                        this.Dispose();
                    }
                }
                else
                    CommonModule.OutPutLogFile("■終了処理キャンセル");
            }

            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message, "【BtnEnd_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「丁合指示データ読込み」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadCollatingData_Click(Object sender, EventArgs e)
        {
            CommonModule.OutPutLogFile("■「丁合指示データ読込み」ボタンクリック");
            readCollatingDataForm.Show(this);
            this.Hide();
        }

        /// <summary>
        /// 単協・デポ一覧の内容表示（隠しコマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            //if (!CommonModule.ReadLinePlanFile(false))
            //{
            //    // ライン計画ファイルが正しく読めなかった
            //    return;
            //}
            MasterOfOrganizationForm form = new MasterOfOrganizationForm();
            form.Show();
            form.Activate();
        }

        /// <summary>
        /// 「日報　作成」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport_Click(Object sender, EventArgs e)
        {
            CommonModule.OutPutLogFile("■「日報　作成」ボタンクリック");
            InputDailyReportForm form = new InputDailyReportForm();
            form.Show(this);
        }

        /// <summary>
        /// 「保守」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMaintenance_Click(Object sender, EventArgs e)
        {
            try
            {
                CommonModule.OutPutLogFile("■「保　守」ボタンクリック");

                // パスワード入力画面表示
                PasswordForm formP = new PasswordForm();
                formP.ShowDialog(this);
                if (PubConstClass.blnIsOkPasswod == true)
                {
                    MaintenanceForm formM = new MaintenanceForm();
                    formM.ShowDialog(this);
                    //this.Hide();
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnMaintenance_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「生産実績閲覧」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnActualProduction_Click(Object sender, EventArgs e)
        {
            try
            {
                CommonModule.OutPutLogFile("■「生産実績閲覧」ボタンクリック");

                // If PubConstClass.intCollatingCount = 0 Then
                // OutPutLogFile("丁合指示データを取り込んで下さい。")
                // MsgBox("丁合指示データを取り込んで下さい。", CType(MsgBoxStyle.Information + MsgBoxStyle.OkOnly, MsgBoxStyle), "情報")
                // Exit Sub
                // End If
                ActualProductionForm form = new ActualProductionForm();
                form.Show(this);
                this.Hide();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【BtnActualProduction_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「手直し品 登録」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdjust_Click(Object sender, EventArgs e)
        {
            CommonModule.OutPutLogFile("■「手直し品 登録」ボタン（メイン画面）クリック");

            EntryAdjustForm form = new EntryAdjustForm();
            form.Show(this);
        }

        /// <summary>
        /// PHC-D08からのデータ受信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string data;
            object[] args = new object[1];
            string strRecData;

            data = "";

            try
            {
                // シリアルポートをオープンしていない場合、処理を行わない。
                if (SerialPort.IsOpen == false)
                    return;

                strRecData = SerialPort.ReadExisting();
                CommonModule.OutPutLogFile("■受信：" + strRecData.Replace(Strings.Chr(6).ToString(), "<ACK>").Replace(Strings.Chr(15).ToString(), "<NAK>"));
                BeginInvoke(new Delegate_RcvDataToTextBox(RcvDataToTextBox), strRecData);
            }
            catch (TimeoutException)
            {
                // ディスカードするデータ
                CommonModule.OutPutLogFile("■データ受信タイムアウトエラー：<CR>未受信で切り捨てたデータ：" + data);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【SerialPort_DataReceived】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 受信データによる各コマンド処理
        /// </summary>
        /// <param name="data"></param>
        private void RcvDataToTextBox(string data)
        {
            string strMessage;
            int intOrderArrayIndex;

            try
            {
                // 受信データをテキストボックスの最後に追記する。
                if (Information.IsNothing(data) == false)
                {
                    if (data.Length >= 2)
                    {
                        switch (data.Substring(0, 1))
                        {
                            case PubConstClass.CMD_RECIEVE_A:    // コントローラーから結束束区分け
                                {
                                    CommonModule.OutPutLogFile("■コントローラから結束束区分け（" + PubConstClass.CMD_RECIEVE_A + "<CR>）受信");
                                    if (drivingForm.LstDrivingData.Items.Count > 0)
                                    {
                                        // 自動判定モードのチェック
                                        if (PubConstClass.pblIsAutoJudge == "1")
                                        {
                                            // 自動判定モードがＯＮの場合の処理
                                            intOrderArrayIndex = Convert.ToInt32(drivingForm.LstDrivingData.Items[drivingForm.LstDrivingData.Items.Count - 1].SubItems[0].Text) - 1;
                                            readCollatingDataForm.GetPrintDataOfRollBox(PubConstClass.pblOrderArray[intOrderArrayIndex]);
                                        }
                                    }
                                    else
                                        CommonModule.OutPutLogFile("■運転画面にデータが表示されていない状態で「Ａ（結束区分け）」コマンドを受信した。");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_B:    // コントローラーからコース区分け
                                {
                                    CommonModule.OutPutLogFile("■コントローラからコース区分け（" + PubConstClass.CMD_RECIEVE_B + "<CR>）受信");
                                    if (drivingForm.LstDrivingData.Items.Count > 0)
                                    {
                                        // 自動判定モードのチェック
                                        if (PubConstClass.pblIsAutoJudge == "1")
                                        {
                                            // 自動判定モードがＯＮの場合の処理
                                            intOrderArrayIndex = Convert.ToInt32(drivingForm.LstDrivingData.Items[drivingForm.LstDrivingData.Items.Count - 1].SubItems[0].Text) - 1;
                                            readCollatingDataForm.GetPrintDataOfRollBox(PubConstClass.pblOrderArray[intOrderArrayIndex]);
                                        }
                                    }
                                    else
                                        CommonModule.OutPutLogFile("■運転画面にデータが表示されていない状態で「Ｂ（コース区分け）」コマンドを受信した。");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_C:    // コントローラーからセンター区分け
                                {
                                    CommonModule.OutPutLogFile("■コントローラからセンター区分け（" + PubConstClass.CMD_RECIEVE_C + "<CR>）受信");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_D:    // コントローラーから単協区分け
                                {
                                    CommonModule.OutPutLogFile("■コントローラから単協区分け（" + PubConstClass.CMD_RECIEVE_D + "<CR>）受信");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_E:    // コントローラーから排出ゲート開放
                                {
                                    CommonModule.OutPutLogFile("■コントローラから排出ゲート開放（" + PubConstClass.CMD_RECIEVE_E + "<CR>）受信");
                                    break;
                                }

                            default:
                                {
                                    // 受信データの格納
                                    CommonModule.OutPutLogFile("■コントローラから未定義データ（" + data.Replace(ControlChars.Cr.ToString(), "<CR>") + "）受信");
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strMessage = "【RcvDataToTextBox】" + ex.StackTrace;
                CommonModule.OutPutLogFile(strMessage);               
                MessageBox.Show(strMessage, "【RcvDataToTextBox】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「ＰＣレディコマンド送信処理」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimPcReadyOn_Tick(object sender, EventArgs e)
        {
            try
            {
                // シリアルポートにデータ送信（動作可コマンド）
                byte[] dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(PubConstClass.CMD_SEND_a + Constants.vbCr);
                SerialPort.Write(dat, 0, dat.GetLength(0));
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【TimPcReadyOn_Tick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblVersion_DoubleClick(object sender, EventArgs e)
        {
            if (BtnOK.Visible == false)
            {
                BtnOK.Visible = true;
                BtnNG.Visible = true;
                BtnVoice1Start.Visible = true;
                BtnVoice2Start.Visible = true;
                BtnDifferent.Visible = true;
                BtnEnvironmentalSound.Visible = true;
                BtnDepot.Visible = true;
                BtnRootNo.Visible = true;
            }
            else
            {
                BtnOK.Visible = false;
                BtnNG.Visible = false;
                BtnVoice1Start.Visible = false;
                BtnVoice2Start.Visible = false;
                BtnDifferent.Visible = false;
                BtnEnvironmentalSound.Visible = false;
                BtnDepot.Visible = false;
                BtnRootNo.Visible = false;
            }
        }

        /// <summary>
        /// PHC-D08 にコマンド送信
        /// </summary>
        /// <param name="sData"></param>
        public void SendBuzzerData(string sData)
        {

            // 「@??」＋sData＋「!」
            string strSendData = PubConstClass.CMD_SEND_HEADER + sData + "!";

            // シリアルポートをオープンしていない場合、処理を行わない。
            if (SerialPort.IsOpen == false)
                return;

            try
            {
                // PHC-D08 にコマンド送信
                byte[] dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(strSendData);
                SerialPort.Write(dat, 0, dat.GetLength(0));
                LoggingSerialSendData(strSendData);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【SendBuzzerData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PHC-D08 にコマンド送信（出力オールクリア）
        /// </summary>
        public void SendBuzzerStopData()
        {
            string strSendData = "@??03?!";

            // シリアルポートをオープンしていない場合、処理を行わない。
            if (SerialPort.IsOpen == false)
                return;

            try
            {
                // PHC-D08 にコマンド送信
                byte[] dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(strSendData);
                SerialPort.Write(dat, 0, dat.GetLength(0));
                LoggingSerialSendData(strSendData);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【SendBuzzerStopData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「ＯＫ音」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\ok.wav");
        }

        /// <summary>
        /// 「ＮＧ音」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNG_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\ng.wav");
        }

        /// <summary>
        /// 「センターが変わりました」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVoice1Start_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\center.wav");
        }

        /// <summary>
        /// 「〜と〜が異なります」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDifferent_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\different.wav");
        }

        /// <summary>
        /// 「環境音」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnvironmentalSound_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\EnvironmentalSound.wav");
        }

        /// <summary>
        /// 年月日時分秒の表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimDateTime_Tick(object sender, EventArgs e)
        {
            LblDateTime.Text = DateTime.Now.ToString("yyyy年MM月dd日(ddd) HH:mm:ss");
        }

        /// <summary>
        /// 「生協が変わりました」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCoop_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\coop.wav");
        }

        /// <summary>
        /// 「デポが変わりました」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDepot_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\depot.wav");
        }

        /// <summary>
        /// 「コースが変わりました」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVoice2Start_Click(object sender, EventArgs e)
        {
            CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\course.wav");
        }

        /// <summary>
        /// 「生産ログチェック」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProductionLogCheck_Click(object sender, EventArgs e)
        {
            try
            {
                CommonModule.OutPutLogFile("■「生産ログチェック」ボタンクリック");

                ProductionLogCheckForm form = new ProductionLogCheckForm();
                form.Show(this);
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnProductionLogCheck_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
