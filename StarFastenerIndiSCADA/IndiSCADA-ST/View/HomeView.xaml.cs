using IndiSCADA_ST.ViewModel;
using IndiSCADAEntity.Entity;
using MahApps.Metro.Controls;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Configuration;

namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : ChromelessWindow
    {
        int checkInstanceFlag = 0;
        public HomeView()
        {
            try
            {
               //// DeviceCommunication.IOTConnection _IOTobj = new DeviceCommunication.IOTConnection();

                try
                {
                    UserActivityInsert("IndiSCADA-ST Started");
                    SleepMethods.PreventSleep();
                }
                catch (Exception ex)
                {
                    //ErrorLogger.LogError.ErrorLog("HomeView UserActivityInsert() when checkInstanceFlag == 0", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" exception" + ex.ToString());
                // ErrorLogger.LogError.ErrorLog("HomeView UserActivityInsert()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }


            InitializeComponent();
            try
            {
                IndiSCADAGlobalLibrary.AccessConfig.GetMySQLConnectionString = ConfigurationManager.ConnectionStrings["GetMySQLConnectionString"].ConnectionString;
            }
            catch (Exception ex)
            {
                // log.Error("HomeViewModel HomeViewModel() GetConnectionString", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            //SystemStatusView objAlarmAndEventView = new SystemStatusView(); objAlarmAndEventView.Show();
        }


        //private void checkExeInProcess()
        //{
        //    try
        //    {
        //        if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
        //        {
        //            checkInstanceFlag = 1;
        //        }
        //        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

        //        int countNum = 0;
        //        Process[] processes = Process.GetProcesses();

        //        foreach (Process p in processes)
        //        {
        //            if (p.ProcessName.ToString() == "IndiSCADA-ST (32 bit)" || p.ProcessName.ToString() == "IndiSCADA-ST.exe" || p.ProcessName.ToString() == "IndiSCADA-ST")//e277
        //            {
        //                countNum++;
        //                if (countNum > 1)
        //                {
        //                    p.Kill();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError.ErrorLog(" Error in HomeView checkExeInProcess() checkInstanceFlag value = "+ checkInstanceFlag, DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
        //    }
        //}


        private void UserActivityInsert(string Activity)
        {
            try
            {
                UserActivityEntity _insert = new UserActivityEntity();
                _insert.DateTimeCol = DateTime.Now;
                _insert.Activity = Activity;
                if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                {
                    _insert.UserName = "System";
                }
                else
                {
                    _insert.UserName = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                }
                IndiSCADABusinessLogic.UserActivityLogic.InsertUserActivity(_insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeView UserActivityInsert()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        private void HamburgerMenu_ItemInvoked(object sender, MahApps.Metro.Controls.HamburgerMenuItemInvokedEventArgs e)
        {
            try
            {
                var SelectedScreen = e.InvokedItem as HamburgerMenuGlyphItem;

                switch (SelectedScreen.Tag.ToString())
                {

                    case "Home":
                        {
                            break;
                        }
                    case "User Master":
                        {
                            NewUserView objReport = new NewUserView(); objReport.ShowDialog();
                            UserActivityInsert("Open User Master Screen");
                            break;
                        }
                    case "Configuration":
                        {
                            if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                            {
                                LoginView loginView = new LoginView();
                                loginView.ShowDialog();
                            }
                            else
                            {
                                ConfigurationView objConfiguration = new ConfigurationView(); objConfiguration.ShowDialog();
                                UserActivityInsert("Open Configuration Screen");
                            }
                            break;
                        }
                    case "IO Status":
                        {
                            IOStatusView objIOStatusView = new IOStatusView(); objIOStatusView.ShowDialog();
                            UserActivityInsert("Open IO Status Screen");
                            break;
                        }
                    case "Alarm And Event":
                        {
                            AlarmAndEventView objAlarmAndEventView = new AlarmAndEventView(); objAlarmAndEventView.ShowDialog();
                            UserActivityInsert("Open Alarm And Event Screen");
                            break;
                        }
                    case "Settings":
                        {
                            if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                            {
                                LoginView loginView = new LoginView();
                                loginView.ShowDialog();
                            }
                            else
                            {
                                SettingsView objSettingsVieww = new SettingsView(); objSettingsVieww.ShowDialog();
                                UserActivityInsert("Open Settings Screen");
                            }
                            break;
                        }
                    case "Tank Details":
                        {
                            TankDetailsView objTankDetailsView = new TankDetailsView(); objTankDetailsView.ShowDialog();
                            UserActivityInsert("Open Tank Details Screen");
                            break;
                        }
                    case "Alarm Time":
                        {
                            if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                            {
                                LoginView loginView = new LoginView();
                                loginView.ShowDialog();
                            }
                            else
                            {
                                AlarmTime objAlarmTime = new AlarmTime(); objAlarmTime.ShowDialog();
                                UserActivityInsert("Open Alarm Time Screen");
                            }
                            break;
                        }
                    case "Trends":
                        {
                            TrendView objTrendVieww = new TrendView(); objTrendVieww.ShowDialog();
                            UserActivityInsert("Open Trends Screen");
                            break;
                        }
                    case "Overview":
                        {
                            OverviewView objOverviewView = new OverviewView(); objOverviewView.ShowDialog();
                            UserActivityInsert("Open Overview Screen");
                            break;
                        }
                    case "Next Load":
                        {
                            NextLoadView objNextloadView = new NextLoadView(); objNextloadView.ShowDialog();
                            UserActivityInsert("Open Nextload entry Screen");
                            break;
                        }
                    case "Part Master":
                        {
                            if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                            {
                                LoginView loginView = new LoginView();
                                loginView.ShowDialog();
                            }
                            else
                            {
                                PartMasterView objPartMasterView = new PartMasterView(); objPartMasterView.ShowDialog();
                                UserActivityInsert("Open Part Master Screen");
                            }
                            break;
                        }
                    case "Reports":
                        {
                            string strReportPath = System.IO.Directory.GetCurrentDirectory() + @"\ReportEXE\ReportManager.exe";
                            Process p = new Process();
                            p.StartInfo = new ProcessStartInfo(strReportPath);
                            p.Start();
                            UserActivityInsert("Open Report Application Screen");
                            break;
                        }
                }//switch  
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeView.cs HamburgerMenu_ItemInvoked()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        private void UserLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                {
                    LoginView loginView = new LoginView();
                    loginView.ShowDialog();
                }
                else
                {
                    IndiSCADAGlobalLibrary.UserLoginDetails.UserName = null;
                    IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel = null;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeView.cs UserLogout_Click()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }


        //this code is to set pc in never sleep mode 
        #region   SleepMethods : set pc in never sleep mode 
        static class SleepMethods
        {
            public static void PreventSleep()
            {
                SetThreadExecutionState(ExecutionState.EsContinuous | ExecutionState.EsSystemRequired | ExecutionState.EsDisplayRequired);
            }

            public static void AllowSleep()
            {
                SetThreadExecutionState(ExecutionState.EsContinuous);
            }

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

            [FlagsAttribute]
            private enum ExecutionState : uint
            {
                EsAwaymodeRequired = 0x00000040,
                EsContinuous = 0x80000000,
                EsDisplayRequired = 0x00000002,
                EsSystemRequired = 0x00000001
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ChromelessWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure that you want to exit the application?", "Exiting", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Environment.Exit(0);
                }
                else if (messageBoxResult == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeView.cs ChromelessWindow_Closing()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        private void HamburgerMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                //ToolTip t = new ToolTip();
                // t.Content("Alarm");
                //t.(, "Save changes");

                //var SelectedScreen = e.InvokedItem as HamburgerMenuGlyphItem;



                //switch (SelectedScreen.Tag.ToString())
                //{

                //    case "Home":
                //        {
                //            ToolTip t = new ToolTip();
                //            t.(button1, "Save changes");
                //        }
                //    case "User Master":
                //        {
                //            NewUserView objReport = new NewUserView(); objReport.ShowDialog();
                //            UserActivityInsert("Open User Master Screen");
                //            break;
                //        }
                //}
            }
            catch (Exception ex)
            { }
        }


        #endregion


        private void _Window_Loaded(object sender, RoutedEventArgs e)
        {
            int countNum = 0;
            try
            {
                Process[] processes = Process.GetProcesses();

                foreach (Process _process in processes)
                {
                    if (_process.ProcessName.ToString() == "IndiSCADA-ST (32 bit)" || _process.ProcessName.ToString() == "IndiSCADA-ST.exe" || _process.ProcessName.ToString() == "IndiSCADA-ST")
                    {
                        countNum++;
                    }
                }

                if (countNum > 1)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("ReportManagr is already Open in Backgroud. Do you want to close that exe and open new exe of ReportManager?", "Exiting", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        processes = Process.GetProcesses();
                        foreach (Process _process in processes)
                        {
                            if (_process.ProcessName.ToString() == "IndiSCADA-ST (32 bit)" || _process.ProcessName.ToString() == "IndiSCADA-ST.exe" || _process.ProcessName.ToString() == "IndiSCADA-ST")
                            {
                                //
                                int nProcessID = Process.GetCurrentProcess().Id;
                                if (nProcessID != _process.Id)
                                {
                                    _process.Kill();
                                }
                            }
                        }
                    }
                    else if (messageBoxResult == MessageBoxResult.No)
                    {
                        processes = Process.GetProcesses();
                        foreach (Process _process in processes)
                        {
                            if (_process.ProcessName.ToString() == "IndiSCADA-ST (32 bit)" || _process.ProcessName.ToString() == "IndiSCADA-ST.exe" || _process.ProcessName.ToString() == "IndiSCADA-ST")
                            {
                                //
                                int nProcessID = Process.GetCurrentProcess().Id;
                                if (nProcessID != _process.Id)
                                {
                                    // Here we have to show process in front which is already running in background
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog(" Error in HomeView _Window_Loaded() ", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
    }
}
