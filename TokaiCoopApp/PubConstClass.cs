using System;
using System.Collections.Generic;

namespace TokaiCoopApp
{

    public class PubConstClass
    {
        public const string CMD_SEND_a = "a";     // 生産管理PCレディ
        public const string CMD_SEND_b = "b";     // 登録コマンド
        public const string CMD_SEND_c = "c";     // コース区分けコマンド
        public const string CMD_SEND_d = "d";     // センター区分けコマンド
        public const string CMD_SEND_e = "e";     // 単協区分けコマンド
        public const string CMD_SEND_f = "f";     // 排出ゲート開放コマンド
        public const string CMD_SEND_g = "g";     // ＯＫ束コマンド
        public const string CMD_SEND_h = "h";     // ＮＧ束コマンド
        public const string CMD_SEND_i = "i";     // 予備①コマンド
        public const string CMD_SEND_j = "j";     // 予備②コマンド
        public const string CMD_SEND_k = "k";     // テストボタン
        public const string CMD_SEND_l = "l";     // ＯＫ束判定コマンド
        public const string CMD_SEND_m = "m";     // ＮＧ束判定コマンド
        public const string CMD_SEND_n = "n";     // メンテナンス画面遷移コマンド

        public const string CMD_SEND_HEADER = "@??";   // PHC-D08へのヘッダー送信

        public const string CMD_RECIEVE_A = "A";  // シトマからの結束束区分け信号を受信
        public const string CMD_RECIEVE_B = "B";  // シトマからコース区分け信号を受信
        public const string CMD_RECIEVE_C = "C";  // シトマからセンター区分け信号を受信
        public const string CMD_RECIEVE_D = "D";  // シトマから単協区分け信号を受信
        public const string CMD_RECIEVE_E = "E";  // シトマから排出ゲート開放信号を受信

        public const string DEF_VERSION              = "Ver.20.26.01.05";           // バージョン情報 
        public const string DEF_FILENAME             = "GreenCoopApp.def";          // DEFファイル名称
        //public const string DEF_ORG_MASTER           = "組織マスタ.txt";
        public const string DEF_ALARM_LIST           = "アラームリスト.txt";
        public const string DEF_NAME_CONVERSION_LIST = "名称変換リスト.txt";
        public const string DEF_OMIT_ALARM_LIST      = "アラーム除外リスト.txt";
        public const string DEF_DAILY_SHIPPING_LIST  = "日次出庫確認表.csv";
        public const string DEF_BUZZER_SET_NAME      = "ブザー区分け名称.txt";
        public const string DEF_BUZZER_TONE_NAME     = "ブザー音色名称.txt";
        public const string DEF_TITLE                = "ラッピングシステム　生産管理ＰＣ";
        public const char   DEF_MASTER_DELIMITER     = ',';
        public const string DEF_FROM_TO_DELIMITER    = " - ";
        public const int    DEF_CNT_FOR_DRIVING      = 10;
        public const string DEF_OPERATING_LOCATION   = "サンシャインワークス";
        public const string DEF_COOP_AND_DEPO_LIST   = "CoopAndDepoList.txt";

        public const string LIST_NUMBER_CARS                = "カゴ車数一覧（デポ毎）";
        public const string LIST_PROCESSED_ITEMS            = "処理件数一覧（生協毎）";
        public const string SHIPMENT_CONFIRMATION_TABLE     = "出荷確認表";
        public const string NUMBER_FORMS_CONFIRMATION_TABLE = "帳票枚数確認表";

        public static int iMaxBoxOfRollBox;          // カゴ車への積載数（出荷束数）
        public static int iMaxNumberOfBundle;        // 結束最大束数（セット／束）       

        // 配布曜日用辞書
        public static Dictionary<string, string> dicDistributionDay = new Dictionary<string, string>()
        {
            { "A", "月AM" },
            { "B", "月PM" },
            { "C", "火AM" },
            { "D", "火PM" },
            { "E", "水AM" },
            { "F", "水PM" },
            { "G", "木AM" },
            { "H", "木PM" },
            { "I", "金AM" },
            { "J", "金PM" },
            { "K", "土AM" },
            { "L", "土PM" },
        };

