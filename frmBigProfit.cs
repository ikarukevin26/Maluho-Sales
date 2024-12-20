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
using DocumentFormat.OpenXml.Bibliography;
using MySql.Data.MySqlClient;

namespace Incentives
{
    public partial class frmBigProfit : Form
    {
        public frmBigProfit()
        {
            InitializeComponent();
            
        }
        public DataTable dt; 

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
                    if (profit > 500000)
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
            catch(Exception ex)
            {
                MessageBox.Show("Please contact your administrator",ex.Message);
            }
        }
        public void gridlist()
        {
            try
            {
                string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                string query = "SELECT inventory.Barcode,customer.Name, customer.Brand, customer.ItemName, customer.Status, customer.Liver, customer.Date, customer.Cost, customer.Price FROM customer inner join inventory on customer.InventoryID = inventory.InventoryID  WHERE MONTH(customer.Date) = MONTH(CURRENT_DATE()) AND YEAR(customer.Date) = YEAR(CURRENT_DATE()) AND customer.Status='paid' LIMIT 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("Profit", typeof(decimal));
                dt.Columns.Add("ProfitPercentage", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                    decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    decimal profitPercentage = (profit / cost) * 100;

                    if (profit > 500000 || profitPercentage>150)
                    {
                        row["Cost"]= cost.ToString("#,0");
                        row["Price"] = price.ToString("#,0");
                        row["Profit"] = profit.ToString("#,0");
                        row["ProfitPercentage"] = profitPercentage.ToString("N2");
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
                    .GroupBy(row => row.Field<string>("Liver"))
                    .Select(group =>
                        new
                        {
                            Liver = group.Key,
                            TotalProfit = group.Sum(row => row.Field<decimal>("Profit"))
                        })
                    .Where(item => item.TotalProfit > 500000);

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
                MessageBox.Show("Please contact your administrator", ex.Message);
            }

        }

        public void search()
        {
            if (txtNameSearch.Text != "")
            {
                try
                {
                    string connection = "datasource=localhost;database=therealmaluho;port=3306;userid=root;password=root";
                    string query = "SELECT Name, Brand, ItemName, Status, Liver, Date, Cost, Price FROM customer WHERE Name like '"+txtNameSearch.Text+"%' AND Status='paid' LIMIT 10000000";
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
                        if (profit > 500000)
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
                string query = "SELECT InventoryID, Name, Brand, ItemName, Status, Liver, Date, Cost, Price FROM customer WHERE Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'and Status='paid' LIMIT 10000000";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("Profit", typeof(decimal));
                dt.Columns.Add("ProfitPercentage", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["Cost"]) * 2.50m;
                    decimal price = Convert.ToDecimal(row["Price"]) * 2.50m;
                    row["Cost"] = cost.ToString("#,0");
                    row["Price"] = price.ToString("#,0");
                    decimal profit = price - cost;
                    decimal profitPercentage = (profit / cost) * 100;

                    if (profit > 500000 || profitPercentage>150)
                    {
                        if (profit % 1 == 0)
                        {
                            row["Cost"]= cost.ToString("#,0");
                            row["Profit"] = profit.ToString("#,0");
                            row["Price"] = price.ToString("#,0");
                            row["ProfitPercentage"] = profitPercentage.ToString("N2");
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
                    .GroupBy(row => row.Field<string>("Liver"))
                    .Select(group =>
                        new
                        {
                            Liver = group.Key,
                            TotalProfit = group.Sum(row => row.Field<decimal>("Profit"))
                        })
                    .Where(item => item.TotalProfit > 500000);

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
                MessageBox.Show("Please contact your administrator",ex.Message);
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmBigProfit_Load(object sender, EventArgs e)
        {
            gridlist();
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            search();
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

        private void negativeProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNegativeProfit fn=new frmNegativeProfit();
            fn.ShowDialog();
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

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPin fp=new frmPin();
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
    }
}
