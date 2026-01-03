using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 「✕」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void PasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// パスワード入力フォーム初期ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void PasswordForm_Load(Object sender, EventArgs e)
        {
            MskTxtPassword.Text = "";
            // 「パスワード入力」テキストボックスにフォーカスセット
            this.ActiveControl = this.MskTxtPassword;
        }

        /// <summary>
        /// 「キャンセル」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnCancel_Click(Object sender, EventArgs e)
        {
            PubConstClass.blnIsOkPasswod = false;
            this.Dispose();
        }

        /// <summary>
        /// 「ＯＫ」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void BtnOK_Click(Object sender, EventArgs e)
        {
            if (MskTxtPassword.Text == PubConstClass.pblPassWord)
            {
                PubConstClass.blnIsOkPasswod = true;
                this.Dispose();
            }
            else
            {
                //Interaction.MsgBox("パスワードが違います", (MsgBoxStyle)MsgBoxStyle.Exclamation | MsgBoxStyle.OkOnly, "情報");
                MessageBox.Show("パスワードが違います", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                PubConstClass.blnIsOkPasswod = false;
            }
        }

        /// <summary>
        /// パスワード入力時の「Enter」キーの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                BtnOK.PerformClick();
            }
        }
    }
}
