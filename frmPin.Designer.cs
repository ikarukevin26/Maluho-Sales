﻿namespace Incentives
{
    partial class frmPin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPin = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbShowpassword = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeProfitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigProfitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pendingProfitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPin
            // 
            this.txtPin.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPin.Location = new System.Drawing.Point(62, 46);
            this.txtPin.Margin = new System.Windows.Forms.Padding(2);
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(308, 34);
            this.txtPin.TabIndex = 0;
            this.txtPin.UseSystemPasswordChar = true;
            this.txtPin.TextChanged += new System.EventHandler(this.txtPin_TextChanged);
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEnter.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.ForeColor = System.Drawing.Color.Snow;
            this.btnEnter.Location = new System.Drawing.Point(175, 100);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(88, 38);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            this.btnEnter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnEnter_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pin";
            // 
            // cbShowpassword
            // 
            this.cbShowpassword.AutoSize = true;
            this.cbShowpassword.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowpassword.Location = new System.Drawing.Point(64, 81);
            this.cbShowpassword.Margin = new System.Windows.Forms.Padding(2);
            this.cbShowpassword.Name = "cbShowpassword";
            this.cbShowpassword.Size = new System.Drawing.Size(77, 21);
            this.cbShowpassword.TabIndex = 7;
            this.cbShowpassword.Text = "Show pin";
            this.cbShowpassword.UseVisualStyleBackColor = true;
            this.cbShowpassword.CheckedChanged += new System.EventHandler(this.cbShowpassword_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(398, 31);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainFormToolStripMenuItem,
            this.negativeProfitToolStripMenuItem,
            this.bigProfitToolStripMenuItem,
            this.pendingProfitToolStripMenuItem});
            this.menuToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(67, 27);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // mainFormToolStripMenuItem
            // 
            this.mainFormToolStripMenuItem.Name = "mainFormToolStripMenuItem";
            this.mainFormToolStripMenuItem.Size = new System.Drawing.Size(195, 28);
            this.mainFormToolStripMenuItem.Text = "Main Form";
            this.mainFormToolStripMenuItem.Click += new System.EventHandler(this.mainFormToolStripMenuItem_Click);
            // 
            // negativeProfitToolStripMenuItem
            // 
            this.negativeProfitToolStripMenuItem.Name = "negativeProfitToolStripMenuItem";
            this.negativeProfitToolStripMenuItem.Size = new System.Drawing.Size(195, 28);
            this.negativeProfitToolStripMenuItem.Text = "Negative Profit";
            this.negativeProfitToolStripMenuItem.Click += new System.EventHandler(this.negativeProfitToolStripMenuItem_Click);
            // 
            // bigProfitToolStripMenuItem
            // 
            this.bigProfitToolStripMenuItem.Name = "bigProfitToolStripMenuItem";
            this.bigProfitToolStripMenuItem.Size = new System.Drawing.Size(195, 28);
            this.bigProfitToolStripMenuItem.Text = "Big Profit";
            this.bigProfitToolStripMenuItem.Click += new System.EventHandler(this.bigProfitToolStripMenuItem_Click);
            // 
            // pendingProfitToolStripMenuItem
            // 
            this.pendingProfitToolStripMenuItem.Name = "pendingProfitToolStripMenuItem";
            this.pendingProfitToolStripMenuItem.Size = new System.Drawing.Size(195, 28);
            this.pendingProfitToolStripMenuItem.Text = "Pending Profit";
            this.pendingProfitToolStripMenuItem.Click += new System.EventHandler(this.pendingProfitToolStripMenuItem_Click);
            // 
            // frmPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 154);
            this.Controls.Add(this.cbShowpassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.txtPin);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pin";
            this.Load += new System.EventHandler(this.frmPin_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmPin_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbShowpassword;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeProfitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bigProfitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pendingProfitToolStripMenuItem;
    }
}