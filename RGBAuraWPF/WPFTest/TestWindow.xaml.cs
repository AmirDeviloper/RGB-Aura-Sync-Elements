using RGBAuraWPF.Options;
using RGBAuraWPF;
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
using System.Xml.Linq;

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private List<Task> _elements;

        public TestWindow()
        {
            InitializeComponent();
            _elements = new List<Task>();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RGBAuraSync.StartAll();
            _elements.Add(lbl1.RGBOneColorAsync(OneColor.Red2Yellow, Options.RGBAura.Speed.Normal, transportType: Options.RGBAura.TransportType.Recursive));
            _elements.Add(lbl2.RGBOneColorAsync(OneColor.Green2Cyan, Options.RGBAura.Speed.Fast, transportType: Options.RGBAura.TransportType.Normal));
            _elements.Add(lbl3.RGBSpectrumAsync(Options.RGBAura.Speed.Normal, transportType: Options.RGBAura.TransportType.Recursive, colorSection: ColorSection.ForeColor));
            _elements.Add(lbl3.RGBSpectrumAsync(Options.RGBAura.Speed.Fast, transportType: Options.RGBAura.TransportType.Recursive, colorSection: ColorSection.BackColor));
            _elements.Add(lbl4.RGBSpectrumAsync(Options.RGBAura.Speed.Slow, transportType: Options.RGBAura.TransportType.Normal));
            _elements.Add(lbl5.RGBSpectrumAsync(Options.RGBAura.Speed.Extreme, transportType: Options.RGBAura.TransportType.Normal, colorSection: ColorSection.BackColor));
            _elements.Add(lbl6.RGBSpectrumAsync(Options.RGBAura.Speed.Ultra, transportType: Options.RGBAura.TransportType.Normal, colorSection: ColorSection.ForeColor)) ;

            await Task.WhenAny(_elements);
        }
    }
}
