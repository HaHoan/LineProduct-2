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
        private List<Model> list = new List<Model>();
    
        public ListModel()
        {
            InitializeComponent();
           // SetDrgvListModel();

        }

        private void SetDataForListModel()
        {
            try
            {
                dgrvListModel.DataSource = list;
                dgrvListModel.Refresh();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }


        }


        private void ListModel_Load(object sender, EventArgs e)
        {
            list = DataProvider.Instance.ModelQuantities.Select();
            SetDataForListModel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addModelForm = new AddModelForm();
            addModelForm.close = () =>
            {
                list = DataProvider.Instance.ModelQuantities.Select();
                SetDataForListModel();
            };
            addModelForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in dgrvListModel.SelectedRows)
                {
                    string ModelID = r.Cells[1].Value.ToString();
                    int result = DataProvider.Instance.ModelQuantities.Delete(ModelID);
                    if (result == 0)
                    {
                        MessageBox.Show("Có lỗi xảy ra!");
                        return;
                    }
                    list = DataProvider.Instance.ModelQuantities.Select();
                    SetDataForListModel();
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
                    string ModelID = r.Cells[1].Value.ToString();
                    var addModelForm = new AddModelForm(ModelID);
                    addModelForm.close = () =>
                    {
                        list = DataProvider.Instance.ModelQuantities.Select();
                        SetDataForListModel();
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

        private void txbSearchModel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string txtSearch = txbSearchModel.Text.Trim();
                if (!string.IsNullOrEmpty(txtSearch))
                {
                    var listFilter = list.Where(m => m.ModelID.ToUpper().Contains(txtSearch.ToUpper())).ToList();
                    if (listFilter != null)
                    {
                        dgrvListModel.DataSource = listFilter;
                        dgrvListModel.Refresh();
                    }
                    else
                    {
                        dgrvListModel.DataSource = new List<Model>();
                        dgrvListModel.Refresh();
                    }
                }
                else
                {
                    dgrvListModel.DataSource = list;
                    dgrvListModel.Refresh();
                }
               
                             
            }
        }
    }
}
