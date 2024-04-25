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
    /// Interaction logic for ORSettingsView.xaml
    /// </summary>
    public partial class ORSettingsView :  ChromelessWindow
    {
        public ORSettingsView()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBox_PreviewTextInputFloat(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+.");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
    
}
