using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    public partial class EntryAdjustForm : Form
    {
        public EntryAdjustForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ウィンドウアクティブ時に「バーコード読取」にセットフォーカスする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void EntryAdjustForm_Activated(object sender, EventArgs e)
        {
            TxtBcrRead.Focus();
        }

        /// <summary>
        /// BCR手直し商品登録画面初期ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void EntryAdjustForm_Load(Object sender, EventArgs e)
        {
            try
            {
                LblTitle.Text = "QR手直し商品登録";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                // 本画面の右下端が表示画面の右下端になるように表示
                //this.Location = new Point(1920 - this.Width, 1024 - this.Height);

                TxtBcrRead.Text = "";
                TxtBcrInput.Text = "";
                TxtReadBcr.Text = "";
                LstBoxWorkCode.Items.Clear();

                LblForPrint.Visible = false;

                // バーコード読取りにフォーカスをセットする
                TxtBcrRead.Focus();

                #region 結束束情報のサンプルデータ表示
                LstReadBcrData.Clear();
                // 生産実績表示ListViewのカラムヘッダー設定
                LstReadBcrData.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                ColumnHeader col4 = new ColumnHeader();
                ColumnHeader col5 = new ColumnHeader();
                ColumnHeader col6 = new ColumnHeader();

                col1.Text = "ｾｯﾄ順序番号";
                col2.Text = "組合員ｺｰﾄﾞ";
                col3.Text = "組合員氏名";
                col4.Text = "デポ名（CD）";
                col5.Text = "配布";
                col6.Text = "判定";

                col1.Width = 100;        // 連番
                col2.Width = 130;        // 組合員ｺｰﾄﾞ
                col3.Width = 190;        // 組合員氏名
                col4.Width = 190;        // デポ名（CD）
                col5.Width = 100;        // 配布
                col6.Width = 100;        // 判定

                col1.TextAlign = HorizontalAlignment.Center;
                col2.TextAlign = HorizontalAlignment.Center;
                col3.TextAlign = HorizontalAlignment.Center;
                col4.TextAlign = HorizontalAlignment.Center;
                col5.TextAlign = HorizontalAlignment.Center;
                col6.TextAlign = HorizontalAlignment.Center;

                ColumnHeader[] colHeader = new[] { col1, col2, col3, col4, col5, col6 };
                LstReadBcrData.Columns.AddRange(colHeader);

                //ListViewItem itm;
                //string[] col = new string[9];
                //col[0] = "000066";                     // セット順序番号
                //col[1] = "12345672";                   // 組合員ｺｰﾄﾞ
                //col[2] = "組合員００００２";           // 組合員氏名
                //col[3] = "福岡なか（18）";             // デポ名（CD）
                //col[4] = "C21";                        // 配布
                //col[5] = "OK";                         // 判定
                //// データの表示
                //itm = new ListViewItem(col);
                //LstReadBcrData.Items.Add(itm);

                //if (col[5]=="OK")
                //{
                //    LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].BackColor = Color.Green;
                //    LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                //}
                //else
                //{
                //    LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].BackColor = Color.Red;
                //    LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                //}

                //LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].UseItemStyleForSubItems = false;
                //LstReadBcrData.Select();
                //LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].EnsureVisible();
                #endregion

                //TxtBcrRead.Text = "12345672";
                //TxtReadBcr.Text = "12345672";

                test();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【EntryAdjustForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void test()
        {
            try
            {
                LstFormList.Clear();
                // 生産実績表示ListViewのカラムヘッダー設定
                LstFormList.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                ColumnHeader col4 = new ColumnHeader();
                ColumnHeader col5 = new ColumnHeader();
                ColumnHeader col6 = new ColumnHeader();
                ColumnHeader col7 = new ColumnHeader();

                col1.Text = "組合員番号";
                col2.Text = "帳票区分";
                col3.Text = "分子";
                col4.Text = "分母";
                col5.Text = "ページ連番";
                col6.Text = "判定";
                col7.Text = "備考";

                col1.Width = 120;        // 組合員番号
                col2.Width = 90;         // 帳票区分
                col3.Width = 90;         // 分子
                col4.Width = 90;         // 分母
                col5.Width = 100;        // ページ連番
                col6.Width = 100;        // 判定
                col7.Width = 150;        // 備考

                col1.TextAlign = HorizontalAlignment.Center;
                col2.TextAlign = HorizontalAlignment.Center;
                col3.TextAlign = HorizontalAlignment.Center;
                col4.TextAlign = HorizontalAlignment.Center;
                col5.TextAlign = HorizontalAlignment.Center;
                col6.TextAlign = HorizontalAlignment.Center;
                col7.TextAlign = HorizontalAlignment.Left;

                ColumnHeader[] colHeader2 = new[] { col1, col2, col3, col4, col5, col6, col7 };
                LstFormList.Columns.AddRange(colHeader2);

                ListViewItem itm2;
                string[] col = new string[9];

                string[] sTestData = new string[7];
                sTestData[0] = "12345672,A,1,2,000001,OK,デポチラシ";
                sTestData[1] = "12345672,A,2,2,000002,未処理,デポチラシ";
                sTestData[2] = "12345672,B,1,2,000001,未処理,納品計画書/請求書";
                sTestData[3] = "12345672,B,2,2,000002,未処理,納品計画書/請求書";
                sTestData[4] = "12345672,C,1,1,000001,未処理,定期予約品チラシ";
                sTestData[5] = "12345672,D,1,1,000001,未処理,OCR注文書";
                sTestData[6] = "12345672,E,1,1,000001,未処理,OCR特殊";

                string[] sAray;
                foreach(string s in sTestData)
                {
                    sAray = s.Split(',');
                    col[0] = sAray[0];  // 組合員番号
                    col[1] = sAray[1];  // 帳票区分
                    col[2] = sAray[2];  // 分子
                    col[3] = sAray[3];  // 分母
                    col[4] = sAray[4];  // ページ連番
                    col[5] = sAray[5];  // 判定
                    col[6] = sAray[6];  // 備考
                    // データの表示
                    itm2 = new ListViewItem(col);
                    LstFormList.Items.Add(itm2);
                    if (col[5]=="OK")
                    {
                        LstFormList.Items[LstFormList.Items.Count - 1].SubItems[5].BackColor = Color.Green;
                        LstFormList.Items[LstFormList.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                    }
                    else
                    {
                        LstFormList.Items[LstFormList.Items.Count - 1].SubItems[5].BackColor = Color.Red;
                        LstFormList.Items[LstFormList.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                    }


                    LstFormList.Items[LstFormList.Items.Count - 1].UseItemStyleForSubItems = false;
                    LstFormList.Select();
                    LstFormList.Items[LstFormList.Items.Count - 1].EnsureVisible();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【EntryAdjustForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            TxtReadBcr.Text = "";
            TxtBcrInput.Text = "";
            TxtBcrRead.Text = "";
            LstReadBcrData.Items.Clear();
            LstBoxWorkCode.Items.Clear();
            this.Dispose();
        }

        /// <summary>
        /// バーコード入力処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TxtBcrInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == ControlChars.Cr)
                {
                    if (TxtBcrInput.Text.Length == 8)
                    {
                        // 同一番号の存在チェック
                        for (var intLoopCnt = 0; intLoopCnt <= LstReadBcrData.Items.Count - 1; intLoopCnt++)
                        {
                            if (LstReadBcrData.Items[intLoopCnt].SubItems[3].Text == TxtBcrInput.Text)
                            {                                
                                MessageBox.Show("同一組合員コードが存在します。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        // 作業コードの設定                                                                                     
                        TxtReadBcr.Text = TxtBcrInput.Text;
                        // 読取りまたは入力作業コードのチェック
                        CheckWorkCode(TxtReadBcr.Text);
                        // 運転画面のNG束をOK束にする
                        DrivingFormCheckWorkCode(TxtReadBcr.Text);
                        // バーコード読取りにフォーカスをセットする
                        TxtBcrRead.Focus();
                    }
                    else
                    {                        
                        MessageBox.Show("入力桁数は８桁です。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
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
                MessageBox.Show(ex.StackTrace, "【TxtBcrInput_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 運転画面のNG束をOK束にする処理
        /// </summary>
        /// <param name="sUnionNo"></param>
        private void DrivingFormCheckWorkCode(string sUnionNumber)
        {
            try
            {
                // 運転中かどうかのチェック
                if (PubConstClass.pblIsDriving)
                {
                    if (LstBoxWorkCode.Items.Count > 0)
                    {
                        string[] sTopUnionNumber = LstBoxWorkCode.Items[0].ToString().Split('：');
                        ((DrivingForm)Application.OpenForms["DrivingForm"]).CheckWorkCode(sTopUnionNumber[1]);
                    }
                }
                else
                {
                    CommonModule.OutPutLogFile("【運手中では無い】検索組合員番号：" + sUnionNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【DrivingFormCheckWorkCode】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「バーコード読取り」処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TxtBcrRead_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == ControlChars.Cr)
                {
                    //if (TxtBcrRead.TextLength >= 29)
                    if (TxtBcrRead.TextLength >= 27)
                    {
                        // 同一番号の存在チェック
                        for (var intLoopCnt = 0; intLoopCnt <= LstReadBcrData.Items.Count - 1; intLoopCnt++)
                        {
                            //if (LstReadBcrData.Items[intLoopCnt].SubItems[3].Text == TxtBcrRead.Text.Substring(7, 9))
                            if (LstReadBcrData.Items[intLoopCnt].SubItems[3].Text == TxtBcrRead.Text.Substring(5, 10))
                            {                                
                                MessageBox.Show("同一作業コードが存在します。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                // 読み取ったデータのクリア
                                TxtBcrRead.Text = "";
                                TxtBcrRead.Focus();
                                return;
                            }
                        }

                        // 組合員ｺｰﾄﾞの切り出し                    
                        //TxtReadBcr.Text = TxtBcrRead.Text.Substring(7, 9);
                        TxtReadBcr.Text = TxtBcrRead.Text.Substring(5, 10);
                        // 読取りまたは入力作業コードのチェック
                        CheckWorkCode(TxtReadBcr.Text);
                        // 運転画面のNG束をOK束にする
                        DrivingFormCheckWorkCode(TxtReadBcr.Text);
                        // 読み取ったデータのクリア
                        TxtBcrRead.Text = "";
                        // バーコード読取りにフォーカスをセットする
                        TxtBcrRead.Focus();
                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message, "【TxtBcrRead_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LstReadBcrData_DoubleClick(object sender, EventArgs e)
        {
            string[] strArray;
            string strWorkCode;

            try
            {
                if (PnlFormList.Visible == true)
                {
                    PnlFormList.Visible = false;
                }
                else
                {
                    PnlFormList.Visible = true;
                }

                //// MsgBox("選択作業コード：" & LstReadBcrData.Items(LstReadBcrData.SelectedItems(0).Index).SubItems(3).Text)

                //strWorkCode = LstReadBcrData.Items[LstReadBcrData.SelectedItems[0].Index].SubItems[1].Text;
                //for (var intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                //{
                //    strArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');
                //    // 該当の作業ｺｰﾄﾞのチェック
                //    if (strArray[13] == strWorkCode)
                //    {
                //        CommonModule.OutPutLogFile("■検索結果：" + PubConstClass.pblTransactionData[intLoopCnt]);
                //        // DispWorkCodeList(intLoopCnt)
                //        // 指定された連番のデータを含む結束束区分けの内容を表示する
                //        DispOneUnityContent(intLoopCnt, strWorkCode);
                //        break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【LstReadBcrData_DoubleClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 読取りまたは入力組合員コードのチェック
        /// </summary>
        /// <param name="strWorkCode"></param>
        /// <remarks></remarks>
        private void CheckWorkCode(string strWorkCode)
        {
            ListViewItem itm;
            string[] strArray;
            string[] strWorkArray;
            string[] strTranArray;
            string strReadData;
            string[] col = new string[9];
            string strPutData;
            int intPutIndex;
            bool blnOKFlag;
            int intLoopCnt;

            try
            {
                // 初期値の設定
                strTranArray = PubConstClass.pblTransactionData[0].Split(',');
                intPutIndex = 0;
                blnOKFlag = false;
                for (intLoopCnt = 0; intLoopCnt <= PubConstClass.intTransactionIndex - 1; intLoopCnt++)
                {
                    strTranArray = PubConstClass.pblTransactionData[intLoopCnt].Split(',');
                    // 該当の作業ｺｰﾄﾞのチェック
                    if (strTranArray[17].PadLeft(8).Replace(" ", "0") == strWorkCode)
                    {
                        CommonModule.OutPutLogFile("■検索結果：" + PubConstClass.pblTransactionData[intLoopCnt]);
                        intPutIndex = intLoopCnt;
                        // DispWorkCodeList(intLoopCnt)
                        blnOKFlag = true;                       
                        break;
                    }
                }

                if (blnOKFlag == false)
                {
                    // 該当データが見つからない場合は抜ける                    
                    MessageBox.Show("該当する組合員コードが見つかりません", "情報", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // 「単協」「センター」「コース」情報の取得
                //strReadData = CommonModule.GetTankyoCenterCourseInfomation(strWorkCode);
                //strArray = strReadData.Split(',');
                string sDepoName;
                // デポ名（前回値を採用）の取得
                if (PubConstClass.dicDepoCodeData.ContainsKey(strTranArray[4]))
                {
                    sDepoName = PubConstClass.dicDepoCodeData[strTranArray[4]] + "(" + strTranArray[4] + ")";
                }
                else
                {
                    sDepoName = "該当なし" + "(" + strTranArray[4] + ")";
                }

                // 読み取ったデータの表示
                //col[0] = (intLoopCnt + 1).ToString("000000");   // 連番
                col[0] = strTranArray[43];                      // セット順序番号
                col[1] = strWorkCode;                           // 組合員ｺｰﾄﾞ
                col[2] = strTranArray[18].Trim();               // 組合員氏名
                col[3] = sDepoName;                             // デポ名（デポCD）
                col[4] = strTranArray[7] +                      // 配布曜日 
                         strTranArray[8] + 
                         strTranArray[9];
                col[5] = strTranArray[43];                      // セット順序番号

                // ｺｰｽ箱数
                 
                if (strTranArray.Length > 47)
                {
                    strWorkArray = PubConstClass.pblOrderArray[Convert.ToInt32(strTranArray[53])].Split(',');

                    // 判定（区分け種類の場合もある）
                    col[5] = strTranArray[50];
                    if (blnOKFlag == true)
                        strTranArray[50] = "RE";

                    string[] strCmpArray;

                    if (PubConstClass.pblTransactionData[intPutIndex] != null)
                    {
                        strCmpArray = PubConstClass.pblTransactionData[intPutIndex].Split(',');
                        col[8] = strCmpArray[52];
                        strWorkArray = PubConstClass.pblTransactionDataForPrint[intPutIndex].Split(',');
                        LblForPrint.Text = strWorkArray[2] + "　" + strWorkArray[1];
                        if (strWorkArray[2].Trim() == "")
                        {
                            // 印字内容Ａが空白の場合は非表示
                            LblForPrint.Visible = false;
                        }
                        else
                        {
                            LblForPrint.Visible = true;
                        }
                    }

                    strPutData = "";
                    for (intLoopCnt = 0; intLoopCnt <= strTranArray.Length - 1; intLoopCnt++)
                    {
                        if (intLoopCnt == strTranArray.Length - 1)
                            strPutData += strTranArray[intLoopCnt];
                        else
                            strPutData += strTranArray[intLoopCnt] + ",";
                    }
                    PubConstClass.pblTransactionData[intPutIndex] = strPutData;
                    CommonModule.OutPutLogFile("■再処理後：" + PubConstClass.pblTransactionData[intPutIndex]);
                }

                // 指定された連番のデータを含む結束束区分けの内容を表示する
                DispOneUnityContent(intPutIndex, strWorkCode);

                // データの表示
                itm = new ListViewItem(col);
                LstReadBcrData.Items.Add(itm);

                // ① ＯＫ　：緑色（OK）
                // ② ＮＧ　：赤色（NG）
                // ③ 区分け：緑色（結束区分け）
                // ④ 再処理：緑色（RE）
                // ⑤ 未処理：白色（未）
                switch (LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].Text)
                {
                    case "OK":
                        {
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].BackColor = Color.Green;
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                            break;
                        }

                    case "NG":
                        {
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].BackColor = Color.Red;
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                            break;
                        }

                    case "結束区分け":
                        {
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].BackColor = Color.Green;
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                            break;
                        }

                    case "RE":
                        {
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].BackColor = Color.Green;
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].ForeColor = Color.White;
                            break;
                        }

                    default:
                        {
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].BackColor = Color.LightYellow;
                            LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].SubItems[5].ForeColor = Color.Black;
                            break;
                        }
                }
                LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].UseItemStyleForSubItems = false;
                LstReadBcrData.Select();
                LstReadBcrData.Items[LstReadBcrData.Items.Count - 1].EnsureVisible();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【CheckWorkCode】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 指定された連番のデータを含む結束束区分けの内容を表示する
        /// </summary>
        /// <param name="intPutIndex">指定連番</param>
        /// <param name="strWorkCode">組合員ｺｰﾄﾞ</param>
        /// <remarks></remarks>
        private void DispOneUnityContent(int intPutIndex, string strWorkCode)
        {
            string[] strArray;
            int iTabaIndex=0;
            int iFirstIndex=0;
            int iLastIndex=0;

            try
            {
                // 該当組合員の結束束区分け（区分けＡ）をチェックする
                strArray = PubConstClass.pblTransactionData[intPutIndex].Split(',');
                if (strArray[52] != "")
                {
                    // 結束区分けが立っているという事は最後のデータ
                    iLastIndex = intPutIndex;
                    // 結束束の先頭を求める（結束束の最大上限を50としている）
                    for (var N = 1; N <= 50; N++)
                    {
                        if ((intPutIndex - N) < 0)
                        {
                            // 結束束の先頭のインデックス
                            iFirstIndex = 0;
                            break;
                        }
                        else
                        {
                            strArray = PubConstClass.pblTransactionData[intPutIndex - N].Split(',');
                            if (strArray[52] != "")
                            {
                                // 結束束の先頭のインデックス
                                iFirstIndex = intPutIndex - N + 1;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // 結束束の先頭を求める（結束束の最大上限を50としている）
                    for (var N = 1; N <= 50; N++)
                    {
                        if ((intPutIndex - N) < 0)
                        {
                            // 結束束の先頭のインデックス
                            iFirstIndex = 0;
                            break;
                        }
                        else
                        {
                            strArray = PubConstClass.pblTransactionData[intPutIndex - N].Split(',');
                            if (strArray[52] != "")
                            {
                                // 結束束の先頭のインデックス
                                iFirstIndex = intPutIndex - N + 1;
                                break;
                            }
                        }
                    }
                    // 結束束の最後を求める（結束束の最大上限を50としている）
                    for (var N = 1; N <= 50; N++)
                    {
                        if ((intPutIndex + N) > PubConstClass.intTransactionIndex - 1)
                        {
                            // 結束束の最後のインデックス
                            iLastIndex = PubConstClass.intTransactionIndex;
                            break;
                        }
                        strArray = PubConstClass.pblTransactionData[intPutIndex + N].Split(',');
                        if (strArray[52] != "")
                        {
                            iLastIndex = intPutIndex + N;
                            break;
                        }
                    }
                }

                // 結束束の最後から先頭までを表示する
                LstBoxWorkCode.Items.Clear();
                int iWorkIndex = 0;
                for (var N = iLastIndex; N >= iFirstIndex; N += -1)
                {
                    strArray = PubConstClass.pblTransactionData[N].Split(',');
                    if (strArray[13] == strWorkCode)
                        iTabaIndex = iWorkIndex;
                    LstBoxWorkCode.Items.Add((iLastIndex - iFirstIndex - iWorkIndex + 1).ToString("00") + "：" +
                                              strArray[43] + "：" +
                                              strArray[17] + "：" + 
                                              strArray[18].Trim() + 
                                              "（" + strArray[50] + "）");
                    iWorkIndex += 1;
                }
                LstBoxWorkCode.SelectedIndex = iTabaIndex;
                if (iTabaIndex==0)
                {
                    //　インクジェット印字有り【99/99】を表示する
                    LblForPrint.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【DispOneUnityContent】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// バージョン表示ラベルのダブルクリック処理（デバック用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void LblVersion_DoubleClick(object sender, EventArgs e)
        {
            if (PnlFormList.Visible == true)
            {
                PnlFormList.Visible = false;
            }
            else
            {
                PnlFormList.Visible = true;
            }
        }

        private void BtnOkForFormList_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未処理の帳票があります", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnCancelForFormList_Click(object sender, EventArgs e)
        {
            PnlFormList.Visible = false;
        }

        private void LstFormList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int iIndex = LstFormList.SelectedItems[0].Index;
                string sData = LstFormList.Items[iIndex].SubItems[5].Text;
                if (sData != "OK")
                {
                    LstFormList.Items[iIndex].SubItems[5].Text = "OK";
                    LstFormList.Items[iIndex].SubItems[5].BackColor = Color.Green;
                    LstFormList.Items[iIndex].SubItems[5].ForeColor = Color.White;
                }
                else
                {
                    LstFormList.Items[iIndex].SubItems[5].Text = "未処理";
                    LstFormList.Items[iIndex].SubItems[5].BackColor = Color.Red;
                    LstFormList.Items[iIndex].SubItems[5].ForeColor = Color.White;
                }
                LstFormList.EnsureVisible(iIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【LstFormList_DoubleClick】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
