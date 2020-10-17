using Line_Production.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Line_Production
{
    public partial class frmConfig : Form
    {
        public Action updateAfterSetting;
        public frmConfig()
        {
            InitializeComponent();
            cbbCOM.DataSource = SerialPort.GetPortNames();
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            Common.WriteRegistry(Control.PathConfig, RegistryKeys.id, txtId.Text);
            if (!string.IsNullOrEmpty(txtId.Text))
                DataProvider.Instance.TimeLines.InsertLine(txtId.Text);
            Common.WriteRegistry(Control.PathConfig, RegistryKeys.useWip, chkWip.Checked.ToString());
            Common.WriteRegistry(Control.PathConfig, RegistryKeys.pathWip, txtLog.Text);
            Common.WriteRegistry(Control.PathConfig, RegistryKeys.station, txtStation.Text.Trim());
            Common.WriteRegistry(Control.PathConfig, RegistryKeys.COM, cbbCOM.Text.Trim());
            Common.WriteRegistry(Control.PathConfig, RegistryKeys.LinkWip, chkLinkWip.Checked.ToString());
            var confirm = MessageBox.Show("Save success!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (confirm == DialogResult.OK)
            {
                updateAfterSetting();
                Close();
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            txtId.Text = Common.GetValueRegistryKey(Control.PathConfig, RegistryKeys.id);
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

        private void btnTestCOM_Click(object sender, EventArgs e)
        {
            Common.SendToComport("test", result => { MessageBox.Show("Test COM connection : " + result); });
        }

    }
}
