using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenCoopApp
{
    public partial class NumberOfFormOverList : Form
    {

        public string numberOverList;

        // ハードコピーオブジェクト
        private NonHCopyNet.HardCopyClass objHardCopy = new NonHCopyNet.HardCopyClass();

        public NumberOfFormOverList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberOfFormOverList_Load(object sender, EventArgs e)
        {
            try
            {
                LblTitle.Text = "帳票枚数オーバーリスト";
                LblVersion.Text = PubConstClass.DEF_VERSION;

                // 帳票枚数オーバーリスト表示ListViewのカラムヘッダー設定
                LstNumberOverList.View = View.Details;
                ColumnHeader col1 = new ColumnHeader();
                ColumnHeader col2 = new ColumnHeader();
                ColumnHeader col3 = new ColumnHeader();
                ColumnHeader col4 = new ColumnHeader();
                ColumnHeader col5 = new ColumnHeader();

                col1.Text = "SEQ";
                col2.Text = "組合員番号";
                col3.Text = "組合員名称";
                col4.Text = "帳票ST1";
                col5.Text = "帳票ST2";

                col1.TextAlign = HorizontalAlignment.Center;
                col2.TextAlign = HorizontalAlignment.Center;
                col3.TextAlign = HorizontalAlignment.Center;
                col4.TextAlign = HorizontalAlignment.Center;
                col5.TextAlign = HorizontalAlignment.Center;

                col1.Width = 100;        // SEQ
                col2.Width = 150;        // 組合員番号
                col3.Width = 250;        // 組合員名称                
                col4.Width = 100;        // 帳票ST1
                col5.Width = 100;        // 帳票ST2

                ColumnHeader[] colHeader = new[] { col1, col2, col3, col4, col5 };
                LstNumberOverList.Columns.AddRange(colHeader);

                // データの表示
                if(PubConstClass.numberOverList.Count > 0)
                {
                    ListViewItem itm;
                    string[] sAry;
                    foreach(var sData in PubConstClass.numberOverList)
                    {
                        sAry = sData.Split(',');
                        // データの表示
                        itm = new ListViewItem(sAry);
                        LstNumberOverList.Items.Add(itm);
                        LstNumberOverList.Items[LstNumberOverList.Items.Count - 1].UseItemStyleForSubItems = false;
                        if (LstNumberOverList.Items.Count % 2 == 1)
                        {
                            for (int iIndex = 0; iIndex < 5; iIndex++)
                            {
                                // 奇数行の色反転
                                LstNumberOverList.Items[LstNumberOverList.Items.Count - 1].SubItems[iIndex].BackColor = Color.FromArgb(200, 200, 230);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "【NumberOfFormOverList_Load】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // ［Alt］キー＋［Print Screen］キーの送信
                SendKeys.SendWait("%{PRTSC}");

                // 用紙方向を横向きに設定
                PrintDocument objPrinter = new PrintDocument();
                objPrinter.DefaultPageSettings.Landscape = true;

                // 用紙サイズをＡ４に設定
                foreach (PaperSize psz in objPrinter.PrinterSettings.PaperSizes)
                {
                    if (psz.Kind == PaperKind.A4)
                    {
                        objPrinter.DefaultPageSettings.PaperSize = psz;
                        break;
                    }
                }

                // 通常使うプリンタへのアクティブウィンドウ（アクティブウィンドウ）のハードコピー
                objHardCopy.HardCopy(true, objPrinter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "【BtnPrintScreen_Click】", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
