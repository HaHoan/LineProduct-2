using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using Line_Production.Database;
using Line_Production.Entities;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic

namespace Line_Production
{
    public partial class Control
    {
        // bien quy dinh duong dan file
        public static string PathApplication = Application.StartupPath;
        public static string PathSetup = PathApplication + @"\Setup";
        public static string PathPassrate = PathApplication + @"\Passrate";
        public static bool waitWipConfirm = true;
        public static string PathConfig = @"SOFTWARE\LINEPRODUCTION\Configs";
        public static string pathBackup = string.Empty;
        public static string pathWip = string.Empty;
        public static bool unlock = false;
        // cac bien cai dat den line san xuat---------------------------------------------------------------
        public static string IdLine = "";
        public static int NoPeople = 0; // bien lua gia tri so nguoi can cua 1 model
        public static bool BarcodeEnable = false; // bien cho phep model co su dung chuc nang barcode hay khong
        public static double CycleTimeModel = 30.6d;
        public static double CycleTimeActual = 0.0d;
        public static bool Shiftcheck = true; // true la ca dem, False la ca dem
        public static string Datecheck = "";
        public static bool StartProduct = false; // bien quy dinh ve line chay hay dung
        public static DateTime[] TimeLine = new DateTime[22]; // bien quy dinh khung gio 
        public static int TimePauseLine; // bien ghi gio tam dung line
        public static int CountProduct = 0; // dem san luong cua line model
        public static int[] CountProductPerHour = new int[22]; // dem san luong cua tung gio
        public static string[] notePerHour = new string[11];
        public static int ProductPlan = 0; // dem san luong tu dong theo cycle time cua model
        public static int ProductPlanBegin = 0; // dem san luong tu dong theo cycle time cua model
        public static int BalanceProduction = 0; // chenh lech san luong thuc te
        public static int BalanceAlarmSetup; // gia tri dat bat dau canh bao san luong
        public static int BalanceErrorSetup; // gia tri dat bat dau canh bao san luong
        public static bool BitPress = false; // bien xac nhan chong nhieu cho nut an tang qu cong com
        public static bool PauseProduct = false; // bien xac nhan trang thai line dang tam dung
        public static int TimeCountPlan = 0; // bien dem thoi gian cua cycle time theo line
        public static int TimeCycleActual = 0; // bien dem thoi gian cycle time thuc te ma line dang chay
        public static bool[] TimeUse = new bool[10]; // moc thoi gian ma line da chay va su dung
        public static int StatusLine = 0; // bien luu trang thai line, 0: chua hoat dong, 1: bat dau hoat dong, 2: Bao dong san luong  3:Bat thuong
        public static int TimeCycleActual2 = 0;
        public static string FilePassrate; // ten file passrate
        public static bool CheckServer = false;
        public static int PCBBOX = 10;
        public static string MacCurrent = "";
        public static int IDCount = 0;
        public static int IDCount_box = 0;
        public static bool ConfirmModel = false;
        // Public MacLe As Boolean = False
        // QuyetPham add 26.11
        public static string pathConfirm = PathApplication + @"\Confirm";
        public static string STATION = "";
        public static string STATION_BEFORE = "";
        // ///////////////////////////////////////////////////////////////////////
        public static string Box_curent = "";
        // //////////////////////////////////////////////////////////////////////////////////
        // cac bien lien quan den may chuc nang--------------------------------------------------------------
        public static string PathModelCurrent = ""; // duong dan file cai dat model hien tai
                                                    // Public PathLogMachine As String = "" ' duong dan ma log may chuc nang sinh ra khi can su dung 
                                                    // Public PathLogWip As String = "" ' duong dan sinh file log cho WIP
                                                    // Public NoLinePass As Integer = 3 ' dong co ki tu pass khi may chuc nang sinh log
                                                    // Public KiHieuPass As String = "P" ' ki tu thong bao la pass trong log
                                                    // Public SetProcess As String = "ICT_MUR" ' cong doan tren WIP
                                                    // Public setfilenamereport As String ' cai dat ten lien quan den file name của file report may chuc nang
        public static int CountLabel;
        public static string ModelRev = "";
        public static int ModelRevPosition = 1;
        public static string ModelCurrent = "";
        public static string Idcode = "";
        public static short IdCodeLenght = 0;
        public static bool confirmCode_emp = false;

        // cac bien lien quan den com port
        public static string ComControl = "COM7";
        public static int SetBaudRateComControl = 9600;
        public static int SetDataBitsComControl = 8;
        public static int SetStopBitsComControl = 1;
        public static string SetParityComControl = "None";
        public static string ComPress = "COM6";
        public static int SetBaudRateComPress = 9600;
        public static int SetDataBitsComPress = 8;
        public static int SetStopBitsComPress = 1;
        public static string SetParityComPress = "None";
        public static string ArraySend = "S00000000000000B";