        // （生協CD＋デポCD）（生協名,デポ名）
        public static string[,] pblMasterOfOrg = new string[1001, 2];       // 組織マスタデータ格納配列
        public static int intMasterOfOrgCnt;                                // 組織マスタデータ格納数
        public static string[,] pblMasterOfOffice = new string[1001, 2];    // 事業所マスタデータ格納配列
        public static int intMasterOfOfficeCnt;                             // 事業所マスタデータ格納数
        public static string pblCurrentDate;                                // 現在年月日（アプリ起動年月日）
        public static string[,] pblAlarmList = new string[1001, 2];         // アラームリスト格納配列
        public static int intAlarmListCnt;                                  // アラームリスト格納数
        public static string[] pblOmitAlarmList = new string[1001];         // アラーム除外リスト格納配列
        public static int intOmitAlarmListCnt;                              // アラーム除外リスト格納数
        public static string[,] pblNameConversionList = new string[21, 2];  // 名称変換リスト
        public static int intNameConversionListCnt;                         // 名称変換リスト格納数

        public static List<string> lstLoginData = new List<string>();       // ログインファイル格納リスト
        public static List<string> lstLinePlanData = new List<string>();    // ライン計画ファイル格納リスト
        
        public static List<string> lstCoopDepoData = new List<string>();    // 生協・デポ一覧ファイル格納リスト
        public static Dictionary<string, string> dicCoopCodeData;           // 生協コード変換用辞書
        public static Dictionary<string, string> dicDepoCodeData;           // デポコード変換用辞書

        //public static string[] pblCollatingData = new string[100001];           // 丁合指示データ格納配列（最大10万件）
        public static List<string> pblCollatingData = new List<string>();
        public static int intCollatingIndex;                                    // 丁合指示データ格納インデックス
        public static string[] pblTransactionData = new string[100001];         // 稼働データ格納配列（最大10万件）
        public static int intTransactionIndex;                                  // 稼働データ格納インデックス
        public static string[] pblTransactionDataForPrint = new string[100001]; // 稼働データ（印字用）格納配列（最大10万件）
        public static int intTransactionIndexForPrint;                          // 稼働データ（印字用）格納インデックス

        public static string[] pblReadCollating = new string[1000000];          // 読み込んだ丁合指示データを格納する配列
        public static string[] pblControlWorkArray = new string[100001];        // 制御ファイル作成用配列
        public static int intControlWorkIndex;
        public static string[] pblCenterBoxNum = new string[5001];              // センター箱数用配列
        public static int intCenterBoxNumIndex;
        public static int[] pblDistributionNum = new int[10001];                // 按分した束数を格納する配列
        public static int intDistributionNumIndex;


        // 帳票枚数オーバーリスト
        public static List<string> numberOverList = new List<string>();

        // （「企画週」-「配達日」-「配達曜日」-「単協」-「ｾﾝﾀｰ」-「ｺｰｽ」）（件数）
        public static string[,] pblCollArray = new string[9001, 2];       // 丁合指示データ分析結果格納配列
        public static int intCollArrayIndex;                              // 丁合指示データ分析結果格納インデックス
        public static string pblBeforeData;                               // 前回値データ格納変数
        public static string[,] pblCollShipArray = new string[50001, 2];  // 出荷表データ分析結果格納配列
        public static int intCollShipArrayIndex;                          // 出荷表データ分析結果格納インデックス
        public static string[] strCmbShippingArray = new string[1001];    // 出荷表印刷データ
        public static int intCmbShippingArrayIndex;                       // 出荷表印刷データインデックス

        public static string[] pblOrderArray = new string[30001];         // ジャーナルプリンタ印字用データ格納配列
        public static int intOrderArrayIndex;                             // ジャーナルプリンタ印字用データ格納配列インデックス
        public static int intOrderArrayCurrentIndex;                      // ジャーナルプリンタ印字用カレントインデックス

        public static string pblControlDataFileName;                      // 制御ファイル名称格納変数
        public static string pblFirstSeqCode;                             // ファイル内最初のＳＥＱコード
        public static string pblLastSeqCode;                              // ファイル内最後のＳＥＱコード
        public static string pblFirstUnionMemberCode;                     // ファイル内最初の組合員コード
        public static string pblLastUnionMemberCode;                      // ファイル内最後の組合員コード    

