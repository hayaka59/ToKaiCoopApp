using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class BasketCarCheckForm : Form
    {
        public BasketCarCheckForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 「閉じる」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasketCarCheckForm_Load(object sender, EventArgs e)
        {
            try
            {
                LblTitle.Text = "カゴ車セットチェック";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                // カゴ車数一覧表示ListViewのカラムヘッダー設定
                LstBasketCarCheck.Clear();                
                #region 列の新規作成
                LstBasketCarCheck.View = View.Details;
                ColumnHeader col1  = new ColumnHeader();
                ColumnHeader col2  = new ColumnHeader();
                ColumnHeader col3  = new ColumnHeader();
                ColumnHeader col4  = new ColumnHeader();
                ColumnHeader col5  = new ColumnHeader();
                ColumnHeader col6  = new ColumnHeader();
                ColumnHeader col7  = new ColumnHeader();
                ColumnHeader col8  = new ColumnHeader();
                ColumnHeader col9  = new ColumnHeader();
                ColumnHeader col10 = new ColumnHeader();
                ColumnHeader col11 = new ColumnHeader();
                #endregion
                #region 列名称設定
                col1.Text  = "デポコード";
                col2.Text  = "デポ名";
                col3.Text  = "セット数";
                col4.Text  = "内予備セット";
                col5.Text  = "カゴ車台数";
                col6.Text  = "コンテナ数";
                col7.Text  = "ｾｯﾄ数/ｺﾝﾃﾅ";
                col8.Text  = "スタート";
                col9.Text  = "エンド";
                col10.Text = "カゴ車連番";
                col11.Text = "確認状況";
                #endregion
                #region 列幅指定
                col1.Width  = 110;   // デポコード
                col2.Width  = 200;   // デポ名
                col3.Width  = 110;   // セット数
                col4.Width  = 110;   // 内予備セット
                col5.Width  = 110;   // カゴ車台数
                col6.Width  = 110;   // コンテナ数
                col7.Width  = 110;   // ｾｯﾄ数/ｺﾝﾃﾅ
                col8.Width  = 110;   // スタート
                col9.Width  = 110;   // エンド
                col10.Width = 110;   // カゴ車連番
                col11.Width = 110;   // 確認状況
                #endregion
                #region 列揃え指定
                col1.TextAlign  = HorizontalAlignment.Center;    // デポコード
                col2.TextAlign  = HorizontalAlignment.Left;      // デポ名
                col3.TextAlign  = HorizontalAlignment.Right;     // セット数
                col4.TextAlign  = HorizontalAlignment.Right;     // 内予備セット
                col5.TextAlign  = HorizontalAlignment.Right;     // カゴ車台数
                col6.TextAlign  = HorizontalAlignment.Right;     // コンテナ数
                col7.TextAlign  = HorizontalAlignment.Right;     // ｾｯﾄ数/ｺﾝﾃﾅ
                col8.TextAlign  = HorizontalAlignment.Center;    // スタート
                col9.TextAlign  = HorizontalAlignment.Center;    // エンド
                col10.TextAlign = HorizontalAlignment.Center;    // カゴ車連番
                col11.TextAlign = HorizontalAlignment.Center;    // 確認状況
                #endregion
                #region 列表示
                ColumnHeader[] colHeader = new[] { col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11 };
                LstBasketCarCheck.Columns.AddRange(colHeader);
                #endregion

                #region カゴ車数一覧（デポ毎）テストデータの定義
                string[] sTestData1 = new string[62];

                sTestData1[0]  = "06,小倉北    ,ZZZZ9,ZZZ9,3,ZZZ9,Z9,999999,999999,1,0";
                sTestData1[1]  = "06,小倉北    ,ZZZZ9,ZZZ9,3,ZZZ9,Z9,999999,999999,2,0";
                sTestData1[2]  = "06,小倉北    ,ZZZZ9,ZZZ9,3,ZZZ9,Z9,999999,999999,3,0";
                sTestData1[3]  = "07,小倉南    ,ZZZZ9,ZZZ9,4,ZZZ9,Z9,999999,999999,1,0";
                sTestData1[4]  = "07,小倉南    ,ZZZZ9,ZZZ9,4,ZZZ9,Z9,999999,999999,2,0";
                sTestData1[5]  = "07,小倉南    ,ZZZZ9,ZZZ9,4,ZZZ9,Z9,999999,999999,3,0";
                sTestData1[6]  = "07,小倉南    ,ZZZZ9,ZZZ9,4,ZZZ9,Z9,999999,999999,4,0";
                sTestData1[7]  = "08,八幡西    ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,1,0";
                sTestData1[8]  = "08,八幡西    ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,2,0";
                sTestData1[9]  = "08,八幡西    ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,3,0";
                sTestData1[10] = "08,八幡西    ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,4,0";
                sTestData1[11] = "08,八幡西    ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,5,0";
                sTestData1[12] = "09,折尾若松  ,ZZZZ9,ZZZ9,6,ZZZ9,Z9,999999,999999,1,0";
                sTestData1[13] = "09,折尾若松  ,ZZZZ9,ZZZ9,6,ZZZ9,Z9,999999,999999,2,0";
                sTestData1[14] = "09,折尾若松  ,ZZZZ9,ZZZ9,6,ZZZ9,Z9,999999,999999,3,0";
                sTestData1[15] = "09,折尾若松  ,ZZZZ9,ZZZ9,6,ZZZ9,Z9,999999,999999,4,0";
                sTestData1[16] = "09,折尾若松  ,ZZZZ9,ZZZ9,6,ZZZ9,Z9,999999,999999,5,0";
                sTestData1[17] = "09,折尾若松  ,ZZZZ9,ZZZ9,6,ZZZ9,Z9,999999,999999,6,0";
                sTestData1[18] = "10,中遠      ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,1,0";
                sTestData1[19] = "10,中遠      ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,2,0";
                sTestData1[20] = "10,中遠      ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,3,0";
                sTestData1[21] = "10,中遠      ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,4,0";
                sTestData1[22] = "10,中遠      ,ZZZZ9,ZZZ9,5,ZZZ9,Z9,999999,999999,5,0";
                //sTestData1[0]  = "06,小倉北    ,12345,123,123,123,12,111111,999999,1,1";
                //sTestData1[1]  = "07,小倉南    ,12345,123,123,123,12,111111,999999,1,1";
                //sTestData1[2]  = "08,八幡西    ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[3]  = "09,折尾若松  ,12345,123,123,123,12,111111,999999,1,1";
                //sTestData1[4]  = "10,中遠      ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[5]  = "11,筑豊東    ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[6]  = "16,福岡西    ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[7]  = "18,福岡なか  ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[8]  = "22,福岡みなみ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[9]  = "23,宗像      ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[10] = "24,筑豊西    ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[11] = "25,福岡東    ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[12] = "27,筑紫      ,12345,123,123,123,12,111111,999999,1,0";
                //sTestData1[13] = "30,久留米,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[14] = "31,筑後,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[15] = "32,大牟田,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[16] = "59,京築,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[17] = "33,さが,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[18] = "63,からつ,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[19] = "67,たけお,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[20] = "73,伊丹,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[21] = "74,堺,12345,123,123,123,12,111111,999999,備考１";
                //sTestData1[22] = "78,高槻,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[23] = "19,近江八幡,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[24] = "34,長崎東,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[25] = "35,長崎西,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[26] = "37,させぼ,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[27] = "21,福島,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[28] = "38,くまもと玉名,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[29] = "39,くまもと鹿本,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[30] = "40,くまもと東部,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[31] = "41,くまもとセンター留め,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[32] = "42,くまもと西部,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[33] = "43,くまもと北部,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[34] = "44,くまもと県南,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[35] = "45,くまもと松橋,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[36] = "66,くまもと天草,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[37] = "64,宮崎,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[38] = "65,都城,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[39] = "14,日田,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[40] = "47,大分県北,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[41] = "48,別府,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[42] = "49,大分西,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[43] = "56,大分県南,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[44] = "57,大分東,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[45] = "50,姶良,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[46] = "51,鹿児島南,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[47] = "52,北薩,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[48] = "61,鹿児島北,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[49] = "02,山口東部,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[50] = "03,山口中部,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[51] = "04,山口県南,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[52] = "05,山口西部,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[53] = "53,山口周南,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[54] = "70,斐川,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[55] = "71,浜田,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[56] = "69,米子,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[57] = "62,ひろしま中央,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[58] = "72,東広島,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[59] = "01,ひろしま西,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[60] = "77,福山,12345,123,123,123,12,111111,999999,備考１";
                sTestData1[61] = "68,岡山,12345,123,123,123,12,111111,999999,備考１";
                #endregion

                string[] col = new string[51];
                ListViewItem itm;
                string[] sArray;

                foreach(var sData in sTestData1)
                {
                    #region テストデータの読込
                    int iIndex = 0;
                    sArray = sData.Split(',');
                    foreach(string s in sArray)
                    {
                        if(iIndex == 10)
                        {
                            // 確認状況のチェック
                            if (s=="1")
                            {
                                col[iIndex] = "OK";
                            }
                            else
                            {
                                col[iIndex] = "未処理";
                            }
                        }
                        else
                        {
                            // 確認状況以外はそのままの値をセット
                            col[iIndex] = s;
                        }                        
                        iIndex++;
                    }
                    #endregion

                    #region テストデータの表示
                    itm = new ListViewItem(col);
                    LstBasketCarCheck.Items.Add(itm);
                    LstBasketCarCheck.Items[LstBasketCarCheck.Items.Count - 1].UseItemStyleForSubItems = false;

                    if (LstBasketCarCheck.Items.Count % 2 == 0)
                    {
                        // 偶数行の色反転
                        for (var intLoopCnt = 0; intLoopCnt <= 10; intLoopCnt++)
                        {
                            LstBasketCarCheck.Items[LstBasketCarCheck.Items.Count - 1].SubItems[intLoopCnt].BackColor = Color.FromArgb(200, 200, 230);
                            
                        }
                    }
                    #endregion

                    if (LstBasketCarCheck.Items[LstBasketCarCheck.Items.Count - 1].SubItems[10].Text == "OK")
                    {
                        // 確認状況が「OK」の場合は背景色を変更する
                        LstBasketCarCheck.Items[LstBasketCarCheck.Items.Count - 1].SubItems[10].BackColor = Color.Green;
                        LstBasketCarCheck.Items[LstBasketCarCheck.Items.Count - 1].SubItems[10].ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BasketCarCheckForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int iIndexForTest = 0;
        /// <summary>
        /// バージョン表示ラベルのクリック処理（テストの為）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblVersion_Click(object sender, EventArgs e)
        {
            try
            {
                // リスト一覧されたデータの先頭から順番に「OK」とする
                LstBasketCarCheck.Items[iIndexForTest].SubItems[10].Text = "OK";
                LstBasketCarCheck.Items[iIndexForTest].SubItems[10].BackColor = Color.Green;
                LstBasketCarCheck.Items[iIndexForTest].SubItems[10].ForeColor = Color.White;
                LstBasketCarCheck.EnsureVisible(iIndexForTest);                
                iIndexForTest++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【LblVersion_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
