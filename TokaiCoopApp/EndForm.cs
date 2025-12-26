using System;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class EndForm : Form
    {
        public EndForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 「×」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void EndForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 終了メニュー初期ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void EndForm_Load(Object sender, EventArgs e)
        {

            // 「終了実行」ボタンにフォーカスセット
            this.ActiveControl = this.BtnEnd;
        }

        /// <summary>
        /// 「キャンセル」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnCancel_Click(Object sender, EventArgs e)
        {
            PubConstClass.blnShutDownFlag = false;
            PubConstClass.blnReturnFlag = false;
            this.Dispose();
        }

        /// <summary>
        /// 「終了実行」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnEnd_Click(Object sender, EventArgs e)
        {
            if (ChkShutDown.Checked == true)
                PubConstClass.blnShutDownFlag = true;
            else
                PubConstClass.blnShutDownFlag = false;

            PubConstClass.blnReturnFlag = true;
            this.Dispose();
        }
    }
}