        public static string pblRunCoopName;                              // 運転画面表示用の生協名
        public static string pblRunTotalCount;                            // 運転画面表示用の全生産数

        // 「保守画面」→「システム設定」
        public const string DEF_MACHINE_NAME     = "号機名称";
        public const string DEF_LINE_NUMBER      = "ライン番号";
        public const string DEF_HDD_SPACE        = "ディスク空き容量";
        // COMポート１
        public const string DEF_COMPORT          = "COMポート名";
        public const string DEF_COM_SPEED        = "COM通信速度";
        public const string DEF_COM_DATA_LENGTH  = "COMデータ長";
        public const string DEF_COM_IS_PARITY    = "COMパリティ有無";
        public const string DEF_COM_PARITY_VAR   = "COMパリティ種別";
        public const string DEF_COM_STOPBIT      = "COMストップビット";
        // COMポート２
        public const string DEF_COMPORT2         = "COM2ポート名";
        public const string DEF_COM_SPEED2       = "COM2通信速度";
        public const string DEF_COM_DATA_LENGTH2 = "COM2データ長";
        public const string DEF_COM_IS_PARITY2   = "COM2パリティ有無";
        public const string DEF_COM_PARITY_VAR2  = "COM2パリティ種別";
        public const string DEF_COM_STOPBIT2     = "COM2ストップビット";
        public const string DEF_IS_DISP_LOGO     = "ロゴ表示";
        public const string DEF_PASSWORD         = "パスワード";
        public const string DEF_MAX_OF_BUNDLE    = "１箱最大束数";
        public const string DEF_AUTO_JUDGE_MODE  = "自動判定モード";
        public const string DEF_A4_PRINTER_NAME  = "Ａ４レーザーリンタ名称";
        public const string DEF_JR_PRINTER_NAME  = "ジャーナルプリンタ名称";
        public const string DEF_LOGSAVE_MONTH    = "ログ保存期間";
        public const string DEF_ERROR_DISP_TIME  = "エラー表示時間";
        // 「保守画面」→「フォルダ設定」
        public const string DEF_CHOAI_FOLDER           = "丁合指示データ読込ドライブ";
        public const string DEF_CONTROL_FOLDER         = "制御データ格納フォルダ";
        public const string DEF_SEISAN_FOLDER          = "生産ログ格納フォルダ";
        public const string DEF_FINAL_SEISAN_FOLDER    = "最終生産ログ格納フォルダ";
        public const string DEF_EVENT_FOLDER           = "イベントログ格納フォルダ";
        public const string DEF_FINAL_EVENT_FOLDER     = "最終イベントログ格納フォルダ";
        public const string DEF_INTERNAL_TRAN_FOLDER   = "内部実績ログ格納フォルダ";
        public const string DEF_OPERATION_TOTAL_FOLDER = "稼働管理PC集計フォルダ（生産ログ）";
        public const string DEF_OPERATION_EVENT_FOLDER = "稼働管理PC集計フォルダ（イベントログ）";
        public const string DEF_FORM_OUTPUT_FOLDER     = "帳票出力格納フォルダ";

        // 「保守画面」→「ブザー設定」
        public const string DEF_TONE_NAME1 = "ブザー音色１";
        public const string DEF_TONE_NAME2 = "ブザー音色２";
        public const string DEF_TONE_NAME3 = "ブザー音色３";
        public const string DEF_TONE_NAME4 = "ブザー音色４";
        public const string DEF_TONE_NAME5 = "ブザー音色５";
        public const string DEF_TONE_NAME6 = "ブザー音色６";
        public const string DEF_TONE_NAME7 = "ブザー音色７";
        public const string DEF_TONE_NAME8 = "ブザー音色８";
        public const string DEF_TONE_TIME1 = "ブザー発音時間１";
        public const string DEF_TONE_TIME2 = "ブザー発音時間２";
        public const string DEF_TONE_TIME3 = "ブザー発音時間３";
        public const string DEF_TONE_TIME4 = "ブザー発音時間４";
        public const string DEF_TONE_TIME5 = "ブザー発音時間５";
        public const string DEF_TONE_TIME6 = "ブザー発音時間６";
        public const string DEF_TONE_TIME7 = "ブザー発音時間７";
        public const string DEF_TONE_TIME8 = "ブザー発音時間８";

