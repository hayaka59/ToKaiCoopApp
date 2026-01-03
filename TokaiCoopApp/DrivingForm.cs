using CdioCs;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    public partial class DrivingForm : Form
    {
        public DrivingForm()
        {
            InitializeComponent();
        }

        private int intSesanCounter;
        private int intErrorCnt;
        
        // --------------------------------------------------------------------------
        private delegate void Delegate_RcvDataToTextBox(string data);

        /// <summary>
        /// 運転画面初期ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void DrivingForm_Load(Object sender, EventArgs e)
        {
            try
            {
                //LblTitle.Text = "運転画面" + PubConstClass.pblRunCoopName;
                LblTitle.Text = "運転画面";
                LblVersion.Text = PubConstClass.DEF_VERSION;
                LblDateTime.Text = "";
                TxtWorkCode.Text = "";
                TxtBCRCode.Text = "";

                LblUp.Visible = false;
                LblDown.Visible = false;
                LsbRet.Visible = false;

                // 運転中フラグＯＮ
                PubConstClass.pblIsDriving = true;
                // エラーメッセージ表示領域の非表示
                LblErrorMessage.Visible = false;

                if (PubConstClass.pblIsAutoJudge == "1")
                {
                    LblAutoMode.Visible = true;
                    LblAutoMode.Text = "自動判定モード";
                }
                else
                {
                    LblAutoMode.Visible = false;
                }

                // マルチディスプレイの21インチの方に表示
                // Me.Location = New Point(1280, 0)
                CommonModule.OutPutLogFile("〓CONTEC DIO_デバイスＩＤ：" + PubConstClass.sDioName);
                this.Location = new Point(PubConstClass.intXPosition, 0);

                // １秒タイマー設定
                TimDateTime.Interval = 1000;
                TimDateTime.Enabled = true;

                // タイトル項目名称の設定
                string sColName01 = "   No";
                string sColName02 = "企画";
                string sColName03 = "生協名(ｺｰﾄﾞ)";
                string sColName04 = "デポ名(ｺｰﾄﾞ)";
                string sColName05 = "コース";          // 配布曜日(1桁)-配布方面(1桁)-配布方面No(1桁)
                //string sColName06 = "ｶｺﾞ車積載数";
                string sColName06 = "ｶｺﾞ車No";
                string sColName07 = "ｺｰｽ箱";
                string sColName08 = "組合員ｺｰﾄﾞ(FROM-TO)";
                string sColName09 = "ｾｯﾄ順序番号(FROM-TO)";
                string sColName10 = "判定";
                string sColName11 = "束数";
                // タイトル項目幅の設定
                int iColWidth01 = 135;      // No
                int iColWidth02 = 95;       // 企画
                int iColWidth03 = 300;      // 生協名(ｺｰﾄﾞ)
                int iColWidth04 = 300;      // デポ名(ｺｰﾄﾞ)
                int iColWidth05 = 130;      // コース
                int iColWidth06 = 160;      // ｶｺﾞ車積載数→ｶｺﾞ車No
                int iColWidth07 = 130;      // ｺｰｽ箱
                //int iColWidth08 = 390;      // 組合員ｺｰﾄﾞ(FROM-TO)
                int iColWidth08 = 1;        // 組合員ｺｰﾄﾞ(FROM-TO) ← 非表示とする
                int iColWidth09 = 380;      // ｾｯﾄ順序番号(FROM-TO)
                int iColWidth10 = 110;      // 判定
                int iColWidth11 = 110;      // 束数

                // ＯＫ束用表示リストビューのカラムヘッダー設定
                LstDrivingData.View = View.Details;
                ColumnHeader col01 = new ColumnHeader();
                ColumnHeader col02 = new ColumnHeader();
                ColumnHeader col03 = new ColumnHeader();
                ColumnHeader col04 = new ColumnHeader();
                ColumnHeader col05 = new ColumnHeader();
                ColumnHeader col06 = new ColumnHeader();
                ColumnHeader col07 = new ColumnHeader();
                ColumnHeader col08 = new ColumnHeader();
                ColumnHeader col09 = new ColumnHeader();
                ColumnHeader col10 = new ColumnHeader();
                ColumnHeader col11 = new ColumnHeader();
                col01.Text = sColName01;
                col02.Text = sColName02;
                col03.Text = sColName03;
                col04.Text = sColName04;
                col05.Text = sColName05;
                col06.Text = sColName06;
                col07.Text = sColName07;
                col08.Text = sColName08;
                col09.Text = sColName09;
                col10.Text = sColName10;
                col11.Text = sColName11;
                col01.TextAlign = HorizontalAlignment.Center;
                col02.TextAlign = HorizontalAlignment.Center;
                col03.TextAlign = HorizontalAlignment.Center;
                col04.TextAlign = HorizontalAlignment.Center;
                col05.TextAlign = HorizontalAlignment.Center;
                col06.TextAlign = HorizontalAlignment.Center;
                col07.TextAlign = HorizontalAlignment.Center;
                col08.TextAlign = HorizontalAlignment.Center;
                col09.TextAlign = HorizontalAlignment.Center;
                col10.TextAlign = HorizontalAlignment.Center;
                col11.TextAlign = HorizontalAlignment.Center;
                col01.Width = iColWidth01;
                col02.Width = iColWidth02;
                col03.Width = iColWidth03;
                col04.Width = iColWidth04;
                col05.Width = iColWidth05;
                col06.Width = iColWidth06;
                col07.Width = iColWidth07;
                col08.Width = iColWidth08;
                col09.Width = iColWidth09;
                col10.Width = iColWidth10;
                col11.Width = iColWidth11;
                ColumnHeader[] colHeader = new[] { col01, col02, col03, col04, col05, 
                                                   col06, col07, col08, col09, col10, col11 };
                LstDrivingData.Columns.AddRange(colHeader);

                // ＮＧ束用表示リストビューのカラムヘッダー設定
                LstDrivingError.View = View.Details;
                ColumnHeader colNG01 = new ColumnHeader();
                ColumnHeader colNG02 = new ColumnHeader();
                ColumnHeader colNG03 = new ColumnHeader();
                ColumnHeader colNG04 = new ColumnHeader();
                ColumnHeader colNG05 = new ColumnHeader();
                ColumnHeader colNG06 = new ColumnHeader();
                ColumnHeader colNG07 = new ColumnHeader();
                ColumnHeader colNG08 = new ColumnHeader();
                ColumnHeader colNG09 = new ColumnHeader();
                ColumnHeader colNG10 = new ColumnHeader();
                ColumnHeader colNG11 = new ColumnHeader();
                colNG01.Text = sColName01;
                colNG02.Text = sColName02;
                colNG03.Text = sColName03;
                colNG04.Text = sColName04;
                colNG05.Text = sColName05;
                colNG06.Text = sColName06;
                colNG07.Text = sColName07;
                colNG08.Text = sColName08;
                colNG09.Text = sColName09;
                colNG10.Text = sColName10;
                colNG11.Text = sColName11;
                colNG01.TextAlign = HorizontalAlignment.Center;
                colNG02.TextAlign = HorizontalAlignment.Center;
                colNG03.TextAlign = HorizontalAlignment.Center;
                colNG04.TextAlign = HorizontalAlignment.Center;
                colNG05.TextAlign = HorizontalAlignment.Center;
                colNG06.TextAlign = HorizontalAlignment.Center;
                colNG07.TextAlign = HorizontalAlignment.Center;
                colNG08.TextAlign = HorizontalAlignment.Center;
                colNG09.TextAlign = HorizontalAlignment.Center;
                colNG10.TextAlign = HorizontalAlignment.Center;
                colNG11.TextAlign = HorizontalAlignment.Center;
                colNG01.Width = iColWidth01;
                colNG02.Width = iColWidth02;
                colNG03.Width = iColWidth03;
                colNG04.Width = iColWidth04;
                colNG05.Width = iColWidth05;
                colNG06.Width = iColWidth06;
                colNG07.Width = iColWidth07;
                colNG08.Width = iColWidth08;
                colNG09.Width = iColWidth09;
                colNG10.Width = iColWidth10;
                colNG11.Width = iColWidth11;
                ColumnHeader[] colHeaderError = new[] { colNG01, colNG02, colNG03, colNG04, colNG05, 
                                                        colNG06, colNG07, colNG08, colNG09, colNG10, colNG11 };
                LstDrivingError.Columns.AddRange(colHeaderError);

                PubConstClass.iNumberOfProcesses = 0;
                PubConstClass.iNumberOfNGProcesses = 0;
                LblOperationStatus.Text = "生産数：" + PubConstClass.iNumberOfProcesses.ToString("#,###,##0") + "／" + 
                                          PubConstClass.pblRunTotalCount + "（ＮＧ：" + PubConstClass.iNumberOfNGProcesses.ToString("0") + "）";

                for (int N=0; N < PubConstClass.dblCountOfProcesses.Length - 1; N++)
                {
                    PubConstClass.dblCountOfProcesses[N] = 0;
                }                

                // リストビューのダブルバッファを有効とする
                EnableDoubleBuffering(LstDrivingData);
                EnableDoubleBuffering(LstDrivingError);

                // ブザー設定情報の送信
                ((MainForm)Owner.Owner).SendBuzzerSetInfomation();

                // 生産ログファイルチェックタイマー（3秒）設定
                TimCheckSeisanLog.Interval = 3000;
                TimCheckSeisanLog.Enabled = true;

                // アラーム除外リスト取得
                CommonModule.GetOmitAlarmList();

                // 排出ゲート開放信号チェックタイマー
                TimBgEjectThread.Interval = 100;
                TimBgEjectThread.Enabled = true;
                Dio_Init();
                TriggerCheckStart();
                // Call CheckProcess(0, True)
                // 排出ゲート開放信号監視スレッド開始
                BgEjectThread.RunWorkerAsync();

                // シリアルポート名の設定
                SerialPortBcr.PortName = PubConstClass.pblComPort2;
                // シリアルポートの通信速度指定
                switch (PubConstClass.pblComSpeed2)
                {
                    case "0":
                        {
                            SerialPortBcr.BaudRate = 4800;
                            break;
                        }

                    case "1":
                        {
                            SerialPortBcr.BaudRate = 9600;
                            break;
                        }

                    case "2":
                        {
                            SerialPortBcr.BaudRate = 19200;
                            break;
                        }

                    case "3":
                        {
                            SerialPortBcr.BaudRate = 38400;
                            break;
                        }

                    case "4":
                        {
                            SerialPortBcr.BaudRate = 57600;
                            break;
                        }

                    case "5":
                        {
                            SerialPortBcr.BaudRate = 115200;
                            break;
                        }

                    default:
                        {
                            SerialPortBcr.BaudRate = 38400;
                            break;
                        }
                }
                // シリアルポートのパリティ指定
                switch (PubConstClass.pblComParityVar2)
                {
                    case "0":
                        {
                            SerialPortBcr.Parity = Parity.Odd;
                            break;
                        }

                    case "1":
                        {
                            SerialPortBcr.Parity = Parity.Even;
                            break;
                        }

                    default:
                        {
                            SerialPortBcr.Parity = Parity.Even;
                            break;
                        }
                }

                // シリアルポートのパリティ有無
                if (PubConstClass.pblComIsParity2 == "0")
                    SerialPortBcr.Parity = Parity.None;

                // シリアルポートのビット数指定
                switch (PubConstClass.pblComDataLength2)
                {
                    case "0":
                        {
                            SerialPortBcr.DataBits = 8;
                            break;
                        }

                    case "1":
                        {
                            SerialPortBcr.DataBits = 7;
                            break;
                        }

                    default:
                        {
                            SerialPortBcr.DataBits = 8;
                            break;
                        }
                }

                // シリアルポートのストップビット指定
                switch (PubConstClass.pblComStopBit2)
                {
                    case "0":
                        {
                            SerialPortBcr.StopBits = StopBits.One;
                            break;
                        }

                    case "1":
                        {
                            SerialPortBcr.StopBits = StopBits.Two;
                            break;
                        }

                    default:
                        {
                            SerialPortBcr.StopBits = StopBits.One;
                            break;
                        }
                }

                // シリアルポートのオープン
                SerialPortBcr.Open();

                // シリアルポート（バーコードリーダ）にデータ送信（動作可コマンド）
                byte[] dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(PubConstClass.CMD_SEND_a + Constants.vbCr);
                SerialPortBcr.Write(dat, 0, dat.GetLength(0));
                LoggingSerialSendData(PubConstClass.CMD_SEND_a);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DrivingForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// コントロールのDoubleBufferedプロパティをTrueにする
        /// </summary>
        /// <param name="control"></param>
        public static void EnableDoubleBuffering(Control control)
        {
            control.GetType().InvokeMember("DoubleBuffered", 
                                            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, 
                                            null/* TODO Change to default(_) if this is not a reference type */, 
                                            control, 
                                            new object[] { true }
                                            );
        }

        /// <summary>
        /// 通信ログの保存処理（送信データ用）
        /// </summary>
        /// <param name="strWriteData"></param>
        /// <remarks></remarks>
        private void LoggingSerialSendData(string strWriteData)
        {
            CommonModule.OutPutLogFile("【バーコードリーダー】送信：" + strWriteData + "<CR>");
        }


        /// <summary>
        /// 日付時間表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TimDateTime_Tick(object sender, EventArgs e)
        {
            try
            {
                // 現在時刻の表示
                LblDateTime.Text = DateTime.Now.ToString("yyyy年MM月dd日(ddd) HH:mm:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【TimDateTime_Tick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 二つのファイルの最終書き込み日時を取得して比較する
        /// </summary>
        /// <param name="fileX"></param>
        /// <param name="fileY"></param>
        /// <returns></returns>
        public static int CompareLastWriteTime(string fileX, string fileY)
        {
            return DateTime.Compare(File.GetLastWriteTime(fileX), File.GetLastWriteTime(fileY));
        }

        /// <summary>
        /// 生産ログ存在チェック及び読み取り処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TimCheckSeisanLog_Tick(object sender, EventArgs e)
        {
            string[] strArray;
            string strSeisanLogFile;
            string strEventLogFile;
            string strCheckFilePath;
            bool blnRetVal;

            try
            {
                //ストップウォッチを開始する
                Stopwatch stw = Stopwatch.StartNew();
                stw.Reset();
                stw.Start();

                ////////////////////////////////////////
                #region 生産ログファイル一覧の取得
                string[] strTempArray = Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder), 
                                                           "*.plg", SearchOption.TopDirectoryOnly);
                // 取得したすべてのファイルを最終書き込み日時順でソートする)
                Array.Sort<string>(strTempArray, CompareLastWriteTime);

                //for (var N = 0; N <= strTempArray.Length - 1; N++)
                //{
                //    CommonModule.OutPutLogFile("【読取ファイル名】" + strTempArray[N]);
                //}

                foreach (string strSeisanLogFileFullPathName in strTempArray)
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    // フルパス名からファイル名のみ取得
                    strSeisanLogFile = strArray[strArray.Length - 1];
                    CommonModule.OutPutLogFile("生産ログファイル（" + strSeisanLogFile + "）読込");

                    //////////////////////////////////////
                    // 生産ログファイルの内容をチェック //
                    //////////////////////////////////////
                    blnRetVal = CheckSeisanLogData(strSeisanLogFile);
                    if (blnRetVal == false)
                    {
                        CommonModule.OutPutLogFile("生産ログ読取処理スキップ【TimCheckSeisanLog_Tick】");
                        return;
                    }

                    FileExistDeleteMove(PubConstClass.pblSeisanFolder, strSeisanLogFile, PubConstClass.pblOperationPcTotallingFolder);
                }
                #endregion
                ////////////////////////////////////////

                ////////////////////////////////////////
                #region 最終生産ログファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder), 
                                                                                   "*.plog", SearchOption.TopDirectoryOnly))
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    // フルパス名からファイル名のみ取得
                    strSeisanLogFile = strArray[strArray.Length - 1];
                    CommonModule.OutPutLogFile("最終生産ログファイル（" + strSeisanLogFile + "）読込");

                    //// 移動先に同名のファイルが存在するかチェック
                    //if (File.Exists(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                                                          PubConstClass.pblCurrentDate) + strSeisanLogFile) == true)
                    //{
                    //    // 同名ファイルの削除
                    //    File.Delete(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                                                          PubConstClass.pblCurrentDate) + strSeisanLogFile);
                    //    CommonModule.OutPutLogFile(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                               PubConstClass.pblCurrentDate) + strSeisanLogFile + " 削除");
                    //}
                    //// 最終生産ログを管理パソコン内部フォルダに移動
                    //File.Move(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder) + strSeisanLogFile, 
                    //          CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                                                    PubConstClass.pblCurrentDate) + strSeisanLogFile);

                    FileExistDeleteMove(PubConstClass.pblSeisanFolder, strSeisanLogFile, PubConstClass.pblOperationPcTotallingFolder);

                    // 最終生産ログファイル受信時の内部稼動データをファイルに出力
                    // （仕様書では最終生産ログとのマージ処理となっている）
                    string strPutDataPath;
                    strPutDataPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + 
                                                                               PubConstClass.pblCurrentDate + @"\" + "稼働データ格納配列ファイル" + 
                                                                               DateTime.Now.ToString("HHmmss") + ".txt";
                    using (StreamWriter sw = new StreamWriter(strPutDataPath, false, Encoding.Default))
                    {
                        for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                        {
                            sw.WriteLine(PubConstClass.pblTransactionData[intLoopCnt]);
                        }
                    }
                    CommonModule.OutPutLogFile("■稼動データ格納ファイル作成：" + strPutDataPath);
                }
                #endregion
                ////////////////////////////////////////

                ////////////////////////////////////////
                #region イベントログファイル一覧の取得
                foreach (string strEventLogFileFullPathName in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblEventFolder), 
                                                                                  "*.elg", SearchOption.TopDirectoryOnly))
                {
                    strArray = strEventLogFileFullPathName.Split('\\');
                    // フルパス名からファイル名のみ取得
                    strEventLogFile = strArray[strArray.Length - 1];
                    CommonModule.OutPutLogFile("イベントログファイル（" + strEventLogFile + "）読込");

                    //// 移動先に同名のファイルが存在するかチェック
                    //if (File.Exists(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                                                          PubConstClass.pblCurrentDate) + strEventLogFile) == true)
                    //{
                    //    // 同名ファイルの削除
                    //    File.Delete(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                                                          PubConstClass.pblCurrentDate) + strEventLogFile);
                    //    CommonModule.OutPutLogFile(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                                                                         PubConstClass.pblCurrentDate) + strEventLogFile + " 削除");
                    //}

                    string strErrorMessage = "";
                    string strReadData;
                    string sErrorCode;
                    // イベントログの内容チェック
                    strCheckFilePath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblEventFolder) + strEventLogFile;
                    using (StreamReader sr = new StreamReader(strCheckFilePath, Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {
                            strReadData = sr.ReadLine();
                            strArray = strReadData.Split(',');
                            // エラーメッセージを格納                        
                            strErrorMessage = "エ　ラ　ー　停　止" + Constants.vbCr + strArray[2].Substring(strArray[2].Length - 5, 5);
                            // エラーコードを格納
                            sErrorCode = strArray[2].Substring(strArray[2].Length - 5, 5);
                            // アラーム除外リストと一致するかのチェック
                            for (var N = 0; N <= PubConstClass.intOmitAlarmListCnt - 1; N++)
                            {
                                if (PubConstClass.pblOmitAlarmList[N] == sErrorCode)
                                {
                                    // アラーム除外リストと一致（エラーメッセージクリア）
                                    strErrorMessage = "";
                                    break;
                                }
                            }
                            // エラーメッセージのチェック
                            if (strErrorMessage != "")
                            {
                                // エラーメッセージが存在する場合は表示する
                                LblErrorMessage.Text = strErrorMessage;
                                LblErrorMessage.Visible = true;
                                TimErrorDispTime.Interval = 1000;    // １秒タイマーセット
                                TimErrorDispTime.Enabled = true;
                                // エラー表示時間の設定
                                intErrorCnt = Convert.ToInt32(PubConstClass.pblErrorDispTime);
                                break;
                            }
                        }
                    }

                    //// イベントログを管理パソコン内部フォルダに移動
                    //File.Move(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblEventFolder) + strEventLogFile, 
                    //          CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                    //                                                    PubConstClass.pblCurrentDate) + strEventLogFile);

                    FileExistDeleteMove(PubConstClass.pblEventFolder, strEventLogFile, PubConstClass.pblOperationPcEventFolder);
                }
                #endregion
                ////////////////////////////////////////

                ////////////////////////////////////////
                #region 最終イベントログファイル一覧の取得
                foreach (string strEventLogFileFullPathName in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblEventFolder), 
                                                                                  "*.elog", SearchOption.TopDirectoryOnly))
                {
                    strArray = strEventLogFileFullPathName.Split('\\');
                    // フルパス名からファイル名のみ取得
                    strEventLogFile = strArray[strArray.Length - 1];
                    CommonModule.OutPutLogFile("最終イベントログファイル（" + strEventLogFile + "）読込");

                    string sSourceFile = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblEventFolder) + strEventLogFile;
                    string sDestinationFile;
                    //string sDestinationFolder;

                    sDestinationFile = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" +
                                                                                 PubConstClass.pblCurrentDate) + strEventLogFile;

                    // 移動先に同名のファイルが存在するかチェック
                    if (File.Exists(sDestinationFile) == true)
                    {
                        // 同名ファイルの削除
                        File.Delete(sDestinationFile);
                        CommonModule.OutPutLogFile(sDestinationFile + " 削除");
                    }

                    // 最終イベントログの内容チェック
                    //strCheckFilePath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblEventFolder) + strEventLogFile;
                    using (StreamReader sr = new StreamReader(sSourceFile, Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {

                            string strReadData = sr.ReadLine();
                            strArray = strReadData.Split(',');
                            CommonModule.OutPutLogFile("strReadData：" + strReadData);
                            // 「-0009:END OF PRODUCTION」が存在するかチェック
                            if (strArray[2] == "-0009")
                            {
                                // 存在する場合は、制御データファイルを削除する
                                // 制御データファイル名称の生成
                                foreach (string strDeleteFile in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblControlFolder), 
                                                                                    "*.sel", SearchOption.TopDirectoryOnly))
                                {
                                    // 制御ファイルの削除
                                    File.Delete(strDeleteFile);
                                    CommonModule.OutPutLogFile("■制御データ削除：" + strDeleteFile);
                                }
                            }
                        }
                    }

                    //// リネーム後のファイル名を取得する
                    //string strRenameFile;
                    //string strHHmmss;
                    //strArray = strEventLogFile.Split('.');
                    //strHHmmss = DateTime.Now.ToString("HHmmss");
                    //strRenameFile = strArray[0] + "_" + strHHmmss + "." + strArray[1];
                    //// 最終イベントログを管理パソコン内部フォルダに移動しタイムスタンプ付きでリネームする
                    //File.Copy(sSourceFile, CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" +
                    //                                                                 PubConstClass.pblCurrentDate) + strRenameFile);
                    //sDestinationFolder = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblOperationPcTotallingFolder + @"\" +
                    //                                                               PubConstClass.pblCurrentDate);
                    //// 移動先フォルダの存在チェック
                    //if (Directory.Exists(sDestinationFolder))
                    //{
                    //    sDestinationFile = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblOperationPcTotallingFolder + @"\" +
                    //                                                                 PubConstClass.pblCurrentDate) + strEventLogFile;
                    //    // 移動先に同名のファイルが存在するかチェック
                    //    if (File.Exists(sDestinationFile) == true)
                    //    {
                    //        // 同名ファイルの削除
                    //        File.Delete(sDestinationFile);
                    //        CommonModule.OutPutLogFile(sDestinationFile + " 削除");
                    //    }
                    //    // 生産ログを管理パソコン内部フォルダに移動
                    //    File.Move(sSourceFile, sDestinationFile);
                    //}
                    //else
                    //{
                    //    // 移動元ファイルの削除
                    //    File.Delete(sSourceFile);
                    //}

                    FileExistDeleteMove(PubConstClass.pblEventFolder, strEventLogFile, PubConstClass.pblOperationPcEventFolder);
                }
                #endregion
                ////////////////////////////////////////

                stw.Stop();
                //CommonModule.OutPutLogFile("生産ログ存在チェック処理時間：" + stw.Elapsed);

            }
            catch (Exception ex)
            {
                CommonModule.OutPutLogFile("■生産ログ内容チェックエラースキップ【TimCheckSeisanLog_Tick】" + ex.StackTrace);
            }
        }


        /// <summary>
        /// （１）指定ファイルの内部受信フォルダへの上書きコピー処理
        /// （２）指定ファイルの稼働管理PCファルダへの上書きコピー処理
        /// （３）指定ファイルの削除処理
        /// </summary>
        /// <param name="sLogFolder">内部受信フォルダ</param>
        /// <param name="sLogFile">コピーファイル</param>
        /// <param name="sOperationPcFolder">稼働管理PCフォルダ</param>
        private void FileExistDeleteMove(string sLogFolder, string sLogFile, string sOperationPcFolder)
        {
            string sSourceFile = CommonModule.IncludeTrailingPathDelimiter(sLogFolder) + sLogFile; ;
            string sDestinationFile;
            string sDestinationFolder;

            try
            {
                // 内部受信フォルダ＋（YYYYMMDD）＋コピーファイル
                sDestinationFile = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder + @"\" + 
                                                                             PubConstClass.pblCurrentDate) + sLogFile;
                // 移動先に同名のファイルが存在するかチェック
                if (File.Exists(sDestinationFile) == true)
                {
                    // 同名ファイルの削除
                    File.Delete(sDestinationFile);
                    CommonModule.OutPutLogFile(sDestinationFile + " 削除");
                }
                // 生産ログを管理パソコン内部フォルダにコピー
                File.Copy(sSourceFile, sDestinationFile);

                // 稼働管理PCフォルダ＋（YYYYMMDD）
                sDestinationFolder = CommonModule.IncludeTrailingPathDelimiter(sOperationPcFolder + @"\" + PubConstClass.pblCurrentDate);
                // 移動先フォルダの存在チェック
                if (Directory.Exists(sDestinationFolder))
                {
                    sDestinationFile = CommonModule.IncludeTrailingPathDelimiter(sOperationPcFolder + @"\" + PubConstClass.pblCurrentDate) + sLogFile;
                    // 移動先に同名のファイルが存在するかチェック
                    if (File.Exists(sDestinationFile) == true)
                    {
                        // 同名ファイルの削除
                        File.Delete(sDestinationFile);
                        CommonModule.OutPutLogFile(sDestinationFile + " 削除");
                    }
                    // 生産ログを管理パソコン内部フォルダに移動
                    File.Move(sSourceFile, sDestinationFile);
                }
                else
                {
                    // 移動元ファイルの削除
                    File.Delete(sSourceFile);
                }
            }
            catch (Exception ex)
            {
                CommonModule.OutPutLogFile("■エラー【FileExistDeleteMove】" + ex.Message);
                // 移動元ファイルの削除
                File.Delete(sSourceFile);
            }
        }

        private bool bIsBusyFlag;
        private bool bIsPlayFlag;

        /// <summary>
        /// 生産ログの内容をチェック
        /// （１）作業ｺｰﾄﾞから該当する結束束の最初と最後の作業ｺｰﾄﾞを取得する
        /// （２）「OK」「NG」を判定し内部メモリに格納する
        /// （３）結束束の最後の作業ｺｰﾄﾞが存在する時は表示する
        /// （４）表示する前に判定結果を取得する
        /// </summary>
        /// <param name="strReadFileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CheckSeisanLogData(string strReadFileName)
        {
            string strReadDataPath;
            string strReadData;
            string[] strArray;
            string[] strTranArray;
            string strJudge;
            string strUnionMemberCode;
            int intLoopCnt;
            string strConcatData;

            bool CheckSeisanLogData = true;

            if (bIsBusyFlag == true)
            {
                CommonModule.OutPutLogFile("〓〓〓CheckSeisanLogData()処理スキップ〓〓〓");
                return false;
            }
            try
            {
                //CommonModule.OutPutLogFile("■CheckSeisanLogData()呼び出し");
                bIsBusyFlag = true;
                bIsPlayFlag = false;

                // 生産ログファイルの内容チェック
                strReadDataPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder) + strReadFileName;
                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strReadData = sr.ReadLine();
                        CommonModule.OutPutLogFile("読取り内容：" + strReadData);
                        // ↓読取り内容が存在するかのチェック
                        if (strReadData != "")
                        {
                            strArray = strReadData.Split(',');
                            // ↓対象となる生産ログのチェック
                            if (strArray[0].Substring(10,10) == "0000000000")
                            {
                                // コープ九州は何もしない                            }
                            }
                            else
                            {
                                // エラー（排出結果：OK以外の場合）のチェック
                                if (strArray[3].Substring(11, 1) != "0")
                                {
                                    // 判定
                                    strJudge = "NG";
                                    //CommonModule.OutPutLogFile("【DEBUG】排出結果エラー：" + strArray[3]);
                                    //TimBuzzer.Enabled = false;
                                    PubConstClass.iNumberOfNGProcesses++;
                                }
                                else
                                {
                                    // 排出結果OK
                                    // エラー（最終結果：OKとｼﾄﾏﾘｶﾊﾞﾘｰ以外の場合）のチェック
                                    if (strArray[3].Substring(12, 1) == "0" | strArray[3].Substring(12, 1) == "1")
                                    {
                                        // 判定
                                        strJudge = "OK";
                                        //CommonModule.OutPutLogFile("【DEBUG】最終結果ＯＫ：" + strArray[3]);
                                        PubConstClass.iNumberOfProcesses++;
                                        int iIndex = DateTime.Now.Hour;
                                        PubConstClass.dblCountOfProcesses[iIndex]++;
                                    }
                                    else
                                    {
                                        // 判定
                                        strJudge = "NG";
                                        //CommonModule.OutPutLogFile("【DEBUG】最終結果エラー：" + strArray[3]);
                                    }
                                }

                                // 組合員ｺｰﾄﾞ（10桁）の取得
                                //strUnionMemberCode = strArray[0].Substring(10, 9);
                                strUnionMemberCode = strArray[0].Substring(10, 8);
                                LblReadStatus.Text = strUnionMemberCode;
                                // ↓該当の組合員ｺｰﾄﾞのチェック
                                for (intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                                {
                                    strTranArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');
                                    // 該当の組合員ｺｰﾄﾞのチェック
                                    if (strTranArray[17].PadLeft(8).Replace(" ","0") == strUnionMemberCode)
                                    {
                                        // 判定をセットする
                                        strTranArray[50] = strJudge;
                                        strConcatData = "";
                                        for (var intConcatCnt = 0; intConcatCnt <= strTranArray.Length - 1; intConcatCnt++)
                                        {
                                            strConcatData += strTranArray[intConcatCnt] + ",";
                                        }                                            
                                        PubConstClass.pblTransactionData[intLoopCnt] = strConcatData;
                                        //CommonModule.OutPutLogFile("【DEBUG】結合データ：" + PubConstClass.pblTransactionData[intLoopCnt]);
                                        // 該当の組合員ｺｰﾄﾞが最終の組合員ｺｰﾄﾞと同じか確認
                                        //if (strTranArray[52] == strUnionMemberCode)
                                        if (strTranArray[52].PadLeft(8).Replace(" ", "0") == strUnionMemberCode)
                                        {
                                            //CommonModule.OutPutLogFile("■最終組合員ｺｰﾄﾞ：" + strTranArray[52] + "／結束束インデックス：" + strTranArray[53]);
                                            ////////////////////
                                            // 生産ログの表示 //
                                            ////////////////////
                                            DisplaySeisanLogData(PubConstClass.pblTransactionData[intLoopCnt], intLoopCnt);                                            
                                            LblOperationStatus.Text = "生産数：" + PubConstClass.iNumberOfProcesses.ToString("#,###,##0") + "／" +
                                                                      PubConstClass.pblRunTotalCount + 
                                                                      "（ＮＧ：" + PubConstClass.iNumberOfNGProcesses.ToString("0") + "）";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonModule.OutPutLogFile("■生産ログ内容チェックスキップ【CheckSeisanLogData】" + ex.Message);
                CheckSeisanLogData = false;
                bIsBusyFlag = false;
            }
            bIsBusyFlag = false;
            return CheckSeisanLogData;
        }

        private string sDepoCode = "";
        private string sCourceCode = "";

        /// <summary>
        /// 生産ログファイルの内容表示
        /// </summary>
        /// <param name="strTransactionData"></param>
        /// <param name="intIndex"></param>
        /// <remarks></remarks>
        private void DisplaySeisanLogData(string strTransactionData, int intIndex)
        {
            string[] col = new string[12];
            ListViewItem itm;
            string[] strArray;
            string[] strWorkArray;
            string[] strCompArray;
            bool blnOKFlag;
            int intLoopCnt;
            string strSagyoCode;
            byte[] dat;
            bool bNonPlaySoundFlg;
            string sCoopName;       // 生協名
            string sDepoName;       // デポ名

            try
            {
                intSesanCounter += 1;
                strArray = strTransactionData.Split(',');
                col[0] = intSesanCounter.ToString("000000");
                // 生協コードから生協名を取得する
                if (PubConstClass.dicCoopCodeData.ContainsKey(strArray[2]))
                {
                    sCoopName = PubConstClass.dicCoopCodeData[strArray[2]];
                }
                else
                {
                    sCoopName = "該当なし";
                }
                // デポコードからデポ名を取得する
                if (PubConstClass.dicDepoCodeData.ContainsKey(strArray[4]))
                {
                    sDepoName = PubConstClass.dicDepoCodeData[strArray[4]];
                }
                else
                {
                    sDepoName = "該当なし";
                }
                // 企画号数(1桁)
                col[1] = strArray[0];
                // 生協名(ｺｰﾄﾞ)
                col[2] = sCoopName + "(" + strArray[2] + ")";
                // タイトル「運転画面（生協名）」表示
                LblTitle.Text = "運転画面（" + sCoopName + "）";
                // デポ名(ｺｰﾄﾞ)
                col[3] = sDepoName + "(" + strArray[4] + ")";
                // コース「配達次曜日(1桁)＋配達次方面(1桁)＋配達次方面No(1桁)」
                col[4] = strArray[7] + "-" + strArray[8] + "-" + strArray[9];

                bNonPlaySoundFlg = false;

                if (sDepoCode == "")
                {
                    sDepoCode = strArray[4];
                }                    
                else if (sDepoCode != strArray[4])
                {
                    sDepoCode = strArray[4];
                    // デポが変わりました
                    CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\depot.wav");
                    bNonPlaySoundFlg = true;
                    sCourceCode = strArray[7] + strArray[8] + strArray[9];
                }

                if (sCourceCode == "")
                {
                    sCourceCode = strArray[7] + strArray[8] + strArray[9];
                }                    
                else if (sCourceCode != (strArray[7] + strArray[8] + strArray[9]) & bNonPlaySoundFlg == false)
                {
                    sCourceCode = strArray[7] + strArray[8] + strArray[9];
                    if (bIsPlayFlag == false)
                    {
                        bIsPlayFlag = true;
                        // コースが変わりました
                        CommonModule.PlaySound(CommonModule.IncludeTrailingPathDelimiter(Application.StartupPath) + @"sound\course.wav");
                    }
                    else
                    {
                        CommonModule.OutPutLogFile("【デバッグ】「コースが変わりました」が既に発音されました。");
                    }
                }
                // カゴ車積載数
                col[5] = strArray[57] + "/" + strArray[58];

                //  0  1  2 3 4 5  6 7 8 9        * 1 2
                // 51,01,18,C,2,1,20,1,4,該当なし,1,1,66,
                // 51,01,18,C,2,1,20,2,4,該当なし,1,2,66,
                // ｺｰｽ箱数「配達次曜日(1桁)＋配達次方面(1桁)＋配達次方面No(1桁)」
                strWorkArray = PubConstClass.pblOrderArray[Convert.ToInt32(strArray[53])].Split(',');
                if (strWorkArray.Length > 12)
                {
                    col[6] = strWorkArray[11] + "/" + strWorkArray[12];
                }
                else
                {
                    col[6] = "99/99";
                }

                // 組合員ｺｰﾄﾞ（FROM-TO）
                col[7] = strArray[51] + " - " + strArray[52];

                // 作業ｺｰﾄﾞ(FROM - TO)
                col[8] = strArray[55] + " - " + strArray[56];
                TxtWorkCode.Text = strArray[56];

                // 束数
                col[10] = strArray[54];

                strSagyoCode = "";

                // 自動判定モードのチェック
                if (PubConstClass.pblIsAutoJudge == "1")
                    col[9] = "OK";
                else
                    col[9] = "未登録";

                blnOKFlag = true;

                // 判定の確認処理
                for (intLoopCnt = intIndex; intLoopCnt >= 0; intLoopCnt += -1)
                {
                    strCompArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');
                    // 最初の作業ｺｰﾄﾞが同じかチェック
                    if (strCompArray[51] == strArray[51])
                    {
                        // 判定文字をチェック
                        if (strCompArray[50] != "OK" && strCompArray[50] != "未")
                        {
                            // NG判定
                            col[9] = "NG";
                            blnOKFlag = false;
                            strSagyoCode += strCompArray[45] + Constants.vbCr;
                            //CommonModule.OutPutLogFile("■ＮＧデータ：" + PubConstClass.pblTransactionData[intLoopCnt]);
                        }
                    }
                    else
                        // 最初の作業ｺｰﾄﾞが異なったら抜ける
                        break;
                }

                // データの表示
                itm = new ListViewItem(col);

                // 自動判定モードのチェック
                if (PubConstClass.pblIsAutoJudge == "1")
                {
                    if (blnOKFlag == true)
                    {
                        // 自動判定モード（ＯＫ処理）
                        LstDrivingData.Items.Add(itm);
                        LstDrivingData.Items[LstDrivingData.Items.Count - 1].UseItemStyleForSubItems = false;
                        LstDrivingData.Select();
                        LstDrivingData.Items[LstDrivingData.Items.Count - 1].EnsureVisible();

                        for (intLoopCnt = 0; intLoopCnt <= PubConstClass.DEF_CNT_FOR_DRIVING; intLoopCnt++)
                            LstDrivingData.Items[LstDrivingData.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.White;
                        LstDrivingData.Items[LstDrivingData.Items.Count - 1].SubItems[9].BackColor = Color.Green;
                        LstDrivingData.Items[LstDrivingData.Items.Count - 1].SubItems[9].ForeColor = Color.White;

                        CommonModule.OutPutLogFile("■（DisplaySeisanLogData：自動判定モード）ＯＫ束の音色で発音" +
                           PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName1) - 1]);
                        // ＯＫ束の音色で発音
                        ((MainForm)Owner.Owner).SendBuzzerData(PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName1) - 1]);
                        TimBuzzer.Interval = 1000;
                        TimBuzzer.Enabled = true;
                        CommonModule.OutPutLogFile("【DisplaySeisanLogData】ＯＫ束音発音");
                    }
                    else
                    {
                        // 自動判定モード（ＮＧ処理）
                        LstDrivingError.Items.Add(itm);
                        LstDrivingError.Items[LstDrivingError.Items.Count - 1].UseItemStyleForSubItems = false;
                        LstDrivingError.Select();
                        LstDrivingError.Items[LstDrivingError.Items.Count - 1].EnsureVisible();

                        for (intLoopCnt = 0; intLoopCnt <= PubConstClass.DEF_CNT_FOR_DRIVING; intLoopCnt++)
                            LstDrivingError.Items[LstDrivingError.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.LightGray;
                        LstDrivingError.Items[LstDrivingError.Items.Count - 1].SubItems[9].BackColor = Color.Red;
                        LstDrivingError.Items[LstDrivingError.Items.Count - 1].SubItems[9].ForeColor = Color.White;
                    }
                }
                else
                    // 自動判定でない（従来のバーコード読込むモード）
                    if (blnOKFlag == true)
                {
                    // 「ＯＫ」の処理（「未登録」と表示する）
                    LstDrivingData.Items.Add(itm);
                    LstDrivingData.Items[LstDrivingData.Items.Count - 1].UseItemStyleForSubItems = false;
                    LstDrivingData.Select();
                    LstDrivingData.Items[LstDrivingData.Items.Count - 1].EnsureVisible();

                    for (intLoopCnt = 0; intLoopCnt <= PubConstClass.DEF_CNT_FOR_DRIVING; intLoopCnt++)
                        LstDrivingData.Items[LstDrivingData.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.LightGray;
                }
                else
                {
                    // 「ＮＧ」の表示
                    LstDrivingError.Items.Add(itm);
                    LstDrivingError.Items[LstDrivingError.Items.Count - 1].UseItemStyleForSubItems = false;
                    LstDrivingError.Select();
                    LstDrivingError.Items[LstDrivingError.Items.Count - 1].EnsureVisible();

                    for (intLoopCnt = 0; intLoopCnt <= PubConstClass.DEF_CNT_FOR_DRIVING; intLoopCnt++)
                        LstDrivingError.Items[LstDrivingError.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.LightGray;
                    LstDrivingError.Items[LstDrivingError.Items.Count - 1].SubItems[9].BackColor = Color.Red;
                    LstDrivingError.Items[LstDrivingError.Items.Count - 1].SubItems[9].ForeColor = Color.White;
                }

                if (blnOKFlag == false)
                {
                    // シリアルポートにデータ送信（ＮＧ束コマンド）
                    dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes(PubConstClass.CMD_SEND_m + Constants.vbCr);
                    ((MainForm)Owner.Owner).SerialPort.Write(dat, 0, dat.GetLength(0));
                    LoggingSerialSendData(PubConstClass.CMD_SEND_m);
                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace, "【DisplaySeisanLogData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CommonModule.OutPutLogFile("【DisplaySeisanLogData】" + ex.StackTrace);
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
            DialogResult result;
            try
            {
                CommonModule.OutPutLogFile("■運転画面「閉じる」ボタンクリック");
                result = MessageBox.Show("運転画面を閉じますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    CommonModule.OutPutLogFile("■運転画面「閉じる」ボタン処理");
                    PubConstClass.pblIsDriving = false;
                    this.Hide();
                }
                else
                    CommonModule.OutPutLogFile("■運転画面「閉じる」ボタンキャンセル");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnClose_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 曜日コードから曜日を取得する
        /// </summary>
        /// <param name="strWeekCode"></param>
        /// <returns></returns>z
        /// <remarks></remarks>
        private string GetWeekName(string strWeekCode)
        {
            string GetWeekName = "";

            try
            {
                switch (strWeekCode)
                {
                    case "1":
                        {
                            GetWeekName = "月";
                            break;
                        }

                    case "2":
                        {
                            GetWeekName = "火";
                            break;
                        }

                    case "3":
                        {
                            GetWeekName = "水";
                            break;
                        }

                    case "4":
                        {
                            GetWeekName = "木";
                            break;
                        }

                    case "5":
                        {
                            GetWeekName = "金";
                            break;
                        }

                    case "6":
                        {
                            GetWeekName = "土";
                            break;
                        }

                    case "7":
                        {
                            GetWeekName = "日";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.StackTrace, "【GetWeekName】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return GetWeekName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void SerialPortBcr_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data;
            object[] args = new object[1];

            data = "";

            try
            {
                // シリアルポートをオープンしていない場合、処理を行わない。
                if (SerialPortBcr.IsOpen == false)
                    return;
                // <CR>まで読み込む
                data = SerialPortBcr.ReadTo(ControlChars.Cr.ToString());

                if (data.IndexOf("?") > 0)
                {
                    CommonModule.OutPutLogFile("■受信（パリティエラー）：" + data.ToString() + "<CR>");
                    BeginInvoke(new Delegate_RcvDataToTextBox(RcvDataToTextBox), "パリティエラー：" + "data.ToString" + ControlChars.Cr);
                }

                // 受信データの格納
                BeginInvoke(new Delegate_RcvDataToTextBox(RcvDataToTextBox), data.ToString() + ControlChars.Cr);
            }
            catch (TimeoutException)
            {
                // ディスカードするデータ
                CommonModule.OutPutLogFile("■データ受信タイムアウトエラー：<CR>未受信で切り捨てたデータ：" + data);
            }
            catch (Exception ex)
            {                
                CommonModule.OutPutLogFile("【SerialPortBcr_DataReceived】" + ex.Message);
            }
        }


        /// <summary>
        /// 受信データによる各コマンド処理
        /// </summary>
        /// <param name="data">受信した文字列</param>
        /// <remarks></remarks>
        private void RcvDataToTextBox(string data)
        {
            string strMessage;

            try
            {
                // 受信データをテキストボックスの最後に追記する。
                if (Information.IsNothing(data) == false)
                {
                    if (data.Length < 9)
                    {
                        CommonModule.OutPutLogFile("■不正データ受信：" + data.Replace(Constants.vbCr, "<CR>"));
                        return;
                    }

                    TxtBCRCode.Text = data.Replace(Constants.vbCr, "").Substring(7, 9);

                    // 作業コード（組合員番号）のチェック
                    CheckWorkCode(TxtBCRCode.Text);

                    CommonModule.OutPutLogFile("受信データ：" + data.Replace(Constants.vbCr, "<CR>"));

                    if (data.Length >= 2)
                    {
                        switch (data.Substring(0, 1))
                        {
                            case PubConstClass.CMD_RECIEVE_A:    // シトマからの結束束区分け信号を受信
                                {
                                    CommonModule.OutPutLogFile("■シトマからの結束束区分け信号（" + PubConstClass.CMD_RECIEVE_A + "）を受信");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_B:    // シトマからコース区分け信号を受信
                                {
                                    CommonModule.OutPutLogFile("■シトマからコース区分け信号（" + PubConstClass.CMD_RECIEVE_B + "）を受信");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_C:    // シトマからセンター区分け信号を受信
                                {
                                    CommonModule.OutPutLogFile("■シトマからセンター区分け信号（" + PubConstClass.CMD_RECIEVE_C + "）を受信");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_D:    // シトマから単協区分け信号を受信
                                {
                                    CommonModule.OutPutLogFile("■シトマから単協区分け信号（" + PubConstClass.CMD_RECIEVE_D + "）を受信");
                                    break;
                                }

                            case PubConstClass.CMD_RECIEVE_E:    // シトマから排出ゲート開放信号を受信
                                {
                                    CommonModule.OutPutLogFile("■シトマから排出ゲート開放信号（" + PubConstClass.CMD_RECIEVE_E + "）を受信");
                                    break;
                                }

                            default:
                                {
                                    // 受信データの格納
                                    CommonModule.OutPutLogFile("■シリアルポートから受信：" + data.Replace(ControlChars.Cr.ToString(), "<CR>"));
                                    break;
                                }
                        }
                    }
                    // 受信データの格納
                    CommonModule.OutPutLogFile("■受信：" + data.Replace(ControlChars.Cr.ToString(), "<CR>"));
                }
            }
            catch (Exception ex)
            {
                strMessage = "【RcvDataToTextBox】" + ex.Message;
                CommonModule.OutPutLogFile(strMessage);
                MessageBox.Show(strMessage, "システムエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 作業コード（ｾｯﾄ順序番号）のチェック処理
        /// </summary>
        /// <param name="strChkData"></param>
        /// <remarks></remarks>
        public void CheckWorkCode(string strChkData)
        {
            string strMessage;
            bool blnIsOKFlag;
            string[] strArray;
            string[] strWorkArray;

            int intWorkCnt;
            int intChkIndex;
            int intOrderArrayIndex;

            string sStartUnionCode;     // 最初の組合員コード
            string sEndUnionCode;       // 最後の組合員コード
            int iStartUnionIndex=0;     // 最初の組合員コードインデックス
            int iEndUnionIndex=0;       // 最後の組合員コードインデックス

            try
            {
                blnIsOKFlag = true;

                // 該当ＮＧ束リストビューのチェック
                for (var intLoopCnt = 0; intLoopCnt <= LstDrivingError.Items.Count - 1; intLoopCnt++)
                {

                    // 最初と最後のｾｯﾄ順序番号を抽出する
                    strArray = LstDrivingError.Items[intLoopCnt].SubItems[8].Text.Split('-');
                    sStartUnionCode = strArray[0].Trim();
                    sEndUnionCode = strArray[1].Trim();

                    // 最後の組合員コードの存在チェック
                    if (strArray[1].Trim() == strChkData)
                    {
                        for (intWorkCnt = 0; intWorkCnt <= PubConstClass.intTransactionIndex - 1; intWorkCnt++)
                        {
                            strWorkArray = PubConstClass.pblTransactionData[intWorkCnt].Split(',');

                            // 最初のｾｯﾄ順序番号のチェック
                            if (sStartUnionCode == strWorkArray[46])
                                // インデックスをセット
                                iStartUnionIndex = intWorkCnt;

                            // 最後ｾｯﾄ順序番号のチェック
                            if (sEndUnionCode == strWorkArray[46])
                            {
                                // インデックスをセット
                                iEndUnionIndex = intWorkCnt;
                                // 最後のセット順序番号が一致した場合はループから抜ける
                                break;
                            }
                        }

                        // 「OK」「RE」以外が無いかをチェック
                        for (intChkIndex = iStartUnionIndex; intChkIndex <= iEndUnionIndex; intChkIndex++)
                        {
                            strWorkArray = PubConstClass.pblTransactionData[intChkIndex].Split(',');
                            // 判定結果をチェック
                            if (strWorkArray[50] != "OK" & strWorkArray[50] != "RE")
                            {
                                blnIsOKFlag = false;
                                CommonModule.OutPutLogFile("■（CheckWorkCode）NGデータ：" + PubConstClass.pblTransactionData[intChkIndex]);
                                PubConstClass.pblTransactionData[intChkIndex] = PubConstClass.pblTransactionData[intChkIndex].Replace("未", "NG");
                            }
                        }

                        if (blnIsOKFlag == false)
                        {
                            LstDrivingError.Items[intLoopCnt].UseItemStyleForSubItems = false;
                            LstDrivingError.Select();
                            LstDrivingError.Items[intLoopCnt].EnsureVisible();

                            // ＮＧ束の音色で発音
                            ((MainForm)Owner.Owner).SendBuzzerData(PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName2) - 1]);
                            TimBuzzer.Interval = 1000;
                            TimBuzzer.Enabled = true;
                        }
                        else
                        {
                            LstDrivingError.Items[intLoopCnt].UseItemStyleForSubItems = false;
                            LstDrivingError.Select();
                            LstDrivingError.Items[intLoopCnt].EnsureVisible();

                            for (var N = 0; N <= PubConstClass.DEF_CNT_FOR_DRIVING; N++)
                                LstDrivingError.Items[intLoopCnt].SubItems[N].BackColor = Color.White;
                            LstDrivingError.Items[intLoopCnt].SubItems[9].BackColor = Color.Green;
                            LstDrivingError.Items[intLoopCnt].SubItems[9].ForeColor = Color.White;
                            LstDrivingError.Items[intLoopCnt].SubItems[9].Text = "OK";

                            CommonModule.OutPutLogFile("■（CheckWorkCode：ＮＧリスト）ＯＫ束の音色で発音" + 
                                                       PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName1) - 1]);
                            // ＯＫ束の音色で発音
                            ((MainForm)Owner.Owner).SendBuzzerData(PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName1) - 1]);
                            TimBuzzer.Interval = 1000;
                            TimBuzzer.Enabled = true;

                            // ＮＧ表示リストビューからＯＫとなった行の番号を取得する
                            intOrderArrayIndex = Convert.ToInt32(LstDrivingError.Items[intLoopCnt].SubItems[0].Text) - 1;
                            string[] sFindArray;
                            int iFindArrayIndex=0;
                            for (var N = 0; N <= PubConstClass.intOrderArrayIndex; N++)
                            {
                                if (PubConstClass.pblOrderArray[N] == null)
                                {
                                    break;
                                }
                                sFindArray = PubConstClass.pblOrderArray[N].Split(',');
                                if (LstDrivingError.Items[intLoopCnt].SubItems[4].Text.Substring(LstDrivingError.Items[intLoopCnt].SubItems[4].Text.Length - 5, 4) + LstDrivingError.Items[intLoopCnt].SubItems[5].Text + LstDrivingError.Items[intLoopCnt].SubItems[7].Text == sFindArray[3] + sFindArray[4] + sFindArray[11] + "/" + sFindArray[12])
                                {
                                    iFindArrayIndex = N;
                                    break;
                                }
                            }

                            // ＮＧ束領域からデータを削除し、ＯＫ束表示領域にデータを追加する
                            ListViewItem itm;
                            string[] col = new string[12];
                            for (var N = 0; N <= PubConstClass.DEF_CNT_FOR_DRIVING; N++)
                                col[N] = LstDrivingError.Items[intLoopCnt].SubItems[N].Text;
                            // ＯＫ束データの表示
                            itm = new ListViewItem(col);
                            LstDrivingData.Items.Add(itm);
                            LstDrivingData.Items[LstDrivingData.Items.Count - 1].UseItemStyleForSubItems = false;
                            LstDrivingData.Select();
                            LstDrivingData.Items[LstDrivingData.Items.Count - 1].EnsureVisible();

                            LstDrivingData.Items[LstDrivingData.Items.Count - 1].SubItems[9].BackColor = Color.White;
                            LstDrivingData.Items[LstDrivingData.Items.Count - 1].SubItems[9].ForeColor = Color.Green;

                            // ＮＧ束データの削除
                            LstDrivingError.Items.RemoveAt(intLoopCnt);
                        }

                        // 後続の処理を行わない
                        return;
                    }
                }

                // ＯＫ束リストビューのチェック
                for (var intLoopCnt = 0; intLoopCnt <= LstDrivingData.Items.Count - 1; intLoopCnt++)
                {
                    // 作業コードのチェック
                    strArray = LstDrivingData.Items[intLoopCnt].SubItems[8].Text.Split('-');

                    if (strArray[1].Trim() == strChkData)
                    {
                        if (LstDrivingData.Items[intLoopCnt].SubItems[9].Text == "OK")
                        {
                            // 既に「OK」の場合は何もしない
                            blnIsOKFlag = true;
                            Debug.Print("■既に「OK」なので何もしない");
                            break;
                        }

                        for (var N = 0; N <= PubConstClass.DEF_CNT_FOR_DRIVING; N++)
                            LstDrivingData.Items[intLoopCnt].SubItems[N].BackColor = Color.White;
                        LstDrivingData.Items[intLoopCnt].SubItems[9].BackColor = Color.Green;
                        LstDrivingData.Items[intLoopCnt].SubItems[9].ForeColor = Color.White;
                        LstDrivingData.Items[intLoopCnt].SubItems[9].Text = "OK";
                        LstDrivingData.Items[intLoopCnt].UseItemStyleForSubItems = false;
                        LstDrivingData.Select();
                        LstDrivingData.Items[intLoopCnt].EnsureVisible();

                        CommonModule.OutPutLogFile("■（CheckWorkCode：ＯＫリスト）ＯＫ束の音色で発音" +
                                                   PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName1) - 1]);
                        // ＯＫ束の音色で発音
                        ((MainForm)Owner.Owner).SendBuzzerData(PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName1) - 1]);
                        TimBuzzer.Interval = 1000;
                        TimBuzzer.Enabled = true;

                        blnIsOKFlag = true;

                        // ＯＫ表示リストビューからＯＫとなった行の番号を取得する
                        intOrderArrayIndex = Convert.ToInt32(LstDrivingData.Items[intLoopCnt].SubItems[0].Text) - 1;
                        string[] sFindArray;
                        int iFindArrayIndex=0;
                        for (var N = 0; N <= PubConstClass.intOrderArrayIndex; N++)
                        {
                            if (PubConstClass.pblOrderArray[N]==null)
                            {
                                // データが、nullでブレイクする
                                break;
                            }
                            sFindArray = PubConstClass.pblOrderArray[N].Split(',');
                            if (LstDrivingData.Items[intLoopCnt].SubItems[4].Text.Substring(LstDrivingData.Items[intLoopCnt].SubItems[4].Text.Length - 5, 4) + LstDrivingData.Items[intLoopCnt].SubItems[5].Text + LstDrivingData.Items[intLoopCnt].SubItems[7].Text == sFindArray[3] + sFindArray[4] + sFindArray[11] + "/" + sFindArray[12])
                            {
                                iFindArrayIndex = N;
                                break;
                            }
                        }
                        break;
                    }
                }

                if (blnIsOKFlag == false)
                {
                    CommonModule.OutPutLogFile("■（CheckWorkCode）ＮＧ束の音色で発音" +
                                               PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName2) - 1]);
                    // ＮＧ束の音色で発音
                    ((MainForm)Owner.Owner).SendBuzzerData(PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName2) - 1]);
                    TimBuzzer.Interval = 1000;
                    TimBuzzer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                strMessage = "【CheckWorkCode】" + ex.Message;
                CommonModule.OutPutLogFile(strMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TxtWorkCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == ControlChars.Cr)
                {
                    // 作業コード（組合員番号）のチェック
                    CheckWorkCode(TxtWorkCode.Text);
                    return;
                }

                if (e.KeyChar == ControlChars.Back | e.KeyChar == ControlChars.Tab)
                    // 「BS」キーは対象外とする
                    return;

                if (e.KeyChar < '0' || '9' < e.KeyChar)
                    // 数字キー「0」～「9」以外はイベントをキャンセルする
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【TxtWorkCode_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TxtBCRCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == ControlChars.Cr)
                {
                    // 「Enter」キーの処理
                    // 作業コード（組合員番号）のチェック
                    CheckWorkCode(TxtBCRCode.Text);
                    return;
                }

                if (e.KeyChar == ControlChars.Back | e.KeyChar == ControlChars.Tab)
                    // 「BS」キーは対象外とする
                    return;

                if (e.KeyChar < '0' || '9' < e.KeyChar)
                    // 数字キー「0」～「9」以外はイベントをキャンセルする
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【TxtWorkCode_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// エラーメッセージ表示タイマ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TimErrorDispTime_Tick(object sender, EventArgs e)
        {
            try
            {
                intErrorCnt -= 1;
                if (intErrorCnt < 1)
                {
                    // エラータイマー停止
                    TimErrorDispTime.Enabled = false;
                    // エラーメッセージ非表示
                    LblErrorMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【TimErrorDispTime_Tick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// シグナルフォン発音停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimBuzzer_Tick(object sender, EventArgs e)
        {
            TimBuzzer.Enabled = false;
            ((MainForm)Owner.Owner).SendBuzzerStopData();
            CommonModule.OutPutLogFile("【TimBuzzer_Tick】シグナルフォン発音停止");
        }

        private short Id;                   // デバイスID
        private int Ret;                    // 戻り値
        private String szError;   // エラー文字列
        private int UpCount;                      // アップカウンタ
        private int DownCount;                    // ダウンカウンタ
        private bool bEjectOn;
        private Cdio cdio = new Cdio();


        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        private void Dio_Init()
        {
            string szDeviceName;

            try
            {
                bEjectOn = false;

                // デバイス名の設定
                if (PubConstClass.sDioName == "")
                    szDeviceName = "DIO000";
                else
                    szDeviceName = PubConstClass.sDioName;
                CommonModule.OutPutLogFile("〓デバイスＩＤ：" + PubConstClass.sDioName);

                // 初期化処理を行ない、IDを取得する
                Ret = cdio.Init(szDeviceName, out Id);

                // エラー処理
                cdio.GetErrorString(Ret, out szError);
                CommonModule.OutPutLogFile("【Dio_Init】Ret=" + Ret + ":" + szError.ToString());
                LsbRet.Items.Add("【Dio_Init】Ret=" + Ret + ":" + szError.ToString());
            }
            catch (Exception ex)
            {
                CommonModule.OutPutLogFile("【Dio_Init】" + ex.Message);
                LsbRet.Items.Add("【Dio_Init】" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        private void TriggerCheckStart()
        {
            short BitNo=0;
            short Kind;
            int Tim;

            try
            {
                // 変数を初期化する
                UpCount = 0;                             // アップカウンタ
                DownCount = 0;                           // ダウンカウンタ

                Tim = 100;                               // 100ms周期で監視
                Kind = (short)(CdioConst.DIO_TRG_RISE | CdioConst.DIO_TRG_FALL);     // アップダウン両方を監視

                Ret = cdio.NotifyTrg(Id, BitNo, Kind, Tim, this.Handle.ToInt32());
                if ((Ret != (int)CdioConst.DIO_ERR_SUCCESS))
                    LsbRet.Items.Add("【TriggerCheckStart】ｴﾗｰ：Ret=" + Ret + ":" + szError.ToString());

                // エラー処理
                cdio.GetErrorString(Ret, out szError);
                LsbRet.Items.Add("【TriggerCheckStart】Ret=" + Ret + ":" + szError.ToString());
            }
            catch (Exception ex)
            {
                CommonModule.OutPutLogFile("【TriggerCheckStart】" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BitNo"></param>
        /// <param name="bIsStart"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object CheckProcess(ref short BitNo, bool bIsStart)
        {
            int Tim;              // 100ms周期で監視
            short Kind;               // アップダウン両方を監視

            object CheckProcess = 1;

            try
            {
                Tim = 100;
                Kind = (short)(CdioConst.DIO_TRG_RISE | CdioConst.DIO_TRG_FALL);

                if (bIsStart == true)
                    // トリガ開始
                    Ret = cdio.NotifyTrg(Id, BitNo, Kind, Tim, this.Handle.ToInt32());
                else
                    // トリガ停止
                    Ret = cdio.StopNotifyTrg(Id, BitNo);

                // エラー処理
                cdio.GetErrorString(Ret, out szError);
                LsbRet.Items.Add("【CheckProcess】Ret=" + Ret + ":" + szError.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                CommonModule.OutPutLogFile("【TriggerCheckStart】" + ex.Message);
            }
            return CheckProcess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BgEjectThread_DoWork(object sender, DoWorkEventArgs e)
        {

            // 優先順位＋１
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            long ret;
            byte lastData = 0;
            Stopwatch stopWatch = new Stopwatch();

            do
            {
                stopWatch.Reset();
                stopWatch.Start();
                // ポートデータ
                ret = cdio.InpBit(Id, 0, out byte data);
                stopWatch.Stop();
                if (ret == 0)
                {
                    if (data != lastData)
                    {
                        if ((data != 0))
                        {
                            bEjectOn = true;
                            UpCount += 1;
                        }
                        else
                        {
                            DownCount += 1;
                        }
                        lastData = data;
                    }
                    //Trace.WriteLine(string.Format("DATA:{0}", data));
                }
                // Sleep
                Thread.Sleep(0);
            }
            while (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TimBgEjectThread_Tick(object sender, EventArgs e)
        {
            LblUp.Text = UpCount.ToString();
            LblDown.Text = DownCount.ToString();

            if (bEjectOn == true)
            {
                bEjectOn = false;
                // 排出ゲート開放の音色
                ((MainForm)Owner.Owner).SendBuzzerData(PubConstClass.pblToneData[Convert.ToInt32(PubConstClass.pblToneName6) - 1]);
                TimBuzzer.Interval = 1000;
                TimBuzzer.Enabled = true;
                CommonModule.OutPutLogFile("【TimBgEjectThread_Tick】排出ゲート開放音発音");
            }
        }

        /// <summary>
        /// デバッグモード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblVersion_DoubleClick(object sender, EventArgs e)
        {
            if (LblUp.Visible == false)
            {
                LblUp.Visible = true;
                LblDown.Visible = true;
                LsbRet.Visible = true;
            }
            else
            {
                LblUp.Visible = false;
                LblDown.Visible = false;
                LsbRet.Visible = false;
            }
        }

        /// <summary>
        /// 「ＯＫ束」行ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstDrivingData_DoubleClick(object sender, EventArgs e)
        {
            string[] sArray;
            try
            {
                sArray = LstDrivingData.SelectedItems[0].SubItems[8].Text.Split('-');
                TxtWorkCode.Text = sArray[1].Trim(' ');
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【LstDrivingData_DoubleClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// 「ＮＧ束」行ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstDrivingError_DoubleClick(object sender, EventArgs e)
        {
            string[] sArray;
            try
            {
                sArray = LstDrivingError.SelectedItems[0].SubItems[8].Text.Split('-');
                TxtWorkCode.Text = sArray[1].Trim(' ');
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【LstDrivingError_DoubleClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
