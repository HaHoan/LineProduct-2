using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Line_Production.Database;
using Line_Production.Entities;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic
namespace Line_Production
{
    public partial class Control : Form
    {
        private System.Timers.Timer t = new System.Timers.Timer();
        private System.Windows.Forms.Timer t2 = new System.Windows.Forms.Timer();
        public Control()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        private PVSReference.PVSWebServiceSoapClient pvsservice;
        private USAPReference.USAPWebServiceSoapClient usapservice = new USAPReference.USAPWebServiceSoapClient();
        private LineProductWebServiceReference.LineProductRealtimeWebServiceSoapClient _lineproduct_service = new LineProductWebServiceReference.LineProductRealtimeWebServiceSoapClient();
        private LineProductWebServiceReference.tbl_Product_RealtimeEntity _entity = new LineProductWebServiceReference.tbl_Product_RealtimeEntity();
        private DateTime time_scanBarcode;
        private int bien_dem = 0;
        private bool useWip = true;
        private int _index = 0;
        private int _counter = 0;

        public void FormatNgayCasx()
        {
            CheckCaSX();
            if (Shiftcheck == true)
            {
                Datecheck = DateAndTime.Now.Date.ToString("dd-MM-yyyy");
            }
            else if (DateAndTime.Now.Hour >= 0 & DateAndTime.Now.Hour <= 7)
            {
                if (DateAndTime.Now.Day > 1)
                {
                    Datecheck = (DateAndTime.Now.Day - 1).ToString().PadLeft(2, '0') + "-" + DateAndTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateAndTime.Now.Year;
                }
                else
                {
                    switch (DateAndTime.Now.Month - 1)
                    {
                        case 1:
                            {
                                Datecheck = DateAndTime.Now.Year - 1 + "1231";
                                break;
                            }

                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            {
                                Datecheck = "31-" + (DateAndTime.Now.Month - 1).ToString().PadLeft(2, '0') + "-" + DateAndTime.Now.Year;
                                break;
                            }

                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            {
                                Datecheck = "30-" + (DateAndTime.Now.Month - 1).ToString().PadLeft(2, '0') + "-" + DateAndTime.Now.Year;
                                break;
                            }

                        case 2:
                            {
                                if (DateAndTime.Now.Year % 4 == 0 & DateAndTime.Now.Year % 100 != 0 | DateAndTime.Now.Year % 400 == 0)
                                {
                                    Datecheck = "29-02" + DateAndTime.Now.Year;
                                }
                                else
                                {
                                    Datecheck = "28-02" + DateAndTime.Now.Year;
                                }

                                break;
                            }
                    }
                }
            }
            else
            {
                Datecheck = DateAndTime.Now.Date.ToString("dd-MM-yyyy");
            }

            txtDate.Text = Datecheck;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileShare.Connect(FileShare.PATH, new System.Net.NetworkCredential(FileShare.USER, FileShare.PASSWORD));
            Loadsetting();
            // Me.Height = 885

        }

        public void SetupDisplay()
        {
            Common.SendToComport("S0000000000000R", (result) =>
            {
                lblState.Text = result;
            });
            Common.SendToComport("C", (result) =>
            {
                lblState.Text = result;
            });


            cbbModel.Enabled = true;
            cbbModel.Text = "";
            for (int index = 1; index <= 10; index++)
            {
                Table1.Controls.Find("TextTime" + index, true)[0].Text = "";
                Table1.Controls.Find("TextPlan" + index, true)[0].Text = "";
                Table1.Controls.Find("TextActual" + index, true)[0].Text = "";
                Table1.Controls.Find("TextBalance" + index, true)[0].Text = "";
                Table1.Controls.Find("TextComment" + index, true)[0].Text = "";
                notePerHour[index] = "";
            }

            lblComcontrol.Text = Common.GetValueRegistryKey(Control.PathConfig, "COM");
            chkNG.Enabled = false;
            lblVersion.Text = GetRunningVersion();
            TextCycleTimeCurrent.Text = "0";
            TextCycleTimeModel.Text = "0";
            txtShift.Clear();
            txtDate.Clear();
            PauseProduct = false;
            StartProduct = false;
            StatusLine = 0;
            CountProduct = 0;
            ProductPlan = 0;
            TimeCycleActual = 0;
            LabelPCBA.Text = "0";
            LabelSoThung.Text = "0";
            LabelPCS1BOX.Text = "0";
            IDCount = 0;
            IDCount_box = 0;
            MacCurrent = "";
            for (int index = 1; index <= 9; index++)
                CountProductPerHour[index] = 0;
            NoPeople = 0;
            ModelCurrent = "";
            Shape2.Visible = false;
            Shape1.Visible = false;
            Shape3.Visible = false;
            LabelShapeOnline.Visible = false;
            LabelShapeOffLine.Visible = false;
            LabelShapeError.Visible = false;
            Timer1.Enabled = true;
            TextMacBox.Text = "";
            txtSerial.Text = "";
            TextMacBox.Enabled = false;
            txtSerial.Enabled = false;
            TextCycleTimeCurrent.Text = "0";
            txtPeople.Text = "0";
            txtPlan.Text = "0";
            txtActual.Text = "0";
            TextBalance.Text = "0";
            CountProduct = 0;
            lblQuantity.Text = CountProduct.ToString();
            txtShift.Text = "";
            BtStart.Enabled = false;
            BtStop.Enabled = false;
            BtStart.Text = "Bắt đầu";
            BtStart.Image = Properties.Resources.play;
            try
            {
                useWip = bool.Parse(Common.GetValueRegistryKey(PathConfig, "useWip"));
            }
            catch { }

            // quyetpham add 16/9
            CheckBox1.Enabled = false;
            txtConfirm.Text = "";
            txtConfirm.Enabled = false;
            chkOK.Checked = true;
        }