        public static string[] pblToneData = new string[33];             // 音色データ

        // 「保守画面」→「システム設定」
        public static string pblMachineName;
        public static string pblLineNumber;
        public static string pblHddSpace;
        // COMポート１
        public static string pblComPort;                  // COMポート名
        public static string pblComSpeed;                 // 通信速度
        public static string pblComDataLength;            // データ長（0：8bit／1：7bit）
        public static string pblComIsParity;              // パリティの有無（0：無効／1：有効）
        public static string pblComParityVar;             // パリティ種別（0：奇数／1：偶数）
        public static string pblComStopBit;               // ストップビット（0：1bit／1：2bit）
                                                          // COMポート２
        public static string pblComPort2;                 // COMポート名
        public static string pblComSpeed2;                // 通信速度
        public static string pblComDataLength2;           // データ長（0：8bit／1：7bit）
        public static string pblComIsParity2;             // パリティの有無（0：無効／1：有効）
        public static string pblComParityVar2;            // パリティ種別（0：奇数／1：偶数）
        public static string pblComStopBit2;              // ストップビット（0：1bit／1：2bit）
        public static string pblPassWord;                 // パスワード
        public static string pblMaxOfBundle;              // １箱最大束数 
        public static string pblIsAutoJudge;              // 自動判定モード（0：OFF／1：ON）
        public static string pblIsDispLogo;               // ロゴ表示
        public static string pblErrorDispTime;            // エラー表示時間
        public static string pblJournalPrinterName;       // ジャーナルプリンタ
        public static string pblReportPrinterName;        // Ａ４レーザープリンタ
                                                          // 「保守画面」→「フォルダ設定」
        public static string pblChoaiFolder;              // 丁合指示データ読込ドライブ
        public static string pblControlFolder;            // 制御データ格納フォルダ
        public static string pblSeisanFolder;             // 生産ログ格納フォルダ
        public static string pblFinalSeisanFolder;        // 最終生産ログ格納フォルダ
        public static string pblEventFolder;              // イベントログ格納フォルダ
        public static string pblFinalEventFolder;         // 最終イベントログ格納フォルダ
        public static string pblOperationPcTotallingFolder; // 稼働管理PC集計フォルダ（生産ログ）
        public static string pblOperationPcEventFolder;     // 稼働管理PC集計フォルダ（イベントログ）
        public static string pblFormOutPutFolder;         // 帳票出力フォルダ
        public static string pblInternalTranFolder;       // 内部実績ログ格納フォルダ
        public static string pblSaveLogMonth;             // ログ保存期間
                                                          // 「保守画面」→「ブザー設定」
        public static string pblToneName1;                // 音色１：コース区分け
        public static string pblToneName2;                // 音色２：センター区分け
        public static string pblToneName3;                // 音色３：単協区分け
        public static string pblToneName4;                // 音色４：排出ゲート開放
        public static string pblToneName5;                // 音色５：ＯＫ束
        public static string pblToneName6;                // 音色６：ＮＧ束
        public static string pblToneName7;                // 音色７：予備①
        public static string pblToneName8;                // 音色８：予備②
        public static string pblToneTime1;                // ブザー発音時間（音色１）
        public static string pblToneTime2;                // ブザー発音時間（音色２）
        public static string pblToneTime3;                // ブザー発音時間（音色３）
        public static string pblToneTime4;                // ブザー発音時間（音色４）
        public static string pblToneTime5;                // ブザー発音時間（音色５）
        public static string pblToneTime6;                // ブザー発音時間（音色６）
        public static string pblToneTime7;                // ブザー発音時間（音色７）
        public static string pblToneTime8;                // ブザー発音時間（音色８）
                                                          
        public static string pblShipMemoString1;            // 出荷表の添付文言１
        public static string pblShipMemoString2;            // 出荷表の添付文言２
        public static string pblShipMemoString3;            // 出荷表の添付文言３
        public static string pblNameMemoString1;            // 名札の添付文言１
        public static string pblNameMemoString2;            // 名札の添付文言２
        public static string pblNameMemoString3;            // 名札の添付文言３
                                                          
