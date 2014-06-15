using System;
using System.Windows.Forms;

namespace Last.fm
{
    public partial class FrmBigList : Form
    {
        public FrmBigList()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMain t = Application.OpenForms["FrmMain"] as FrmMain; // получение ссылки на открытую основную форму.

                for (int i = 0; i < tbBig.Lines.Length; i++)
                {
                    if (!String.IsNullOrEmpty(tbBig.Lines[i]))
                        t.AddOneLine(tbBig.Lines[i].TrimEnd(' ')); // обрезаем пробелы в конце, а то с ними возникают ошибки
                }

                t.lblSongCount.Text = t.lbList.Items.Count.ToString();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                return;
            }
            MessageBox.Show("Треки добавлены", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
