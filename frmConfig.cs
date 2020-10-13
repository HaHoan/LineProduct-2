using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Line_Production
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
            cbbCOM.DataSource = SerialPort.GetPortNames();
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            Common.WriteRegistry(Control.PathConfig, "id", txtId.Text);
            Common.WriteRegistry(Control.PathConfig, "useWip", chkWip.Checked.ToString());
            Common.WriteRegistry(Control.PathConfig, "pathWip", txtLog.Text);
            Common.WriteRegistry(Control.PathConfig, "station", txtStation.Text.Trim());
            Common.WriteRegistry(Control.PathConfig, "COM", cbbCOM.Text.Trim());
            var confirm = MessageBox.Show("Save success!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (confirm == DialogResult.OK)
            {
                Close();
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            txtId.Text = Common.GetValueRegistryKey(Control.PathConfig, "id");
            try
            {
                bool chkWipValue = bool.Parse(Common.GetValueRegistryKey(Control.PathConfig, "useWip"));
                chkWip.Checked = chkWipValue;
            }
            catch { }

            txtLog.Text = Common.GetValueRegistryKey(Control.PathConfig, "pathWip");
            txtStation.Text = Common.GetValueRegistryKey(Control.PathConfig, "station");
            cbbCOM.Text = Common.GetValueRegistryKey(Control.PathConfig, "COM");

        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog choofdlog = new FolderBrowserDialog();

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                txtLog.Text = choofdlog.SelectedPath;
            }
        }

        private void btnTestComConnection_Click(object sender, EventArgs e)
        {
            
        }
    }
}