        public const string DEF_DRIVING_XPOS_DISP    = "運転画面横表示座標";
        public const string DEF_CONTEC_DIO_0808LY_ID = "CONTEC_DIO_0808LY_ID";
        public static int intXPosition;                     // 運転画面表示位置（Ｘ座標）
        public static string sDioName;                      // CONTEC DIOｰ0808LYのデバイスID

        public const string DEF_DUMMY_CREATE_COUNT1 = "区分け束数１束の作成ダミー束数";
        public const string DEF_DUMMY_CREATE_COUNT2 = "区分け束数２束の作成ダミー束数";
        public const string DEF_DUMMY_CREATE_COUNT3 = "区分け束数３束の作成ダミー束数";
        public const string DEF_DUMMY_CREATE_COUNT4 = "区分け束数４束の作成ダミー束数";
        public static int iDummyCreateCount1;               // 区分け束数１束の作成ダミー束数
        public static int iDummyCreateCount2;               // 区分け束数２束の作成ダミー束数
        public static int iDummyCreateCount3;               // 区分け束数３束の作成ダミー束数
        public static int iDummyCreateCount4;               // 区分け束数４束の作成ダミー束数

        public const string DEF_THICK_BOOKLET_USE_FEEDER = "厚物冊子使用フィーダー";
        public const string DEF_NUMBER_OF_BOOKS          = "厚物冊子（N）冊以上";
        public const string DEF_CARD_BOARD_USE_FEEDER    = "ボール紙使用フィーダー";

        public static int iThickBookletUseFeeder;           // 厚物冊子使用フィーダー
        public static int iNumberOfBooks;                   // 厚物冊子（N）冊以上
        public static int iCardboard;                       // ボール紙使用フィーダー

        public const string DEF_ERROR_MESSAGE_FILE_NAME = "ErrorMessage.txt";
        public static string[] pblErrorContents = new string[101];       // エラーメッセージ格納配列
        public static int pblErrorContentsIndex;            // エラーメッセージ個数

        public static string pblDriveLogFileName;           // 稼動ログファイル名（フルパス指定）
        public static string pblBackUpLogFileName1;         // 稼動バックアップ１ファイル名（フルパス指定）
        public static string pblBackUpLogFileName2;         // 稼動バックアップ２ファイル名（フルパス指定）
        public static string[] pblTranBufArray = new string[11];         // 稼動ログ格納用配列
        public static int pblTranBufIndex;                  // 稼動ログ格納配列用インデックス
        public static bool pblIsDriving;                    // 運転中フラグ

        // 「終了画面」
        public static bool blnShutDownFlag;            // シャットダウンフラグ

        public static bool blnReturnFlag;              // 終了メニューリターンフラグ
        public static bool blnIsOkPasswod;             // パスワードチェックフラグ
        public static object objSyncHist;
        public static object objSyncSeri;

        public static TimeSpan[] tsKadouTime = new TimeSpan[24];
        public static TimeSpan[] tsTeisiTime = new TimeSpan[24];
        public static TimeSpan tsStop5MinUnder;
        public static TimeSpan tsStop5MinOver;
        public static string strStartTime;
        public static string strEndTime;
        public static string strMaxRunTime;
        public static string strMaxRunFromTime;
        public static string strMaxRunToTime;
        public static string strRunTime;                 // 稼働時間
        public static string strSeisanCount;             // 生産数

        // 曜日,便,ｾﾝﾀｰｺｰﾄﾞ,ｺｰｽ,束数,組合員ｺｰﾄﾞ(先頭),組合員ｺｰﾄﾞ(最後),結束No.
        public static string[] pblOrderSetTransDetail = new string[9001];               // 注文明細セット転送明細書印字用データ格納配列
        public static int intOrderSetTransIndex;                                        // 注文明細セット転送明細書印字用データ格納配列インデックス
        public static string[] pblOrderSetTransDetailForSkewered = new string[2001];    // 注文明細セット転送明細書印字用データ格納配列（串刺し印刷用）
        // 棚替えするセンターコード（仮倉庫コード）
        public static List<string> lstTemporaryWareHouse = new List<string>();
        // 結束テーブル
        public static List<string> sListUnityTable = new List<string>();
        
        public static int iNumberOfProcesses;                           // 現在の生産数
        public static int iNumberOfNGProcesses;                         // 現在のＮＧ数
        public static double[] dblCountOfProcesses = new double[25];    // 時間毎の生産数


