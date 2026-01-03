using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TokaiCoopApp
{
    class ButtonTextCustomizableMessageBox
    {

        private IntPtr hHook = IntPtr.Zero;

        /// <summary>
        /// ボタンに表示するテキストの指定
        /// </summary>
        public CustomButtonText ButtonText { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ButtonTextCustomizableMessageBox()
        {
            this.ButtonText = new CustomButtonText();
        }

        /// <summary>
        /// メッセージボックス表示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icons"></param>
        /// <returns></returns>
        public DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icons)
        {
            try
            {
                BeginHook();
                return MessageBox.Show(text, caption, buttons, icons);
            }
            finally
            {
                EndHook();
            }
        }

        /// <summary>
        /// フックの開始
        /// </summary>
        void BeginHook()
        {
            EndHook();
            this.hHook = SetWindowsHookEx(WH_CBT, new HOOKPROC(this.HookProc), IntPtr.Zero, GetCurrentThreadId());
        }

        IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == HCBT_ACTIVATE)
            {
                if (this.ButtonText.Abort != null) SetDlgItemText(wParam, ID_BUT_ABORT, this.ButtonText.Abort);
                if (this.ButtonText.Cancel != null) SetDlgItemText(wParam, ID_BUT_CANCEL, this.ButtonText.Cancel);
                if (this.ButtonText.Ignore != null) SetDlgItemText(wParam, ID_BUT_IGNORE, this.ButtonText.Ignore);
                if (this.ButtonText.No != null) SetDlgItemText(wParam, ID_BUT_NO, this.ButtonText.No);
                if (this.ButtonText.OK != null) SetDlgItemText(wParam, ID_BUT_OK, this.ButtonText.OK);
                if (this.ButtonText.Retry != null) SetDlgItemText(wParam, ID_BUT_RETRY, this.ButtonText.Retry);
                if (this.ButtonText.Yes != null) SetDlgItemText(wParam, ID_BUT_YES, this.ButtonText.Yes);

                EndHook();
            }

            return CallNextHookEx(this.hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// フックの終了
        /// 複数呼び出し可能
        /// </summary>
        void EndHook()
        {
            if (this.hHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(this.hHook);
                this.hHook = IntPtr.Zero;
            }
        }

        #region メッセージのテキストのクラス定義
        public class CustomButtonText
        {
            public string OK { get; set; }
            public string Cancel { get; set; }
            public string Abort { get; set; }
            public string Retry { get; set; }
            public string Ignore { get; set; }
            public string Yes { get; set; }
            public string No { get; set; }
        }
        #endregion

        #region Win32API

        const int WH_CBT = 5;
        const int HCBT_ACTIVATE = 5;

        const int ID_BUT_OK = 1;
        const int ID_BUT_CANCEL = 2;
        const int ID_BUT_ABORT = 3;
        const int ID_BUT_RETRY = 4;
        const int ID_BUT_IGNORE = 5;
        const int ID_BUT_YES = 6;
        const int ID_BUT_NO = 7;

        private delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hInstance, IntPtr threadId);
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hHook);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThreadId();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SetDlgItemText(IntPtr hWnd, int nIDDlgItem, string lpString);
        #endregion

    }
}
