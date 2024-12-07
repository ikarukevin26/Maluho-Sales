using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Incentives
{
    public partial class frmEditor : Form
    {
        public frmEditor()
        {
            InitializeComponent();
            txtCost.KeyPress += txtCost_KeyPress;
            txtPrice.KeyPress += txtPrice_KeyPress;
            txtProfit.KeyPress += txtProfit_KeyPress;
            
        }
        public DataTable dt;
        public void grid()
        {
            try
            {
                string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                string query = "SELECT customer.Name, customer.Brand, customer.ItemName, customer.Status, customer.Liver, customer.Date,customer.Id,inventory.Barcode, customer.Cost, customer.Price  FROM customer inner join inventory on customer.InventoryID=inventory.InventoryID  WHERE MONTH(customer.Date) = MONTH(CURRENT_DATE()) AND YEAR(customer.Date) = YEAR(CURRENT_DATE()) and Status='paid' limit 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable(); // Initialize dt
                da.Fill(dt);

                // Add a new DataColumn for profit
                dt.Columns.Add("Profit", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                    decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    if (profit % 1 == 0)
                    {
                        // If profit is a whole number, assign it without decimal places
                        row["Profit"] = Math.Truncate(profit);
                    }
                    else
                    {
                        // If profit has decimal places, assign it with two decimal place
                        row["Profit"] = Math.Round(profit, 2);
                    }
                }

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void search()
        {
            if (txtNameSearch.Text != "")
            {
                try
                {
                    string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                    string query = "SELECT customer.Name, customer.Brand, customer.ItemName, customer.Status, customer.Liver, customer.Date,customer.Id,inventory.Barcode, customer.Cost, customer.Price  FROM customer inner join inventory on customer.InventoryID=inventory.InventoryID where Name like'" + txtNameSearch.Text + "%' and Status!='cancel'";
                    MySqlConnection conn = new MySqlConnection(connection);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    dataGridView1.RowTemplate.Height = 100;
                    dataGridView1.AllowUserToAddRows = false;
                    da.Fill(dt);

                    // Add a new DataColumn for profit
                    dt.Columns.Add("Profit", typeof(decimal));

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                        decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                        row["Cost"] = cost.ToString("#,0");
                        row["Price"] = price.ToString("#,0");
                        decimal profit = price - cost;
                        if (profit % 1 == 0)
                        {
                            // If profit is a whole number, assign it without decimal places
                            row["Profit"] = Math.Truncate(profit);
                        }
                        else
                        {
                            // If profit has decimal places, assign it with two decimal place
                            row["Profit"] = Math.Round(profit, 2);
                        }
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                    conn.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                grid();
            }          
        }
        public void barcodesearch()
        {
            if (txtBarcode.Text!="")
            {
                try
                {

                    string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                    string query = "SELECT customer.Name, customer.Brand, customer.ItemName, customer.Status, customer.Liver, customer.Date,customer.Id,inventory.Barcode, customer.Cost, customer.Price  FROM customer inner join inventory on customer.InventoryID=inventory.InventoryID where inventory.Barcode like'" + txtBarcode.Text + "%' and Status!='cancel'";
                    MySqlConnection conn = new MySqlConnection(connection);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    dataGridView1.RowTemplate.Height = 100;
                    dataGridView1.AllowUserToAddRows = false;
                    da.Fill(dt);

                    // Add a new DataColumn for profit
                    dt.Columns.Add("Profit", typeof(decimal));

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                        decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                        row["Cost"] = cost.ToString("#,0");
                        row["Price"] = price.ToString("#,0");
                        decimal profit = price - cost;
                        if (profit % 1 == 0)
                        {
                            // If profit is a whole number, assign it without decimal places
                            row["Profit"] = Math.Truncate(profit);
                        }
                        else
                        {
                            // If profit has decimal places, assign it with two decimal place
                            row["Profit"] = Math.Round(profit, 2);
                        }
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please contact your administrator", ex.Message);
                }
            }
            else
            {
                grid();
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            grid();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin fm=new frmLogin();
            fm.ShowDialog();        
        }
        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            search();
        }
        public void update()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Name can't be empty");
            }
            else if (txtBrand.Text == "")
            {
                MessageBox.Show("Brand can't be empty");
            }
            else if (txtItemName.Text == "")
            {
                MessageBox.Show("Item Name can't be empty");
            }
            else if (txtCost.Text == "")
            {
                MessageBox.Show("Cost can't be empty");
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("Price can't be empty");
            }
            else
            {
                try
                {
                    string cost = txtCost.Text.Replace(",", "");
                    string price = txtPrice.Text.Replace(",", "");
                    int price1 = int.Parse(price);
                    int cost1 = int.Parse(cost);
                    double price2 = price1 * .40;
                    double cost2 = cost1 * .40; 


                    MySqlConnection con = new MySqlConnection("datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root");
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("update customer set Name='" + txtName.Text + "', Brand='" + txtBrand.Text + "', ItemName='" + txtItemName.Text + "', Liver='" + txtLiveSeller.Text + "',Cost='" + cost2 + "',Price='" + price2 + "',Date='" + txtDate.Text + "' where Id='" + txtID.Text + "'", con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Can't update inventory information");
                        con.Close();
                        reader.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Successfuly updated inventory information");
                        txtID.Clear();
                        txtName.Clear();
                        txtBrand.Clear();
                        txtItemName.Clear();
                        txtCost.Clear();
                        txtPrice.Clear();
                        txtProfit.Clear();
                        txtLiveSeller.Clear();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            
                grid();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    txtID.Text = row.Cells["Id"].Value.ToString();
                    txtName.Text = row.Cells["Name"].Value.ToString();
                    txtBrand.Text = row.Cells["Brand"].Value.ToString();
                    txtItemName.Text = row.Cells["ItemName"].Value.ToString();
                    txtCost.Text = row.Cells["Cost"].Value.ToString();
                    txtPrice.Text = row.Cells["Price"].Value.ToString();
                    decimal profit = Convert.ToDecimal(row.Cells["Profit"].Value);
                    txtProfit.Text = profit.ToString("#,0");
                    txtLiveSeller.Text = row.Cells["Liver"].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtProfit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void mainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain fm = new frmMain();
            fm.ShowDialog();
        }

        private void negativeProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNegativeProfit fn=new frmNegativeProfit();
            fn.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bigProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBigProfit fn=new frmBigProfit();
            fn.ShowDialog();
        }

        private void pendingProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPendingSales fp=new frmPendingSales();
            fp.ShowDialog();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            txtName.Clear();
            barcodesearch();
        }
    }
}