        public bool Init()
        {
            try
            {
                IdLine = Common.GetValueRegistryKey(PathConfig, RegistryKeys.id);
                STATION = Common.GetValueRegistryKey(PathConfig, RegistryKeys.station);
                pathBackup = Path.Combine(Common.GetValueRegistryKey(PathConfig, RegistryKeys.pathWip), "backup", DateTime.Now.ToString("yyyyMMdd"));
                pathWip = Common.GetValueRegistryKey(PathConfig, RegistryKeys.pathWip);

                if (!Directory.Exists(pathBackup))
                    Directory.CreateDirectory(pathBackup);
                if (!Directory.Exists(Path.Combine(pathBackup, "OK")))
                    Directory.CreateDirectory(Path.Combine(pathBackup, "OK"));
                if (!Directory.Exists(Path.Combine(pathBackup, "NG")))
                    Directory.CreateDirectory(Path.Combine(pathBackup, "NG"));
               
                txtLine.Text = Common.GetValueRegistryKey(PathConfig, RegistryKeys.id);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return false;
            }

            return true;
        }

        public static bool SaveInit()
        {
            if (Common.GetValueRegistryKey(PathConfig, RegistryKeys.id) is null)
            {
                Common.WriteRegistry(PathConfig, RegistryKeys.id, "CA-Default");
                Common.WriteRegistry(PathConfig, RegistryKeys.pathWip, @"C:\LOGPROCESS");
                Common.WriteRegistry(PathConfig, RegistryKeys.station, "VI2_CAN");
                Common.WriteRegistry(PathConfig, RegistryKeys.useWip, true.ToString());
                Common.WriteRegistry(PathConfig, RegistryKeys.LinkWip, true.ToString());
                string[] listCOM = SerialPort.GetPortNames();
                if (listCOM != null && listCOM.Length > 0)
                {
                    Common.WriteRegistry(PathConfig, RegistryKeys.COM, listCOM[0]);
                }
                return true;
            }

            return false;
        }

        public bool CheckModelList()
        {
            var list = DataProvider.Instance.ModelQuantities.Select();
            foreach (var model in list)
            {
                cbbModel.Items.Add(model.ModelID);
            }

            return true;

        }

        public bool LoadModelCurrent(string ModelLoad)
        {
            string Strcheck = ModelLoad;
            PathModelCurrent = "";
            if (Strcheck.Length != 0)
            {
                Model model = DataProvider.Instance.ModelQuantities.Select(Strcheck);
                try
                {
                    NoPeople = model.PersonInLine;
                    CycleTimeModel = model.Cycle;
                    BarcodeEnable = model.UseBarcode;
                    BalanceAlarmSetup = (int)model.WarnQuantity;
                    BalanceErrorSetup = (int)model.MinQuantity;
                    ModelRev = model.CharModel;
                    ConfirmModel = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }

                return true;

            }
            else
            {
                return false;
            }
        }

        public static bool CheckCaSX()
        {
            if (DateAndTime.Now.Hour >= 8 & DateAndTime.Now.Hour <= 19)
            {
                Shiftcheck = true;
                string PathTimeLine = PathSetup + @"\Setup Time Line Day.ini";
                string ReadTime = ReadTextFile(PathTimeLine, 2);
                TimeLine[1] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[2] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 4);
                TimeLine[3] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[4] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 6);
                TimeLine[5] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[6] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 8);
                TimeLine[7] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[8] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 10);
                TimeLine[9] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[10] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 12);
                TimeLine[11] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[12] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 14);
                TimeLine[13] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[14] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 16);
                TimeLine[15] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[16] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 18);
                TimeLine[17] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[18] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 20);
                TimeLine[19] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[20] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
            }
            else
            {
                Shiftcheck = false;
                string PathTimeLine = PathSetup + @"\Setup Time Line Night.ini";
                string ReadTime = ReadTextFile(PathTimeLine, 2);
                TimeLine[1] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[2] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 4);
                TimeLine[3] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[4] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 6);
                TimeLine[5] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[6] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 8);
                TimeLine[7] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[8] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 10);
                TimeLine[9] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[10] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 12);
                TimeLine[11] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[12] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 14);
                TimeLine[13] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[14] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 16);
                TimeLine[15] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[16] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 18);
                TimeLine[17] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[18] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
                ReadTime = ReadTextFile(PathTimeLine, 20);
                TimeLine[19] = Convert.ToDateTime(Strings.Mid(ReadTime, 1, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) - 1));
                TimeLine[20] = Convert.ToDateTime(Strings.Mid(ReadTime, Strings.InStr(1, ReadTime, ",", CompareMethod.Text) + 1, ReadTime.Length));
            }

            return Shiftcheck;
        }

        public static double CalTimeWork(DateTime t1, DateTime t2)
        {
            double kq;
            var span = t2.Subtract(t1);
            kq = span.Hours * 3600 + span.Minutes * 60 + span.Seconds;
            return kq;
        }
    }
}


