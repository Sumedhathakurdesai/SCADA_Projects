using IndiSCADA_ST.ViewModel;
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

using System.Windows.Forms;
using System.Drawing;

namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for AlarmAndEventView.xaml
    /// </summary>
    public partial class AlarmAndEventView : ChromelessWindow
    {
        public AlarmAndEventView()
        {
            InitializeComponent();
            //this.DataContext = new AlarmAndEventViewModel();
        }
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    MainWindow mainWindow = new MainWindow();

        //    if (Screen.AllScreens.Length > 1)
        //    {
        //        Screen s2 = Screen.AllScreens[1];
        //        Rectangle r2 = s2.WorkingArea;
        //        mainWindow.Top = r2.Top;
        //        mainWindow.Left = r2.Left;
        //        mainWindow.Show();
        //    }

        //    else
        //    {
        //        Screen s1 = Screen.AllScreens[0];
        //        Rectangle r1 = s1.WorkingArea;
        //        mainWindow.Top = r1.Top;
        //        mainWindow.Left = r1.Left;
        //        mainWindow.Show();
        //    }

        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgvAlarmAndEvent.Print();
            }
            catch { }
        }
    }
}
