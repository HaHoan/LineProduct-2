﻿using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic

namespace Line_Production
{
    

    [DesignerGenerated()]
    public partial class frmConfig : System.Windows.Forms.Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.chkWip = new System.Windows.Forms.CheckBox();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnBrower = new System.Windows.Forms.Button();
            this.txtStation = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbCOM = new System.Windows.Forms.ComboBox();
            this.btnTestCOM = new System.Windows.Forms.Button();
            this.chkLinkWip = new System.Windows.Forms.CheckBox();
            this.btnSaveChanged = new System.Windows.Forms.Button();
            this.cbbProcess = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkWip
            // 
            this.chkWip.AutoSize = true;
            this.chkWip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWip.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.chkWip.Location = new System.Drawing.Point(183, 199);
            this.chkWip.Name = "chkWip";
            this.chkWip.Size = new System.Drawing.Size(109, 17);
            this.chkWip.TabIndex = 2;
            this.chkWip.Text = "Write Log WIP";
            this.chkWip.UseVisualStyleBackColor = true;
            this.chkWip.Visible = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 98);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 13);
            this.Label1.TabIndex = 25;
            this.Label1.Text = "Path Log";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(87, 88);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(205, 20);
            this.txtLog.TabIndex = 26;
            // 
            // btnBrower
            // 
            this.btnBrower.Location = new System.Drawing.Point(302, 88);
            this.btnBrower.Name = "btnBrower";
            this.btnBrower.Size = new System.Drawing.Size(31, 20);
            this.btnBrower.TabIndex = 27;
            this.btnBrower.Text = "...";
            this.btnBrower.UseVisualStyleBackColor = true;
            this.btnBrower.Click += new System.EventHandler(this.btnBrower_Click);
            // 
            // txtStation
            // 
            this.txtStation.Location = new System.Drawing.Point(87, 124);
            this.txtStation.Name = "txtStation";
            this.txtStation.Size = new System.Drawing.Size(205, 20);
            this.txtStation.TabIndex = 32;
            this.txtStation.Text = "VI_YO";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(12, 131);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(40, 13);
            this.Label3.TabIndex = 31;
            this.Label3.Text = "Station";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(87, 52);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(205, 20);
            this.txtId.TabIndex = 36;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(12, 59);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(27, 13);
            this.Label5.TabIndex = 35;
            this.Label5.Text = "Line";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "COM";
            // 
            // cbbCOM
            // 
            this.cbbCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCOM.FormattingEnabled = true;
            this.cbbCOM.Location = new System.Drawing.Point(87, 162);
            this.cbbCOM.Name = "cbbCOM";
            this.cbbCOM.Size = new System.Drawing.Size(121, 21);
            this.cbbCOM.TabIndex = 38;
            // 
            // btnTestCOM
            // 
            this.btnTestCOM.Location = new System.Drawing.Point(217, 160);
            this.btnTestCOM.Name = "btnTestCOM";
            this.btnTestCOM.Size = new System.Drawing.Size(75, 23);
            this.btnTestCOM.TabIndex = 39;
            this.btnTestCOM.Text = "Test COM";
            this.btnTestCOM.UseVisualStyleBackColor = true;
            this.btnTestCOM.Click += new System.EventHandler(this.btnTestCOM_Click);
            // 
            // chkLinkWip
            // 
            this.chkLinkWip.AutoSize = true;
            this.chkLinkWip.Checked = true;
            this.chkLinkWip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLinkWip.Location = new System.Drawing.Point(87, 199);
            this.chkLinkWip.Name = "chkLinkWip";
            this.chkLinkWip.Size = new System.Drawing.Size(70, 17);
            this.chkLinkWip.TabIndex = 40;
            this.chkLinkWip.Text = "Link WIP";
            this.chkLinkWip.UseVisualStyleBackColor = true;
            // 
            // btnSaveChanged
            // 
            this.btnSaveChanged.BackColor = System.Drawing.Color.Green;
            this.btnSaveChanged.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanged.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanged.Image = global::Line_Production.Properties.Resources.Save;
            this.btnSaveChanged.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanged.Location = new System.Drawing.Point(174, 236);
            this.btnSaveChanged.Name = "btnSaveChanged";
            this.btnSaveChanged.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveChanged.Size = new System.Drawing.Size(118, 30);
            this.btnSaveChanged.TabIndex = 24;
            this.btnSaveChanged.Text = "Save change";
            this.btnSaveChanged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveChanged.UseVisualStyleBackColor = false;
            this.btnSaveChanged.Click += new System.EventHandler(this.btnSaveChanged_Click);
            // 
            // cbbProcess
            // 
            this.cbbProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbProcess.FormattingEnabled = true;
            this.cbbProcess.Location = new System.Drawing.Point(87, 12);
            this.cbbProcess.Name = "cbbProcess";
            this.cbbProcess.Size = new System.Drawing.Size(205, 21);
            this.cbbProcess.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Process:";
            // 
            // frmConfig
            // 
            this.AcceptButton = this.btnSaveChanged;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 290);
            this.Controls.Add(this.cbbProcess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkLinkWip);
            this.Controls.Add(this.btnTestCOM);
            this.Controls.Add(this.cbbCOM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtStation);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btnBrower);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnSaveChanged);
            this.Controls.Add(this.chkWip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configs";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private CheckBox chkWip;
        private Button btnSaveChanged;
        internal FolderBrowserDialog FolderBrowserDialog1;
        internal Label Label1;
        internal TextBox txtLog;
        internal Button btnBrower;
        internal TextBox txtStation;
        internal Label Label3;
        internal TextBox txtId;
        internal Label Label5;
        private Label label4;
        private ComboBox cbbCOM;
        private Button btnTestCOM;
        internal CheckBox chkLinkWip;
        private ComboBox cbbProcess;
        private Label label2;
    }
}