﻿using System;
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
    public partial class frmNegativeProfit : Form
    {
        public frmNegativeProfit()
        {
            InitializeComponent();
        }
        private DataTable dt;

        public void grid()
        {
            try
            {
                string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                string query = "SELECT Name, Brand, ItemName, Status, Liver, Date, Cost, Price FROM customer WHERE MONTH(Date) = MONTH(CURRENT_DATE()) AND YEAR(Date) = YEAR(CURRENT_DATE()) AND Status='paid' LIMIT 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("Profit", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                    decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    if (profit < 0)
                    {
                        if (profit % 1 == 0)
                        {
                            row["Profit"] = Math.Truncate(profit);
                        }
                        else
                        {
                            row["Profit"] = Math.Round(profit, 2);
                        }
                    }
                    else
                    {
                        row.Delete();
                    }
                }

                dt.AcceptChanges();

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void gridlist()
        {
            try
            {
                string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                string query = "SELECT Name, Brand, ItemName, Status, Liver, Date, Cost, Price FROM customer WHERE MONTH(Date) = MONTH(CURRENT_DATE()) AND YEAR(Date) = YEAR(CURRENT_DATE()) AND Status='paid' LIMIT 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                dt.Columns.Add("Profit", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                    decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    if (profit < 0)
                    {
                        if (profit % 1 == 0)
                        {
                            row["Profit"] = Math.Truncate(profit);
                        }
                        else
                        {
                            row["Profit"] = Math.Round(profit, 2);
                        }
                    }
                    else
                    {
                        row.Delete();
                    }
                }

                dt.AcceptChanges();

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                var liverProfits = dt.AsEnumerable()
                    .Where(row => row.Field<decimal?>("Profit") < 0)
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
                    string query = "SELECT Name, Brand, ItemName, Status, Liver, Date, Cost, Price FROM customer WHERE Name like'" + txtNameSearch.Text + "%' and Status='paid' and Cost>Price  limit 10000000";
                    MySqlConnection conn = new MySqlConnection(connection);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dt.Columns.Add("Profit", typeof(decimal));

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                        decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                        row["Cost"] = cost.ToString("#,0");
                        row["Price"] = price.ToString("#,0");
                        decimal profit = price - cost;
                        if (profit < 0)
                        {
                            if (profit % 1 == 0)
                            {
                                row["Profit"] = Math.Truncate(profit);
                            }
                            else
                            {
                                row["Profit"] = Math.Round(profit, 2);
                            }
                        }
                        else
                        {
                            row.Delete();
                        }
                    }

                    dt.AcceptChanges();

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Please contact your administrator");
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
                string query = "SELECT Name, Brand, ItemName, Status, Liver, Date, Cost, Price FROM customer WHERE Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'and Status='paid' LIMIT 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("Profit", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                    decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    if (profit < 0)
                    {
                        if (profit % 1 == 0)
                        {
                            row["Profit"] = Math.Truncate(profit);
                        }
                        else
                        {
                            row["Profit"] = Math.Round(profit, 2);
                        }
                    }
                    else
                    {
                        row.Delete();
                    }
                }

                dt.AcceptChanges();

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Profit"].DefaultCellStyle.Format = "N2";
                var liverProfits = dt.AsEnumerable()
                    .Where(row => row.Field<decimal?>("Profit") < 0)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please contact your administrator");
            }
        }
        private void frmNegativeProfit_Load(object sender, EventArgs e)
        {
            gridlist();
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void mainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain fm = new frmMain();
            fm.ShowDialog();
        }
        private void editorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmPin fm = new frmPin();
            fm.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            searchdate();
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            search();
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

        private void exportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1);
        }

        private void listbox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listbox1_Click(object sender, EventArgs e)
        {
           
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pendingProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPendingSales fp=new frmPendingSales();
            fp.ShowDialog();
        }

        private void bigProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBigProfit fp=new frmBigProfit();
            fp.ShowDialog();
        }
    }
}
