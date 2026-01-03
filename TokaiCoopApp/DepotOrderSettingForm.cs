using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    public partial class DepotOrderSettingForm : Form
    {
        public DepotOrderSettingForm()
        {
            InitializeComponent();
        }

        private List<string> sListDepo = new List<string>();

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepotOrderSettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                // デポ情報表示ListViewのカラムヘッダー設定
                LsvDepo.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                col1.Text = "デポNo";
                col2.Text = "  デポ名";
                col1.TextAlign = HorizontalAlignment.Left;
                col2.TextAlign = HorizontalAlignment.Left;
                col1.Width = 80;        // デポNo
                col2.Width = 200;       // デポ名
                ColumnHeader[] colHeader = new[] { col1, col2 };
                LsvDepo.Columns.AddRange(colHeader);

                sListDepo.Clear();

                string sData;
                // 生協・デポ一覧ファイル格納リスト取得
                foreach (var s in PubConstClass.dicDepoCodeData)
                {
                    ListViewItem itm;
                    string[] sCol = new string[2];
                    sCol[0] = s.Key;
                    sCol[1] = s.Value;
                    // データの表示
                    itm = new ListViewItem(sCol);
                    LsvDepo.Items.Add(itm);
                    LsvDepo.Items[LsvDepo.Items.Count - 1].UseItemStyleForSubItems = false;
                    SetColorForOddRows();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー【DepotOrderSettingForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 奇数行の色をセットする
        /// </summary>
        private void SetColorForOddRows()
        {
            try
            {
                int iRow = 1;
                foreach(var s in LsvDepo.Items) 
                {
                    if (iRow % 2 == 1)
                    {
                        for (int iIndex = 0; iIndex < 2; iIndex++)
                        {
                            // 奇数行の色をセットする
                            LsvDepo.Items[iRow - 1].SubItems[iIndex].BackColor = Color.FromArgb(200, 200, 230);
                        }
                    }
                    else
                    {
                        for (int iIndex = 0; iIndex < 2; iIndex++)
                        {
                            // 偶数行の色を解除する
                            LsvDepo.Items[iRow - 1].SubItems[iIndex].BackColor = Color.White;
                        }
                    }
                    iRow++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー【SetColorForOddRows】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「確定」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 「上へ」キーボタン処理（デポコード）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpDepo_Click(object sender, EventArgs e)
        {
            try
            {
                if (LsvDepo.SelectedItems.Count < 1)
                {
                    MessageBox.Show("移動する項目を選択してください", "警告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                int iSelIndex = LsvDepo.SelectedItems[0].Index;
                if (iSelIndex == 0)
                {
                    MessageBox.Show("先頭行の項目は移動できません", "警告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                ListViewItem itm;
                string[] sCol = new string[2];
                sCol[0] = LsvDepo.Items[iSelIndex].SubItems[0].Text;
                sCol[1] = LsvDepo.Items[iSelIndex].SubItems[1].Text;
                // データの表示
                itm = new ListViewItem(sCol);
                LsvDepo.Items.Insert(iSelIndex - 1, itm);
                LsvDepo.Items.RemoveAt(iSelIndex + 1);

                LsvDepo.Items[iSelIndex - 1].Selected = true;
                LsvDepo.Select();
                SetColorForOddRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【BtnUpDepo_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「下へ」キーボタン処理（デポコード）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownDepo_Click(object sender, EventArgs e)
        {
            try
            {
                if (LsvDepo.SelectedItems.Count < 1)
                {
                    MessageBox.Show("移動する項目を選択してください", "警告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                int iSelIndex = LsvDepo.SelectedItems[0].Index;
                if (iSelIndex == LsvDepo.Items.Count - 1)
                {
                    MessageBox.Show("最終行の項目は移動できません", "警告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                
                ListViewItem itm;
                string[] sCol = new string[2];
                sCol[0] = LsvDepo.Items[iSelIndex].SubItems[0].Text;
                sCol[1] = LsvDepo.Items[iSelIndex].SubItems[1].Text;
                // データの表示
                itm = new ListViewItem(sCol);

                LsvDepo.Items.RemoveAt(iSelIndex);
                LsvDepo.Items.Insert(iSelIndex + 1, itm);
                
                LsvDepo.Items[iSelIndex + 1].Selected = true;
                LsvDepo.Select();
                SetColorForOddRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【BtnDownDepo_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