        private void Loadsetting()
        {

            if (CheckModelList() == true)
            {
                SaveInit();
                Init();
                SetupDisplay();
            }
            else
            {
                MessageBox.Show("Setup List Model Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

        }

        public void LoadProduction()
        {
            string line = Common.GetValueRegistryKey(PathConfig, RegistryKeys.id);
            PassRate passRate = DataProvider.Instance.PassRates.GetPassRate(line, cbbModel.Text, Datecheck + "_" + Shiftcheck);
            if (passRate != null)
            {
                ProductPlanBegin = passRate.ProductPlan;
                ProductPlan = passRate.ProductPlan;
                CountProduct = passRate.Actual;
                IDCount = 0;
                IDCount_box = 0;
                Box_curent = "";
                TimeCycleActual = (int)passRate.TimeCycleActual;
                for (int index = 0; index < 10; index++)
                {
                    CountProductPerHour[index + 1] = int.Parse(passRate.TimeValue[index]);
                    notePerHour[index + 1] = "";
                }
            }
            else
            {
                for (int index = 1; index <= 10; index++)
                {
                    CountProductPerHour[index] = 0;
                    notePerHour[index] = "";
                }
                PassRate p = new PassRate();
                p.ProductionID = cbbModel.Text;
                p.Line = line;
                p.Time = Datecheck + "_" + Shiftcheck;
                p.ProductPlan = ProductPlan;
                p.Actual = CountProduct;
                p.IDCount = IDCount;
                p.IDCountBox = IDCount_box;
                p.BoxCurrent = Box_curent;
                p.TimeCycleActual = TimeCycleActual;
                p.TimeValue = new string[10];
                for (int index = 0; index < 10; index++)
                {
                    p.TimeValue[index] = "0";
                }
                DataProvider.Instance.PassRates.Insert(p);
            }

        }

        public void RecordProduction()
        {
            if (ModelCurrent != "")
            {
                string line = Common.GetValueRegistryKey(PathConfig, RegistryKeys.id);
                PassRate passRate = DataProvider.Instance.PassRates.GetPassRate(line, cbbModel.Text, Datecheck + "_" + Shiftcheck);
                if (passRate != null)
                {
                    passRate.ProductPlan = ProductPlan;
                    passRate.Actual = CountProduct;
                    passRate.IDCount = IDCount;
                    passRate.IDCountBox = IDCount_box;
                    passRate.BoxCurrent = Box_curent;
                    passRate.TimeCycleActual = TimeCycleActual;
                    for (int index = 0; index < 10; index++)
                    {
                        passRate.TimeValue[index] = CountProductPerHour[index + 1].ToString();
                    }
                    DataProvider.Instance.PassRates.Update(passRate);
                }
                else
                {
                    PassRate p = new PassRate();
                    p.ProductionID = cbbModel.Text;
                    p.Line = line;
                    p.Time = Datecheck + "_" + Shiftcheck;
                    p.ProductPlan = ProductPlan;
                    p.Actual = CountProduct;
                    p.IDCount = IDCount;
                    p.IDCountBox = IDCount_box;
                    p.BoxCurrent = Box_curent;
                    p.TimeCycleActual = TimeCycleActual;
                    p.TimeValue = new string[10];
                    for (int index = 0; index < 10; index++)
                    {
                        p.TimeValue[index] = "0";
                    }
                    DataProvider.Instance.PassRates.Insert(p);
                }
            }
        }

        private void ComboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbModel.Text != "")
            {
                Schedule schedule = new Schedule();
                schedule.ShowInTaskbar = false;
                schedule.updateTotal = new Action<string>(m =>
                {
                    lblTotal.Text = m;
                });
                schedule.ShowDialog();

                LabelPCBA.Text = "0";
                ModelCurrent = cbbModel.Text;
                if (LoadModelCurrent(ModelCurrent) == true)
                {
                    cbbModel.Enabled = false;
                    TextCycleTimeModel.Text = CycleTimeModel.ToString();
                    TextCycleTimeCurrent.Text = "";
                    txtPeople.Text = NoPeople.ToString();

                    FormatNgayCasx();
                    BtStart.Enabled = true;
                    BtStop.Enabled = true;
                    // quyetpham add 16/9

                    CheckBox1.Enabled = false;
                    CheckBox1.Checked = true;
                    LoadProduction();
                    txtPlan.Text = Math.Round(TimeCycleActual / CycleTimeModel, 0, MidpointRounding.AwayFromZero).ToString(); // ProductPlanBegin.ToString()
                    txtActual.Text = CountProduct.ToString();
                    TextBalance.Text = Math.Abs(CountProduct - ProductPlanBegin).ToString();
                    lblQuantity.Text = CountProduct.ToString();
                    TextCycleTimeCurrent.Text = Math.Round((double)TimeCycleActual / CountProduct, 1, MidpointRounding.AwayFromZero).ToString();
                    if (CheckCaSX() == true)
                    {
                        txtShift.Text = "Ca Ngày";
                        for (int index = 1; index <= 10; index++)
                        {
                            Table1.Controls.Find("TextComment" + index, true)[0].Text = notePerHour[index];
                            Table1.Controls.Find("TextTime" + index, true)[0].Text = TimeLine[1 + 2 * (index - 1)].Hour + ":" + TimeLine[1 + 2 * (index - 1)].Minute + ":" + TimeLine[1 + 2 * (index - 1)].Second + ">" + TimeLine[2 + 2 * (index - 1)].Hour + ":" + TimeLine[2 + 2 * (index - 1)].Minute + ":" + TimeLine[2 + 2 * (index - 1)].Second;
                            Table1.Controls.Find("TextPlan" + index, true)[0].Text = Strings.Format(((TimeLine[2 + 2 * (index - 1)].Hour - TimeLine[1 + 2 * (index - 1)].Hour) * 3600 + (TimeLine[2 + 2 * (index - 1)].Minute - TimeLine[1 + 2 * (index - 1)].Minute) * 60 + (TimeLine[2 + 2 * (index - 1)].Second - TimeLine[1 + 2 * (index - 1)].Second)) / CycleTimeModel, "0");
                            if (CountProductPerHour[index] > 0)
                            {
                                Table1.Controls.Find("TextActual" + index, true)[0].Text = CountProductPerHour[index].ToString();
                                Table1.Controls.Find("TextBalance" + index, true)[0].Text = (int.Parse(Table1.Controls.Find("TextActual" + index, true)[0].Text) - int.Parse(Table1.Controls.Find("TextPlan" + index, true)[0].Text)).ToString();
                            }
                        }
                    }
                    else
                    {
                        txtShift.Text = "Ca Đêm";
                        for (int index = 1; index <= 10; index++)
                        {
                            Table1.Controls.Find("TextComment" + index, true)[0].Text = notePerHour[index];
                            Table1.Controls.Find("TextTime" + index, true)[0].Text = TimeLine[1 + 2 * (index - 1)].Hour + ":" + TimeLine[1 + 2 * (index - 1)].Minute + ":" + TimeLine[1 + 2 * (index - 1)].Second + ">" + TimeLine[2 + 2 * (index - 1)].Hour + ":" + TimeLine[2 + 2 * (index - 1)].Minute + ":" + TimeLine[2 + 2 * (index - 1)].Second;
                            Table1.Controls.Find("TextPlan" + index, true)[0].Text = Strings.Format(((TimeLine[2 + 2 * (index - 1)].Hour - TimeLine[1 + 2 * (index - 1)].Hour) * 3600 + (TimeLine[2 + 2 * (index - 1)].Minute - TimeLine[1 + 2 * (index - 1)].Minute) * 60 + (TimeLine[2 + 2 * (index - 1)].Second - TimeLine[1 + 2 * (index - 1)].Second)) / CycleTimeModel, "0");
                            if (CountProductPerHour[index] > 0)
                            {
                                Table1.Controls.Find("TextActual" + index, true)[0].Text = CountProductPerHour[index].ToString();
                                Table1.Controls.Find("TextBalance" + index, true)[0].Text = Strings.Format(((TimeLine[2 + 2 * (index - 1)].Hour - TimeLine[1 + 2 * (index - 1)].Hour) * 3600 + (TimeLine[2 + 2 * (index - 1)].Minute - TimeLine[1 + 2 * (index - 1)].Minute) * 60 + (TimeLine[2 + 2 * (index - 1)].Second - TimeLine[1 + 2 * (index - 1)].Second)) / CycleTimeModel, "0");
                            }
                        }
                    }
                }
            }
        }

