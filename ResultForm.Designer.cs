using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic

namespace Line_Production
{
    [DesignerGenerated()]
    public partial class ResultForm : System.Windows.Forms.Form
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
            this.dgrvSearch = new System.Windows.Forms.DataGridView();
            this._macBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrvSearch
            // 
            this.dgrvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._macBox,
            this._id});
            this.dgrvSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrvSearch.Location = new System.Drawing.Point(0, 0);
            this.dgrvSearch.Name = "dgrvSearch";
            this.dgrvSearch.Size = new System.Drawing.Size(841, 721);
            this.dgrvSearch.TabIndex = 0;
            // 
            // _macBox
            // 
            this._macBox.DataPropertyName = "Item1";
            this._macBox.HeaderText = "Mac box";
            this._macBox.Name = "_macBox";
            this._macBox.ReadOnly = true;
            this._macBox.Width = 300;
            // 
            // _id
            // 
            this._id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._id.DataPropertyName = "Item2";
            this._id.HeaderText = "Serial";
            this._id.Name = "_id";
            this._id.ReadOnly = true;
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 721);
            this.Controls.Add(this.dgrvSearch);
            this.Name = "ResultForm";
            this.Text = "Result";
            ((System.ComponentModel.ISupportInitialize)(this.dgrvSearch)).EndInit();
            this.ResumeLayout(false);

        }

        internal DataGridView dgrvSearch;
        private DataGridViewTextBoxColumn _macBox;
        private DataGridViewTextBoxColumn _id;
    }
}
