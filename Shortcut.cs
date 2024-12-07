﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.IO;

namespace Incentives
{
    public partial class Shortcut : Form
    {
        public Shortcut()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Create WshShell instance
                IWshShell wshShell = new WshShell();

                // Define the shortcut path
                string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sales.lnk");

                // Create the shortcut object
                IWshShortcut shortcut = (IWshShortcut)wshShell.CreateShortcut(shortcutPath);

                // Set the target path (e.g., Notepad)
                shortcut.TargetPath = @"C:\Users\mark_\source\repos\Incentives\Incentives\bin\Debug\Incentives.exe";

                // Set additional shortcut properties (optional)
                shortcut.WorkingDirectory = @"C:\Windows\System32";
                shortcut.WindowStyle = 1;
                shortcut.Description = "Shortcut to Sales";
                shortcut.IconLocation = @"C:\Users\mark_\Downloads\Sales.ico";

                // Save the shortcut
                shortcut.Save();

                MessageBox.Show("Shortcut created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating shortcut: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}