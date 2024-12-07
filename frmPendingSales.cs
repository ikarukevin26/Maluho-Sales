using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;

namespace Incentives
{
    public partial class frmPendingSales : Form
    {
        public frmPendingSales()
        {
            InitializeComponent();
        }
        public DataTable dt;
        public void gridlist()
        {
            try
            {
               
                string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                string query = "SELECT Name, Brand, ItemName, Status, Liver, Date, Cost, Price FROM customer WHERE MONTH(Date) = MONTH(CURRENT_DATE()) AND YEAR(Date) = YEAR(CURRENT_DATE()) and (Status='pending' or Status='pending-layaway') limit 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);


                dt.Columns.Add("Profit", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) /0.42m;
                    decimal price = Convert.ToDecimal(row["Price"]) / 0.42m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    if (profit % 1 == 0)
                    {
                        row["Profit"] = Math.Truncate(profit);
                    }
                    else
                    {
                        row["Profit"] = Math.Round(profit, 2);
                    }
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";

                var liverProfits = dt.AsEnumerable()
                                    .GroupBy(row => row.Field<string>("Liver"))
                                    .Select(group =>
                                        new
                                        {
                                            Liver = group.Key,
                                            TotalProfit = group.Sum(row => row.Field<decimal>("Profit"))
                                        })
                                            .OrderByDescending(item => item.TotalProfit);

                decimal totalProfit = 0m;

                foreach (var liverProfit in liverProfits)
                {
                    listbox1.Items.Add($"{liverProfit.Liver}:  ¥ {liverProfit.TotalProfit.ToString("N2")}");
                    totalProfit += liverProfit.TotalProfit;
                }

                lblProfit.Text = $" ¥ {totalProfit.ToString("N2")}";

                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Please contact your administrator");
            }
        }
        public void grid()
        {
            try
            {
                string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                string query = "SELECT Name, Brand, ItemName, Status, Liver, Date, Cost, Price  FROM customer WHERE MONTH(Date) = MONTH(CURRENT_DATE()) AND YEAR(Date) = YEAR(CURRENT_DATE()) and (Status='pending' or Status='pending-layaway') limit 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("Profit", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) /0.42m;
                    decimal price = Convert.ToDecimal(row["Price"]) / 0.42m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    if (profit % 1 == 0)
                    {
                        row["Profit"] = Math.Truncate(profit);
                    }
                    else
                    {
                        row["Profit"] = Math.Round(profit, 2);
                    }
                }

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                conn.Close();
            }
            catch(Exception ex)
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
                    string query = "SELECT Name, Brand, ItemName,Status, Liver, Date, Cost, Price FROM customer where Name like'" + txtNameSearch.Text + "%' and (Status ='pending' or Status='pending-layaway') limit 100000";
                    MySqlConnection conn = new MySqlConnection(connection);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    dataGridView1.RowTemplate.Height = 100;
                    dataGridView1.AllowUserToAddRows = false;
                    da.Fill(dt);

                    dt.Columns.Add("Profit", typeof(decimal));

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal cost = Convert.ToDecimal(row["Cost"]) /0.42m;
                        decimal price = Convert.ToDecimal(row["Price"]) /0.42m;
                        row["Cost"] = cost.ToString("#,0");
                        row["Price"] = price.ToString("#,0");
                        decimal profit = price - cost;
                        if (profit % 1 == 0)
                        {
                            row["Profit"] = Math.Truncate(profit);
                        }
                        else
                        {
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

        public void searchdate()
        {
            try
            {
                listbox1.Items.Clear();
                string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                string query = "SELECT Name, Brand, ItemName, Liver,Status, Date, Cost, Price FROM customer WHERE Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'and (Status='pending' or Status='pending-layaway') limit 10000000 ";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dataGridView1.RowTemplate.Height = 100;
                dataGridView1.AllowUserToAddRows = false;
                da.Fill(dt);

                dt.Columns.Add("Profit", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) / 0.42m;
                    decimal price = Convert.ToDecimal(row["Price"]) /  0.42m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    if (profit % 1 == 0)
                    {
                        row["Profit"] = Math.Truncate(profit);
                    }
                    else
                    {
                        row["Profit"] = Math.Round(profit, 2);
                    }
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";

                var liverProfits = dt.AsEnumerable()
                                    .GroupBy(row => row.Field<string>("Liver"))
                                    .Select(group =>
                                        new
                                        {
                                            Liver = group.Key,
                                            TotalProfit = group.Sum(row => row.Field<decimal>("Profit"))
                                        });

                decimal totalProfit = 0m;

                foreach (var liverProfit in liverProfits)
                {
                    listbox1.Items.Add($"{liverProfit.Liver}:  ¥ {liverProfit.TotalProfit.ToString("N2")}");
                    totalProfit += liverProfit.TotalProfit;
                }

                lblProfit.Text = $" ¥ {totalProfit.ToString("N2")}";

                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PendingSales_Load(object sender, EventArgs e)
        {
            gridlist();
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void listbox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox1.SelectedItem != null)
                {
                    string selectedLiver = listbox1.SelectedItem.ToString().Split(':')[0].Trim();

                    DataTable filteredData = dt.AsEnumerable()
                                               .Where(row => row.Field<string>("Liver") == selectedLiver)
                                               .CopyToDataTable();

                    dataGridView1.DataSource = filteredData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchdate();
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain fm=new frmMain();
            fm.ShowDialog();
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPin fp=new frmPin();
            fp.ShowDialog();
        }

        private void negativeProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNegativeProfit fp=new frmNegativeProfit();
            fp.ShowDialog();
        }
        private void ExportToExcel(DataGridView dataGridView)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Sheet1");

                        for (int i = 1; i <= dataGridView.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i).Value = dataGridView.Columns[i - 1].HeaderText;
                        }

                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            }
                        }
                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    MessageBox.Show("Data exported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1);
        }

        private void bigProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBigProfit fb=new frmBigProfit();
            fb.ShowDialog();
        }
    }
}