        #region 印刷文字データ
        public static string printCoopName   = "";   // 生協名
        public static string printCenterName = "";   // センター名
        public static string printPlanNo     = "";   // 企画号数
        public static string printDayOfWeek  = "";   // 曜日

        /// <summary>
        /// カゴ車数一覧（デポ毎）の印刷文字データ
        /// </summary>
        public struct ListOfNumberOfCarsInBasket
        {
            public string depoCode;                 // デポコード
            public string depoName;                 // デポ名
            public string setNumber;                // セット数
            public string preliminarySetNumber;     // 予備セット数
            public string basketCarNumber;          // カゴ車台数
            public string containerNumber;          // コンテナ数
            public string setNumberPercontainer;    // ｾｯﾄ数/ｺﾝﾃﾅ
            public string startNumber;              // スタート
            public string endNumber;                // エンド
        }
        public static List<ListOfNumberOfCarsInBasket> listOfNumberOfCarsInBasket = new List<ListOfNumberOfCarsInBasket>();

        /// <summary>
        /// 処理件数一覧（生協毎）の印刷文字データ
        /// </summary>
        public struct ListOfProcessedItems
        {
            public string coopCode;                 // 生協コード
            public string coopName;                 // 生協名
            public string setNumber;                // セット数
            public string preliminarySetNumber;     // 予備セット数
            public string basketCarNumber;          // カゴ車台数
            public string containerNumber;          // コンテナ数
            public string setNumberPercontainer;    // ｾｯﾄ数/ｺﾝﾃﾅ
            public string startNumber;              // スタート
            public string endNumber;                // エンド
        }
        public static List<ListOfProcessedItems> listOfProcessedItems = new List<ListOfProcessedItems>();

        /// <summary>
        /// 出荷確認表の印刷文字データ
        /// </summary>
        public struct ShipmentConfirmationTable
        {
            public string depoCode;                 // デポコード
            public string depoName;                 // デポ名
            public string basketCarNumber;          // カゴ車台数
        }
        public static List<ShipmentConfirmationTable> shipmentConfirmationTable = new List<ShipmentConfirmationTable>();

        /// <summary>
        /// 帳票枚数確認表の印刷文字データ
        /// </summary>
        public struct NumberOfConfirmationTable
        {
            public string coopCode;                 // 生協コード　：処理件数一覧（生協毎）
            public string coopName;                 // 生協名　　　：処理件数一覧（生協毎）
            public string numberOfFormST1;          // 帳票ST1
            public string numberOfFormST2;          // 帳票ST2
            public string numberOfFormST3;          // 帳票ST3
            public string numberOfFormST4;          // 帳票ST4
            public string numberOfFormST5;          // 帳票ST5
            public string numberOfFormST6;          // 帳票ST6
        }
        public static List<NumberOfConfirmationTable> numberOfConfirmationTable = new List<NumberOfConfirmationTable>();

        /// <summary>
        /// 吊札の印刷文字データ
        /// </summary>
        public struct HangingTagTable
        {
            public bool endMark;                    // ENDマーク
            public string coopCode;                 // 生協コード
            public string coopName;                 // 生協名
            public string planNumber;               // 企画号
            public string distributionDayFrom;      // 配布曜日（FROM）
            public string distributionDayTo;        // 配布曜日（TO）
            public string courseFrom;               // コース（FROM）
            public string courseTo;                 // コース（TO）
            public string depoCode;                 // デポコード
            public string depoName;                 // デポ名
            public string containerNumber;          // 積載コンテナ数
            public string setNumber;                // セット数／コンテナ
            public string numberOfCopies;           // 積載部数
            public string setOrderStart;            // セット順（スタート）
            public string setOrderEnd;              // セット順（エンド）
            public string basketCarSerialNumber;    // カゴ車連番
            public string basketCarSerialTotal;     // カゴ車連番総数
            public string lastUnionNumber;          // カゴ車チェックQR（最後の組合員番号+Z）
            public string pageNumber;               // ページ番号
        }
        public static List<HangingTagTable> hangingTagTable = new List<HangingTagTable>();

        public static int printIndex = 0;
        #endregion
    }
}
