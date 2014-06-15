using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            FrmMain t = Application.OpenForms["FrmMain"] as FrmMain; // получение ссылки на открытую основную форму.

            for (int i = 0; i < tbBig.Lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(tbBig.Lines[i]))
                t.AddOneLine(tbBig.Lines[i]);
            }

            t.lblSongCount.Text = t.lbList.Items.Count.ToString();

            MessageBox.Show("Треки добавлены", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
