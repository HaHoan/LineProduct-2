using Line_Production.Database;
using Line_Production.Entities;
using Microsoft.VisualBasic;
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
    public partial class TimeDetailForm : Form
    {
        public TimeDetailForm()
        {
            InitializeComponent();
            cbbCaSX.SelectedIndex = (DateAndTime.Now.Hour >= 8 & DateAndTime.Now.Hour <= 19) ? CaSX.DAY : CaSX.NIGHT;
        }

        private void SettingListLine()
        {
            var list = DataProvider.Instance.TimeLines.SelectLine(cbbCaSX.SelectedIndex);
            cbbTimeLine.DataSource = list;
            cbbTimeLine.SelectedIndex = list.IndexOf(Common.GetValueRegistryKey(Control.PathConfig, RegistryKeys.id));
        }

        private void SettingTimeLines()
        {
            try
            {
                var list = DataProvider.Instance.TimeLines.Select(cbbTimeLine.Text, cbbCaSX.SelectedIndex);
                if (list == null || list.Count == 0)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        TextBox textBoxFrom = Controls["txbTimeFrom" + i.ToString()] as TextBox;
                        textBoxFrom.Text = "";
                        TextBox textBoxTo = Controls["txbTimeTo" + i.ToString()] as TextBox;
                        textBoxTo.Text = "";
                    }
                    return;
                }
                else
                {
                    foreach (TimeLine time in list)
                    {
                        TextBox textBoxFrom = Controls["txbTimeFrom" + time.TimeIndex.ToString()] as TextBox;
                        textBoxFrom.Text = time.TimeFrom;
                        TextBox textBoxTo = Controls["txbTimeTo" + time.TimeIndex.ToString()] as TextBox;
                        textBoxTo.Text = time.TimeTo;
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }


        private void cbbCaSX_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingListLine();
            SettingTimeLines();
        }

        private void cbbTimeLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingTimeLines();
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            string line = Common.GetValueRegistryKey(Control.PathConfig, RegistryKeys.id);
            int CaSX = cbbCaSX.SelectedIndex;
            bool isCompleteTextBox = true;
            for (int i = 1; i <= 10; i++)
            {
                TextBox textBoxFrom = Controls["txbTimeFrom" + i.ToString()] as TextBox;
                if (string.IsNullOrEmpty(textBoxFrom.Text))
                {
                    isCompleteTextBox = false;
                    break;
                }
                TextBox textBoxTo = Controls["txbTimeTo" + i.ToString()] as TextBox;

                if (string.IsNullOrEmpty(textBoxTo.Text))
                {
                    isCompleteTextBox = false;
                    break;
                }
            }
            if (string.IsNullOrEmpty(line) || CaSX < 0 || !isCompleteTextBox) return;
            for (int i = 1; i <= 10; i++)
            {
                TextBox textBoxFrom = Controls["txbTimeFrom" + i.ToString()] as TextBox;
                string from = textBoxFrom.Text;
                TextBox textBoxTo = Controls["txbTimeTo" + i.ToString()] as TextBox;
                string to = textBoxTo.Text;
                DataProvider.Instance.TimeLines.Insert(new TimeLine()
                {
                    TimeIndex = i,
                    TimeFrom = from,
                    TimeTo = to,
                    IdTimeLine = DataProvider.Instance.TimeLines.SelectIdLine(CaSX, line)
                });
            }
            var confirm = MessageBox.Show("Lưu thành công! Bạn cần khởi động lại để dùng timeline mới!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (confirm == DialogResult.OK)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }
    }
}
