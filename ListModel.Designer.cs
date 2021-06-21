namespace Line_Production
{
    partial class ListModel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgrvListModel = new System.Windows.Forms.DataGridView();
            this.txbSearchModel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ModelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PersonInLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CycleTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarmQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CharModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseMacbox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvListModel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 148);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txbSearchModel);
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 70);
            this.panel3.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(686, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(93, 45);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "XÓA";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Gold;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(587, 19);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 45);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "SỬA";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAdd.Location = new System.Drawing.Point(488, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 45);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "THÊM";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Times New Roman", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Red;
            this.Label1.Location = new System.Drawing.Point(21, 9);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(355, 52);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Danh sách Model";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgrvListModel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 148);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 302);
            this.panel2.TabIndex = 1;
            // 
            // dgrvListModel
            // 
            this.dgrvListModel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrvListModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvListModel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ModelID,
            this.Id,
            this.PersonInLine,
            this.CycleTime,
            this.WarmQuantity,
            this.MinQuantity,
            this.CharModel,
            this.UseBarcode,
            this.UseMacbox});
            this.dgrvListModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrvListModel.Location = new System.Drawing.Point(0, 0);
            this.dgrvListModel.Name = "dgrvListModel";
            this.dgrvListModel.Size = new System.Drawing.Size(800, 302);
            this.dgrvListModel.TabIndex = 0;
            // 
            // txbSearchModel
            // 
            this.txbSearchModel.Location = new System.Drawing.Point(100, 32);
            this.txbSearchModel.Name = "txbSearchModel";
            this.txbSearchModel.Size = new System.Drawing.Size(331, 20);
            this.txbSearchModel.TabIndex = 3;
            this.txbSearchModel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txbSearchModel_PreviewKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Model";
            // 
            // ModelID
            // 
            this.ModelID.DataPropertyName = "ModelID";
            this.ModelID.HeaderText = "Model ID";
            this.ModelID.Name = "ModelID";
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // PersonInLine
            // 
            this.PersonInLine.DataPropertyName = "PersonInLine";
            this.PersonInLine.HeaderText = "Số người trên line";
            this.PersonInLine.Name = "PersonInLine";
            // 
            // CycleTime
            // 
            this.CycleTime.DataPropertyName = "Cycle";
            this.CycleTime.HeaderText = "Cycle Time";
            this.CycleTime.Name = "CycleTime";
            // 
            // WarmQuantity
            // 
            this.WarmQuantity.DataPropertyName = "WarnQuantity";
            this.WarmQuantity.HeaderText = "Cảnh báo số lượng";
            this.WarmQuantity.Name = "WarmQuantity";
            // 
            // MinQuantity
            // 
            this.MinQuantity.DataPropertyName = "MinQuantity";
            this.MinQuantity.HeaderText = "Cảnh báo số lượng bất thường";
            this.MinQuantity.Name = "MinQuantity";
            // 
            // CharModel
            // 
            this.CharModel.DataPropertyName = "CharModel";
            this.CharModel.HeaderText = "Kí tự Model";
            this.CharModel.Name = "CharModel";
            // 
            // UseBarcode
            // 
            this.UseBarcode.DataPropertyName = "UseBarcode";
            this.UseBarcode.HeaderText = "Sủ dụng barcode";
            this.UseBarcode.Name = "UseBarcode";
            // 
            // UseMacbox
            // 
            this.UseMacbox.DataPropertyName = "UseMacbox";
            this.UseMacbox.HeaderText = "Sử dụng Mac box";
            this.UseMacbox.Name = "UseMacbox";
            // 
            // ListModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ListModel";
            this.Text = "ListModel";
            this.Load += new System.EventHandler(this.ListModel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrvListModel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.DataGridView dgrvListModel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbSearchModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonInLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn CycleTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarmQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn CharModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseMacbox;
    }
}