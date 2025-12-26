using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private bool blnCancelFlag;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void SearchForm_Load(Object sender, EventArgs e)
        {
            string strYYYYMMDD;
            string[] strArray;

            try
            {
                LblTitle.Text = "生産データ 検索画面";
                LblVersion.Text = PubConstClass.DEF_VERSION;
                LblSearchResult.Text = "";
                // 左上端に表示
                this.Location = new Point(0, 0);

                LblWaiting.Visible = false;
                ProgressBar1.Visible = false;
                BtnCancel.Visible = false;

                TxtSearchData.Text = "";
                CmbCodeType.Items.Clear();
                CmbCodeType.Items.Add("すべて");
                CmbCodeType.Items.Add("組合員コード");
                CmbCodeType.SelectedIndex = 0;

                CmbJudge.Items.Clear();
                CmbJudge.Items.Add("すべて");
                CmbJudge.Items.Add("ＯＫ");
                CmbJudge.Items.Add("ＮＧ");
                CmbJudge.Items.Add("区分け");
                CmbJudge.Items.Add("再処理");
                CmbJudge.Items.Add("未処理");
                CmbJudge.SelectedIndex = 0;

                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";
                CmbSeisanFileName.Items.Clear();
                // 最終生産ログファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(
                    CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder), 
                    strYYYYMMDD + "稼働データ格納配列ファイル*.txt", SearchOption.TopDirectoryOnly)
                    )
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                        CmbSeisanFileName.Items.Add(strArray[strArray.Length - 1]);
                    CmbSeisanFileName.SelectedIndex = 0;
                }

                // 運転中かどうかのチェック
                if (PubConstClass.pblIsDriving == true)
                {
                    // 運転中の時は「読込」ボタンを操作不可とする
                    DTPicForm.Enabled = false;
                    CmbSeisanFileName.Enabled = false;
                    BtnRead.Enabled = false;
                    ChkIsDispFeeder.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("【SearchForm_Load】" + ex.Message);
            }
        }

        /// <summary>
        /// 検索結果リストビューのヘッダー表示
        /// </summary>
        /// <remarks></remarks>
        private void DispSeisanDataHeader()
        {
            try
            {
                // 丁合指示データ表示ListViewのカラムヘッダー設定
                LstSeisanData.View = View.Details;
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
                ColumnHeader col40 = new ColumnHeader();
                ColumnHeader col41 = new ColumnHeader();
                ColumnHeader col42 = new ColumnHeader();
                ColumnHeader col43 = new ColumnHeader();
                ColumnHeader col44 = new ColumnHeader();
                ColumnHeader col45 = new ColumnHeader();
                ColumnHeader col46 = new ColumnHeader();
                ColumnHeader col47 = new ColumnHeader();
                ColumnHeader col48 = new ColumnHeader();
                ColumnHeader col49 = new ColumnHeader();
                ColumnHeader col50 = new ColumnHeader();
                ColumnHeader col51 = new ColumnHeader();
                ColumnHeader col52 = new ColumnHeader();

                col01.Text = "No.";
                col02.Text = "企画回";
                col03.Text = "ﾗｲﾝ番号";
                col04.Text = "ｾﾝﾀｰ作業順位";
                col05.Text = "生協ｺｰﾄﾞ";
                col06.Text = "地区ｺｰﾄﾞ";
                col07.Text = "支所ｺｰﾄﾞ";
                col08.Text = "支部ｺｰﾄﾞ";
                col09.Text = "曜日";
                col10.Text = "ｺｰｽ名";
                col11.Text = "班ｺｰﾄﾞ";
                col12.Text = "班枝ｺｰﾄﾞ";
                col13.Text = "班名";
                col14.Text = "班人数";
                col15.Text = "組合員ｺｰﾄﾞ";
                col16.Text = "組合員氏名";
                col17.Text = "予備2";
                col18.Text = "利用回数";
                col19.Text = "加入年月日";
                col20.Text = "SEQ";
                col21.Text = "FG(ﾏｰｷﾝｸﾞ)";
                col22.Text = "印字内容";
                col23.Text = "支所ｺｰﾄﾞ";
                col24.Text = "事業所名";
                col25.Text = "鞍1";
                col26.Text = "鞍2";
                col27.Text = "鞍3";
                col28.Text = "鞍4";
                col29.Text = "鞍5";
                col30.Text = "鞍6";
                col31.Text = "鞍7";
                col32.Text = "鞍8";
                col33.Text = "鞍9";
                col34.Text = "鞍10";
                col35.Text = "鞍11";
                col36.Text = "鞍12";
                col37.Text = "鞍13";
                col38.Text = "鞍14";
                col39.Text = "鞍15";
                col40.Text = "鞍16";
                col41.Text = "鞍17";
                col42.Text = "鞍18";
                col43.Text = "鞍19";
                col44.Text = "鞍20";
                col45.Text = "鞍21";
                col46.Text = "鞍22";
                col47.Text = "帳票1";
                col48.Text = "帳票2";
                col49.Text = "帳票3";
                col50.Text = "帳票4";
                col51.Text = "帳票5";
                col52.Text = "判定";

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
                col16.TextAlign = HorizontalAlignment.Left;
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
                col40.TextAlign = HorizontalAlignment.Center;
                col41.TextAlign = HorizontalAlignment.Center;
                col42.TextAlign = HorizontalAlignment.Center;
                col43.TextAlign = HorizontalAlignment.Center;
                col44.TextAlign = HorizontalAlignment.Center;
                col45.TextAlign = HorizontalAlignment.Center;
                col46.TextAlign = HorizontalAlignment.Center;
                col47.TextAlign = HorizontalAlignment.Center;
                col48.TextAlign = HorizontalAlignment.Center;
                col49.TextAlign = HorizontalAlignment.Center;
                col50.TextAlign = HorizontalAlignment.Center;
                col51.TextAlign = HorizontalAlignment.Center;
                col52.TextAlign = HorizontalAlignment.Center;

                col01.Width = 70;        // No.
                col02.Width = 70;        // 企画回
                col03.Width = 70;        // ﾗｲﾝ番号
                col04.Width = 110;       // ｾﾝﾀｰ作業順位
                col05.Width = 70;        // 生協ｺｰﾄﾞ
                col06.Width = 70;        // 地区ｺｰﾄﾞ
                col07.Width = 70;        // 支所ｺｰﾄﾞ
                col08.Width = 70;        // 支部ｺｰﾄﾞ
                col09.Width = 50;        // 曜日
                col10.Width = 90;        // ｺｰｽ名
                col11.Width = 90;        // 班ｺｰﾄﾞ
                col12.Width = 70;        // 班枝ｺｰﾄﾞ
                col13.Width = 140;       // 班名
                col14.Width = 70;        // 班人数
                col15.Width = 120;       // 組合員ｺｰﾄﾞ
                col16.Width = 150;       // 組合員氏名
                if (ChkIsDispFeeder.Checked == true)
                {
                    col17.Width = 60;        // 予備2
                    col18.Width = 90;        // 利用回数
                    col19.Width = 100;       // 加入年月日
                    col20.Width = 75;        // SEQ
                    col21.Width = 110;       // FG(ﾏｰｷﾝｸﾞ)
                    col22.Width = 150;       // 印字内容
                    col23.Width = 80;        // 支所ｺｰﾄﾞ
                    col24.Width = 35;        // 鞍1
                    col25.Width = 35;        // 鞍2
                    col26.Width = 35;        // 鞍3
                    col27.Width = 35;        // 鞍4
                    col28.Width = 35;        // 鞍5
                    col29.Width = 35;        // 鞍6
                    col30.Width = 35;        // 鞍7
                    col31.Width = 35;        // 鞍8
                    col32.Width = 35;        // 鞍9
                    col33.Width = 45;        // 鞍10
                    col34.Width = 45;        // 鞍11
                    col35.Width = 45;        // 鞍12
                    col36.Width = 45;        // 鞍13
                    col37.Width = 45;        // 鞍14
                    col38.Width = 45;        // 鞍15
                    col39.Width = 45;        // 鞍16
                    col40.Width = 45;        // 鞍17
                    col41.Width = 45;        // 鞍18
                    col42.Width = 45;        // 鞍19
                    col43.Width = 45;        // 鞍20
                    col44.Width = 45;        // 鞍21
                    col45.Width = 45;        // 鞍22
                    col46.Width = 55;        // 帳票1
                    col47.Width = 55;        // 帳票2
                    col48.Width = 55;        // 帳票3
                    col49.Width = 55;        // 帳票4
                    col50.Width = 55;        // 帳票5
                }
                else
                {
                    // 非表示とする
                    col04.Width = 0;         // ｾﾝﾀｰ作業順位

                    col06.Width = 0;         // 地区ｺｰﾄﾞ
                    col07.Width = 0;         // 支所ｺｰﾄﾞ
                    col08.Width = 0;         // 支部ｺｰﾄﾞ

                    col11.Width = 0;         // 班ｺｰﾄﾞ
                    col12.Width = 0;         // 班枝ｺｰﾄﾞ
                    col13.Width = 0;         // 班名

                    col17.Width = 0;         // 予備2
                    col18.Width = 0;         // 利用回数
                    col19.Width = 0;         // 加入年月日
                    col20.Width = 0;         // SEQ
                    col21.Width = 0;         // FG(ﾏｰｷﾝｸﾞ)
                    col22.Width = 0;         // 印字内容
                    col23.Width = 0;         // 支所ｺｰﾄﾞ
                    col24.Width = 0;         // 事業所名
                    col25.Width = 0;         // 鞍1
                    col26.Width = 0;         // 鞍2
                    col27.Width = 0;         // 鞍3
                    col28.Width = 0;         // 鞍4
                    col29.Width = 0;         // 鞍5
                    col30.Width = 0;         // 鞍6
                    col31.Width = 0;         // 鞍7
                    col32.Width = 0;         // 鞍8
                    col33.Width = 0;         // 鞍9
                    col34.Width = 0;         // 鞍10
                    col35.Width = 0;         // 鞍11
                    col36.Width = 0;         // 鞍12
                    col37.Width = 0;         // 鞍13
                    col38.Width = 0;         // 鞍14
                    col39.Width = 0;         // 鞍15
                    col40.Width = 0;         // 鞍16
                    col41.Width = 0;         // 鞍17
                    col42.Width = 0;         // 鞍18
                    col43.Width = 0;         // 鞍19
                    col44.Width = 0;         // 鞍20
                    col45.Width = 0;         // 鞍21
                    col46.Width = 0;         // 鞍22
                    col47.Width = 0;         // 帳票1
                    col48.Width = 0;         // 帳票2
                    col49.Width = 0;         // 帳票3
                    col50.Width = 0;         // 帳票4
                    col51.Width = 0;         // 帳票5
                }
                col52.Width = 80;            // 判定

                ColumnHeader[] colHeader = new[] { col01, col02, col03, col04, col05, col06, col07, col08, col09, col10, col11, col12, col13, col14, col15, col16, col17, col18, col19, col20, col21, col22, col23, col24, col25, col26, col27, col28, col29, col30, col31, col32, col33, col34, col35, col36, col37, col38, col39, col40, col41, col42, col43, col44, col45, col46, col47, col48, col49, col50, col51, col52 };

                LstSeisanData.Columns.AddRange(colHeader);
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("【DispSeisanDataHeader】" + ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnClose_Click(Object sender, EventArgs e)
        {
            this.Dispose();
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

            try
            {
                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";

                CmbSeisanFileName.Items.Clear();

                // 生産ログ格納フォルダの存在チェック
                if (Directory.Exists(CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD) == false)
                {
                    Interaction.MsgBox("最終生産ログファイルが存在しません。");
                    return;
                }

                // 最終生産ログファイル一覧の取得
                foreach (string strSeisanLogFileFullPathName in Directory.GetFiles(
                    CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder), 
                    strYYYYMMDD + "稼働データ格納配列ファイル*.txt", SearchOption.TopDirectoryOnly)
                    )
                {
                    strArray = strSeisanLogFileFullPathName.Split('\\');
                    if (strArray.Length > 2)
                        CmbSeisanFileName.Items.Add(strArray[strArray.Length - 1]);
                    CmbSeisanFileName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("【DTPicForm_ValueChanged】" + ex.Message);
            }
        }

        private int iSearchResultCount;   // 検査結果数

        /// <summary>
        /// 「検索」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnSearch_Click(Object sender, EventArgs e)
        {
            string strYYYYMMDD;
            string[] strArray;
            int intLoopCnt;

            try
            {
                if (PubConstClass.intTransactionIndex == 0)
                {
                    Interaction.MsgBox("丁合指示データを読み込み、制御データを作成してください。", (MsgBoxStyle)MsgBoxStyle.Exclamation | MsgBoxStyle.OkOnly, "警告");
                    return;
                }

                CmbSeisanFileName.Enabled = false;
                CmbCodeType.Enabled = false;
                CmbJudge.Enabled = false;
                BtnRead.Enabled = false;
                BtnSearch.Enabled = false;
                BtnClose.Enabled = false;
                iSearchResultCount = 0;

                if (CmbCodeType.SelectedIndex == 0 & CmbJudge.SelectedIndex == 0)
                {
                    // すべてのデータ表示
                    DispAllData();
                    return;
                }


                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";

                LstSeisanData.Clear();

                // ListView コントロールの再描画を禁止
                LstSeisanData.BeginUpdate();

                // ListView コントロールのヘッダー表示
                DispSeisanDataHeader();

                // 検索処理中メッセージの非表示
                LblWaiting.Visible = true;
                ProgressBar1.Visible = true;
                BtnCancel.Visible = true;
                blnCancelFlag = false;
                ProgressBar1.Minimum = 0;
                ProgressBar1.Maximum = PubConstClass.intTransactionIndex;
                for (intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                {
                    ProgressBar1.Value = intLoopCnt;
                    // 「キャンセル」ボタンクリックのチェック
                    if (blnCancelFlag == true)
                        break;

                    strArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');
                    // OutPutLogFile("【検索】" & PubConstClass.pblTransactionData(intLoopCnt))
                    if (TxtSearchData.Text != "")
                    {
                        // 検索キー「作業ｺｰﾄﾞ」「組合員番号」が存在する場合
                        switch (CmbCodeType.SelectedIndex)
                        {
                            case 0:
                                {
                                    // すべて
                                    switch (CmbJudge.SelectedIndex)
                                    {
                                        case 0:
                                            {
                                                // すべて
                                                DisplayListViewData(intLoopCnt);
                                                break;
                                            }

                                        case 1:
                                            {
                                                // OK
                                                if (strArray[50] == "OK")
                                                    DisplayListViewData(intLoopCnt);
                                                break;
                                            }

                                        case 2:
                                            {
                                                // NG
                                                if (strArray[50] == "NG")
                                                    DisplayListViewData(intLoopCnt);
                                                break;
                                            }

                                        case 3:
                                            {
                                                // 結束区分け
                                                if (strArray[52] != "")
                                                    DisplayListViewData(intLoopCnt);
                                                break;
                                            }

                                        case 4:
                                            {
                                                // 再処理
                                                if (strArray[50] == "RE")
                                                    DisplayListViewData(intLoopCnt);
                                                break;
                                            }

                                        case 5:
                                            {
                                                // 未処理
                                                if (strArray[50] == "未")
                                                    DisplayListViewData(intLoopCnt);
                                                break;
                                            }
                                    }

                                    break;
                                }

                            case 1:
                                {
                                    // 組合員コードのチェック
                                    if (TxtSearchData.Text == strArray[13])
                                    {
                                        // 判定のチェック
                                        switch (CmbJudge.SelectedIndex)
                                        {
                                            case 0:
                                                {
                                                    // すべて
                                                    DisplayListViewData(intLoopCnt);
                                                    break;
                                                }

                                            case 1:
                                                {
                                                    // OK
                                                    if (strArray[50] == "OK")
                                                    {
                                                        DisplayListViewData(intLoopCnt);
                                                        break;
                                                    }

                                                    break;
                                                }

                                            case 2:
                                                {
                                                    if (strArray[50] == "NG")
                                                    {
                                                        DisplayListViewData(intLoopCnt);
                                                        break;
                                                    }

                                                    break;
                                                }

                                            case 3:
                                                {
                                                    // 結束区分け
                                                    if (strArray[52] != "")
                                                    {
                                                        DisplayListViewData(intLoopCnt);
                                                        break;
                                                    }

                                                    break;
                                                }

                                            case 4:
                                                {
                                                    // 再処理
                                                    if (strArray[50] == "RE")
                                                        DisplayListViewData(intLoopCnt);
                                                    break;
                                                }

                                            case 5:
                                                {
                                                    // 未処理
                                                    if (strArray[50] == "未")
                                                        DisplayListViewData(intLoopCnt);
                                                    break;
                                                }
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                    else
                        // 検索キーが空白の場合
                        switch (CmbJudge.SelectedIndex)
                        {
                            case 0:
                                {
                                    break;
                                }

                            case 1:
                                {
                                    // OK
                                    if (strArray[50] == "OK")
                                        DisplayListViewData(intLoopCnt);
                                    break;
                                }

                            case 2:
                                {
                                    // NG
                                    if (strArray[50] == "NG")
                                        DisplayListViewData(intLoopCnt);
                                    break;
                                }

                            case 3:
                                {
                                    // 結束区分け
                                    if (strArray[52] != "")
                                        DisplayListViewData(intLoopCnt);
                                    break;
                                }

                            case 4:
                                {
                                    // 再処理
                                    if (strArray[50] == "RE")
                                        DisplayListViewData(intLoopCnt);
                                    break;
                                }

                            case 5:
                                {
                                    // 未処理
                                    if (strArray[50] == "未" | strArray[50].IndexOf("区分") > 1)
                                        DisplayListViewData(intLoopCnt);
                                    break;
                                }
                        }
                    Application.DoEvents();
                }
                CommonModule.OutPutLogFile("■検索結果：" + PubConstClass.pblTransactionData[intLoopCnt]);
                if (iSearchResultCount == 0)
                    LblSearchResult.Text = "対象データはありません";
                else
                    LblSearchResult.Text += " 件 見つかりました";
            }


            catch (Exception ex)
            {
                Interaction.MsgBox("【BtnSearch_Click】" + ex.Message);
            }
            finally
            {
                // ListView コントロールの再描画
                LstSeisanData.EndUpdate();

                // 運転中かどうかのチェック
                if (PubConstClass.pblIsDriving == false)
                {
                    CmbSeisanFileName.Enabled = true;
                    BtnRead.Enabled = true;
                }
                CmbCodeType.Enabled = true;
                CmbJudge.Enabled = true;
                BtnSearch.Enabled = true;
                BtnClose.Enabled = true;
                // 検索処理中メッセージの非表示
                LblWaiting.Visible = false;
                ProgressBar1.Visible = false;
                BtnCancel.Visible = false;
            }
        }


        /// <summary>
        /// すべてのデータの表示処理
        /// </summary>
        /// <remarks></remarks>
        private void DispAllData()
        {
            try
            {
                if (PubConstClass.intTransactionIndex == 0)
                {
                    Interaction.MsgBox("丁合指示データを読み込み、制御データを作成してください。", (MsgBoxStyle)MsgBoxStyle.Exclamation | MsgBoxStyle.OkOnly, "警告");
                    return;
                }

                LstSeisanData.Clear();

                // ListView コントロールの再描画を禁止
                LstSeisanData.BeginUpdate();

                // ListView コントロールのヘッダー表示
                DispSeisanDataHeader();

                LblWaiting.Visible = true;
                ProgressBar1.Visible = true;
                BtnCancel.Visible = true;

                blnCancelFlag = false;
                ProgressBar1.Minimum = 0;
                ProgressBar1.Maximum = PubConstClass.intTransactionIndex;
                for (var N = 0; N <= PubConstClass.intTransactionIndex - 1; N++)
                {
                    ProgressBar1.Value = N;
                    Application.DoEvents();
                    // 「キャンセル」ボタンクリックのチェック
                    if (blnCancelFlag == true)
                        break;

                    DisplayListViewData(N);
                }
                if (iSearchResultCount == 0)
                    LblSearchResult.Text = "対象データはありません";
                else
                    LblSearchResult.Text += " 件 見つかりました";
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("【DispAllData】" + ex.Message);
            }
            finally
            {
                // ListView コントロールの再描画
                LstSeisanData.EndUpdate();

                LblWaiting.Visible = false;
                ProgressBar1.Visible = false;
                BtnCancel.Visible = false;
            }
        }

        private void DisplayListViewData(int intDispNumber)
        {
            string[] col = new string[52];
            ListViewItem itm;
            string[] strArray;

            try
            {
                strArray = PubConstClass.pblTransactionData[intDispNumber].Split(',');
                col[0] = (intDispNumber + 1).ToString("000000");
                col[1] = strArray[0];        
                col[2] = strArray[1];        
                col[3] = strArray[2];        
                col[4] = strArray[3];        
                col[5] = strArray[4];        
                col[6] = strArray[5];        
                col[7] = strArray[6];        
                col[8] = strArray[7];        
                col[9] = strArray[8];        
                col[10] = strArray[9];       
                col[11] = strArray[10];      
                col[12] = strArray[11];      
                col[13] = strArray[12];      
                col[14] = strArray[13];      
                col[15] = strArray[14];      
                col[16] = strArray[15];      
                col[17] = strArray[16];      
                col[18] = strArray[17];      
                col[19] = strArray[18];      
                col[20] = strArray[19];      
                col[21] = strArray[20];      
                col[22] = strArray[21];      
                col[23] = strArray[22];      
                col[24] = strArray[23];      
                col[25] = strArray[24];      
                col[26] = strArray[25];      
                col[27] = strArray[26];      
                col[28] = strArray[27];      
                col[29] = strArray[28];      
                col[30] = strArray[29];      
                col[31] = strArray[30];      
                col[32] = strArray[31];      
                col[33] = strArray[32];      
                col[34] = strArray[33];      
                col[35] = strArray[34];      
                col[36] = strArray[35];      
                col[37] = strArray[36];      
                col[38] = strArray[37];      
                col[39] = strArray[38];      
                col[40] = strArray[39];      
                col[41] = strArray[40];      
                col[42] = strArray[41];      
                col[43] = strArray[42];      
                col[44] = strArray[43];      
                col[45] = strArray[44];      
                col[46] = strArray[45];      
                col[47] = strArray[46];      
                col[48] = strArray[47];      
                col[49] = strArray[48];      
                col[50] = strArray[49];      
                // 判定
                if (strArray[50] == "未")
                    col[51] = "未処理";
                else if (strArray[50] == "RE")
                    col[51] = "再処理";
                else if (strArray[50].IndexOf("区分") > 1)
                    col[51] = "未処理";
                else
                    col[51] = strArray[50];

                // データの表示
                itm = new ListViewItem(col);
                LstSeisanData.Items.Add(itm);
                LstSeisanData.Items[LstSeisanData.Items.Count - 1].UseItemStyleForSubItems = false;

                if (LstSeisanData.Items.Count % 2 == 0)
                {
                    // 偶数行の色反転
                    for (var intLoopCnt = 0; intLoopCnt <= 51; intLoopCnt++)
                        LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.FromArgb(200, 200, 230);
                }

                // ① ＯＫ　：緑色（OK）
                // ② ＮＧ　：赤色（NG）
                // ③ 区分け：緑色（結束区分け）
                // ④ 再処理：緑色（RE）
                // ⑤ 未処理：白色（未）
                switch (LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].Text)
                {
                    case "OK":
                        {
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].BackColor = Color.Green;
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].ForeColor = Color.White;
                            break;
                        }

                    case "NG":
                        {
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].BackColor = Color.Red;
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].ForeColor = Color.White;
                            break;
                        }

                    case "結束区分け":
                        {
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].BackColor = Color.Green;
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].ForeColor = Color.White;
                            break;
                        }

                    case "RE":
                        {
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].BackColor = Color.Green;
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].ForeColor = Color.White;
                            break;
                        }

                    default:
                        {
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].BackColor = Color.LightYellow;
                            LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[51].ForeColor = Color.Black;
                            break;
                        }
                }
                LstSeisanData.Items[LstSeisanData.Items.Count - 1].UseItemStyleForSubItems = false;
                LstSeisanData.Select();
                LstSeisanData.Items[LstSeisanData.Items.Count - 1].EnsureVisible();

                iSearchResultCount += 1;
                LblSearchResult.Text = iSearchResultCount.ToString();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("【DisplayListViewData】" + ex.Message);
            }
        }


        /// <summary>
        /// 「読込」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnRead_Click(Object sender, EventArgs e)
        {
            string strSeisanLogFileFullPathName;
            string strYYYYMMDD;
            string strReadData;
            int intReadCount;

            try
            {
                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";

                LstSeisanData.Clear();

                // ListView コントロールの再描画を禁止
                LstSeisanData.BeginUpdate();

                CmbSeisanFileName.Enabled = false;
                CmbCodeType.Enabled = false;
                CmbJudge.Enabled = false;
                BtnRead.Enabled = false;
                BtnSearch.Enabled = false;
                BtnClose.Enabled = false;

                PubConstClass.intTransactionIndex = 0;

                intReadCount = 0;
                strSeisanLogFileFullPathName = CommonModule.IncludeTrailingPathDelimiter(PubConstClass.pblInternalTranFolder) + strYYYYMMDD + @"\" + CmbSeisanFileName.Text;

                using (StreamReader sr = new StreamReader(strSeisanLogFileFullPathName, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        strReadData = sr.ReadLine();
                        if (strReadData.Length < 6)
                            break;

                        PubConstClass.pblTransactionData[PubConstClass.intTransactionIndex] = strReadData;
                        PubConstClass.intTransactionIndex += 1;
                        LblSearchResult.Text = PubConstClass.intTransactionIndex.ToString();

                        Application.DoEvents();
                    }
                }
                CommonModule.OutPutLogFile("最終生産ログの件数：" + intReadCount.ToString());
                LblSearchResult.Text += "件 読込完了";
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("【BtnRead_Click】" + ex.Message);
            }
            finally
            {
                // ListView コントロールの再描画
                LstSeisanData.EndUpdate();

                CmbSeisanFileName.Enabled = true;
                CmbCodeType.Enabled = true;
                CmbJudge.Enabled = true;
                BtnRead.Enabled = true;
                BtnSearch.Enabled = true;
                BtnClose.Enabled = true;
            }
        }

        private void BtnCancel_Click(Object sender, EventArgs e)
        {
            blnCancelFlag = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TxtSearchData_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == ControlChars.Cr)
                {
                    // 「検索」ボタンの呼び出し
                    BtnSearch.PerformClick();
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
                Interaction.MsgBox("【TxtSearchData_KeyPress】" + ex.Message);
            }
        }


        private void DisplayListViewDataForTest(int intDispNumber)
        {
            string[] col = new string[51];
            ListViewItem itm;
            string[] strArray;

            try
            {
                strArray = PubConstClass.pblTransactionData[intDispNumber].Split(',');
                // No,
                col[0] = (intDispNumber + 1).ToString("000000"); // No.
                col[1] = strArray[0];                            // 企画回
                col[2] = strArray[1];                            // ﾗｲﾝ番号
                col[3] = strArray[2];                            // ｾﾝﾀｰ作業順位
                col[4] = strArray[3];                            // 生協ｺｰﾄﾞ
                col[5] = strArray[4];                            // 地区ｺｰﾄﾞ
                col[6] = strArray[5];                            // 支所ｺｰﾄﾞ
                col[7] = strArray[6];                            // 支部ｺｰﾄﾞ
                col[8] = strArray[7];                            // 曜日
                col[9] = strArray[8];                            // ｺｰｽ名
                col[10] = strArray[9];                           // 班ｺｰﾄﾞ
                col[11] = strArray[10];                          // 班枝ｺｰﾄﾞ
                col[12] = strArray[11];                          // 班名
                col[13] = strArray[12];                          // 班人数
                col[14] = strArray[13];                          // 組合員ｺｰﾄﾞ
                col[15] = strArray[14];                          // 組合員氏名
                col[16] = strArray[15];                          // 予備2
                col[17] = strArray[16];                          // 利用回数
                col[18] = strArray[17];                          // 加入年月日
                col[19] = strArray[18];                          // SEQ
                col[20] = strArray[19];                          // FG(ﾏｰｷﾝｸﾞ)
                col[21] = strArray[20];                          // 印字内容
                col[22] = strArray[21];                          // 支所ｺｰﾄﾞ
                col[23] = strArray[22];                          // 事業所名
                col[24] = strArray[23];                          // 鞍1
                col[25] = strArray[24];                          // 鞍2
                col[26] = strArray[25];                          // 鞍3
                col[27] = strArray[26];                          // 鞍4
                col[28] = strArray[27];                          // 鞍5
                col[29] = strArray[28];                          // 鞍6
                col[30] = strArray[29];                          // 鞍7
                col[31] = strArray[30];                          // 鞍8
                col[32] = strArray[31];                          // 鞍9
                col[33] = strArray[32];                          // 鞍10
                col[34] = strArray[33];                          // 鞍11
                col[35] = strArray[34];                          // 鞍12
                col[36] = strArray[35];                          // 鞍13
                col[37] = strArray[36];                          // 鞍14
                col[38] = strArray[37];                          // 鞍15
                col[39] = strArray[38];                          // 鞍16
                col[40] = strArray[39];                          // 鞍17
                col[41] = strArray[40];                          // 鞍18
                col[42] = strArray[41];                          // 鞍19
                col[43] = strArray[42];                          // 鞍20
                col[44] = strArray[43];                          // 鞍21
                col[45] = strArray[44];                          // 鞍22
                col[46] = strArray[45];                          // 帳票1
                col[47] = strArray[46];                          // 帳票2
                col[48] = strArray[47];                          // 帳票3
                col[49] = strArray[48];                          // 帳票4
                col[50] = strArray[49];                          // 帳票5
                col[51] = strArray[50];                          // 判定

                // データの表示
                itm = new ListViewItem(col);
                LstSeisanData.Items.Add(itm);
                LstSeisanData.Items[LstSeisanData.Items.Count - 1].UseItemStyleForSubItems = false;

                if (LstSeisanData.Items.Count % 2 == 0)
                {
                    // 偶数行の色反転
                    for (var intLoopCnt = 0; intLoopCnt <= 49; intLoopCnt++)
                        LstSeisanData.Items[LstSeisanData.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.FromArgb(200, 200, 230);
                }
                LblSearchResult.Text = intDispNumber.ToString();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("【DisplayListViewData】" + ex.Message);
            }
        }

        /// <summary>
        /// 「デバッグボタン」処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(Object sender, EventArgs e)
        {
            string strYYYYMMDD;
            string[] strArray;
            int intLoopCnt;
            bool blnIsOkFlag;

            try
            {
                if (PubConstClass.intTransactionIndex == 0)
                {
                    Interaction.MsgBox("丁合指示データを読み込み、制御データを作成してください。", (MsgBoxStyle)MsgBoxStyle.Exclamation | MsgBoxStyle.OkOnly, "警告");
                    return;
                }

                CmbSeisanFileName.Enabled = false;
                CmbCodeType.Enabled = false;
                CmbJudge.Enabled = false;
                BtnRead.Enabled = false;
                BtnSearch.Enabled = false;
                BtnClose.Enabled = false;

                strYYYYMMDD = DTPicForm.Value.ToString("yyyyMMdd") + @"\";

                LstSeisanData.Clear();

                // ListView コントロールの再描画を禁止
                LstSeisanData.BeginUpdate();

                // ListView コントロールのヘッダー表示
                DispSeisanDataHeader();
                blnIsOkFlag = true;

                for (intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                {
                    strArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');

                    if (strArray[50] == "未" | strArray[50] == "NG")
                        blnIsOkFlag = false;

                    // 結束区分け
                    if (strArray[48] != "")
                    {
                        if (blnIsOkFlag == true)
                            DisplayListViewDataForTest(intLoopCnt);
                        else
                            DisplayListViewDataForTest(intLoopCnt);
                        blnIsOkFlag = true;
                    }
                    Application.DoEvents();
                }
            }
            // OutPutLogFile("■検索結果：" & PubConstClass.pblTransactionData(intLoopCnt))

            catch (Exception ex)
            {
                Interaction.MsgBox("【BtnSearch_Click】" + ex.Message);
            }
            finally
            {
                // ListView コントロールの再描画
                LstSeisanData.EndUpdate();

                CmbSeisanFileName.Enabled = true;
                CmbCodeType.Enabled = true;
                CmbJudge.Enabled = true;
                BtnRead.Enabled = true;
                BtnSearch.Enabled = true;
                BtnClose.Enabled = true;
            }
        }


        /// <summary>
        /// 「手直し品 登録」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnAdjust_Click(Object sender, EventArgs e)
        {
            try
            {
                CommonModule.OutPutLogFile("■「手直し品 登録」ボタン（丁合指示データ読込画面）クリック");

                EntryAdjustForm form = new EntryAdjustForm();
                form.Show(this);
                form.Activate();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("【BtnAdjust_Click】" + ex.Message);
            }
        }
    }
}
