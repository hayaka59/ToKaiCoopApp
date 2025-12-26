using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GreenCoopAppTest
{
    public partial class MainForm : Form
    {

        public ImageConversionForm imageConversionForm = new ImageConversionForm();
        
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// メインフォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LblDateTime.Text = "";
                TimDateTime.Interval = 1000;
                TimDateTime.Enabled = true;
                TxtQrReadData.Text = "";
                LsbQrReadDataZ.Items.Clear();
                LsbQrReadDataY.Items.Clear();

                // QR用シリアルポート名の設定
                SerialPortQr.PortName = "COM1";

                // シリアルポートの通信速度指定
                SerialPortQr.BaudRate = 115200;

                // シリアルポートのパリティ指定
                SerialPortQr.Parity = Parity.Even;

                // シリアルポートのパリティ有無
                SerialPortQr.Parity = Parity.Even;

                // シリアルポートのビット数指定
                SerialPortQr.DataBits = 8;

                // シリアルポートのストップビット指定
                SerialPortQr.StopBits = StopBits.One;

                // シリアルポートのオープン
                SerialPortQr.Open();

                // シリアルポート（ＱＲリーダ）にデータ送信（動作可コマンド）
                byte[] dat = Encoding.GetEncoding("SHIFT-JIS").GetBytes("Send Test data" + ControlChars.Cr);
                SerialPortQr.Write(dat, 0, dat.GetLength(0));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【MainForm_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 時計表示用タイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimDateTime_Tick(object sender, EventArgs e)
        {
            LblDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void TxtQrReadData_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == ControlChars.Cr)
                {
                    string sQR1 = TxtQrReadData.Text.Substring(0,8);
                    string sQR2 = TxtQrReadData.Text.Substring(9, 10);
                    string sQRZ = sQR1 + "Z" + sQR2;
                    string sQRY = sQR1 + "Y" + sQR2;

                    LsbQrReadDataZ.Items.Add(sQRZ);
                    LsbQrReadDataY.Items.Add(sQRY);

                    TxtQrReadData.Text = "";

                    // シリアルポート（ＱＲリーダ）にデータ送信
                    byte[] datZ = Encoding.GetEncoding("SHIFT-JIS").GetBytes(sQRZ + ControlChars.Cr);
                    SerialPortQr.Write(datZ, 0, datZ.GetLength(0));

                    // シリアルポート（ＱＲリーダ）にデータ送信
                    byte[] datY = Encoding.GetEncoding("SHIFT-JIS").GetBytes(sQRY + ControlChars.Cr);
                    SerialPortQr.Write(datY, 0, datY.GetLength(0));

                    return;
                }

                if (e.KeyChar == ControlChars.Back | e.KeyChar == ControlChars.Tab)
                    // 「BS」キーは対象外とする
                    return;

                //if (e.KeyChar < '0' || '9' < e.KeyChar)
                //    // 数字キー「0」～「9」以外はイベントをキャンセルする
                //    e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【TxtWorkCode_KeyPress】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// 「画像変換」ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConversion_Click(object sender, EventArgs e)
        {
            imageConversionForm.Show(this);
            this.Hide();
        }
    }
}