        public void ShowStatus(int value, bool VisibleShow)
        {
            switch (value)
            {
                case 0:
                    {

                        // LabelStatus.Text = "Tình trạng Line:       "
                        LabelShapeOnline.Visible = false;
                        LabelShapeOffLine.Visible = false;
                        LabelShapeError.Visible = false;
                        Shape1.Visible = false;
                        Shape2.Visible = false;
                        Shape3.Visible = false;
                        break;
                    }

                case 1:
                    {
                        // LabelStatus.Text = "Tình trạng Line:Online"
                        LabelShapeOnline.Visible = VisibleShow;
                        LabelShapeOffLine.Visible = false;
                        LabelShapeError.Visible = false;
                        Shape1.Visible = VisibleShow;
                        if (VisibleShow == false)
                        {
                            Common.SendToComport("R", result =>
                             {
                                 lblState.Text = result;
                             });
                        }
                        else
                        {
                            Common.SendToComport("X", result =>
                            {
                                lblState.Text = result;
                            });
                        }
                        Shape2.Visible = false;
                        Shape3.Visible = false;
                        break;
                    }

                case 2:
                    {
                        // LabelStatus.Text = "Tình trạng Line: Bao Dong"
                        LabelShapeOnline.Visible = false;
                        LabelShapeOffLine.Visible = VisibleShow;
                        LabelShapeError.Visible = false;
                        Shape1.Visible = false;
                        Shape2.Visible = VisibleShow;
                        if (VisibleShow == false)
                        {
                            Common.SendToComport("R", result =>
                            {
                                lblState.Text = result;
                            });
                        }
                        else
                        {
                            Common.SendToComport("V", result =>
                            {
                                lblState.Text = result;
                            });
                        }

                        Shape3.Visible = false;
                        break;
                    }

                case 3:
                    {
                        // LabelStatus.Text = "Tình trạng Line: Bat Thuong"
                        LabelShapeOnline.Visible = false;
                        LabelShapeOffLine.Visible = false;
                        LabelShapeError.Visible = VisibleShow;
                        Shape1.Visible = false;
                        Shape2.Visible = false;
                        Shape3.Visible = VisibleShow;
                        if (VisibleShow == false)
                        {
                            Common.SendToComport("R", result => { lblState.Text = result; });

                        }
                        else
                        {
                            Common.SendToComport("D", result => { lblState.Text = result; });
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        private void BtStart_Click(object sender, EventArgs e)
        {
            cbbModel.Enabled = false;
            LabelShapeOnline.Visible = true;
            LabelShapeOffLine.Visible = false;
            LabelShapeError.Visible = false;
            Shape1.Visible = false;
            Shape2.Visible = false;
            Shape3.Visible = false;
            // quyetpham add 16/9
            CheckBox1.Enabled = true;
            chkNG.Enabled = true;
            if (BtStart.Text == "Bắt đầu")
            {
                PauseProduct = false;
                StartProduct = true;
                TextMacBox.Enabled = true;
                // GroupBox3.Controls("Shape" & StatusLine).Visible = True
                BtStart.Text = "Online";
                BtStart.Image = Properties.Resources.pause;
                int sumtime = DateAndTime.Now.Hour * 100 + DateAndTime.Now.Minute;
                if (StatusLine == 0)
                {
                    StatusLine = 1;
                    // LabelStatus.Text = "Tình trạng Line: Online"
                    ShowStatus(StatusLine, true);
                    for (int index = 1; index <= 20; index++)
                    {
                        if (index % 2 != 0 & sumtime >= TimeLine[index].Hour * 100 + TimeLine[index].Minute & sumtime <= TimeLine[index + 1].Hour * 100 + TimeLine[index + 1].Minute)
                        {
                            time_scanBarcode = DateAndTime.Now;
                            TimeLine[index] = new DateTime(DateAndTime.Now.Year, DateAndTime.Now.Month, DateAndTime.Now.Day, DateAndTime.Now.Hour, DateAndTime.Now.Minute, DateAndTime.Now.Second);
                            Table1.Controls.Find("TextTime" + (index / 2 + 1), true)[0].Text = TimeLine[index].Hour + ":" + TimeLine[index].Minute + ":" + TimeLine[index].Second + ">" + TimeLine[index + 1].Hour + ":" + TimeLine[index + 1].Minute + ":" + TimeLine[index + 1].Second;
                            Table1.Controls.Find("TextPlan" + (index / 2 + 1), true)[0].Text = Strings.Format(((TimeLine[index + 1].Hour - TimeLine[index].Hour) * 3600 + (TimeLine[index + 1].Minute - TimeLine[index].Minute) * 60 + (TimeLine[index + 1].Second - TimeLine[index].Second)) / CycleTimeModel + CountProductPerHour[index / 2 + 1], "0");
                            // TextPlan.Text = Table1.Controls("TextPlan" & (index \ 2) + 1).Text
                            // TextPlan.Text = ProductPlanBegin.ToString()
                            txtActual.Text = CountProduct.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    // LabelStatus.Text = "Tình trạng Line: Online"
                    PauseProduct = false;
                    StartProduct = true;
                    ShowStatus(StatusLine, true);
                    for (int index = 1; index <= 20; index++)
                    {
                        if (index % 2 != 0 & sumtime >= TimeLine[index].Hour * 100 + TimeLine[index].Minute & sumtime <= TimeLine[index + 1].Hour * 100 + TimeLine[index + 1].Minute)
                        {
                            Table1.Controls.Find("TextTime" + (index / 2 + 1), true)[0].Text = TimeLine[index].Hour + ":" + TimeLine[index].Minute + ":" + TimeLine[index].Second + ">" + TimeLine[index + 1].Hour + ":" + TimeLine[index + 1].Minute + ":" + TimeLine[index + 1].Second;
                            Table1.Controls.Find("TextPlan" + (index / 2 + 1), true)[0].Text = Strings.Format(((TimeLine[index + 1].Hour - TimeLine[index].Hour) * 3600 + (TimeLine[index + 1].Minute - TimeLine[index].Minute) * 60 + (TimeLine[index + 1].Second - TimeLine[index].Second)) / CycleTimeModel + CountProductPerHour[index / 2 + 1], "0");
                            break;
                        }
                    }

                    TimePauseLine = 0;
                }

                time_scanBarcode = DateAndTime.Now;
                ProductPlan = (int)Math.Round(TimeCycleActual / CycleTimeModel, 0, MidpointRounding.AwayFromZero);
                txtPlan.Text = ProductPlan.ToString();
            }
            TextMacBox.Enabled = true;
            TextMacBox.Focus();

        }

        private void BtStop_Click(object sender, EventArgs e)
        {
            var ett = new LineProductWebServiceReference.tbl_Product_RealtimeEntity()
            {
                CUSTOMER = Common.GetValueRegistryKey(Control.PathConfig, RegistryKeys.Customer),
                LINE_NO = IdLine,
                MODEL = cbbModel.Text,
                QTY_PLAN = ProductPlan,
                QTY_ACTUAL = CountProduct,
                UPDATE_TIME = DateTime.Now.Date,
                PEOPLE = NoPeople,
                CYRCLETIME_PLAN = (decimal)CycleTimeModel,
                CYRCLETIME_ACTUAL = (decimal)CycleTimeActual,
                DIFF = BalanceProduction,
                ALARM = StatusLine,
                STATUS = "STOP"
            };
            try
            {
                _lineproduct_service.UpdateRealtime(ett);

            }
            catch (Exception)
            {

            }
            // Repository.UpdatateData(entities)
            SetupDisplay();
        }


        public void IncreaseProduct()
        {
            if (StartProduct == true)
            {
                for (int i = 1; i <= 10; i++)
                    notePerHour[i] = Table1.Controls.Find("TextComment" + i, true)[0].Text;
                int sumtime = DateAndTime.Now.Hour * 100 + DateAndTime.Now.Minute;
                for (int index = 1; index <= 20; index++)
                {
                    if (index % 2 != 0 & sumtime >= TimeLine[index].Hour * 100 + TimeLine[index].Minute & sumtime <= TimeLine[index + 1].Hour * 100 + TimeLine[index + 1].Minute)
                    {
                        bien_dem = bien_dem + 1;
                        CountProductPerHour[index / 2 + 1] = CountProductPerHour[index / 2 + 1] + 1;
                        Table1.Controls.Find("TextActual" + (index / 2 + 1), true)[0].Text = CountProductPerHour[index / 2 + 1].ToString();
                        Table1.Controls.Find("TextBalance" + (index / 2 + 1), true)[0].Text = (CountProductPerHour[index / 2 + 1] - int.Parse(Table1.Controls.Find("TextPlan" + (index / 2 + 1), true)[0].Text)).ToString();
                        _index = (int)((index + 1) / 2d);
                        break;
                    }
                    else
                    {
                        bien_dem = 0;
                    }
                }

                if (bien_dem != 0)
                {
                    CountProduct = CountProduct + 1;
                    if (CountProduct == 1)
                    {
                        CycleTimeActual = (DateAndTime.Now - time_scanBarcode).TotalSeconds; // CalTimeWork(time_scanBarcode, Now) 
                    }
                    else
                    {
                        TimeCycleActual = (int)(TimeCycleActual + (DateAndTime.Now - time_scanBarcode).TotalSeconds); // CalTimeWork(time_scanBarcode, Now)
                        CycleTimeActual = Math.Round((double)TimeCycleActual / CountProduct, 1, MidpointRounding.AwayFromZero);
                    }

                    time_scanBarcode = DateAndTime.Now;
                    TextCycleTimeCurrent.Text = CycleTimeActual.ToString();
                    lblQuantity.Text = CountProduct.ToString();
                    ProductPlan = (int)Math.Round((double)TimeCycleActual / CycleTimeModel, 0, MidpointRounding.AwayFromZero);
                    txtPlan.Text = ProductPlan.ToString();
                    time_scanBarcode = DateAndTime.Now;
                }

                RecordProduction();
            }
        }

        public void ReduceProduct()
        {
            if (StartProduct == true & CountProduct > 0)
            {
                int sumtime = DateAndTime.Now.Hour * 100 + DateAndTime.Now.Minute;
                for (int index = 1; index <= 20; index++)
                {
                    if (index % 2 != 0 & sumtime >= TimeLine[index].Hour * 100 + TimeLine[index].Minute & sumtime <= TimeLine[index + 1].Hour * 100 + TimeLine[index + 1].Minute)
                    {
                        CountProduct = CountProduct - 1;
                        lblQuantity.Text = CountProduct.ToString();
                        CountProductPerHour[index / 2 + 1] = CountProductPerHour[index / 2 + 1] - 1;
                        Table1.Controls.Find("TextActual" + (index / 2 + 1), true)[0].Text = CountProductPerHour[index / 2 + 1].ToString();
                        Table1.Controls.Find("TextBalance" + (index / 2 + 1), true)[0].Text = (CountProductPerHour[index / 2 + 1] - int.Parse(Table1.Controls.Find("TextPlan" + (index / 2 + 1), true)[0].Text)).ToString();
                        break;
                    }
                }

                RecordProduction();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            LabelTimeDate.Text = DateAndTime.Now.ToString("HH:mm:ss  dd/MM/yyyy");
            _counter++;
            if (BtStart.Text != "Bắt đầu")
            {
                int sumtime = DateAndTime.Now.Hour * 100 + DateAndTime.Now.Minute;
                int indexCurrent = 0;
                for (int index = 1; index <= 20; index++)
                {
                    if (index % 2 != 0)
                    {
                        if (sumtime >= TimeLine[index].Hour * 100 + TimeLine[index].Minute & sumtime <= TimeLine[index + 1].Hour * 100 + TimeLine[index + 1].Minute)
                        {
                            bien_dem = bien_dem + 1;
                            indexCurrent = index/2 + 1;
                            PauseProduct = false;
                            break;
                        }
                        else
                        {
                            bien_dem = 0;
                        }
                    }
                    else if (index == 20)
                    {
                        PauseProduct = true;
                    }
                }

                if (bien_dem == 0)
                {
                    time_scanBarcode = DateAndTime.Now;
                  
                }


                BalanceProduction = CountProduct - ProductPlan;
                
                var perBalanceProduction = ProductPlan == 0 ? 0 : BalanceProduction * 100 / ProductPlan;
                if (perBalanceProduction < BalanceErrorSetup)
                {
                    StatusLine = 3;
                    ShowStatus(StatusLine, true);
                }
                else if (perBalanceProduction < BalanceAlarmSetup)
                {
                    StatusLine = 2;
                    ShowStatus(StatusLine, true);
                }
                else
                {
                    StatusLine = 1;
                    ShowStatus(StatusLine, true);
                }

                if (PauseProduct == true)
                {
                    Timer2.Enabled = true;
                }
                else
                {
                    Timer2.Enabled = false;
                }

                txtPlan.Text = ProductPlan.ToString();
                txtActual.Text = CountProduct.ToString();
                TextBalance.Text = BalanceProduction.ToString();
                int total = 0;
                try
                {
                    total = int.Parse(lblTotal.Text);
                }
                catch { }
                if (BalanceProduction < 0)
                {
                    if (Math.Abs(BalanceProduction) >= 1000)
                    {
                        ArraySend = "S-" + Strings.Format(999, "000") + Strings.Format(CountProduct, "0000") + Strings.Format(total, "0000") + Strings.Format(NoPeople, "00") + "*";
                    }
                    else
                    {
                        ArraySend = "S" + Strings.Format(BalanceProduction, "000") + Strings.Format(CountProduct, "0000") + Strings.Format(total, "0000") + Strings.Format(NoPeople, "00") + "*";
                    }
                }
                else
                {
                    ArraySend = "S+" + Strings.Format(BalanceProduction, "000") + Strings.Format(CountProduct, "0000") + Strings.Format(total, "0000") + Strings.Format(NoPeople, "00") + "*";
                }
                var entity = new Online()
                {
                    LineID = IdLine,
                    ModelID = cbbModel.Text,
                    Plan = ProductPlan,
                    Actual = CountProduct,
                    _Date = DateTime.Now.Date
                };
                var entities = new LineProductWebServiceReference.tbl_Product_RealtimeEntity()
                {
                    CUSTOMER = "",
                    LINE_NO = IdLine,
                    MODEL = cbbModel.Text,
                    QTY_PLAN = ProductPlan,
                    QTY_ACTUAL = CountProduct,
                    UPDATE_TIME = DateTime.Now.Date,
                    PEOPLE = NoPeople,
                    CYRCLETIME_PLAN = (decimal)CycleTimeModel,
                    CYRCLETIME_ACTUAL = (decimal)CycleTimeActual,
                    DIFF = BalanceProduction,
                    ALARM = StatusLine,
                    STATUS = "RUNNING",
                    NOTE = Table1.Controls.Find("TextComment" + indexCurrent, true)[0].Text
                };
                // Repository.UpdatateData(entities)
                if (_counter >= 60)
                {
                    _lineproduct_service.UpdateRealtime(entities);
                    _counter = 0;
                }
                Common.SendToComport(ArraySend, result => { lblState.Text = result; });

            }
        }

        private void BtIncrease_Click(object sender, EventArgs e)
        {
            IncreaseProduct();
        }

        private void BtReduce_Click(object sender, EventArgs e)
        {
            ReduceProduct();
        }

        private void Control_FormClosed(object sender, FormClosedEventArgs e)
        {
            RecordProduction();
            SetupDisplay();
            Application.Exit();
        }

        private void KiemTraTrenHondaLock(Action ChuaTonTai, Action DaTonTai)
        {
            // Check trên HondaLock
            if (DataProvider.Instance.HondaLocks.KiemTraBanMachDaBan(TextMacBox.Text.ToString(), txtSerial.Text.ToString()))
            {
                DaTonTai();
                NG_FORM NG_FORM = new NG_FORM();
                NG_FORM.Lb_inform_NG.Text = "Đã tồn tại bản mạch " + txtSerial.Text;
                NG_FORM.GroupBox3.Visible = false;
                NG_FORM.ShowDialog();
            }
            else
            {
                DataProvider.Instance.HondaLocks.Insert(new HondaLock()
                {
                    ProductionID = cbbModel.Text.ToString(),
                    BoxID = TextMacBox.Text,
                    BoardNo = txtSerial.Text,
                    UpdateTime = pvsservice.GetDateTime(),
                    Status = chkOK.Checked ? "1" : "0",
                    Update_Code = lblCode.Text,
                    Update_Name = lUser.Text
                });

                ChuaTonTai();
            }
        }

        /*
        * db : 172.28.10.9 / UMC3000 / BCLBFLM 
        */
        private int LaySoThung(string mathung)
        {
            var bc = usapservice.GetByBcNo(TextMacBox.Text.ToString());
            if (bc != null)
            {
                try
                {
                    return (int)bc.OS_QTY;
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        private void TextMacBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                MacCurrent = TextMacBox.Text.Trim().TrimEnd().TrimStart();
                IDCount = DataProvider.Instance.HondaLocks.SoLuongBanMachDaDem(MacCurrent, cbbModel.Text);
                PCBBOX = LaySoThung(TextMacBox.Text);
                if (PCBBOX < 0)
                {
                    NG_FORM NG_FORM = new NG_FORM();
                    NG_FORM.Show();
                    NG_FORM.Lb_inform_NG.Text = "Mã thùng không tồn tại";
                    NG_FORM.GroupBox3.Visible = false;
                    NG_FORM.GroupBox3.Enabled = false;
                    NG_FORM.ControlBox = true;
                    TextMacBox.SelectAll();
                    return;
                }
                else if (IDCount == PCBBOX)
                {
                    NG_FORM NG_FORM = new NG_FORM();
                    NG_FORM.Show();
                    NG_FORM.Lb_inform_NG.Text = "Thùng đã kiểm tra xong";
                    NG_FORM.GroupBox3.Visible = false;
                    NG_FORM.GroupBox3.Enabled = false;
                    NG_FORM.ControlBox = true;
                    TextMacBox.SelectAll();
                    return;
                }
                else
                {
                    LabelPCS1BOX.Text = PCBBOX.ToString();
                }

                TextMacBox.Enabled = false;
                if (ConfirmModel)
                {
                    txtConfirm.Enabled = true;
                    txtConfirm.SelectAll();
                    txtConfirm.Focus();
                }
                else
                {
                    txtSerial.Enabled = true;
                    txtSerial.Focus();
                }

                CheckBox1.Enabled = false;
                LabelPCBA.Text = IDCount.ToString();
                //Box_curent = TextMacBox.Text;



            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            TimePauseLine = TimePauseLine + 1;
            if (TimePauseLine % 2 == 0)
            {
                ShowStatus(StatusLine, true);
                TimePauseLine = 0;
            }
            else
            {
                ShowStatus(StatusLine, false);
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == false)
            {
                TextMacBox.Enabled = false;
                txtSerial.Enabled = true;
                txtSerial.Focus();
                CheckBox1.Enabled = false;
            }
            else
            {
                // TextMacBox.Enabled = True
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        private void lblConfig_Click(object sender, EventArgs e)
        {
            frmConfig frmConfig = new frmConfig();
            frmConfig.updateAfterSetting = () => { lblComcontrol.Text = Common.GetValueRegistryKey(PathConfig, RegistryKeys.COM); };
            frmConfig.ShowDialog();
            useWip = bool.Parse(Common.GetValueRegistryKey(PathConfig, RegistryKeys.useWip));
            pathWip = Common.GetValueRegistryKey(PathConfig, RegistryKeys.pathWip);
            txtLine.Text = Common.GetValueRegistryKey(PathConfig, RegistryKeys.id);
            Init();
        }

        private void chkNG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNG.Checked)
            {
                chkOK.Checked = false;
                TextMacBox.Enabled = false;
                txtSerial.Enabled = true;
                txtSerial.Focus();
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            BtStop_Click(null, null);
            fmLogin frmLogin = new fmLogin();
            frmLogin.txtUsername.Clear();
            frmLogin.txtPassword.Clear();
            frmLogin.txtUsername.Select();
            frmLogin.Show();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (BtStart.Text != "Bắt đầu")
            {
                int sumtime = DateAndTime.Now.Hour * 100 + DateAndTime.Now.Minute;
                for (int index = 1; index <= 20; index++)
                {
                    if (index % 2 != 0)
                    {
                        var time = TimeLine[index];
                        if (sumtime >= TimeLine[index].Hour * 100 + TimeLine[index].Minute & sumtime <= TimeLine[index + 1].Hour * 100 + TimeLine[index + 1].Minute)
                        {
                            bien_dem = bien_dem + 1;
                            break;
                        }
                        else
                        {
                            bien_dem = 0;
                        }
                    }
                    else
                    {
                    }
                }

                if (bien_dem == 0)
                {
                    time_scanBarcode = DateAndTime.Now;
                }
                else
                {
                    TimeCycleActual = (int)(TimeCycleActual + (DateAndTime.Now - time_scanBarcode).TotalSeconds);
                    time_scanBarcode = DateAndTime.Now;
                    ProductPlan = (int)Math.Round(TimeCycleActual / CycleTimeModel, 0, MidpointRounding.AwayFromZero);
                    txtPlan.Text = ProductPlan.ToString();
                    RecordProduction();
                }
            }
        }

        private void txtConfirm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtConfirm.Text == cbbModel.Text)
                {
                    txtSerial.Enabled = true;
                    txtSerial.Focus();
                    txtConfirm.Enabled = false;
                }
                else
                {
                    NG_FORM NG_FORM = new NG_FORM();
                    NG_FORM.Show();
                    NG_FORM.Lb_inform_NG.Text = "Sai Model";
                    NG_FORM.GroupBox3.Visible = false;
                    NG_FORM.GroupBox3.Enabled = false;
                    NG_FORM.ControlBox = true;
                    txtConfirm.SelectAll();
                }
            }
        }

        private void chkOK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOK.Checked)
            {
                chkNG.Checked = false;
                TextMacBox.Enabled = false;
                txtSerial.Enabled = true;
                txtSerial.Focus();
            }
        }

        private void txtSerial_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Repository = New Repository
            pvsservice = new PVSReference.PVSWebServiceSoapClient();
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtSerial.Text))
            {
                if (Strings.Mid(txtSerial.Text, ModelRevPosition, ModelRev.Length) == ModelRev)
                {
                    if (PauseProduct == false)
                    {
                        try
                        {
                            var orderItem = pvsservice.GetWorkOrderItemByBoardNo(txtSerial.Text.Trim());
                            var orderNo = pvsservice.GetWorkOrdersByOrderNo(orderItem.ORDER_NO);
                            string customer = "";
                            if(CUSTOMER.CUSTOMERS.TryGetValue(orderNo.CUSTOMER_ID, out customer))
                            {
                                Common.WriteRegistry(PathConfig, RegistryKeys.Customer,customer);
                            }

                        }
                        catch
                        {
                            MessageBox.Show("Không tồn tại serial trên Wip");
                        }

                        if (chkNG.Checked)
                        {
                            var content = new StringBuilder();
                            string sTime = pvsservice.GetDateTime().ToString("yyMMddHHmmss");
                            content.AppendLine(string.Join("|", cbbModel.Text, txtSerial.Text, sTime, State.F.ToString(), STATION));
                            Common.WriteLog(Path.Combine(pathWip, $"{sTime}_{txtSerial.Text.Trim()}.txt"), content);
                            Common.WriteLog(Path.Combine(pathBackup, "NG", $"{sTime}_{txtSerial.Text.Trim()}.txt"), content);

                        }
                        /* TODO ERROR: Skipped RegionDirectiveTrivia */
                        else if (bool.Parse(Common.GetValueRegistryKey(PathConfig, RegistryKeys.LinkWip)))
                        {
                            try
                            {
                                string nameSoft = Common.FindApplication("Board Inspector");
                                int wipHandle = 0;
                                wipHandle = NativeWin32.FindWindow(null, nameSoft);
                                bool temp = Common.IsRunning(nameSoft);
                                if (!temp)
                                {
                                    MessageBox.Show("Chương trình Wip chưa bật", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else
                                {

                                    Common.ActiveProcess(nameSoft);
                                    Clipboard.SetText(txtSerial.Text.Trim());
                                    SendKeys.Send("^V");
                                    Thread.Sleep(170);
                                    SendKeys.Send("{ENTER}");
                                    Thread.Sleep(170);
                                    Common.ActiveProcess(this.Text);
                                    Thread.Sleep(220);
                                 
                                    bool IsWipSuccess = false;
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (pvsservice.GetWorkOrderItem(txtSerial.Text, STATION) != null)
                                        {
                                            IsWipSuccess = true;
                                            break;
                                        }
                                        Thread.Sleep(100);
                                    }
                                    //SendKeys.SendWait("%{TAB}");
                                    if (!IsWipSuccess)
                                    {
                                        txtSerial.SelectAll();
                                        txtSerial.Focus();
                                        return;
                                    }

                                    KiemTraTrenHondaLock(() =>
                                    {
                                        IDCount += 1;
                                        if (IDCount == PCBBOX)
                                        {
                                            if (CheckBox1.Checked)
                                            {
                                                TextMacBox.Enabled = true;
                                                TextMacBox.Focus();
                                                txtSerial.Enabled = false;
                                                TextMacBox.Clear();
                                            }
                                            else
                                            {
                                                txtSerial.Focus();
                                            }

                                            IDCount = 0;
                                            IDCount_box += 1;
                                            Box_curent = "";
                                        }

                                        LabelPCBA.Text = IDCount.ToString();
                                        LabelSoThung.Text = IDCount_box.ToString();
                                        IncreaseProduct();
                                        if (ConfirmModel & IDCount != 0)
                                        {
                                            txtConfirm.Enabled = true;
                                            txtConfirm.SelectAll();
                                            txtConfirm.Focus();
                                            txtSerial.Enabled = false;
                                        }

                                    }, () =>
                                    {
                                        txtSerial.Focus();
                                    });

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message.ToString());
                            }

                        }
                        else if (useWip == false)
                        {

                            KiemTraTrenHondaLock(() =>
                            {
                                IDCount += 1;
                                if (IDCount == PCBBOX)
                                {
                                    if (CheckBox1.Checked)
                                    {
                                        TextMacBox.Enabled = true;
                                        TextMacBox.Focus();
                                        txtSerial.Enabled = false;
                                        TextMacBox.Clear();
                                    }
                                    else
                                    {
                                        txtSerial.Focus();
                                    }

                                    IDCount = 0;
                                    IDCount_box += 1;
                                    Box_curent = "";
                                }

                                LabelPCBA.Text = IDCount.ToString();
                                LabelSoThung.Text = IDCount_box.ToString();
                                IncreaseProduct();
                                if (ConfirmModel & IDCount != 0)
                                {
                                    txtConfirm.Enabled = true;
                                    txtConfirm.SelectAll();
                                    txtConfirm.Focus();
                                    txtSerial.Enabled = false;
                                }
                            }, () => { });

                            //}
                        }
                        else
                        {
                            try
                            {
                                var contentWip = new StringBuilder();
                                string sTime = pvsservice.GetDateTime().ToString("yyMMddHHmmss");
                                contentWip.AppendLine(string.Join("|", cbbModel.Text, txtSerial.Text, sTime, State.P.ToString(), STATION));
                                Common.WriteLog(Path.Combine(pathWip, $"{sTime}_{txtSerial.Text.Trim()}.txt"), contentWip);
                                Common.WriteLog(Path.Combine(pathBackup, "OK", $"{sTime}_{txtSerial.Text.Trim()}.txt"), contentWip);

                                KiemTraTrenHondaLock(() =>
                                {
                                    /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
                                    IDCount += 1;
                                    if (IDCount == PCBBOX)
                                    {
                                        if (CheckBox1.Checked)
                                        {
                                            TextMacBox.Enabled = true;
                                            TextMacBox.Focus();
                                            txtSerial.Enabled = false;
                                            TextMacBox.Clear();
                                        }
                                        else
                                        {
                                            txtSerial.Focus();
                                        }

                                        IDCount = 0;
                                        IDCount_box += 1;
                                        Box_curent = "";
                                    }

                                    LabelPCBA.Text = IDCount.ToString();
                                    LabelSoThung.Text = IDCount_box.ToString();
                                    IncreaseProduct();
                                    if (ConfirmModel & IDCount != 0)
                                    {
                                        txtConfirm.Enabled = true;
                                        txtConfirm.SelectAll();
                                        txtConfirm.Focus();
                                        txtSerial.Enabled = false;
                                    }

                                }, () => { });

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Kết nối đến server thất bại !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
                        }

                    }
                    else
                    {
                        NG_FORM NG_FORM = new NG_FORM();
                        NG_FORM.Lb_inform_NG.Text = "Đang thời gian nghỉ!";
                        NG_FORM.ControlBox = true;
                        NG_FORM.GroupBox3.Visible = false;
                        NG_FORM.ShowDialog();
                    }
                }
                else
                {
                    NG_FORM NG_FORM = new NG_FORM();
                    NG_FORM.Lb_inform_NG.Text = txtSerial.Text + " sai ma quy dinh model: " + ModelRev;
                    NG_FORM.GroupBox3.Visible = false;
                    // NG_FORM.ControlBox = False
                    // NG_FORM.ShowInTaskbar = False
                    NG_FORM.ShowDialog();
                }
                txtSerial.SelectAll();
            }
        }

        private void lblListModel_Click(object sender, EventArgs e)
        {
            new ListModel().ShowDialog();
        }

        private void lblSettingTime_Click(object sender, EventArgs e)
        {
            var timeLine = new TimeDetailForm();
            timeLine.ShowDialog();
        }

        private void txtSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                var text = txtSearch.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    new ResultForm(text).ShowDialog();
                }

            }

        }
    }
}

