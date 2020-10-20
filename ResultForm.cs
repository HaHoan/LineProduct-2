using Line_Production.Database;
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
    public partial class ResultForm : Form
    {
        public ResultForm(string text)
        {
            InitializeComponent();
            SetUpData(text);
        }

        private void SetUpData(string text)
        {
            var list = DataProvider.Instance.HondaLocks.SearchSerial(text);
            dgrvSearch.DataSource = list;
        }
    }
}
