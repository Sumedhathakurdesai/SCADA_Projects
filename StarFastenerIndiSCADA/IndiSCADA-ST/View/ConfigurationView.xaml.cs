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
using System.Configuration;
using System.Text.RegularExpressions;

namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView : ChromelessWindow
    {
        public ConfigurationView()
        {
            InitializeComponent();
            IndiSCADAGlobalLibrary.TagList.DataLogDebug = true; // start debug mode

        }

        private void TempItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFormula_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                FormulaView formula = new FormulaView();
                formula.ShowDialog();
            }
            catch { }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+");
                if (regex.IsMatch(e.Text))
                {
                    float val = Convert.ToSingle(e.Text);
                    if ((val >= 0) && (val < 100))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                    e.Handled = regex.IsMatch(e.Text);

            }
            catch { }

        }
    }
}
