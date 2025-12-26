using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class ShelfChangeForm : Form
    {
        public ShelfChangeForm()
        {
            InitializeComponent();
        }

        private void ShelfChangeForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// センターコード（仮倉庫コード）一覧の表示
        /// </summary>
        /// <param name="lstTemporaryWareHouse"></param>
        public void New(List<string> lstTemporaryWareHouse)
        {
            try
            {
                ChkdLstShelfChange.Items.Clear();
                foreach (string item in lstTemporaryWareHouse)
                {
                    string[] sArray = item.Split(',');
                    ChkdLstShelfChange.Items.Add(item.Replace(",","："));
                    if (PubConstClass.lstTemporaryWareHouse.Contains(sArray[0]))
                    {
                        ChkdLstShelfChange.SetItemChecked(ChkdLstShelfChange.Items.Count - 1, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー【ShelfChangeForm.New】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「確定」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            string[] sArray;

            try
            {
                PubConstClass.lstTemporaryWareHouse.Clear();
                // チェックされた項目を抽出する
                foreach (var item in ChkdLstShelfChange.CheckedItems)
                {
                    sArray = item.ToString().Split('：');
                    PubConstClass.lstTemporaryWareHouse.Add(sArray[0]);
                }
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー【ShelfChangeForm.New】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
