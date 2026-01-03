using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    public partial class QrReadForm : Form
    {
        public bool bOKFlag = false;
        public string sQrReadData;

        /// <summary>
        /// 
        /// </summary>
        public QrReadForm()
        {
            InitializeComponent();

            TxtQrRead.Text = "";
            LblQrRead.Text = "設定用ＱＲコードを読み取って下さい。";
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QrReadForm_Load(object sender, EventArgs e)
        {
            try
            {
                // ログインファイルから処理生協名の設定
                string[] sArray;
                CmbCoopName.Items.Clear();
                foreach (string sData in PubConstClass.lstLoginData)
                {
                    sArray = sData.Split(PubConstClass.DEF_MASTER_DELIMITER);
                    if (sArray.Length > 2)
                    {
                        // 「生協コード」＋「：」＋「生協名」
                        CmbCoopName.Items.Add(sArray[0] + "：" + sArray[1]);
                    }
                }
                CmbCoopName.SelectedIndex = 0;

                // データ種別の設定
                CmbDataType.Items.Clear();
                CmbDataType.Items.Add("データＡ");
                CmbDataType.Items.Add("データＢ");
                CmbDataType.Items.Add("データＣ");
                CmbDataType.SelectedIndex = 0;

                TxtQrRead.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【QrReadForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 「確認」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            bOKFlag = true;
            this.Hide();
        }

        /// <summary>
        /// 「キャンセル」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            bOKFlag = false;
            this.Hide();
        }

        /// <summary>
        /// キー入力処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QrReadForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            string sData = "";
            string[] sArray;

            try
            {
                if (e.KeyChar == ControlChars.Cr)
                {
                    sQrReadData = TxtQrRead.Text;
                    sArray = sQrReadData.Split(',');

                    CmbCoopName.SelectedIndex = int.Parse(sArray[0]);
                    CmbDataType.SelectedIndex = int.Parse(sArray[1]);
                    
                    LblMaxBundle.Text = sArray[3];
                    LblFoldingCon.Text = sArray[4];

                    sData += "（１）処理生協　　　　　　　：" + CmbCoopName.Text + Environment.NewLine;
                    sData += "" + Environment.NewLine;

                    sData += "（２）データ種別　　　　　　：" + CmbDataType.Text + Environment.NewLine;
                    sData += "" + Environment.NewLine;

                    sData += "（３）丁合指示データ　　　　：" + sArray[2].Replace("]",@"\").Replace("+",":").Replace("=", "_") + Environment.NewLine;
                    sData += "" + Environment.NewLine;
                    
                    //sData += "（４）棚替えフラグ　　　　　　　　　：0028：城西／0038：町田" + Environment.NewLine;
                    //sData += "" + Environment.NewLine;
                    
                    sData += "（４）結束最大束数　　　　　：" + LblMaxBundle.Text + Environment.NewLine;
                    sData += "" + Environment.NewLine;
                    
                    sData += "（５）カゴ車への積載数　　　：" + LblFoldingCon.Text + Environment.NewLine;

                    LblQrRead.Text = sData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "エラー【QrReadForm_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
