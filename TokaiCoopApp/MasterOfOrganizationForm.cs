using System;
using System.Drawing;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class MasterOfOrganizationForm : Form
    {
        public MasterOfOrganizationForm()
        {
            InitializeComponent();
        }                                                                                   

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MasterOfOrganizationForm_Load(Object sender, EventArgs e)
        {
            string[] col = new string[10];
            string[] sAray;
            ListViewItem itm;

            try
            {
                LblTitle.Text = "単協・デポ一覧";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                // 組織マスタ表示ListViewのカラムヘッダー設定
                LstMaster.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                ColumnHeader col4 = new ColumnHeader();
                ColumnHeader col5 = new ColumnHeader();

                col1.Text = "No.";
                col2.Text = "生協コード";                
                col3.Text = "　生協名称";               
                col4.Text = "デポコード";                
                col5.Text = "　デポ名";

                col1.TextAlign = HorizontalAlignment.Center;
                col2.TextAlign = HorizontalAlignment.Center;
                col3.TextAlign = HorizontalAlignment.Left;
                col4.TextAlign = HorizontalAlignment.Center;
                col5.TextAlign = HorizontalAlignment.Left;

                col1.Width = 50;         // No.
                col2.Width = 100;        // 生協コード
                col3.Width = 230;        // 生協名称                
                col4.Width = 100;        // デポコード
                col5.Width = 230;        // デポ名                               

                ColumnHeader[] colHeader = new[] { col1, col2, col3, col4, col5 };
                LstMaster.Columns.AddRange(colHeader);

                int iCount = 1;
                // 生協・デポ一覧ファイル格納リスト取得
                foreach (string sData in PubConstClass.lstCoopDepoData)
                {
                    sAray = sData.Split(',');
                    col[0] = iCount.ToString("00");
                    iCount++;
                    col[1] = sAray[0];
                    col[2] = "　" + sAray[1];
                    col[3] = sAray[2];
                    col[4] = "　" + sAray[3];

                    // データの表示
                    itm = new ListViewItem(col);
                    LstMaster.Items.Add(itm);
                    LstMaster.Items[LstMaster.Items.Count - 1].UseItemStyleForSubItems = false;
                    if (LstMaster.Items.Count % 2 == 1)
                    {
                        for(int iIndex=0; iIndex < 5; iIndex++)
                        {
                            // 奇数行の色反転
                            LstMaster.Items[LstMaster.Items.Count - 1].SubItems[iIndex].BackColor = Color.FromArgb(200, 200, 230);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【MasterOfOrganizationForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.Dispose();
        }

        /// <summary>
        /// フォームが閉じられる時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterOfOrganizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonModule.OutPutLogFile("「X」ボタンキャンセル");
            // フォームを閉じるのをキャンセル
            e.Cancel = true;
        }
    }
}
