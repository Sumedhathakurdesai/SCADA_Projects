using Syncfusion.Windows.Shared;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;


namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for TrendView.xaml
    /// </summary>
    public partial class TrendView : ChromelessWindow
    {
        public TrendView()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //var viewbox = new Viewbox();
                //viewbox.Child = TempTrendChart;
                //viewbox.Measure(TempTrendChart.RenderSize);
                //viewbox.Arrange(new Rect(new Point(0, 0), TempTrendChart.RenderSize));
                ////TempTrendChart.updUpdate(true, true); //force chart redraw
                //viewbox.UpdateLayout();




                SaveToPng(TempTrendChart, "chart.png");
                //png file was created at the root directory.
                PrintDialog myPrintDialog = new PrintDialog();
                if (myPrintDialog.ShowDialog() == true)
                {
                    myPrintDialog.PrintVisual(TempTrendChart, "Trend");
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            try
            {
                var encoder = new PngBitmapEncoder();
                EncodeVisual(visual, fileName, encoder);
                
            }
            catch { }
        }
        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            try
            {
                var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(visual);
                var frame = BitmapFrame.Create(bitmap);
                encoder.Frames.Add(frame);
                using (var stream = File.Create(fileName)) encoder.Save(stream);
            }
            catch { }
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            TempItemsSearchBox.Focus();
            CurrentItemsSearchBox.Focus();
        }

        private void BtnCurrentPrintTrend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveToPng(TempTrendChart, "chart.png");
                //png file was created at the root directory.
                PrintDialog myPrintDialog = new PrintDialog();
                if (myPrintDialog.ShowDialog() == true)
                {
                    //myPrintDialog.PrintVisual(CurrentTrendChart, "Trend");
                }
            }
            catch (Exception ex)
            {

            }
        }

        //current checkbox click
        private void CurrentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string CurrentStationName = ((System.Windows.Controls.ContentControl)sender).Content.ToString();       
                TrendViewModelobj.CurrentCheckedcmdClick(CurrentStationName);
            }
            catch (Exception ex)
            {

            }
        }

        private void CurrentCheckbox_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                string CurrentStationName = ((System.Windows.Controls.ContentControl)sender).Content.ToString();
                TrendViewModelobj.CurrentUnCheckedcmdClick(CurrentStationName);
            }
            catch (Exception ex)
            {

            }
        }

        private void TempertureCheckedcmdClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string CurrentStationName = ((System.Windows.Controls.ContentControl)sender).Content.ToString();
                TrendViewModelobj.TempertureCheckedcmdClick(CurrentStationName);
            }
            catch (Exception ex)
            {

            }
        }

        private void TempertureUnCheckedcmdClick(object sender, RoutedEventArgs e)
        {

            try
            {
                string CurrentStationName = ((System.Windows.Controls.ContentControl)sender).Content.ToString();
                TrendViewModelobj.TempertureUnCheckedcmdClick(CurrentStationName);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
