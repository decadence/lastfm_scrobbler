using System;
using System.Windows.Forms;

namespace Last.fm
{
    public partial class FrmSetUsername : Form
    {
        public FrmSetUsername()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbUsername.Text))
            {
                Program.username = tbUsername.Text;
                DialogResult = DialogResult.OK;
            }
            else MessageBox.Show("Поле пусто!");
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
