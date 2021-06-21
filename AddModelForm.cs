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
    public partial class AddModelForm : Form
    {
        public Action close;
        private string ModelID;
        private int ID;
        public AddModelForm(string ModelID = null)
        {
            InitializeComponent();
            this.ModelID = ModelID;
            
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new Model()
                {
                    Id = ID,
                    ModelID = txbModelID.Text.ToString().Trim(),
                    PersonInLine = int.Parse(txbPersonInLine.Text.ToString()),
                    Cycle = float.Parse(txbCycle.Text.ToString()),
                    WarnQuantity = float.Parse(txbWarmQuatity.Text.ToString()),
                    MinQuantity = float.Parse(txbMnQuantity.Text.ToString()),
                    CharModel = txbRegex.Text.ToString().Trim(),
                    UseBarcode = ckbUseBarcode.Checked,
                    UseMacbox = cbUseMacbox.Checked
                };
                if(model.Id == 0)
                {
                    DataProvider.Instance.ModelQuantities.Insert(model);
                }else
                {
                    DataProvider.Instance.ModelQuantities.Update(model);
                }
                close();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                
            }
            
        }

        private void AddModelForm_Shown(object sender, EventArgs e)
        {
            if(ModelID == null)
            {
                return;
            }
            Model model = DataProvider.Instance.ModelQuantities.Select(ModelID);
            if(model != null)
            {
                ID = model.Id;
                txbModelID.Text = model.ModelID;
                txbPersonInLine.Text = model.PersonInLine.ToString();
                txbCycle.Text = model.Cycle.ToString();
                txbWarmQuatity.Text = model.WarnQuantity.ToString();
                txbMnQuantity.Text = model.MinQuantity.ToString();
                txbRegex.Text = model.CharModel;
                ckbUseBarcode.Checked = model.UseBarcode;
                cbUseMacbox.Checked = model.UseMacbox;
            }
            else
            {
                MessageBox.Show("Không tìm thấy model này");
            }
             

        }
    }
}
