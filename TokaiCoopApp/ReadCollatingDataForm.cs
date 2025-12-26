using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GreenCoopApp
{
    public partial class ReadCollatingDataForm : Form
    {
        public static string strPrtKikakukai;
        public static string strPrtTankyoName;
        public static string strPrtTankyoCode;
        public static string strPrtCenterName;
        public static string strPrtCenterCode;
        public static string strPrtCourseCode;
        public static string strPrtCourseBoxNum;
        public static string strPrtCenterBoxNum;

        public static string sCoopHeader;       // 生協名（帳票ヘッダー印字用）
        public static string sCenterHeader;     // センター名（帳票ヘッダー印字用）
        public static string sKikakuHeader;     // 企画（帳票ヘッダー印字用）
        public static string sYobiFromHeader;   // 曜日（FROM）（帳票ヘッダー印字用）
        public static string sYobiToHeader;     // 曜日（TO）（帳票ヘッダー印字用）

        private bool blnCancelFlag;                             // 表示処理キャンセルフラグ
        private int intForSesanDataIndex;                       // テスト用生産ログ作成インデックス
        private string[] strCmbOrderArray = new string[10001];  // オリコン印刷データ
        private int intCmbOrderArrayIndex;                      // オリコン印刷データインデックス
        private int[] iUnionNumForCenter = new int[120];        // 事業所単位の結束束数（120ﾙｰﾄNo分）
        private int iUnionNumForCenterIndex;
        private int iSpareSetSerialNumber;		                // 予備セット連番
        private string sTemporaryWareHouse;                     // センターコード（仮倉庫コード）（棚替えフラグチェックに使用する）
        private List<string> lstTemporaryWareHouse = new List<string>();
        private DotNetBarcode bc1 = new DotNetBarcode();        
        ActualLogDisplayForm form = new ActualLogDisplayForm(); // 実績ログ表示画面用フォーム

        public ReadCollatingDataForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 運転画面が表示されなかった時の対応処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadCollatingDataForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    {
                        this.Location = new Point(0, 0);
                        if (((MainForm)Owner).drivingForm.Visible == true)
                            ((MainForm)Owner).drivingForm.Location = new Point(1280, 0);
                        CommonModule.OutPutLogFile("■丁合指示データ読込画面にて「F1」キー押下");
                        break;
                    }
            }
        }

        /// <summary>
        /// 丁合指示データ読込み処理画面初期ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadCollatingDataForm_Load(Object sender, EventArgs e)
        {
            try
            {
                LblTitle.Text = "丁合指示データ　読込処理";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                ProgressBar1.Visible = false;
                BtnCancel.Visible = false;
                LblWaiting.Visible = false;
                LblProgress.Visible = false;
                LblCollatingFile.Text = "";
                ListResult.Visible = false;
                ListOrderList.Visible = false;
                ListShipping.Visible = false;

                LblStartRow.Text = "指示データ読込開始行：000001";

                CmbDummyCheck.Items.Clear();
                CmbDummyCheck.Items.Add("　　１束以下でダミー製品作成");
                CmbDummyCheck.Items.Add("　　２束以下でダミー製品作成");
                CmbDummyCheck.Items.Add("　　３束以下でダミー製品作成");
                CmbDummyCheck.Items.Add("　　４束以下でダミー製品作成");
                CmbDummyCheck.Items.Add("　　５束以下でダミー製品作成");
                CmbDummyCheck.Items.Add("　　ダミー製品作成は行わない");
                CmbDummyCheck.SelectedIndex = 1;

                // 印字タイプの設定
                CmbPrintType.Items.Clear();
                CmbPrintType.Items.Add("吊札");
                CmbPrintType.Items.Add("出荷ラベル");
                CmbPrintType.Items.Add("カゴ車数一覧(デポ毎)");
                CmbPrintType.Items.Add("処理件数一覧(生協毎)");
                CmbPrintType.Items.Add("出荷確認表");
                CmbPrintType.Items.Add("帳票枚数確認表");
                // 印字デバッグの為
                CmbPrintType.Items.Add("テスト印字");
                CmbPrintType.Items.Add("雛形印字");
                CmbPrintType.SelectedIndex = 0;
                // 丁合指示データ表示ヘッダーの作成
                DisplayHeader();
                // 結束テーブルファイルの読込
                LoadBindingTableFile();

                DTPDeliveryDate.Value = DateTime.Now.AddDays(3);

                CommonModule.OutPutLogFile("■「実績ログ表示画面」の表示");
                form.Show(this);
                form.Visible = false;

                LblConter.Text = "";
                // セレクティブデータ一覧表示
                DispSelectiveDataList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【ReadCollatingDataForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DispSelectiveDataList()
        {
            List<string> lstFileList = new List<string>();

            try
            {
                #region ListViewに画像を表示するサンプルコード
                //var smallImageList = new ImageList();
                //smallImageList.ImageSize = new Size(16, 16);
                //smallImageList.ColorDepth = ColorDepth.Depth32Bit;
                ////smallImageList.Images.Add("folder", Image.FromFile(@"C:\Images\folder.png"));
                ////smallImageList.Images.Add("file", Image.FromFile(@"C:\Images\file.png"));
                //smallImageList.Images.Add("pen", Image.FromFile(@"D:\Images\pen.png"));
                //smallImageList.Images.Add("trash", Image.FromFile(@"D:\Images\trash.png"));
                ////smallImageList.Images.Add("gifu", Image.FromFile(@"D:\Images\coopgifu.png"));
                //LsvSelectiveData.SmallImageList = smallImageList;

                //// 詳細表示の列定義
                //LsvSelectiveData.Columns.Add("Name", 200);
                //LsvSelectiveData.Columns.Add("Size", 80);
                //LsvSelectiveData.Columns.Add("Type", 120);

                //// 項目（1列目の左に画像が付く）
                //var itemA = new ListViewItem("ペン") { ImageKey = "pen" };
                //itemA.SubItems.Add("0 Byte");
                //itemA.SubItems.Add("Folder");

                //var itemB = new ListViewItem("ゴミ箱") { ImageKey = "trash" };
                //itemB.SubItems.Add("2.4 MB");
                //itemB.SubItems.Add("PDF");

                //LsvSelectiveData.Items.AddRange(new[] { itemA, itemB });
                //LsvSelectiveData.View = View.Details;
                //LsvSelectiveData.FullRowSelect = true;

                //return;

                //// 1) ImageList を準備
                //var largeImageList = new ImageList();
                //largeImageList.ImageSize = new Size(180, 50); // 任意サイズ
                //largeImageList.ColorDepth = ColorDepth.Depth32Bit;

                //// 画像の追加（ファイルから）
                //largeImageList.Images.Add("aichi", Image.FromFile(@"D:\Images\coopaichi.png"));
                //largeImageList.Images.Add("mie", Image.FromFile(@"D:\Images\coopmie.png"));
                //largeImageList.Images.Add("gifu", Image.FromFile(@"D:\Images\coopgifu.png"));

                //// 2) ListView に割り当て
                //LsvSelectiveData.LargeImageList = largeImageList;

                //// 3) 項目作成（画像キーを紐づけ）
                //var item1 = new ListViewItem("Aichi") { ImageKey = "aichi" };
                //item1.SubItems.Add("あいち");
                //var item2 = new ListViewItem("Mie") { ImageKey = "mie" };
                //item2.SubItems.Add("みえ");
                //var item3 = new ListViewItem("Gifu") { ImageKey = "gifu" };
                //item3.SubItems.Add("ぎふ");

                //LsvSelectiveData.Items.AddRange(new[] { item1, item2, item3 });

                //// 4) 表示モード
                ////LsvSelectiveData.View = View.LargeIcon;
                //LsvSelectiveData.View = View.Details;

                //return;

                //// 大きい画像を使う
                //LsvSelectiveData.LargeImageList = largeImageList;
                //LsvSelectiveData.View = View.Tile;

                //// タイルのサイズと行数（サブアイテムを表示）
                //LsvSelectiveData.TileSize = new Size(200, 80);

                //var tileItem = new ListViewItem("Camera-01") { ImageKey = "aichi" };
                //tileItem.SubItems.Add("Status: Online");
                //tileItem.SubItems.Add("FPS: 30");
                //LsvSelectiveData.Items.Add(tileItem);

                //return;
                #endregion

                LsvSelectiveData.View = View.Details;
                #region 列の新規作成
                ColumnHeader col01 = new ColumnHeader();
                ColumnHeader col02 = new ColumnHeader();
                ColumnHeader col03 = new ColumnHeader();
                ColumnHeader col04 = new ColumnHeader();
                ColumnHeader col05 = new ColumnHeader();
                ColumnHeader col06 = new ColumnHeader();
                #endregion
                #region 列名称設定
                col01.Text = "No.";
                col02.Text = "ファイル名";
                col03.Text = "生協名";
                col04.Text = "企画号数";
                col05.Text = "日目";
                col06.Text = "件数";
                #endregion
                #region 列揃え指定
                col01.TextAlign = HorizontalAlignment.Center;
                col02.TextAlign = HorizontalAlignment.Center;
                col03.TextAlign = HorizontalAlignment.Center;
                col04.TextAlign = HorizontalAlignment.Center;
                col05.TextAlign = HorizontalAlignment.Center;
                col06.TextAlign = HorizontalAlignment.Center;
                #endregion
                #region 列幅指定
                col01.Width = 80;       // No.
                col02.Width = 550;      // ファイル名
                col03.Width = 100;      // 生協名
                col04.Width = 100;      // 企画号数
                col05.Width = 100;      // 日目
                col06.Width = 100;      // 件数
                #endregion
                #region 列表示
                ColumnHeader[] colHeader = new[] { col01, col02, col03, col04, col05, col06 };
                LsvSelectiveData.Columns.AddRange(colHeader);
                #endregion

                int iNumber = 0;
                LsvSelectiveData.Items.Clear();
                // ファイル作成順
                foreach (string sTranFile in Directory.GetFiles(CommonModule.IncludeTrailingPathDelimiter(
                                                                      PubConstClass.pblChoaiFolder) ,
                                                                      "*", SearchOption.AllDirectories).OrderByDescending(f => File.GetLastWriteTime(f)))
                {
                    // 読込ファイルの行数を取得する
                    string[] lines = File.ReadAllLines(sTranFile);

                    string sCoop = "";
                    string sKikaku = "";
                    string sNichime = "";
                    // ファイルの内容を１行だけ読む
                    using (StreamReader sr = new StreamReader(sTranFile, Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {
                            string sData = sr.ReadLine();
                            sKikaku = sData.Substring(12, 2).Trim();            // 企画号数
                            sNichime = sData.Substring(14, 1).Trim();           // 日目
                            string sCoopCode = sData.Substring(15, 1).Trim();   // 生協コード
                            switch(sCoopCode) { 
                                case "1":
                                    sCoop = "ぎふ";
                                    break;
                                case "3":
                                    sCoop = "あいち";
                                    break;
                                case "5":
                                    sCoop = "みえ";
                                    break;
                                default:
                                    sCoop = "不明";
                                    break;
                            }
                            // １行だけ読む
                            break;  
                        }
                    }

                    string[] col = new string[6];
                    ListViewItem itm;
                    iNumber++;
                    col[0] = iNumber.ToString("000");   // No.
                    col[1] = sTranFile;                 // ファイル名
                    col[2] = sCoop;                     // 生協名
                    col[3] = sKikaku + "号";            // 企画号数
                    col[4] = sNichime + "日目";         // 日目
                    col[5] = lines.Length.ToString("#,###,##0") + "件";
                    // データの表示
                    itm = new ListViewItem(col);
                    LsvSelectiveData.Items.Add(itm);
                    LsvSelectiveData.Items[0].UseItemStyleForSubItems = false;
                    LsvSelectiveData.Select();
                    LsvSelectiveData.Items[0].EnsureVisible();                                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DispSelectiveData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 結束テーブルファイルの読込
        /// </summary>
        private void LoadBindingTableFile()
        {
            string sFileName;
            string sReadData;

            try
            {
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
                if (CmbUnityTable.Items.Count >= int.Parse(PubConstClass.pblMaxOfBundle))
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
        /// 丁合指示データ表示ヘッダーの作成
        /// </summary>
        private void DisplayHeader()
        {
            try
            {
                LstCollatingData.View = View.Details;

                #region 列の新規作成
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
                ColumnHeader col12 = new ColumnHeader();
                ColumnHeader col13 = new ColumnHeader();
                ColumnHeader col14 = new ColumnHeader();
                ColumnHeader col15 = new ColumnHeader();
                ColumnHeader col16 = new ColumnHeader();
                ColumnHeader col17 = new ColumnHeader();
                ColumnHeader col18 = new ColumnHeader();
                ColumnHeader col19 = new ColumnHeader();
                ColumnHeader col20 = new ColumnHeader();
                ColumnHeader col21 = new ColumnHeader();
                ColumnHeader col22 = new ColumnHeader();
                ColumnHeader col23 = new ColumnHeader();
                ColumnHeader col24 = new ColumnHeader();
                ColumnHeader col25 = new ColumnHeader();
                ColumnHeader col26 = new ColumnHeader();
                ColumnHeader col27 = new ColumnHeader();
                ColumnHeader col28 = new ColumnHeader();
                ColumnHeader col29 = new ColumnHeader();
                ColumnHeader col30 = new ColumnHeader();
                ColumnHeader col31 = new ColumnHeader();
                ColumnHeader col32 = new ColumnHeader();
                ColumnHeader col33 = new ColumnHeader();
                ColumnHeader col34 = new ColumnHeader();
                ColumnHeader col35 = new ColumnHeader();
                ColumnHeader col36 = new ColumnHeader();
                ColumnHeader col37 = new ColumnHeader();
                ColumnHeader col38 = new ColumnHeader();
                ColumnHeader col39 = new ColumnHeader();
                #endregion
                #region 列名称設定
                col01.Text = "No.";
                col02.Text = "作業コード";
                col03.Text = "シーケンスNo";
                col04.Text = "企画号数";
                col05.Text = "日目";
                col06.Text = "生協コード";
                col07.Text = "生協名称";
                col08.Text = "事業所コード";
                col09.Text = "事業所名";
                col10.Text = "コースコード";
                col11.Text = "組合員コード";
                col12.Text = "組合員名";
                col13.Text = "配送形態";
                col14.Text = "班コード";
                col15.Text = "班名";
                col16.Text = "配布日";
                col17.Text = "カゴ車切替";
                col18.Text = "コース仕分";
                col19.Text = "結束仕分";                
                col20.Text = "鞍1";
                col21.Text = "鞍2";
                col22.Text = "鞍3";
                col23.Text = "鞍4";
                col24.Text = "鞍5";
                col25.Text = "鞍6";
                col26.Text = "鞍7";
                col27.Text = "鞍8";
                col28.Text = "鞍9";
                col29.Text = "鞍10";
                col30.Text = "鞍11";
                col31.Text = "鞍12";
                col32.Text = "鞍13";
                col33.Text = "鞍14";
                col34.Text = "鞍15";
                col35.Text = "鞍16";
                col36.Text = "DT1";
                col37.Text = "DT2";
                col38.Text = "DT3";
                col39.Text = "DT4";
                #endregion
                #region 列揃え指定
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
                col12.TextAlign = HorizontalAlignment.Center;
                col13.TextAlign = HorizontalAlignment.Center;
                col14.TextAlign = HorizontalAlignment.Center;
                col15.TextAlign = HorizontalAlignment.Center;
                col16.TextAlign = HorizontalAlignment.Center;
                col17.TextAlign = HorizontalAlignment.Center;
                col18.TextAlign = HorizontalAlignment.Center;
                col19.TextAlign = HorizontalAlignment.Center;
                col20.TextAlign = HorizontalAlignment.Center;
                col21.TextAlign = HorizontalAlignment.Center;
                col22.TextAlign = HorizontalAlignment.Center;
                col23.TextAlign = HorizontalAlignment.Center;
                col24.TextAlign = HorizontalAlignment.Center;
                col25.TextAlign = HorizontalAlignment.Center;
                col26.TextAlign = HorizontalAlignment.Center;
                col27.TextAlign = HorizontalAlignment.Center;
                col28.TextAlign = HorizontalAlignment.Center;
                col29.TextAlign = HorizontalAlignment.Center;
                col30.TextAlign = HorizontalAlignment.Center;
                col31.TextAlign = HorizontalAlignment.Center;
                col32.TextAlign = HorizontalAlignment.Center;
                col33.TextAlign = HorizontalAlignment.Center;
                col34.TextAlign = HorizontalAlignment.Center;
                col35.TextAlign = HorizontalAlignment.Center;
                col36.TextAlign = HorizontalAlignment.Center;
                col37.TextAlign = HorizontalAlignment.Center;
                col38.TextAlign = HorizontalAlignment.Center;
                col39.TextAlign = HorizontalAlignment.Center;
                #endregion
                #region 列幅指定
                col01.Width = 70;       // No.
                col02.Width = 100;      // 作業コード
                col03.Width = 100;      // シーケンスNo
                col04.Width = 100;      // 企画号数
                col05.Width = 100;      // 日目
                col06.Width = 100;      // 生協コード
                col07.Width = 100;      // 生協名称
                col08.Width = 100;      // 事業所コード
                col09.Width = 100;      // 事業所名
                col10.Width = 100;      // コースコード
                col11.Width = 100;      // 組合員コード
                col12.Width = 100;      // 組合員名
                col13.Width = 100;      // 配送形態
                col14.Width = 100;      // 班コード
                col15.Width = 100;      // 班名
                col16.Width = 100;      // 配布日
                col17.Width = 100;      // カゴ車切替
                col18.Width = 100;      // コース仕分
                col19.Width = 100;      // 結束仕分              
                col20.Width = 80;       // 鞍1
                col21.Width = 80;       // 鞍2
                col22.Width = 60;       // 鞍3
                col23.Width = 80;       // 鞍4
                col24.Width = 80;       // 鞍5
                col25.Width = 80;       // 鞍6
                col26.Width = 80;       // 鞍7
                col27.Width = 80;       // 鞍8
                col28.Width = 80;       // 鞍9
                col29.Width = 80;       // 鞍10
                col30.Width = 80;       // 鞍11
                col31.Width = 80;       // 鞍12
                col32.Width = 80;       // 鞍13
                col33.Width = 80;       // 鞍14
                col34.Width = 80;       // 鞍15
                col35.Width = 80;       // 鞍16
                col36.Width = 80;       // DT1
                col37.Width = 80;       // DT2
                col38.Width = 80;       // DT3
                col39.Width = 80;       // DT4
                #endregion
                #region 列表示
                ColumnHeader[] colHeader = new[] { col01, col02, col03, col04, col05, col06, col07, col08, col09, col10,
                                                   col11, col12, col13, col14, col15, col16, col17, col18, col19, col20,
                                                   col21, col22, col23, col24, col25, col26, col27, col28, col29, col30,
                                                   col31, col32, col33, col34, col35, col36, col37, col38, col39
                                                   };
                LstCollatingData.Columns.AddRange(colHeader);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DisplayHeader】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 丁合指示データの内容表示処理
        /// </summary>
        /// <param name="strReadDataPath"></param>
        /// <returns></returns>
        private bool DispCollatingData(string strReadDataPath)
        {
            int intReadCounter;
            string strReadData;         // 読み込みデータ
            string[] strArray;
            int intStartRow;

            try
            {
                ProgressBar1.Visible = true;
                BtnCancel.Visible = true;
                BtnCancel.Update();
                LblWaiting.Visible = true;
                LblWaiting.Update();
                LblProgress.Visible = true;

                // 表示リストビューのクリア
                LstCollatingData.Items.Clear();

                // 丁合指示データ開始行の取得
                strArray = LblStartRow.Text.Split('：');
                intStartRow = Convert.ToInt32(strArray[1]) - 1;

                intReadCounter = 0;
                PubConstClass.pblFirstSeqCode = "";
                PubConstClass.pblLastSeqCode = "";

                strArray = strReadDataPath.Split('\\');
                string strYYYYMMDD = DateTime.Now.ToString("yyyyMMdd") + @"\";
                // コピー先フォルダの存在チェック
                if (Directory.Exists(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD) == false)
                {
                    Directory.CreateDirectory(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD);
                }
                // 読取り丁合指示データの上書きコピー
                File.Copy(strReadDataPath,
                          CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + strArray[strArray.Length - 1],
                          true);

                // ListView コントロールの再描画を禁止
                LstCollatingData.BeginUpdate();

                intReadCounter = 0;
                // 読込ファイルの行数を取得する
                string[] lines = File.ReadAllLines(strReadDataPath);

                string sLineLength = lines.Length.ToString("#,###,##0");
                blnCancelFlag = false;
                ProgressBar1.Minimum = 0;
                ProgressBar1.Maximum = lines.Length;

                PubConstClass.pblCollArray[0, 0] = "";
                PubConstClass.pblCollArray[0, 1] = "";
                PubConstClass.intCollArrayIndex = 0;
                PubConstClass.pblBeforeData = "";
                
                PubConstClass.pblCollatingData.Clear();
                PubConstClass.intCollatingIndex = 0;
                using (StreamReader sr = new StreamReader(strReadDataPath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strReadData = sr.ReadLine();
                        // 指示データ開始行のチェック
                        if (intStartRow > 0)
                            // 指示データ開始行分読み飛ばす
                            intStartRow -= 1;
                        else if (strReadData.Trim() != "")
                        {
                            PubConstClass.pblCollatingData.Add(strReadData);
                            intReadCounter += 1;
                        }
                    }
                }
                //////////////////////////////////////
                // 丁合指示データを逆順に並べ替える //
                //////////////////////////////////////
                PubConstClass.pblCollatingData.Reverse();
                
                int iIndex = 0;
                iSpareSetSerialNumber = 1;  // 予備セット連番の初期化
                PubConstClass.numberOverList.Clear();
                for (int N = 0; N < PubConstClass.pblCollatingData.Count; N++)
                {
                    DispSubCollatingData(PubConstClass.pblCollatingData[N]);
                    iIndex++;
                    if (iIndex % 100 == 0)
                    {
                        ProgressBar1.Value = iIndex;
                        LblProgress.Text = iIndex.ToString("#,###,##0") + "/" + sLineLength;
                    }
                    Application.DoEvents();
                    // 「キャンセル」ボタンクリックのチェック
                    if (blnCancelFlag == true)
                    {
                        break;                                                                                                                                                                                                                                                      
                    }
                }
                // 帳票枚数オーバーリストの確認
                if (PubConstClass.numberOverList.Count > 0)
                {
                    NumberOfFormOverList form = new NumberOfFormOverList();
                    form.ShowDialog(this);                    
                }

                ListResult.Items.Clear();
                string disp;
                for (var N = 0; N <= PubConstClass.pblDistributionNum.Length - 1; N++)
                {
                    PubConstClass.pblDistributionNum[N] = 0;
                }
                PubConstClass.intDistributionNumIndex = 0;
                for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intCollArrayIndex; intLoopCnt++)
                {

                    //CommonModule.OutPutLogFile("〓PubConstClass.pblCollArray[" + intLoopCnt.ToString("00") + ", 1]＝" + PubConstClass.pblCollArray[intLoopCnt, 1]);
                    if (PubConstClass.pblCollArray[intLoopCnt, 1] == "")
                    {
                        break;
                    }
                    disp = "【";
                    if (Convert.ToInt32(PubConstClass.pblCollArray[intLoopCnt, 1]) > Convert.ToInt32(LblMaxBundle.Text))
                    {
                        int tabaCnt = Convert.ToInt32(PubConstClass.pblCollArray[intLoopCnt, 1]);
                        while (true)
                        {
                            if (tabaCnt - Convert.ToInt32(LblMaxBundle.Text) > Convert.ToInt32(LblMaxBundle.Text))
                            {
                                // 最大束数より大きい場合
                                tabaCnt -= Convert.ToInt32(LblMaxBundle.Text);
                                disp = disp + LblMaxBundle.Text + ",";
                                PubConstClass.pblDistributionNum[PubConstClass.intDistributionNumIndex] = Convert.ToInt32(LblMaxBundle.Text);
                                PubConstClass.intDistributionNumIndex += 1;
                            }
                            else
                            {
                                // 最大束数より小さい場合
                                if (tabaCnt % 2 == 0)
                                {
                                    // 偶数
                                    disp = disp + (tabaCnt / (double)2).ToString() + "," + (tabaCnt / (double)2).ToString();
                                    PubConstClass.pblDistributionNum[PubConstClass.intDistributionNumIndex] = Convert.ToInt32(tabaCnt / (double)2);
                                    PubConstClass.intDistributionNumIndex += 1;
                                    PubConstClass.pblDistributionNum[PubConstClass.intDistributionNumIndex] = Convert.ToInt32(tabaCnt / (double)2);
                                    PubConstClass.intDistributionNumIndex += 1;
                                }
                                else
                                {
                                    // 奇数
                                    disp = disp + (((tabaCnt - 1) / (double)2) + 1).ToString() + "," + ((tabaCnt - 1) / (double)2).ToString();
                                    PubConstClass.pblDistributionNum[PubConstClass.intDistributionNumIndex] = Convert.ToInt32((tabaCnt - 1) / (double)2) + 1;
                                    PubConstClass.intDistributionNumIndex += 1;
                                    PubConstClass.pblDistributionNum[PubConstClass.intDistributionNumIndex] = Convert.ToInt32((tabaCnt - 1) / (double)2);
                                    PubConstClass.intDistributionNumIndex += 1;
                                }
                                disp += "】";
                                // 結束束数が最大束数より小さい場合にループから抜ける
                                break;
                            }
                        }
                        string sData = "【１】" + PubConstClass.pblCollArray[intLoopCnt, 0] + " = " + PubConstClass.pblCollArray[intLoopCnt, 1] + disp;
                        ListResult.Items.Add(sData);
                        CommonModule.OutPutLogFile(sData);
                        disp = "【";
                    }
                    else
                    {
                        string sData = "【２】" + PubConstClass.pblCollArray[intLoopCnt, 0] + " = " + PubConstClass.pblCollArray[intLoopCnt, 1];
                        ListResult.Items.Add(sData);
                        CommonModule.OutPutLogFile(sData);
                        disp = "【";
                        PubConstClass.pblDistributionNum[PubConstClass.intDistributionNumIndex] = Convert.ToInt32(PubConstClass.pblCollArray[intLoopCnt, 1]);
                        PubConstClass.intDistributionNumIndex += 1;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string sData = ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(sData, "【DispCollatingData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "【DispCollatingData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message, "【DispCollatingData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                ProgressBar1.Visible = false;
                BtnCancel.Visible = false;
                LblWaiting.Visible = false;
                LblProgress.Visible = false;
                // ListView コントロールの再描画
                LstCollatingData.EndUpdate();
            }
        }

        /// <summary>
        /// CSV形式の丁合指示データの表示処理（一行分）
        /// </summary>
        /// <param name="strCollatingData"></param>
        private void DispSubCollatingData(string strCollatingData)
        {
            string[] col = new string[51];
            ListViewItem itm;
            string strWorkData;
            string[] sArray;
            try
            {
                sArray = strCollatingData.Split(',');
                if (sArray.Length >= 45)
                {
                    // No,
                    col[0]  = (PubConstClass.intCollatingIndex + 1).ToString("000000");
                    col[1]  = sArray[0];                                    // 企画号数　　　　　 ⇒※ブレーク条件０
                    col[2]  = sArray[1];                                    // 生協作業順位
                    col[3]  = sArray[2].PadLeft(2).Replace(" ", "0");       // 生協コード（単協） ⇒※ブレーク条件０
                    col[4]  = sArray[3].Replace("\"", "");                  // 生協名（単協名）
                    col[5]  = sArray[4];                                    // デポNo.            ⇒※ブレーク条件０
                    col[6]  = sArray[5].Replace("\"", "");                  // デポ名
                    col[7]  = sArray[6].Replace("\"", "");                  // 地区名
                    col[8]  = sArray[7].Replace("\"", "");                  // 配布曜日           ⇒※ブレーク条件１
                    col[9]  = sArray[8];                                    // 配布方面　　　　   ⇒※ブレーク条件１
                    col[10] = sArray[9];                                    // 配布方面内No.　    ⇒※ブレーク条件１
                    col[11] = sArray[10];                                   // 配送順
                    col[12] = sArray[11];                                   // 配達次デポNo.
                    col[13] = sArray[12].Replace("\"", "");                 // 配達次曜日
                    col[14] = sArray[13];                                   // 配達次方面
                    col[15] = sArray[14];                                   // 配達次方面内No.
                    col[16] = sArray[15].PadLeft(8).Replace(" ", "0");      // 配布配班No.
                    col[17] = sArray[16].Replace("\"", "");                 // 配布班名
                    col[18] = sArray[17].PadLeft(8).Replace(" ", "0");      // 組合員No.
                    col[19] = sArray[18].Replace("\"", "");                 // 組合員名
                    col[20] = sArray[19];                                   // チラシ部ST1
                    col[21] = sArray[20];                                   // チラシ部ST2
                    col[22] = sArray[21];                                   // チラシ部ST3
                    col[23] = sArray[22];                                   // チラシ部ST4
                    col[24] = sArray[23];                                   // チラシ部ST5
                    col[25] = sArray[24];                                   // チラシ部ST6
                    col[26] = sArray[25];                                   // チラシ部ST7
                    col[27] = sArray[26];                                   // チラシ部ST8
                    col[28] = sArray[27];                                   // チラシ部ST9
                    col[29] = sArray[28];                                   // チラシ部ST10
                    col[30] = sArray[29];                                   // チラシ部ST11
                    col[31] = sArray[30];                                   // チラシ部ST12
                    col[32] = sArray[31];                                   // チラシ部ST13
                    col[33] = sArray[32];                                   // チラシ部ST14
                    col[34] = sArray[33];                                   // チラシ部ST15
                    col[35] = sArray[34];                                   // チラシ部ST16
                    col[36] = sArray[35];                                   // 帳票部ST1（0～10枚）
                    col[37] = sArray[36];                                   // 帳票部ST2（0～10枚）
                    col[38] = sArray[37];                                   // 帳票部ST3
                    col[39] = sArray[38];                                   // 帳票部ST4
                    col[40] = sArray[39];                                   // 帳票部ST5
                    col[41] = sArray[40];                                   // 帳票部ST6
                    col[42] = sArray[41];                                   // 予備１
                    col[43] = sArray[42];                                   // 予備２
                    col[44] = sArray[43].PadLeft(6).Replace(" ","0");       // SEQ
                    col[45] = sArray[44];                                   // センターコード

                    if (col[8]=="")
                    {
                        // 配布曜日が未セットの場合は「Z」を設定する
                        col[8] = " ";
                        CommonModule.OutPutLogFile("【DEBUG】配布曜日がセットされていないので「" + col[8] + "」をセット");
                    }
                    if (col[18]=="00000000")
                    {
                        //// 組合員No.が「00000000」の場合はSEQを設定する
                        //col[18] = sArray[43].PadLeft(8).Replace(" ","0");
                        // 組合員No.が「00000000」の場合は連番を付与する
                        col[18] = "9" + col[5] + iSpareSetSerialNumber.ToString("00000");
                        CommonModule.OutPutLogFile("【DEBUG】組合員番号が「00000000」なので「" + col[18] + "」をセット");
                        iSpareSetSerialNumber++;      // 予備セット連番
    }
                    string sNumberOverData;
                    if (int.Parse(col[36]) > 10 || int.Parse(col[37]) > 10)
                    {
                        sNumberOverData = col[44] + "," + col[18] + "," + col[19] + "," + col[36] + "," + col[37];
                        PubConstClass.numberOverList.Add(sNumberOverData);
                    }
                    #region 帳票用ヘッダーデータの取得
                    if (PubConstClass.intCollatingIndex == 0)
                    {
                        // 生協名（帳票ヘッダー印字用）
                        if (PubConstClass.dicCoopCodeData.ContainsKey(col[3]))
                        {
                            sCoopHeader = PubConstClass.dicCoopCodeData[col[3]];
                        }
                        else
                        {
                            sCoopHeader = "該当なし";
                        }
                        // センター名（帳票ヘッダー印字用）
                        if (col[45] == "1")
                        {
                            sCenterHeader = "凸版印刷 古賀工場";
                        }
                        else if (col[45] == "2")
                        {
                            sCenterHeader = "サンシャインワークス";
                        }
                        else
                        {
                            sCenterHeader = "サンシャインワークス（未定義）";
                        }
                        sKikakuHeader = col[1];     // 企画（帳票ヘッダー印字用）
                        sYobiFromHeader = col[8];   // 曜日（FROM）（帳票ヘッダー印字用）
                        sYobiToHeader = col[8];     // 曜日（TO）（帳票ヘッダー印字用）
                    }
                    #endregion

                    #region 「配達次方面」単位でカゴ車を分ける
                    // 「企画号数」-「生協ｺｰﾄﾞ」-「ﾃﾞﾎﾟNo.」-「配達次曜日」-「配達次方面」-「配達次方面内No.」単位の件数をカウントアップする
                    if (PubConstClass.pblBeforeData == "")
                    {
                        PubConstClass.pblBeforeData = col[1] + "-" + col[3] + "-" + col[5] + "-" + col[8] + "-" + col[9] + "-" + col[10];
                        PubConstClass.intCollArrayIndex = 0;
                        PubConstClass.pblCollArray[PubConstClass.intCollArrayIndex, 0] = PubConstClass.pblBeforeData;
                        PubConstClass.pblCollArray[PubConstClass.intCollArrayIndex, 1] = "1";
                    }
                    else
                        // 前回値と比較する
                        if (PubConstClass.pblBeforeData == col[1] + "-" + col[3] + "-" + col[5] + "-" + col[8] + "-" + col[9] + "-" + col[10])
                    {
                        // 前回値と同じ場合は件数を＋１する
                        PubConstClass.pblCollArray[PubConstClass.intCollArrayIndex, 1] = (Convert.ToInt32(PubConstClass.pblCollArray[PubConstClass.intCollArrayIndex, 1]) + 1).ToString();
                    }
                    else
                    {
                        // 前回値を入れ替える
                        PubConstClass.pblBeforeData = col[1] + "-" + col[3] + "-" + col[5] + "-" + col[8] + "-" + col[9] + "-" + col[10];
                        PubConstClass.intCollArrayIndex += 1;
                        PubConstClass.pblCollArray[PubConstClass.intCollArrayIndex, 0] = PubConstClass.pblBeforeData;
                        PubConstClass.pblCollArray[PubConstClass.intCollArrayIndex, 1] = "1";
                    }
                    #endregion

                    //if (sTemporaryWareHouse != col[45] || sTemporaryWareHouse == "")
                    if (sTemporaryWareHouse != col[5] || sTemporaryWareHouse == "")
                    {
                        // センターコード（仮倉庫ｺｰﾄﾞ→事業所ｺｰﾄﾞ）が前回と異なる場合または、空白の場合
                        // デポコードで判断する
                        //sTemporaryWareHouse = col[45];
                        sTemporaryWareHouse = col[5];
                        //lstTemporaryWareHouse.Add(col[45] + "," + GetOfficeNameFromOfficeCode(col[45]));
                        // とりあえず「単協コード,デポコード【単協名,デポ名】」を格納する
                        lstTemporaryWareHouse.Add(col[3] + "," + col[5] + "【" + GetCoopAndDepoName(col[3], col[5]) + "】");
                    }

                    // 最初と最後のｾｯﾄ順序番号と組合員コードを格納する
                    if (PubConstClass.pblFirstSeqCode == "")
                    {
                        PubConstClass.pblFirstSeqCode = col[44];
                        PubConstClass.pblFirstUnionMemberCode = col[18];
                    }
                    else
                    {
                        PubConstClass.pblLastSeqCode = col[44];
                        PubConstClass.pblLastUnionMemberCode = col[18];
                    }
                    //CommonModule.OutPutLogFile($"■■最初の組合員No.＝{PubConstClass.pblFirstUnionMemberCode}／SEQ＝{PubConstClass.pblFirstSeqCode}");
                    //CommonModule.OutPutLogFile($"■■最後の組合員No.＝{PubConstClass.pblLastUnionMemberCode}／SEQ＝{PubConstClass.pblLastSeqCode}");

                    // データの表示
                    itm = new ListViewItem(col);
                    LstCollatingData.Items.Add(itm);
                    LstCollatingData.Items[LstCollatingData.Items.Count - 1].UseItemStyleForSubItems = false;

                    strWorkData = "";
                    for (var intLoopCnt = 1; intLoopCnt <= 50; intLoopCnt++)
                    {
                        strWorkData += col[intLoopCnt] + ",";
                    }
                    //CommonModule.OutPutLogFile($"■■読込データ({PubConstClass.intCollatingIndex.ToString("00000")})：{strWorkData}");

                    PubConstClass.pblCollatingData[PubConstClass.intCollatingIndex] = strWorkData;
                    PubConstClass.intCollatingIndex += 1;
                    // 作業コードのインデックスにデータを格納する（検索で使用する）
                    PubConstClass.pblReadCollating[Convert.ToInt32(col[0])] = strWorkData;

                    if (LstCollatingData.Items.Count % 2 == 0)
                    {
                        // 偶数行の色反転
                        for (var intLoopCnt = 0; intLoopCnt <= 50; intLoopCnt++)
                        {
                            LstCollatingData.Items[LstCollatingData.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.FromArgb(200, 200, 230);
                        }
                    }
                }
                else
                {
                    CommonModule.OutPutLogFile($"■■読み飛ばしデータ（{PubConstClass.intCollatingIndex + 1}行目）：{strCollatingData}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DispSubCollatingData_ForCsvData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「丁合指示データ取込」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(Object sender, EventArgs e)
        {

            //string[] sArray;
            string sFolderPath;
            string sFileName;
            string sCollationInstructionFileName;

            if (ChkDataReadFromUSB.Checked)
            {
                // 「障害時データ読取」にチェックが入っている場合
                // 指定ファイルが存在しない場合は、ファイル選択ダイアログを表示する
                OpenFileDialog ofd = new OpenFileDialog();
                try
                {
                    CommonModule.OutPutLogFile("■「丁合指示データ取込」ボタンクリック");
                    // 初期表示するフォルダの指定（「空の文字列」の時は現在のディレクトリを表示）
                    //ofd.InitialDirectory = @"C:\";
                    // 「ファイルの種類」に表示される選択肢の指定
                    ofd.Filter = "TXTファイル(*.txt;*.TXT)|*.txt;*.TXT|すべてのファイル(*.*)|*.*";
                    // 「ファイルの種類」ではじめに「すべてのファイル(*.*)|*.*」を選択
                    ofd.FilterIndex = 2;
                    // タイトルを設定
                    ofd.Title = "丁合指示データファイルを選択してください";
                    // ダイアログボックスを閉じる前に現在のディレクトリを復元
                    ofd.RestoreDirectory = true;
                    // 存在しないファイルの名前が指定されたとき警告を表示
                    ofd.CheckFileExists = true;
                    // 存在しないパスが指定されたとき警告を表示
                    ofd.CheckPathExists = true;
                    // ダイアログを表示する
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // 各ボタン等を使用不可とする
                        BtnIsEnable(false);
                        // 「OK」ボタンがクリック（選択されたファイル名を表示）
                        LblCollatingFile.Text = ofd.FileName;
                        // 丁合指示データ読込処理
                        ReadCollationInstructionData(LblCollatingFile.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, "【BtnSelect_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // 各ボタンを使用可能とする
                    BtnIsEnable(true);
                }
            }
            else
            {
                // YK520P2.csv（サンシャインワークス）
                // YK520P.csv（凸版印刷 古賀工場）←とりあえすこちらでテスト
                //sCollationInstructionFileName = "YK520P2.csv";
                sCollationInstructionFileName = "YK520P.csv";
                sFolderPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblChoaiFolder);
                sFileName = sFolderPath + sCollationInstructionFileName;

                // 指定ファイル存在チェック
                if (File.Exists(sFileName))
                {
                    // 各ボタン等を使用不可とする
                    BtnIsEnable(false);
                    // 丁合指示データ読込処理
                    ReadCollationInstructionData(sFileName);
                }
                else
                {
                    MessageBox.Show("丁合指示データが見つかりません：" + sFileName,
                                    "丁合指示データ取込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // 各ボタンを使用可能とする
            BtnIsEnable(true);
        }

        /// <summary>
        /// 丁合指示データ読込処理
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        private bool ReadCollationInstructionData(string sFileName)
        {
            string[] strArray;
            string strFileName;

            try
            {
                CmbShipping.Items.Clear();
                CmbShippingRePrint.Items.Clear();

                // 選択されたファイル名を表示
                LblCollatingFile.Text = sFileName;

                // 仮倉庫コード作業領域クリア
                sTemporaryWareHouse = "";
                lstTemporaryWareHouse.Clear();

                //インスタンスの生成と同時に計測の開始
                var sw = Stopwatch.StartNew();
                CommonModule.OutPutLogFile("【読込処理開始】");

                ////////////////////////////////
                // 丁合指示データの内容を表示 //
                ////////////////////////////////
                if (DispCollatingData(LblCollatingFile.Text) == false)
                {
                    return false;
                }

                //計測の停止
                sw.Stop();
                //計測結果の出力
                Console.WriteLine("計測時間：{0}", sw.Elapsed);
                CommonModule.OutPutLogFile("【読込処理終了】" + sw.Elapsed);

                strArray = LblCollatingFile.Text.Split('\\');
                strFileName = strArray[strArray.Length - 1];
                strArray = strFileName.Split('_');
                //PubConstClass.pblControlDataFileName = strArray[0] + "_" + strArray[1] + "_" + strArray[2];
                PubConstClass.pblControlDataFileName = strArray[0];
                LblStartRow.Text = "指示データ読込開始行：000001";
                CommonModule.OutPutLogFile("【結束最大束数　　：" + LblFoldingCon.Text + "束】で丁合指示データ取込");
                CommonModule.OutPutLogFile("【カゴ車への積載数：" + LblMaxBundle.Text + "束】で丁合指示データ取込");
                //CommonModule.OutPutLogFile("【処理生協　　　　：" + CmbCoopName.Text + "】で丁合指示データ取込");
                // 「結束最大束数」「カゴ車への積載数」を変更不可とする
                //CmbUnityTable.Enabled = false;

                // 棚替えフラグ設定画面表示
                ShelfChangeForm shelfChangeForm = new ShelfChangeForm();
                shelfChangeForm.New(lstTemporaryWareHouse);
                shelfChangeForm.ShowDialog();

                BtnShelfChange.Visible = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【ReadCollationInstructionData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 「キャンセル」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(Object sender, EventArgs e)
        {
            // キャンセルフラグＯＮ
            blnCancelFlag = true;
        }

        /// <summary>
        /// 「戻る」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(Object sender, EventArgs e)
        {
            DialogResult result;
            try
            {
                result = MessageBox.Show("メインメニュー画面に戻りますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    Owner.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnBack_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「運転開始」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDriving_Click(Object sender, EventArgs e)
        {
            try
            {
                CommonModule.OutPutLogFile("■「運転開始」ボタンクリック");

                // ブザー設定情報の送信
                ((MainForm)Owner).SendBuzzerSetInfomation();

                if (PubConstClass.intCollatingIndex == 0)
                {
                    MessageBox.Show("「丁合指示データ取込」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CmbShipping.Items.Count == 0)
                {
                    MessageBox.Show("「制御データ作成」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sChkFolder = PubConstClass.pblOperationPcTotallingFolder;
                if (Directory.Exists(sChkFolder))
                {
                    if (Directory.Exists(CommonModule.IncludeTrailingPathDelimiter(sChkFolder) + PubConstClass.pblCurrentDate) == false)
                    {
                        // 処理日付のフォルダが存在しない場合は作成する
                        Directory.CreateDirectory(CommonModule.IncludeTrailingPathDelimiter(sChkFolder) + PubConstClass.pblCurrentDate);
                    }
                }
                else
                {
                    LblStatus.Text = "下記の稼働計PC集計フォルダが見つかりません" + Environment.NewLine + "（" + sChkFolder + "）";
                    LblStatus.Visible = true;
                }

                //PubConstClass.pblRunCoopName = "（" + CmbCoopName.Text + "）";
                PubConstClass.pblRunCoopName = "（【生協】）";
                PubConstClass.pblRunTotalCount = LstCollatingData.Items.Count.ToString("#,###,##0");

                // 運転画面はマルチディスプレイの21インチの方に表示する
                ((MainForm)Owner).drivingForm.Show(this);
                // 「丁合指示データ取込」ボタン使用不可とする
                BtnSelect.Enabled = false;
                BtnMakeControlDataFile.Enabled = false;
                BtnDriving.Text = "運転中";
                BtnDriving.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnDriving_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 隠しコマンド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblCollatingFile_DoubleClick(object sender, EventArgs e)
        {
            if (ListResult.Visible == false)
            {
                ListResult.Visible = true;
                ListOrderList.Visible = true;
                ListShipping.Visible = true;
            }
            else
            {
                ListResult.Visible = false;
                ListOrderList.Visible = false;
                ListShipping.Visible = false;
            }
        }

        /// <summary>
        /// 実績データの作成
        /// </summary>
        private void GetTransactionData()
        {
            string strStartWorkCode;    // 最初の作業ｺｰﾄﾞ
            string strEndWorkCode;      // 最後の作業ｺｰﾄﾞ
            string strPutData;
            int intBoxCount;            // 結束束カウンタ
            string strKuwakeType;       // 区分けタイプ

            string[] strNextArray;
            int intMaxOfBox;            // 結束区分け束数
            string[] strArray;
            string[] strChkArray;

            try
            {
                strStartWorkCode = "";
                strEndWorkCode = "";
                PubConstClass.intControlWorkIndex = 0;
                sTemporaryWareHouse = "";

                PubConstClass.intCenterBoxNumIndex = 0;
                PubConstClass.intTransactionIndex = 0;
                intBoxCount = 0;
                int intReWriteIndex = 0;
                int iIndex = 0;

                // １箱最大束数のセット
                intMaxOfBox = int.Parse(LblMaxBundle.Text);
                PubConstClass.pblMaxOfBundle = intMaxOfBox.ToString();
                intMaxOfBox = PubConstClass.pblDistributionNum[iIndex];

                // カゴ車への積載数
                int iFoldingConIndex = 0;
                int iFoldingCon = iPrintBunbo[iFoldingConIndex];

                // 厚物冊子使用フィーダー
                int iThickBookletUseFeeder = PubConstClass.iThickBookletUseFeeder;
                // 厚物冊子（N）冊以上で分ける
                int iNumberOfBook = PubConstClass.iNumberOfBooks;

                // 読み込んだ丁合指示データの件数分処理する
                for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intCollatingIndex - 1; intLoopCnt++)
                {
                    strArray = PubConstClass.pblCollatingData[intLoopCnt].Split(',');
                    strPutData = PubConstClass.pblCollatingData[intLoopCnt];

                    //// 先頭の行の値を採用する
                    //if (intLoopCnt == 0)
                    //{
                    //    // 企画回の取得
                    //    strPlanningWeek = strArray[0];
                    //    // 配達曜日の取得
                    //    strDeliveryWeekName = CommonModule.GetDeliveryWeek(strArray[7]) + "曜日";
                    //    strDeliveryWeekCode = strArray[7];
                    //}
                    // 事業所ｺｰﾄﾞ（デポコード）が変更になったかのチェック
                    if (sTemporaryWareHouse != strArray[4] || sTemporaryWareHouse == "")
                    {
                        // 事業所ｺｰﾄﾞ（デポコード）が前回と異なる場合または、空白の場合
                        sTemporaryWareHouse = strArray[4];
                    }

                    // 結束束単位の最初の作業コード（SEQ）をチェック
                    if (strStartWorkCode == "")
                    {
                        // 結束束単位の最初の作業コード（SEQ）を格納
                        strStartWorkCode = strArray[43];
                    }

                    // チラシ無し組合員のチェック
                    bool bIsAllZero = true;
                    for (int N = 20; N <= 40; N++)
                    {
                        int.TryParse(strArray[N], out int iLeafValue);

                        if (iLeafValue != 0)
                        {
                            // チラシ有り組合員
                            bIsAllZero = false;
                        }
                    }
                    if (bIsAllZero)
                    {
                        // チラシ無し組合員の場合
                        if (PubConstClass.iCardboard != 0)
                        {
                            // ボール紙使用フィーダーが設定されている場合は「1」セット
                            if (PubConstClass.iCardboard == 21)
                            {
                                // ST21 の場合
                                strArray[PubConstClass.iCardboard + 19] = "01";
                            }
                            else
                            {
                                // ST21 以外の場合
                                strArray[PubConstClass.iCardboard + 19] = "1";
                            }
                        }
                    }

                    intMaxOfBox -= 1;        // １箱最大束数のデクリメント
                    strKuwakeType = "";      // 区分けタイプのクリア
                                             // 結束区分けとｺｰｽ区分けの時に最後の作業コードを格納する
                    if (intMaxOfBox == 0)
                    {
                        // 結束区分け
                        strKuwakeType = "結束区分け";
                        // 最後の作業コード（SEQ）を格納する
                        strEndWorkCode = strArray[43];
                        iIndex += 1;
                        intMaxOfBox = PubConstClass.pblDistributionNum[iIndex];
                    }

                    // 厚物冊子使用フィーダーの設定チェック
                    if (PubConstClass.iThickBookletUseFeeder > 0)
                    {
                        // 厚物冊子使用フィーダーが設定されている
                        int iBoolletValue = int.Parse(strArray[PubConstClass.iThickBookletUseFeeder + 20]);
                        if (iBoolletValue > 0)
                        {
                            iNumberOfBook--;
                            if (iNumberOfBook == 0)
                            {
                                iNumberOfBook = PubConstClass.iNumberOfBooks;
                                strKuwakeType = "結束区分け";
                            }
                        }
                    }

                    // 最後のデータのチェック
                    if (intLoopCnt == PubConstClass.intCollatingIndex - 1)
                    {
                        #region 最後のデータ
                        ////////// 単協区分け //////////
                        intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                        #endregion
                    }
                    else
                    {
                        #region 最後のデータではない
                        strNextArray = PubConstClass.pblCollatingData[intLoopCnt + 1].Split(',');

                        ////////// 単協区分け //////////
                        if (strArray[2] == strNextArray[2])
                        {
                            // 次のﾃﾞｰﾀと単協ｺｰﾄﾞが同じ
                        }
                        else
                        {
                            intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                            if (intReWriteIndex == 1 | intReWriteIndex == 2 | intReWriteIndex == 3)
                            {
                                if (PubConstClass.intControlWorkIndex - intReWriteIndex >= 0)
                                {
                                    if (PubConstClass.intControlWorkIndex >= intReWriteIndex)
                                    {
                                        PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                    Convert.ToString(PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex]);

                                        PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                    Convert.ToString(PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]) +
                                                    ",単協区分けしない";
                                    }
                                }
                            }
                            // 単協ｺｰﾄﾞが変わった
                            if (strKuwakeType == "")
                            {
                                strKuwakeType = "単協区分け";
                            }
                            // 最後の作業コード（SEQ）を格納する
                            strEndWorkCode = strArray[43];
                        }

                        ////////// ｾﾝﾀｰ（デポ）区分け //////////
                        if (strArray[4] == strNextArray[4])
                        {
                            // 次のﾃﾞｰﾀとｾﾝﾀｰｺｰﾄﾞが同じ
                        }
                        else
                        {
                            ////////// ｾﾝﾀｰ（デポ）区分け //////////
                            intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                            if (intReWriteIndex == 1 | intReWriteIndex == 2 | intReWriteIndex == 3)
                            {
                                // チェック先の指示データの情報を取得する
                                if (PubConstClass.intControlWorkIndex - intReWriteIndex >= 0)
                                {
                                    strChkArray = PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex].Split(',');
                                    if (strChkArray[45] != "単協区分け" & strChkArray[45] != "コース区分け")
                                    {
                                        if (PubConstClass.intControlWorkIndex >= intReWriteIndex)
                                        {
                                            PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                        Convert.ToString(PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex]);

                                            PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                        Convert.ToString(PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]) +
                                                        ",センター区分けしない";
                                        }
                                    }
                                    else
                                    {
                                        CommonModule.OutPutLogFile("★<ｾﾝﾀｰ区分>書換先データ：" +
                                                                   PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]);
                                    }
                                }
                            }

                            // ｾﾝﾀｰｺｰﾄﾞ（デポコード）が変わった
                            if (strKuwakeType == "結束区分け")
                            {
                                strKuwakeType = "デポ区分け";
                            }
                            // 最後の作業コード（SEQ）を格納する
                            strEndWorkCode = strArray[43];
                        }

                        ////////// ｺｰｽ区分け //////////
                        // 結束区分けとｺｰｽ区分けの時に最後の作業コードを格納する
                        if ((strArray[7] + strArray[8] + strArray[9]) == (strNextArray[7] + strNextArray[8] + strNextArray[9]))
                        {
                            // 次のﾃﾞｰﾀと「ﾙｰﾄNo」が同じ
                            // 次のﾃﾞｰﾀとｺｰｽｺｰﾄﾞが同じ
                            //IsCourseSepFlag = false;
                        }
                        else
                        {
                            ////////// ｺｰｽ区分け //////////
                            intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                            if (intReWriteIndex == 1 | intReWriteIndex == 2 | intReWriteIndex == 3)
                            {
                                // 下記の条件は丁合指示データの先頭に１人コース及び２人コースが存在すると
                                // 「インデックが配列の境界外です」となるエラーを回避する為。
                                if (PubConstClass.intControlWorkIndex >= intReWriteIndex)
                                {
                                    //// チェック先の指示データの情報を取得する
                                    //strChkArray = PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex].Split(',');
                                    //if (strChkArray[45] != "単協区分け" &
                                    //    strChkArray[45] != "センター区分け" &
                                    //    strChkArray[45] != "コース区分け" &
                                    //    strArray[6] == strChkArray[6])
                                    //{
                                    //    PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                    //                Convert.ToString(PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex]);

                                    //    PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                    //                Convert.ToString(PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]) +
                                    //                ",コース区分けしない";
                                    //}
                                    //else
                                    //{
                                    //    CommonModule.OutPutLogFile("★<ｺｰｽ区分>書換先データ：" +
                                    //                               PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]);
                                    //}
                                }
                            }

                            // ｺｰｽｺｰﾄﾞが変わった
                            if (strKuwakeType == "結束区分け")
                            {
                                strKuwakeType = "コース区分け";
                            }

                            // 最後の作業コードを格納する
                            strEndWorkCode = strArray[43];
                        }
                        #endregion
                    }

                    // 結束束単位の最初の作業コードと最後の作業コードをチェック
                    if (strStartWorkCode != "" & strEndWorkCode != "")
                    {
                        // 結束区分けかｺｰｽ区分けが発生した時の処理
                        PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex] = strPutData +
                                                                                              strKuwakeType + "," +
                                                                                              strStartWorkCode + "," +
                                                                                              strEndWorkCode + "," +
                                                                                              intBoxCount.ToString() + ",,,";
                        PubConstClass.intTransactionIndex += 1;
                        intBoxCount += 1;
                        strKuwakeType = "";
                        strStartWorkCode = "";
                        strEndWorkCode = "";
                    }
                    else if (intLoopCnt == (PubConstClass.intCollatingIndex - 1))
                    {
                        // 最終束の処理
                        PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex] = strPutData +
                                                                                              "最終結束区分け" + "," +
                                                                                              strStartWorkCode + "," +
                                                                                              strArray[44] + "," +
                                                                                              intBoxCount.ToString() + ",,,";
                        PubConstClass.intTransactionIndex += 1;
                        intBoxCount += 1;
                    }
                    else
                    {
                        // 通常の処理
                        PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex] = strPutData +
                                                                                              "未" + "," +
                                                                                              strStartWorkCode + ",," +
                                                                                              intBoxCount.ToString() + ",,,";
                        PubConstClass.intTransactionIndex += 1;
                    }
                    CommonModule.OutPutLogFile($"【GetTransactionData】PubConstClass.pblTransactionData[{PubConstClass.intTransactionIndex - 1}]＝" +
                                               $"{PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex - 1]}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "【GetTransactionData】", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 制御データの書込処理
        /// </summary>
        private void WriteControlData()
        {
            string strStartSetOrder;    // セット順（From）
            string strEndSetOrder;      // セット順（To）
            string strStartUnionCd;     // 組合員CD（From）
            string strEndUnionCd;       // 組合員CD（To）

            string strKikakuNo;         // 企画番号
            string strYobi;             // 配達曜日
            string strLineNol;          // ラインNo.
            string strPutData;
            int intBoxCount;            // 結束束カウンタ
            string strKuwakeType;       // 区分けタイプ
            string strPutControlData;   // 制御データ書込レコード

            string[] strNextArray;
            int intMaxOfBox;            // 結束区分け束数
            string[] strArray;
            string[] strChkArray;
            string strPutDataPath;      // 制御データ書込用フルパス

            bool IsTankyoSepFlag;
            bool IsCenterSepFlag;
            bool IsCourseSepFlag;

            string sTanaChangeValue;

            int iDummyCreateCount;      // ダミー作成数カウンタ

            try
            {
                strStartSetOrder = "";
                strEndSetOrder = "";
                strStartUnionCd = "";
                strEndUnionCd = "";

                PubConstClass.intControlWorkIndex = 0;
                sTemporaryWareHouse = "";
                iDummyCreateCount = 0;

                // 制御データファイル名称の生成               
                int iIdx = LstCollatingData.Items.Count - 1;
                string sPlanning = LstCollatingData.Items[iIdx].SubItems[1].Text +
                                   LstCollatingData.Items[iIdx].SubItems[3].Text +
                                   LstCollatingData.Items[iIdx].SubItems[5].Text;
                string sDistribution = LstCollatingData.Items[iIdx].SubItems[8].Text +
                                       LstCollatingData.Items[iIdx].SubItems[9].Text +
                                       LstCollatingData.Items[iIdx].SubItems[10].Text;
                strPutDataPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblControlFolder) +
                                 "SIT" + PubConstClass.pblLineNumber + "_" +
                                 sPlanning + "_" +
                                 sDistribution + "_" +
                                 PubConstClass.pblControlDataFileName.Replace(".csv","") + ".sel";
                CommonModule.OutPutLogFile("■制御データ作成処理開始：" + strPutDataPath);

                PubConstClass.intCenterBoxNumIndex = 0;
                PubConstClass.intTransactionIndex = 0;
                intBoxCount = 0;
                int intReWriteIndex = 0;
                int iIndex = 0;
                // 制御データを上書モードで書き込む
                using (StreamWriter sw = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {
                    // １箱最大束数のセット
                    intMaxOfBox = int.Parse(LblMaxBundle.Text);
                    PubConstClass.pblMaxOfBundle = intMaxOfBox.ToString();
                    intMaxOfBox = PubConstClass.pblDistributionNum[iIndex];

                    // カゴ車への積載数
                    int iFoldingConIndex = 0;
                    int iFoldingCon = iPrintBunbo[iFoldingConIndex];
                    int iFoldingConSeq = 1;

                    // 厚物冊子使用フィーダー
                    int iThickBookletUseFeeder = PubConstClass.iThickBookletUseFeeder;
                    // 厚物冊子（N）冊以上で分ける
                    int iNumberOfBook = PubConstClass.iNumberOfBooks;

                    // 読み込んだ丁合指示データの件数分処理する
                    for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intCollatingIndex - 1; intLoopCnt++)
                    {
                        sTanaChangeValue = "0";             // 棚替えフラグ「0」
                        strArray = PubConstClass.pblCollatingData[intLoopCnt].Split(',');
                        strPutData = PubConstClass.pblCollatingData[intLoopCnt];

                        strPutControlData = "";                             // 書込データのクリア                      
                        strPutControlData += strArray[0];                   // 企画（2桁）
                        strPutControlData += strArray[2];                   // 生協コード（2桁）
                        strPutControlData += strArray[4];                   // デポNo（2桁）
                        strPutControlData += strArray[7].Replace("Z"," ");  // 配布曜日（1桁）
                        strPutControlData += strArray[8];                   // 配布方面（1桁）
                        strPutControlData += strArray[9];                   // 配布方面No（1桁）
                        strPutControlData += PubConstClass.pblLineNumber;   // ライン番号（1桁）
                        // 組合員番号（8桁）
                        strPutControlData += strArray[17].PadLeft(8).Replace(" ","0");
                        // セット順序番号（6桁）
                        strPutControlData += strArray[43].PadLeft(6).Replace(" ", "0");
                        strPutControlData += ",";                       // カンマ（1桁）

                        //// 先頭の行の値を採用する
                        //if (intLoopCnt == 0)
                        //{
                        //    // 企画回の取得
                        //    strPlanningWeek = strArray[0];
                        //    // 配達曜日の取得
                        //    strDeliveryWeekName = CommonModule.GetDeliveryWeek(strArray[7]);
                        //    strDeliveryWeekCode = strArray[7];
                        //}
                        // ﾃﾞﾎﾟNoが変更になったかのチェック
                        if (sTemporaryWareHouse != strArray[4] || sTemporaryWareHouse == "")
                        {
                            // デポNoが前回と異なる場合または、空白の場合
                            sTemporaryWareHouse = strArray[4];
                            if (PubConstClass.lstTemporaryWareHouse.Contains(strArray[4]))
                            {
                                sTanaChangeValue = "1";
                            }
                        }

                        // 結束束単位の最初のセット順をチェック
                        if (strStartSetOrder == "")
                        {
                            // 結束束単位の組合員CD（From）を格納
                            strStartUnionCd = strArray[17];
                            // 結束束単位のセット順（From）を格納
                            strStartSetOrder = strArray[43];
                        }

                        #region チラシ無し組合員のチェック
                        // チラシ無し組合員のチェック
                        bool bIsAllZero = true;
                        for (int N = 20; N <= 40; N++)
                        {
                            int.TryParse(strArray[N], out int iLeafValue);
                            if (iLeafValue != 0)
                            {
                                // チラシ有り組合員
                                bIsAllZero = false;
                            }
                        }
                        if (bIsAllZero)
                        {
                            // チラシ無し組合員の場合
                            if (PubConstClass.iCardboard != 0)
                            {
                                // ボール紙使用フィーダーが設定されている場合は「1」セット
                                if (PubConstClass.iCardboard == 21)
                                {
                                    // ST21 の場合
                                    strArray[PubConstClass.iCardboard + 19] = "01";
                                }
                                else
                                {
                                    // ST21 以外の場合
                                    strArray[PubConstClass.iCardboard + 19] = "1";
                                }
                            }
                        }
                        #endregion

                        #region 丁合指示（ST1～ST16）のセット                        
                        strPutControlData += strArray[19] + strArray[20] + strArray[21] + strArray[22] + strArray[23];
                        strPutControlData += strArray[24] + strArray[25] + strArray[26] + strArray[27] + strArray[28];
                        strPutControlData += strArray[29] + strArray[30] + strArray[31] + strArray[32] + strArray[33];
                        strPutControlData += strArray[34];
                        #endregion

                        #region 帳票指示のセット
                        //24,帳票指示 (SAF1)  ,2,42,43, 支部チラシ ※最大名寄せ10枚
                        //25,帳票指示 (SAF2)  ,2,44,45, 納品・請求書 ※最大名寄10枚
                        //26,帳票指示 (ST19)  ,1,46,46, 定期予約品 0：無 1：有
                        //27,帳票指示 (ST20)  ,1,47,47, りんご・みかん OCRW注文書 0：無 1：有
                        //28,帳票指示（ST21） ,1,48,48, OCR注文書 0：無 1：有
                        //29,帳票指示（ST22） ,1,49,49, 予備帳票 0：無 1：有
                        strPutControlData += strArray[35].PadLeft(2).Replace(" ", "0");
                        strPutControlData += strArray[36].PadLeft(2).Replace(" ", "0");
                        strPutControlData += strArray[37];
                        strPutControlData += strArray[38];
                        strPutControlData += strArray[39];
                        strPutControlData += strArray[40];
                        #endregion

                        #region 丁合指示（予備）のセット
                        //30,丁合指示（予備1）,1,50,50,0：無 1：有
                        //31,丁合指示（予備2）,1,51,51,0：無 1：有
                        //32,カンマ,1,52,52,
                        //strPutControlData += "0";
                        //strPutControlData += "0";
                        strPutControlData += strArray[41];
                        strPutControlData += strArray[42];
                        strPutControlData += ",";
                        #endregion

                        // （33）プロダクトチェンジ（棚変えﾌﾗｸﾞ）
                        strPutControlData += sTanaChangeValue;
                        // （34）ダミー製品指示
                        strPutControlData += "0";
                        // （35）チラシ無し組合員情報の設定
                        //strPutControlData += GetFlyerInformation(strPutData);
                        if (bIsAllZero)
                        {
                            // チラシ無し組合員
                            strPutControlData += "1";
                        }
                        else
                        {
                            // チラシ有り組合員
                            strPutControlData += "0";
                        }

                        #region 区分け（A,B,C,D）のセット
                        #endregion

                        intMaxOfBox -= 1;        // １箱最大束数のデクリメント
                        strKuwakeType = "";      // 区分けタイプのクリア
                        string sPrintData = "";
                        // 結束区分けとｺｰｽ区分けの時に最後の作業コードを格納する
                        if (intMaxOfBox == 0)
                        {
                            // （37）印字内容Ｂ-１（カゴ車に積載される結束束数の連番：分子）
                            //strPutControlData += iFoldingConSeq.ToString("00");
                            PubConstClass.pblTransactionDataForPrint[intLoopCnt] = strArray[13] + ",（" + iFoldingConSeq.ToString("00") + "/";

                            // （38）印字内容Ｂ-２（カゴ車に積載される結束束数：分母）
                            //strPutControlData += iFoldingCon.ToString("00");
                            PubConstClass.pblTransactionDataForPrint[intLoopCnt] += iFoldingCon.ToString("00") + "）," + strArray[47];

                            // 分子＋分母（PubConstClass.pblTransactionData[] 付加用）                           
                            sPrintData += iFoldingConSeq.ToString("00") + ",";
                            sPrintData += iFoldingCon.ToString("00") + ",";

                            iFoldingConSeq++;
                            if (iFoldingConSeq > iFoldingCon)
                            {
                                iFoldingConSeq = 1;
                                iFoldingConIndex++;
                                iFoldingCon = iPrintBunbo[iFoldingConIndex];
                            }

                            // 結束区分け
                            strPutControlData += "1";
                            //strPutData += "結束区分け,";
                            strKuwakeType = "結束区分け";
                            // 組合員CD（To）を格納する
                            strEndUnionCd = strArray[17];
                            // セット順（To）を格納する
                            strEndSetOrder = strArray[43];
                            iIndex += 1;
                            intMaxOfBox = PubConstClass.pblDistributionNum[iIndex];
                        }
                        else
                        {
                            // （37）印字内容Ｂ-１（カゴ車に積載される結束束数の連番：分子）
                            //strPutControlData += "--";
                            PubConstClass.pblTransactionDataForPrint[intLoopCnt] = strArray[13] + ",,";
                            // （38）印字内容Ｂ-２（カゴ車に積載される結束束数：分母）
                            //strPutControlData += "--";
                            PubConstClass.pblTransactionDataForPrint[intLoopCnt] += strArray[47];

                            // 分子＋分母（PubConstClass.pblTransactionData[] 付加用）                           
                            sPrintData += ",";
                            sPrintData += ",";

                            // 結束区分け束数リセット
                            strPutControlData += "0";
                        }

                        // 厚物冊子使用フィーダーの設定チェック
                        if (PubConstClass.iThickBookletUseFeeder > 0)
                        {
                            // 厚物冊子使用フィーダーが設定されている
                            int iBoolletValue = int.Parse(strArray[PubConstClass.iThickBookletUseFeeder + 20]);
                            if (iBoolletValue > 0)
                            {
                                iNumberOfBook--;
                                if (iNumberOfBook == 0)
                                {
                                    iNumberOfBook = PubConstClass.iNumberOfBooks;
                                    //strPutControlData += "*";
                                    // 区分けＡの書き換え
                                    strPutControlData = strPutControlData.Substring(0, strPutControlData.Length - 1) + "1";
                                    //strPutData += "厚物冊子区分け,";
                                    //strKuwakeType = "厚物冊子区分け";
                                    strPutData += "結束区分け,";
                                    strKuwakeType = "結束区分け";
                                }
                            }
                        }

                        // 最後のデータのチェック
                        if (intLoopCnt == PubConstClass.intCollatingIndex - 1)
                        {
                            #region 最後のデータ
                            strPutControlData += "001";
                            ////////// 単協区分け //////////
                            intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                            if (intReWriteIndex == 1 | intReWriteIndex == 2)
                            {
                                CommonModule.OutPutLogFile("★最後のデータ：" + intReWriteIndex.ToString() + "／" + strPutControlData);
                            }
                            #endregion
                        }
                        else
                        {
                            #region 最後のデータではない
                            strNextArray = PubConstClass.pblCollatingData[intLoopCnt + 1].Split(',');

                            ////////// 単協区分け //////////
                            if (strArray[2] == strNextArray[2])
                            {
                                // 次のﾃﾞｰﾀと単協ｺｰﾄﾞが同じ
                                IsTankyoSepFlag = false;
                            }
                            else
                            {
                                intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                                if (intReWriteIndex == 1 | intReWriteIndex == 2 | intReWriteIndex == 3)
                                {
                                    //CommonModule.OutPutLogFile("■単協区分け数＝" + intReWriteIndex.ToString());
                                    PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                Convert.ToString(PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex]);
                                    PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                Convert.ToString(PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]) +
                                                ",単協区分けしない";
                                    CommonModule.OutPutLogFile("★単協区分け：" + intReWriteIndex.ToString() + "／" + strPutControlData);
                                }
                                // 単協ｺｰﾄﾞが変わった
                                IsTankyoSepFlag = true;
                                if (strKuwakeType == "結束区分け")
                                {
                                    //strPutData += "単協区分け,";
                                    strKuwakeType = "単協区分け";
                                }
                                // 組合員CD（To）を格納する
                                strEndUnionCd = strArray[17];
                                // セット順（To）を格納する
                                strEndSetOrder = strArray[43];
                                //CommonModule.OutPutLogFile("▲最後のＳＥＱ（単協区分け）：" + strArray[18] + "／組合員コード：" + strArray[13]);
                            }

                            ////////// ｾﾝﾀｰ区分け（デポ区分け） //////////
                            if (strArray[4] == strNextArray[4])
                            {
                                // 次のﾃﾞｰﾀとｾﾝﾀｰｺｰﾄﾞが同じ
                                IsCenterSepFlag = false;
                            }
                            else
                            {
                                ////////// ｾﾝﾀｰ区分け //////////
                                intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                                if (intReWriteIndex == 1 | intReWriteIndex == 2 | intReWriteIndex == 3)
                                {
                                    // チェック先の指示データの情報を取得する
                                    strChkArray = PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex].Split(',');
                                    if (strChkArray[45] != "単協区分け" & strChkArray[45] != "コース区分け")
                                    {
                                        PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                    Convert.ToString(PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex]);
                                        PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                    Convert.ToString(PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]) +
                                                    ",センター区分けしない";
                                        CommonModule.OutPutLogFile("★センター区分け：" + intReWriteIndex.ToString() + "／" + strPutControlData);
                                    }
                                    else
                                    {
                                        CommonModule.OutPutLogFile("★<ｾﾝﾀｰ区分>書換先データ：" +
                                                                   PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]);
                                    }
                                }

                                // ｾﾝﾀｰｺｰﾄﾞが変わった
                                IsCenterSepFlag = true;
                                if (strKuwakeType == "結束区分け")
                                {
                                    //strPutData += "センター区分け,";
                                    strKuwakeType = "デポ区分け";
                                }
                                // 組合員CD（To）を格納する
                                strEndUnionCd = strArray[17];
                                // セット順（To）を格納する
                                strEndSetOrder = strArray[43];
                                //CommonModule.OutPutLogFile("▲最後のＳＥＱ（ｾﾝﾀｰ区分け）：" + strArray[18] + "／組合員コード：" + strArray[13]);
                            }

                            ////////// ｺｰｽ区分け //////////
                            // 結束区分けとｺｰｽ区分けの時に最後の作業コードを格納する
                            //if (strArray[8].Substring(0, 4) == strNextArray[8].Substring(0, 4))
                            if ((strArray[7]+ strArray[8]+ strArray[9]) == (strNextArray[7]+ strNextArray[8]+ strNextArray[9]))
                            {
                                // 次のﾃﾞｰﾀと「ﾙｰﾄNo」が同じ
                                // 次のﾃﾞｰﾀとｺｰｽｺｰﾄﾞが同じ
                                IsCourseSepFlag = false;
                            }
                            else
                            {
                                ////////// ｺｰｽ区分け //////////
                                intReWriteIndex = Convert.ToInt32(LblMaxBundle.Text) - intMaxOfBox;
                                if (intReWriteIndex == 1 | intReWriteIndex == 2 | intReWriteIndex == 3)
                                {
                                    // 下記の条件は丁合指示データの先頭に１人コース及び２人コースが存在すると
                                    // 「インデックが配列の境界外です」となるエラーを回避する為。
                                    if (PubConstClass.intControlWorkIndex >= intReWriteIndex)
                                    {
                                        // チェック先の指示データの情報を取得する
                                        strChkArray = PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex].Split(',');
                                        if (strChkArray[45] != "単協区分け" &
                                            strChkArray[45] != "センター区分け" &
                                            strChkArray[45] != "コース区分け" &
                                            strArray[6] == strChkArray[6])
                                        {
                                            PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                        Convert.ToString(PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex - intReWriteIndex]);
                                            PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex] =
                                                        Convert.ToString(PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]) +
                                                        ",コース区分けしない";
                                            //CommonModule.OutPutLogFile("★コース区分け：" + intReWriteIndex.ToString() + "／" + strPutControlData);
                                        }
                                        else
                                        {
                                            CommonModule.OutPutLogFile("★<ｺｰｽ区分>書換先データ：" +
                                                                       PubConstClass.pblTransactionData[PubConstClass.intControlWorkIndex - intReWriteIndex]);
                                        }
                                    }
                                }

                                // ｺｰｽｺｰﾄﾞが変わった
                                IsCourseSepFlag = true;
                                if (strKuwakeType == "結束区分け")
                                {
                                    //strPutData += "コース区分け,";
                                    strKuwakeType = "コース区分け";
                                }

                                // 組合員CD（To）を格納する
                                strEndUnionCd = strArray[17];
                                // セット順（To）を格納する
                                strEndSetOrder = strArray[43];
                                // 結束区分け束数リセット
                                //CommonModule.OutPutLogFile("▲最後のＳＥＱ（ｺｰｽ区分け）：" + strArray[18] + "／組合員コード：" + strArray[13]);
                            }

                            // 区分け情報の付加
                            if (IsTankyoSepFlag == true)
                            {
                                // 区分け無し
                                strPutControlData += "000";
                            }
                            else if (IsCenterSepFlag == true)
                            {
                                // センター区分け
                                strPutControlData += "010";
                            }
                            else if (IsCourseSepFlag == true)
                            {
                                // コース区分け（未使用予定）
                                strPutControlData += "100";
                            }
                            else
                            {
                                // 区分け無し
                                strPutControlData += "000";
                            }
                            #endregion
                        }

                        // 制御データの書込
                        sw.WriteLine(strPutControlData);
                        PubConstClass.pblControlWorkArray[PubConstClass.intControlWorkIndex] = strPutControlData;
                        PubConstClass.intControlWorkIndex += 1;

                        // 結束束単位の最初の作業コードと最後の作業コードをチェック
                        if (strStartSetOrder != "" & strEndSetOrder != "")
                        {
                            // 結束区分けかｺｰｽ区分けが発生した時の処理
                            PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex] = strPutData +
                                                                                                  strKuwakeType + "," +
                                                                                                  strStartUnionCd + "," +
                                                                                                  strEndUnionCd + "," +
                                                                                                  intBoxCount.ToString() + "," +
                                                                                                  strStartSetOrder + "," +
                                                                                                  strEndSetOrder + "," +                                                                                                  
                                                                                                  sPrintData;
                            CommonModule.OutPutLogFile($"WriteControlData【区分】PubConstClass.pblTransactionData[{PubConstClass.intTransactionIndex}]＝" +
                                                       $"{PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex]}");
                            PubConstClass.intTransactionIndex += 1;
                            intBoxCount += 1;
                            strKuwakeType = "";
                            strStartUnionCd = "";
                            strEndUnionCd = "";
                            strStartSetOrder = "";
                            strEndSetOrder = "";
                        }
                        else if (intLoopCnt == (PubConstClass.intCollatingIndex - 1))
                        {
                            // 最終束の処理
                            PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex] = strPutData + 
                                                                                                  "最終結束区分け" + "," +
                                                                                                  strStartUnionCd + "," +
                                                                                                  strEndUnionCd + "," +
                                                                                                  intBoxCount.ToString() + "," +
                                                                                                  strStartSetOrder + "," +
                                                                                                  strEndSetOrder + "," +
                                                                                                  sPrintData;
                            //CommonModule.OutPutLogFile("【最終】" + PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex]);
                            CommonModule.OutPutLogFile($"WriteControlData【最終】PubConstClass.pblTransactionData[{PubConstClass.intTransactionIndex}]＝" +
                                                       $"{PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex]}");
                            PubConstClass.intTransactionIndex += 1;
                            intBoxCount += 1;
                        }
                        else
                        {
                            // 通常の処理
                            PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex] = strPutData + 
                                                                                                  "未" + "," +
                                                                                                  strStartUnionCd + ",," +
                                                                                                  intBoxCount.ToString() + "," +
                                                                                                  strStartSetOrder + "," +
                                                                                                  "通常," +
                                                                                                  sPrintData;
                            //CommonModule.OutPutLogFile("【通常】" + PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex]);
                            CommonModule.OutPutLogFile($"WriteControlData【通常】PubConstClass.pblTransactionData[{PubConstClass.intTransactionIndex}]＝" +
                                                       $"{PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex]}");

                            PubConstClass.intTransactionIndex += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "【WriteControlData】", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 「結束束」単位の箱数を取得
        /// </summary>
        private void GetBundleBoxCount()
        {
            int intMaxOfBox;          // 結束区分け束数
            int intDivValue;
            int intModValue;
            int intSubLoop;

            int intCourseNum = 0;
            int intCourseNumAll;
            string[] strArray;
            string strName;

            try
            {
                // １箱最大束数のセット
                intMaxOfBox = Convert.ToInt32(LblMaxBundle.Text);

                ListOrderList.Items.Clear();
                PubConstClass.intCollShipArrayIndex = 0;
                PubConstClass.intOrderArrayIndex = 0;

                for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intCollArrayIndex; intLoopCnt++)
                {

                    // 「単協ｺｰﾄﾞ」「ｾﾝﾀｰｺｰﾄﾞ」「ｺｰｽｺｰﾄﾞ」の処理件数チェック
                    if (Convert.ToInt32(PubConstClass.pblCollArray[intLoopCnt, 1]) > intMaxOfBox)
                    {
                        // １箱最大束数から商を求める
                        intDivValue = Convert.ToInt32(Conversion.Fix(Convert.ToDouble(PubConstClass.pblCollArray[intLoopCnt, 1]) / (double)intMaxOfBox));
                        // １箱最大束数から余りを求める
                        intModValue = Convert.ToInt32(PubConstClass.pblCollArray[intLoopCnt, 1]) % intMaxOfBox;

                        intCourseNum = 1;
                        intCourseNumAll = intDivValue;

                        if (intModValue != 0)
                            intCourseNumAll += 1;

                        for (intSubLoop = 1; intSubLoop <= intDivValue; intSubLoop++)
                        {
                            // 【単協名,センター名】取得
                            strArray = PubConstClass.pblCollArray[intLoopCnt, 0].Split('-');
                            // hayakawa
                            if (Convert.ToInt32(PubConstClass.pblCollArray[intLoopCnt, 1]) < 3)
                            {
                                CommonModule.OutPutLogFile("★区分けしない：" + PubConstClass.pblCollArray[intLoopCnt, 0]);
                                CommonModule.OutPutLogFile("★区分けしない：" + PubConstClass.pblCollArray[intLoopCnt, 1]);
                            }

                            strName = "該当なし";
                            for (var K = 0; K <= PubConstClass.intMasterOfOrgCnt - 1; K++)
                            {
                                if (PubConstClass.pblMasterOfOrg[K, 0] == strArray[1] + strArray[3])
                                {
                                    strName = PubConstClass.pblMasterOfOrg[K, 1];
                                    break;
                                }
                            }
                            // 出荷表分析配列に格納
                            if (intSubLoop == intDivValue)
                            {
                                PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] = PubConstClass.pblCollArray[intLoopCnt, 0];
                                PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] = PubConstClass.pblDistributionNum[PubConstClass.intCollShipArrayIndex].ToString();
                                ListOrderList.Items.Add(PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1]);
                                PubConstClass.pblOrderArray[PubConstClass.intOrderArrayIndex] = PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] + "(" + intCourseNum + "/" + intCourseNumAll + ")" + "【" + strName + "】";
                                PubConstClass.intOrderArrayIndex += 1;
                                PubConstClass.intCollShipArrayIndex += 1;
                                intCourseNum += 1;
                            }
                            else
                            {
                                PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] = PubConstClass.pblCollArray[intLoopCnt, 0];
                                PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] = PubConstClass.pblDistributionNum[PubConstClass.intCollShipArrayIndex].ToString();
                                ListOrderList.Items.Add(PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1]);
                                PubConstClass.pblOrderArray[PubConstClass.intOrderArrayIndex] = PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] + "(" + intCourseNum + "/" + intCourseNumAll + ")" + "【" + strName + "】";
                                PubConstClass.intOrderArrayIndex += 1;
                                PubConstClass.intCollShipArrayIndex += 1;
                                intCourseNum += 1;
                            }
                        }
                        // 余り（端数）がある場合の処理
                        if (intModValue > 0)
                        {
                            // 【単協名,センター名】取得
                            strArray = PubConstClass.pblCollArray[intLoopCnt, 0].Split('-');
                            strName = "該当なし";
                            for (var K = 0; K <= PubConstClass.intMasterOfOrgCnt - 1; K++)
                            {
                                if (PubConstClass.pblMasterOfOrg[K, 0] == strArray[1] + strArray[3])
                                {
                                    strName = PubConstClass.pblMasterOfOrg[K, 1];
                                    break;
                                }
                            }

                            // 出荷表分析配列に格納
                            PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] = PubConstClass.pblCollArray[intLoopCnt, 0];
                            PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] = PubConstClass.pblDistributionNum[PubConstClass.intCollShipArrayIndex].ToString();
                            ListOrderList.Items.Add(PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1]);
                            PubConstClass.pblOrderArray[PubConstClass.intOrderArrayIndex] = PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] + "(" + intCourseNum + "/" + intCourseNumAll + ")" + "【" + strName + "】";
                            PubConstClass.intOrderArrayIndex += 1;
                            PubConstClass.intCollShipArrayIndex += 1;
                            intCourseNum += 1;
                        }
                    }
                    else
                    {
                        // 【単協名,センター名】取得
                        strArray = PubConstClass.pblCollArray[intLoopCnt, 0].Split('-');
                        strName = "該当なし";
                        for (var K = 0; K <= PubConstClass.intMasterOfOrgCnt - 1; K++)
                        {
                            if (PubConstClass.pblMasterOfOrg[K, 0] == strArray[1] + strArray[3])
                            {
                                strName = PubConstClass.pblMasterOfOrg[K, 1];
                                break;
                            }
                        }
                        // 出荷表分析配列に格納
                        PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] = PubConstClass.pblCollArray[intLoopCnt, 0];
                        PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] = PubConstClass.pblDistributionNum[PubConstClass.intCollShipArrayIndex].ToString();
                        ListOrderList.Items.Add(PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1]);
                        PubConstClass.pblOrderArray[PubConstClass.intOrderArrayIndex] = PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 0] + " = " + PubConstClass.pblCollShipArray[PubConstClass.intCollShipArrayIndex, 1] + "(1/1)" + "【" + strName + "】";
                        PubConstClass.intOrderArrayIndex += 1;
                        PubConstClass.intCollShipArrayIndex += 1;
                        intCourseNum += 1;
                    }
                }
                ListOrderList.Items.Add("【折コン用名札件数】" + ListOrderList.Items.Count + " 件");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetBundleBoxCount】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 各ボタン等の「使用可能／不可能」の制御
        /// </summary>
        /// <param name="blnIsTrue"></param>
        private void BtnIsEnable(bool blnIsTrue)
        {
            // 制御対象のボタン
            BtnSelect.Enabled = blnIsTrue;                  // 「丁合指示データ取込」ボタン
            BtnMakeControlDataFile.Enabled = blnIsTrue;     // 「制御データ作成」ボタン
            BtnDriving.Enabled = blnIsTrue;                 // 「運転開始」ボタン
            BtnBack.Enabled = blnIsTrue;                    // 「戻る」ボタン
            BtnAdjust.Enabled = blnIsTrue;                  // 「手直し品登録」ボタン            
            BtnSearch.Enabled = blnIsTrue;                  // 「検索」ボタン
            BtnQrRead.Enabled = blnIsTrue;                  // 「ＱＲ読込」ボタン
            BtnQrWrite.Enabled = blnIsTrue;                 // 「ＱＲ書込」ボタン
            BtnPrint.Enabled = blnIsTrue;                   // 「印刷」ボタン
            BtnDepotOrderSetting.Enabled = blnIsTrue;       // 「デポ順序設定」ボタン
            BtnShelfChange.Enabled = blnIsTrue;             // 「棚替え設定」ボタン
            BtnBasketCarCheck.Enabled = blnIsTrue;          // 「カゴ車確認」ボタン
            BtnCheckNumberOfForm.Enabled = blnIsTrue;       // 「帳票枚数確認」ボタン
            // 制御対象のコンボボックス
            CmbPrintType.Enabled = blnIsTrue;               // 「印字種類」コンボボックス
            CmbUnityTable.Enabled = blnIsTrue;              // 「結束束／積載数」コンボボックス
            CmbDummyCheck.Enabled = blnIsTrue;              // 「ダミー束作成」コンボボックス
            CmbPrintType.Refresh();
            CmbUnityTable.Refresh();
            CmbDummyCheck.Refresh();
            CmbShipping.Enabled = blnIsTrue;                // 「出荷表印字」コンボボックス
            CmbShippingRePrint.Enabled = blnIsTrue;         // 「出荷表再印字」コンボボックス
            CmbOrderRePrint.Enabled = blnIsTrue;            // 「オーダーリスト再印字」コンボボックス
        }

        /// <summary>
        /// 「センターコード」単位で箱数を取得
        /// </summary>
        private void GetCenterBoxCount()
        {
            string strName;               // 【単協名,センター名】の保存
            int intKagoCnt = 0;
            int intTankyoCenterCnt;
            string[] strArray;
            string[] strCmpArray;

            try
            {
                // 「単協コード」「センターコード」単位で箱数を取得
                intTankyoCenterCnt = 0;
                PubConstClass.pblBeforeData = "";
                lstShippingRePrint.Clear();

                ListShipping.Items.Clear();
                CmbShipping.Items.Clear();
                CmbShippingRePrint.Items.Clear();                

                // 出荷表印刷データのクリア
                for (var N = 0; N <= PubConstClass.strCmbShippingArray.Length - 1; N++)
                {
                    PubConstClass.strCmbShippingArray[N] = "";
                }
                PubConstClass.intCmbShippingArrayIndex = 0;

                int intShippingRePrintIndex = 1;

                for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intCollShipArrayIndex - 1; intLoopCnt++)
                {
                    if (PubConstClass.pblBeforeData == "")
                    {
                        PubConstClass.pblBeforeData = PubConstClass.pblCollShipArray[intLoopCnt, 0];
                        intTankyoCenterCnt = 1;
                    }
                    else
                    {
                        strArray = PubConstClass.pblBeforeData.Split('-');
                        strCmpArray = PubConstClass.pblCollShipArray[intLoopCnt, 0].Split('-');
                        if (PubConstClass.pblBeforeData == PubConstClass.pblCollShipArray[intLoopCnt, 0])
                            intTankyoCenterCnt += 1;
                        else
                        {
                            strArray = PubConstClass.pblBeforeData.Split('-');

                            strName = GetCoopAndDepoName(strArray[1], strArray[2]);

                            //CommonModule.OutPutLogFile("〓" + PubConstClass.pblBeforeData + " = " + intTankyoCenterCnt + "【" + strName + "】");
                            PubConstClass.pblCenterBoxNum[PubConstClass.intCenterBoxNumIndex] = PubConstClass.pblBeforeData.Replace("-", ",") + "," +
                                                                                                intTankyoCenterCnt + "," + strName;
                            PubConstClass.intCenterBoxNumIndex += 1;

                            ListShipping.Items.Add(PubConstClass.pblBeforeData + " = " + intTankyoCenterCnt + "【" + strName + "】");

                            // オリコン用データ作成用
                            strCmbOrderArray[intCmbOrderArrayIndex] = PubConstClass.pblBeforeData + "," +
                                                                      intTankyoCenterCnt + "," +
                                                                      intLoopCnt + "," +
                                                                      intKagoCnt + "," +
                                                                      strName;
                            intCmbOrderArrayIndex += 1;

                            // 生協単位のカゴ車の数を取得
                            intKagoCnt = Convert.ToInt32(Conversion.Fix(intTankyoCenterCnt / (double)PubConstClass.iMaxBoxOfRollBox));
                            if (intTankyoCenterCnt % PubConstClass.iMaxBoxOfRollBox != 0)
                            {
                                intKagoCnt += 1;
                            }

                            for (var intInLoopCnt = 1; intInLoopCnt <= intKagoCnt; intInLoopCnt++)
                            {
                                CmbShipping.Items.Add(PubConstClass.pblBeforeData + " = " + intTankyoCenterCnt + "(" + intInLoopCnt + "/" + intKagoCnt + ")" + "【" + strName + "】");
                            }

                            PubConstClass.pblBeforeData = PubConstClass.pblCollShipArray[intLoopCnt, 0];
                            intTankyoCenterCnt = 1;
                        }
                    }
                }

                strArray = PubConstClass.pblBeforeData.Split('-');
                strName = GetCoopAndDepoName(strArray[1], strArray[2]);

                // OutPutLogFile("〓" & PubConstClass.pblBeforeData & " = " & intTankyoCenterCnt & "【" & strName & "】")
                PubConstClass.pblCenterBoxNum[PubConstClass.intCenterBoxNumIndex] = PubConstClass.pblBeforeData.Replace("-", ",") + "," + intTankyoCenterCnt + "," + strName;
                PubConstClass.intCenterBoxNumIndex += 1;

                ListShipping.Items.Add(PubConstClass.pblBeforeData + " = " + intTankyoCenterCnt + "【" + strName + "】");

                // 生協単位のカゴ車の数を取得
                intKagoCnt = Convert.ToInt32(Conversion.Fix(intTankyoCenterCnt / (double)PubConstClass.iMaxBoxOfRollBox));
                if (intTankyoCenterCnt % PubConstClass.iMaxBoxOfRollBox != 0)
                {
                    intKagoCnt += 1;
                }
                for (var intInLoopCnt = 1; intInLoopCnt <= intKagoCnt; intInLoopCnt++)
                {
                    CmbShipping.Items.Add(PubConstClass.pblBeforeData + " = " + intTankyoCenterCnt + "(" + intInLoopCnt + "/" + intKagoCnt + ")" + "【" + strName + "】");
                    // オリコン用データ作成用
                    strCmbOrderArray[intCmbOrderArrayIndex] = PubConstClass.pblBeforeData + "," + intTankyoCenterCnt + "," + intInLoopCnt + "," + intKagoCnt + "," + strName;
                    intCmbOrderArrayIndex += 1;
                }

                int intKagoCntMod;
                PubConstClass.pblBeforeData = "";
                for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intCollShipArrayIndex - 1; intLoopCnt++)
                {
                    if (PubConstClass.pblBeforeData == "")
                    {
                        PubConstClass.pblBeforeData = PubConstClass.pblCollShipArray[intLoopCnt, 0];
                        intTankyoCenterCnt = 1;
                    }
                    else
                    {
                        strArray = PubConstClass.pblBeforeData.Split('-');
                        strCmpArray = PubConstClass.pblCollShipArray[intLoopCnt, 0].Split('-');
                        // If PubConstClass.pblBeforeData = PubConstClass.pblCollShipArray(intLoopCnt, 0) Then
                        //if (strArray[0] + strArray[1] + strArray[2] + strArray[3] == strCmpArray[0] + strCmpArray[1] + strCmpArray[2] + strCmpArray[3])
                        if (strArray[0] + strArray[1] + strArray[2] == strCmpArray[0] + strCmpArray[1] + strCmpArray[2])
                            intTankyoCenterCnt += 1;
                        else
                        {
                            strArray = PubConstClass.pblBeforeData.Split('-');
                            strName = GetCoopAndDepoName(strArray[1], strArray[2]);

                            // 単協単位のカゴ車の数を取得
                            intKagoCntMod = 0;
                            intKagoCnt = Convert.ToInt32(Conversion.Fix(intTankyoCenterCnt / (double)PubConstClass.iMaxBoxOfRollBox));
                            if (intTankyoCenterCnt % PubConstClass.iMaxBoxOfRollBox != 0)
                            {
                                intKagoCnt += 1;
                                intKagoCntMod = intTankyoCenterCnt % PubConstClass.iMaxBoxOfRollBox;
                            }
                            for (var intInLoopCnt = 1; intInLoopCnt <= intKagoCnt; intInLoopCnt++)
                            {
                                if ((intInLoopCnt == intKagoCnt) & (intKagoCntMod > 0))
                                {
                                    CmbShippingRePrint.Items.Add(intShippingRePrintIndex + "頁 → ｾﾝﾀｰ箱数：" +
                                                                 intKagoCntMod + "箱(" + intInLoopCnt + "/" + intKagoCnt + ")" + "【" + strName + "】");
                                }
                                else
                                {
                                    CmbShippingRePrint.Items.Add(intShippingRePrintIndex + "頁 → ｾﾝﾀｰ箱数：" +
                                                                 PubConstClass.iMaxBoxOfRollBox + "箱(" + intInLoopCnt + "/" + intKagoCnt + ")" + "【" + strName + "】");
                                }
                                intShippingRePrintIndex += 1;
                                // 出荷表印刷データの格納
                                PubConstClass.strCmbShippingArray[PubConstClass.intCmbShippingArrayIndex] = PubConstClass.pblBeforeData + "," +
                                                                                                            intTankyoCenterCnt + "," +
                                                                                                            intInLoopCnt + "," +
                                                                                                            intKagoCnt + "," +
                                                                                                            strName;
                                PubConstClass.intCmbShippingArrayIndex += 1;
                            }
                            PubConstClass.pblBeforeData = PubConstClass.pblCollShipArray[intLoopCnt, 0];
                            intTankyoCenterCnt = 1;
                        }
                    }
                }
                strArray = PubConstClass.pblBeforeData.Split('-');
                strName = GetCoopAndDepoName(strArray[1], strArray[2]);
                CommonModule.OutPutLogFile("☆" + PubConstClass.pblBeforeData + " = " + intTankyoCenterCnt + "【" + strName + "】");

                // 単協単位のカゴ車の数を取得
                intKagoCntMod = 0;
                intKagoCnt = Convert.ToInt32(Conversion.Fix(intTankyoCenterCnt / (double)PubConstClass.iMaxBoxOfRollBox));
                if (intTankyoCenterCnt % PubConstClass.iMaxBoxOfRollBox != 0)
                {
                    intKagoCnt += 1;
                    intKagoCntMod = intTankyoCenterCnt % PubConstClass.iMaxBoxOfRollBox;
                }
                for (var intInLoopCnt = 1; intInLoopCnt <= intKagoCnt; intInLoopCnt++)
                {
                    if ((intInLoopCnt == intKagoCnt) & (intKagoCntMod > 0))
                    {
                        CmbShippingRePrint.Items.Add(intShippingRePrintIndex + "頁 → ｾﾝﾀｰ箱数：" +
                                                     intKagoCntMod + "箱(" + intInLoopCnt + "/" + intKagoCnt + ")" + "【" + strName + "】");
                    }
                    else
                    {
                        CmbShippingRePrint.Items.Add(intShippingRePrintIndex + "頁 → ｾﾝﾀｰ箱数：" +
                                                     PubConstClass.iMaxBoxOfRollBox + "箱(" + intInLoopCnt + "/" + intKagoCnt + ")" + "【" + strName + "】");
                    }
                    intShippingRePrintIndex += 1;
                    // 出荷表印刷データの格納
                    PubConstClass.strCmbShippingArray[PubConstClass.intCmbShippingArrayIndex] = PubConstClass.pblBeforeData + "," +
                                                                                                intTankyoCenterCnt + "," +
                                                                                                intInLoopCnt + "," +
                                                                                                intKagoCnt + "," +
                                                                                                strName + ",フェーズ２";
                    PubConstClass.intCmbShippingArrayIndex += 1;
                }

                CmbShipping.SelectedIndex = CmbShipping.Items.Count - 1;
                CmbShippingRePrint.SelectedIndex = CmbShippingRePrint.Items.Count - 1;

                foreach(var s in CmbShippingRePrint.Items)
                {
                    // 1頁 → ｾﾝﾀｰ箱数：20箱(1/4)【ふくおか,福岡なか】
                    string[] sAry = s.ToString().Split('：');
                    string sData = sAry[1];
                    sAry = sData.Split('【');
                    sData = sAry[0].Replace("箱",",").Replace("(", "").Replace("/", ",").Replace(")", ",");
                    lstShippingRePrint.Add(sData);

                    CommonModule.OutPutLogFile("CmbShippingRePrint.Items：" + s.ToString());
                    CommonModule.OutPutLogFile("sData：" + sData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetCenterBoxCount】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// 生協コード、デポコードから生協名、デポ名を取得する
        /// </summary>
        /// <param name="sCoopCode"></param>
        /// <param name="sDepoName"></param>
        /// <returns></returns>
        private string GetCoopAndDepoName(string sCoopCode, string sDepoName)
        {
            string strName;
            try
            {
                if (PubConstClass.dicCoopCodeData.ContainsKey(sCoopCode))
                {
                    strName = PubConstClass.dicCoopCodeData[sCoopCode] + ",";
                }
                else
                {
                    strName = "生協該当なし,";
                }
                if (PubConstClass.dicDepoCodeData.ContainsKey(sDepoName))
                {
                    strName += PubConstClass.dicDepoCodeData[sDepoName];
                }
                else
                {
                    strName += "デポ該当なし";
                }
                return strName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetCoopAndDepoName】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "生協該当なし,デポ該当なし";
            }
        }
        /// <summary>
        /// 「コースコード」単位で箱数を取得
        /// </summary>
        private void GetCourseBoxCount()
        {
            string strBeforeData;
            int intCenterBoxNum;
            int intCenterCount;
            string[] strOrderRePrint;
            string[] strArray;
            string[] strCmpArray;

            string[] sTabaArray;
            string[] sBundleArray;
            int intLoopCnt;
            int iUnionIndex;

            ArrayList iMaxCount = new ArrayList();

            try
            {
                CmbOrderRePrint.Items.Clear();
                intCenterBoxNum = 1;
                strBeforeData = "";
                iMaxCount.Clear();
                iUnionNumForCenterIndex = 0;
                for (int N = 0; N < iUnionNumForCenter.Length; N++)
                {
                    iUnionNumForCenter[N] = 0;
                }
                iUnionIndex = 0;
                PubConstClass.intOrderSetTransIndex = 0;
                for (intLoopCnt = 0; intLoopCnt <= PubConstClass.intOrderArrayIndex - 1; intLoopCnt++)
                {
                    if (strBeforeData == "")
                    {
                        // 前回値が無い場合（一番最初の処理）
                        strBeforeData = PubConstClass.pblOrderArray[intLoopCnt];
                        strArray = PubConstClass.pblOrderArray[intLoopCnt].Replace(" = ", "-").Split('-');
                        for (var intLoopInCnt = 0; intLoopInCnt <= intCmbOrderArrayIndex - 1; intLoopInCnt++)
                        {
                            strCmpArray = strCmbOrderArray[intLoopInCnt].Replace(",", "-").Split('-');
                            // センター＋コースが異なるかのチェック
                            if ((strArray[2] + strArray[3]) == (strCmpArray[2] + strCmpArray[3]))
                            {
                                // Public Const DEF_MAX_BOX_OF_ROLLBOX = 14
                                intCenterCount = Convert.ToInt32(Conversion.Fix(intCenterBoxNum / (double)PubConstClass.iMaxBoxOfRollBox));
                                if (intCenterBoxNum % PubConstClass.iMaxBoxOfRollBox != 0)
                                {
                                    intCenterCount += 1;
                                }

                                strOrderRePrint = PubConstClass.pblOrderArray[intLoopCnt].Split('=');
                                CmbOrderRePrint.Items.Add(intLoopCnt + 1 + "頁 → ｺｰｽ箱数：" + strOrderRePrint[1].Trim().Replace("(", "束(") +
                                                                           " ｾﾝﾀｰ箱数：" + strCmpArray[5] + "箱(" + intCenterBoxNum + "/");
                                PubConstClass.pblOrderArray[intLoopCnt] = PubConstClass.pblOrderArray[intLoopCnt].Replace("-", ",").
                                                                                                                  Replace(" = ", ",").
                                                                                                                  Replace("(", ",").
                                                                                                                  Replace("/", ",").
                                                                                                                  Replace(")【", ",").
                                                                                                                  Replace("】", ",") +
                                                                                                                  strCmpArray[5] + "," +
                                                                                                                  intCenterBoxNum + ",";
                                // センターコード単位の個数
                                iUnionNumForCenter[iUnionNumForCenterIndex] += 1;

                                sTabaArray = PubConstClass.pblOrderArray[intLoopCnt].Split(',');
                                //iUnionIndex += int.Parse(sTabaArray[5]) - 1;
                                iUnionIndex += 1;
                                CommonModule.OutPutLogFile("【０】【iUnionIndex】" + iUnionIndex);
                                sBundleArray = PubConstClass.pblCollatingData[iUnionIndex].Split(',');
                                // 曜日,便,ｾﾝﾀｰｺｰﾄﾞ,ｺｰｽ,束数,組合員ｺｰﾄﾞ(先頭),組合員ｺｰﾄﾞ(最後),結束No.
                                PubConstClass.pblOrderSetTransDetail[intLoopCnt] = sTabaArray[2] + "," +
                                                                                   sTabaArray[0] + "," +
                                                                                   sTabaArray[3] + "," +
                                                                                   sTabaArray[4] + "," +
                                                                                   sTabaArray[5] + "," +
                                                                                   sBundleArray[14] + "," +
                                                                                   sBundleArray[13] + "," +
                                                                                   intCenterBoxNum.ToString();
                                PubConstClass.intOrderSetTransIndex++;
                                break;
                            }
                        }
                        intCenterBoxNum += 1;
                    }
                    else
                    {
                        strArray = strBeforeData.Replace(",", "-").Replace(" = ", "-").Split('-');
                        strCmpArray = PubConstClass.pblOrderArray[intLoopCnt].Replace(" = ", "-").Split('-');
                        if ((strArray[2] + strArray[3]) == (strCmpArray[2] + strCmpArray[3]))
                        {
                            // 前回のデータと「センターコード」「コースコード」が同じ。
                            for (var intLoopInCnt = 0; intLoopInCnt <= intCmbOrderArrayIndex - 1; intLoopInCnt++)
                            {
                                strCmpArray = strCmbOrderArray[intLoopInCnt].Replace(",", "-").Split('-');
                                if ((strArray[2] + strArray[3]) == (strCmpArray[2] + strCmpArray[3]))
                                {
                                    CommonModule.OutPutLogFile("【１】" + strCmbOrderArray[intLoopInCnt]);
                                    intCenterCount = Convert.ToInt32(Conversion.Fix(intCenterBoxNum / (double)PubConstClass.iMaxBoxOfRollBox));
                                    if (intCenterBoxNum % PubConstClass.iMaxBoxOfRollBox != 0)
                                    {
                                        intCenterCount += 1;
                                    }
                                    strOrderRePrint = PubConstClass.pblOrderArray[intLoopCnt].Split('=');
                                    CmbOrderRePrint.Items.Add(intLoopCnt + 1 + "頁 → ｺｰｽ箱数：" + strOrderRePrint[1].Trim().Replace("(", "束(") +
                                                                               " ｾﾝﾀｰ箱数：" + strCmpArray[5] + "箱(" + intCenterBoxNum + "/");
                                    PubConstClass.pblOrderArray[intLoopCnt] = PubConstClass.pblOrderArray[intLoopCnt].Replace("-", ",").
                                                                                                                      Replace(" = ", ",").
                                                                                                                      Replace("(", ",").
                                                                                                                      Replace("/", ",").
                                                                                                                      Replace(")【", ",").
                                                                                                                      Replace("】", ",") +
                                                                                                                      strCmpArray[5] + "," +
                                                                                                                      intCenterBoxNum + ",";
                                    // センターコード単位の個数
                                    iUnionNumForCenter[iUnionNumForCenterIndex] += 1;

                                    sTabaArray = PubConstClass.pblOrderArray[intLoopCnt].Split(',');
                                    //iUnionIndex += int.Parse(sTabaArray[5]);
                                    iUnionIndex += 1;
                                    CommonModule.OutPutLogFile("【＝】【iUnionIndex】" + iUnionIndex);

                                    sBundleArray = PubConstClass.pblCollatingData[iUnionIndex].Split(',');
                                    // 曜日,便,ｾﾝﾀｰｺｰﾄﾞ,ｺｰｽ,束数,組合員ｺｰﾄﾞ(先頭),組合員ｺｰﾄﾞ(最後),結束No.
                                    PubConstClass.pblOrderSetTransDetail[intLoopCnt] = sTabaArray[2] + "," +
                                                                                       sTabaArray[0] + "," +
                                                                                       sTabaArray[3] + "," +
                                                                                       sTabaArray[4] + "," +
                                                                                       sTabaArray[5] + "," +
                                                                                       sBundleArray[14] + "," +
                                                                                       sBundleArray[13] + "," +
                                                                                       intCenterBoxNum.ToString();
                                    PubConstClass.intOrderSetTransIndex++;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (strArray[2] != strCmpArray[2])
                            {
                                // センターコードが異なる
                                iMaxCount.Add(intCenterBoxNum - 1);
                                intCenterBoxNum = 1;
                                iUnionNumForCenterIndex++;
                            }
                            // 前回のデータと「センターコード」「コースコード」が異なる
                            strBeforeData = PubConstClass.pblOrderArray[intLoopCnt];
                            strArray = strBeforeData.Replace(" = ", "-").Replace(" = ", "-").Split('-');
                            for (var intLoopInCnt = 0; intLoopInCnt <= intCmbOrderArrayIndex - 1; intLoopInCnt++)
                            {
                                strCmpArray = strCmbOrderArray[intLoopInCnt].Replace(",", "-").Split('-');
                                if ((strArray[2] + strArray[3]) == (strCmpArray[2] + strCmpArray[3]))
                                {
                                    CommonModule.OutPutLogFile("【２】" + strCmbOrderArray[intLoopInCnt]);
                                    intCenterCount = Convert.ToInt32(Conversion.Fix(intCenterBoxNum / (double)PubConstClass.iMaxBoxOfRollBox));
                                    if (intCenterBoxNum % PubConstClass.iMaxBoxOfRollBox != 0)
                                    {
                                        intCenterCount += 1;
                                    }
                                    strOrderRePrint = PubConstClass.pblOrderArray[intLoopCnt].Split('=');
                                    CmbOrderRePrint.Items.Add(intLoopCnt + 1 + "頁 → ｺｰｽ箱数：" + strOrderRePrint[1].Trim().Replace("(", "束(") +
                                                                               " ｾﾝﾀｰ箱数：" + strCmpArray[5] + "箱(" + intCenterBoxNum + "/");
                                    PubConstClass.pblOrderArray[intLoopCnt] = PubConstClass.pblOrderArray[intLoopCnt].Replace("-", ",").
                                                                                                                      Replace(" = ", ",").
                                                                                                                      Replace("(", ",").
                                                                                                                      Replace("/", ",").
                                                                                                                      Replace(")【", ",").
                                                                                                                      Replace("】", ",") +
                                                                                                                      strCmpArray[5] + "," +
                                                                                                                      intCenterBoxNum + ",";
                                    // センターコード単位の個数
                                    iUnionNumForCenter[iUnionNumForCenterIndex] += 1;

                                    sTabaArray = PubConstClass.pblOrderArray[intLoopCnt].Split(',');
                                    iUnionIndex += int.Parse(sTabaArray[5]);
                                    iUnionIndex += 1;
                                    CommonModule.OutPutLogFile("【≠】【iUnionIndex】" + iUnionIndex);

                                    sBundleArray = PubConstClass.pblCollatingData[iUnionIndex].Split(',');
                                    // 曜日,便,ｾﾝﾀｰｺｰﾄﾞ,ｺｰｽ,束数,組合員ｺｰﾄﾞ(先頭),組合員ｺｰﾄﾞ(最後),結束No.
                                    PubConstClass.pblOrderSetTransDetail[intLoopCnt] = sTabaArray[2] + "," +
                                                                                       sTabaArray[0] + "," +
                                                                                       sTabaArray[3] + "," +
                                                                                       sTabaArray[4] + "," +
                                                                                       sTabaArray[5] + "," +
                                                                                       sBundleArray[14] + "," +
                                                                                       sBundleArray[13] + "," +
                                                                                       intCenterBoxNum.ToString();
                                    PubConstClass.intOrderSetTransIndex++;
                                    break;
                                }
                            }
                        }
                        intCenterBoxNum += 1;
                    }
                }
                iMaxCount.Add(intCenterBoxNum - 1);

                for (int idx = 0; idx <= iUnionNumForCenterIndex; idx++)
                {
                    CommonModule.OutPutLogFile("【センター毎の最大箱数】iUnionNumForCenter[" + idx.ToString("00") + "] = " + iUnionNumForCenter[idx].ToString());
                }

                //////////////////////////////////////
                #region センター箱数の分母情報付加
                // 折コン用名札コンボボックスに「センター箱数の分母」情報の付加。
                ArrayList arOrderRePrint = new ArrayList();
                ArrayList arOrderArray = new ArrayList();
                string sMaxCount = iMaxCount[0].ToString();
                int iCnt = Convert.ToInt32(iMaxCount[0].ToString());
                int iIndex = 0;
                arOrderRePrint.Clear();
                arOrderArray.Clear();
                for (var N = 0; N <= CmbOrderRePrint.Items.Count - 1; N++)
                {
                    // センター箱数の分母情報付加
                    arOrderRePrint.Add(CmbOrderRePrint.Items[N].ToString() + sMaxCount + ")");
                    arOrderArray.Add(PubConstClass.pblOrderArray[N] + sMaxCount + ",");
                    iCnt -= 1;
                    if ((iCnt == 0) & iIndex < iMaxCount.Count - 1)
                    {
                        iIndex += 1;
                        sMaxCount = iMaxCount[iIndex].ToString();
                        iCnt = Convert.ToInt32(iMaxCount[iIndex].ToString());
                    }
                }
                CmbOrderRePrint.Items.Clear();
                for (var N = 0; N <= arOrderRePrint.Count - 1; N++)
                {
                    CmbOrderRePrint.Items.Add(arOrderRePrint[N].ToString());
                    PubConstClass.pblOrderArray[N] = arOrderArray[N].ToString();
                }
                #endregion
                //////////////////////////////////////
            }
            catch (Exception ex)
            {
                string sError = ex.Message + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(sError, "【GetCourseBoxCount】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int[] iPrintBunbo = new int[100001];
        //private List<int> iPrintBunbo = new List<int>();

        /// <summary>
        /// 印字内容（分子/分母）データの取得処理
        /// </summary>
        private void GetPrintData()
        {
            string[] strArray;
            int iSetCount = 0;              // セット数／束
            string sLastTimeDepot = "";     // 前回のデポコード
            string sFromData = "";          // 積付表Ａ（１レコード）の先頭データ
            int iIndexForPrint = 0;         // 束情報印字用インデックス
            // 出荷束数のセット
            int iMaxCount = PubConstClass.iMaxBoxOfRollBox;

            try
            {
                for (int intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                {                                                           
                    strArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');

                    // 結束区分けデータのみを対象とする
                    if (strArray[50].Contains("結束区分け") || strArray[50].Contains("コース区分け") || strArray[50].Contains("デポ区分け"))
                    {
                        if (sFromData == "")
                        {
                            // FROMデータが設定されていない場合
                            sFromData = PubConstClass.pblTransactionData[intLoopCnt];
                            // 前回のデポコードとして残す。
                            sLastTimeDepot = strArray[4];
                            //CommonModule.OutPutLogFile("〓最初のFROMデータ：" + sFromData);
                        }
                        else
                        {
                            // デポコードが変化したかのチェック
                            if (sLastTimeDepot != strArray[4])
                            {
                                iPrintBunbo[iIndexForPrint] = iSetCount;
                                CommonModule.OutPutLogFile("【分母データ】" + iPrintBunbo[iIndexForPrint]);
                                iIndexForPrint++;

                                iMaxCount = PubConstClass.iMaxBoxOfRollBox;
                                // 前回のデポコードとして残す。
                                sLastTimeDepot = strArray[4];
                                iSetCount = 0;
                                sFromData = PubConstClass.pblTransactionData[intLoopCnt];
                            }
                            else
                            {
                                iMaxCount--;
                                if (iMaxCount == 0)
                                {
                                    iPrintBunbo[iIndexForPrint] = iSetCount;
                                    CommonModule.OutPutLogFile("【分母データ】" + iPrintBunbo[iIndexForPrint]);
                                    iIndexForPrint++;                                    
                                    iMaxCount = PubConstClass.iMaxBoxOfRollBox;
                                    iSetCount = 0;
                                    sFromData = PubConstClass.pblTransactionData[intLoopCnt];
                                }
                            }
                        }
                        // セット数＋１
                        iSetCount++;
                    }
                }
                // 最後のデータの有無チェック
                if (sFromData != "")
                {
                    // 最後データの追加
                    iPrintBunbo[iIndexForPrint] = iSetCount;
                    CommonModule.OutPutLogFile("【分母データ】" + iPrintBunbo[iIndexForPrint]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetPrintData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「制御データ作成」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMakeControlDataFile_Click(Object sender, EventArgs e)
        {
            DialogResult dialogResult;

            try
            {
                if (PubConstClass.intCollatingIndex == 0)
                {
                    MessageBox.Show("「丁合指示データ取込」を行ってください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dialogResult = MessageBox.Show("制御データを作成しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

                CommonModule.OutPutLogFile("■「制御データ作成」ボタンクリック");

                // カゴ車への積載数（出荷束数）
                PubConstClass.iMaxBoxOfRollBox = int.Parse(LblFoldingCon.Text);
                // 結束最大束数（セット／束）
                PubConstClass.iMaxNumberOfBundle = int.Parse(LblMaxBundle.Text);
                CommonModule.OutPutLogFile("【結束最大束数　　：" + LblFoldingCon.Text + "束】で制御データ作成");
                CommonModule.OutPutLogFile("【カゴ車への積載数：" + LblMaxBundle.Text + "束】で制御データ作成");

                // 各ボタン等を使用不可とする
                BtnIsEnable(false);

                // （１）実績データ取得処理
                GetTransactionData();

                // （２）印字内容（分子/分母）データの取得処理
                GetPrintData();

                ////////////////////////////////
                // （３）制御データの書込処理 //
                ////////////////////////////////                
                WriteControlData();

                // （４）稼働データ格納配列ファイル作成
                CreateOperationDataStorageSequenceFile();

                // （５）「結束束」単位の箱数を取得
                GetBundleBoxCount();

                // （６）「センターコード」単位で箱数を取得
                GetCenterBoxCount();

                // （７）「コースコード」単位で箱数を取得
                GetCourseBoxCount();

                // （８）各帳票データの作成
                CreationOfEachFormData();

                // （９）吊札用印字データの作成
                CreationOfPrintingDataForHangingTags();
            }
            catch (Exception ex)
            {
                string sData = ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(sData, "【BtnMakeControlDataFile_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 各ボタン等を使用可能とする
                BtnIsEnable(true);
                // １箱最大束数は制御データを作成すると丁合指示データを
                // 再度読み込みしないと選択不可とする。
                //CmbMaxBundle.Enabled = false;
            }
        }

        private List<String> lstShippingRePrint = new List<String>();
        private List<String> lstHangingTag = new List<String>();

        /// <summary>
        /// 吊札用印字データの作成
        /// </summary>
        private void CreationOfPrintingDataForHangingTags()
        {
            string strPutDataPath;
            try
            {
                strPutDataPath = GetPathForHangingTag();
                using (StreamWriter sw = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {
                    string sdistributionDayFrom = "";       // （先頭の）配布曜日
                    string sCourse = "";                    // （先頭の）コース
                    string ssetOrderStart = "";             // （先頭の）セット順

                    string sPutData;
                    int iPageNumber = 1;
                    int iBasketCarSerialNumber = 1;
                    string[] strArray;
                    foreach (var s in lstHangingTag)
                    {
                        strArray = s.Split(',');
                        // １カゴ車の積載コンテナ数連番と積載コンテナ総数が等しいかチェック
                        if (strArray[57] != strArray[58])
                        {
                            if (sdistributionDayFrom == "")
                            {
                                sdistributionDayFrom = strArray[7];
                                sCourse = strArray[7] + strArray[8] + strArray[9];
                                ssetOrderStart = strArray[55];
                            }
                        }
                        else
                        {
                            string[] sAry = lstShippingRePrint[iPageNumber - 1].Split(',');

                            if (sAry[1] == sAry[2])
                            {
                                // ENDマーク
                                sPutData = "1" + ",";
                            }
                            else
                            {
                                // ENDマーク
                                sPutData = "0" + ",";
                            }
                            // 生協コード
                            sPutData += strArray[2] + ",";
                            // 生協名
                            if (PubConstClass.dicCoopCodeData.ContainsKey(strArray[2]))
                            {
                                sPutData += PubConstClass.dicCoopCodeData[strArray[2]] + ",";
                            }
                            else
                            {
                                sPutData += "該当なし" + ",";
                            }
                            // 企画号
                            sPutData += strArray[0] + ",";
                            if (sAry[0] == "1")
                            {
                                // 積載コンテナ数が「1」のときの処理
                                // 配布曜日（FROM）※（TO）と同じ
                                sPutData += strArray[7] + ",";
                            }
                            else
                            {
                                // 配布曜日（FROM）
                                sPutData += sdistributionDayFrom + ",";
                            }
                            // 配布曜日（TO）
                            sPutData += strArray[7] + ",";
                            // コース（FROM）
                            sPutData += sCourse + ",";
                            // コース（TO）
                            sPutData += strArray[7] + strArray[8] + strArray[9] + ",";
                            // デポコード
                            sPutData += strArray[4] + ",";
                            // デポ名
                            if (PubConstClass.dicDepoCodeData.ContainsKey(strArray[4]))
                            {
                                sPutData += PubConstClass.dicDepoCodeData[strArray[4]] + ",";
                            }
                            else
                            {
                                sPutData += "該当なし" + ",";
                            }
                            // 積載コンテナ数
                            sPutData += sAry[0] + ",";
                            // セット数／コンテナ
                            sPutData += LblFoldingCon.Text + ",";
                            if (sAry[0] == "1")
                            {
                                // 積載コンテナ数が「1」のときの処理
                                // 積載部数
                                int iSekisai = int.Parse(strArray[55]) - int.Parse(strArray[56]) + 1;
                                sPutData += iSekisai.ToString() + ",";
                                // セット順（スタート）
                                sPutData += strArray[55] + ",";
                                // セット順（エンド）
                                sPutData += strArray[56] + ",";
                            }
                            else
                            {
                                // 積載部数
                                int iSekisai = int.Parse(ssetOrderStart) - int.Parse(strArray[56]) + 1;
                                sPutData += iSekisai.ToString() + ",";
                                // セット順（スタート）
                                sPutData += ssetOrderStart + ",";
                                // セット順（エンド）
                                sPutData += strArray[56] + ",";
                            }
                            // カゴ車連番
                            sPutData += sAry[1] + ",";
                            iBasketCarSerialNumber++;
                            // カゴ車連番総数
                            sPutData += sAry[2] + ",";
                            // カゴ車チェックQR（最後の組合員番号 + Z）
                            sPutData += strArray[52] + "Z,";
                            // ページ番号
                            sPutData += iPageNumber.ToString("000") + ",";
                            iPageNumber++;
                            // 吊札データの書き込み
                            sw.WriteLine(sPutData);

                            sdistributionDayFrom = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【CreationOfPrintingDataForHangingTags】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 各帳票データの作成
        /// </summary>
        private void CreationOfEachFormData()
        {
            //string strPutDataPath;
            try
            {
                //////////////////////////////////////////////
                #region カゴ車数一覧（デポ毎）データ作成（データ枠の作成）（テスト中なので現在、未使用）
                List<string[]> KagoList = new List<string[]>();
                KagoList.Clear();
                //string sLastTimeDepoCode = "";

                //foreach (string sData in PubConstClass.lstLinePlanData)
                //{
                //    string[] sCol = new string[14];
                //    // 生協コード
                //    string sCoopCode = sData.Substring(3, 4);
                //    // 生協名
                //    sCol[0] = sData.Substring(7, 20).Replace("　", "");
                //    // 企画番号
                //    sCol[1] = "";
                //    // 基準番号
                //    sCol[2] = "";
                //    // デポコード
                //    sCol[3] = sData.Substring(29, 4);
                //    // デポ名
                //    sCol[4] = sData.Substring(33, 20).Replace("　", "");

                //    sCol[5] = "";
                //    sCol[6] = "";
                //    sCol[7] = "";
                //    sCol[8] = "";
                //    sCol[9] = "";
                //    sCol[10] = "";
                //    sCol[11] = "";
                //    sCol[12] = "";
                //    sCol[13] = "";

                //    if (sCoopCode == sCurrentCoopCode)
                //    {
                //        if (sLastTimeDepoCode != sCol[3])
                //        {
                //            // 前回のデポコードと異なっている場合のみ登録
                //            KagoList.Add(sCol);

                //        }
                //        sLastTimeDepoCode = sCol[3];
                //    }
                //}
                #endregion
                //////////////////////////////////////////////

                List<String> lstNumberOfBasketCars = new List<String>();

                // カゴ車数一覧（デポ毎）データ書込み処理
                WriteListOfBasketCarsByDepot(lstNumberOfBasketCars);
                // 処理件数一覧（生協毎）データ書込み処理
                WriteListOfProcessedItemsForEachCoop(lstNumberOfBasketCars);
                // 出荷確認表データ書込み処理
                WriteShipmentConfirmationTableData(lstNumberOfBasketCars);
                //帳票枚数確認表データ書込み処理
                WriteFormCountConfirmationTableData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【CreationOfEachFormData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// カゴ車数一覧（デポ毎）データ書込み処理
        /// </summary>
        /// <param name="lstNumberOfBasketCars"></param>
        private void WriteListOfBasketCarsByDepot(List<string> lstNumberOfBasketCars)
        {
            string[] sData = "1,2,3,4,5,6,7,8,9,A".Split(',');
            string sCoopNo = "";
            string sDepoNo = "";
            string sDepoName = "";
            int iSetNumber = 0;
            int iYobiSet = 0;
            string sKagoNumber = "";
            string sContainer = "";
            string sSetFrom = "";
            string sSetTo = "";
            string sPutData = "";
            int iIndex = 0;
            int iKagoIndex = 0;
            string[] sKagoArray;
            string strPutDataPath;

            try
            {
                foreach (var s in PubConstClass.pblTransactionData)
                {
                    if (s == null)
                    {
                        break;
                    }
                    sData = s.Split(',');
                    if (sData[50].Contains("デポ区分け") || sData[50].Contains("単協区分け"))
                    {
                        // カゴ車台数の取得
                        sKagoArray = lstShippingRePrint[iKagoIndex].Split(',');
                        sKagoNumber = sKagoArray[2];
                        iKagoIndex++;
                    }
                    if (sData[50] == "未")
                    {
                        // 区分け以外は対象外
                        continue;
                    }

                    if (sData[7] == "Z")
                    {
                        // 予備セット数の取得
                        iYobiSet += int.Parse(sData[54]);
                    }

                    if (sDepoNo == "")
                    {
                        sCoopNo = sData[2];
                        sDepoNo = sData[4];
                        sSetFrom = sData[55];
                        sSetTo = sData[56];
                    }
                    else
                    {
                        if (sDepoNo == sData[4])
                        {
                            sSetTo = sData[56];
                        }
                        else
                        {
                            // デポ名（前回値を採用）の取得
                            if (PubConstClass.dicDepoCodeData.ContainsKey(sDepoNo))
                            {
                                sDepoName = PubConstClass.dicDepoCodeData[sDepoNo];
                            }
                            else
                            {
                                sDepoName = "該当なし";
                            }
                            // セット数の計算
                            iSetNumber = int.Parse(sSetFrom) - int.Parse(sSetTo) + 1;
                            // コンテナ数の取得
                            sContainer = iUnionNumForCenter[iIndex].ToString();
                            // カゴ車数一覧（デポ毎）データの作成
                            sPutData = sDepoNo + "," +                  // デポNo
                                       sDepoName + "," +                // デポ名
                                       iSetNumber.ToString() + "," +    // セット数
                                       iYobiSet.ToString() + "," +      // 予備セット
                                       sKagoNumber + "," +              // カゴ車台数
                                       sContainer + "," +               // コンテナ数
                                       LblFoldingCon.Text + "," +       // セット数／コンテナ
                                       sSetFrom + "," +                 // SEQ（FROM）
                                       sSetTo + ","  +                  // SEQ（TO）
                                       sCoopNo + ",";                   // 生協コード（処理件数一覧（生協毎）用）
                            lstNumberOfBasketCars.Add(sPutData);
                            iIndex++;

                            sCoopNo = sData[2];
                            sDepoNo = sData[4];
                            sSetFrom = sData[55];
                            sSetTo = sData[56];
                            iSetNumber = 0;
                            iYobiSet = 0;
                        }
                    }
                }
                // デポ名の取得
                if (PubConstClass.dicDepoCodeData.ContainsKey(sData[4]))
                {
                    sDepoName = PubConstClass.dicDepoCodeData[sData[4]];
                }
                else
                {
                    sDepoName = "該当なし";
                }
                // セット数の計算
                iSetNumber = int.Parse(sSetFrom) - int.Parse(sSetTo) + 1;
                // コンテナ数の取得
                sContainer = iUnionNumForCenter[iIndex].ToString();
                // カゴ車台数の取得
                sKagoArray = lstShippingRePrint[lstShippingRePrint.Count - 1].Split(',');
                sKagoNumber = sKagoArray[2];
                // カゴ車数一覧（デポ毎）データの作成
                sPutData = sData[4] + "," +                 // デポNo
                           sDepoName + "," +                // デポ名
                           iSetNumber.ToString() + "," +    // セット数
                           iYobiSet.ToString() + "," +      // 予備セット
                           sKagoNumber + "," +              // カゴ車台数
                           sContainer + "," +               // コンテナ数
                           LblFoldingCon.Text + "," +       // セット数／コンテナ
                           sSetFrom + "," +                 // SEQ（FROM）
                           sSetTo + "," +                   // SEQ（TO）
                           sData[2] + ",";                  // 生協コード（処理件数一覧（生協毎）用）

                lstNumberOfBasketCars.Add(sPutData);

                strPutDataPath = GetPathForListOfNumberOfCarsInBasket();
                using (StreamWriter swLoading = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {
                    foreach (string sDataTest in lstNumberOfBasketCars)
                    {
                        if (sDataTest != null)
                        {
                            swLoading.WriteLine(sDataTest);
                        }
                    }
                    #region テスト中なので現在、未使用
                    //foreach (var sData in KagoList)
                    //{
                    //    string sPutData = "";
                    //    int iIndex = 0;
                    //    int iTotal = 0;
                    //    foreach (string sData2 in sData)
                    //    {
                    //        if (iIndex == 6 || iIndex == 9 || iIndex == 12)
                    //        {
                    //            // カゴＡの台数＋カゴＢの台数＋カゴＣの台数を求める
                    //            if (int.TryParse(sData2, out int result))
                    //            {
                    //                // 数値の場合のみ積算
                    //                iTotal += int.Parse(sData2);
                    //            }
                    //        }
                    //        // 最後の項目（台数合計）かのチェック
                    //        if (iIndex == 13)
                    //        {
                    //            if (iTotal > 0)
                    //            {
                    //                // 合計値がセットされているかのチェック
                    //                sPutData += iTotal.ToString() + ",";
                    //            }
                    //            else
                    //            {
                    //                sPutData += ",";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sPutData += sData2 + ",";
                    //        }
                    //        iIndex++;
                    //    }
                    //    if (sData[1] == "" && sData[2] == "")
                    //    {
                    //        string sDebugMessage = "";
                    //        foreach (string sDebug in sData)
                    //        {
                    //            sDebugMessage += sDebug + ",";
                    //        }
                    //        CommonModule.OutPutLogFile("カゴ車データ出力対象外：" + sDebugMessage);
                    //        //swLoading.WriteLine(sPutData);
                    //    }
                    //    else
                    //    {
                    //        // カゴ車データの書込み
                    //        swLoading.WriteLine(sPutData);
                    //    }
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【WriteListOfBasketCarsByDepot】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 処理件数一覧（生協毎）データ書込み処理
        /// </summary>
        private void WriteListOfProcessedItemsForEachCoop(List<string> lstNumberOfBasketCars)
        {
            string[] sData = "1,2,3,4,5,6,7,8,9,A".Split(',');
            string sCoopCode = "";      // 生協コード
            string sCoopName = "";      // 生協名
            int iSetNumber = 0;         // セット数
            int iYobiSet = 0;           // 予備セット
            int iKagoNumber = 0;        // カゴ車台数
            int iContainer = 0;         // コンテナ数
            string sSetFrom = "";       // セット値（FROM）
            string sSetTo = "";         // セット値（TO）

            string sPutData = "";
            string strPutDataPath;

            List<String> lstProcessedItemsForEachCoop = new List<String>();

            try
            {
                foreach (var s in lstNumberOfBasketCars)
                {
                    // カゴ車数一覧（デポ毎）データの分解
                    sData = s.Split(',');
                    if (sCoopCode == "")
                    {
                        // 最初のデータ
                        sCoopCode = sData[9];                   // 生協コード
                        #region 生協名の取得
                        if (PubConstClass.dicCoopCodeData.ContainsKey(sCoopCode))
                        {
                            sCoopName = PubConstClass.dicCoopCodeData[sCoopCode];
                        }
                        else
                        {
                            sCoopName = "該当なし";
                        }
                        #endregion
                        iSetNumber = int.Parse(sData[2]);       // セット数
                        iYobiSet = int.Parse(sData[3]);         // 予備セット
                        iKagoNumber = int.Parse(sData[4]);      // カゴ車台数
                        iContainer = int.Parse(sData[5]);       // コンテナ数
                        sSetFrom = sData[7];                    // セット値（FROM）
                        sSetTo = sData[8];                      // セット値（TO）
                    }
                    else
                    {
                        // 生協コードの比較
                        if (sCoopCode == sData[9])
                        {
                            // 生協コードが同じ
                            iSetNumber += int.Parse(sData[2]);      // セット数
                            iYobiSet += int.Parse(sData[3]);        // 予備セット
                            iKagoNumber += int.Parse(sData[4]);     // カゴ車台数
                            iContainer += int.Parse(sData[5]);      // コンテナ数
                            //sSetFrom = sData[7];                    // セット値（FROM）
                            sSetTo = sData[8];                      // セット値（TO）

                        }
                        else
                        {
                            // 生協コードが異なる
                            // 処理件数一覧（生協毎）データの作成
                            sPutData = sCoopCode + "," +                // 生協コード
                                       sCoopName + "," +                // 生協名
                                       iSetNumber.ToString() + "," +    // セット数
                                       iYobiSet.ToString() + "," +      // 予備セット
                                       iKagoNumber.ToString() + "," +   // カゴ車台数
                                       iContainer.ToString() + "," +    // コンテナ数
                                       LblFoldingCon.Text + "," +       // セット数／コンテナ
                                       sSetFrom + "," +                 // SEQ（FROM）
                                       sSetTo + ",";                    // SEQ（TO）
                            lstProcessedItemsForEachCoop.Add(sPutData);

                            sCoopCode = sData[9];
                            #region 生協名の取得
                            if (PubConstClass.dicCoopCodeData.ContainsKey(sCoopCode))
                            {
                                sCoopName = PubConstClass.dicCoopCodeData[sCoopCode];
                            }
                            else
                            {
                                sCoopName = "該当なし";
                            }
                            #endregion
                            iSetNumber = int.Parse(sData[2]);       // セット数
                            iYobiSet = int.Parse(sData[3]);         // 予備セット
                            iKagoNumber = int.Parse(sData[4]);      // カゴ車台数
                            iContainer = int.Parse(sData[5]);       // コンテナ数
                            sSetFrom = sData[7];                    // セット値（FROM）
                            sSetTo = sData[8];                      // セット値（TO）
                        }
                    }
                }
                // 処理件数一覧（生協毎）データの作成
                sPutData = sCoopCode + "," +                // 生協コード
                           sCoopName + "," +                // 生協名
                           iSetNumber.ToString() + "," +    // セット数
                           iYobiSet.ToString() + "," +      // 予備セット
                           iKagoNumber.ToString() + "," +   // カゴ車台数
                           iContainer.ToString() + "," +    // コンテナ数
                           LblFoldingCon.Text + "," +       // セット数／コンテナ
                           sSetFrom + "," +                 // SEQ（FROM）
                           sSetTo + ",";                    // SEQ（TO）
                lstProcessedItemsForEachCoop.Add(sPutData);

                strPutDataPath = GetPathForListOfProcessedItems();
                using (StreamWriter swLoading = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {
                    foreach (string sDataTest in lstProcessedItemsForEachCoop)
                    {
                        if (sDataTest != null)
                        {
                            swLoading.WriteLine(sDataTest);
                        }
                    }
                    #region テスト中なので現在、未使用
                    //foreach (var sData in KagoList)
                    //{
                    //    string sPutData = "";
                    //    int iIndex = 0;
                    //    int iTotal = 0;
                    //    foreach (string sData2 in sData)
                    //    {
                    //        if (iIndex == 6 || iIndex == 9 || iIndex == 12)
                    //        {
                    //            // カゴＡの台数＋カゴＢの台数＋カゴＣの台数を求める
                    //            if (int.TryParse(sData2, out int result))
                    //            {
                    //                // 数値の場合のみ積算
                    //                iTotal += int.Parse(sData2);
                    //            }
                    //        }
                    //        // 最後の項目（台数合計）かのチェック
                    //        if (iIndex == 13)
                    //        {
                    //            if (iTotal > 0)
                    //            {
                    //                // 合計値がセットされているかのチェック
                    //                sPutData += iTotal.ToString() + ",";
                    //            }
                    //            else
                    //            {
                    //                sPutData += ",";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sPutData += sData2 + ",";
                    //        }
                    //        iIndex++;
                    //    }
                    //    if (sData[1] == "" && sData[2] == "")
                    //    {
                    //        string sDebugMessage = "";
                    //        foreach (string sDebug in sData)
                    //        {
                    //            sDebugMessage += sDebug + ",";
                    //        }
                    //        CommonModule.OutPutLogFile("カゴ車データ出力対象外：" + sDebugMessage);
                    //        //swLoading.WriteLine(sPutData);
                    //    }
                    //    else
                    //    {
                    //        // カゴ車データの書込み
                    //        swLoading.WriteLine(sPutData);
                    //    }
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【WriteListOfProcessedItemsForEachCoop】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 出荷確認表データ書込み処理
        /// </summary>
        /// <param name="lstNumberOfBasketCars"></param>
        private void WriteShipmentConfirmationTableData(List<string> lstNumberOfBasketCars)
        {
            string strPutDataPath;

            try
            {
                #region 出荷確認表データ書込み処理
                strPutDataPath = GetPathForShipmentConfirmationTable();
                using (StreamWriter swLoading = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {
                    foreach (string sDataTest in lstNumberOfBasketCars)
                    {
                        if (sDataTest != null)
                        {
                            swLoading.WriteLine(sDataTest);
                        }
                    }
                    #region テスト中なので現在、未使用
                    //foreach (var sData in KagoList)
                    //{
                    //    string sPutData = "";
                    //    int iIndex = 0;
                    //    int iTotal = 0;
                    //    foreach (string sData2 in sData)
                    //    {
                    //        if (iIndex == 6 || iIndex == 9 || iIndex == 12)
                    //        {
                    //            // カゴＡの台数＋カゴＢの台数＋カゴＣの台数を求める
                    //            if (int.TryParse(sData2, out int result))
                    //            {
                    //                // 数値の場合のみ積算
                    //                iTotal += int.Parse(sData2);
                    //            }
                    //        }
                    //        // 最後の項目（台数合計）かのチェック
                    //        if (iIndex == 13)
                    //        {
                    //            if (iTotal > 0)
                    //            {
                    //                // 合計値がセットされているかのチェック
                    //                sPutData += iTotal.ToString() + ",";
                    //            }
                    //            else
                    //            {
                    //                sPutData += ",";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            // 「生協名」「管理用企画番号」「曜日」「デポコード」「デポ名」のみ格納
                    //            if (iIndex == 0 || iIndex == 1 || iIndex == 2 || iIndex == 3 || iIndex == 4)
                    //            {
                    //                sPutData += sData2 + ",";
                    //            }
                    //        }
                    //        iIndex++;
                    //    }
                    //    if (sData[1] == "" && sData[2] == "")
                    //    {
                    //        string sDebugMessage = "";
                    //        foreach (string sDebug in sData)
                    //        {
                    //            sDebugMessage += sDebug + ",";
                    //        }
                    //        CommonModule.OutPutLogFile("出荷確認表データ出力対象外：" + sDebugMessage);
                    //    }
                    //    else
                    //    {
                    //        // 出荷確認表データの書込み
                    //        swLoading.WriteLine(sPutData);
                    //    }
                    //}
                    #endregion
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【WriteShipmentConfirmationTableData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 帳票枚数確認表データ書込み処理
        /// </summary>
        private void WriteFormCountConfirmationTableData()
        {
            string[] sAray = "1,2,3,4,5,6,7,8,".Split(',');
            string sCoopCode = "";
            string sCoopName = "";
            int iSt1Count = 0;
            int iSt2Count = 0;
            int iSt3Count = 0;
            int iSt4Count = 0;
            int iSt5Count = 0;
            int iSt6Count = 0;

            List<String> llstNumberOfFormsConfirmationTable = new List<String>();
            
            string strPutDataPath;
            string sPutData;
            try
            {
                foreach (var s in PubConstClass.pblTransactionData)
                {
                    if (s == null)
                    {
                        break;
                    }
                    sAray = s.Split(',');
                    if (sCoopCode == "")
                    {
                        sCoopCode = sAray[2];
                        #region 生協名の取得
                        if (PubConstClass.dicCoopCodeData.ContainsKey(sAray[2]))
                        {
                            sCoopName = PubConstClass.dicCoopCodeData[sAray[2]];
                        }
                        else
                        {
                            sCoopName = "該当なし";
                        }
                        #endregion
                    }
                    else
                    {
                        if (sCoopCode != sAray[2])
                        {
                            // 帳票枚数確認表データの作成
                            sPutData = sCoopCode + "," +                // 生協コード
                                       sCoopName + "," +                // 生協名
                                       iSt1Count.ToString() + "," +     // 帳票ST1枚数
                                       iSt2Count.ToString() + "," +     // 帳票ST2枚数
                                       iSt3Count.ToString() + "," +     // 帳票ST3枚数
                                       iSt4Count.ToString() + "," +     // 帳票ST4枚数
                                       iSt5Count.ToString() + "," +     // 帳票ST5枚数
                                       iSt6Count.ToString() + ",";      // 帳票ST6枚数
                            llstNumberOfFormsConfirmationTable.Add(sPutData);

                            sCoopCode = sAray[2];
                            #region 生協名の取得
                            if (PubConstClass.dicCoopCodeData.ContainsKey(sAray[2]))
                            {
                                sCoopName = PubConstClass.dicCoopCodeData[sAray[2]];
                            }
                            else
                            {
                                sCoopName = "該当なし";
                            }
                            #endregion
                            iSt1Count = int.Parse(sAray[35]);
                            iSt2Count = int.Parse(sAray[36]);
                            iSt3Count = int.Parse(sAray[37]);
                            iSt4Count = int.Parse(sAray[38]);
                            iSt5Count = int.Parse(sAray[39]);
                            iSt6Count = int.Parse(sAray[40]);
                        }
                        else
                        {
                            // 生協コードが前回値と等しい
                            #region 帳票枚数のカウントアップ
                            if (int.Parse(sAray[35]) > 0)
                            {
                                iSt1Count += int.Parse(sAray[35]);
                            }
                            if (int.Parse(sAray[36]) > 0)
                            {
                                iSt2Count += int.Parse(sAray[36]);
                            }
                            if (int.Parse(sAray[37]) > 0)
                            {
                                iSt3Count += int.Parse(sAray[37]);
                            }
                            if (int.Parse(sAray[38]) > 0)
                            {
                                iSt4Count += int.Parse(sAray[38]);
                            }
                            if (int.Parse(sAray[39]) > 0)
                            {
                                iSt5Count += int.Parse(sAray[39]);
                            }
                            if (int.Parse(sAray[40]) > 0)
                            {
                                iSt6Count += int.Parse(sAray[40]);
                            }
                            #endregion
                        }
                    }
                }
                #region 帳票枚数のカウントアップ
                if (int.Parse(sAray[35]) > 0)
                {
                    iSt1Count += int.Parse(sAray[35]);
                }
                if (int.Parse(sAray[36]) > 0)
                {
                    iSt2Count += int.Parse(sAray[36]);
                }
                if (int.Parse(sAray[37]) > 0)
                {
                    iSt3Count += int.Parse(sAray[37]);
                }
                if (int.Parse(sAray[38]) > 0)
                {
                    iSt4Count += int.Parse(sAray[38]);
                }
                if (int.Parse(sAray[39]) > 0)
                {
                    iSt5Count += int.Parse(sAray[39]);
                }
                if (int.Parse(sAray[40]) > 0)
                {
                    iSt6Count += int.Parse(sAray[40]);
                }
                #endregion

                // 帳票枚数確認表データの作成
                sPutData = sCoopCode + "," +                // 生協コード
                           sCoopName + "," +                // 生協名
                           iSt1Count.ToString() + "," +     // 帳票ST1枚数
                           iSt2Count.ToString() + "," +     // 帳票ST2枚数
                           iSt3Count.ToString() + "," +     // 帳票ST3枚数
                           iSt4Count.ToString() + "," +     // 帳票ST4枚数
                           iSt5Count.ToString() + "," +     // 帳票ST5枚数
                           iSt6Count.ToString() + ",";      // 帳票ST6枚数
                llstNumberOfFormsConfirmationTable.Add(sPutData);

                strPutDataPath = GetPathForNumberOfFormsConfirmationTable();
                using (StreamWriter swLoading = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {
                    int iSum1 = 0;
                    int iSum2 = 0;
                    int iSum3 = 0;
                    int iSum4 = 0;
                    int iSum5 = 0;
                    int iSum6 = 0;
                    string[] sSumData;
                    foreach (string sDataLine in llstNumberOfFormsConfirmationTable)
                    {
                        if (sDataLine != null)
                        {
                            swLoading.WriteLine(sDataLine);
                            sSumData = sDataLine.Split(',');
                            iSum1 += int.Parse(sSumData[2]);
                            iSum2 += int.Parse(sSumData[3]);
                            iSum3 += int.Parse(sSumData[4]);
                            iSum4 += int.Parse(sSumData[5]);
                            iSum5 += int.Parse(sSumData[6]);
                            iSum6 += int.Parse(sSumData[7]);
                        }
                    }
                    swLoading.WriteLine(",,,,,,,,");
                    swLoading.WriteLine(",,,,,,,,");
                    swLoading.WriteLine(",            計  ," + 
                                        iSum1.ToString() + "," + iSum2.ToString() + "," + iSum3.ToString() + "," +
                                        iSum4.ToString() + "," + iSum5.ToString() + "," + iSum6.ToString() + ","
                                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【WriteFormCountConfirmationTableData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 稼働データ格納配列ファイル作成
        /// </summary>
        private void CreateOperationDataStorageSequenceFile()
        {
            string[] strArray;
            string strWork = "";
            string strFromWorkCode = "";
            string strFromSetSeq = "";      // セット順序番号
            int intIndex;
            int intTabaCount;
            string strPutDataPath;

            try
            {
                lstHangingTag.Clear();
                strPutDataPath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) +
                                 PubConstClass.pblCurrentDate + @"\" + "稼働データ格納配列ファイル.txt";
                // 稼働データ格納配列ファイルの書込み
                using (StreamWriter sw = new StreamWriter(strPutDataPath, false, Encoding.Default))
                {
                    intIndex = 0;
                    intTabaCount = 1;
                    for (int intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                    {
                        strArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');

                        if (strFromWorkCode == "")
                        {
                            // 区分けの最初の組合員コードを設定
                            strFromWorkCode = strArray[17];
                            // 区分けの最初のセット順序番号を設定x
                            strFromSetSeq = strArray[43];
                        }
                        // 区分けの最初の組合員コードを設定
                        strArray[51] = strFromWorkCode;
                        strArray[53] = intIndex.ToString();
                        // 区分けの最初のセット順序番号を設定
                        strArray[55] = strFromSetSeq;

                        if (strArray[50] != "未")
                        {
                            if (strArray.Length != 50)
                            {
                                // このレコードの組合員コードを設定
                                strArray[52] = strArray[17];
                                // このレコードのセット順序番号を設定
                                strArray[55] = strArray[43];
                                // INDEX＋束数                                                                                     
                                strArray[53] = intIndex.ToString() + "," + intTabaCount.ToString();
                                intIndex += 1;
                                strFromWorkCode = "";
                                intTabaCount = 0;
                            }
                        }

                        if (strArray.Length == 50)
                        {
                            strArray[50] = "未," + strArray[17] + ",,0,区分け無し";
                        }

                        intTabaCount += 1;
                        strWork = "";
                        for (var N = 0; N <= strArray.Length - 1; N++)
                        {
                            if (N == 3 || N == 5 || N == 6)
                            {
                                // 半角スペースを全角スペースに置き換える
                                strWork += strArray[N].PadRight(11).Replace(" ", "　") + ",";
                            }
                            else if (N == 10)
                            {
                                strWork += strArray[N].PadLeft(3).Replace(" ", "0") + ",";
                            }
                            else if (N == 15 || N == 16 || N == 17 || N == 18)
                            {
                                strWork += strArray[N].PadLeft(8).Replace(" ", "0") + ",";
                            }
                            else
                            {
                                strWork += strArray[N] + ",";
                            }
                        }
                        // 稼働データ格納配列ファイルへデータ書込み
                        sw.WriteLine(strWork);
                        PubConstClass.pblTransactionData[intLoopCnt] = strWork;
                        CommonModule.OutPutLogFile("【稼働データデータ作成】" + strWork);
                        if (strArray[50].Contains("結束区分け") || 
                            strArray[50].Contains("コース区分け") || 
                            strArray[50].Contains("デポ区分け") || 
                            strArray[50].Contains("単協区分け"))
                        {
                            // 結束区分けのデータを吊札データとして格納
                            lstHangingTag.Add(strWork);
                            CommonModule.OutPutLogFile("【吊り札データ作成】" + strWork);
                        }
                    }
                }
                CommonModule.OutPutLogFile("■稼動データ格納ファイル作成：" + strPutDataPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【CreateOperationDataStorageSequenceFile】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 吊札ファイル名取得処理
        /// "吊札"＋"_"＋組合員納品日＋".txt"
        /// </summary>
        /// <returns></returns>
        private string GetPathForHangingTag()
        {
            string sLoadingDataPath;

            sLoadingDataPath = "";
            sLoadingDataPath += CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
            sLoadingDataPath += @"吊札\";
            sLoadingDataPath += PubConstClass.DEF_OPERATING_LOCATION + @"\";
            sLoadingDataPath += @"吊札_";
            sLoadingDataPath += DTPDeliveryDate.Value.ToString("yyyyMMdd");
            sLoadingDataPath += ".txt";
            return sLoadingDataPath;
        }

        /// <summary>
        /// カゴ車数一覧（デポ毎）ファイル名取得処理
        /// "カゴ車数一覧（デポ毎）"＋"_"＋組合員納品日＋".txt"
        /// </summary>
        /// <returns></returns>
        private string GetPathForListOfNumberOfCarsInBasket()
        {
            string sLoadingDataPath;

            sLoadingDataPath = "";
            sLoadingDataPath += CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
            sLoadingDataPath += @"カゴ車数一覧（デポ毎）\";
            sLoadingDataPath += PubConstClass.DEF_OPERATING_LOCATION + @"\";
            sLoadingDataPath += @"カゴ車数一覧（デポ毎）_";
            sLoadingDataPath += DTPDeliveryDate.Value.ToString("yyyyMMdd");
            sLoadingDataPath += ".txt";
            return sLoadingDataPath;
        }

        /// <summary>
        /// 処理件数一覧（生協毎）ファイル名取得処理
        /// "処理件数一覧（生協毎）"＋"_"＋組合員納品日＋".txt"
        /// </summary>
        /// <returns></returns>
        private string GetPathForListOfProcessedItems()
        {
            string sLoadingDataPath;

            sLoadingDataPath = "";
            sLoadingDataPath += CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
            sLoadingDataPath += @"処理件数一覧（生協毎）\";
            sLoadingDataPath += PubConstClass.DEF_OPERATING_LOCATION + @"\";
            sLoadingDataPath += @"処理件数一覧（生協毎）_";
            sLoadingDataPath += DTPDeliveryDate.Value.ToString("yyyyMMdd");
            sLoadingDataPath += ".txt";
            return sLoadingDataPath;
        }

        /// <summary>
        /// 出荷確認表ファイル名取得処理
        /// "出荷確認表"＋"_"＋組合員納品日＋".txt"
        /// </summary>
        /// <returns></returns>
        private string GetPathForShipmentConfirmationTable()
        {
            string sLoadingDataPath;

            sLoadingDataPath = "";
            sLoadingDataPath += CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
            sLoadingDataPath += @"出荷確認表\";
            sLoadingDataPath += PubConstClass.DEF_OPERATING_LOCATION + @"\";
            sLoadingDataPath += @"出荷確認表_";
            sLoadingDataPath += DTPDeliveryDate.Value.ToString("yyyyMMdd");
            sLoadingDataPath += ".txt";
            return sLoadingDataPath;
        }

        /// <summary>
        /// 帳票枚数確認表ファイル名取得処理
        /// </summary>
        /// <returns></returns>
        private string GetPathForNumberOfFormsConfirmationTable()
        {
            string sLoadingDataPath;

            sLoadingDataPath = "";
            sLoadingDataPath += CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblFormOutPutFolder);
            sLoadingDataPath += @"帳票枚数確認表\";
            sLoadingDataPath += PubConstClass.DEF_OPERATING_LOCATION + @"\";
            sLoadingDataPath += @"帳票枚数確認表_";
            sLoadingDataPath += DTPDeliveryDate.Value.ToString("yyyyMMdd");
            sLoadingDataPath += ".txt";
            return sLoadingDataPath;
        }

        /// <summary>
        /// 事業所コードから事業所名を取得する
        /// </summary>
        /// <param name="sOfficeCode"></param>
        /// <returns></returns>
        private string GetOfficeNameFromOfficeCode(string sOfficeCode)
        {
            string sOfficeName;
            string[] sArray;
            try
            {
                sOfficeName = "該当なし";
                for (var K = 0; K <= PubConstClass.intMasterOfOfficeCnt - 1; K++)
                {
                    if (PubConstClass.pblMasterOfOffice[K, 0].Substring(4, 4) == sOfficeCode.Substring(4, 4))
                    {
                        sArray = PubConstClass.pblMasterOfOffice[K, 1].Split(',');
                        sOfficeName = sArray[1];
                        break;
                    }
                }
                return sOfficeName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetOfficeNameFromOfficeCode】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /// <summary>
        /// 折コン用名札印刷データの取得処理
        /// </summary>
        /// <param name="strPrintData"></param>
        public void GetPrintDataOfRollBox(string strPrintData)
        {
            string[] strArray;

            try
            {
                strArray = strPrintData.Split(',');

                // 企画回
                strPrtKikakukai = strArray[0];

                // 単協名の設定とセンタリング処理
                strPrtTankyoName = strArray[8];

                // センター名の設定とセンタリング処理
                strPrtCenterName = strArray[9];

                // 単協コード
                strPrtTankyoCode = strArray[1];

                // センターコード
                strPrtCenterCode = strArray[3];

                // コース名（４桁のコード）
                strPrtCourseCode = strArray[4];

                // コース箱数
                strPrtCourseBoxNum = strArray[6] + "/" + strArray[7];

                // センター箱数
                strPrtCenterBoxNum = strArray[11] + "/" + strArray[12];

                // 曜日の取得
                //string strYobi;
                //strYobi = "";
                //switch (strArray[2])
                //{
                //    case "1":
                //        {
                //            strYobi = "月";
                //            break;
                //        }

                //    case "2":
                //        {
                //            strYobi = "火";
                //            break;
                //        }

                //    case "3":
                //        {
                //            strYobi = "水";
                //            break;
                //        }

                //    case "4":
                //        {
                //            strYobi = "木";
                //            break;
                //        }

                //    case "5":
                //        {
                //            strYobi = "金";
                //            break;
                //        }

                //    case "6":
                //        {
                //            strYobi = "土";
                //            break;
                //        }

                //    case "7":
                //        {
                //            strYobi = "日";
                //            break;
                //        }

                //    default:
                //        {
                //            strYobi = "？";
                //            break;
                //        }
                //}

                //// 配達曜日
                //strPrtHaitatuWek = " " + strYobi + "曜日";

                //// 配達日
                //strPrtHaitatuDat = " " + strArray[0].Substring(0, 2) + "/" + strArray[0].Substring(2, 2) + "(" + strYobi + ")";

                //// カゴ車番号
                //strPrtKagoNumber = " " + strArray[6];

                //// カゴ車総数
                //strPrtKagoAllNum = " " + strArray[5];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetPrintData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「生産ログ作成テスト」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTestForSeisan_Click(Object sender, EventArgs e)
        {
            try
            {
                if (BtnTestForSeisan.Text == "生産ログ作成開始")
                {
                    BtnTestForSeisan.Text = "生産ログ作成停止";
                    //TimTestForSeisan.Interval = 2000;     // 2秒固定とする
                    TimTestForSeisan.Interval = 5000;     // 5秒固定とする
                    TimTestForSeisan.Enabled = true;
                }
                else
                {
                    BtnTestForSeisan.Text = "生産ログ作成開始";
                    TimTestForSeisan.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnTestForSeisan_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 生産ログテストデータ作成処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimTestForSeisan_Tick(object sender, EventArgs e)
        {
            string strPutData;
            string strPutFileName;
            string strPutFilePath;
            string[] strArray;
            string sInstructionData;
            try
            {
                for (var TestCnt = 0; TestCnt <= 0; TestCnt++)
                {
                    // 丁合指示データの表示の最後かどうかの確認
                    if (intForSesanDataIndex > LstCollatingData.Items.Count - 1)
                    {
                        // MsgBox("生産ログデータの最後まで達した")
                        return;
                    }

                    strArray = PubConstClass.pblControlDataFileName.Split('_');
                    //strPutFileName = strArray[0] + "_";
                    strPutFileName = "";
                    strPutData = "";

                    for (var intLoopCnt = 1; intLoopCnt <= 5; intLoopCnt++)
                    {
                        // 丁合指示データの表示の最後かどうかの確認
                        if (intForSesanDataIndex > LstCollatingData.Items.Count - 1)
                        {
                            // MsgBox("生産ログデータの最後まで達した")
                            // 制御データファイル名称の生成（上書保存）
                            //strPutFileName += LstCollatingData.Items[intForSesanDataIndex - 1].SubItems[45].Text + ".plg";
                            strPutFilePath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder) + strPutFileName;
                            using (StreamWriter sw = new StreamWriter(strPutFilePath, false, Encoding.Default))
                            {
                                sw.WriteLine(strPutData);
                            }
                            return;
                        }

                        // 最初の作業コード
                        if (intLoopCnt == 1)
                        {
                            // 固定値「SIT」
                            strPutFileName += "SIT";
                            // ライン番号
                            if (PubConstClass.pblLineNumber == "1")
                            {
                                // 1：TP古賀
                                strPutFileName += "1_";
                                sInstructionData = "YK520P_";

                            }
                            else if(PubConstClass.pblLineNumber == "2")
                            {
                                // 2：サンシャインワークス
                                strPutFileName += "2_";
                                sInstructionData = "YK520P2_";
                            }
                            else
                            {
                                // 3：予備
                                strPutFileName += "3_";
                                sInstructionData = "YK520P3_";
                            }
                            
                            // 企画回（6桁）＝企画週（2桁）＋生協コード（2桁）＋デポNo（2桁）
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[1].Text;
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[3].Text;
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[5].Text + "_";
                            // 配布（6桁）＝配布曜日（1桁）＋配布方面（1桁）＋配布方面No（1桁）
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[8].Text;
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[9].Text;
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[10].Text + "_";
                            // 丁合指示データファイル名（YK520P：TP古賀／YK520P2：サンシャインワークス）
                            strPutFileName += sInstructionData;
                            // 組合員コード（8桁）＋連番（6桁）
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[18].Text.PadLeft(8).Replace(" ", "0");
                            strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[44].Text.PadLeft(6).Replace(" ", "0") + ".plg";
                        }
                        // 最後（５番目）の組合員コード
                        if (intLoopCnt == 5)
                        {
                            //strPutFileName += DateTime.Now.ToString("yyyyMMddHHmmss") + ".plg";
                            //strPutFileName += LstCollatingData.Items[intForSesanDataIndex].SubItems[14].Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".plg";
                        }

                        // 企画（6桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[1].Text;    // 企画  （2桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[3].Text;    // 生協CD（2桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[5].Text;    // デポMo（2桁）
                        // 配布（3桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[8].Text;    // 配布曜日  （1桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[9].Text;    // 配布方面  （1桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[10].Text;   // 配布方面No（1桁）
                        // ライン番号（1桁）
                        strPutData += "2";      // （1：TP古賀）（2：サンシャインワークス）（3：予備）
                        // 組合員コード（8桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[18].Text.PadLeft(8).Replace(" ","0");
                        // セット順番（6桁）
                        strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[44].Text + ",";

                        // 生産日付
                        strPutData += DateTime.Now.ToString("yyyyMMdd") + ",";
                        // 生産日時
                        strPutData += DateTime.Now.ToString("hhmmss") + ",";

                        // 「NG製品」ﾁｪｯｸﾎﾞｯｸｽにﾁｪｯｸが入っていたら「排出結果をエラーとする」
                        if (ChkNgProduct.Checked == true)
                        {
                            strPutData += "00000000000" + "11,";
                            // ﾁｪｯｸを外す（生産ログ１ファイル分とする）
                            ChkNgProduct.Checked = false;
                        }
                        else
                        {
                            strPutData += "00000000000" + "00,";
                        }

                        // フィーダー結果（16桁）
                        strPutData += "123456789*123456";
                        // 帳票結果（6桁）
                        strPutData += "123456";
                        // 予備フィーダー結果（2桁）
                        strPutData += "12";
                        // 名寄せ枚数（8桁）
                        strPutData += "01011111,";

                        if (intLoopCnt == 5)
                        {
                            // 最後のデータには<CR><LF>を付加しない
                            strPutData += "12345678";
                        }
                        else
                        {
                            strPutData += "12345678" + Constants.vbCrLf;
                        }

                        intForSesanDataIndex += 1;
                        Debug.Print("■intForSesanDataIndex＝" + intForSesanDataIndex.ToString());
                    }

                    // テスト用生産ログファイルの生成（上書保存）
                    strPutFilePath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder) + strPutFileName;
                    using (StreamWriter sw = new StreamWriter(strPutFilePath, false, Encoding.Default))
                    {
                        sw.WriteLine(strPutData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【TimTestForSeisan_Tick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// テスト最終生産ログ作成処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLastSeisanLog_Click(Object sender, EventArgs e)
        {
            string strPutData;
            string strPutFileName;
            string strPutFilePath;
            string[] strArray;
            int iLoopCnt = 0;

            try
            {
                strArray = PubConstClass.pblControlDataFileName.Split('_');
                // 「SIT」＋「生協コード」＋「_」＋「企画回」＋「_」＋「週」
                strPutFileName = strArray[0] + "_" + strArray[1] + "_" + LstCollatingData.Items[0].SubItems[8].Text + "_";
                strPutData = "";

                // 最初の作業コード
                strPutFileName += LstCollatingData.Items[0].SubItems[14].Text + "_";
                // 最後の作業コード
                strPutFileName += LstCollatingData.Items[LstCollatingData.Items.Count - 1].SubItems[14].Text + ".plog";

                int iWriteCount = 200;
                int iHHmmss = 1;
                // 制御データファイル名称の生成（上書保存）
                strPutFilePath = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblSeisanFolder) + strPutFileName;
                using (StreamWriter sw = new StreamWriter(strPutFilePath, false, Encoding.Default))
                {
                    for (iLoopCnt = 0; iLoopCnt < LstCollatingData.Items.Count; iLoopCnt++)
                    {
                        strPutData = "";
                        // 企画回（5桁）
                        strPutData += LstCollatingData.Items[iLoopCnt].SubItems[1].Text;
                        // 配達曜日（1桁）
                        strPutData += LstCollatingData.Items[iLoopCnt].SubItems[8].Text;
                        // 生協（3桁）
                        strPutData += Convert.ToInt32(LstCollatingData.Items[iLoopCnt].SubItems[4].Text).ToString("000");
                        // 機械（1桁）
                        strPutData += Convert.ToInt32(LstCollatingData.Items[iLoopCnt].SubItems[4].Text).ToString("0");
                        // 組合員コード（9桁）
                        strPutData += LstCollatingData.Items[iLoopCnt].SubItems[14].Text + " ";
                        // 生産日付
                        strPutData += DateTime.Now.ToString("yyyyMMdd") + " ";
                        // 生産日時
                        //strPutData += DateTime.Now.ToString("HHmmss") + " ";                        
                        iWriteCount--;
                        if (iWriteCount <= 0)
                        {
                            iWriteCount = 200;
                            iHHmmss++;
                        }
                        strPutData += (iHHmmss * 10000).ToString("000000") + " ";
                        // ｼﾞｬﾑ登録
                        strPutData += "00";
                        // 排出結果
                        strPutData += "0";
                        // 最終結果
                        strPutData += "0";
                        strPutData += " ";
                        // フィーダー／帳票 結果
                        for (int intFeedCnt = 0; intFeedCnt < 27; intFeedCnt++)
                        {
                            //strPutData += LstCollatingData.Items[intForSesanDataIndex].SubItems[24 + intFeedCnt].Text;
                            strPutData += "0";
                        }
                        strPutData += " ";
                        // BCR1～7
                        strPutData += "0000000";

                        sw.WriteLine(strPutData);

                        LblVersion.Text = (iLoopCnt + 1).ToString() + "件作成";

                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnLastSeisanLog_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「手直し品 登録」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdjust_Click(Object sender, EventArgs e)
        {
            try
            {
                if (PubConstClass.intCollatingIndex == 0)
                {
                    MessageBox.Show("「丁合指示データ取込」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (CmbShipping.Items.Count == 0)
                {
                    MessageBox.Show("「制御データ作成」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonModule.OutPutLogFile("■「手直し品 登録」ボタン（丁合指示データ読込画面）クリック");

                EntryAdjustForm form = new EntryAdjustForm();
                form.Show(this);
                form.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnAdjust_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// バージョン表示ラベルﾀﾞﾌﾞﾙｸﾘｯｸ（テスト用★隠しコマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblVersion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (BtnTestForSeisan.Visible == false)
                {
                    CmbShipping.Visible = true;
                    BtnTestForSeisan.Visible = true;
                    ChkNgProduct.Visible = true;
                    BtnLastSeisanLog.Visible = true;
                    Label2.Visible = true;
                    CmbShippingRePrint.Visible = true;
                    CmbOrderRePrint.Visible = true;
                    CommonModule.OutPutLogFile("■「生産ログ作成テスト」ボタン表示");
                }
                else
                {
                    CmbShipping.Visible = false;
                    BtnTestForSeisan.Visible = false;
                    ChkNgProduct.Visible = false;
                    BtnLastSeisanLog.Visible = false;
                    Label2.Visible = false;
                    CmbShippingRePrint.Visible = false;
                    CmbOrderRePrint.Visible = false;
                    CommonModule.OutPutLogFile("■「生産ログ作成テスト」ボタン非表示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【LblVersion_DoubleClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「出荷表 再印刷」コンボボックスの選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbShippingRePrint_SelectedIndexChanged(Object sender, EventArgs e)
        {
            CmbShipping.SelectedIndex = CmbShippingRePrint.SelectedIndex;
        }

        /// <summary>
        /// 「検索」ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(Object sender, EventArgs e)
        {
            try
            {
                if (PubConstClass.intCollatingIndex == 0)
                {
                    MessageBox.Show("「丁合指示データ取込」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (CmbShipping.Items.Count == 0)
                {
                    MessageBox.Show("「制御データ作成」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonModule.OutPutLogFile("■「検索」ボタン（丁合指示データ読込画面）クリック");
                SearchForm form = new SearchForm();
                form.Show();
                form.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnSearch_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 作成開始行の設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstCollatingData_DoubleClick(object sender, EventArgs e)
        {
            DialogResult result;
            try
            {
                if (PubConstClass.pblIsDriving == false)
                {
                    // 運転中で無い時のみ有効
                    String sMessage = "制御データ開始行を（" + (LstCollatingData.SelectedItems[0].Index + 1).ToString() + "）に設定して再読み込みしますか？";
                    result = MessageBox.Show(sMessage, "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                        LblStartRow.Text = "制御データ作成開始行：" + (LstCollatingData.SelectedItems[0].Index + 1).ToString("000000");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【LstCollatingData_DoubleClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQrRead_Click(object sender, EventArgs e)
        {
            string sQrReadData;
            string[] sArray;

            try
            {
                QrReadForm frm = new QrReadForm();
                frm.ShowDialog();
                if (frm.bOKFlag == true)
                {
                    sQrReadData = frm.sQrReadData;
                    if (sQrReadData == null || sQrReadData == "")
                    {
                        MessageBox.Show("ＱＲデータを読込んで下さい", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    sArray = sQrReadData.Split(',');

                    // 処理生協の設定
                    //CmbCoopName.SelectedIndex = int.Parse(sArray[0]);

                    // 結束最大束数
                    LblMaxBundle.Text = sArray[3];
                    // カゴ車への積載数
                    LblFoldingCon.Text = sArray[4];

                    // 「丁合指示データ取込」ボタン呼び出し
                    BtnSelect.PerformClick();
                    // 「制御データ作成」ボタン呼び出し
                    BtnMakeControlDataFile.PerformClick();

                    // 制御データ作成処理呼び出し
                    //MakeControlDataFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー【BtnQrRead_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 各ボタン等を使用可能とする
                BtnIsEnable(true);
                // １箱最大束数は制御データを作成すると丁合指示データを
                // 再度読み込みしないと選択不可とする。
                //CmbMaxBundle.Enabled = false;
            }
        }

        /// <summary>
        /// QRデータ印字処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintQrWrite_PrintPage(object sender, PrintPageEventArgs e)
        {
            int intHdrXPos;     // ヘッダーのＸ座標
            int intHdrYPos;     // ヘッダーのＹ座標
            int intRowHeight;   // １行の高さ

            try
            {
                // 1mm≒4.11
                intRowHeight = 25;

                Font f0 = new Font("ＭＳ ゴシック", 12, FontStyle.Regular);
                Font f1 = new Font("ＭＳ ゴシック", 16, FontStyle.Regular);
                Font f2 = new Font("ＭＳ ゴシック", 18, FontStyle.Regular);
                Font f3 = new Font("ＭＳ ゴシック", 20, FontStyle.Regular);
                Font f4 = new Font("ＭＳ ゴシック", 24, FontStyle.Regular);
                Font f5 = new Font("ＭＳ ゴシック", 26, FontStyle.Regular);
                Font f6 = new Font("ＭＳ ゴシック", 74, FontStyle.Regular);

                Pen blackPen = new Pen(Color.Black, 1)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
                };

                intHdrXPos = 20;
                intHdrYPos = 120;

                string sPrintData;
                string sQrData = "";

                sPrintData = "印刷日時：" + DateAndTime.Now.ToString("yyyy年MM月dd日 HH時mm分ss秒");
                e.Graphics.DrawString(sPrintData, f0, Brushes.Black, 800, 40);

                //sPrintData = "（１）処理生協　　　　　　" + CmbCoopName.Text;
                //sQrData += CmbCoopName.SelectedIndex.ToString() + ",";
                //e.Graphics.DrawString(sPrintData, f1, Brushes.Black, intHdrXPos + 20, intHdrYPos + 10);
                //intHdrYPos += 60;

                sPrintData = "（３）丁合指示データ　　　" + LblCollatingFile.Text;
                //sQrData += LblCollatingFile.Text.Replace(":","+").Replace(@"\","//") + ",";
                sQrData += LblCollatingFile.Text + ",";
                e.Graphics.DrawString(sPrintData, f1, Brushes.Black, intHdrXPos + 20, intHdrYPos + 10);
                intHdrYPos += 60;

                //sPrintData = "（２）棚替えフラグ　　　　　　　　0028：城西／0038：町田";
                //e.Graphics.DrawString(sPrintData, f1, Brushes.Black, intHdrXPos + 20, intHdrYPos + 10);
                //intHdrYPos += 60;

                sPrintData = "（４）結束最大束数　　　　" + LblMaxBundle.Text + "束";
                sQrData += LblMaxBundle.Text + ",";
                e.Graphics.DrawString(sPrintData, f1, Brushes.Black, intHdrXPos + 20, intHdrYPos + 10);
                intHdrYPos += 60;

                sPrintData = "（５）カゴ車への積載数　  " + LblFoldingCon.Text;
                sQrData += LblFoldingCon.Text + ",";
                e.Graphics.DrawString(sPrintData, f1, Brushes.Black, intHdrXPos + 20, intHdrYPos + 10);
                intHdrYPos += 60;

                // 150x150サイズのImageオブジェクトを作成する
                Bitmap img = new Bitmap(150, 150);
                // ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics g = Graphics.FromImage(img);

                // ＱＲコードをイメージを取得する
                string sTestData = sQrData;
                //bc1.QRWriteBar("A" + sTestData, 0, 0, 3, g);
                bc1.QRWriteBar(sTestData, 0, 0, 3, g);

                // リソースを解放する
                g.Dispose();
                // ＱＲコードの印字
                e.Graphics.DrawImage(img, new Rectangle(intHdrXPos + 70, intHdrYPos, img.Width, img.Height));

                intHdrYPos += intRowHeight;
                intHdrYPos += intRowHeight;
                intHdrYPos += intRowHeight;
                intHdrYPos += intRowHeight;
                intHdrYPos += intRowHeight;
                intHdrYPos += intRowHeight;

                sPrintData = "　　QR内容　" + sQrData;
                e.Graphics.DrawString(sPrintData, f0, Brushes.Black, intHdrXPos + 20, intHdrYPos + 10);

                //　次ページ無し
                e.HasMorePages = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintQrWrite_PrintPage】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「ＱＲ書込」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQrWrite_Click(object sender, EventArgs e)
        {
            try
            {
                if (PubConstClass.intCollatingIndex == 0)
                {
                    MessageBox.Show("「丁合指示データ取込」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ListOrderList.Items.Count == 0)
                {
                    MessageBox.Show("「制御データ作成」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                PrintDocument pd = new PrintDocument();
                // 用紙方向 横向き
                pd.DefaultPageSettings.Landscape = true;
                // 用紙サイズを指定する
                PaperKind pPaperSz;
                pPaperSz = PaperKind.A4;    // A4 サイズの定数

                bool setPaperSize = false;
                // サポートされている用紙サイズの一覧を取得
                foreach (PaperSize pkSize in PrintDocument.PrinterSettings.PaperSizes)
                {
                    // 指定の用紙サイズがサポートされているか
                    if (pkSize.Kind == pPaperSz)
                    {
                        // 指定の用紙サイズが見つかったら用紙サイズを設定する
                        pd.DefaultPageSettings.PaperSize = pkSize;
                        setPaperSize = true;     // 設定完了のフラグ
                    }
                }
                if (setPaperSize == false)
                {
                    MessageBox.Show("指定の用紙サイズが設定できませんでした。");
                    return;
                }
                // PrintPageイベントハンドラの追加
                pd.PrintPage += PrintQrWrite_PrintPage;
                CommonModule.OutPutLogFile("■ＱＲコード印刷プレビュー");

                // 印刷プレビューダイアログを表示する
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Width = 1000,
                    Height = 800,
                    // プレビューするPrintDocumentを設定
                    Document = pd

                };
                // 印刷プレビューダイアログを表示する
                ppd.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【BtnPrintPickingList_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「印刷」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            string strLoadingDataPath;

            try
            {
                switch (CmbPrintType.SelectedIndex)
                {                    
                    case 0:
                        // 吊札
                        PrintHangingTag();
                        break;
                                            
                    case 1:
                        // 出荷ラベル                        
                        MessageBox.Show("ジャーナルプリンタ「(富士通)FP-510II(USB)」で出力予定", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    
                    case 2:
                        // カゴ車一覧（デポ毎）  [List of number of cars in basket]
                        strLoadingDataPath = GetPathForListOfNumberOfCarsInBasket();
                        if (!File.Exists(strLoadingDataPath))
                        {
                            MessageBox.Show("「カゴ車一覧（デポ毎）」のデータが作成されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        // データ読込処理
                        CommonModule.ReadListOfNumberOfCarsInBasket(strLoadingDataPath);
                        CommonProcessingForListPrinting(strLoadingDataPath);
                        break;
                    
                    case 3:
                        // 処理件数一覧（生協毎）  [List of processed items]
                        strLoadingDataPath = GetPathForListOfProcessedItems();
                        if (!File.Exists(strLoadingDataPath))
                        {
                            MessageBox.Show("「処理件数一覧（生協毎）」のデータが作成されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        // データ読込処理
                        CommonModule.ReadListOfProcessedItems(strLoadingDataPath);
                        CommonProcessingForListPrinting(strLoadingDataPath);
                        break;
                    
                    case 4:
                        // 出荷確認票  [shipment confirmation table]
                        strLoadingDataPath = GetPathForShipmentConfirmationTable();
                        if (!File.Exists(strLoadingDataPath))
                        {
                            MessageBox.Show("「出荷確認票」のデータが作成されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        // データ読込処理
                        CommonModule.ReadShipmentConfirmationTable(strLoadingDataPath);
                        CommonProcessingForListPrinting(strLoadingDataPath);
                        break;
                    
                    case 5:
                        // 帳票枚数一覧
                        PrintNumberOfConfirmationTable();
                        break;
                    
                    case 6:
                        // テスト印字
                        PrintHangingTag();
                        break;
                    
                    case 7:
                        // 雛形印字
                        PrintHangingTag();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【BtnPrint_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                                   
        }

        /// <summary>
        /// 吊札印刷処理
        /// </summary>
        private void PrintHangingTag()
        {
            string strHangingTagPath;

            try
            {
                PubConstClass.hangingTagTable.Clear();

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                // 吊札ファイル名取得処理
                strHangingTagPath = GetPathForHangingTag();

                if (!File.Exists(strHangingTagPath))
                {
                    MessageBox.Show("「吊札」のデータが作成されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // 吊札データ読込処理
                CommonModule.ReadHangingTagData(strHangingTagPath);

                pd.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

                PubConstClass.printIndex = 0;

                //印刷の選択ダイアログを表示する
                PrintDialog pdlg = new PrintDialog
                {
                    Document = pd,
                    AllowSomePages = true
                };
                //ページ指定の最小値と最大値を指定する
                pdlg.PrinterSettings.MinimumPage = 1;
                if (PubConstClass.hangingTagTable.Count == 0)
                {
                    pdlg.PrinterSettings.MaximumPage = 1;
                }
                else
                {
                    pdlg.PrinterSettings.MaximumPage = PubConstClass.hangingTagTable.Count;
                }

                pdlg.PrinterSettings.MaximumPage = 1;

                //印刷開始と終了ページを指定する
                pdlg.PrinterSettings.FromPage = pdlg.PrinterSettings.MinimumPage;
                pdlg.PrinterSettings.ToPage = pdlg.PrinterSettings.MaximumPage;
                //印刷の選択ダイアログを表示する
                if (pdlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                //PrintPreviewDialogオブジェクトの作成
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Width = 1000,
                    Height = 800,
                    //プレビューするPrintDocumentを設定
                    Document = pd
                };
                //印刷プレビューダイアログを表示する
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintHangingTag】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 一覧印字の為の共通処理
        /// </summary>
        /// <param name="strLoadingDataPath"></param>
        private void CommonProcessingForListPrinting(string strLoadingDataPath)
        {
            try
            {
                //PubConstClass.listTablePrintDatas.Clear();

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                // 用紙方向 横向き
                pd.DefaultPageSettings.Landscape = true;

                pd.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

                PubConstClass.printIndex = 0;

                //印刷の選択ダイアログを表示する
                PrintDialog pdlg = new PrintDialog
                {
                    Document = pd,
                    AllowSomePages = true
                };
                ////ページ指定の最小値と最大値を指定する
                //pdlg.PrinterSettings.MinimumPage = 1;
                //if (PubConstClass.loadingTablePrintDatas.Count == 0)
                //{
                //    pdlg.PrinterSettings.MaximumPage = 1;
                //}
                //else
                //{
                //    pdlg.PrinterSettings.MaximumPage = PubConstClass.loadingTablePrintDatas.Count;
                //}

                pdlg.PrinterSettings.MaximumPage = 1;

                //印刷開始と終了ページを指定する
                pdlg.PrinterSettings.FromPage = pdlg.PrinterSettings.MinimumPage;
                pdlg.PrinterSettings.ToPage = pdlg.PrinterSettings.MaximumPage;
                //印刷の選択ダイアログを表示する
                if (pdlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                //PrintPreviewDialogオブジェクトの作成
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Width = 1000,
                    Height = 800,
                    //プレビューするPrintDocumentを設定
                    Document = pd
                };
                //印刷プレビューダイアログを表示する
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【CommonProcessingForListPrinting】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 帳票枚数一覧印刷処理
        /// </summary>
        private void PrintNumberOfConfirmationTable()
        {
            string strLoadingDataPath;

            try
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                // 用紙方向 横向き
                pd.DefaultPageSettings.Landscape = true;

                strLoadingDataPath = GetPathForNumberOfFormsConfirmationTable();

                if (!File.Exists(strLoadingDataPath))
                {
                    MessageBox.Show("「帳票枚数一覧」のデータが作成されていません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // 帳票枚数一覧データ読込処理
                CommonModule.ReadNumberOfConfirmationTable(strLoadingDataPath);

                pd.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

                PubConstClass.printIndex = 0;

                //印刷の選択ダイアログを表示する
                PrintDialog pdlg = new PrintDialog
                {
                    Document = pd,
                    AllowSomePages = true
                };
                pdlg.PrinterSettings.MaximumPage = 1;

                //印刷開始と終了ページを指定する
                pdlg.PrinterSettings.FromPage = pdlg.PrinterSettings.MinimumPage;
                pdlg.PrinterSettings.ToPage = pdlg.PrinterSettings.MaximumPage;
                //印刷の選択ダイアログを表示する
                if (pdlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                //PrintPreviewDialogオブジェクトの作成
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Width = 1000,
                    Height = 800,
                    //プレビューするPrintDocumentを設定
                    Document = pd
                };
                //印刷プレビューダイアログを表示する
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【PrintNumberOfFormsList】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 結束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbUnityTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] sArray;
            string[] sArraySub;

            try
            {
                //　結束最大束数
                sArray = CmbUnityTable.Text.Split('／');
                sArraySub = sArray[0].Split('：');
                LblMaxBundle.Text = sArraySub[1];
                // カゴ車への積載数
                sArraySub = sArray[1].Split('：');
                LblFoldingCon.Text = sArraySub[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【CmbUnityTable_SelectedIndexChanged】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                switch (CmbPrintType.SelectedIndex)
                {
                    // 0：吊札
                    case 0:
                        CommonModule.Pd_PrinthangingTag(sender, e, 0);
                        break;

                    // 1：出荷ラベル
                    case 1:
                        CommonModule.Pd_PrinthangingTag(sender, e, 1);
                        break;

                    // 2：カゴ車一覧（デポ毎）※リスト形式印刷
                    case 2:
                        CommonModule.Pd_PrintList(sender, e, 0);
                        break;

                    // 3：処理件数一覧（生協毎）※リスト形式印刷
                    case 3:
                        CommonModule.Pd_PrintList(sender, e, 1);
                        break;

                    // 4：出荷確認票　※リスト形式印刷
                    case 4:
                        CommonModule.Pd_PrintList(sender, e, 2);
                        break;

                    // 5：帳票枚数一覧　※リスト形式印刷
                    case 5:
                        CommonModule.Pd_PrintList(sender, e, 3);
                        break;

                    // 6：テスト印字
                    case 6:
                        CommonModule.Pd_PrintLoadingTableForTest(sender, e);
                        break;

                    // 7：雛形印字
                    case 7:
                        CommonModule.Pd_PrintLoadingTableForTemplate(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【PrintDocument_PrintPage】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「棚替え設定」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShelfChange_Click(object sender, EventArgs e)
        {
            try
            {
                ShelfChangeForm shelfChangeForm = new ShelfChangeForm();
                shelfChangeForm.New(lstTemporaryWareHouse);
                shelfChangeForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー【BtnShelfChange_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// 積付表Ａデータ作成処理
        /// </summary>
        /// <param name="sFromData"></param>
        /// <param name="sToData"></param>
        /// <param name="iSetCount"></param>
        /// <param name="iBasketSerialNumber"></param>
        /// <param name="CoopUnitSerialNumber"></param>
        /// <returns></returns>
        private string CreateLodingDataTypeA(string sFromData, string sToData, int iSetCount, int iBasketSerialNumber, int CoopUnitSerialNumber, string sEndMark)
        {
            string[] sFromArray;
            string[] sToArray;
            string sLoadingData = "";

            try
            {
                sFromArray = sFromData.Split(',');
                sToArray = sToData.Split(',');

                // ⓪ ENDマーク（デポの最後のカゴの時「１」）
                sLoadingData += sEndMark + ",";
                // ① 生協コード
                sLoadingData += sFromArray[2] + ",";
                // ② 生協名
                sLoadingData += GetCoopNameFromCoopCode(sFromArray[2]) + ",";
                // ③ デポコード
                sLoadingData += sFromArray[3] + ",";
                // ④ 基準曜日
                sLoadingData += sFromArray[7] + sFromArray[8] + ",";
                if ((sFromArray[7] + sFromArray[8]) == (sToArray[7] + sToArray[8]))
                {
                    // ⑤ 便区分
                    sLoadingData += ",";
                }
                else
                {
                    // ⑤ 便区分
                    sLoadingData += sToArray[7] + sToArray[8] + ",";
                }
                // ⑥ デポ名
                sLoadingData += GetDepoNameFromDepoCode(sFromArray[2]) + ",";

                // ⑦ 組合員配達日（デフォルト：当日＋３日）半角→全角変換
                DateTime nowdt = DTPDeliveryDate.Value;
                //sLoadingData += Strings.StrConv(nowdt.AddDays(3).ToString("MM月dd日"), VbStrConv.Wide, 0x411) + ",";
                sLoadingData += Strings.StrConv(nowdt.ToString("MM月dd日"), VbStrConv.Wide, 0x411) + ",";

                // ⑧ 担当コース
                sLoadingData += sFromArray[6] + "～" + sToArray[6] + ",";

                // ⑨ 出荷束数（最大が「カゴ車への積載数」となる）
                sLoadingData += iSetCount.ToString("0") + ",";
                // ⑩ セット数/束（結束最大束数）
                sLoadingData += PubConstClass.iMaxNumberOfBundle.ToString("0") + ",";

                // ⑪ セット順所番号
                sLoadingData += ReplaceZeroToSpace(sFromArray[56], 0) + "～" + ReplaceZeroToSpace(sToArray[57], 1) + ",";

                // ⑫ カゴ連番（デポ）
                sLoadingData += iBasketSerialNumber.ToString("0") + ",";
                // ⑬ 配達曜日コース①
                //sLoadingData += DeliveryDayConversionTable(sFromArray[2], sFromArray[7], sFromArray[8]) + ",";
                sLoadingData += "8111";
                if ((sFromArray[2] + sFromArray[7] + sFromArray[8]) == (sToArray[2] + sToArray[7] + sToArray[8]))
                {
                    // ⑭ 配達曜日コース②
                    sLoadingData += ",";
                }
                else
                {
                    // ⑭ 配達曜日コース②
                    //sLoadingData += DeliveryDayConversionTable(sToArray[2], sToArray[7], sToArray[8]) + ",";
                    sLoadingData += "8111";
                }

                // ⑮ 支所コード（先頭）
                sLoadingData += sFromArray[44] + ",";
                // ⑯ 支所名（先頭）
                sLoadingData += GetOfficeNameFromOfficeCode(sFromArray[44]) + ",";
                

                if (sFromArray[44] == sToArray[44])
                {
                    // ⑰ 支所コード（最後）
                    sLoadingData += ",";
                    // ⑱ 支所名（最後）
                    sLoadingData += ",";
                }
                else
                {
                    // ⑰ 支所コード（最後）
                    sLoadingData += sToArray[44] + ",";
                    // ⑱ 支所名（最後）
                    sLoadingData += GetOfficeNameFromOfficeCode(sToArray[44]) + ",";
                }

                // ⑲ 頁番号
                sLoadingData += CoopUnitSerialNumber.ToString("0") + ",";
                // 企画回（積付表Ａ印字には使用しない）
                sLoadingData += sFromArray[0];

                return sLoadingData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【CreateLodingDataTypeA】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /// <summary>
        /// 生協コードから生協名を取得する
        /// </summary>
        /// <param name="sCoopCode"></param>
        /// <returns></returns>
        private string GetCoopNameFromCoopCode(string sCoopCode)
        {
            string sCoopName;
            string[] sArray;
            try
            {
                sCoopName = "該当なし";
                for (var K = 0; K <= PubConstClass.intMasterOfOrgCnt - 1; K++)
                {
                    if (PubConstClass.pblMasterOfOrg[K, 0].Substring(0, 4) == "00" + sCoopCode)
                    {
                        sArray = PubConstClass.pblMasterOfOrg[K, 1].Split(',');
                        sCoopName = sArray[0];
                        break;
                    }
                }
                return sCoopName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetCoopNameFromCoopCode】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /// <summary>
        /// デポコードからデポ名を取得する
        /// </summary>
        /// <param name="sDepoCode"></param>
        /// <returns></returns>
        private string GetDepoNameFromDepoCode(string sDepoCode)
        {
            string sDepoName;
            string[] sArray;
            try
            {
                sDepoName = "該当なし";
                for (var K = 0; K <= PubConstClass.intMasterOfOrgCnt - 1; K++)
                {
                    if (PubConstClass.pblMasterOfOrg[K, 0].Substring(4, 4) == "00" + sDepoCode)
                    {
                        sArray = PubConstClass.pblMasterOfOrg[K, 1].Split(',');
                        sDepoName = sArray[1];
                        break;
                    }
                }
                return sDepoName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【GetDepoNameFromDepoCode】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /// <summary>
        /// 「0」を空白に置き換える処理
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="iMode"></param>
        /// <returns></returns>
        private string ReplaceZeroToSpace(string sData, int iMode)
        {
            string sRetData;
            int iCount;

            try
            {
                if (sData=="")
                {
                    return sData;
                }
                iCount = sData.Length - int.Parse(sData).ToString("0").Length;
                string repeatedString = new string(' ', iCount);
                if (iMode == 0)
                {
                    // 先頭に付加
                    sRetData = repeatedString + int.Parse(sData).ToString("0");
                }
                else
                {
                    // 後方に付加
                    sRetData = int.Parse(sData).ToString("0") + repeatedString;
                }

                return sRetData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【ReplaceZeroToSpace】", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return sData;
            }
        }

        /// <summary>
        /// 配達曜日変換処理（生協CD＋曜日＋便から変換する）
        /// </summary>
        /// <param name="sCoopCode">生協CD</param>
        /// <param name="sYoubi">曜日</param>
        /// <param name="sBin">便</param>
        /// <returns></returns>
        //private string DeliveryDayConversionTable(string sCoopCode, string sYoubi, string sBin)
        //{
        //    string sDeliveryWeekCourse = "";

        //    try
        //    {
        //        switch (sCoopCode)
        //        {
        //            case PubConstClass.CODE_FCOOP:
        //            case PubConstClass.CODE_COOP_KUMAMOTO:
        //                sDeliveryWeekCourse = sYoubi;
        //                if (sBin == "1")
        //                {
        //                    sDeliveryWeekCourse += "A";
        //                }
        //                else
        //                {
        //                    sDeliveryWeekCourse += "P";
        //                }
        //                break;

        //            case PubConstClass.CODE_COOP_SAGA:
        //            case PubConstClass.CODE_COOP_OITA:
        //            case PubConstClass.CODE_COOP_KAGOSHIMA:
        //                sDeliveryWeekCourse = sYoubi + sBin;
        //                break;

        //            case PubConstClass.CODE_LALA_COOP:
        //                switch (sYoubi)
        //                {
        //                    case "1":
        //                    case "2":
        //                    case "3":
        //                        sDeliveryWeekCourse = sYoubi + sBin;
        //                        break;

        //                    case "4":
        //                        if (sBin == "1")
        //                        {
        //                            sDeliveryWeekCourse = "13";
        //                        }
        //                        else
        //                        {
        //                            sDeliveryWeekCourse = "14";
        //                        }
        //                        break;

        //                    case "5":
        //                        if (sBin == "1")
        //                        {
        //                            sDeliveryWeekCourse = "23";
        //                        }
        //                        else
        //                        {
        //                            sDeliveryWeekCourse = "24";
        //                        }
        //                        break;
        //                }
        //                break;

        //            default:
        //                break;

        //        }
        //        return sDeliveryWeekCourse;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.StackTrace, "【DeliveryDayConversionTable】", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return "XX・XX";
        //    }
        //}

        /// <summary>
        /// タイトルラベルダブルクリック処理（隠しコマンド）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblTitle_DoubleClick(object sender, EventArgs e)
        {
            CommonModule.OutPutLogFile("■タイトルラベルダブルクリック");
            
            if (form.Visible == true)
            {
                CommonModule.OutPutLogFile("■「実績ログ表示画面」が既に表示されている");
                form.Visible = false;
            }
            else
            {
                CommonModule.OutPutLogFile("■「実績ログ表示画面」が非表示");
                form.Visible = true;
            }            
        }

        /// <summary>
        /// 「カゴ車確認」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBasketCarCheck_Click(object sender, EventArgs e)
        {
            try
            {
                //if (PubConstClass.intCollatingIndex == 0)
                //{
                //    MessageBox.Show("「丁合指示データ取込」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //if (CmbShipping.Items.Count == 0)
                //{
                //    MessageBox.Show("「制御データ作成」を行ってください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                CommonModule.OutPutLogFile("■「カゴ車確認」ボタンクリック");

                BasketCarCheckForm form = new BasketCarCheckForm();
                form.Show(this);
                form.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnBasketCarCheck_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDepotOrderSetting_Click(object sender, EventArgs e)
        {
            try
            {
                DepotOrderSettingForm depotOrderSettingForm = new DepotOrderSettingForm();
                //depotOrderSettingForm.New(lstTemporaryWareHouse);
                depotOrderSettingForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー【BtnDepotOrderSetting_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadCollatingDataForm_Activated(object sender, EventArgs e)
        {
            //LoadBindingTableFile();
        }

        private void BtnCheckNumberOfForm_Click(object sender, EventArgs e)
        {
            try
            {
                CommonModule.OutPutLogFile("■「帳票枚数確認」ボタンクリック");

                NumberOfFormOverList form = new NumberOfFormOverList();
                form.Show(this);
                form.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnCheckNumberOfForm_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LsvSelectiveData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string sReadFilePath = LsvSelectiveData.SelectedItems[0].SubItems[1].Text;

                LstFileContent.Items.Clear();
                LstCollatingData.Items.Clear();

                LblConter.Text = "";
                PicWaitList.Visible = true;
                // 進捗表示などが必要なら ProgressBar を併用可能
                await LoadFileToListBoxAsync(sReadFilePath);
                LblConter.Text = LstFileContent.Items.Count.ToString("#,###,##0") + "件";
                PicWaitList.Visible = false;

                string[] lines = File.ReadAllLines(sReadFilePath);

                string sLineLength = lines.Length.ToString("#,###,##0");
                blnCancelFlag = false;
                ProgressBar1.Minimum = 0;
                ProgressBar1.Maximum = lines.Length;

                // ListView コントロールの再描画を禁止
                LstCollatingData.BeginUpdate();
                // プログレスバー関係のコントルールを表示
                LblWaiting.Visible = true;
                ProgressBar1.Visible = true;
                LblProgress.Visible = true;
                BtnCancel.Visible = true;

                int iIndex = 0;
                //LstCollatingData.Items.Clear();
                PubConstClass.intCollatingIndex = 0;

                foreach (var line in File.ReadLines(sReadFilePath, Encoding.GetEncoding(932)))
                {
                    DispSelectiveData(line);

                    iIndex++;
                    if (iIndex % 100 == 0)
                    {
                        ProgressBar1.Value = iIndex;
                        LblProgress.Text = iIndex.ToString("#,###,##0") + "/" + sLineLength;                        
                    }
                    Application.DoEvents();
                    // 「キャンセル」ボタンクリックのチェック
                    if (blnCancelFlag == true)
                    {
                        break;
                    }
                }

                // ListView コントロールの再描画
                LstCollatingData.EndUpdate();
                // プログレスバー関係のコントルールを非表示
                LblWaiting.Visible = false;
                ProgressBar1.Visible = false;
                LblProgress.Visible = false;
                BtnCancel.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【LsvSelectiveData_DoubleClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async Task LoadFileToListBoxAsync(string path)
        {
            //const int BATCH_SIZE = 1000;  // UIへ追加する1回あたりの行数（調整可）
            const int BATCH_SIZE = 100;  // UIへ追加する1回あたりの行数（調整可）

            var buffer = new List<string>(BATCH_SIZE);

            // 読み込みはバックグラウンド、UI反映はUIスレッドで
            await Task.Run(() =>
            {
                // 文字コードは適宜変更（SJISなら Encoding.GetEncoding(932)）
                foreach (var line in File.ReadLines(path, Encoding.GetEncoding(932)))
                {                    
                    buffer.Add(line);
                    if (buffer.Count >= BATCH_SIZE)
                    {
                        var batch = buffer.ToArray();
                        buffer.Clear();

                        // UIスレッドへバッチ追加を投げる
                        LstFileContent.BeginInvoke((Action)(() =>
                        {                            
                            LstFileContent.BeginUpdate();
                            try
                            {
                                PicWaitList.Refresh();
                                LstFileContent.Items.AddRange(batch);
                            }
                            finally
                            {
                                LstFileContent.EndUpdate();
                            }
                        }));
                    }
                }
            });

            // 端数の反映
            if (buffer.Count > 0)
            {
                LstFileContent.BeginUpdate();
                try
                {
                    LstFileContent.Items.AddRange(buffer.ToArray());
                }
                finally
                {
                    LstFileContent.EndUpdate();
                }
            }
        }

        private void DispSelectiveData(string sSelectiveData)
        {
            string[] col = new string[39];
            ListViewItem itm;
            string strWorkData;

            try
            {
                int iColIndex = 0;
                
                col[iColIndex++] = (PubConstClass.intCollatingIndex + 1).ToString("000000");
                //col[iColIndex++] = sSelectiveData.Substring(0, 7);
                //col[iColIndex++] = sSelectiveData.Substring(7, 5);
                //col[iColIndex++] = sSelectiveData.Substring(12, 2);
                //col[iColIndex++] = sSelectiveData.Substring(14, 1);
                //col[iColIndex++] = sSelectiveData.Substring(15, 1);
                //col[iColIndex++] = sSelectiveData.Substring(16, 8/2);
                //col[iColIndex++] = sSelectiveData.Substring(20, 4);
                //col[iColIndex++] = sSelectiveData.Substring(24, 16/2);
                //col[iColIndex++] = sSelectiveData.Substring(32, 7);

                //col[iColIndex++] = sSelectiveData.Substring(39, 7);
                //col[iColIndex++] = sSelectiveData.Substring(46, 16/2);
                //col[iColIndex++] = sSelectiveData.Substring(53, 1);
                //col[iColIndex++] = sSelectiveData.Substring(54, 6);
                //col[iColIndex++] = sSelectiveData.Substring(60, 32/2);
                //col[iColIndex++] = sSelectiveData.Substring(76, 4);
                //col[iColIndex++] = sSelectiveData.Substring(80, 1);
                //col[iColIndex++] = sSelectiveData.Substring(81, 1);
                //col[iColIndex++] = sSelectiveData.Substring(82, 1);
                //col[iColIndex++] = sSelectiveData.Substring(83, 1);

                //col[iColIndex++] = sSelectiveData.Substring(84, 1);
                //col[iColIndex++] = sSelectiveData.Substring(85, 1);
                //col[iColIndex++] = sSelectiveData.Substring(86, 1);
                //col[iColIndex++] = sSelectiveData.Substring(87, 1);
                //col[iColIndex++] = sSelectiveData.Substring(88, 1);
                //col[iColIndex++] = sSelectiveData.Substring(89, 1);
                //col[iColIndex++] = sSelectiveData.Substring(90, 1);
                //col[iColIndex++] = sSelectiveData.Substring(91, 1);
                //col[iColIndex++] = sSelectiveData.Substring(92, 1);
                //col[iColIndex++] = sSelectiveData.Substring(93, 1);

                //col[iColIndex++] = sSelectiveData.Substring(94, 1);
                //col[iColIndex++] = sSelectiveData.Substring(95, 1);
                //col[iColIndex++] = sSelectiveData.Substring(96, 1);
                //col[iColIndex++] = sSelectiveData.Substring(97, 1);
                //col[iColIndex++] = sSelectiveData.Substring(98, 1);
                //col[iColIndex++] = sSelectiveData.Substring(99, 1);
                //col[iColIndex++] = sSelectiveData.Substring(100, 1);
                //col[iColIndex++] = sSelectiveData.Substring(101, 1);



                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 0, 7);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 7, 5);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 12, 2);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 14, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 15, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 16, 8);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 24, 4);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 28, 16);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 44, 7);

                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 51, 7);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 58, 16);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 74, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 75, 6);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 81, 32);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 113, 4);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 117, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 118, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 119, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 120, 1);

                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 121, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 122, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 123, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 124, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 125, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 126, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 127, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 128, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 129, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 130, 1);

                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 131, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 132, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 133, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 134, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 135, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 136, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 137, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 138, 1);
                col[iColIndex++] = SubstringByCp932Bytes(sSelectiveData, 139, 1);

                // データの表示
                itm = new ListViewItem(col);
                LstCollatingData.Items.Add(itm);
                LstCollatingData.Items[LstCollatingData.Items.Count - 1].UseItemStyleForSubItems = false;

                strWorkData = "";
                for (var intLoopCnt = 1; intLoopCnt <= 38; intLoopCnt++)
                {
                    strWorkData += col[intLoopCnt] + ",";
                }
                //CommonModule.OutPutLogFile($"■■読込データ({PubConstClass.intCollatingIndex.ToString("00000")})：{strWorkData}");

                PubConstClass.pblCollatingData.Add(strWorkData);
                PubConstClass.intCollatingIndex += 1;
                // 作業コードのインデックスにデータを格納する（検索で使用する）
                PubConstClass.pblReadCollating[Convert.ToInt32(col[0])] = strWorkData;

                if (LstCollatingData.Items.Count % 2 == 0)
                {
                    // 偶数行の色反転
                    for (var intLoopCnt = 0; intLoopCnt <= 38; intLoopCnt++)
                    {
                        LstCollatingData.Items[LstCollatingData.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.FromArgb(200, 200, 230);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DispSelectiveData】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// CP932（Windows-31J）でのバイト数を桁として、
        /// Nバイト目からMバイトぶんを切り出す（文字境界を壊さない）。
        /// </summary>
        public static string SubstringByCp932Bytes(string s, int startBytes, int byteCount)
        {
            if (string.IsNullOrEmpty(s) || byteCount <= 0) return string.Empty;

            var enc = Encoding.GetEncoding(932); // CP932
                                                 // 文字破壊を防ぐため、グラフェム単位で積み上げてバイト幅を数える
            var si = new System.Globalization.StringInfo(s);
            int total = si.LengthInTextElements;

            int posBytes = 0;
            int endBytes = startBytes + byteCount;
            var sb = new StringBuilder();

            for (int i = 0; i < total; i++)
            {
                string elem = si.SubstringByTextElements(i, 1);
                int w = enc.GetByteCount(elem);

                int next = posBytes + w;
                // 部分的に重なる場合は丸ごと含める（文字破壊を避ける）
                if (next > startBytes && posBytes < endBytes)
                {
                    sb.Append(elem);
                }
                posBytes = next;
                if (posBytes >= endBytes) break;
            }
            return sb.ToString();
        }

    }
}
