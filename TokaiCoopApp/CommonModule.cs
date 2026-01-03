using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TokaiCoopApp
{
    static class CommonModule
    {
        private static System.Media.SoundPlayer player = null;
        private static DotNetBarcode bc1 = new DotNetBarcode();

        /// <summary>
        /// フォルダの末尾の「\」を保証する
        /// </summary>
        /// <param name="strCheckFolder">チェック対象のフォルダ名称</param>
        /// <returns>チェック後のフォルダ名称</returns>
        /// <remarks></remarks>
        public static string IncludeTrailingPathDelimiter(string strCheckFolder)
        {
            string IncludeTrailingPathDelimiter;
            try
            {
                if (strCheckFolder.Substring(strCheckFolder.Length - 1, 1) == @"\")
                    IncludeTrailingPathDelimiter = strCheckFolder;
                else
                    IncludeTrailingPathDelimiter = strCheckFolder + @"\";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【IncludeTrailingPathDelimiter】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            return IncludeTrailingPathDelimiter;
        }

        /// <summary>
        /// 実績ログデータの書き込み処理
        /// </summary>
        /// <param name="strResultsData"></param>
        /// <remarks></remarks>
        public static void OutPutResultsLogData(string strResultsData)
        {
            string strPutMessage;

            try
            {
                DateTime dtNow = DateTime.Now;

                // 指定した書式で日付を文字列に変換する
                string strNowFormat = dtNow.ToString("yyyy/MM/dd HH:mm:ss");

                // 実績ログファイルに操作ログ内容を書き込む
                strPutMessage = strNowFormat + "：" + strResultsData;
                // 追記モードで書き込む
                using (StreamWriter sw = new StreamWriter(PubConstClass.pblDriveLogFileName, true, Encoding.Default))
                {
                    sw.WriteLine(strPutMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【OutPutResultsLogData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 通信ログデータの書き込み処理
        /// </summary>
        /// <param name="strOutSerialData"></param>
        /// <remarks></remarks>
        public static void OutPutSerialLogFile(string strOutSerialData)
        {
            string strOutPutFolder;
            string strOutPutFileName;
            string strYYYYMMDDvalue;
            string strHHMMSSvalue;
            string strPutMessage;

            lock ((PubConstClass.objSyncSeri))
            {
                try
                {
                    DateTime dtNow = DateTime.Now;

                    // 指定した書式で日付を文字列に変換する
                    string strNowFormat = dtNow.ToString("yyyy/MM/dd HH:mm:ss");

                    {
                        var withBlock = DateTime.Now;
                        strYYYYMMDDvalue = string.Format("{0:D4}{1:D2}{2:D2}", withBlock.Year, withBlock.Month, withBlock.Day);
                        strHHMMSSvalue = string.Format("{0:D2}{1:D2}{2:D2}", withBlock.Hour, withBlock.Minute, withBlock.Second);
                    }

                    // 格納フォルダ名の設定
                    strOutPutFolder = IncludeTrailingPathDelimiter(Application.StartupPath) + @"OPHISTORYLOG\";
                    // 格納ファイル名の設定
                    strOutPutFileName = "通信ログ_" + strYYYYMMDDvalue + ".LOG";

                    if (Directory.Exists(strOutPutFolder) == false)
                    {
                        // フォルダの作成
                        Directory.CreateDirectory(strOutPutFolder);
                        strPutMessage = strNowFormat + "【" + strOutPutFolder + "】フォルダを作成しました。";
                        // 追記モードで書き込む
                        using (StreamWriter sw = new StreamWriter(strOutPutFolder + strOutPutFileName, true, Encoding.Default))
                        {
                            sw.WriteLine(strPutMessage);
                        }
                    }

                    // 通信ログに送受信内容を書き込む
                    strPutMessage = strNowFormat + "：" + strOutSerialData;
                    // 追記モードで書き込む
                    using (StreamWriter sw = new StreamWriter(strOutPutFolder + strOutPutFileName, true, Encoding.Default))
                    {
                        sw.WriteLine(strPutMessage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, "【OutPutSerialLogFile】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 操作履歴ログの書込処理
        /// </summary>
        /// <param name="strOutPutData">操作履歴メッセージ</param>
        /// <remarks></remarks>
        public static void OutPutLogFile(string strOutPutData)
        {
            string strOutPutFolder;
            string strOutPutFileName;
            string strYYYYMMDDvalue;
            string strHHMMSSvalue;
            string strPutMessage;

            lock ((PubConstClass.objSyncHist))
            {
                try
                {
                    DateTime dtNow = DateTime.Now;

                    // 指定した書式で日付を文字列に変換する
                    string strNowFormat = dtNow.ToString("yyyy/MM/dd HH:mm:ss");

                    {
                        var withBlock = DateTime.Now;
                        strYYYYMMDDvalue = string.Format("{0:D4}{1:D2}{2:D2}", withBlock.Year, withBlock.Month, withBlock.Day);
                        strHHMMSSvalue = string.Format("{0:D2}{1:D2}{2:D2}", withBlock.Hour, withBlock.Minute, withBlock.Second);
                    }

                    // 格納フォルダ名の設定
                    strOutPutFolder = IncludeTrailingPathDelimiter(Application.StartupPath) + @"OPHISTORYLOG\";
                    // 格納ファイル名の設定
                    strOutPutFileName = "操作履歴ログ_" + strYYYYMMDDvalue + ".LOG";

                    if (Directory.Exists(strOutPutFolder) == false)
                    {
                        // フォルダの作成
                        Directory.CreateDirectory(strOutPutFolder);
                        strPutMessage = strNowFormat + "【" + strOutPutFolder + "】フォルダを作成しました。";
                        // 追記モードで書き込む
                        using (StreamWriter sw = new StreamWriter(strOutPutFolder + strOutPutFileName, true, Encoding.Default))
                        {
                            sw.WriteLine(strPutMessage);
                        }
                    }

                    // 操作履歴ログに操作ログ内容を書き込む
                    strPutMessage = strNowFormat + "：" + strOutPutData;
                    // 追記モードで書き込む
                    using (StreamWriter sw = new StreamWriter(strOutPutFolder + strOutPutFileName, true, Encoding.Default))
                    {
                        sw.WriteLine(strPutMessage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, "【OutPutLogFile】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// システム定義ファイルの読込処理
        /// </summary>
        /// <remarks></remarks>
        public static void GetSystemDefinition()
        {
            string strReadDataPath;
            string[] strArray;

            PubConstClass.pblShipMemoString1 = "※オリコンのラベルは必ずはがして返却して下さい。";
            PubConstClass.pblShipMemoString2 = "";
            PubConstClass.pblShipMemoString3 = "";
            PubConstClass.pblNameMemoString1 = "※このラベルは配達後に必ずはがして下さい。";
            PubConstClass.pblNameMemoString2 = "";
            PubConstClass.pblNameMemoString3 = "";

            try
            {
                strReadDataPath = IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_FILENAME;

                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        switch (strArray[0])
                        {
                            // 号機名称
                            case PubConstClass.DEF_MACHINE_NAME:
                                {
                                    PubConstClass.pblMachineName = strArray[1];
                                    break;
                                }
                            // ライン番号
                            case PubConstClass.DEF_LINE_NUMBER:
                                {
                                    PubConstClass.pblLineNumber = strArray[1];
                                    break;
                                }
                            // ディスク空き容量
                            case PubConstClass.DEF_HDD_SPACE:
                                {
                                    PubConstClass.pblHddSpace = strArray[1];
                                    break;
                                }
                            // COMポート名
                            case PubConstClass.DEF_COMPORT:
                                {
                                    PubConstClass.pblComPort = strArray[1];
                                    break;
                                }
                            // COM通信速度
                            case PubConstClass.DEF_COM_SPEED:
                                {
                                    PubConstClass.pblComSpeed = strArray[1];
                                    break;
                                }
                            // COMデータ長
                            case PubConstClass.DEF_COM_DATA_LENGTH:
                                {
                                    PubConstClass.pblComDataLength = strArray[1];
                                    break;
                                }
                            // COMパリティ有無
                            case PubConstClass.DEF_COM_IS_PARITY:
                                {
                                    PubConstClass.pblComIsParity = strArray[1];
                                    break;
                                }
                            // COMパリティ種別
                            case PubConstClass.DEF_COM_PARITY_VAR:
                                {
                                    PubConstClass.pblComParityVar = strArray[1];
                                    break;
                                }
                            // COMストップビット
                            case PubConstClass.DEF_COM_STOPBIT:
                                {
                                    PubConstClass.pblComStopBit = strArray[1];
                                    break;
                                }
                            // COM2ポート名
                            case PubConstClass.DEF_COMPORT2:
                                {
                                    PubConstClass.pblComPort2 = strArray[1];
                                    break;
                                }
                            // COM2通信速度
                            case PubConstClass.DEF_COM_SPEED2:
                                {
                                    PubConstClass.pblComSpeed2 = strArray[1];
                                    break;
                                }
                            // COM2データ長
                            case PubConstClass.DEF_COM_DATA_LENGTH2:
                                {
                                    PubConstClass.pblComDataLength2 = strArray[1];
                                    break;
                                }
                            // COM2パリティ有無
                            case PubConstClass.DEF_COM_IS_PARITY2:
                                {
                                    PubConstClass.pblComIsParity2 = strArray[1];
                                    break;
                                }
                            // COM2パリティ種別
                            case PubConstClass.DEF_COM_PARITY_VAR2:
                                {
                                    PubConstClass.pblComParityVar2 = strArray[1];
                                    break;
                                }
                            // COM2ストップビット
                            case PubConstClass.DEF_COM_STOPBIT2:
                                {
                                    PubConstClass.pblComStopBit2 = strArray[1];
                                    break;
                                }
                            // パスワード
                            case PubConstClass.DEF_PASSWORD:
                                {
                                    PubConstClass.pblPassWord = strArray[1];
                                    break;
                                }
                            // １箱最大束数
                            case PubConstClass.DEF_MAX_OF_BUNDLE:
                                {
                                    PubConstClass.pblMaxOfBundle = strArray[1];
                                    break;
                                }
                            // 自動判定モード
                            case PubConstClass.DEF_AUTO_JUDGE_MODE:
                                {
                                    PubConstClass.pblIsAutoJudge = strArray[1];
                                    break;
                                }
                            // エラー表示時間
                            case PubConstClass.DEF_ERROR_DISP_TIME:
                                {
                                    PubConstClass.pblErrorDispTime = strArray[1];
                                    break;
                                }
                            // Ａ４レーザープリンタ名称
                            case PubConstClass.DEF_A4_PRINTER_NAME:
                                {
                                    PubConstClass.pblReportPrinterName = strArray[1];
                                    break;
                                }
                            // ジャーナルプリンタ名称
                            case PubConstClass.DEF_JR_PRINTER_NAME:
                                {
                                    PubConstClass.pblJournalPrinterName = strArray[1];
                                    break;
                                }
                            // 丁合指示データ読込ドライブ
                            case PubConstClass.DEF_CHOAI_FOLDER:
                                {
                                    PubConstClass.pblChoaiFolder = strArray[1];
                                    break;
                                }
                            // 制御データ格納フォルダ
                            case PubConstClass.DEF_CONTROL_FOLDER:
                                {
                                    PubConstClass.pblControlFolder = strArray[1];
                                    break;
                                }
                            // 生産ログ格納フォルダ
                            case PubConstClass.DEF_SEISAN_FOLDER:
                                {
                                    PubConstClass.pblSeisanFolder = strArray[1];
                                    break;
                                }
                            // 最終生産ログ格納フォルダ
                            case PubConstClass.DEF_FINAL_SEISAN_FOLDER:
                                {
                                    PubConstClass.pblFinalSeisanFolder = strArray[1];
                                    break;
                                }
                            // イベントログ格納フォルダ
                            case PubConstClass.DEF_EVENT_FOLDER:
                                {
                                    PubConstClass.pblEventFolder = strArray[1];
                                    break;
                                }
                            // 最終イベントログ格納フォルダ
                            case PubConstClass.DEF_FINAL_EVENT_FOLDER:
                                {
                                    PubConstClass.pblFinalEventFolder = strArray[1];
                                    break;
                                }
                            // 帳票出力格納フォルダ
                            case PubConstClass.DEF_FORM_OUTPUT_FOLDER:
                                {
                                    PubConstClass.pblFormOutPutFolder = strArray[1];
                                    break;
                                }
                            // 内部実績ログ格納フォルダ
                            case PubConstClass.DEF_INTERNAL_TRAN_FOLDER:
                                {
                                    PubConstClass.pblInternalTranFolder = strArray[1];
                                    break;
                                }
                            // 稼働管理PC集計フォルダ
                            case PubConstClass.DEF_OPERATION_TOTAL_FOLDER:
                                {
                                    PubConstClass.pblOperationPcTotallingFolder = strArray[1];
                                    break;
                                }
                            // 稼働管理PC集計フォルダ
                            case PubConstClass.DEF_OPERATION_EVENT_FOLDER:
                                {
                                    PubConstClass.pblOperationPcEventFolder = strArray[1];
                                    break;
                                }
                            // ブザー音色１
                            case PubConstClass.DEF_TONE_NAME1:
                                {
                                    PubConstClass.pblToneName1 = strArray[1];
                                    break;
                                }
                            // ブザー音色２
                            case PubConstClass.DEF_TONE_NAME2:
                                {
                                    PubConstClass.pblToneName2 = strArray[1];
                                    break;
                                }
                            // ブザー音色３
                            case PubConstClass.DEF_TONE_NAME3:
                                {
                                    PubConstClass.pblToneName3 = strArray[1];
                                    break;
                                }
                            // ブザー音色４
                            case PubConstClass.DEF_TONE_NAME4:
                                {
                                    PubConstClass.pblToneName4 = strArray[1];
                                    break;
                                }
                            // ブザー音色５
                            case PubConstClass.DEF_TONE_NAME5:
                                {
                                    PubConstClass.pblToneName5 = strArray[1];
                                    break;
                                }
                            // ブザー音色６
                            case PubConstClass.DEF_TONE_NAME6:
                                {
                                    PubConstClass.pblToneName6 = strArray[1];
                                    break;
                                }
                            // ブザー音色７
                            case PubConstClass.DEF_TONE_NAME7:
                                {
                                    PubConstClass.pblToneName7 = strArray[1];
                                    break;
                                }
                            // ブザー音色８
                            case PubConstClass.DEF_TONE_NAME8:
                                {
                                    PubConstClass.pblToneName8 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間１
                            case PubConstClass.DEF_TONE_TIME1:
                                {
                                    PubConstClass.pblToneTime1 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間２
                            case PubConstClass.DEF_TONE_TIME2:
                                {
                                    PubConstClass.pblToneTime2 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間３
                            case PubConstClass.DEF_TONE_TIME3:
                                {
                                    PubConstClass.pblToneTime3 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間４
                            case PubConstClass.DEF_TONE_TIME4:
                                {
                                    PubConstClass.pblToneTime4 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間５
                            case PubConstClass.DEF_TONE_TIME5:
                                {
                                    PubConstClass.pblToneTime5 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間６
                            case PubConstClass.DEF_TONE_TIME6:
                                {
                                    PubConstClass.pblToneTime6 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間７
                            case PubConstClass.DEF_TONE_TIME7:
                                {
                                    PubConstClass.pblToneTime7 = strArray[1];
                                    break;
                                }
                            // ブザー発音時間８
                            case PubConstClass.DEF_TONE_TIME8:
                                {
                                    PubConstClass.pblToneTime8 = strArray[1];
                                    break;
                                }
                            // ログ保存期間
                            case PubConstClass.DEF_LOGSAVE_MONTH:
                                {
                                    PubConstClass.pblSaveLogMonth = strArray[1];
                                    break;
                                }
                            // 運転画面横表示座標
                            case PubConstClass.DEF_DRIVING_XPOS_DISP:
                                {
                                    PubConstClass.intXPosition = Convert.ToInt32(strArray[1]);
                                    break;
                                }
                            // CONTEC_DIO_0808LY_ID
                            case PubConstClass.DEF_CONTEC_DIO_0808LY_ID:
                                {
                                    PubConstClass.sDioName = strArray[1];
                                    break;
                                }

                            case PubConstClass.DEF_DUMMY_CREATE_COUNT1:
                                PubConstClass.iDummyCreateCount1 = int.Parse(strArray[1]);
                                break;

                            case PubConstClass.DEF_DUMMY_CREATE_COUNT2:
                                PubConstClass.iDummyCreateCount2 = int.Parse(strArray[1]);
                                break;

                            case PubConstClass.DEF_DUMMY_CREATE_COUNT3:
                                PubConstClass.iDummyCreateCount3 = int.Parse(strArray[1]);
                                break;

                            case PubConstClass.DEF_DUMMY_CREATE_COUNT4:
                                PubConstClass.iDummyCreateCount4 = int.Parse(strArray[1]);
                                break;

                            case PubConstClass.DEF_THICK_BOOKLET_USE_FEEDER:
                                PubConstClass.iThickBookletUseFeeder = int.Parse(strArray[1]);
                                break;

                            case PubConstClass.DEF_CARD_BOARD_USE_FEEDER:
                                PubConstClass.iCardboard = int.Parse(strArray[1]);
                                break;

                            case PubConstClass.DEF_NUMBER_OF_BOOKS:
                                PubConstClass.iNumberOfBooks = int.Parse(strArray[1]);
                                break;

                            default:
                                {
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【getSystemDefinition】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// システム定義ファイルの書込処理
        /// </summary>
        /// <remarks></remarks>
        public static void PutSystemDefinition()
        {
            string strPutDataPath;

            try
            {
                strPutDataPath = IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_FILENAME;

                // 上書モードで書き込む
                using (StreamWriter sw = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {

                    // 号機名称
                    sw.WriteLine(PubConstClass.DEF_MACHINE_NAME + "," + PubConstClass.pblMachineName);
                    // ライン番号
                    sw.WriteLine(PubConstClass.DEF_LINE_NUMBER + "," + PubConstClass.pblLineNumber);
                    // ディスク空き容量
                    sw.WriteLine(PubConstClass.DEF_HDD_SPACE + "," + PubConstClass.pblHddSpace);

                    // COMポート名
                    sw.WriteLine(PubConstClass.DEF_COMPORT + "," + PubConstClass.pblComPort);
                    // COM通信速度
                    sw.WriteLine(PubConstClass.DEF_COM_SPEED + "," + PubConstClass.pblComSpeed);
                    // COMデータ長
                    sw.WriteLine(PubConstClass.DEF_COM_DATA_LENGTH + "," + PubConstClass.pblComDataLength);
                    // COMパリティ有無
                    sw.WriteLine(PubConstClass.DEF_COM_IS_PARITY + "," + PubConstClass.pblComIsParity);
                    // COMパリティ種別
                    sw.WriteLine(PubConstClass.DEF_COM_PARITY_VAR + "," + PubConstClass.pblComParityVar);
                    // COMストップビット
                    sw.WriteLine(PubConstClass.DEF_COM_STOPBIT + "," + PubConstClass.pblComStopBit);

                    // COM2ポート名
                    sw.WriteLine(PubConstClass.DEF_COMPORT2 + "," + PubConstClass.pblComPort2);
                    // COM2通信速度
                    sw.WriteLine(PubConstClass.DEF_COM_SPEED2 + "," + PubConstClass.pblComSpeed2);
                    // COM2データ長
                    sw.WriteLine(PubConstClass.DEF_COM_DATA_LENGTH2 + "," + PubConstClass.pblComDataLength2);
                    // COM2パリティ有無
                    sw.WriteLine(PubConstClass.DEF_COM_IS_PARITY2 + "," + PubConstClass.pblComIsParity2);
                    // COM2パリティ種別
                    sw.WriteLine(PubConstClass.DEF_COM_PARITY_VAR2 + "," + PubConstClass.pblComParityVar2);
                    // COM2ストップビット
                    sw.WriteLine(PubConstClass.DEF_COM_STOPBIT2 + "," + PubConstClass.pblComStopBit2);

                    // パスワード
                    sw.WriteLine(PubConstClass.DEF_PASSWORD + "," + PubConstClass.pblPassWord);

                    // １箱最大束数
                    sw.WriteLine(PubConstClass.DEF_MAX_OF_BUNDLE + "," + PubConstClass.pblMaxOfBundle);

                    // 自動判定モード
                    sw.WriteLine(PubConstClass.DEF_AUTO_JUDGE_MODE + "," + PubConstClass.pblIsAutoJudge);

                    // エラー表示時間
                    sw.WriteLine(PubConstClass.DEF_ERROR_DISP_TIME + "," + PubConstClass.pblErrorDispTime);

                    // Ａ４レーザープリンタ名称
                    sw.WriteLine(PubConstClass.DEF_A4_PRINTER_NAME + "," + PubConstClass.pblReportPrinterName);
                    // ジャーナルプリンタ名称
                    sw.WriteLine(PubConstClass.DEF_JR_PRINTER_NAME + "," + PubConstClass.pblJournalPrinterName);

                    // 丁合指示データ読込ドライブ
                    sw.WriteLine(PubConstClass.DEF_CHOAI_FOLDER + "," + PubConstClass.pblChoaiFolder);

                    // 制御データ格納フォルダ
                    sw.WriteLine(PubConstClass.DEF_CONTROL_FOLDER + "," + PubConstClass.pblControlFolder);

                    // 生産ログ格納フォルダ
                    sw.WriteLine(PubConstClass.DEF_SEISAN_FOLDER + "," + PubConstClass.pblSeisanFolder);

                    // 最終生産ログ格納フォルダ
                    sw.WriteLine(PubConstClass.DEF_FINAL_SEISAN_FOLDER + "," + PubConstClass.pblFinalSeisanFolder);

                    // イベントログ格納フォルダ
                    sw.WriteLine(PubConstClass.DEF_EVENT_FOLDER + "," + PubConstClass.pblEventFolder);

                    // 最終イベントログ格納フォルダ
                    sw.WriteLine(PubConstClass.DEF_FINAL_EVENT_FOLDER + "," + PubConstClass.pblFinalEventFolder);

                    // 帳票出力格納フォルダ
                    sw.WriteLine(PubConstClass.DEF_FORM_OUTPUT_FOLDER + "," + PubConstClass.pblFormOutPutFolder);

                    // 稼働管理PC集計フォルダ（生産ログ）
                    sw.WriteLine(PubConstClass.DEF_OPERATION_TOTAL_FOLDER + "," + PubConstClass.pblOperationPcTotallingFolder);

                    // 稼働管理PC集計フォルダ（イベントログ）
                    sw.WriteLine(PubConstClass.DEF_OPERATION_EVENT_FOLDER + "," + PubConstClass.pblOperationPcEventFolder);

                    // 内部実績ログ格納フォルダ
                    sw.WriteLine(PubConstClass.DEF_INTERNAL_TRAN_FOLDER + "," + PubConstClass.pblInternalTranFolder);

                    // ブザー音色１
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME1 + "," + PubConstClass.pblToneName1);

                    // ブザー音色２
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME2 + "," + PubConstClass.pblToneName2);

                    // ブザー音色３
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME3 + "," + PubConstClass.pblToneName3);

                    // ブザー音色４
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME4 + "," + PubConstClass.pblToneName4);

                    // ブザー音色５
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME5 + "," + PubConstClass.pblToneName5);

                    // ブザー音色６
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME6 + "," + PubConstClass.pblToneName6);

                    // ブザー音色７
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME7 + "," + PubConstClass.pblToneName7);

                    // ブザー音色８
                    sw.WriteLine(PubConstClass.DEF_TONE_NAME8 + "," + PubConstClass.pblToneName8);

                    // ブザー発音時間１
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME1 + "," + PubConstClass.pblToneTime1);

                    // ブザー発音時間２
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME2 + "," + PubConstClass.pblToneTime2);

                    // ブザー発音時間３
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME3 + "," + PubConstClass.pblToneTime3);

                    // ブザー発音時間４
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME4 + "," + PubConstClass.pblToneTime4);

                    // ブザー発音時間５
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME5 + "," + PubConstClass.pblToneTime5);

                    // ブザー発音時間６
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME6 + "," + PubConstClass.pblToneTime6);

                    // ブザー発音時間７
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME7 + "," + PubConstClass.pblToneTime7);

                    // ブザー発音時間８
                    sw.WriteLine(PubConstClass.DEF_TONE_TIME8 + "," + PubConstClass.pblToneTime8);

                    // ログ保存期間
                    sw.WriteLine(PubConstClass.DEF_LOGSAVE_MONTH + "," + PubConstClass.pblSaveLogMonth);

                    // 運転画面表示位置（Ｘ座標）
                    sw.WriteLine(PubConstClass.DEF_DRIVING_XPOS_DISP + "," + PubConstClass.intXPosition);

                    // CONTEC DIOｰ0808LYのデバイスID
                    sw.WriteLine(PubConstClass.DEF_CONTEC_DIO_0808LY_ID + "," + PubConstClass.sDioName);

                    // 作成ダミー束数の設定
                    sw.WriteLine(PubConstClass.DEF_DUMMY_CREATE_COUNT1 + "," + PubConstClass.iDummyCreateCount1.ToString());
                    sw.WriteLine(PubConstClass.DEF_DUMMY_CREATE_COUNT2 + "," + PubConstClass.iDummyCreateCount2.ToString());
                    sw.WriteLine(PubConstClass.DEF_DUMMY_CREATE_COUNT3 + "," + PubConstClass.iDummyCreateCount3.ToString());
                    sw.WriteLine(PubConstClass.DEF_DUMMY_CREATE_COUNT4 + "," + PubConstClass.iDummyCreateCount4.ToString());

                    // 厚物冊子使用フィーダー
                    sw.WriteLine(PubConstClass.DEF_THICK_BOOKLET_USE_FEEDER + "," + PubConstClass.iThickBookletUseFeeder.ToString());
                    // 厚物冊子（N）冊以上
                    sw.WriteLine(PubConstClass.DEF_NUMBER_OF_BOOKS + "," + PubConstClass.iNumberOfBooks.ToString());
                    // ボール紙使用フィーダー
                    sw.WriteLine(PubConstClass.DEF_CARD_BOARD_USE_FEEDER + "," + PubConstClass.iCardboard.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【putSystemDefinition】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ディスクの空き領域をチェック
        /// </summary>
        /// <remarks></remarks>
        public static void CheckAvairableFreeSpace()
        {
            long lngAvailableValue;
            string strMessage;

            try
            {
                DriveInfo drive = new DriveInfo(PubConstClass.pblInternalTranFolder.Substring(0, 1));

                if (drive.IsReady == true)
                {
                    lngAvailableValue = drive.AvailableFreeSpace;

                    if ((lngAvailableValue / (double)1024 / 1024 / 1024) < Convert.ToDouble(PubConstClass.pblHddSpace))
                    {
                        strMessage = "ドライブ「" + PubConstClass.pblInternalTranFolder.Substring(0, 1) + "」の空き領域（" + 
                            (lngAvailableValue / (double)1024 / 1024 / 1024).ToString("F1") + " GB）が、" + PubConstClass.pblHddSpace + " GB より少なくなっています。";
                        // MsgBox("空き領域：" & (lngAvailableValue / 1024 / 1024 / 1024).ToString & " GB")                        
                        MessageBox.Show(strMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【CheckAvairableFreeSpace】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 指定した月より古い下記のファイルを削除する
        /// （１）内部実績ログ格納フォルダ
        /// （２）履歴履歴ログファイル
        /// </summary>
        /// <param name="intMinusMonth"></param>
        /// <remarks></remarks>
        public static void DeleteLogFiles(int intMinusMonth)
        {
            string[] strArray;
            string strCompDate;

            try
            {
                // 現在の日付（年月日）を求める
                DateTime dtCurrent = DateTime.Now;

                // 現在日付から指定月を減算
                DateTime dtPassDate = dtCurrent.AddMonths(-(intMinusMonth));

                if (Directory.Exists(IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder)) == false)
                {
                    OutPutLogFile("「" + IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + "」フォルダが存在しません。");
                }
                else
                {
                    // 削除対象ファイル（稼動ログ）の取得
                    foreach (string strDeleteFolder in Directory.GetDirectories(IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder), 
                                                                                                             "*", SearchOption.TopDirectoryOnly))
                    {
                        // OutPutLogFile("ログファイル一覧取得：" & strDeleteFile)
                        strArray = strDeleteFolder.Split('\\');
                        // 「YYYYMMDD」部分を切り出す                    
                        strCompDate = strArray[strArray.Length - 1];
                        if (string.Compare(strCompDate, dtPassDate.ToString("yyyyMMdd")) < 0)
                        {
                            // ファイルを削除する
                            Directory.Delete(strDeleteFolder, true);
                            OutPutLogFile("【稼動ログ】削除対象ファイル（" + strDeleteFolder + "）を削除しました。");
                        }
                    }
                }

                // 削除対象ファイル（操作履歴ログ）の取得
                foreach (string strDeleteFile in Directory.GetFiles(IncludeTrailingPathDelimiter(Application.StartupPath) + 
                                                                    @"OPHISTORYLOG\", "*.LOG", SearchOption.AllDirectories))
                {

                    // OutPutLogFile("ログファイル一覧取得：" & strDeleteFile)
                    strArray = strDeleteFile.Split('\\');
                    // 「YYYYMMDD」部分を切り出す
                    strCompDate = strArray[strArray.Length - 1].Substring(strArray[strArray.Length - 1].Length - 12, 8);
                    if (string.Compare(strCompDate, dtPassDate.ToString("yyyyMMdd")) < 0)
                    {
                        // ファイルを削除する
                        File.Delete(strDeleteFile);
                        OutPutLogFile("【操作履歴ログ】削除対象ファイル（" + strDeleteFile + "）を削除しました。");
                    }
                }

                OutPutLogFile("削除処理が完了しました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DeleteLogFiles】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// バックアップフォルダに実績ログをコピーする
        /// </summary>
        /// <remarks></remarks>
        public static void CopyResultsLogData1()
        {
            try
            {
                // コピー元ファイルの存在チェック
                if (File.Exists(PubConstClass.pblDriveLogFileName) == false)
                {
                    OutPutLogFile("【CopyResultsLogData1】コピー元ファイル無し：" + PubConstClass.pblDriveLogFileName);
                    return;
                }

                if (File.Exists(PubConstClass.pblBackUpLogFileName1) == false)
                {
                }

                // 上書きコピー
                File.Copy(PubConstClass.pblDriveLogFileName, PubConstClass.pblBackUpLogFileName1, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【CopyResultsLogData1】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 帳票出力フォルダの存在チェックと作成
        /// </summary>
        public static void CreateOutPutFolder()
        {
            try
            {
                string sOutPutFolder;

                // 吊札
                sOutPutFolder = "";
                sOutPutFolder += IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
                sOutPutFolder += "吊札" + @"\";
                sOutPutFolder += PubConstClass.DEF_OPERATING_LOCATION;
                if (Directory.Exists(sOutPutFolder) == false)
                {
                    Directory.CreateDirectory(sOutPutFolder);
                    OutPutLogFile("【" + sOutPutFolder + "】フォルダを作成しました。");
                }
              
                // カゴ車数一覧（デポ毎）
                sOutPutFolder = "";
                sOutPutFolder += IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
                sOutPutFolder += PubConstClass.LIST_NUMBER_CARS + @"\";
                sOutPutFolder += PubConstClass.DEF_OPERATING_LOCATION;
                if (Directory.Exists(sOutPutFolder) == false)
                {
                    Directory.CreateDirectory(sOutPutFolder);
                    OutPutLogFile("【" + sOutPutFolder + "】フォルダを作成しました。");
                }

                // 処理件数一覧（生協毎）
                sOutPutFolder = "";
                sOutPutFolder += IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
                sOutPutFolder += PubConstClass.LIST_PROCESSED_ITEMS + @"\";
                sOutPutFolder += PubConstClass.DEF_OPERATING_LOCATION;
                if (Directory.Exists(sOutPutFolder) == false)
                {
                    Directory.CreateDirectory(sOutPutFolder);
                    OutPutLogFile("【" + sOutPutFolder + "】フォルダを作成しました。");
                }

                // 出荷確認表
                sOutPutFolder = "";
                sOutPutFolder += IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
                sOutPutFolder += PubConstClass.SHIPMENT_CONFIRMATION_TABLE + @"\";
                sOutPutFolder += PubConstClass.DEF_OPERATING_LOCATION;
                if (Directory.Exists(sOutPutFolder) == false)
                {
                    Directory.CreateDirectory(sOutPutFolder);
                    OutPutLogFile("【" + sOutPutFolder + "】フォルダを作成しました。");
                }

                // 帳票枚数確認表
                sOutPutFolder = "";
                sOutPutFolder += IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
                sOutPutFolder += PubConstClass.NUMBER_FORMS_CONFIRMATION_TABLE + @"\";
                sOutPutFolder += PubConstClass.DEF_OPERATING_LOCATION;
                if (Directory.Exists(sOutPutFolder) == false)
                {
                    Directory.CreateDirectory(sOutPutFolder);
                    OutPutLogFile("【" + sOutPutFolder + "】フォルダを作成しました。");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【CreateOutPutFolder】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static int intSeekRecord;

        /// <summary>
        /// 組合員コードから単協コード、センターコード、コースコードを取得する
        /// </summary>
        /// <param name="strUnionMemberCd"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetTankyoCenterCourseInfomation(string strUnionMemberCd)
        {
            string[] strArray;
            string strTankyoCenter;
            string[] strTankyoCenterInfo;

            string GetTankyoCenterCourseInfomation = " , , ";
            strTankyoCenter = " , ";

            try
            {
                for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intCollatingIndex - 1; intLoopCnt++)
                {
                    strArray = PubConstClass.pblCollatingData[intLoopCnt].Split(',');
                    // 組合員コードのチェック
                    if (strUnionMemberCd == strArray[13])
                    {
                        for (var intInfoLoopCnt = 0; intInfoLoopCnt <= PubConstClass.intMasterOfOrgCnt - 1; intInfoLoopCnt++)
                        {
                            //if (strArray[3] + strArray[21] == PubConstClass.pblMasterOfOrg[intInfoLoopCnt, 0])
                            if (strArray[2] + strArray[3] == PubConstClass.pblMasterOfOrg[intInfoLoopCnt, 0])
                            {
                                strTankyoCenter = PubConstClass.pblMasterOfOrg[intInfoLoopCnt, 1];
                                intSeekRecord = intLoopCnt;
                                break;
                            }
                        }
                        strTankyoCenterInfo = strTankyoCenter.Split(',');
                        // 単協名(ｺｰﾄﾞ),ｾﾝﾀｰ名(ｾﾝﾀｰｺｰﾄﾞ),ｺｰｽｺｰﾄﾞ
                        //GetTankyoCenterCourseInfomation = strTankyoCenterInfo[0] + "(" + strArray[3] + ")," + 
                        //                                  strTankyoCenterInfo[1] + "(" + strArray[21] + ")," + strArray[8].Substring(0, 4);
                        GetTankyoCenterCourseInfomation = strTankyoCenterInfo[0] + "(" + strArray[2] + ")," +
                                                          strTankyoCenterInfo[1] + "(" + strArray[3] + ")," + strArray[6];
                        break;
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetTankyoCenterCourseInfomation】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return GetTankyoCenterCourseInfomation;
        }

        /// <summary>
        /// 配達日から配達曜日を返す
        /// </summary>
        /// <param name="strDeliveryDay"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetDeliveryWeek(string sDistributionDay)
        {
            string GetDeliveryWeek = "NNNN";

            if (PubConstClass.dicDistributionDay.ContainsKey(sDistributionDay))
            {
                GetDeliveryWeek = PubConstClass.dicDistributionDay[sDistributionDay];
            }
            return GetDeliveryWeek;
        }

        /// <summary>
        /// 最終イベントログから稼働時間と停止時間の情報（ファイル）を取得する
        /// </summary>
        /// <param name="strYYYYMMDD"></param>
        /// <remarks></remarks>
        public static void GetRunAndStopTime(string strYYYYMMDD, string strEventLogFileName)
        {
            string strReadData;
            string[] strArray;

            try
            {
                // 稼働時間ログファイルの設定
                string strRunTimeLogPath;
                strRunTimeLogPath = IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + "稼働時間ログ.txt";

                // 停止時間ログファイルの設定
                string strStopTimeLogPath;
                strStopTimeLogPath = IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + "停止時間ログ.txt";

                // チョコ停（５分以内）時間ログファイルの設定
                string strAlarmStopTimeLogPath;
                strAlarmStopTimeLogPath = IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + "アラーム停止時間ログ.txt";

                // 最終イベントログファイルの設定
                string strEventLogPath;
                strEventLogPath = IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + strEventLogFileName;

                string strRunFrom = "";
                string strStopFrom = "";
                string strAlarmTime = "";
                string strMaxStopTime = "00:00:00";
                bool blnIsOmitFlag;

                PubConstClass.strStartTime = "";

                TimeSpan tsRun = new TimeSpan();
                TimeSpan tsStop = new TimeSpan();

                for (var intLoopCnt = 0; intLoopCnt <= 23; intLoopCnt++)
                {
                    PubConstClass.tsKadouTime[intLoopCnt] = new TimeSpan(0, 0, 0);
                    PubConstClass.tsTeisiTime[intLoopCnt] = new TimeSpan(0, 0, 0);
                }
                PubConstClass.tsStop5MinUnder = new TimeSpan(0, 0, 0);
                PubConstClass.tsStop5MinOver = new TimeSpan(0, 0, 0);

                // 稼動ログ格納
                using (StreamWriter swRun = new StreamWriter(strRunTimeLogPath, false, Encoding.Default))
                {
                    // 停止ログ格納
                    using (StreamWriter swStop = new StreamWriter(strStopTimeLogPath, false, Encoding.Default))
                    {
                        // チョコ停ログ格納
                        using (StreamWriter swAlarmStop = new StreamWriter(strAlarmStopTimeLogPath, false, Encoding.Default))
                        {
                            using (StreamReader sr = new StreamReader(strEventLogPath, Encoding.Default))
                            {
                                while (!sr.EndOfStream)
                                {
                                    strReadData = sr.ReadLine();
                                    // OutPutLogFile("読取り内容：" & strReadData)
                                    if (strReadData != "")
                                    {
                                        strArray = strReadData.Split(',');
                                        if (strArray[3] == "-0002" || strArray[3] == "-0004")
                                        {
                                            //commonModule.OutPutLogFile("【strReadData】" + strReadData);
                                            // 「-0002：START LINE」の処理
                                            // 「-0004：START PRODUCTION」の処理
                                            strRunFrom = strArray[0] + "," + strArray[1] + ",";
                                            // 稼働開始時間を格納する
                                            if (PubConstClass.strStartTime == "")
                                            {
                                                PubConstClass.strStartTime = strArray[0] + " " + strArray[1];
                                            }
                                            // アラーム停止時間の算出       
                                            if (strAlarmTime != "")
                                            {
                                                DateTime dtArm1 = new DateTime(Convert.ToInt32(strAlarmTime.Substring(0, 4)), 
                                                                               Convert.ToInt32(strAlarmTime.Substring(4, 2)), 
                                                                               Convert.ToInt32(strAlarmTime.Substring(6, 2)), 
                                                                               Convert.ToInt32(strAlarmTime.Substring(9, 2)), 
                                                                               Convert.ToInt32(strAlarmTime.Substring(11, 2)), 
                                                                               Convert.ToInt32(strAlarmTime.Substring(13, 2)));

                                                DateTime dtArm2 = new DateTime(Convert.ToInt32(strRunFrom.Substring(0, 4)), 
                                                                               Convert.ToInt32(strRunFrom.Substring(4, 2)), 
                                                                               Convert.ToInt32(strRunFrom.Substring(6, 2)), 
                                                                               Convert.ToInt32(strRunFrom.Substring(9, 2)), 
                                                                               Convert.ToInt32(strRunFrom.Substring(11, 2)), 
                                                                               Convert.ToInt32(strRunFrom.Substring(13, 2)));
                                                // 停止時間を求める
                                                TimeSpan tsArm0 = dtArm2 - dtArm1;
                                                // 「Alarm List」の処理
                                                swAlarmStop.WriteLine(strAlarmTime + tsArm0.ToString());
                                                strAlarmTime = "";
                                            }

                                            if (strStopFrom != "")
                                            {
                                                DateTime dt1 = new DateTime(Convert.ToInt32(strStopFrom.Substring(0, 4)), 
                                                                            Convert.ToInt32(strStopFrom.Substring(4, 2)), 
                                                                            Convert.ToInt32(strStopFrom.Substring(6, 2)), 
                                                                            Convert.ToInt32(strStopFrom.Substring(9, 2)), 
                                                                            Convert.ToInt32(strStopFrom.Substring(11, 2)), 
                                                                            Convert.ToInt32(strStopFrom.Substring(13, 2)));

                                                DateTime dt2 = new DateTime(Convert.ToInt32(strStopFrom.Substring(0, 4)), 
                                                                            Convert.ToInt32(strStopFrom.Substring(4, 2)), 
                                                                            Convert.ToInt32(strStopFrom.Substring(6, 2)), 
                                                                            Convert.ToInt32(strArray[1].Substring(0, 2)), 
                                                                            Convert.ToInt32(strArray[1].Substring(2, 2)), 
                                                                            Convert.ToInt32(strArray[1].Substring(4, 2)));
                                                // 停止時間を求める
                                                TimeSpan ts0 = dt2 - dt1;
                                                // 最大停止時間を求める
                                                if (string.Compare(strMaxStopTime, ts0.ToString()) < 0)
                                                    strMaxStopTime = ts0.ToString();
                                                // 停止時間の積算
                                                tsStop += ts0;
                                                swStop.WriteLine(strStopFrom + strArray[1] + "," + ts0.ToString());

                                                if (strStopFrom.Substring(9, 2) == strArray[1].Substring(0, 2))
                                                {
                                                    // 時間帯毎に格納
                                                    PubConstClass.tsTeisiTime[Convert.ToInt32(strStopFrom.Substring(9, 2))] += ts0;
                                                }
                                                else
                                                {
                                                    DateTime dtSplit1 = new DateTime(int.Parse(strStopFrom.Substring(0, 4)),
                                                                                     int.Parse(strStopFrom.Substring(4, 2)),
                                                                                     int.Parse(strStopFrom.Substring(6, 2)),
                                                                                     int.Parse(strStopFrom.Substring(9, 2)),
                                                                                     int.Parse(strStopFrom.Substring(11, 2)),
                                                                                     int.Parse(strStopFrom.Substring(13, 2)));

                                                    DateTime dtSplit2 = new DateTime(int.Parse(strStopFrom.Substring(0, 4)),
                                                                                     int.Parse(strStopFrom.Substring(4, 2)),
                                                                                     int.Parse(strStopFrom.Substring(6, 2)),
                                                                                     int.Parse(strArray[1].Substring(0, 2)),
                                                                                     0,
                                                                                     0);
                                                    TimeSpan tsSplit0 = dtSplit2 - dtSplit1;
                                                    // 時間帯毎に格納
                                                    PubConstClass.tsTeisiTime[Convert.ToInt32(strStopFrom.Substring(9, 2))] += tsSplit0;

                                                    DateTime dtSplit3 = new DateTime(int.Parse(strStopFrom.Substring(0, 4)),
                                                                                     int.Parse(strStopFrom.Substring(4, 2)),
                                                                                     int.Parse(strStopFrom.Substring(6, 2)),
                                                                                     int.Parse(strArray[1].Substring(0, 2)),
                                                                                     0,
                                                                                     0);

                                                    DateTime dtSplit4 = new DateTime(int.Parse(strStopFrom.Substring(0, 4)),
                                                                                     int.Parse(strStopFrom.Substring(4, 2)),
                                                                                     int.Parse(strStopFrom.Substring(6, 2)),
                                                                                     int.Parse(strArray[1].Substring(0, 2)),
                                                                                     int.Parse(strArray[1].Substring(2, 2)),
                                                                                     int.Parse(strArray[1].Substring(4, 2)));
                                                    TimeSpan tsSplit1 = dtSplit4 - dtSplit3;
                                                    // 時間帯毎に格納
                                                    PubConstClass.tsTeisiTime[int.Parse(strArray[1].Substring(0, 2))] += tsSplit1;
                                                }

                                                // チョコ停のチェック
                                                if (ts0.TotalMinutes >= 5)
                                                {
                                                    // 再調整（５分以上）
                                                    PubConstClass.tsStop5MinOver += ts0;
                                                }
                                                else
                                                {
                                                    // チョコ停（５分以内）
                                                    PubConstClass.tsStop5MinUnder += ts0;
                                                }
                                                strStopFrom = "";
                                            }
                                        }
                                        else if (strArray[3] == "-0001" || strArray[3] == "-0005")
                                        {
                                            //commonModule.OutPutLogFile("【strReadData】" + strReadData);
                                            // 「-0001：STOP LINE」の処理
                                            // 「-0005：STOP PRODUCTION」の処理
                                            strStopFrom = strArray[0] + "," + strArray[1] + ",";
                                            if (strRunFrom != "")
                                            {
                                                DateTime dt1 = new DateTime(Convert.ToInt32(strRunFrom.Substring(0, 4)), 
                                                                            Convert.ToInt32(strRunFrom.Substring(4, 2)), 
                                                                            Convert.ToInt32(strRunFrom.Substring(6, 2)), 
                                                                            Convert.ToInt32(strRunFrom.Substring(9, 2)), 
                                                                            Convert.ToInt32(strRunFrom.Substring(11, 2)), 
                                                                            Convert.ToInt32(strRunFrom.Substring(13, 2)));

                                                DateTime dt2 = new DateTime(Convert.ToInt32(strRunFrom.Substring(0, 4)), 
                                                                            Convert.ToInt32(strRunFrom.Substring(4, 2)), 
                                                                            Convert.ToInt32(strRunFrom.Substring(6, 2)), 
                                                                            Convert.ToInt32(strArray[1].Substring(0, 2)), 
                                                                            Convert.ToInt32(strArray[1].Substring(2, 2)), 
                                                                            Convert.ToInt32(strArray[1].Substring(4, 2)));
                                                // 稼働時間を求める
                                                TimeSpan ts0 = dt2 - dt1;
                                                // 最大可動時間を求める
                                                if (string.Compare(PubConstClass.strMaxRunTime, ts0.ToString()) < 0)
                                                {
                                                    PubConstClass.strMaxRunTime = ts0.ToString();
                                                    PubConstClass.strMaxRunFromTime = strRunFrom.Substring(9, 6);
                                                    PubConstClass.strMaxRunToTime = strArray[1];
                                                }
                                                // 稼働時間の積算
                                                tsRun += ts0;
                                                swRun.WriteLine(strRunFrom + strArray[1] + "," + ts0.ToString());

                                                if (strRunFrom.Substring(9, 2) == strArray[1].Substring(0, 2))
                                                {
                                                    // 時間帯毎に格納
                                                    PubConstClass.tsKadouTime[Convert.ToInt32(strRunFrom.Substring(9, 2))] += ts0;
                                                }
                                                else
                                                {
                                                    DateTime dtSplit1 = new DateTime(int.Parse(strRunFrom.Substring(0, 4)),
                                                                                     int.Parse(strRunFrom.Substring(4, 2)),
                                                                                     int.Parse(strRunFrom.Substring(6, 2)),
                                                                                     int.Parse(strRunFrom.Substring(9, 2)),
                                                                                     int.Parse(strRunFrom.Substring(11, 2)),
                                                                                     int.Parse(strRunFrom.Substring(13, 2)));

                                                    DateTime dtSplit2 = new DateTime(int.Parse(strRunFrom.Substring(0, 4)),
                                                                                     int.Parse(strRunFrom.Substring(4, 2)),
                                                                                     int.Parse(strRunFrom.Substring(6, 2)),
                                                                                     int.Parse(strArray[1].Substring(0, 2)),
                                                                                     0,
                                                                                     0);
                                                    TimeSpan tsSplit0 = dtSplit2 - dtSplit1;
                                                    // 時間帯毎に格納
                                                    PubConstClass.tsKadouTime[Convert.ToInt32(strRunFrom.Substring(9, 2))] += tsSplit0;

                                                    DateTime dtSplit3 = new DateTime(int.Parse(strRunFrom.Substring(0, 4)),
                                                                                     int.Parse(strRunFrom.Substring(4, 2)),
                                                                                     int.Parse(strRunFrom.Substring(6, 2)),
                                                                                     int.Parse(strArray[1].Substring(0, 2)),
                                                                                     0,
                                                                                     0);

                                                    DateTime dtSplit4 = new DateTime(int.Parse(strRunFrom.Substring(0, 4)),
                                                                                     int.Parse(strRunFrom.Substring(4, 2)),
                                                                                     int.Parse(strRunFrom.Substring(6, 2)),
                                                                                     int.Parse(strArray[1].Substring(0, 2)),
                                                                                     int.Parse(strArray[1].Substring(2, 2)),
                                                                                     int.Parse(strArray[1].Substring(4, 2)));
                                                    TimeSpan tsSplit1 = dtSplit4 - dtSplit3;
                                                    // 時間帯毎に格納
                                                    PubConstClass.tsKadouTime[int.Parse(strArray[1].Substring(0, 2))] += tsSplit1;
                                                }

                                                strRunFrom = "";
                                                // 稼働終了時間を格納する
                                                PubConstClass.strEndTime = strArray[1];
                                            }
                                        }
                                        else
                                        {
                                            // 「Alarm List」の処理
                                            blnIsOmitFlag = false;
                                            // アラーム除外リストとの比較
                                            for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intOmitAlarmListCnt - 1; intLoopCnt++)
                                            {
                                                //if (PubConstClass.pblOmitAlarmList[intLoopCnt] == strReadData.Substring(16, 5))
                                                if (PubConstClass.pblOmitAlarmList[intLoopCnt] == strArray[3])
                                                {
                                                    // 除外リストと一致
                                                    blnIsOmitFlag = true;
                                                    break;
                                                }
                                            }
                                            // 除外フラグとのチェック
                                            if (blnIsOmitFlag == false)
                                            {
                                                // 除外リストに含まれない
                                                if (strAlarmTime != "")
                                                    swAlarmStop.WriteLine(strAlarmTime);
                                                strAlarmTime = strReadData + "■";
                                            }
                                        }
                                    }
                                }
                            }
                            swRun.WriteLine("END");
                            swRun.WriteLine("■稼働開始時間：" + PubConstClass.strStartTime);
                            swRun.WriteLine("■稼働終了時間：" + PubConstClass.strEndTime);
                            swRun.WriteLine("■稼働最大時間：" + PubConstClass.strMaxRunTime + "(" + PubConstClass.strMaxRunFromTime + "～" + PubConstClass.strMaxRunToTime + ")");
                            swRun.WriteLine("■稼働時間合計：" + tsRun.ToString());
                            PubConstClass.strRunTime = tsRun.ToString();

                            swStop.WriteLine("END");
                            swStop.WriteLine("■停止最大時間：" + strMaxStopTime);
                            swStop.WriteLine("■停止時間合計：" + tsStop.ToString());

                            for (var intLoopCnt = 0; intLoopCnt <= 23; intLoopCnt++)
                            {
                                swRun.WriteLine("稼動時間（" + intLoopCnt + "）" + PubConstClass.tsKadouTime[intLoopCnt].ToString());
                                swStop.WriteLine("停止時間（" + intLoopCnt + "）" + PubConstClass.tsTeisiTime[intLoopCnt].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【GetRunAndStopTime】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// アラーム除外リスト取得
        /// </summary>
        /// <remarks></remarks>
        public static void GetOmitAlarmList()
        {
            string strReadDataPath;
            string[] strArray;

            try
            {
                PubConstClass.intOmitAlarmListCnt = 0;
                // アラームリストファイルの読込
                strReadDataPath = IncludeTrailingPathDelimiter(Application.StartupPath) + PubConstClass.DEF_OMIT_ALARM_LIST;
                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        if (strArray.Length > 1)
                        {
                            // アラームCD
                            PubConstClass.pblOmitAlarmList[PubConstClass.intOmitAlarmListCnt] = strArray[0];
                            OutPutLogFile("■アラーム除外リスト：" + PubConstClass.pblOmitAlarmList[PubConstClass.intOmitAlarmListCnt]);
                            PubConstClass.intOmitAlarmListCnt += 1;
                        }
                    }
                    // 最後の配列データをクリアする
                    PubConstClass.pblOmitAlarmList[PubConstClass.intAlarmListCnt] = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetOmitAlarmList】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// WAVEファイルを再生する
        /// </summary>
        /// <param name="waveFile"></param>
        /// <remarks></remarks>
        public static void PlaySound(string waveFile)
        {
            OutPutLogFile($"【DEBUG】音声ファイル（waveFile={waveFile}）再生");
            // 再生されているときは止める
            if (!(player == null))
                StopSound();

            // 読み込む
            player = new System.Media.SoundPlayer(waveFile);
            // 非同期再生する
            player.Play();
        }

        /// <summary>
        /// 再生中の音声を停止する処理
        /// </summary>
        /// <remarks></remarks>
        public static void StopSound()
        {
            try
            {
                if (!(player == null))
                {
                    player.Stop();
                    player.Dispose();
                    player = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【StopSound】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// フォルダ内の最新ファイルの名称をプルパス指定で返却する
        /// </summary>
        /// <param name="sfolderName"></param>
        /// <param name="sfileName"></param>
        /// <returns></returns>
        public static string GetNewestFileName(string sfolderName, string sfileName)
        {

            try
            {
                // 指定されたフォルダ内の指定ファイル名をすべて取得する
                string[] files = Directory.GetFiles(sfolderName, sfileName, SearchOption.TopDirectoryOnly);

                string newestFileName = string.Empty;
                DateTime updateTime = DateTime.MinValue;
                foreach (string file in files)
                {
                    // それぞれのファイルの更新日付を取得する
                    FileInfo fi = new FileInfo(file);
                    // 更新日付が最新なら更新日付とファイル名を保存する
                    if (fi.LastWriteTime > updateTime)
                    {
                        updateTime = fi.LastWriteTime;
                        newestFileName = file;
                    }
                }
                // ファイル名をプルパス指定で返す
                return CommonModule.IncludeTrailingPathDelimiter(sfolderName) + Path.GetFileName(newestFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【getNewestFileName】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /// <summary>
        /// 吊札用 印刷文字データ 読込処理
        /// </summary>
        /// <param name="strLoadingDataPath"></param>
        public static void ReadHangingTagData(string strHangingTagPath)
        {
            string[] strArray;

            try
            {
                // 吊札データ読出し
                using (StreamReader sr = new StreamReader(strHangingTagPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        PubConstClass.HangingTagTable data = new PubConstClass.HangingTagTable
                        {
                            endMark = (strArray[0] == "1" ? true : false),  // ENDマーク
                            coopCode = strArray[1],                         // 生協コード
                            coopName = strArray[2],                         // 生協名
                            planNumber = strArray[3],                       // 企画号
                            distributionDayFrom = strArray[4],              // 配布曜日（FROM）
                            distributionDayTo = strArray[5],                // 配布曜日（TO）
                            courseFrom = strArray[6],                       // コース（FROM）
                            courseTo = strArray[7],                         // コース（TO）
                            depoCode = strArray[8],                         // デポコード
                            depoName = strArray[9],                         // デポ名
                            containerNumber = strArray[10],                 // 積載コンテナ数
                            setNumber = strArray[11],                       // セット数／コンテナ
                            numberOfCopies = strArray[12],                  // 積載部数
                            setOrderStart = strArray[13],                   // セット順（スタート）
                            setOrderEnd = strArray[14],                     // セット順（エンド）
                            basketCarSerialNumber = strArray[15],           // カゴ車連番
                            basketCarSerialTotal = strArray[16],            // カゴ車連番総数
                            lastUnionNumber = strArray[17],                 // カゴ車チェックQR（最後の組合員番号+Z）
                            pageNumber = strArray[18],                      // ページ番号
                        };
                        PubConstClass.hangingTagTable.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ReadHangingTagData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// カゴ車数一覧（デポ毎）用 印刷文字データ 読込処理
        /// </summary>
        /// <param name="strLoadingDataPath"></param>
        public static void ReadListOfNumberOfCarsInBasket(string strLoadingDataPath)
        {
            string[] strArray;
            try
            {
                PubConstClass.listOfNumberOfCarsInBasket.Clear();
                // データ読出し
                using (StreamReader sr = new StreamReader(strLoadingDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        PubConstClass.ListOfNumberOfCarsInBasket data = new PubConstClass.ListOfNumberOfCarsInBasket
                        {
                            depoCode = strArray[0],                 // デポコード
                            depoName = strArray[1],                 // デポ名
                            setNumber = strArray[2],                // セット数
                            preliminarySetNumber = strArray[3],     // 予備セット数
                            basketCarNumber = strArray[4],          // カゴ車台数
                            containerNumber = strArray[5],          // コンテナ数
                            setNumberPercontainer = strArray[6],    // ｾｯﾄ数/ｺﾝﾃﾅ
                            startNumber = strArray[7],              // スタート
                            endNumber = strArray[8],                // エンド
                        };
                        PubConstClass.listOfNumberOfCarsInBasket.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ReadListOfNumberOfCarsInBasket】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 処理件数一覧（生協毎）用 印刷文字データ 読込処理
        /// </summary>
        /// <param name="strLoadingDataPath"></param>
        public static void ReadListOfProcessedItems(string strLoadingDataPath)
        {
            string[] strArray;
            try
            {
                PubConstClass.listOfProcessedItems.Clear();
                // データ読出し
                using (StreamReader sr = new StreamReader(strLoadingDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        PubConstClass.ListOfProcessedItems data = new PubConstClass.ListOfProcessedItems
                        {
                            coopCode = strArray[0],                 // 生協コード
                            coopName = strArray[1],                 // 生協名
                            setNumber = strArray[2],                // セット数
                            preliminarySetNumber = strArray[3],     // 予備セット数
                            basketCarNumber = strArray[4],          // カゴ車台数
                            containerNumber = strArray[5],          // コンテナ数
                            setNumberPercontainer = strArray[6],    // ｾｯﾄ数/ｺﾝﾃﾅ
                            startNumber = strArray[7],              // スタート
                            endNumber = strArray[8],                // エンド
                        };
                        PubConstClass.listOfProcessedItems.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ReadListOfProcessedItems】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 出荷確認表用 印刷文字データ 読込処理
        /// </summary>
        /// <param name="strLoadingDataPath"></param>
        public static void ReadShipmentConfirmationTable(string strLoadingDataPath)
        {
            string[] strArray;
            try
            {
                PubConstClass.shipmentConfirmationTable.Clear();
                // データ読出し
                using (StreamReader sr = new StreamReader(strLoadingDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        PubConstClass.ShipmentConfirmationTable data = new PubConstClass.ShipmentConfirmationTable
                        {
                            depoCode = strArray[0],                 // 生協コード
                            depoName = strArray[1],                 // 生協名
                            basketCarNumber = strArray[4],          // カゴ車台数
                        };
                        PubConstClass.shipmentConfirmationTable.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ReadShipmentConfirmationTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 帳票枚数確認表 印刷文字データ 読込処理
        /// </summary>
        /// <param name="strLoadingDataPath"></param>
        public static void ReadNumberOfConfirmationTable(string strLoadingDataPath)
        {
            string[] strArray;

            try
            {
                PubConstClass.numberOfConfirmationTable.Clear();
                // データ読出し
                using (StreamReader sr = new StreamReader(strLoadingDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strArray = sr.ReadLine().Split(',');
                        PubConstClass.NumberOfConfirmationTable data = new PubConstClass.NumberOfConfirmationTable
                        {
                            coopCode = strArray[0],                 // 生協コード
                            coopName = strArray[1],                 // 生協名
                            numberOfFormST2 = strArray[3],          // 帳票ST1
                            numberOfFormST3 = strArray[4],          // 帳票ST2
                            numberOfFormST1 = strArray[2],          // 帳票ST3
                            numberOfFormST4 = strArray[5],          // 帳票ST4
                            numberOfFormST5 = strArray[6],          // 帳票ST5
                            numberOfFormST6 = strArray[7],          // 帳票ST6
                        };
                        PubConstClass.numberOfConfirmationTable.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ReadNumberOfConfirmationTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 積付表用 フォント・画像など
        private static Image imgEndMark = Image.FromFile("EndMark.bmp");
        private static Image imgBetsu = Image.FromFile("Betsu.bmp");
        private static Font fontHGS15P = new Font("HGS創英角ｺﾞｼｯｸUB", 15.0f);
        private static Font fontHGS20P = new Font("HGS創英角ｺﾞｼｯｸUB", 20.0f);
        private static Font fontHGS24P = new Font("HGS創英角ｺﾞｼｯｸUB", 24.0f);
        private static Font fontHGS26P = new Font("HGS創英角ｺﾞｼｯｸUB", 26.0f);
        private static Font fontHGS28P = new Font("HGS創英角ｺﾞｼｯｸUB", 28.0f);
        private static Font fontHGS36P = new Font("HGS創英角ｺﾞｼｯｸUB", 36.0f);
        private static Font fontHGS55P = new Font("HGS創英角ｺﾞｼｯｸUB", 55.0f);
        private static Font fontHGS72P = new Font("HGS創英角ｺﾞｼｯｸUB", 72.0f);
        #endregion

        /// <summary>
        /// 吊札用プリント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Pd_PrinthangingTag(object sender, PrintPageEventArgs e, int iPrintType)
        {
            try
            {
                // ページ範囲が指定されているかチェック
                // 印刷開始ページまで飛ばす
                if (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages && PubConstClass.printIndex == 0)
                {
                    PubConstClass.printIndex = e.PageSettings.PrinterSettings.FromPage - 1;
                }

                // 印刷設定など
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // データ取りだし
                PubConstClass.HangingTagTable data = PubConstClass.hangingTagTable[PubConstClass.printIndex];

                string pData01 = data.coopCode;                     // 生協コード
                string pData02 = data.coopName;                     // 生協名
                string pData03 = "<"+ data.planNumber +  "  >";     // 企画号
                e.Graphics.DrawString(pData01, fontHGS28P, Brushes.Black, 60, 40);
                e.Graphics.DrawString(pData02, fontHGS36P, Brushes.Black, 60, 52);
                e.Graphics.DrawString(pData03, fontHGS36P, Brushes.Black, 141, 33);
                e.Graphics.DrawString("号", fontHGS24P, Brushes.Black, 164, 36);

                string pData04 = data.distributionDayFrom + "・" + data.distributionDayTo;  // 配布曜日
                string pData05 = data.courseFrom + " - " + data.courseTo;                   // 配達次曜日＋配達次方面＋配達次方面内No.（スタート）
                e.Graphics.DrawString(pData04, fontHGS72P, Brushes.Black, 40, 85);
                e.Graphics.DrawString(pData05, fontHGS36P, Brushes.Black, 115, 94);

                string pData08 = data.depoCode;              // デポコード
                string pData09 = data.depoName;              // デポ名
                e.Graphics.DrawString(pData08, fontHGS36P, Brushes.Black, 38, 126);
                e.Graphics.DrawString(pData09, fontHGS72P, Brushes.Black, 40, 141);

                string pData10 = data.containerNumber;       // 積載コンテナ数 
                string pData11 = "箱";
                string pData12 = data.numberOfCopies;        // 積載部数
                string pData13 = "部";
                e.Graphics.DrawString(pData10, fontHGS36P, Brushes.Black, 40, 190);
                e.Graphics.DrawString(pData11, fontHGS36P, Brushes.Black, 58, 190);
                e.Graphics.DrawString(pData12, fontHGS36P, Brushes.Black, 136, 190);
                e.Graphics.DrawString(pData13, fontHGS15P, Brushes.Black, 163, 196);

                string pData14 = "(      部/箱)";                     // 1コンテナあたりの部数
                string pData15 = "(            ～             )";     // セット順 スタート・エンド
                string pData16 = data.setNumber;
                string pData17 = data.setOrderStart;
                string pData18 = data.setOrderEnd;
                e.Graphics.DrawString(pData14, fontHGS20P, Brushes.Black, 32, 205);
                e.Graphics.DrawString(pData15, fontHGS20P, Brushes.Black, 105, 205);
                e.Graphics.DrawString(pData16, fontHGS20P, Brushes.Black, 40, 205);
                e.Graphics.DrawString(pData17, fontHGS20P, Brushes.Black, 110, 205);
                e.Graphics.DrawString(pData18, fontHGS20P, Brushes.Black, 147, 205);

                string pData19 = data.basketCarSerialNumber.PadLeft(2);  // 同一デポ内カゴ車連番
                pData19 += "/";
                pData19 += data.basketCarSerialTotal.PadLeft(2);         // 同一デポ内カゴ車数
                e.Graphics.DrawString(pData19, fontHGS72P, Brushes.Black, 116 - 10, 225);

                // 150x150サイズのImageオブジェクトを作成する
                Bitmap img = new Bitmap(150, 150);
                // ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics g = Graphics.FromImage(img);
                // ＱＲコードをイメージを取得する
                string sUnionNumber = data.lastUnionNumber;
                //bc1.QRWriteBar("A" + sTestData, 0, 0, 3, g);
                bc1.QRWriteBar(sUnionNumber, 0, 0, 3, g);
                // リソースを解放する
                g.Dispose();
                // ＱＲコードの印字
                e.Graphics.DrawImage(img, new Rectangle(50, 226, 20, 20));

                if (data.endMark == true)
                {
                    // エンドマークの枠線を引く
                    Pen p = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(p, 102, 223, 95, 27);
                }

                // 頁番号
                string sPageNumber = data.pageNumber;
                e.Graphics.DrawString(sPageNumber, fontHGS20P, Brushes.Black, 20, 250);

                PubConstClass.printIndex++;
                //次のページがあるか調べる
                if (PubConstClass.printIndex >= PubConstClass.hangingTagTable.Count ||
                    (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages &&
                     e.PageSettings.PrinterSettings.ToPage <= PubConstClass.printIndex))
                {
                    // 印刷終了を指定
                    e.HasMorePages = false;
                    PubConstClass.printIndex = 0;
                }
                else
                {
                    // 印刷継続を指定
                    e.HasMorePages = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【Pd_PrinthangingTag】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 雛形印字処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="iPrintType"></param>
        public static void Pd_PrintLoadingTableForTemplate(object sender, PrintPageEventArgs e)
        {
            try
            {
                // ページ範囲が指定されているかチェック
                // 印刷開始ページまで飛ばす
                if (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages && PubConstClass.printIndex == 0)
                {
                    PubConstClass.printIndex = e.PageSettings.PrinterSettings.FromPage - 1;
                }

                // 印刷設定など
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // データ取りだし
                //PubConstClass.LoadingTablePrintData data = PubConstClass.loadingTablePrintDatas[PubConstClass.printIndex];

                string pData1 = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
                string pData2 = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５";
                // ENDマーク
                //e.Graphics.DrawImage(imgEndMark, 20, 20, 21, 25);
                e.Graphics.DrawImage(imgEndMark, 10, 10, 21, 25);

                e.Graphics.DrawString(pData1, fontHGS15P, Brushes.Black, 40, 10);
                e.Graphics.DrawString(pData1, fontHGS15P, Brushes.Black, 40, 20);
                e.Graphics.DrawString(pData1, fontHGS15P, Brushes.Black, 40, 30);

                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 40);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 50);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 60);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 70);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 80);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 90);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 100);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 110);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 120);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 130);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 140);

                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 150);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 160);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 170);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 180);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 190);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 200);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 210);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 220);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 230);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 240);

                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 250);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 260);
                e.Graphics.DrawString(pData2, fontHGS15P, Brushes.Black, 10, 270); 

                // 頁番号
                e.Graphics.DrawString("999", fontHGS15P, Brushes.Black, 10, 280);

                //PubConstClass.printIndex++;
                ////次のページがあるか調べる
                //if (PubConstClass.printIndex >= PubConstClass.loadingTablePrintDatas.Count ||
                //    (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages &&
                //     e.PageSettings.PrinterSettings.ToPage <= PubConstClass.printIndex))
                //{
                //    // 印刷終了を指定
                //    e.HasMorePages = false;
                //    PubConstClass.printIndex = 0;
                //}
                //else
                //{
                //    // 印刷継続を指定
                //    e.HasMorePages = true;
                //}

                // 印刷終了を指定
                e.HasMorePages = false;
                PubConstClass.printIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【Pd_PrintLoadingTableForTemplate】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// テスト用印字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="iPrintType"></param>
        public static void Pd_PrintLoadingTableForTest(object sender, PrintPageEventArgs e)
        {
            try
            {
                // ページ範囲が指定されているかチェック
                // 印刷開始ページまで飛ばす
                if (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages && PubConstClass.printIndex == 0)
                {
                    PubConstClass.printIndex = e.PageSettings.PrinterSettings.FromPage - 1;
                }

                // 印刷設定など
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // データ取りだし
                //PubConstClass.LoadingTablePrintData data = PubConstClass.loadingTablePrintDatas[PubConstClass.printIndex];

                string pData01 = "01";                  // 生協コード
                string pData02 = "ＧＣ生協ふくおか";            // 生協名
                string pData03 = "<51  >";              // 企画号
                e.Graphics.DrawString(pData01, fontHGS28P, Brushes.Black, 60, 40);
                e.Graphics.DrawString(pData02, fontHGS36P, Brushes.Black, 60, 52);
                e.Graphics.DrawString(pData03, fontHGS36P, Brushes.Black, 141, 33);
                e.Graphics.DrawString("号",    fontHGS24P, Brushes.Black, 164, 36);

                string pData04 = "E・E";                // 配布曜日
                string pData05 = "E21 - E21";           // 配達次曜日＋配達次方面＋配達次方面内No.（スタート）
                e.Graphics.DrawString(pData04, fontHGS72P, Brushes.Black, 40, 85);
                e.Graphics.DrawString(pData05, fontHGS36P, Brushes.Black, 115, 94);

                string pData08 = "25";                  // デポコード
                string pData09 = "福岡東";              // デポ名
                e.Graphics.DrawString(pData08, fontHGS36P, Brushes.Black, 38, 126);
                e.Graphics.DrawString(pData09, fontHGS72P, Brushes.Black, 40, 141);

                string pData10 = "16";　                // 積載コンテナ数 
                string pData11 = "箱";
                string pData12 = "625";                 // 積載部数
                string pData13 = "部";
                e.Graphics.DrawString(pData10, fontHGS36P, Brushes.Black, 40, 190);
                e.Graphics.DrawString(pData11, fontHGS36P, Brushes.Black, 58, 190);
                e.Graphics.DrawString(pData12, fontHGS36P, Brushes.Black, 136, 190);
                e.Graphics.DrawString(pData13, fontHGS15P, Brushes.Black, 163, 196);

                string pData14 = "(      部/箱)";                     // 1コンテナあたりの部数
                string pData15 = "(            ～            )";      // セット順 スタート・エンド
                string pData16 = "20";
                string pData17 = "003055";
                string pData18 = "003535";
                e.Graphics.DrawString(pData14, fontHGS20P, Brushes.Black, 32, 205);
                e.Graphics.DrawString(pData15, fontHGS20P, Brushes.Black, 105, 205);
                e.Graphics.DrawString(pData16, fontHGS20P, Brushes.Black, 40, 205);
                e.Graphics.DrawString(pData17, fontHGS20P, Brushes.Black, 110, 205);
                e.Graphics.DrawString(pData18, fontHGS20P, Brushes.Black, 147, 205);

                string pData19 = "3";           // 同一デポ内カゴ車番号
                string pData20 = "/";
                string pData21 = "3";           // 同一デポ内カゴ車番号
                e.Graphics.DrawString(pData19, fontHGS72P, Brushes.Black, 116, 225);
                e.Graphics.DrawString(pData20, fontHGS72P, Brushes.Black, 140, 225);
                e.Graphics.DrawString(pData21, fontHGS72P, Brushes.Black, 168, 225);

                // 150x150サイズのImageオブジェクトを作成する
                Bitmap img = new Bitmap(150, 150);
                // ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics g = Graphics.FromImage(img);
                // ＱＲコードをイメージを取得する
                string sTestData = "12345678Z";
                //bc1.QRWriteBar("A" + sTestData, 0, 0, 3, g);
                bc1.QRWriteBar(sTestData, 0, 0, 3, g);
                // リソースを解放する
                g.Dispose();
                // ＱＲコードの印字
                //e.Graphics.DrawImage(img, new Rectangle(50, 226, img.Width, img.Height));
                e.Graphics.DrawImage(img, new Rectangle(50, 226, 20, 20));

                //if (data.end == true)
                //{
                //    e.Graphics.DrawImage(imgEndMark, 20, 20, 21, 25);
                //}
                // エンドマークの枠線を引く
                Pen p = new Pen(Color.Black, 1);
                e.Graphics.DrawRectangle(p, 102, 223, 95, 27);
                
                // 頁番号
                e.Graphics.DrawString("025", fontHGS20P, Brushes.Black, 20, 250);

                //PubConstClass.printIndex++;
                ////次のページがあるか調べる
                //if (PubConstClass.printIndex >= PubConstClass.loadingTablePrintDatas.Count ||
                //    (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages &&
                //     e.PageSettings.PrinterSettings.ToPage <= PubConstClass.printIndex))
                //{
                //    // 印刷終了を指定
                //    e.HasMorePages = false;
                //    PubConstClass.printIndex = 0;
                //}
                //else
                //{
                //    // 印刷継続を指定
                //    e.HasMorePages = true;
                //}

                // 印刷終了を指定
                e.HasMorePages = false;
                PubConstClass.printIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【Pd_PrintLoadingTableForTest】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region カゴ車数一覧（デポ毎）及び処理件数（生協毎）用 フォント・ペンなど
        private static Font fontMSG8P = new Font("ＭＳ Ｐゴシック", 8.0f);
        private static Font fontMSG9P = new Font("ＭＳ Ｐゴシック", 9.0f);
        private static Font fontMSG14P = new Font("ＭＳ Ｐゴシック", 14.0f);
        private static Pen pen1 = new Pen(Color.Black, 0.2f);
        private static Pen pen2 = new Pen(Color.Black, 0.4f);
        private static StringFormat stringFormatC = new StringFormat();
        private static StringFormat stringFormatL = new StringFormat();
        private static StringFormat stringFormatR = new StringFormat();

        // 一覧表に関する座標などの定義
        const int TSX = 5;
        const int TEX = 285;
        const int TSY = 50;
        const int ROWSIZE = 5;
        const int TEY = 50 + ROWSIZE * 25;

        const int TVL1 = TSX + 23;
        const int TVL2 = TVL1 + 77;
        const int TVL3A = TVL2 + 20;
        const int TVL4A = TVL3A + 20;
        const int TVL5A = TVL4A + 20;
        const int TVL6A = TVL5A + 20;
        const int TVL7A = TVL6A + 20;
        const int TVL8A = TVL7A + 20;
        const int TVL9A = TVL8A + 20;
        const int TVL10A = TVL9A + 15;

        const int COL1 = (TSX + TVL1) / 2;
        const int COL2 = TVL1;
        const int COL3A = (TVL2 + TVL3A) / 2;
        const int COL4A = (TVL3A + TVL4A) / 2;        
        const int COL5A = (TVL4A + TVL5A) / 2;
        const int COL6A = (TVL5A + TVL6A) / 2;
        const int COL7A = (TVL6A + TVL7A) / 2;
        const int COL8A = (TVL7A + TVL8A) / 2;
        const int COL9A = (TVL8A + TVL9A) / 2;
        const int COL10A = (TVL9A + TVL10A) / 2;
        const int COL11A = (TVL10A + TEX) / 2;

        // 帳票枚数確認表
        const int MTVL1 = TSX + 23;
        const int MTVL2 = MTVL1 + 77;
        const int MTVL3A = MTVL2 + 30;
        const int MTVL4A = MTVL3A + 30;
        const int MTVL5A = MTVL4A + 30;
        const int MTVL6A = MTVL5A + 30;
        const int MTVL7A = MTVL6A + 30;
        const int MTVL8A = MTVL7A + 30;

        // 帳票枚数確認表
        const int MCOL2 = MTVL1;
        const int MCOL3A = (MTVL2 + MTVL3A) / 2;
        const int MCOL4A = (MTVL3A + MTVL4A) / 2;
        const int MCOL5A = (MTVL4A + MTVL5A) / 2;
        const int MCOL6A = (MTVL5A + MTVL6A) / 2;
        const int MCOL7A = (MTVL6A + MTVL7A) / 2;
        const int MCOL1 = (TSX + MTVL1) / 2;
        const int MCOL8A = (MTVL7A + MTVL8A) / 2;

        // 出荷確認表
        const int STVL1   = TSX     + 23;
        const int STVL2   = STVL1   + 77;
        const int STVL3A  = STVL2   + 20;   // カゴ車数(予定)
        const int STVL4A  = STVL3A  + 15;   // 担当
        const int STVL5A  = STVL4A  + 14;   // チェック

        const int STVL6A  = STVL5A  + 15;   // ①担当
        const int STVL7A  = STVL6A  + 20;   // ①カゴ車数(実績)
        const int STVL8A  = STVL7A  + 15;   // ①ドライバー
        const int STVL9A  = STVL8A  + 15;   // ①積込時間
        
        const int STVL10A = STVL9A  + 15;   // ②担当
        const int STVL11A = STVL10A + 20;   // ②カゴ車数(実績)
        const int STVL12A = STVL11A + 15;   // ②ドライバー
        const int STVL13A = STVL12A + 15 + 1;   // ②積込時間

        // 出荷確認表
        const int SCOL3A = (STVL2 + STVL3A) / 2;
        const int SCOL4A = (STVL3A + STVL4A) / 2;
        const int SCOL5A = (STVL4A + STVL5A) / 2;
        const int SCOL2 = STVL1;
        const int SCOL6A = (STVL5A + STVL6A) / 2;
        const int SCOL7A = (STVL6A + STVL7A) / 2;
        const int SCOL1 = (TSX + STVL1) / 2;
        const int SCOL8A = (STVL7A + STVL8A) / 2;
        const int SCOL9A = (STVL8A + STVL9A) / 2;
        const int SCOL10A = (STVL9A + STVL10A) / 2;
        const int SCOL11A = (STVL10A + STVL11A) / 2;
        const int SCOL12A = (STVL11A + STVL12A) / 2;
        const int SCOL13A = (STVL12A + STVL13A) / 2;

        const int TVL3B = TVL2 + 23;
        const int TVL4B = TVL3B + 14;
        const int TVL5B = TVL4B + 13;
        const int TVL6B = TVL5B + 14;
        const int TVL7B = TVL6B + 22;
        const int TVL8B = TVL7B + 15;
        const int TVL9B = TVL8B + 14;
        const int TVL10B = TVL9B + 14;
        const int TVL11B = TVL10B + 22;
        const int TVL12B = TVL11B + 15;

        const int THL1 = TSY + ROWSIZE;

        const int ROW1 = TSY + (ROWSIZE / 2) + 1;
        const int ROW2 = THL1 + (ROWSIZE / 2) + 1;
        const int ROW3 = THL1 + ROWSIZE + 1 + 1;
        const int ROW4 = THL1 + ROWSIZE * 2 + 1;
        #endregion

        /// <summary>
        /// 「カゴ車一覧」「処理件数一覧」「出荷確認表」「帳票枚数確認表」用プリント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="iPrintType"></param>
        public static void Pd_PrintList(object sender, PrintPageEventArgs e, int iPrintType)
        {
            try
            {
                // 印刷設定など
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;
                stringFormatC.Alignment = StringAlignment.Center;
                stringFormatR.Alignment = StringAlignment.Far;
                stringFormatC.LineAlignment = StringAlignment.Center;
                stringFormatL.LineAlignment = StringAlignment.Center;
                stringFormatR.LineAlignment = StringAlignment.Center;

                if (iPrintType == 0)
                {                    
                    // 書式番号印字
                    e.Graphics.DrawString("書式番号：　XXXX999-00", fontMSG9P, Brushes.Black, TSX, 10, stringFormatL);
                    // タイトル印字 
                    e.Graphics.DrawString("カゴ車数一覧(デポ毎)", fontHGS20P, Brushes.Black, 105, 10);
                    // カゴ車一覧
                    PrintListOfCarts(sender, e);

                    if (PubConstClass.listOfNumberOfCarsInBasket.Count > PubConstClass.printIndex)
                    {
                        // 印刷継続を指定
                        e.HasMorePages = true;
                    }
                    else
                    {
                        // 印刷終了を指定
                        e.HasMorePages = false;
                        PubConstClass.printIndex = 0;
                    }
                }
                else if (iPrintType == 1)
                {                    
                    // 書式番号印字
                    e.Graphics.DrawString("書式番号：　XXXX999-01", fontMSG9P, Brushes.Black, TSX, 10, stringFormatL);
                    // タイトル印字 
                    e.Graphics.DrawString("処理件数一覧(生協毎)", fontHGS20P, Brushes.Black, 105, 10);
                    // 処理件数一覧
                    PrintListOfProcessedItems(sender, e);

                    if (PubConstClass.listOfProcessedItems.Count > PubConstClass.printIndex)
                    {
                        // 印刷継続を指定
                        e.HasMorePages = true;
                    }
                    else
                    {
                        // 印刷終了を指定
                        e.HasMorePages = false;
                        PubConstClass.printIndex = 0;
                    }
                }
                else if (iPrintType == 2)
                {                    
                    // 書式番号印字
                    e.Graphics.DrawString("書式番号：　XXXX999-02", fontMSG9P, Brushes.Black, TSX, 10, stringFormatL);
                    // タイトル印字
                    e.Graphics.DrawString("出荷確認表", fontHGS20P, Brushes.Black, 110, 10);
                    // 出荷確認表
                    PrintShipmentConfirmationTable(sender, e);

                    if (PubConstClass.shipmentConfirmationTable.Count > PubConstClass.printIndex)
                    {
                        // 印刷継続を指定
                        e.HasMorePages = true;
                    }
                    else
                    {
                        // 印刷終了を指定
                        e.HasMorePages = false;
                        PubConstClass.printIndex = 0;
                    }
                }
                else if (iPrintType == 3)
                {                    
                    // 書式番号印字
                    e.Graphics.DrawString("書式番号：　XXXX999-03", fontMSG9P, Brushes.Black, TSX, 10, stringFormatL);
                    // タイトル印字
                    e.Graphics.DrawString("帳票枚数確認表", fontHGS20P, Brushes.Black, 110, 10);
                    // 帳票枚数確認表
                    PrintNumberOfFormsList(sender, e);
                    
                    if (PubConstClass.numberOfConfirmationTable.Count > PubConstClass.printIndex)
                    {
                        // 印刷継続を指定
                        e.HasMorePages = true;
                    }
                    else
                    {
                        // 印刷終了を指定
                        e.HasMorePages = false;
                        PubConstClass.printIndex = 0;
                    }
                }
                else
                {
                    // その他
                    // 書式番号印字
                    e.Graphics.DrawString("書式番号：　XXXX???-??", fontMSG9P, Brushes.Black, TSX, 10, stringFormatL);
                    // タイトル印字
                    e.Graphics.DrawString("ＸＸＸＸ確認表", fontHGS20P, Brushes.Black, 120, 10);
                }


                //if (PubConstClass.listTablePrintDatas.Count > PubConstClass.printIndex)
                //{
                //    // 印刷継続を指定
                //    e.HasMorePages = true;
                //}
                //else
                //{
                //    // 印刷終了を指定
                //    e.HasMorePages = false;
                //    PubConstClass.printIndex = 0;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【Pd_PrintList】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 帳票ヘッダーと枠印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="sItemName">タイトルの先頭の文言</param>
        /// <param name="bIsSetSpecifications">＜セット仕様＞文言印字</param>
        /// <param name="sItemData">タイトル文言の内容</param>
        /// <param name="sKikaku">企画</param>
        /// <param name="sYobi">配達曜日</param>
        private static void PrintFormHeadeAndBorder(object sender, PrintPageEventArgs e, string sItemName, 
                                                                                         bool bIsSetSpecifications, 
                                                                                         string sItemData, 
                                                                                         string sKikaku, 
                                                                                         string sYobi)
        {
            try
            {
                if (bIsSetSpecifications)
                {
                    // ＜セット仕様＞文言印字
                    e.Graphics.DrawString("＜セット仕様＞", fontMSG9P, Brushes.Black, TSX, 28, stringFormatL);
                }
                // 生協名 管理用企画番号 曜日
                e.Graphics.DrawRectangle(pen2, TSX, 30, TVL2 - TSX, 15);
                e.Graphics.DrawLine(pen1, TSX, 35, TVL2, 35);
                e.Graphics.DrawLine(pen1, TSX, 40, TVL2, 40);
                e.Graphics.DrawLine(pen1, TVL1, 30, TVL1, 45);

                e.Graphics.DrawString(sItemName, fontMSG9P, Brushes.Black, TSX, 33, stringFormatL);
                e.Graphics.DrawString("企画号数", fontMSG9P, Brushes.Black, TSX, 38, stringFormatL);
                e.Graphics.DrawString("曜日", fontMSG9P, Brushes.Black, TSX, 43, stringFormatL);

                string pData = sItemData;   // 生協名またはセンター名
                e.Graphics.DrawString(pData, fontMSG9P, Brushes.Black, TSX + 25, 33, stringFormatL);
                pData = sKikaku;            // 企画
                e.Graphics.DrawString(pData, fontMSG9P, Brushes.Black, TSX + 25, 38, stringFormatL);
                pData = sYobi;              // 曜日
                e.Graphics.DrawString(pData, fontMSG9P, Brushes.Black, TSX + 25, 43, stringFormatL);

                e.Graphics.DrawString(PubConstClass.printCoopName, fontMSG9P, Brushes.Black, TVL1, 33, stringFormatL);
                e.Graphics.DrawString(PubConstClass.printPlanNo, fontMSG9P, Brushes.Black, TVL1, 38, stringFormatL);
                e.Graphics.DrawString(PubConstClass.printDayOfWeek, fontMSG9P, Brushes.Black, TVL1, 43, stringFormatL);

                // 承認 審査者 作成者
                e.Graphics.DrawRectangle(pen2, 220, 15, 60, 25);
                e.Graphics.DrawLine(pen1, 220, 20, 280, 20);
                e.Graphics.DrawLine(pen1, 240, 15, 240, 40);
                e.Graphics.DrawLine(pen1, 260, 15, 260, 40);

                e.Graphics.DrawString("承認", fontMSG9P, Brushes.Black, 230, 18, stringFormatC);
                e.Graphics.DrawString("審査者", fontMSG9P, Brushes.Black, 250, 18, stringFormatC);
                e.Graphics.DrawString("作成者", fontMSG9P, Brushes.Black, 270, 18, stringFormatC);

                // 一覧表　フォーマット描画
                e.Graphics.DrawRectangle(pen2, TSX, TSY, TEX - TSX, TEY - TSY);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintListOfCommon】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// カゴ車一覧（デポ毎）印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PrintListOfCarts(object sender, PrintPageEventArgs e)
        {
            try
            {
                // 帳票ヘッダーと枠印刷
                PrintFormHeadeAndBorder(sender, e, "生協名", true,
                                        ReadCollatingDataForm.sCoopHeader,
                                        ReadCollatingDataForm.sKikakuHeader,
                                        ReadCollatingDataForm.sYobiFromHeader + "・" + ReadCollatingDataForm.sYobiToHeader
                                        );
                
                #region ボディ印刷
                // カゴ車数一覧用
                for (int i = 1; i <= 24; i++)
                {
                    if (i == 1)
                    {
                        e.Graphics.DrawLine(pen1, TSX, TSY + i * ROWSIZE, TVL9A, TSY + i * ROWSIZE);
                    }
                    else
                    {
                        e.Graphics.DrawLine(pen1, TSX, TSY + i * ROWSIZE, TEX, TSY + i * ROWSIZE);
                    }                    
                }
                // コラム（カゴ車数）
                e.Graphics.DrawLine(pen1, TVL1, THL1, TVL1, TEY);       // デポコード
                e.Graphics.DrawLine(pen1, TVL2, TSY, TVL2, TEY);        // デポ名
                // コラム（カタログセット）
                e.Graphics.DrawLine(pen1, TVL3A, THL1, TVL3A, TEY);     // セット数
                e.Graphics.DrawLine(pen1, TVL4A, THL1, TVL4A, TEY);     // 予備セット
                e.Graphics.DrawLine(pen1, TVL5A, THL1, TVL5A, TEY);     // カゴ車台数
                e.Graphics.DrawLine(pen1, TVL6A, THL1, TVL6A, TEY);     // コンテナ数
                e.Graphics.DrawLine(pen1, TVL7A, TSY, TVL7A, TEY);      // ｾｯﾄ数/ｺﾝﾃﾅ
                // コラム（セット順）
                e.Graphics.DrawLine(pen1, TVL8A, THL1, TVL8A, TEY);     // スタート
                e.Graphics.DrawLine(pen1, TVL9A, TSY, TVL9A, TEY);      // エンド               
                // テーブルヘッダー１行目
                e.Graphics.DrawString("カゴ車数",       fontMSG8P, Brushes.Black, TSX,        ROW1, stringFormatL);
                e.Graphics.DrawString("カタログセット", fontMSG8P, Brushes.Black, COL4A + 12, ROW1, stringFormatL);
                e.Graphics.DrawString("セット順",       fontMSG8P, Brushes.Black, COL9A - 15, ROW1, stringFormatL);
                // テーブルヘッダー２行目
                e.Graphics.DrawString("デポコード",   fontMSG8P, Brushes.Black, COL1,   ROW2, stringFormatC);
                e.Graphics.DrawString("デポ名",       fontMSG8P, Brushes.Black, COL2,   ROW2, stringFormatL);
                e.Graphics.DrawString("セット数",     fontMSG8P, Brushes.Black, COL3A,  ROW2, stringFormatC);
                e.Graphics.DrawString("内予備セット", fontMSG8P, Brushes.Black, COL4A,  ROW2, stringFormatC);
                e.Graphics.DrawString("カゴ車台数",   fontMSG8P, Brushes.Black, COL5A,  ROW2, stringFormatC);
                e.Graphics.DrawString("コンテナ数",   fontMSG8P, Brushes.Black, COL6A,  ROW2, stringFormatC);
                e.Graphics.DrawString("ｾｯﾄ数/ｺﾝﾃﾅ",   fontMSG8P, Brushes.Black, COL7A,  ROW2, stringFormatC);
                e.Graphics.DrawString("スタート",     fontMSG8P, Brushes.Black, COL8A,  ROW2, stringFormatC);
                e.Graphics.DrawString("エンド",       fontMSG8P, Brushes.Black, COL9A,  ROW2, stringFormatC);
                e.Graphics.DrawString("備考",         fontMSG8P, Brushes.Black, COL10A, ROW1, stringFormatC);
                #endregion

                string pData = "";
                // 一覧表　データ描画
                for (int i = 1; i <= 23; i++)
                {
                    // データ取りだし                    
                    PubConstClass.ListOfNumberOfCarsInBasket data = PubConstClass.listOfNumberOfCarsInBasket[PubConstClass.printIndex];
                    // デポコード・デポ名
                    e.Graphics.DrawString(data.depoCode, fontMSG14P, Brushes.Black, COL1, ROW2 + i * ROWSIZE, stringFormatC);         // デポコード
                    e.Graphics.DrawString(data.depoName, fontMSG14P, Brushes.Black, COL2 + 2, ROW2 + i * ROWSIZE, stringFormatL);     // デポ名
                    // カゴ車数一覧用データ
                    e.Graphics.DrawString(data.setNumber            , fontMSG14P, Brushes.Black, TVL3A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.preliminarySetNumber , fontMSG14P, Brushes.Black, TVL4A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.basketCarNumber      , fontMSG14P, Brushes.Black, TVL5A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.containerNumber      , fontMSG14P, Brushes.Black, TVL6A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.setNumberPercontainer, fontMSG14P, Brushes.Black, TVL7A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.startNumber          , fontMSG14P, Brushes.Black, TVL8A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.endNumber            , fontMSG14P, Brushes.Black, TVL9A, ROW2 + i * ROWSIZE, stringFormatR);

                    PubConstClass.printIndex++;
                    if (PubConstClass.listOfNumberOfCarsInBasket.Count <= PubConstClass.printIndex)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintListOfCarts】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 処理件数一覧（生協毎）印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PrintListOfProcessedItems(object sender, PrintPageEventArgs e)
        {
            try
            {
                // 帳票ヘッダーと枠印刷
                PrintFormHeadeAndBorder(sender, e, "センター名", true,
                                        ReadCollatingDataForm.sCenterHeader,
                                        ReadCollatingDataForm.sKikakuHeader,
                                        ReadCollatingDataForm.sYobiFromHeader + "・" + ReadCollatingDataForm.sYobiToHeader
                                        );

                #region ボディ印刷
                // カゴ車数一覧用
                for (int i = 1; i <= 24; i++)
                {
                    if (i == 1)
                    {
                        e.Graphics.DrawLine(pen1, TSX, TSY + i * ROWSIZE, TVL9A, TSY + i * ROWSIZE);
                    }
                    else
                    {
                        e.Graphics.DrawLine(pen1, TSX, TSY + i * ROWSIZE, TEX, TSY + i * ROWSIZE);
                    }
                }
                // コラム（カゴ車数）
                e.Graphics.DrawLine(pen1, TVL1, THL1, TVL1, TEY);       // 生協コード
                e.Graphics.DrawLine(pen1, TVL2, TSY , TVL2, TEY);       // 生協名
                // コラム（カタログセット）
                e.Graphics.DrawLine(pen1, TVL3A, THL1, TVL3A, TEY);     // セット数
                e.Graphics.DrawLine(pen1, TVL4A, THL1, TVL4A, TEY);     // 予備セット
                e.Graphics.DrawLine(pen1, TVL5A, THL1, TVL5A, TEY);     // カゴ車台数
                e.Graphics.DrawLine(pen1, TVL6A, THL1, TVL6A, TEY);     // コンテナ数
                e.Graphics.DrawLine(pen1, TVL7A, TSY , TVL7A, TEY);     // ｾｯﾄ数/ｺﾝﾃﾅ
                // コラム（セット順）
                e.Graphics.DrawLine(pen1, TVL8A, THL1, TVL8A, TEY);     // スタート
                e.Graphics.DrawLine(pen1, TVL9A, TSY , TVL9A, TEY);     // エンド               
                // テーブルヘッダー１行目
                e.Graphics.DrawString("カゴ車数",       fontMSG8P, Brushes.Black, TSX,        ROW1, stringFormatL);
                e.Graphics.DrawString("カタログセット", fontMSG8P, Brushes.Black, COL4A + 12, ROW1, stringFormatL);
                e.Graphics.DrawString("セット順",       fontMSG8P, Brushes.Black, COL9A - 15, ROW1, stringFormatL);
                // テーブルヘッダー２行目
                e.Graphics.DrawString("生協コード",   fontMSG8P, Brushes.Black, COL1,   ROW2, stringFormatC);
                e.Graphics.DrawString("生協名",       fontMSG8P, Brushes.Black, COL2,   ROW2, stringFormatL);
                e.Graphics.DrawString("セット数",     fontMSG8P, Brushes.Black, COL3A,  ROW2, stringFormatC);
                e.Graphics.DrawString("内予備セット", fontMSG8P, Brushes.Black, COL4A,  ROW2, stringFormatC);
                e.Graphics.DrawString("カゴ車台数",   fontMSG8P, Brushes.Black, COL5A,  ROW2, stringFormatC);
                e.Graphics.DrawString("コンテナ数",   fontMSG8P, Brushes.Black, COL6A,  ROW2, stringFormatC);
                e.Graphics.DrawString("ｾｯﾄ数/ｺﾝﾃﾅ",   fontMSG8P, Brushes.Black, COL7A,  ROW2, stringFormatC);
                e.Graphics.DrawString("スタート",     fontMSG8P, Brushes.Black, COL8A,  ROW2, stringFormatC);
                e.Graphics.DrawString("エンド",       fontMSG8P, Brushes.Black, COL9A,  ROW2, stringFormatC);
                e.Graphics.DrawString("備考",         fontMSG8P, Brushes.Black, COL10A, ROW1, stringFormatC);
                #endregion
                string pData = "";
                // 一覧表　データ描画
                for (int i = 1; i <= 23; i++)
                {
                    // データ取りだし
                    PubConstClass.ListOfProcessedItems data = PubConstClass.listOfProcessedItems[PubConstClass.printIndex];
                    // 生協コード・生協名
                    e.Graphics.DrawString(data.coopCode, fontMSG14P, Brushes.Black, COL1, ROW2 + i * ROWSIZE, stringFormatC);
                    e.Graphics.DrawString(data.coopName, fontMSG14P, Brushes.Black, COL2 + 2, ROW2 + i * ROWSIZE, stringFormatL);
                    // カゴ車数一覧用データ
                    e.Graphics.DrawString(data.setNumber            , fontMSG14P, Brushes.Black, TVL3A, ROW2 + i * ROWSIZE, stringFormatR);    // セット数
                    e.Graphics.DrawString(data.preliminarySetNumber , fontMSG14P, Brushes.Black, TVL4A, ROW2 + i * ROWSIZE, stringFormatR);    // 予備セット
                    e.Graphics.DrawString(data.basketCarNumber      , fontMSG14P, Brushes.Black, TVL5A, ROW2 + i * ROWSIZE, stringFormatR);    // カゴ車台数
                    e.Graphics.DrawString(data.containerNumber      , fontMSG14P, Brushes.Black, TVL6A, ROW2 + i * ROWSIZE, stringFormatR);    // コンテナ数
                    e.Graphics.DrawString(data.setNumberPercontainer, fontMSG14P, Brushes.Black, TVL7A, ROW2 + i * ROWSIZE, stringFormatR);    // ｾｯﾄ数/ｺﾝﾃﾅ
                    e.Graphics.DrawString(data.startNumber          , fontMSG14P, Brushes.Black, TVL8A, ROW2 + i * ROWSIZE, stringFormatR);    // スタート
                    e.Graphics.DrawString(data.endNumber            , fontMSG14P, Brushes.Black, TVL9A, ROW2 + i * ROWSIZE, stringFormatR);    // エンド

                    PubConstClass.printIndex++;
                    if (PubConstClass.listOfProcessedItems.Count <= PubConstClass.printIndex)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintListOfProcessedItems】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 出荷確認票の印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PrintShipmentConfirmationTable(object sender, PrintPageEventArgs e)
        {
            try
            {
                // 帳票ヘッダーと枠印刷
                PrintFormHeadeAndBorder(sender, e, "生協名", true,
                                        ReadCollatingDataForm.sCoopHeader,
                                        ReadCollatingDataForm.sKikakuHeader,
                                        ReadCollatingDataForm.sYobiFromHeader + "・" + ReadCollatingDataForm.sYobiToHeader
                                        );
                #region ボディ印刷
                // カゴ車数一覧用
                for (int i = 1; i <= 24; i++)
                {
                    if (i == 1)
                    {
                        e.Graphics.DrawLine(pen1, STVL5A, TSY + i * ROWSIZE, TEX, TSY + i * ROWSIZE);
                    }
                    else
                    {
                        e.Graphics.DrawLine(pen1, TSX, TSY + i * ROWSIZE, TEX, TSY + i * ROWSIZE);
                    }
                }
                // コラム（デポ）
                e.Graphics.DrawLine(pen1, STVL1, TSY, STVL1, TEY);          // デポコード
                e.Graphics.DrawLine(pen1, STVL2, TSY, STVL2, TEY);          // デポ名
                // コラム（カゴ）
                e.Graphics.DrawLine(pen1, STVL3A, TSY, STVL3A, TEY);        // カゴ車数(予定)
                e.Graphics.DrawLine(pen1, STVL4A, TSY, STVL4A, TEY);        // 担当
                e.Graphics.DrawLine(pen1, STVL5A, TSY, STVL5A, TEY);        // チェック
                // コラム（①）
                e.Graphics.DrawLine(pen1, STVL6A, THL1, STVL6A,  TEY);      // ①担当
                e.Graphics.DrawLine(pen1, STVL7A, THL1, STVL7A,  TEY);      // ①カゴ車数(実績)       
                e.Graphics.DrawLine(pen1, STVL8A, THL1, STVL8A,  TEY);      // ①ドライバー
                e.Graphics.DrawLine(pen1, STVL9A, TSY,  STVL9A,  TEY);      // ①積込時間
                // コラム（②）
                e.Graphics.DrawLine(pen1, STVL10A, THL1, STVL10A, TEY);     // ②担当
                e.Graphics.DrawLine(pen1, STVL11A, THL1, STVL11A, TEY);     // ②カゴ車数(実績)       
                e.Graphics.DrawLine(pen1, STVL12A, THL1, STVL12A, TEY);     // ②ドライバー
                e.Graphics.DrawLine(pen1, STVL13A, THL1, STVL13A, TEY);     // ②積込時間

                // テーブルヘッダー１行目
                e.Graphics.DrawString("デポコード",     fontMSG8P, Brushes.Black, SCOL1  ,  ROW1, stringFormatC);
                e.Graphics.DrawString("デポ名",         fontMSG8P, Brushes.Black, SCOL2  ,  ROW1, stringFormatL);                
                
                e.Graphics.DrawString("カゴ車数(予定)", fontMSG8P, Brushes.Black, SCOL3A  , ROW1, stringFormatC);
                e.Graphics.DrawString("担当",           fontMSG8P, Brushes.Black, SCOL4A+1, ROW1, stringFormatC);
                e.Graphics.DrawString("チェック",       fontMSG8P, Brushes.Black, SCOL5A  , ROW1, stringFormatC);

                e.Graphics.DrawString("①",             fontMSG8P, Brushes.Black, SCOL6A  , ROW1, stringFormatC);
                e.Graphics.DrawString("担当",           fontMSG8P, Brushes.Black, SCOL6A+1, ROW2, stringFormatC);
                e.Graphics.DrawString("カゴ車数(実績)", fontMSG8P, Brushes.Black, SCOL7A  , ROW2, stringFormatC);
                e.Graphics.DrawString("ドライバー",     fontMSG8P, Brushes.Black, SCOL8A  , ROW2, stringFormatC);
                e.Graphics.DrawString("積込時間",       fontMSG8P, Brushes.Black, SCOL9A  , ROW2, stringFormatC);

                e.Graphics.DrawString("②",             fontMSG8P, Brushes.Black, SCOL10A  , ROW1, stringFormatC);
                e.Graphics.DrawString("担当",           fontMSG8P, Brushes.Black, SCOL10A+1, ROW2, stringFormatC);
                e.Graphics.DrawString("カゴ車数(実績)", fontMSG8P, Brushes.Black, SCOL11A  , ROW2, stringFormatC);
                e.Graphics.DrawString("ドライバー",     fontMSG8P, Brushes.Black, SCOL12A  , ROW2, stringFormatC);
                e.Graphics.DrawString("積込時間",       fontMSG8P, Brushes.Black, SCOL13A  , ROW2, stringFormatC);

                for (int i = 2; i <= 24; i++)
                {
                    e.Graphics.DrawString("：", fontMSG14P, Brushes.Black, SCOL9A , TSY + i * ROWSIZE + 3, stringFormatC);
                    e.Graphics.DrawString("：", fontMSG14P, Brushes.Black, SCOL13A, TSY + i * ROWSIZE + 3, stringFormatC);
                }
                string pData = "";
                // 一覧表　データ描画
                for (int i = 1; i <= 23; i++)
                {
                    // データ取りだし
                    PubConstClass.ShipmentConfirmationTable data = PubConstClass.shipmentConfirmationTable[PubConstClass.printIndex];
                    // デポコード・デポ名
                    e.Graphics.DrawString(data.depoCode       , fontMSG14P, Brushes.Black, SCOL1,     ROW2 + i * ROWSIZE, stringFormatC);    // デポコード
                    e.Graphics.DrawString(data.depoName       , fontMSG14P, Brushes.Black, SCOL2+5,   ROW2 + i * ROWSIZE, stringFormatL);    // デポ名
                    e.Graphics.DrawString(data.basketCarNumber, fontMSG14P, Brushes.Black, SCOL3A+10, ROW2 + i * ROWSIZE, stringFormatR);    // カゴ車数(予定)

                    PubConstClass.printIndex++;
                    if (PubConstClass.shipmentConfirmationTable.Count <= PubConstClass.printIndex)
                    {
                        break;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintShipmentConfirmationTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 帳票枚数確認の印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PrintNumberOfFormsList(object sender, PrintPageEventArgs e)
        {
            try
            {
                // 帳票ヘッダーと枠印刷
                PrintFormHeadeAndBorder(sender, e, "センター名", true,
                                        ReadCollatingDataForm.sCenterHeader,
                                        ReadCollatingDataForm.sKikakuHeader,
                                        ReadCollatingDataForm.sYobiFromHeader + "・" + ReadCollatingDataForm.sYobiToHeader
                                        );
                #region ボディ印刷
                // カゴ車数一覧用
                for (int i = 1; i <= 24; i++)
                {
                    if (i == 1)
                    {
                        e.Graphics.DrawLine(pen1, TSX+100, TSY + i * ROWSIZE, TEX, TSY + i * ROWSIZE);
                    }
                    else if (i == 2 || i == 3)
                    {
                        // 罫線を引かない
                    }
                    else
                    {
                        e.Graphics.DrawLine(pen1, TSX, TSY + i * ROWSIZE, TEX, TSY + i * ROWSIZE);
                    }                    
                }
                // コラム（生協）
                e.Graphics.DrawLine(pen1, MTVL1, TSY, MTVL1, TEY);       // 生協コード
                e.Graphics.DrawLine(pen1, MTVL2, TSY, MTVL2, TEY);       // 生協名
                // コラム（帳票名）
                e.Graphics.DrawLine(pen1, MTVL3A, THL1, MTVL3A, TEY);     // 帳票ＳＴ１
                e.Graphics.DrawLine(pen1, MTVL4A, THL1, MTVL4A, TEY);     // 帳票ＳＴ２
                e.Graphics.DrawLine(pen1, MTVL5A, THL1, MTVL5A, TEY);     // 帳票ＳＴ３
                e.Graphics.DrawLine(pen1, MTVL6A, THL1, MTVL6A, TEY);     // 帳票ＳＴ４
                e.Graphics.DrawLine(pen1, MTVL7A, THL1, MTVL7A, TEY);     // 帳票ＳＴ５

                // テーブルヘッダー１行目
                e.Graphics.DrawString("帳票名", fontMSG8P, Brushes.Black, MCOL5A + 10, ROW1, stringFormatL);
                // テーブルヘッダー２行目
                e.Graphics.DrawString("生協コード", fontMSG8P, Brushes.Black, MCOL1, ROW2, stringFormatC);
                e.Graphics.DrawString("生協名", fontMSG8P, Brushes.Black, MCOL2, ROW2, stringFormatL);
                // 帳票名ヘッダー１行目
                e.Graphics.DrawString("帳票ＳＴ１", fontMSG8P, Brushes.Black, MCOL3A, ROW2, stringFormatC);
                e.Graphics.DrawString("帳票ＳＴ２", fontMSG8P, Brushes.Black, MCOL4A, ROW2, stringFormatC);
                e.Graphics.DrawString("帳票ＳＴ３", fontMSG8P, Brushes.Black, MCOL5A, ROW2, stringFormatC);
                e.Graphics.DrawString("帳票ＳＴ４", fontMSG8P, Brushes.Black, MCOL6A, ROW2, stringFormatC);
                e.Graphics.DrawString("帳票ＳＴ５", fontMSG8P, Brushes.Black, MCOL7A, ROW2, stringFormatC);
                e.Graphics.DrawString("帳票ＳＴ６", fontMSG8P, Brushes.Black, MCOL8A, ROW2, stringFormatC);
                // 帳票名ヘッダー２行目
                e.Graphics.DrawString("デポチラシ", fontMSG8P, Brushes.Black, MCOL3A, ROW3, stringFormatC);
                e.Graphics.DrawString("デポチラシ", fontMSG8P, Brushes.Black, MCOL4A, ROW3, stringFormatC);
                e.Graphics.DrawString("デポチラシ", fontMSG8P, Brushes.Black, MCOL5A, ROW3, stringFormatC);
                e.Graphics.DrawString("デポチラシ", fontMSG8P, Brushes.Black, MCOL6A, ROW3, stringFormatC);
                e.Graphics.DrawString("OCR注文書", fontMSG8P, Brushes.Black, MCOL7A, ROW3, stringFormatC);
                e.Graphics.DrawString("OCR注文書年次", fontMSG8P, Brushes.Black, MCOL8A, ROW3, stringFormatC);
                // 帳票名ヘッダー３行目
                e.Graphics.DrawString("", fontMSG8P, Brushes.Black, MCOL3A, ROW4, stringFormatC);
                e.Graphics.DrawString("", fontMSG8P, Brushes.Black, MCOL4A, ROW4, stringFormatC);
                e.Graphics.DrawString("", fontMSG8P, Brushes.Black, MCOL5A, ROW4, stringFormatC);
                e.Graphics.DrawString("", fontMSG8P, Brushes.Black, MCOL6A, ROW4, stringFormatC);
                e.Graphics.DrawString("", fontMSG8P, Brushes.Black, MCOL7A, ROW4, stringFormatC);
                e.Graphics.DrawString("りんごみかん注文書", fontMSG8P, Brushes.Black, MCOL8A, ROW4, stringFormatC);
                #endregion

                string pData = "";
                // 一覧表　データ描画
                for (int i = 3; i <= 23; i++)
                {
                    // データ取りだし
                    PubConstClass.NumberOfConfirmationTable data = PubConstClass.numberOfConfirmationTable[PubConstClass.printIndex];

                    #region テストデータ印字
                    //// 生協コード・生協名
                    //pData = i.ToString("00");
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MCOL1, ROW2 + i * ROWSIZE, stringFormatC);  // 生協コード
                    //pData = "NNN20NNNNNNNNNNNNNNN";
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MCOL2 + 2, ROW2 + i * ROWSIZE, stringFormatL);  // 生協名
                    //// カゴ車数一覧用データ
                    //pData = "ZZZZ9";
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MTVL3A, ROW2 + i * ROWSIZE, stringFormatR);
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MTVL4A, ROW2 + i * ROWSIZE, stringFormatR);
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MTVL5A, ROW2 + i * ROWSIZE, stringFormatR);
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MTVL6A, ROW2 + i * ROWSIZE, stringFormatR);
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MTVL7A, ROW2 + i * ROWSIZE, stringFormatR);
                    //e.Graphics.DrawString(pData, fontMSG14P, Brushes.Red, MTVL8A, ROW2 + i * ROWSIZE, stringFormatR);
                    #endregion

                    // 生協コード・生協名
                    e.Graphics.DrawString(data.coopCode, fontMSG14P, Brushes.Black, MCOL1, ROW2 + i * ROWSIZE, stringFormatC);  // 生協コード
                    e.Graphics.DrawString(data.coopName, fontMSG14P, Brushes.Black, MCOL2 + 2, ROW2 + i * ROWSIZE, stringFormatL);  // 生協名
                    // カゴ車数一覧用データ
                    e.Graphics.DrawString(data.numberOfFormST1, fontMSG14P, Brushes.Black, MTVL3A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.numberOfFormST2, fontMSG14P, Brushes.Black, MTVL4A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.numberOfFormST3, fontMSG14P, Brushes.Black, MTVL5A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.numberOfFormST4, fontMSG14P, Brushes.Black, MTVL6A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.numberOfFormST5, fontMSG14P, Brushes.Black, MTVL7A, ROW2 + i * ROWSIZE, stringFormatR);
                    e.Graphics.DrawString(data.numberOfFormST6, fontMSG14P, Brushes.Black, MTVL8A, ROW2 + i * ROWSIZE, stringFormatR);

                    PubConstClass.printIndex++;
                    if (PubConstClass.numberOfConfirmationTable.Count <= PubConstClass.printIndex)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintNumberOfFormsList】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
