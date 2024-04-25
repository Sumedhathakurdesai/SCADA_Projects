using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for TankDetailsView.xaml
    /// </summary>
    public partial class TankDetailsView : ChromelessWindow
    {
        public TankDetailsView()
        {
            InitializeComponent();
            
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {

                Regex regex = new Regex("[^1-4]+"); e.Handled = regex.IsMatch(e.Text);
                ////bool A = regex.IsMatch(e.Text);

                e.Handled = regex.IsMatch(e.Text);
                //if (Convert.ToInt16(e.Text.ToString()) > 0 && Convert.ToInt16(e.Text.ToString()) < 5 && e.Text.ToString() != "")
                //{
                //    e.Handled = true;
                //}
                //else
                //{
                //    e.Handled = false;
                //}
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsView.cs TextBox_PreviewTextInput()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
    }
}
