using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Incentives
{
    public partial class frmPin : Form
    {
        public frmPin()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void frmPin_Load(object sender, EventArgs e)
        {

        }
        public void login()
        {
            if (txtPin.Text=="0008")
            {
               this.Hide();
                frmEditor fe=new frmEditor();
                fe.ShowDialog();
            }
            else
            {
                MessageBox.Show("You entered wrong pin.");
                txtPin.Clear();
                txtPin.Focus();
            }
        }
        private void txtPin_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbShowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowpassword.Checked)
            {
                txtPin.UseSystemPasswordChar = false;
            }
            else
            {
                txtPin.UseSystemPasswordChar = true;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            login();
        }

        private void btnEnter_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }

        private void mainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain fm=new frmMain();
            fm.ShowDialog();
        }

        private void negativeProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNegativeProfit fm=new frmNegativeProfit();
            fm.ShowDialog();
        }

        private void bigProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pendingProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPendingSales fp=new frmPendingSales();
            fp.ShowDialog();
        }

        private void frmPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login();
            }
        }
    }
}
