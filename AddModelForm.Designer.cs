namespace Line_Production
{
    partial class AddModelForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txbModelID = new System.Windows.Forms.TextBox();
            this.txbPersonInLine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbCycle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbWarmQuatity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbMnQuantity = new System.Windows.Forms.TextBox();
            this.txbRegex = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbUseBarcode = new System.Windows.Forms.CheckBox();
            this.btnSaveChanged = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbUseMacbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model ID";
            // 
            // txbModelID
            // 
            this.txbModelID.Location = new System.Drawing.Point(154, 20);
            this.txbModelID.Name = "txbModelID";
            this.txbModelID.Size = new System.Drawing.Size(172, 20);
            this.txbModelID.TabIndex = 1;
            // 
            // txbPersonInLine
            // 
            this.txbPersonInLine.Location = new System.Drawing.Point(154, 65);
            this.txbPersonInLine.Name = "txbPersonInLine";
            this.txbPersonInLine.Size = new System.Drawing.Size(172, 20);
            this.txbPersonInLine.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số người trên Line";
            // 
            // txbCycle
            // 
            this.txbCycle.Location = new System.Drawing.Point(154, 112);
            this.txbCycle.Name = "txbCycle";
            this.txbCycle.Size = new System.Drawing.Size(172, 20);
            this.txbCycle.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cycle Time";
            // 
            // txbWarmQuatity
            // 
            this.txbWarmQuatity.Location = new System.Drawing.Point(154, 161);
            this.txbWarmQuatity.Name = "txbWarmQuatity";
            this.txbWarmQuatity.Size = new System.Drawing.Size(172, 20);
            this.txbWarmQuatity.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cảnh báo số lượng";
            // 
            // txbMnQuantity
            // 
            this.txbMnQuantity.Location = new System.Drawing.Point(154, 208);
            this.txbMnQuantity.Name = "txbMnQuantity";
            this.txbMnQuantity.Size = new System.Drawing.Size(172, 20);
            this.txbMnQuantity.TabIndex = 9;
            // 
            // txbRegex
            // 
            this.txbRegex.Location = new System.Drawing.Point(154, 249);
            this.txbRegex.Name = "txbRegex";
            this.txbRegex.Size = new System.Drawing.Size(172, 20);
            this.txbRegex.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Kí tự Model";
            // 
            // ckbUseBarcode
            // 
            this.ckbUseBarcode.AutoSize = true;
            this.ckbUseBarcode.Checked = true;
            this.ckbUseBarcode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbUseBarcode.Location = new System.Drawing.Point(217, 293);
            this.ckbUseBarcode.Name = "ckbUseBarcode";
            this.ckbUseBarcode.Size = new System.Drawing.Size(109, 17);
            this.ckbUseBarcode.TabIndex = 13;
            this.ckbUseBarcode.Text = "Sử dụng Barcode";
            this.ckbUseBarcode.UseVisualStyleBackColor = true;
            // 
            // btnSaveChanged
            // 
            this.btnSaveChanged.BackColor = System.Drawing.Color.Green;
            this.btnSaveChanged.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanged.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanged.Image = global::Line_Production.Properties.Resources.Save;
            this.btnSaveChanged.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanged.Location = new System.Drawing.Point(208, 333);
            this.btnSaveChanged.Name = "btnSaveChanged";
            this.btnSaveChanged.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveChanged.Size = new System.Drawing.Size(118, 30);
            this.btnSaveChanged.TabIndex = 25;
            this.btnSaveChanged.Text = "Save change";
            this.btnSaveChanged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveChanged.UseVisualStyleBackColor = false;
            this.btnSaveChanged.Click += new System.EventHandler(this.btnSaveChanged_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cảnh báo SL bất thường";
            // 
            // cbUseMacbox
            // 
            this.cbUseMacbox.AutoSize = true;
            this.cbUseMacbox.Checked = true;
            this.cbUseMacbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseMacbox.Location = new System.Drawing.Point(88, 293);
            this.cbUseMacbox.Name = "cbUseMacbox";
            this.cbUseMacbox.Size = new System.Drawing.Size(110, 17);
            this.cbUseMacbox.TabIndex = 26;
            this.cbUseMacbox.Text = "Sử dụng Mac box";
            this.cbUseMacbox.UseVisualStyleBackColor = true;
            // 
            // AddModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 385);
            this.Controls.Add(this.cbUseMacbox);
            this.Controls.Add(this.btnSaveChanged);
            this.Controls.Add(this.ckbUseBarcode);
            this.Controls.Add(this.txbRegex);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txbMnQuantity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txbWarmQuatity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbCycle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbPersonInLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbModelID);
            this.Controls.Add(this.label1);
            this.Name = "AddModelForm";
            this.Text = "AddModelForm";
            this.Shown += new System.EventHandler(this.AddModelForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbModelID;
        private System.Windows.Forms.TextBox txbPersonInLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbCycle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbWarmQuatity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbMnQuantity;
        private System.Windows.Forms.TextBox txbRegex;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckbUseBarcode;
        private System.Windows.Forms.Button btnSaveChanged;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbUseMacbox;
    }
}