﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Incentives
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        private MySqlConnection con;
        public void connection()
        {
            con = new MySqlConnection("datasource=localhost;database=maluhotimesheet;port=3306;userid=root;password=root");
        }
        public void login()
        {        
                try
                {
                    connection();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from user where Username ='" + txtUsername.Text + "'and Password='" + txtPassword.Text + "'", con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Login success");
                        frmMain fm = new frmMain();
                        this.Hide();
                        fm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Log-in failed, Please check your Username, Password or Access");
                        txtPassword.Clear();
                    }
                    reader.Close();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("You username or password is incorrect", ex.Message);
                }         
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
           // if (e.KeyChar == (char)Keys.Enter)
           if(e.KeyChar ==(char)Keys.Enter)
            {
                login();
            }
        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cbShowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShowpassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar= true;
            }

        }
    }
}
