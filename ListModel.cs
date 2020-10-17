using Line_Production.Database;
using Line_Production.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Line_Production
{
    public partial class ListModel : Form
    {
        public ListModel()
        {
            InitializeComponent();
            SetDrgvListModel();

        }

        private void SetDataForListModel(List<Model> list)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Model model = list[i];
                    int rowId = dgrvListModel.Rows.Add();
                    DataGridViewRow row = dgrvListModel.Rows[rowId];
                    dgrvListModel.Rows[i].Cells[0].Value = model.ModelID;
                    dgrvListModel.Rows[i].Cells[1].Value = model.PersonInLine;
                    dgrvListModel.Rows[i].Cells[2].Value = model.Cycle;
                    dgrvListModel.Rows[i].Cells[3].Value = model.WarnQuantity;
                    dgrvListModel.Rows[i].Cells[4].Value = model.MinQuantity;
                    dgrvListModel.Rows[i].Cells[5].Value = model.CharModel;
                    dgrvListModel.Rows[i].Cells[6].Value = model.UseBarcode ? "Có" : "Không";
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }


        }

        private void SetDrgvListModel()
        {
            dgrvListModel.ColumnCount = 7;
            dgrvListModel.Columns[0].Name = "Model ID";
            dgrvListModel.Columns[1].Name = "Số người trên line";
            dgrvListModel.Columns[2].Name = "Cycle Time";
            dgrvListModel.Columns[3].Name = "Cảnh báo số lượng";
            dgrvListModel.Columns[4].Name = "Cảnh báo số lượng bất thường";
            dgrvListModel.Columns[5].Name = "Kí tự Model";
            dgrvListModel.Columns[6].Name = "Sử dụng Barcode";
        }

        private void ListModel_Load(object sender, EventArgs e)
        {
            SetDataForListModel(DataProvider.Instance.ModelQuantities.Select());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addModelForm = new AddModelForm();
            addModelForm.close = () =>
            {
                dgrvListModel.Rows.Clear();
                SetDataForListModel(DataProvider.Instance.ModelQuantities.Select());
            };
            addModelForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in dgrvListModel.SelectedRows)
                {
                    string ModelID = r.Cells[0].Value.ToString();
                    int result = DataProvider.Instance.ModelQuantities.Delete(ModelID);
                    if (result == 0)
                    {
                        MessageBox.Show("Có lỗi xảy ra!");
                        return;
                    }
                    dgrvListModel.Rows.Remove(r);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgrvListModel.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dgrvListModel.SelectedRows[0];
                    string ModelID = r.Cells[0].Value.ToString();
                    var addModelForm = new AddModelForm(ModelID);
                    addModelForm.close = () =>
                    {
                        dgrvListModel.Rows.Clear();
                        SetDataForListModel(DataProvider.Instance.ModelQuantities.Select());
                    };
                    addModelForm.ShowDialog();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
