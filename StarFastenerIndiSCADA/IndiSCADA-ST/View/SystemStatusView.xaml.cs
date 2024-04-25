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
namespace IndiSCADA_ST
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SystemStatusView : Window
    {
        public SystemStatusView()
        {
            InitializeComponent();
            try
            {
                Screen s = Screen.AllScreens[1];
               
                System.Drawing.Rectangle r = s.WorkingArea;
                this.Top = r.Top;
                this.Left = r.Left;

                this.Width = 1280; this.Height = 1000;
                //int bottom = r.Bottom;
                //int top = r.Top;
                //int left = r.Left;
                //int right = r.Right;

                //this.WindowState  = System.Windows.WindowState.Maximized  ;
                //2000
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("error while opening alarm event view ", "", ex.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
    }
}
